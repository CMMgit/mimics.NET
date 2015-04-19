Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Reflection
Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmControl
    Private Delegate Sub PrintTextDelegate(ByVal strMsg As String, ByVal clrColor As Color)
    Private Delegate Sub printSDiniDelegate(ByVal strMsg As String, ByVal color__1 As Color)
    Private WithEvents cls As New clsTCP
    Private tcpClient As TcpClient = Nothing
    Private nwStream As NetworkStream = Nothing
    Private thrd As Thread = Nothing
    Private intCount As Integer = 10
    Private intTemp As Integer = 0
    Private strSql As String
    Private MySqlCon As MySqlClient.MySqlConnection
    Private blnSuspendTCP As Boolean = True
    Private blnDelayTCP = False

    Private trackbarMouseDown_1 As Boolean = False
    Private trackbarScrolling_1 As Boolean = False

    Private trackbarMouseDown_2 As Boolean = False
    Private trackbarScrolling_2 As Boolean = False

    Private trackbarMouseDown_3 As Boolean = False
    Private trackbarScrolling_3 As Boolean = False

    Private blnHeatOffClicked As Boolean = False

    Private onColor As Color = Color.Red
    Private offColor As Color = Color.DimGray
    Private disColor As Color = Color.WhiteSmoke


    Private Sub frmControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            btnTransmit = fixButtonImage(btnTransmit)
            btnExit = fixButtonImage(btnExit)
            ToolStrip1.ImageScalingSize = New Size(40, 40)

            Me.pnlLog.Visible = True
            Me.pnlLog.BringToFront()
            Me.Location = New System.Drawing.Point(0.0) '923, 0)

            Me.pnlError.Width = 615
            Me.pnlError.Height = 126
            'Me.pnlError.Location = New System.Drawing.Point(12, 466)
            Me.pnlError.SendToBack()
            Me.pnlError.Visible = False

            Dim MysqlConnString As String = GetIni("MysqlConnString")
            MySqlCon = New MySqlClient.MySqlConnection(MysqlConnString)

            blnSuspendTCP = False
            populateCombo()

            

        Catch ex As Exception
            errorPanelsource("frmLoad")
            errorPanel(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub populateCombo()

        Try
            Dim x As Integer
            Me.cmbIP.Items.Clear()
            For x = 0 To 50
                Me.cmbIP.Items.Add(strSubnet & CStr(baseIP + x))
            Next

            Me.cmbIP.SelectedItem = Me.cmbIP.Items.Item(0)

        Catch ex As Exception
            ' MsgBox(ex.Message)
            errorPanelsource("populateCombo")
            errorPanel(ex.Message)
        End Try
    End Sub

    Private Sub cmbIP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbIP.SelectedIndexChanged
        Try
            refreshTCP()

        Catch ex As Exception
            errorPanelsource("cmbIP changed")
            errorPanel(ex.Message)
        End Try

    End Sub

    Private Sub cls_evPrintText(ByVal szPrefix As String, ByVal szText As String, ByVal color__1 As System.Drawing.Color) Handles cls.evPrintText
        'Receiving a TCP/IP message
        logPanel(szText, Color.Orange)
    End Sub

    Private Sub logPanel(ByVal strMsg As String, ByVal clrColor As Color)
        Try
            If rtbLog.InvokeRequired Then
                Dim delg As New PrintTextDelegate(AddressOf logPanel)
                Dim ary(1) As Object
                ary(0) = strMsg
                ary(1) = clrColor
                Me.Invoke(delg, ary)
            Else
                If (Len(strMsg) = 22) Then
                    If Not IsDate(Microsoft.VisualBasic.Left(strMsg, 19)) Then Exit Sub
                    Me.lblHadecoCycleStart.Text = Microsoft.VisualBasic.Left(strMsg, 19) & Hadeco_Cycle_Day(Microsoft.VisualBasic.Left(strMsg, 19))
                    Dim intAutoFlags As Integer = CInt(Microsoft.VisualBasic.Right(strMsg, 3))
                    Dim bitArray As New BitArray(System.BitConverter.GetBytes(intAutoFlags))

                    'Dim int0, int1, int2, int3, int4, int5, int6, int7
                    'int7 = bitArray(7)
                    'int6 = bitArray(6)
                    'int5 = bitArray(5)

                    Me.btnAutoStep_1.Visible = Not (bitArray(4))
                    Me.btnAutoStep_2.Visible = Not (bitArray(3))
                    Me.btnAutoStep_3.Visible = Not (bitArray(2))
                    Me.btnAutoFans.Visible = Not (bitArray(1))
                    Me.btnAutoHeat.Visible = Not (bitArray(0))

                    If (Me.btnAutoStep_1.Visible) = True Then Me.lblAutoMan_1.Text = "Man" Else Me.lblAutoMan_1.Text = "Auto"
                    If (Me.btnAutoStep_2.Visible) = True Then Me.lblAutoMan_2.Text = "Man" Else Me.lblAutoMan_2.Text = "Auto"
                    If (Me.btnAutoStep_3.Visible) = True Then Me.lblAutoMan_3.Text = "Man" Else Me.lblAutoMan_3.Text = "Auto"
                    If (Me.btnAutoFans.Visible) = True Then Me.lblFansAuto.Text = "Manual" Else Me.lblFansAuto.Text = "Auto"
                    If (Me.btnAutoHeat.Visible) = True Then Me.lblHeatAuto.Text = "Manual" Else Me.lblHeatAuto.Text = "Auto"

                ElseIf (Len(strMsg) >= 90) Then
                    'A TCP/IP message received containing all I/O's in response to a TCP/IP CheckIn request. Process the message and set I/O's accordingly.
                    'However, a similar UDP message should also have been received which will trigger setting I/O's as well - disabled till further notice
                    'Changing an analog value (e.g. stepper motor position) is followed by a checkIn request in which case
                    'this will be the manor in which those analog values are set.
                    setControls(strMsg)

                    'After receipt of the above checkIn message stream, send a request for the Hadeco Cycle start date
                    If (strCustomer = "Hadeco" And Len(Me.cmbIP.Text) > 0) Then
                        sendTCP(Me.cmbIP.Text, 44100, "xVAL:HadecoCycleStart", "Hadeco Start Date")
                    End If
                    Exit Sub
                End If

                If (strMsg.IndexOf("Unable") > -1) Then
                    rtbLog.SelectionColor = Color.Red
                ElseIf (strMsg.IndexOf("DEBUG:") > -1) Then
                    rtbLog.SelectionColor = Color.Orange
                Else
                    rtbLog.SelectionColor = clrColor
                End If

                'Messages initiated from server side of TCP/IP clsTCP
                If (strMsg = "^^") Then
                    strMsg = "CheckIn request"
                    rtbLog.SelectionColor = Color.Blue
                End If
                If (strMsg.IndexOf("Attempting to connect to")) > -1 Then rtbLog.SelectionColor = Color.Blue
                If (strMsg = "Listening...") Then rtbLog.SelectionColor = Color.Blue

                rtbLog.SelectedText = strMsg & vbCrLf
                rtbLog.SelectionStart = rtbLog.Text.Length
                rtbLog.ScrollToCaret()
                pnlLog.BringToFront()
                pnlLog.Visible = True
                If strMsg.IndexOf("Unable") > -1 Then
                    Me.btnStatus.ImageIndex = 0
                ElseIf Microsoft.VisualBasic.Left(strMsg, 9) = "Version 2" Then
                    Me.btnStatus.ImageIndex = 1
                End If

            End If

        Catch ex As Exception
            errorPanelsource("logPanel")
            errorPanel(ex.Message)
        End Try

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            refreshTCP()

        Catch ex As Exception
            errorPanelsource("btnRefresh")
            errorPanel(ex.Message)
        End Try
    End Sub

    Private Sub btnErrorReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnErrorReset.Click
        Me.rtbLog.Text = Nothing
        Me.pnlLog.Visible = False
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Me.lblFansAuto.Text = "Manual"
            Me.optFanSpeed_1.Checked = False
            Me.optFanSpeed_2.Checked = False
            Me.optFanSpeed_3.Checked = False
            Me.optFanSpeed_4.Checked = False
            sendTCP(Me.cmbIP.Text, 44100, "xExSET:00308", "DEBUG: Button3_Click")
            Delay(0.2)
            checkIn_TCP()
        Catch ex As Exception
            errorPanelsource("Button3")
            errorPanel(ex.Message)
        End Try
    End Sub

    Private Sub btnElement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElement.Click
        Try
            Me.lblHeatAuto.Text = "Manual"

            blnHeatOffClicked = True
            Me.chkElement.Checked = False
            Me.chkBoiler.Checked = False
            Me.chkInblaas.Checked = False

            Me.chkClearLog.Checked = False
            rtbLog.Clear()
            sendTCP(Me.cmbIP.Text, 44100, "xSET:O5=0", "DEBUG: btnElement_Click")
            Delay(0.1)
            sendTCP(Me.cmbIP.Text, 44100, "xSET:O6=0", "DEBUG: btnElement_Click")
            Delay(0.1)
            sendTCP(Me.cmbIP.Text, 44100, "xSET:O7=0", "DEBUG: btnElement_Click")
            Me.chkClearLog.Checked = True
        Catch ex As Exception
            errorPanelsource("btnElement")
            errorPanel(ex.Message)
        End Try
    End Sub

    Private Sub txtStep_1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStep_1.KeyDown
        Try
            If e.KeyCode <> Keys.Enter Then Exit Sub

            Me.txtStep_1.Text = Replace(Me.txtStep_1.Text, "%", "")
            Me.TrackBar1.Value = CInt(Me.txtStep_1.Text)
            Me.lblVal_1.Text = Me.txtStep_1.Text
            If (Me.txtStep_1.Text.IndexOf("%") = -1) Then Me.txtStep_1.Text = Me.txtStep_1.Text & "%"

        Catch ex As Exception
            errorPanelsource("txtStep")
            errorPanel(ex.Message)
        End Try
    End Sub
    Private Sub txtStep_2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStep_2.KeyDown
        Try
            If e.KeyCode <> Keys.Enter Then Exit Sub

            Me.txtStep_2.Text = Replace(Me.txtStep_2.Text, "%", "")
            Me.TrackBar2.Value = CInt(Me.txtStep_2.Text)
            Me.lblVal_2.Text = Me.txtStep_2.Text
            If (Me.txtStep_2.Text.IndexOf("%") = -1) Then Me.txtStep_2.Text = Me.txtStep_2.Text & "%"

        Catch ex As Exception
            errorPanelsource("txtStep_2")
            errorPanel(ex.Message)
        End Try
    End Sub
    Private Sub txtStep_3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStep_3.KeyDown
        Try
            If e.KeyCode <> Keys.Enter Then Exit Sub

            Me.txtStep_3.Text = Replace(Me.txtStep_3.Text, "%", "")
            Me.TrackBar3.Value = CInt(Me.txtStep_3.Text)
            Me.lblVal_3.Text = Me.txtStep_3.Text
            If (Me.txtStep_3.Text.IndexOf("%") = -1) Then Me.txtStep_3.Text = Me.txtStep_3.Text & "%"

        Catch ex As Exception
            errorPanelsource("txtStep")
            errorPanel(ex.Message)
        End Try
    End Sub

    Private Sub btnAutoStep_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoStep_1.Click
        Try

            sendTCP(Me.cmbIP.Text, 44100, "xDB:STEP_1A", "DEBUG: btnAutoStep_1_Click")
            Me.txtStep_1.Text = "--%"
            Delay(0.2)
            checkIn_TCP()

        Catch ex As Exception
            errorPanelsource("btnAuto")
            errorPanel(ex.Message)
        End Try
    End Sub

    Private Sub btnAutoStep_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoStep_2.Click
        Try

            sendTCP(Me.cmbIP.Text, 44100, "xDB:STEP_2A", "DEBUG: btnAutoStep_2_Click")
            Me.txtStep_2.Text = "--%"
            Delay(0.2)
            checkIn_TCP()

        Catch ex As Exception
            errorPanelsource("btnAuto")
            errorPanel(ex.Message)
        End Try
    End Sub

    Private Sub btnAutoStep_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoStep_3.Click
        Try

            sendTCP(Me.cmbIP.Text, 44100, "xDB:STEP_3A", "DEBUG: btnAutoStep_3_Click")
            Me.txtStep_3.Text = "--%"
            Delay(0.2)
            checkIn_TCP()

        Catch ex As Exception
            errorPanelsource("btnAuto")
            errorPanel(ex.Message)
        End Try
    End Sub

    Private Sub lblVal_1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblVal_1.TextChanged
        Dim strMsg As String = "xDB:STEP_1:"
        strMsg = strMsg & Me.txtStep_1.Text
        strMsg = Replace(strMsg, "%", "")

        sendTCP(Me.cmbIP.Text, 44100, strMsg, "DEBUG: lblVal_1_TextChanged")
        Delay(0.2)
        checkIn_TCP()
    End Sub

   
    Private Sub setControls(Optional ByVal szTCP As String = Nothing)

        'If a TCP/IP messsage has been received then we set the control values from that string
        'else
        'we set the control values from the MySql database assumimg that a UDP message has very recently been written to MySql
        'But this setting controls from UDP via MySql has been disabled by blnControlActionUDP = False

        Dim intDEBUG As Integer = 1
        Dim strStatus As String = "Null status"
        Try
            Dim aryAnalog(15) As String
            Dim aryDigitalInp(11) As String
            Dim aryDigitalOut(11) As String
            Dim aryPeripheral(2) As String
            Dim aryAccelerometer(11) As String
            Dim Ext As String
            Dim n As Integer

            'Read in the data from MySQL
            Dim strIp As String = Me.cmbIP.Text
            Dim strDateIn As String = Nothing
            Dim strTimeIn As String = Nothing
            Dim dblTimeIn As Double
            Dim strToday As String = Now.ToString("yyyy-MM-dd")
            Dim dblTimeNow As Double = dblTime(Now.ToString("HH:mm"))

            '------------------------------------------------------------------------------------------------------------------------------------------
            '                                        Fill arrays from TCP/IP stream received
            '------------------------------------------------------------------------------------------------------------------------------------------
            '010402030209060008006003F201CC03FD03FC03FC000000000000AA0000000000000000000000000000000000000FFF000000000000000000000000000000000000000000000000000003F00000000000000000000000000000FFFF //with RPM
            '1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234
            '         1         2         3         4         5         6         7         8         9         0         1         2         3         4         5         6         7         8
            intDEBUG = 2
            If Len(szTCP) > 0 Then

                If blnControlActionTCP = False Then Exit Sub
                If Microsoft.VisualBasic.Left(szTCP, 2) <> "01" Then Exit Sub

                Dim strUnix As String
                Dim lngUnix As Long
                Dim q As Integer
                For q = 2 To 20 Step 2
                    strUnix += Mid(szTCP, q, 1)
                Next

                intDEBUG = 3

                If Len(strUnix) <> 10 Then strUnix = 0
                lngUnix = CLng(strUnix)
                If lngUnix = 0 Then lngUnix = 1388534400 '01/01/2014 00:00:00
                If lngUnix > 1388534400 Then lngUnix += 7200

                strDateIn = mimicDate(lngUnix)
                strTimeIn = mimicTime(lngUnix)
                dblTimeIn = dblTime(strTimeIn)
                aryAnalog(0) = CStr(Convert.ToInt32(Mid(szTCP, 23, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 25, 2), 16)
                aryAnalog(1) = CStr(Convert.ToInt32(Mid(szTCP, 27, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 29, 2), 16)
                aryAnalog(2) = CStr(Convert.ToInt32(Mid(szTCP, 31, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 33, 2), 16)
                aryAnalog(3) = CStr(Convert.ToInt32(Mid(szTCP, 35, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 37, 2), 16)
                aryAnalog(4) = CStr(Convert.ToInt32(Mid(szTCP, 39, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 41, 2), 16)
                aryAnalog(5) = CStr(Convert.ToInt32(Mid(szTCP, 43, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 45, 2), 16)
                aryAnalog(6) = CStr(Convert.ToInt32(Mid(szTCP, 47, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 49, 2), 16)
                aryAnalog(7) = CStr(Convert.ToInt32(Mid(szTCP, 51, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 53, 2), 16)
                aryAnalog(8) = CStr(Convert.ToInt32(Mid(szTCP, 55, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 57, 2), 16)
                aryAnalog(9) = CStr(Convert.ToInt32(Mid(szTCP, 59, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 61, 2), 16)
                aryAnalog(10) = CStr(Convert.ToInt32(Mid(szTCP, 63, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 65, 2), 16)
                aryAnalog(11) = CStr(Convert.ToInt32(Mid(szTCP, 67, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 69, 2), 16)
                aryAnalog(12) = CStr(Convert.ToInt32(Mid(szTCP, 71, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 73, 2), 16)
                aryAnalog(13) = CStr(Convert.ToInt32(Mid(szTCP, 75, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 77, 2), 16)
                aryAnalog(14) = CStr(Convert.ToInt32(Mid(szTCP, 79, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 81, 2), 16)
                aryAnalog(15) = CStr(Convert.ToInt32(Mid(szTCP, 83, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 85, 2), 16)



                Ext = CStr(Convert.ToInt32(Mid(szTCP, 87, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 89, 2), 16)

                'Incoming integer includes one decimal place
                aryAnalog(0) = CStr(CDbl(CInt(aryAnalog(0)) / 10))
                aryAnalog(1) = CStr(CDbl(CInt(aryAnalog(1)) / 10))
                aryAnalog(2) = CStr(CDbl(CInt(aryAnalog(2)) / 10))
                aryAnalog(3) = CStr(CDbl(CInt(aryAnalog(3)) / 10))
                aryAnalog(4) = CStr(CDbl(CInt(aryAnalog(4)) / 10))
                aryAnalog(5) = CStr(CDbl(CInt(aryAnalog(5)) / 10))

                aryAnalog(6) = CStr(CDbl(CInt(aryAnalog(6)) / 10))
                aryAnalog(7) = CStr(CDbl(CInt(aryAnalog(7)) / 10))
                aryAnalog(8) = CStr(CDbl(CInt(aryAnalog(8)) / 10))
                aryAnalog(9) = CStr(CDbl(CInt(aryAnalog(9)) / 10))
                aryAnalog(10) = CStr(CDbl(CInt(aryAnalog(10)) / 10))
                aryAnalog(11) = CStr(CDbl(CInt(aryAnalog(11)) / 10))
                aryAnalog(12) = CStr(CDbl(CInt(aryAnalog(12)) / 10))
                aryAnalog(13) = CStr(CDbl(CInt(aryAnalog(13)) / 10))
                aryAnalog(14) = CStr(CDbl(CInt(aryAnalog(14)) / 10))
                aryAnalog(15) = CStr(CDbl(CInt(aryAnalog(15)) / 10))


                If blnFractions = False Then
                    aryAnalog(0) = CStr(CInt(aryAnalog(0)))
                    aryAnalog(1) = CStr(CInt(aryAnalog(1)))
                    aryAnalog(2) = CStr(CInt(aryAnalog(2)))
                    aryAnalog(3) = CStr(CInt(aryAnalog(3)))
                    aryAnalog(4) = CStr(CInt(aryAnalog(4)))
                    aryAnalog(5) = CStr(CInt(aryAnalog(5)))
                    aryAnalog(6) = CStr(CInt(aryAnalog(6)))
                    aryAnalog(7) = CStr(CInt(aryAnalog(7)))
                    aryAnalog(8) = CStr(CInt(aryAnalog(8)))
                    aryAnalog(9) = CStr(CInt(aryAnalog(9)))
                    aryAnalog(10) = CStr(CInt(aryAnalog(10)))
                    aryAnalog(11) = CStr(CInt(aryAnalog(11)))
                    aryAnalog(12) = CStr(CInt(aryAnalog(12)))
                    aryAnalog(13) = CStr(CInt(aryAnalog(13)))
                    aryAnalog(14) = CStr(CInt(aryAnalog(14)))
                    aryAnalog(15) = CStr(CInt(aryAnalog(15)))
                End If


                Dim bitArray_1 As New BitArray(System.BitConverter.GetBytes(Convert.ToInt32(Mid(szTCP, 91, 2), 16)))
                Dim bitArray_2 As New BitArray(System.BitConverter.GetBytes(Convert.ToInt32(Mid(szTCP, 93, 2), 16)))
                Dim bitArray_3 As New BitArray(System.BitConverter.GetBytes(Convert.ToInt32(Mid(szTCP, 95, 2), 16)))

                'Decode and parse OUTPUTS for the received data string into the holding array
                aryDigitalOut(0) = -(bitArray_1(7)) 'LED1
                aryDigitalOut(1) = -(bitArray_1(6)) 'LED2
                aryDigitalOut(2) = -(bitArray_1(5)) 'LED3
                aryDigitalOut(3) = -(bitArray_1(4)) 'LED4
                aryDigitalOut(4) = -(bitArray_1(3)) 'D0
                aryDigitalOut(5) = -(bitArray_1(2)) 'D1
                aryDigitalOut(6) = -(bitArray_1(1)) 'D2
                aryDigitalOut(7) = -(bitArray_1(0)) 'D3
                aryDigitalOut(8) = -(bitArray_2(7)) 'D4
                aryDigitalOut(9) = -(bitArray_2(6)) 'D5
                aryDigitalOut(10) = -(bitArray_2(5)) 'D6
                aryDigitalOut(11) = -(bitArray_2(4)) 'D7

                'Decode and parse INPUTS for the received data string into the holding array
                aryDigitalInp(0) = -(bitArray_2(3)) 'BTN1
                aryDigitalInp(1) = -(bitArray_2(2)) 'BTN2
                aryDigitalInp(2) = -(bitArray_2(1)) 'BTN3
                aryDigitalInp(3) = -(bitArray_2(0)) 'BTN4
                aryDigitalInp(4) = -(bitArray_3(7)) 'D8
                aryDigitalInp(5) = -(bitArray_3(6)) 'D9
                aryDigitalInp(6) = -(bitArray_3(5)) 'D10
                aryDigitalInp(7) = -(bitArray_3(4)) 'D11
                aryDigitalInp(8) = -(bitArray_3(3)) 'D12
                aryDigitalInp(9) = -(bitArray_3(2)) 'D13
                aryDigitalInp(10) = -(bitArray_3(1)) 'D14
                aryDigitalInp(11) = -(bitArray_3(0)) 'D15

                aryPeripheral(0) = Mid(szTCP, 97, 12)
                aryPeripheral(1) = Mid(szTCP, 109, 12)
                aryPeripheral(2) = Mid(szTCP, 121, 12)

                intDEBUG = 4

                aryAccelerometer(0) = CStr(Convert.ToInt32(Mid(szTCP, 133, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 135, 2), 16)
                aryAccelerometer(1) = CStr(Convert.ToInt32(Mid(szTCP, 137, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 139, 2), 16)
                aryAccelerometer(2) = CStr(Convert.ToInt32(Mid(szTCP, 141, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 143, 2), 16)
                aryAccelerometer(3) = CStr(Convert.ToInt32(Mid(szTCP, 145, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 147, 2), 16)
                aryAccelerometer(4) = CStr(Convert.ToInt32(Mid(szTCP, 149, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 151, 2), 16)
                aryAccelerometer(5) = CStr(Convert.ToInt32(Mid(szTCP, 153, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 155, 2), 16)

                aryAccelerometer(6) = CStr(Convert.ToInt32(Mid(szTCP, 157, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 159, 2), 16)
                aryAccelerometer(7) = CStr(Convert.ToInt32(Mid(szTCP, 161, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 163, 2), 16)
                aryAccelerometer(8) = CStr(Convert.ToInt32(Mid(szTCP, 165, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 167, 2), 16)
                aryAccelerometer(9) = CStr(Convert.ToInt32(Mid(szTCP, 169, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 171, 2), 16)
                aryAccelerometer(10) = CStr(Convert.ToInt32(Mid(szTCP, 173, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 177, 2), 16)
                aryAccelerometer(11) = CStr(Convert.ToInt32(Mid(szTCP, 177, 2), 16) << 8) + Convert.ToInt32(Mid(szTCP, 179, 2), 16)

                intDEBUG = 5
                '------------------------------------------------------------------------------------------------------------------------------------------
                '                                        Fill arrays from MySql database
                '------------------------------------------------------------------------------------------------------------------------------------------
            Else
                If blnControlActionUDP = False Then Exit Sub

                If (strIp <> strUDP_IP) Then Exit Sub 'CHeck that the latest UDP datagram came from the currently selected IP address
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

                                aryDigitalInp(0) = RDR.Item("B1").ToString()
                                aryDigitalInp(1) = RDR.Item("B2").ToString()
                                aryDigitalInp(2) = RDR.Item("B3").ToString()
                                aryDigitalInp(3) = RDR.Item("B4").ToString()
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
            End If 'if len(szTCP)>0
            '------------------------------------------------------------------------------------------------------------------------------------------
            If Len(szTCP) > 0 Then
                strStatus = "setControls from TCP/IP"
            Else
                strStatus = "setControls from UDP"
            End If
            logPanel(strStatus, Color.Blue)
            '------------------------------------------------------------------------------------------------------------------------------------------
            'Now that all arrays are filled with values, write them to the controls accordingly
            'Date time stamp
            intDEBUG = 6
            If (strDateIn <> Now.ToString("yyyy-MM-dd") And strDateIn <> Now.ToString("yyyy/MM/dd")) Then
                Me.lblDateTimeStamp.ForeColor = Color.Red
            ElseIf ((strDateIn = strToday) And ((dblTimeNow - dblTimeIn) > (3 / 60))) Then 'Today but more than 3 minutes old = houglass faded green
                Me.lblDateTimeStamp.ForeColor = Color.DarkSeaGreen
            ElseIf ((strDateIn = strToday) And ((dblTimeNow - dblTimeIn) <= (3 / 60))) Then 'Today but more than 3 minutes old = houglass faded green
                Me.lblDateTimeStamp.ForeColor = Color.Green
            End If

            'If (dblTimeIn > dblTimeNow) Then strTimeIn = Now.ToString("HH:mm:ss")
            Me.lblDateTimeStamp.Text = "Last update:      " & strDateIn & "        " & strTimeIn
            intDEBUG = 7
            BTN1.BackColor = btnColor(aryDigitalOut(0))
            BTN2.BackColor = btnColor(aryDigitalOut(1))
            BTN3.BackColor = btnColor(aryDigitalOut(2))
            BTN4.BackColor = btnColor(aryDigitalOut(3))
            intDEBUG = 8
            Dim str1, str2, str3, str4 As String
            If CInt(aryDigitalOut(4)) = 1 Then Me.optFanSpeed_1.Checked = True Else Me.optFanSpeed_1.Checked = False
            If CInt(aryDigitalOut(5)) = 1 Then Me.optFanSpeed_2.Checked = True Else Me.optFanSpeed_2.Checked = False
            If CInt(aryDigitalOut(6)) = 1 Then Me.optFanSpeed_3.Checked = True Else Me.optFanSpeed_3.Checked = False
            If CInt(aryDigitalOut(7)) = 1 Then Me.optFanSpeed_4.Checked = True Else Me.optFanSpeed_4.Checked = False

            If CInt(aryDigitalOut(8)) = 1 Then Me.chkBoiler.Checked = True Else Me.chkBoiler.Checked = False
            If CInt(aryDigitalOut(9)) = 1 Then Me.chkElement.Checked = True Else Me.chkElement.Checked = False
            If CInt(aryDigitalOut(10)) = 1 Then Me.chkInblaas.Checked = True Else Me.chkInblaas.Checked = False
            intDEBUG = 9

            If (CDbl(aryAnalog(13))) > 100 Then aryAnalog(13) = 100
            If (CDbl(aryAnalog(14))) > 100 Then aryAnalog(14) = 100
            If (CDbl(aryAnalog(15))) > 100 Then aryAnalog(15) = 100

            Me.txtStep_1.Text = aryAnalog(13) & "%"
            Me.txtStep_2.Text = aryAnalog(14) & "%"
            Me.txtStep_3.Text = aryAnalog(15) & "%"

            Me.TrackBar1.Value = CInt(aryAnalog(13))
            Me.TrackBar2.Value = CInt(aryAnalog(14))
            Me.TrackBar3.Value = CInt(aryAnalog(15))

        Catch ex As Exception
            errorPanelsource(strStatus)
            errorPanel(ex.Message & " :DEBUG " & CStr(intDEBUG) & vbCrLf & szTCP)
        End Try
    End Sub
    Private Function btnColor(ByVal strX As String) As Color

        If strX Is Nothing Then Return disColor
        If strX = "0" Then Return offColor
        If strX = "1" Then Return onColor

    End Function
    Private Sub checkIn_TCP()
        Try
            '^^ will trigger a return, via TCP/IP, of all I/O's
            sendTCP(Me.cmbIP.Text, 44100, "^^", "Send checkIn_TCP()")

        Catch ex As Exception
            errorPanelsource("checkin_TCP")
            errorPanel(ex.Message)
        End Try

    End Sub
    Private Sub sendTCP(ByVal strIP As String, ByVal intPort As Integer, ByVal strMsg As String, ByVal strSource As String)
        Try
            If (chkClearLog.Checked = True) Then rtbLog.Clear()
            If (blnSuspendTCP = True) Then Exit Sub
            cls.TCP_Send(strIP, intPort, strMsg)
        Catch ex As Exception
            errorPanelsource("sendTCP")
            errorPanel(ex.Message)
        End Try
    End Sub

    Private Sub BTN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
    BTN1.Click, BTN2.Click, BTN3.Click, BTN4.Click

        Exit Sub ' Buttons are now used to drive the menu

        Try
            If Len(Me.cmbIP.Text) = 0 Then
                MsgBox("There is no IP address selected.", MsgBoxStyle.Critical, "IP address required")
                Exit Sub
            End If

            Dim BTN As Button = sender
            Dim strMsg As String = "xBTN:" & Microsoft.VisualBasic.Right(BTN.Name, 1)
            cls.TCP_Send(Me.cmbIP.Text, 44100, strMsg)

            Delay(0.1)
            checkIn_TCP()

        Catch ex As Exception
            errorPanelsource("BTN 1/2/3/4 click")
            errorPanel(ex.Message)
        End Try
    End Sub

    Private Sub tmrRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrRefresh.Tick
        Try
            checkIn_TCP()
        Catch ex As Exception
            errorPanelsource("tmrRefresh tick")
            errorPanel(ex.Message)
        End Try
    End Sub
    Private Sub TrackBar1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrackBar1.MouseDown
        trackbarMouseDown_1 = True
    End Sub

    Private Sub TrackBar1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrackBar1.MouseUp
        If trackbarMouseDown_1 = True AndAlso trackbarScrolling_1 = True Then
            Me.lblVal_1.Text = (TrackBar1.Value)

        End If
        trackbarMouseDown_1 = False
        trackbarScrolling_1 = False
    End Sub
    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        trackbarScrolling_1 = True
        Me.txtStep_1.Text = TrackBar1.Value.ToString & "%"

    End Sub

    Private Sub TrackBar2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrackBar2.MouseDown
        trackbarMouseDown_2 = True
    End Sub

    Private Sub TrackBar2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrackBar2.MouseUp
        If trackbarMouseDown_2 = True AndAlso trackbarScrolling_2 = True Then
            Me.lblVal_2.Text = (TrackBar2.Value)

        End If
        trackbarMouseDown_2 = False
        trackbarScrolling_2 = False
    End Sub

    Private Sub TrackBar2_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar2.Scroll
        trackbarScrolling_2 = True
        Me.txtStep_2.Text = TrackBar2.Value.ToString & "%"

    End Sub
    Private Sub lblVal_2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblVal_2.TextChanged
        Try
            Dim strMsg As String = "xDB:STEP_2:"
            strMsg = strMsg & Me.txtStep_2.Text
            strMsg = Replace(strMsg, "%", "")

            sendTCP(Me.cmbIP.Text, 44100, strMsg, "DEBUG: lblVal_2_TextChanged")
            Delay(0.2)
            checkIn_TCP()
        Catch ex As Exception
            errorPanelsource("lblVal2")
            errorPanel(ex.Message)
        End Try
    End Sub

    Private Sub TrackBar3_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrackBar3.MouseDown
        trackbarMouseDown_3 = True
    End Sub

    Private Sub TrackBar3_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrackBar3.MouseUp
        If trackbarMouseDown_3 = True AndAlso trackbarScrolling_3 = True Then
            Me.lblVal_3.Text = (TrackBar3.Value)

        End If
        trackbarMouseDown_3 = False
        trackbarScrolling_3 = False
    End Sub

    Private Sub TrackBar3_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar3.Scroll
        trackbarScrolling_3 = True
        Me.txtStep_3.Text = TrackBar3.Value.ToString & "%"

    End Sub
    Private Sub lblVal_3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblVal_3.TextChanged
        Try
            Dim strMsg As String = "xDB:STEP_3:"
            strMsg = strMsg & Me.txtStep_3.Text
            strMsg = Replace(strMsg, "%", "")

            sendTCP(Me.cmbIP.Text, 44100, strMsg, "DEBUG: lblVal_3_TextChanged")
            Delay(0.2)
            checkIn_TCP()
        Catch ex As Exception
            errorPanelsource("lblVal3")
            errorPanel(ex.Message)
        End Try
    End Sub
    Private Sub errorPanelsource(ByVal strMsg As String)
        Me.lblError.Text = "Errors:"
        rtbError.SelectionColor = Color.Blue
        rtbError.SelectedText = strMsg & vbCrLf
        rtbError.SelectionStart = rtbError.Text.Length
        rtbError.ScrollToCaret()
        Me.pnlError.Visible = True
        Me.pnlError.BringToFront()
    End Sub
    Private Sub errorPanel(ByVal strMsg As String)
        Me.lblError.Text = "Errors:"
        rtbError.SelectionColor = Color.Red
        rtbError.SelectedText = strMsg & vbCrLf
        rtbError.SelectionStart = rtbError.Text.Length
        rtbError.ScrollToCaret()
        pnlError.Visible = True
        Me.pnlError.Visible = True
        Me.pnlError.BringToFront()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Me.rtbError.Clear()
        Me.pnlError.Visible = False
        Me.pnlError.SendToBack()
    End Sub

    Private Sub tmrActionSQL_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrActionSQL.Tick
        'This timer checks for the global flag to indicate receipt of a UDP message which has just been written be written to MySql
        'Whereafter this timer will action and the data can be retrieved from MySql for display
        'There is a check, within setControls, to verify that the UDP update came from the currently selected IP address
        If (blnUDP = True) Then
            blnUDP = False
            setControls()
        End If

    End Sub

    Private Sub refreshTCP()
        Try

            BTN1.BackColor = btnColor("0")
            BTN2.BackColor = btnColor("0")
            BTN3.BackColor = btnColor("0")
            BTN4.BackColor = btnColor("0")
            Me.optFanSpeed_1.Checked = False
            Me.optFanSpeed_2.Checked = False
            Me.optFanSpeed_3.Checked = False
            Me.optFanSpeed_4.Checked = False
            Me.chkBoiler.Checked = False
            Me.chkElement.Checked = False
            Me.chkInblaas.Checked = False
            Me.lblHadecoCycleStart.Text = Nothing

            checkIn_TCP()
            'tmrDlayTCP.Enabled = True

        Catch ex As Exception

        End Try

    End Sub

    Private Sub tmrDlayTCP_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDlayTCP.Tick
        'This timer is enabled in the refreshTCP() routine and immediately disabled here again
        'This timer provides a 250mS delay between sending a CheckIn_TCP message and this one in order to 
        'allowes for a TCP/IP response first response

        tmrDlayTCP.Enabled = False

        If (strCustomer = "Hadeco" And Len(Me.cmbIP.Text) > 0) Then
            sendTCP(Me.cmbIP.Text, 44100, "xVAL:HadecoCycleStart", "Hadeco Start Date")
        End If

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.rtbLog.Clear()
    End Sub

    Private Sub btnAutoFans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoFans.Click
        sendTCP(Me.cmbIP.Text, 44100, "xAutoFans", "DEBUG: Set Auto Fans On")
        Delay(0.2)
        checkIn_TCP()
    End Sub
    Private Sub btnAutoHeat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoHeat.Click    
        sendTCP(Me.cmbIP.Text, 44100, "xAutoCool", "DEBUG: Set Auto Heat ON")
        Delay(0.2)
        checkIn_TCP()
    End Sub
    Private Sub optFanSpeed_1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFanSpeed_1.CheckedChanged

    End Sub

    Private Sub optFanSpeed_2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFanSpeed_2.CheckedChanged

    End Sub

    Private Sub optFanSpeed_3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFanSpeed_3.CheckedChanged

    End Sub

    Private Sub optFanSpeed_4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFanSpeed_4.CheckedChanged

    End Sub
    Private Sub optFanSpeed_1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optFanSpeed_1.Click
        If Me.optFanSpeed_1.Checked = True Then
            'xExSET:00318 [Output 0, range 0-3, value = high, led 1(8)
            sendTCP(Me.cmbIP.Text, 44100, "xExSET:00318", "DEBUG: optFanSpeed_1_CheckedChanged")
            Delay(0.2)
            checkIn_TCP()
        End If
    End Sub

    Private Sub optFanSpeed_2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optFanSpeed_2.Click
        If Me.optFanSpeed_2.Checked = True Then

            sendTCP(Me.cmbIP.Text, 44100, "xExSET:10318", "DEBUG: optFanSpeed_2_CheckedChanged")
            Delay(0.2)
            checkIn_TCP()
        End If
    End Sub

    Private Sub optFanSpeed_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optFanSpeed_3.Click
        If Me.optFanSpeed_3.Checked = True Then
            sendTCP(Me.cmbIP.Text, 44100, "xExSET:20318", "DEBUG: optFanSpeed_3_CheckedChanged")
            Delay(0.2)
            checkIn_TCP()
        End If
    End Sub

    Private Sub optFanSpeed_4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optFanSpeed_4.Click
        If Me.optFanSpeed_4.Checked = True Then
            sendTCP(Me.cmbIP.Text, 44100, "xExSET:30318", "DEBUG: optFanSpeed_4_CheckedChanged")
            Delay(0.2)
            checkIn_TCP()
        End If
    End Sub
    Private Sub chkBoiler_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBoiler.CheckedChanged

    End Sub

    Private Sub chkElement_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkElement.CheckedChanged

    End Sub

    Private Sub chkInblaas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInblaas.CheckedChanged

    End Sub
    Private Sub chkBoiler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBoiler.Click
        'Very confusing:
        'Outputs are 0 - 7 or O1 - O8
        'So O1 = 0
        If (Me.chkBoiler.Checked = True Or Me.chkElement.Checked = True) Then Me.chkInblaas.Checked = True

        If (blnHeatOffClicked = True) Then
            blnHeatOffClicked = False
            Exit Sub
        End If

        If Me.chkBoiler.Checked = True Then
            sendTCP(Me.cmbIP.Text, 44100, "xSET:O5=1", "DEBUG: chkBoiler_CheckedChanged")
        Else
            sendTCP(Me.cmbIP.Text, 44100, "xSET:O5=0", "DEBUG: chkBoiler_CheckedChanged")
        End If
        Delay(0.2)
        checkIn_TCP()
    End Sub

    Private Sub chkElement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkElement.Click

        If (Me.chkBoiler.Checked = True Or Me.chkElement.Checked = True) Then Me.chkInblaas.Checked = True

        If (blnHeatOffClicked = True) Then
            Exit Sub
        End If
        If Me.chkElement.Checked = True Then
            sendTCP(Me.cmbIP.Text, 44100, "xSET:O6=1", "DEBUG: chkElement_CheckedChanged")
        Else
            sendTCP(Me.cmbIP.Text, 44100, "xSET:O6=0", "DEBUG: chkElement_CheckedChanged")
        End If
        Delay(0.2)
        checkIn_TCP()
    End Sub

    Private Sub chkInblaas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkInblaas.Click
        If Me.chkInblaas.Checked = True Then
            sendTCP(Me.cmbIP.Text, 44100, "xSET:O7=1", "DEBUG: chkInblaas_CheckedChanged")
        Else
            sendTCP(Me.cmbIP.Text, 44100, "xSET:O7=0", "DEBUG: chkInblaas_CheckedChanged")
        End If
        Delay(0.2)
        checkIn_TCP()
    End Sub

End Class