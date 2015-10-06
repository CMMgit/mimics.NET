Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Threading


Public Class frmMonitor

    Private onColor As Color = Color.Red
    Private offColor As Color = Color.DimGray
    Private disColor As Color = Color.WhiteSmoke
    Private blnTrig As Boolean = False
    Private WithEvents cls As New clsTCP
    Private MySqlCon As MySqlClient.MySqlConnection
    Private strSql As String
    Private szDatagram As String
    Private blnMonitorVisible As Boolean = True

    'Listviews and node status's are all updated by reading the MySQL table and getting saved data when loading  
    'and on tmrRefresh
    'In response to an event e.g. button press or accelerometer excedance etc, a UDP broadcast will be sent
    'in which case the list views are updated from the UDP transmission - if the Auto select box is checked or
    'if the auto select box is not checked but the UDP datagram comes from the IP that is currently selected
    'Interface any event, a received UDP datagram is always written to the MySql database
    '
    'For info:  activeTree.SelectedNode = activeTree.Nodes(0)

    Private Sub frmControlCentral_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Me.pnlError.Width = 877
            Me.pnlError.Height = 134
            Me.pnlError.Location = New System.Drawing.Point(12, 452)

            Dim strIPAddress As String = GetIpV4() 'establish what the local PC IP address is in order to work out the subnet
            Dim intPeriod As Integer = strIPAddress.LastIndexOf(".") + 1 'establish the where last quad of the local IP address is
            'strSubnet = Microsoft.VisualBasic.Left(strIPAddress, intPeriod) 'store the subnet
            Dim thisIP As String = Microsoft.VisualBasic.Right(strIPAddress, Len(strIPAddress) - intPeriod) 'maybe we can use the local IP address somewhere later on?

            Dim MysqlConnString As String = GetIni("MysqlConnString")
            MySqlCon = New MySqlClient.MySqlConnection(MysqlConnString)
            'MySqlCon.Open()

            Dim p As Integer = (MysqlConnString.IndexOf("Source=") + 8)
            Dim q As Integer = (MysqlConnString.IndexOf(";Database") + 1)
            strMySqlDb = Mid(MysqlConnString, p, q - p)

            Me.Text = "Mimics Monitor (MySql DB: " & strMySqlDb & ")"

            Dim strFractions As String = GetIni("Fractions")
            If strFractions.ToUpper = "FALSE" Then blnFractions = False
            If strFractions.ToUpper = "TRUE" Then blnFractions = True

            initForm()

            'Start the timer that will, in turn, start the listenUDP thread 100mS after this - gives time for the form load event to finish
            tmrLoad.Enabled = True

        Catch ex As Exception
            errorPanelsource("Form load")
            errorPanel(ex.Message)
        End Try

    End Sub
    Private Sub initForm()
        Try
            Dim n As Integer


            If strCustomer = "MBSA" Then
                Label4.Text = "Extruder"
                Me.Label5.Visible = True
                Me.treeView2.Visible = True
                Label1.Text = "Key MBSA:"
            End If

            If strCustomer = "Hadeco" Then
                Label4.Text = "Cold Room"
                Me.Label5.Visible = False
                Me.treeView2.Visible = False
                Me.treeView3.Location = New System.Drawing.Point(153, 117)
                Label1.Text = "Key Hadeco:"
            End If

            strSubnet = GetIni("Subnet")
            If (Microsoft.VisualBasic.Right(strSubnet, 1) <> ".") Then strSubnet = strSubnet & "."

            'Set the checkin refresh rate
            Dim lngRefreshRate = CLng(GetIni("RefreshRate"))
            'lngRefreshRate = 0      'FOR DEVELOPMENT
            If lngRefreshRate = 0 Then
                Me.tmrRefresh.Enabled = False
                Me.lblRefresh.Text = "Refresh disabled."
                Me.lblRefresh.Visible = True
            Else
                Me.tmrRefresh.Interval = (lngRefreshRate * 1000)
                Me.tmrRefresh.Enabled = True
                Me.lblRefresh.Text = "Refresh: " & CStr(lngRefreshRate) & " secs."
                Me.lblRefresh.Visible = True
            End If

            'Create and add nodes in treeview - one node per Mimics CPU
            Me.treeView1.Nodes.Clear()
            For n = 0 To (24)
                Me.treeView1.Nodes.Add(n, strSubnet & CStr(baseIP + n))
            Next

            Me.treeView2.Nodes.Clear()
            For n = 0 To (14)
                Me.treeView2.Nodes.Add(n, strSubnet & CStr((baseIP + 25) + n))
            Next

            Me.treeView3.Nodes.Clear()
            For n = 0 To (9)
                Me.treeView3.Nodes.Add(n, strSubnet & CStr((baseIP + 40) + n))
            Next

            'Label the nodes as an IP address or label depending on check box
            labelNodes()

            'Set the server and gateway nodes exclusively
            If (strCustomer = "MBSA") Then
                Me.treeView3.Nodes(0).ForeColor = Color.Red
                Me.treeView3.Nodes(0).ImageIndex = 6
                Me.treeView3.Nodes(0).SelectedImageIndex = 6
                Me.treeView3.Nodes(7).ForeColor = Color.Red
                Me.treeView3.Nodes(7).ImageIndex = 6
                Me.treeView3.Nodes(7).SelectedImageIndex = 6
            End If

            'Select the first node
            Me.treeView1.Focus()
            Me.treeView1.SelectedNode = treeView1.Nodes(0)
            'Fill_arrays_MySQL will be triggerred by selecting treeView1 here

            'Dim btn As New Button
            'btn.Size = New Size(75, 75)
            'btn.Location = New Point(269, 51)
            'Dim gp As New Drawing.Drawing2D.GraphicsPath
            'gp.AddEllipse(New Rectangle(New Point(2, 2), New Size(70, 70)))
            'btn.Region = New Region(gp)
            'Me.Controls.Add(btn)

        Catch ex As Exception
            errorPanelsource("Form load")
            errorPanel(ex.Message)
        End Try

    End Sub
    Private Sub tmrLoad_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrLoad.Tick

        Try
            'A one-shot event to start the listenUDP thread 10mS after form.load. Now disable the timer
            tmrLoad.Enabled = False

            ' Make a new udp object.
            Dim new_udp As New clsUDP(Me)
            ' Make threads to run the object's rebroadcastUDP & listenUDP methods.
            Dim rebroadcastUDP As New Thread(AddressOf new_udp.rebroadcastUDP)
            'Dim listenUDP As New Thread(AddressOf new_udp.listenUDP)

            ' Make this a background thread so it automatically aborts when the main program stops.
            rebroadcastUDP.IsBackground = True
            'listenUDP.IsBackground = True
            ' Start the thread.
            'listenUDP.Start()
            rebroadcastUDP.Start()

        Catch ex As Exception
            errorPanelsource("Form load timer")
            errorPanel(ex.Message)
        End Try

    End Sub
    Private Sub tmrRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrRefresh.Tick

        Dim strIp As String = Microsoft.VisualBasic.Right(Me.lblIP.Text, 3)
        Dim intIp As Integer = CInt(Replace(strIp, ".", ""))

        Fill_arrays_MySQL(intIp)

    End Sub
    Private Sub setNodesStatusFromMySQL()

        If blnMonitorVisible = False Then Exit Sub
        'Read MySQL for all data streams received today and set node status's accordingly
        Try
            Dim strToday As String = Now.ToString("yyyy-MM-dd")
            Dim dblTimeNow = dblTime(Now.ToString("HH:mm"))
            Dim n, intIP As Integer
            Dim strIp, strDateIn, strTimeIn, aryA(16) As String
            Dim dblTimeIn As Double
            Dim blnExceptions As Boolean = False

            strSql = "SELECT cmm.tblmimics_holding.ID, cmm.tblmimics_holding.strTime, cmm.tblmimics_holding.datDate, cmm.tblmimics_holding.strDevice" _
            & " FROM(cmm.tblmimics_holding)" _
            & " WHERE (((cmm.tblmimics_holding.datDate)='" & strToday & "'))" _
            & " ORDER BY cmm.tblmimics_holding.strDevice;"

            If (MySqlCon.State <> ConnectionState.Open) Then MySqlCon.Open()
            Using Com As New MySqlClient.MySqlCommand(strSql, MySqlCon)
                Using RDR = Com.ExecuteReader()
                    If RDR.HasRows Then
                        Do While RDR.Read
                            strDateIn = CDate(RDR.Item("datDate").ToString()).ToString("yyyy-MM-dd")
                            strTimeIn = CDate(RDR.Item("strTime").ToString()).ToString("HH:mm")
                            dblTimeIn = dblTime(strTimeIn)
                            strIp = Microsoft.VisualBasic.Right(RDR.Item("strDevice").ToString(), 3)
                            intIP = CInt(Replace(strIp, ".", ""))

                            If (strDateIn <> strToday) Then 'Before today = disconnected red cross
                                setNodeStatus(intIP, 0)
                            ElseIf ((strDateIn = strToday) And ((dblTimeNow - dblTimeIn) > (3 / 60))) Then 'Today but more than 3 minutes old = houglass faded green
                                setNodeStatus(intIP, 7)
                            ElseIf ((strDateIn = strToday) And ((dblTimeNow - dblTimeIn) <= (3 / 60)) And (blnExceptions = False)) Then 'Today and within 3 minutes without exceptions = connected green tick
                                setNodeStatus(intIP, 1)
                            ElseIf ((strDateIn = strToday) And ((dblTimeNow - dblTimeIn) <= (3 / 60)) And (blnExceptions = True)) Then  'Today and within 3 minutes with exceptions = connected light bulb amber
                                setNodeStatus(intIP, 2)
                            End If
                        Loop
                        RDR.Close()
                    End If
                End Using
            End Using
            If (MySqlCon.State <> ConnectionState.Closed) Then MySqlCon.Close()

            'MySQL_to_arrays(intIP)

        Catch ex As Exception
            errorPanelsource("setNodeStatusFromMySQL()")
            errorPanel(ex.Message)
        End Try
    End Sub
    Private Sub setNodeStatus(ByVal intIP As Integer, ByVal intStatus As Integer)

        'Sets the status of the node without actually setting focus to the node

        Try
            If (intIP < baseIP Or intIP > (baseIP + 50)) Then Exit Sub

            Dim Tree As TreeView
            Dim intNode As Integer = (intIP - baseIP)

            If (intIP >= baseIP And intIP <= (baseIP + 24)) Then
                Tree = Me.treeView1
                intNode = (intIP - baseIP)
            End If
            If (intIP >= (baseIP + 25) And intIP <= (baseIP + 40)) Then
                Tree = Me.treeView2
                intNode = intNode - 25
            End If
            If (intIP >= (baseIP + 40) And intIP <= (baseIP + 50)) Then
                Tree = Me.treeView3
                intNode = intNode - 40
            End If

            Dim ndeColor As Color = Color.Red
            If intStatus = 0 Then ndeColor = Color.Red
            If intStatus = 1 Then ndeColor = Color.Green
            If intStatus = 2 Then ndeColor = Color.Orange
            If intStatus = 7 Then ndeColor = Color.DarkSeaGreen

            Tree.Nodes(intNode).ForeColor = ndeColor
            Tree.Nodes(intNode).ImageIndex = intStatus
            Tree.Nodes(intNode).SelectedImageIndex = intStatus

        Catch ex As Exception
            errorPanelsource("setNodeStatus")
            errorPanel(ex.Message)
        End Try
    End Sub
    Private Function peripheralLabel(ByVal strData As String)

        '6043ED150000 Pelitizer 1
        'E80737140000 Pelitizer 2
        '124CED150000 Pelitizer 3
        'aryPeripherals(0, 0) = "6043ED150000"
        'aryPeripherals(0, 1) = "Pelitizer 1"
        'aryPeripherals(1, 0) = "E80737140000"
        'aryPeripherals(1, 1) = "Pelitizer 2"
        'aryPeripherals(2, 0) = "E80737140000"
        'aryPeripherals(2, 1) = "Pelitizer 3"

        Dim x As Integer
        Dim strResult As String = "Peripheral"

        Try

            For x = 0 To 49
                If (strData = aryPeripherals(x, 0)) Then strResult = aryPeripherals(x, 1)
            Next

            Return strResult

        Catch ex As Exception
            errorPanelsource("peripheralLabel")
            errorPanel(ex.Message)
        End Try
    End Function
    Private Sub Fill_arrays_MySQL(ByVal intIP As Integer)

        'Scan all IP's to update node status
        setNodesStatusFromMySQL()

        'Populate and listviews from the MySQL table
        Try
            If intIP = 0 Then Exit Sub

            Me.lblIdent1.Visible = True
            Dim aryAnalog(15) As String
            Dim aryDigitalInp(11) As String
            Dim aryDigitalOut(11) As String
            Dim aryPeripheral(2) As String
            Dim aryAccelerometer(11) As String
            Dim n As Integer

            'Read in the data from MySQL
            Dim strIp As String = strSubnet & CStr(intIP)
            Dim strDateIn As String = Nothing
            Dim strTimeIn As String = Nothing
            Dim dblTimeIn As Double
            Dim strToday As String = Now.ToString("yyyy-MM-dd")
            Dim dblTimeNow As Double = dblTime(Now.ToString("HH:mm"))

            strSql = "SELECT * FROM cmm.tblmimics_holding WHERE (strDevice = '" & strIp & "');"
            If (MySqlCon.State <> ConnectionState.Open) Then MySqlCon.Open()
            Using Com As New MySqlClient.MySqlCommand(strSql, MySqlCon)
                Using RDR = Com.ExecuteReader()
                    If RDR.HasRows Then
                        Do While RDR.Read
                            strDateIn = CDate(RDR.Item("datDate").ToString()).ToString("yyyy-MM-dd")
                            strTimeIn = CDate(RDR.Item("strTime").ToString()).ToString("HH:mm:ss")
                            dblTimeIn = dblTime(strTimeIn)
                            aryAnalog(0) = RDR.Item("A0").ToString()
                            aryAnalog(1) = RDR.Item("A1").ToString()
                            aryAnalog(2) = RDR.Item("A2").ToString()
                            aryAnalog(3) = RDR.Item("A3").ToString()
                            aryAnalog(4) = RDR.Item("A4").ToString()
                            aryAnalog(5) = RDR.Item("A5").ToString()
                            aryAnalog(6) = RDR.Item("A6").ToString()
                            aryAnalog(7) = RDR.Item("A7").ToString()
                            aryAnalog(8) = RDR.Item("A8").ToString()
                            aryAnalog(9) = RDR.Item("A9").ToString()
                            aryAnalog(10) = RDR.Item("A10").ToString()
                            aryAnalog(11) = RDR.Item("A11").ToString()
                            aryAnalog(12) = RDR.Item("A12").ToString()
                            aryAnalog(13) = RDR.Item("A13").ToString()
                            aryAnalog(14) = RDR.Item("A14").ToString()
                            aryAnalog(15) = RDR.Item("A15").ToString()

                            aryDigitalOut(0) = RDR.Item("L1").ToString()
                            aryDigitalOut(1) = RDR.Item("L2").ToString()
                            aryDigitalOut(2) = RDR.Item("L3").ToString()
                            aryDigitalOut(3) = RDR.Item("L4").ToString()
                            aryDigitalOut(4) = RDR.Item("D0").ToString()
                            aryDigitalOut(5) = RDR.Item("D1").ToString()
                            aryDigitalOut(6) = RDR.Item("D2").ToString()
                            aryDigitalOut(7) = RDR.Item("D3").ToString()
                            aryDigitalOut(8) = RDR.Item("D4").ToString()
                            aryDigitalOut(9) = RDR.Item("D5").ToString()
                            aryDigitalOut(10) = RDR.Item("D6").ToString()
                            aryDigitalOut(11) = RDR.Item("D7").ToString()

                            aryDigitalInp(0) = RDR.Item("B1").ToString()     'TODO: NodeToIP longer button inputs - now a menu system
                            aryDigitalInp(1) = RDR.Item("B2").ToString()     'TODO: NodeToIP longer button inputs - now a menu system
                            aryDigitalInp(2) = RDR.Item("B3").ToString()     'TODO: NodeToIP longer button inputs - now a menu system
                            aryDigitalInp(3) = RDR.Item("B4").ToString()     'TODO: NodeToIP longer button inputs - now a menu system
                            aryDigitalInp(4) = RDR.Item("D8").ToString()
                            aryDigitalInp(5) = RDR.Item("D9").ToString()
                            aryDigitalInp(6) = RDR.Item("D10").ToString()
                            aryDigitalInp(7) = RDR.Item("D11").ToString()
                            aryDigitalInp(8) = RDR.Item("D12").ToString()
                            aryDigitalInp(9) = RDR.Item("D13").ToString()
                            aryDigitalInp(10) = RDR.Item("D14").ToString()
                            aryDigitalInp(11) = RDR.Item("D15").ToString()

                            aryPeripheral(0) = RDR.Item("Peripheral_1").ToString()
                            aryPeripheral(1) = RDR.Item("Peripheral_2").ToString()
                            aryPeripheral(2) = RDR.Item("Peripheral_3").ToString()

                            aryAccelerometer(0) = RDR.Item("x_max").ToString()
                            aryAccelerometer(1) = RDR.Item("x_min").ToString()
                            aryAccelerometer(2) = RDR.Item("y_max").ToString()
                            aryAccelerometer(3) = RDR.Item("y_min").ToString()
                            aryAccelerometer(4) = RDR.Item("z_max").ToString()
                            aryAccelerometer(5) = RDR.Item("z_min").ToString()

                            aryAccelerometer(6) = RDR.Item("x_max_2").ToString()
                            aryAccelerometer(7) = RDR.Item("x_min_2").ToString()
                            aryAccelerometer(8) = RDR.Item("y_max_2").ToString()
                            aryAccelerometer(9) = RDR.Item("y_min_2").ToString()
                            aryAccelerometer(10) = RDR.Item("z_max_2").ToString()
                            aryAccelerometer(11) = RDR.Item("z_min_2").ToString()

                        Loop
                        RDR.Close()
                    End If
                End Using
            End Using
            If (MySqlCon.State <> ConnectionState.Closed) Then MySqlCon.Close()

            'Date time stamp
            If (strDateIn <> Now.ToString("yyyy-MM-dd")) Then
                Me.lblDateTimeStamp.ForeColor = Color.Red
            ElseIf ((strDateIn = strToday) And ((dblTimeNow - dblTimeIn) > (3 / 60))) Then 'Today but more than 3 minutes old = houglass faded green
                Me.lblDateTimeStamp.ForeColor = Color.DarkSeaGreen
            ElseIf ((strDateIn = strToday) And ((dblTimeNow - dblTimeIn) <= (3 / 60))) Then 'Today but more than 3 minutes old = houglass faded green
                Me.lblDateTimeStamp.ForeColor = Color.Green
            End If

            'If (dblTimeIn > dblTimeNow) Then strTimeIn = Now.ToString("HH:mm:ss")
            Me.lblDateTimeStamp.Text = "Last update:      " & strDateIn & "        " & strTimeIn

            populateListviews(intIP, aryAnalog, aryDigitalOut, aryDigitalInp, aryPeripheral, aryAccelerometer)

        Catch ex As Exception
            Dim strErrMsg As String = ex.Message
            'MsgBox(strErrMsg)
            If (strErrMsg = "There is already an open DataReader associated with this Connection which must be closed first.") Then Exit Sub
            errorPanelsource("MySQL to arrays")
            errorPanel(strErrMsg)
        End Try
    End Sub
    Private Sub populateListviews(ByVal intIP As Integer, ByVal aryAnalog() As String, ByVal aryDigitalOut() As String, ByVal aryDigitalInp() As String, ByVal aryPeripheral() As String, ByVal aryAccelerometer() As String)

        Try
            'Now the data is held in the arrays - use them to populate
            Dim strForecolour As String = "Black"
            Dim intNode As Integer = (intIP - baseIP)

            If intIP = 130 Or intIP = 137 Then
                Me.lstViewA.Visible = False
                Me.lstViewB.Visible = False
                Me.lstViewC.Visible = False
                Me.lstViewD.Visible = False
                Me.lstViewF.Visible = False
                Me.lblDateTimeStamp.Visible = False
                Me.lblDisconnect.Visible = False
                Exit Sub
            Else
                Me.lstViewA.Visible = True
                Me.lstViewB.Visible = True
                Me.lstViewC.Visible = True
                Me.lstViewD.Visible = True
                Me.lstViewF.Visible = True
                Me.lblDateTimeStamp.Visible = True
                Me.lblDisconnect.Visible = True
            End If

            'Set LED's----------------------------------------------------------------------------------------------------------------
            If (Me.lblDateTimeStamp.ForeColor = Color.Green Or Me.lblDateTimeStamp.ForeColor = Color.DarkSeaGreen) Then
                lblDisconnect.Visible = False
            Else
                lblDisconnect.Visible = True
            End If
            '-------------------------------------------------------------------------------------------------------------------------
            'Set the labelling
            Dim aryAnalogLabels(15) As String
            Select Case intNode
                Case 0 To 24
                    If strCustomer = "MBSA" Then aryAnalogLabels = aryAnalogLabels_1
                    If strCustomer = "Hadeco" Then aryAnalogLabels = aryAnalogLabels_5
                Case 25 To 39
                    aryAnalogLabels = aryAnalogLabels_2
                Case 40
                    aryAnalogLabels = aryAnalogLabels_4
                Case 41
                    aryAnalogLabels = aryAnalogLabels_5 'MBSA Server Roon
                Case 42 To 48
                    aryAnalogLabels = aryAnalogLabels_4
                Case 49
                    aryAnalogLabels = aryAnalogLabels_3
            End Select

            Dim aryDigitalLabels(11, 3) As String
            Dim aryActiveRedInput(11) As String
            Dim aryDigitalOutLabels(11, 3) As String
            Dim aryActiveRedOutput(11) As String
            Select Case intNode
                Case 0 To 24
                    If strCustomer = "MBSA" Then aryDigitalLabels = aryDigitalLabels_1
                    If strCustomer = "MBSA" Then aryActiveRedInput = aryActiveRedInput_1
                    If strCustomer = "Hadeco" Then aryDigitalLabels = aryDigitalLabels_5
                    If strCustomer = "Hadeco" Then aryActiveRedInput = aryActiveRedInput_5
                    If strCustomer = "Hadeco" Then aryDigitalOutLabels = aryDigitalOutLabels_5
                    If strCustomer = "Hadeco" Then aryActiveRedOutput = aryActiveRedOutput_5
                Case 25 To 39
                    aryDigitalLabels = aryDigitalLabels_2
                    aryActiveRedInput = aryActiveRedInput_2
                    aryDigitalOutLabels = aryDigitalOutLabels_2
                    aryActiveRedOutput = aryActiveRedOutput_2
                Case 40
                    aryDigitalLabels = aryDigitalLabels_4
                    aryActiveRedInput = aryActiveRedInput_4
                    aryDigitalOutLabels = aryDigitalOutLabels_4
                    aryActiveRedOutput = aryActiveRedOutput_4
                Case 41
                    aryDigitalLabels = aryDigitalLabels_5           'MBSA Server room
                    aryActiveRedInput = aryActiveRedInput_5
                    aryDigitalOutLabels = aryDigitalOutLabels_3
                    aryActiveRedOutput = aryActiveRedOutput_3
                Case 42 To 48
                    aryDigitalLabels = aryDigitalLabels_4
                    aryActiveRedInput = aryActiveRedInput_4
                    aryDigitalOutLabels = aryDigitalOutLabels_4
                    aryActiveRedOutput = aryActiveRedOutput_4
                Case 49
                    aryDigitalLabels = aryDigitalLabels_3
                    aryActiveRedInput = aryActiveRedInput_3
                    aryDigitalOutLabels = aryDigitalOutLabels_3
                    aryActiveRedOutput = aryActiveRedOutput_3
            End Select

            'Show and populate the list views
            Me.lstViewA.Items.Clear()
            For n = 0 To 15
                addItems(Me.lstViewA, aryAnalogLabels(n), aryAnalog(n), strForecolour)
            Next

            Me.lstViewB.Items.Clear() 'Start at 4 to leave out buttons (0 - 3) - now unused
            For n = 4 To 11
                If (aryActiveRedInput(n) Is Nothing) Then
                    strForecolour = "Black"
                ElseIf (aryDigitalInp(n) = CInt(aryActiveRedInput(n))) Then
                    strForecolour = "Red"
                Else
                    strForecolour = "Black"
                End If

                If aryDigitalInp(n) Is Nothing Then
                    addItems(Me.lstViewB, aryDigitalLabels(n, 2), Nothing, "Black")
                Else
                    addItems(Me.lstViewB, aryDigitalLabels(n, 2), aryDigitalLabels(n, aryDigitalInp(n)), strForecolour)
                End If
            Next

            'If strCustomer = "Hadeco" Then
            '    addItems(Me.lstViewB, Nothing, Nothing, "Black")
            '    addItems(Me.lstViewB, Nothing, Nothing, "Black")
            '    addItems(Me.lstViewB, Nothing, Nothing, "Black")
            '    addItems(Me.lstViewB, "Hadeco Cycle Start", "2015-02-15", "Black")
            'End If

            Me.lstViewF.Items.Clear()
            For n = 0 To 11
                If (aryActiveRedOutput(n) Is Nothing) Then
                    strForecolour = "Black"
                ElseIf (aryDigitalOut(n) = CInt(aryActiveRedOutput(n))) Then
                    strForecolour = "Red"
                Else
                    strForecolour = "Black"
                End If

                If aryDigitalOut(n) Is Nothing Then
                    addItems(Me.lstViewF, aryDigitalOutLabels(n, 2), Nothing, "Black")
                Else
                    addItems(Me.lstViewF, aryDigitalOutLabels(n, 2), aryDigitalOutLabels(n, aryDigitalOut(n)), strForecolour)
                End If
            Next

            Me.lstViewC.Items.Clear()
            If (aryPeripheral(0) = "000000000000" And aryPeripheral(1) = "000000000000" And aryPeripheral(2) = "000000000000") Then
                Me.lstViewC.Visible = False
            ElseIf Len(aryPeripheral(0)) + Len(aryPeripheral(1)) + Len(aryPeripheral(2)) > 0 Then
                addItems(Me.lstViewC, peripheralLabel(aryPeripheral(0)), aryPeripheral(0), "Black")
                addItems(Me.lstViewC, peripheralLabel(aryPeripheral(1)), aryPeripheral(1), "Black")
                addItems(Me.lstViewC, peripheralLabel(aryPeripheral(2)), aryPeripheral(2), "Black")
                Me.lstViewC.Visible = True
            Else
                Me.lstViewC.Visible = False
            End If

            'Acceleromter 0xAA
            Me.lstViewD.Items.Clear()
            If (aryAccelerometer(0) = "0" And aryAccelerometer(1) = "0" And aryAccelerometer(2) = "0" And aryAccelerometer(3) = "0" _
                And aryAccelerometer(4) = "0" And aryAccelerometer(5) = "0") Then
                Me.lstViewD.Visible = False
            ElseIf Len(aryAccelerometer(0)) + Len(aryAccelerometer(1)) + Len(aryAccelerometer(2)) + Len(aryAccelerometer(3)) + _
            Len(aryAccelerometer(4)) + Len(aryAccelerometer(5)) > 0 Then
                addItems_2(Me.lstViewD, "X", aryAccelerometer(0), aryAccelerometer(1), "Black")
                addItems_2(Me.lstViewD, "Y", aryAccelerometer(2), aryAccelerometer(3), "Black")
                addItems_2(Me.lstViewD, "Z", aryAccelerometer(4), aryAccelerometer(5), "Black")
                Me.lstViewD.Visible = True
            Else
                Me.lstViewD.Visible = False
            End If

            'Acceleromter 0xAB
            Me.lstViewE.Items.Clear()
            If (aryAccelerometer(6) = "0" And aryAccelerometer(7) = "0" And aryAccelerometer(8) = "0" And aryAccelerometer(9) = "0" _
                And aryAccelerometer(10) = "0" And aryAccelerometer(11) = "0") Then
                Me.lstViewE.Visible = False
            ElseIf Len(aryAccelerometer(6)) + Len(aryAccelerometer(7)) + Len(aryAccelerometer(8)) + Len(aryAccelerometer(9)) + _
            Len(aryAccelerometer(10)) + Len(aryAccelerometer(11)) > 0 Then
                addItems_2(Me.lstViewE, "X", aryAccelerometer(6), aryAccelerometer(7), "Black")
                addItems_2(Me.lstViewE, "Y", aryAccelerometer(8), aryAccelerometer(9), "Black")
                addItems_2(Me.lstViewE, "Z", aryAccelerometer(10), aryAccelerometer(11), "Black")
                Me.lstViewE.Visible = True
            Else
                Me.lstViewE.Visible = False
            End If

        Catch ex As Exception
            errorPanelsource("populateListviews")
            errorPanel(ex.Message)
        End Try

    End Sub
    Private Sub addItems(ByVal lstView As ListView, ByVal strSource As String, ByVal strData As String, ByVal strForeColour As String)

        Try
            If strSource = Nothing Then strData = Nothing 'No label means no data
            Dim ListViewItem1 As New ListViewItem
            Dim subItem1 As New ListViewItem.ListViewSubItem
            ListViewItem1 = New ListViewItem(strSource)
            subItem1 = New ListViewItem.ListViewSubItem
            subItem1.Text = (strData)

            ListViewItem1.UseItemStyleForSubItems = False
            If strForeColour = "Red" Then
                ListViewItem1.ForeColor = Color.Red
                subItem1.ForeColor = Color.Red
            Else
                ListViewItem1.ForeColor = Color.Black
                subItem1.ForeColor = Color.Black
            End If

            ListViewItem1.SubItems.Add(subItem1)
            lstView.Items.Add(ListViewItem1)

        Catch ex As Exception
            errorPanelsource("addItems")
            errorPanel(ex.Message)
        End Try


    End Sub
    Private Sub addItems_2(ByVal lstView As ListView, ByVal strSource As String, ByVal strData_1 As String, ByVal strData_2 As String, ByVal strForeColour As String)

        Try
            If strSource = Nothing Then strData_1 = Nothing 'No label means no data
            Dim ListViewItem1 As New ListViewItem
            Dim subItem1 As New ListViewItem.ListViewSubItem
            Dim subItem2 As New ListViewItem.ListViewSubItem
            ListViewItem1 = New ListViewItem(strSource)
            subItem1 = New ListViewItem.ListViewSubItem
            subItem2 = New ListViewItem.ListViewSubItem
            subItem1.Text = (strData_1)
            subItem2.Text = (strData_2)

            If (CInt(strData_1) > 1500 Or CInt(strData_2) > 1500) Then strForeColour = "Red"

            ListViewItem1.UseItemStyleForSubItems = False

            If strForeColour = "Red" Then
                ListViewItem1.ForeColor = Color.Red
                subItem1.ForeColor = Color.Red
                subItem2.ForeColor = Color.Red
            Else
                ListViewItem1.ForeColor = Color.Black
                subItem1.ForeColor = Color.Black
                subItem2.ForeColor = Color.Black
            End If

            ListViewItem1.SubItems.Add(subItem1)
            ListViewItem1.SubItems.Add(subItem2)
            lstView.Items.Add(ListViewItem1)

        Catch ex As Exception
            errorPanelsource("addItems")
            errorPanel(ex.Message)
        End Try


    End Sub
    Private Function btnColor(ByVal strX As String) As Color

        If strX Is Nothing Then Return disColor
        If strX = "0" Then Return offColor
        If strX = "1" Then Return onColor

    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Visible = False
    End Sub

    Private Sub treeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles treeView1.AfterSelect
        processSelectedTreeview(Me.treeView1)
    End Sub

    Private Sub treeView2_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles treeView2.AfterSelect
        processSelectedTreeview(Me.treeView2)
    End Sub

    Private Sub treeView3_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles treeView3.AfterSelect
        processSelectedTreeview(Me.treeView3)
    End Sub
    Private Sub processSelectedTreeview(ByVal Tree As TreeView)

        Try
            If Tree.SelectedNode Is Nothing Then Exit Sub
            Dim strSelectedNode As String = Tree.SelectedNode.ToString

            Dim intList As Integer
            If Tree.Name = "treeView1" Then intList = 0
            If Tree.Name = "treeView2" Then intList = 25
            If Tree.Name = "treeView3" Then intList = 40

            Dim intIP As Integer = baseIP + Tree.SelectedNode.Index + intList
            Me.lblIP.Text = strSubnet & CStr(intIP)
            strCurrentIP = strSubnet & CStr(intIP)
            Me.lblName.Text = Tree.SelectedNode.Text

            Fill_arrays_MySQL(intIP)

        Catch ex As Exception
            errorPanelsource("Treeview_Selected")
            errorPanel(ex.Message)
        End Try

    End Sub

    Private Sub treeView1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles treeView1.GotFocus
        Me.treeView2.SelectedNode = Nothing
        Me.treeView3.SelectedNode = Nothing
    End Sub


    Private Sub treeView2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles treeView2.GotFocus
        Me.treeView1.SelectedNode = Nothing
        Me.treeView3.SelectedNode = Nothing
    End Sub


    Private Sub treeView3_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles treeView3.GotFocus
        Me.treeView1.SelectedNode = Nothing
        Me.treeView2.SelectedNode = Nothing
    End Sub
    Private Sub IPtoSetTreeAndNodeFocus(ByVal intIP As Integer)

        'Given an IP address - the treeview and node are selected and focused

        Try
            Me.treeView1.SelectedNode = Nothing
            Me.treeView2.SelectedNode = Nothing
            Me.treeView3.SelectedNode = Nothing
            Me.btnRefresh.Focus()

            Dim Tree As TreeView = Me.treeView1
            Dim intNode As Integer = (intIP - baseIP)

            If (intIP >= baseIP And intIP <= (baseIP + 24)) Then
                Tree = Me.treeView1
                intNode = (intIP - baseIP)
            End If
            If (intIP >= (baseIP + 25) And intIP <= (baseIP + 40)) Then
                Tree = Me.treeView2
                intNode = intNode - 25
            End If
            If (intIP >= (baseIP + 40) And intIP <= (baseIP + 50)) Then
                Tree = Me.treeView3
                intNode = intNode - 40
            End If

            Tree.SelectedNode = Tree.Nodes(intNode)
            Tree.Focus()

        Catch ex As Exception
            errorPanelsource("IPtoNode")
            errorPanel(ex.Message)
        End Try

    End Sub
    Private Function IPtoTree(ByVal intIP As Integer) As TreeView

        'Given an IP address - the treeview in which it is is returned

        Try
            Dim intNode As Integer = (intIP - baseIP)

            If (intIP >= baseIP And intIP <= (baseIP + 24)) Then
                Return Me.treeView1
                intNode = (intIP - baseIP)
            End If
            If (intIP >= (baseIP + 25) And intIP <= (baseIP + 40)) Then
                Return Me.treeView2
                intNode = intNode - 25
            End If
            If (intIP >= (baseIP + 40) And intIP <= (baseIP + 50)) Then
                Return Me.treeView3
                intNode = intNode - 40
            End If

            Return Nothing

        Catch ex As Exception
            errorPanelsource("IPtoNode")
            errorPanel(ex.Message)
        End Try

    End Function
    Private Function IPtoNode(ByVal intIP As Integer) As Integer

        'Returns the node number within a treeview for a given IP address

        Try
            Dim intNode As Integer = (intIP - baseIP)

            If (intIP >= baseIP And intIP <= (baseIP + 24)) Then
                intNode = (intIP - baseIP)
            End If
            If (intIP >= (baseIP + 25) And intIP <= (baseIP + 40)) Then
                intNode = intNode - 25
            End If
            If (intIP >= (baseIP + 40) And intIP <= (baseIP + 50)) Then
                intNode = intNode - 40
            End If

            Return intNode

        Catch ex As Exception
            errorPanelsource("IPtoNode")
            errorPanel(ex.Message)
        End Try

    End Function
    Private Function NodeToIP(ByVal intNode As Integer, ByVal Tree As TreeView) As Integer

        'Given a treeview and node therein, an IP address is calculated

        Try
            Dim intIP, baseNode As Integer
            If Tree.Name = "treeView1" Then baseNode = 0
            If Tree.Name = "treeView2" Then baseNode = 25
            If Tree.Name = "treeView3" Then baseNode = 40

            intIP = baseIP + baseNode + intNode

            Return intIP

        Catch ex As Exception
            errorPanelsource("NodeToIP")
            errorPanel(ex.Message)
        End Try

    End Function

    Private Sub treeView2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles treeView2.KeyDown
        If e.KeyCode = Keys.Return Then

        End If
    End Sub

    Private Sub chkIPs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIPs.CheckedChanged

        labelNodes()
        Dim strIp As String = Microsoft.VisualBasic.Right(Me.lblIP.Text, 3)
        Dim intIp As Integer = CInt(Replace(strIp, ".", ""))

        Dim Tree As TreeView = IPtoTree(intIp)
        If Not IsNothing(Tree.SelectedNode) Then Me.lblName.Text = Tree.SelectedNode.Text

    End Sub
    Private Sub labelNodes()

        Try

            Dim n As Integer
            If (chkIPs.Checked = False) Then
                For n = 0 To 24
                    Me.treeView1.Nodes(n).Text = GetIni("IP.Subnet." & CStr(baseIP + n))
                Next
                For n = 0 To 14
                    Me.treeView2.Nodes(n).Text = GetIni("IP.Subnet." & CStr((baseIP + 25) + n))
                Next
                For n = 0 To 9
                    Me.treeView3.Nodes(n).Text = GetIni("IP.Subnet." & CStr((baseIP + 40) + n))
                Next
            Else
                For n = 0 To 24
                    Me.treeView1.Nodes(n).Text = strSubnet & CStr(baseIP + n)
                Next
                For n = 0 To 14
                    Me.treeView2.Nodes(n).Text = strSubnet & CStr((baseIP + 25) + n)
                Next
                For n = 0 To 9
                    Me.treeView3.Nodes(n).Text = strSubnet & CStr((baseIP + 40) + n)
                Next
            End If


        Catch ex As Exception
            errorPanelsource("labelNodes")
            errorPanel(ex.Message)
        End Try

    End Sub

    Private Sub receiveStatusMsg(ByVal strInput As String)

        'Monitor  : 31030906010002090409B3FF58434D4D204D696D69637300000000000000000000000000000000000000000000FFFF
        'strInput : 31030906010002090409B3FF58434D4D204D696D69637300000000000000000000000000000000000000000000FF"
        '           1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890
        '                    1         2         3         4         5         6         7         8         9         0
        'C  M  M     M  i  m  i  c  s
        '43:4d:4d:20:4d:69:6d:69:63:73

        Try
            Dim strUnix As String
            Dim lngUnix As Long

            Dim q As Integer
            For q = 2 To 20 Step 2
                strUnix += Mid(strInput, q, 1)
            Next

            If Len(strUnix) <> 10 Then strUnix = 0
            lngUnix = CLng(strUnix)
            If lngUnix = 0 Then lngUnix = 1388534400 '01/01/2014 00:00:00
            If lngUnix > 1388534400 Then lngUnix += 7200

            Dim strDate As String = mimicDate(lngUnix)
            Dim datDate As Date = CDate(strDate)
            Dim strTime As String = mimicTime(lngUnix)

            Dim strDevice As String = strSubnet & CStr(Convert.ToInt32(Mid(strInput, 21, 2), 16))

            Dim strCelsius As Integer = Convert.ToInt32(Mid(strInput, 23, 2), 16)
            Dim strWiFiStrength As Integer = Convert.ToInt32(Mid(strInput, 25, 2), 16)
            Dim strSSID As String = Convert.ToChar(Convert.ToUInt32(Mid(strInput, 27, 2), 16))
            Dim strHex As String
            Dim strChar As Char

            For q = 29 To Len(strInput) Step 2
                strHex = Mid(strInput, q, 2)
                If (strHex = "00" Or strHex = "FF") Then Exit For
                strChar = Convert.ToChar(Convert.ToUInt32(strHex, 16))
                strSSID = strSSID + strChar
            Next

            strSSID = Trim(strSSID)
            Dim strStatus As String = strDate & "  " & strTime & "  " & strDevice & " SSID " & ": " & strSSID & " [Strength: " & strWiFiStrength & "]   [Temperature: " & strCelsius & "]"

            rtbStatus.SelectionColor = Color.Blue
            rtbStatus.SelectedText = strStatus & vbCrLf
            rtbStatus.SelectionStart = rtbStatus.Text.Length
            rtbStatus.ScrollToCaret()

        Catch ex As Exception
            errorPanelsource("receiveStatusMsg")
            errorPanel(ex.Message)
        End Try

    End Sub
    Private Sub setNodeFocus()

        'Image 0 = red
        'Image 1 = green
        'Image 2 = amber
        Try
            'Scan each tree and set the focus on a node that is amber - and if not then go for green
            Dim node As TreeNode
            For Each node In Me.treeView1.Nodes
                Me.treeView1.Focus()
                If node.ImageIndex = 2 Then
                    Me.treeView1.SelectedNode = node
                    Exit Sub
                End If
            Next

            For Each node In Me.treeView2.Nodes
                Me.treeView2.Focus()
                If node.ImageIndex = 2 Then
                    Me.treeView1.SelectedNode = node
                    Exit Sub
                End If
            Next

            For Each node In Me.treeView1.Nodes
                Me.treeView3.Focus()
                If node.ImageIndex = 2 Then
                    Me.treeView1.SelectedNode = node
                    Exit Sub
                End If
            Next

            'No ambers found - so try for green
            For Each node In Me.treeView1.Nodes
                Me.treeView1.Focus()
                If node.ImageIndex = 1 Then
                    Me.treeView1.SelectedNode = node
                    Exit Sub
                End If
            Next

            For Each node In Me.treeView2.Nodes
                Me.treeView2.Focus()
                If node.ImageIndex = 1 Then
                    Me.treeView1.SelectedNode = node
                    Exit Sub
                End If
            Next

            For Each node In Me.treeView1.Nodes
                Me.treeView3.Focus()
                If node.ImageIndex = 1 Then
                    Me.treeView1.SelectedNode = node
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            errorPanelsource("setNodeFocus")
            errorPanel(ex.Message)
        End Try

    End Sub
    Private Sub errorPanelsource(ByVal strMsg As String)
        Me.lblError.Text = "Errors:"
        rtbError.SelectionColor = Color.Blue
        rtbError.SelectedText = strMsg & vbCrLf
        rtbError.SelectionStart = rtbError.Text.Length
        rtbError.ScrollToCaret()
    End Sub
    Private Sub errorPanel(ByVal strMsg As String)
        Me.lblError.Text = "Errors:"
        rtbError.SelectionColor = Color.Red
        rtbError.SelectedText = strMsg & vbCrLf
        rtbError.SelectionStart = rtbError.Text.Length
        rtbError.ScrollToCaret()
        pnlError.Visible = True
    End Sub
    Private Sub btnErrorReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnErrorReset.Click
        Me.rtbError.Text = Nothing
        Me.pnlError.Visible = False
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmCalculator
        frm.ShowDialog()
    End Sub
    Public Sub actionInput(ByVal strInput As String)

        '01030905070505080105AA0052005803270311032100FE011B011B00FE00F700FA010B0104010400F402E1000FFF6043ED150000E80737140000124CED150000
        '12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678
        '         1         2         3         4         5         6         7         8         9         0         1         2     

        'First character :  0 = Data logging message
        '                   1 = Receipt of transfer of all I/O's 
        '                   2 = xMessage
        '                   3 = Status message

        Try
            'Extract the sending IP id number
            Dim strFromIP As String = CStr(Convert.ToInt32(Mid(strInput, 21, 2), 16))
            Dim intIP As Integer = CInt(strFromIP)

            'Decode the incoming messsage - data inputs - and populate the data arrays
            If Microsoft.VisualBasic.Left(strInput, 1) = "0" Then '"0" indicator for a full data IO messsage (same as for data logging)
                Me.lblUDP.Visible = True
                tmrUDP.Enabled = True
                szDatagram = strInput
                Fill_arrays_UDP()
                blnUDP = True

            ElseIf Microsoft.VisualBasic.Left(strInput, 1) = "3" Then '"3" indicator for a status messsage
                receiveStatusMsg(strInput)
            End If

        Catch ex As Exception
            errorPanelsource("actionIput")
            errorPanel(ex.Message)
        End Try

    End Sub
    Private Sub Fill_arrays_UDP()

        If Microsoft.VisualBasic.Left(szDatagram, 2) = "31" Then Exit Sub 'if old style message arrives 

        'Receive UDP datagram and write to MySql database:
        '1: Write to MySql database
        '2: Update node status 
        '3: If autoselect then change node which will trigger fill_arrays_mysql
        '4: OR If received UDP equals currently selected IP then run fill_arrays_mysql
        '5: reset the refresh timer so the data persists for 60 secs (or whatever the refresh timer interval is set to)

        'First character :  0 = Data logging message
        '                   1 = Receipt of transfer of all I/O's 
        '                   2 = xMessage
        '                   3 = Status message

        '010402030209060008006003F201CC03FD03FC03FC000000000000AA0000000000000000000000000000000000000FFF000000000000000000000000000000000000000000000000000003F00000000000000000000000000000FFFF //with RPM
        '1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234
        '         1         2         3         4         5         6         7         8         9         0         1         2         3         4         5         6         7         8

        Try
            Dim strUnix As String
            Dim lngUnix As Long
            Dim q As Integer
            For q = 2 To 20 Step 2
                strUnix += Mid(szDatagram, q, 1)
            Next

            If Len(strUnix) <> 10 Then strUnix = 0
            lngUnix = CLng(strUnix)
            If lngUnix = 0 Then lngUnix = 1388534400 '01/01/2014 00:00:00
            If lngUnix > 1388534400 Then lngUnix += 7200

            Dim strDate As String = mimicDate(lngUnix)
            Dim datDate As Date = CDate(strDate)
            Dim strTime As String = mimicTime(lngUnix)

            Dim intIP As Int16 = Convert.ToInt32(Mid(szDatagram, 21, 2), 16)
            Dim strDevice As String = strSubnet & CStr(intIP)
            strUDP_IP = strDevice 'Set the global variable to ID which IP address this UDP datagram comes from

            'If (intIP < 90 Or intIP > 139) Then Exit Sub 'if old IP range then it is pre accelerometer
            Dim strData As String = szDatagram
            'Decode and parse the received data string into the ANALOG inputs holding array
            Dim aryA(15) As String
            aryA(0) = CStr(Convert.ToInt32(Mid(strData, 23, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 25, 2), 16)
            aryA(1) = CStr(Convert.ToInt32(Mid(strData, 27, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 29, 2), 16)
            aryA(2) = CStr(Convert.ToInt32(Mid(strData, 31, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 33, 2), 16)
            aryA(3) = CStr(Convert.ToInt32(Mid(strData, 35, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 37, 2), 16)
            aryA(4) = CStr(Convert.ToInt32(Mid(strData, 39, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 41, 2), 16)
            aryA(5) = CStr(Convert.ToInt32(Mid(strData, 43, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 45, 2), 16)
            aryA(6) = CStr(Convert.ToInt32(Mid(strData, 47, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 49, 2), 16)
            aryA(7) = CStr(Convert.ToInt32(Mid(strData, 51, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 53, 2), 16)
            aryA(8) = CStr(Convert.ToInt32(Mid(strData, 55, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 57, 2), 16)
            aryA(9) = CStr(Convert.ToInt32(Mid(strData, 59, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 61, 2), 16)
            aryA(10) = CStr(Convert.ToInt32(Mid(strData, 63, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 65, 2), 16)
            aryA(11) = CStr(Convert.ToInt32(Mid(strData, 67, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 69, 2), 16)
            aryA(12) = CStr(Convert.ToInt32(Mid(strData, 71, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 73, 2), 16)
            aryA(13) = CStr(Convert.ToInt32(Mid(strData, 75, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 77, 2), 16)
            aryA(14) = CStr(Convert.ToInt32(Mid(strData, 79, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 81, 2), 16)
            aryA(15) = CStr(Convert.ToInt32(Mid(strData, 83, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 85, 2), 16)

            Dim Ext As String
            Ext = CStr(Convert.ToInt32(Mid(strData, 87, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 89, 2), 16)

            'Incoming integer includes one decimal place
            aryA(0) = CStr(CDbl(CInt(aryA(0)) / 10))
            aryA(1) = CStr(CDbl(CInt(aryA(1)) / 10))
            aryA(2) = CStr(CDbl(CInt(aryA(2)) / 10))
            aryA(3) = CStr(CDbl(CInt(aryA(3)) / 10))
            aryA(4) = CStr(CDbl(CInt(aryA(4)) / 10))
            aryA(5) = CStr(CDbl(CInt(aryA(5)) / 10))

            aryA(6) = CStr(CDbl(CInt(aryA(6)) / 10))
            aryA(7) = CStr(CDbl(CInt(aryA(7)) / 10))
            aryA(8) = CStr(CDbl(CInt(aryA(8)) / 10))
            aryA(9) = CStr(CDbl(CInt(aryA(9)) / 10))
            aryA(10) = CStr(CDbl(CInt(aryA(10)) / 10))
            aryA(11) = CStr(CDbl(CInt(aryA(11)) / 10))
            aryA(12) = CStr(CDbl(CInt(aryA(12)) / 10))
            aryA(13) = CStr(CDbl(CInt(aryA(13)) / 10))
            aryA(14) = CStr(CDbl(CInt(aryA(14)) / 10))
            aryA(15) = CStr(CDbl(CInt(aryA(15)) / 10))


            If blnFractions = False Then
                aryA(0) = CStr(CInt(aryA(0)))
                aryA(1) = CStr(CInt(aryA(1)))
                aryA(2) = CStr(CInt(aryA(2)))
                aryA(3) = CStr(CInt(aryA(3)))
                aryA(4) = CStr(CInt(aryA(4)))
                aryA(5) = CStr(CInt(aryA(5)))
                aryA(6) = CStr(CInt(aryA(6)))
                aryA(7) = CStr(CInt(aryA(7)))
                aryA(8) = CStr(CInt(aryA(8)))
                aryA(9) = CStr(CInt(aryA(9)))
                aryA(10) = CStr(CInt(aryA(10)))
                aryA(11) = CStr(CInt(aryA(11)))
                aryA(12) = CStr(CInt(aryA(12)))
                aryA(13) = CStr(CInt(aryA(13)))
                aryA(14) = CStr(CInt(aryA(14)))
                aryA(15) = CStr(CInt(aryA(15)))
            End If


            Dim bitArray_1 As New BitArray(System.BitConverter.GetBytes(Convert.ToInt32(Mid(strData, 91, 2), 16)))
            Dim bitArray_2 As New BitArray(System.BitConverter.GetBytes(Convert.ToInt32(Mid(strData, 93, 2), 16)))
            Dim bitArray_3 As New BitArray(System.BitConverter.GetBytes(Convert.ToInt32(Mid(strData, 95, 2), 16)))

            Dim aryDout(11) As String
            'Decode and parse OUTPUTS for the received data string into the holding array
            aryDout(0) = -(bitArray_1(7)) 'LED1
            aryDout(1) = -(bitArray_1(6)) 'LED2
            aryDout(2) = -(bitArray_1(5)) 'LED3
            aryDout(3) = -(bitArray_1(4)) 'LED4
            aryDout(4) = -(bitArray_1(3)) 'D0
            aryDout(5) = -(bitArray_1(2)) 'D1
            aryDout(6) = -(bitArray_1(1)) 'D2
            aryDout(7) = -(bitArray_1(0)) 'D3
            aryDout(8) = -(bitArray_2(7)) 'D4
            aryDout(9) = -(bitArray_2(6)) 'D5
            aryDout(10) = -(bitArray_2(5)) 'D6
            aryDout(11) = -(bitArray_2(4)) 'D7

            Dim aryDin(11) As String
            'Decode and parse INPUTS for the received data string into the holding array
            aryDin(0) = -(bitArray_2(3)) 'BTN1
            aryDin(1) = -(bitArray_2(2)) 'BTN2
            aryDin(2) = -(bitArray_2(1)) 'BTN3
            aryDin(3) = -(bitArray_2(0)) 'BTN4
            aryDin(4) = -(bitArray_3(7)) 'D8
            aryDin(5) = -(bitArray_3(6)) 'D9
            aryDin(6) = -(bitArray_3(5)) 'D10
            aryDin(7) = -(bitArray_3(4)) 'D11
            aryDin(8) = -(bitArray_3(3)) 'D12
            aryDin(9) = -(bitArray_3(2)) 'D13
            aryDin(10) = -(bitArray_3(1)) 'D14
            aryDin(11) = -(bitArray_3(0)) 'D15

            Dim aryPeripheral(2) As String
            aryPeripheral(0) = Mid(strData, 97, 12)
            aryPeripheral(1) = Mid(strData, 109, 12)
            aryPeripheral(2) = Mid(strData, 121, 12)

            Dim aryAccelerometer(11) As String
            aryAccelerometer(0) = CStr(Convert.ToInt32(Mid(strData, 133, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 135, 2), 16)
            aryAccelerometer(1) = CStr(Convert.ToInt32(Mid(strData, 137, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 139, 2), 16)
            aryAccelerometer(2) = CStr(Convert.ToInt32(Mid(strData, 141, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 143, 2), 16)
            aryAccelerometer(3) = CStr(Convert.ToInt32(Mid(strData, 145, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 147, 2), 16)
            aryAccelerometer(4) = CStr(Convert.ToInt32(Mid(strData, 149, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 151, 2), 16)
            aryAccelerometer(5) = CStr(Convert.ToInt32(Mid(strData, 153, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 155, 2), 16)

            aryAccelerometer(6) = CStr(Convert.ToInt32(Mid(strData, 157, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 159, 2), 16)
            aryAccelerometer(7) = CStr(Convert.ToInt32(Mid(strData, 161, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 163, 2), 16)
            aryAccelerometer(8) = CStr(Convert.ToInt32(Mid(strData, 165, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 167, 2), 16)
            aryAccelerometer(9) = CStr(Convert.ToInt32(Mid(strData, 169, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 171, 2), 16)
            aryAccelerometer(10) = CStr(Convert.ToInt32(Mid(strData, 173, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 177, 2), 16)
            aryAccelerometer(11) = CStr(Convert.ToInt32(Mid(strData, 177, 2), 16) << 8) + Convert.ToInt32(Mid(strData, 179, 2), 16)

 
            strSql = "INSERT INTO `cmm`.`tblmimics` (datDate, strTime, strDevice," _
            & " A0, A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12, A13, A14, A15, Ext," _
            & " L1, L2, L3, L4, D0, D1, D2, D3, D4, D5, D6, D7, B1, B2, B3, B4, D8, D9, D10, D11, D12, D13, D14, D15," _
            & " Peripheral_1, Peripheral_2, Peripheral_3, x_max, x_min, y_max, y_min, z_max, z_min, x_max_2, x_min_2, y_max_2, y_min_2, z_max_2, z_min_2)" _
            & " VALUES ('" & strDate & "', '" & strTime & "', '" & strDevice & "'," _
            & "" & aryA(0) & ", " & aryA(1) & ", " & aryA(2) & ", " & aryA(3) & ", " & aryA(4) & ", " & aryA(5) & ", " & aryA(6) & ", " & aryA(7) & "," _
            & "" & aryA(8) & ", " & aryA(9) & ", " & aryA(10) & ", " & aryA(11) & ", " & aryA(12) & ", " & aryA(13) & ", " & aryA(14) & ", " & aryA(15) & "," _
            & "" & Ext & "," _
            & "" & aryDout(0) & ", " & aryDout(1) & ", " & aryDout(2) & ", " & aryDout(3) & ", " _
            & "" & aryDout(4) & ", " & aryDout(5) & ", " & aryDout(6) & ", " & aryDout(7) & ", " & aryDout(8) & ", " & aryDout(9) & ", " & aryDout(10) & ", " & aryDout(11) & ", " _
            & "" & aryDin(0) & ", " & aryDin(1) & ", " & aryDin(2) & ", " & aryDin(3) & ", " _
            & "" & aryDin(4) & ", " & aryDin(5) & ", " & aryDin(6) & ", " & aryDin(7) & ", " & aryDin(8) & ", " & aryDin(9) & ", " & aryDin(10) & ", " & aryDin(11) & ", " _
            & "'" & aryPeripheral(0) & "', '" & aryPeripheral(1) & "', '" & aryPeripheral(2) & "', " _
            & "" & aryAccelerometer(0) & ", " & aryAccelerometer(1) & ", " & aryAccelerometer(2) & ", " & aryAccelerometer(3) & ", " & aryAccelerometer(4) & ", " & aryAccelerometer(5) & ", " _
            & "" & aryAccelerometer(6) & ", " & aryAccelerometer(7) & ", " & aryAccelerometer(8) & ", " & aryAccelerometer(9) & ", " & aryAccelerometer(10) & ", " & aryAccelerometer(11) & ")"


            If MySqlCon.State = ConnectionState.Closed Then MySqlCon.Open()
            If MySqlCon.State = ConnectionState.Broken Then MySqlCon.Open()
            Dim sqlSelectCMD As MySqlCommand
            sqlSelectCMD = New MySqlCommand(strSql, MySqlCon)
            sqlSelectCMD.ExecuteScalar()

            If Microsoft.VisualBasic.Left(szDatagram, 1) = "0" Then '"0" indicator for a data logging messsage
                'Delete this IP from the mimics holding table - only one entry is required here
                '[NB Disable SQL Queries "Safe mode" in Workbench preferences for this to work]
                If MySqlCon.State = ConnectionState.Closed Then MySqlCon.Open()
                If MySqlCon.State = ConnectionState.Broken Then MySqlCon.Open()
                sqlSelectCMD = New MySqlCommand("DELETE FROM tblmimics_holding WHERE strDevice = '" & strDevice & "';", MySqlCon)
                sqlSelectCMD.ExecuteScalar()
                'Write the data to the mimics holding table
                strSql = Replace(strSql, "tblmimics", "tblmimics_holding")
                sqlSelectCMD = New MySqlCommand(strSql, MySqlCon)
                sqlSelectCMD.ExecuteScalar()
            End If

            'Regardless now, the received UDP datagram has already been written to the MySql database [tblmimics_holding]

            'If the currently selected node equals the received UDP datagram then populate listviews from UDP recently filled arrays
            If (strDevice = Me.lblIP.Text) Then
                tmrRefresh.Stop() 'Reset the refresh timer so that this received datagram persists for a bit
                tmrRefresh.Enabled = False
                populateListviews(intIP, aryA, aryDout, aryDin, aryPeripheral, aryAccelerometer)
                Me.lblDateTimeStamp.Text = "Last update:      " & strDate & "        " & strTime
                'Me.lblDateTimeStamp.Text = "Last update:      " & Now.ToString("yyyy-MM-dd") & "        " & Now.ToString("HH:mm:ss")
                Me.lblDateTimeStamp.ForeColor = Color.Green
                setNodesStatusFromMySQL()
                tmrRefresh.Enabled = True
                tmrRefresh.Start()
            ElseIf (Me.chkAuto.Checked = True) Then
                'If auto select box is checked then set focus to the node of the received UDP datagram which will trigger Fill_arrays_MySQL
                'Select the correct treeview and select the correct node
                'Which will trigger processSelectedTreeview - and then trigger Fill_arrays_MySQL as well as setNodesStatusFromMySQL()
                IPtoSetTreeAndNodeFocus(intIP)
            Else
                'Update node status - all nodes
                setNodesStatusFromMySQL()
            End If

        Catch ex As Exception
            errorPanelsource("UDP_toArrays" & vbCrLf & szDatagram)
            errorPanel(ex.Message)
        End Try
    End Sub

    Private Sub tmrUDP_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrUDP.Tick
        tmrUDP.Enabled = False
        Me.lblUDP.Visible = False
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        'Broadcast on port 44200 for all Mimics units to send status
        sendUDPglobal("xStatus", "255.255.255.255")
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        On Error GoTo Err

        'Reset the node status's and re read in the data from the holding table for the pre selected node

        Me.lblDisconnect.Text = Nothing
        Me.lblDateTimeStamp.Text = Nothing
        Me.lstViewA.Items.Clear()
        Me.lstViewB.Items.Clear()
        Me.lstViewC.Items.Clear()
        Me.treeView1.SelectedNode = Nothing
        Me.treeView2.SelectedNode = Nothing
        Me.treeView3.SelectedNode = Nothing

        Dim strIp As String = Microsoft.VisualBasic.Right(Me.lblIP.Text, 3)
        Dim intIp As Integer = CInt(Replace(strIp, ".", ""))
        IPtoSetTreeAndNodeFocus(intIp)

Err:
        Resume Next
    End Sub

    Private Sub frmMonitor_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible = False Then
            ' MsgBox("Visible = false")
            blnMonitorVisible = False
        Else
            'MsgBox("Visible = true")
            blnMonitorVisible = True
        End If

    End Sub
  
End Class