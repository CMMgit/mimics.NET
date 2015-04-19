Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Reflection


Public Class frmTransmit

    Private Delegate Sub PrintTextDelegate(ByVal strMsg As String, ByVal color__1 As Color)
    Private Delegate Sub printSDiniDelegate(ByVal strMsg As String, ByVal color__1 As Color)
    Private WithEvents cls As New clsTCP
    Private tcpClient As TcpClient = Nothing
    Private nwStream As NetworkStream = Nothing
    Private thrd As Thread = Nothing
    Private intCount As Integer = 10
    Private intTemp As Integer = 0
    Private Sub frmTransmit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Me.TabControl.TabInde
        Me.btnEdit.Visible = False
        pnlWait.Visible = False
        Me.Timer1.Enabled = False
        Me.Timer2.Enabled = False
        'Me.txtIP.Text = strSubnet & InputBox("Enter the IP address you wish to talk to:", "Destination IP address", strSelectedIP)
        Me.txtIP.Text = strCurrentIP

        btnTransmit = fixButtonImage(btnTransmit)
        btnExit = fixButtonImage(btnExit)
        ToolStrip1.ImageScalingSize = New Size(40, 40)

        'Queries
        setupListview(Me.lstViewINI, "Query ini values", "", "", "", "BOLD")
        setupListview(Me.lstViewINI, "SMS number?", "xVAL:smsNumber", "----------", "xVAL:smsNumber;")
        setupListview(Me.lstViewINI, "IP address?", "xVAL:localIP", "----------", "xVAL:localIP;")
        setupListview(Me.lstViewINI, "Customer?", "xVAL:Customer", "----------", "xVAL:Customer;")
        setupListview(Me.lstViewINI, "Digital input ON message?", "xVAL:DIG?_LOW_MSG", "DIG1-DIG8", "xVAL:DIG6_LOW_MSG;")
        setupListview(Me.lstViewINI, "Digital input OFF message?", "xVAL:DIG?_HIGH_MSG", "DIG1-DIG8", "xVAL:DIG6_HIGH_MSG;")
        setupListview(Me.lstViewINI, "Digital input SMS setting?", "xVAL:DIG?_SMS", "DIG1-DIG8", "xVAL:DIG8_SMS;")
        setupListview(Me.lstViewINI, "Digital input trigger", "xVAL:DIG?_TRIG", "DIG1-DIG8 [1=Low] [2=Low & Hi]", "xINI:DIG8_TRIG=2;")
        setupListview(Me.lstViewINI, "Retrieve any value from .ini", "xVAL:", "Any .ini entry", "xVAL:Profile;")
        setupListview(Me.lstViewINI, "Hadeco Cycle Start Date", "xVAL:HadecoCycleStart", "yyyy-MM-dd HH:mm:ss", "xVAL:HadecoCycleStart;")
        'Functions
        setupListview(Me.lstViewFunctions, "Functions", "", "", "", "BOLD")
        setupListview(Me.lstViewFunctions, "Disable watchdog kicker", "xWATCHDOG", "", "")
        setupListview(Me.lstViewFunctions, "Set analog mapping to SD", "xANALOGSD", "", "")
        setupListview(Me.lstViewFunctions, "Query internal analog map", "xMAP_A0", "", "")
        setupListview(Me.lstViewFunctions, "Write to LCD", "xLCD:", "< 17 characters", "xLCD:Hello Mimics")
        setupListview(Me.lstViewFunctions, "Send an sms", "xSMS:", "A text message", "xSMS:Sent from Mimics;")
        setupListview(Me.lstViewFunctions, "Test analog ports", "xANALOG", "----------", "xANALOG;")
        setupListview(Me.lstViewFunctions, "Test digital ports", "xDIGITAL", "----------", "xDIGITAL;")
        setupListview(Me.lstViewFunctions, "Check in", "xCheckIn", "----------", "xCheckIn;")
        setupListview(Me.lstViewFunctions, "Read mimics.ini from uSD", "xPrintINI", "----------", "xPrintINI;")
        setupListview(Me.lstViewFunctions, "CPU temperature?", "xCPUtemp", "----------", "xCPUtemp;")
        setupListview(Me.lstViewFunctions, "RTC DS1307 time?", "xRTC", "----------", "xRTC;")
        setupListview(Me.lstViewFunctions, "CPU MAC address?", "xGetMAC", "----------", "xGETMAC;")
        setupListview(Me.lstViewFunctions, "Firmware version?", "xVersion", "----------", "xVersion;")
        setupListview(Me.lstViewFunctions, "Peripheral SSN's?", "xGetSSN", "----------", "xGetSSN;")
        setupListview(Me.lstViewFunctions, "SSID and logon strength?", "xSSID", "----------", "xSSID;")
        setupListview(Me.lstViewFunctions, "Reset Mimics", "xRESET", "----------", "xRESET;")
        setupListview(Me.lstViewFunctions, "Reset daughter board", "xDB:RESET", "----------", "xDB:RESET;")
        setupListview(Me.lstViewFunctions, "Scan I2C bus", "xI2C", "----------", "xI2C;")

        'Output ports
        setupListview(Me.lstViewOutputs, "Write to output ports", "", "", "", "BOLD")
        setupListview(Me.lstViewOutputs, "Press panel button", "xBTN:", "1;2;3;4", "xBTN:1")
        setupListview(Me.lstViewOutputs, "Switch input/output port", "xSET:", "L1-L4; O1-O8; DIG1-DIG8; A0-A15", "xSET:L1=1")
        setupListview(Me.lstViewOutputs, "Read input/output port", "xREAD:", "B1-B4; O1-O8; DIG1-DIG8;  A0-A15", "xREAD:DIG6")
        setupListview(Me.lstViewOutputs, "Pulse output port", "xPULSE:", "L1-L4; O1-O8", "xPULSE:L1")
        setupListview(Me.lstViewOutputs, "Write to RS232 serial port", "xRS232:", "ASCII", "xRS232:Hello Mimics")
        setupListview(Me.lstViewOutputs, "xMessage to Daughter Board via RS232", "xDB:Show", "ASCII", "xDB:xxx")
        setupListview(Me.lstViewOutputs, "Calibrate TCPA channel", "xDB:TCPA_CH_2=28", "ASCII", "xDB:TCPA:CH_2=144")
        setupListview(Me.lstViewOutputs, "On board relay", "xRELAY:ON", "ON/OFF", "xRELAY:ON")
        setupListview(Me.lstViewOutputs, "Re-calibrate stepper motors", "xDB:FindHome_1", "1,2,3", "xDB:FindHome_x")

        ' Set the Format type and the CustomFormat string.
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss"

    End Sub

    Private Sub setupListview(ByVal lstView As ListView, ByVal strDescription As String, ByVal strInstruction As String, ByVal strValues As String, ByVal strExample As String, Optional ByVal strFont As String = "NORMAL")

        Dim ListViewItem1 As New ListViewItem
        Dim subItem1 As New ListViewItem.ListViewSubItem
        Dim subItem2 As New ListViewItem.ListViewSubItem
        ListViewItem1 = New ListViewItem(strDescription)
        If strFont = "BOLD" Then
            ListViewItem1.Font = New Font(subItem1.Font, FontStyle.Bold)
        Else
            ListViewItem1.Font = New Font(subItem1.Font, FontStyle.Regular)
        End If
        subItem1 = New ListViewItem.ListViewSubItem
        subItem1.Text = (strInstruction)
        ListViewItem1.SubItems.Add(subItem1)
        subItem1 = New ListViewItem.ListViewSubItem
        subItem1.Text = (strValues)
        ListViewItem1.SubItems.Add(subItem1)
        subItem1 = New ListViewItem.ListViewSubItem
        subItem1.Text = (strExample)
        ListViewItem1.SubItems.Add(subItem1)
        lstView.Items.Add(ListViewItem1)

    End Sub
    Private Sub setupListview1(ByVal strDescription As String, ByVal strInstruction As String, Optional ByVal strFont As String = "NORMAL")

        Dim ListViewItem1 As New ListViewItem
        Dim subItem1 As New ListViewItem.ListViewSubItem
        Dim subItem2 As New ListViewItem.ListViewSubItem
        ListViewItem1 = New ListViewItem(strDescription)
        If strFont = "BOLD" Then
            ListViewItem1.Font = New Font(subItem1.Font, FontStyle.Bold)
        Else
            ListViewItem1.Font = New Font(subItem1.Font, FontStyle.Regular)
        End If
        subItem1 = New ListViewItem.ListViewSubItem
        subItem1.Text = (strInstruction)
        ListViewItem1.SubItems.Add(subItem1)

        lstViewFunctions.Items.Add(ListViewItem1)

    End Sub
    Private Sub btnTransmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransmit.Click

        If Len(Me.txtIP.Text) = 0 Then
            MsgBox("Enter destination Mimics unit", MsgBoxStyle.Critical, "Unknown destination")
            Exit Sub
        End If
        If Len(Me.txtMsg.Text) = 0 Then
            MsgBox("Enter a valid instruction for tranmission to Mimics.", MsgBoxStyle.Critical, "Unknown instruction")
            Exit Sub
        End If

        Dim strValue As String = Me.txtMsg.Text

        Try
            If (sendUDPglobal(Me.txtMsg.Text, Me.txtIP.Text) = True) Then
                MsgBox(Me.txtMsg.Text & " sent to: " & Me.txtIP.Text, MsgBoxStyle.Information, "Data instruction sent")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAll.Click

        If Me.btnAll.Text = "All" Then
            Me.btnAll.Text = "Clear"
            Me.txtIP.Text = "255.255.255.255"
        Else
            Me.btnAll.Text = "All"
            Me.txtIP.Text = Nothing
        End If

    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click

        sendXmessage()

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Windows.Forms.Cursor.Show()
        Me.lblMsg.Text = Nothing
        Me.txtMsg.Text = Nothing
        xMessage = Nothing
        Me.Log.Clear()
        Me.btnEdit.Visible = False

    End Sub

    Private Sub txtMsg_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMsg.KeyDown

        If e.KeyCode = Keys.Return Then
            sendTCPmsg(Me.txtMsg.Text)
        End If

    End Sub

    Private Sub UDP_Transmit()

        xMessage = Nothing

        If Microsoft.VisualBasic.Right(Me.txtIP.Text, 1) = "." Then
            MsgBox("Invalid destination IP address", MsgBoxStyle.Critical, "Invalid: " & Me.txtIP.Text)
            Exit Sub
        End If
        If Len(Me.txtIP.Text) = 0 Then
            MsgBox("Enter destination Mimics unit", MsgBoxStyle.Critical, "Unknown destination")
            Exit Sub
        End If
        If Len(Me.txtMsg.Text) = 0 Then
            MsgBox("Enter a valid instruction for tranmission to Mimics.", MsgBoxStyle.Critical, "Unknown instruction")
            Exit Sub
        End If
        If (Me.txtMsg.Text.IndexOf("_SMS=") > -1 And Me.txtMsg.Text.IndexOf("True") = -1 And Me.txtMsg.Text.IndexOf("False") = -1) Then
            MsgBox("Option can only be set as True or False.", MsgBoxStyle.Critical, "Invalid data")
            Exit Sub
        End If

        Try

            'ini file line format address format:
            If Me.txtMsg.Text.IndexOf("xINI") > -1 And Microsoft.VisualBasic.Right(Me.txtMsg.Text, 1) <> ";" Then
                'Missing end ";" - append it in and carry on
                Me.txtMsg.Text += ";"
            End If
            If Me.txtMsg.Text.IndexOf("xINI") > -1 And Me.txtMsg.Text.IndexOf("=") = -1 Then
                'No = sign present - cannot complete the instruction
                MsgBox("Invalid ini file line format.", MsgBoxStyle.Critical, "no = sign in line")
                Exit Sub
            End If
            If Me.txtMsg.Text.IndexOf("xINI") > -1 And Me.txtMsg.Text.IndexOf("?") > -1 Then
                'Digital input not specified
                MsgBox("Digital input must be specified.", MsgBoxStyle.Critical, "Range D8 - D15")
                Exit Sub
            End If

            If (Me.txtMsg.Text.IndexOf("xINI:localIP=") > -1 And Len(Me.txtMsg.Text) < 17) Then
                'e.g. xINIlocalIP=73;
                MsgBox("Invalid IP address format", MsgBoxStyle.Critical, "IP range " & CStr(baseIP) & " - " & CStr(baseIP + 49))
                Exit Sub
            End If

            If (Me.txtMsg.Text.IndexOf("xINI:smsNumber=") > -1 And Len(Me.txtMsg.Text) < 26) Then
                'e.g. xINIsmsNumber=082444779;
                MsgBox("Invalid sms number format", MsgBoxStyle.Critical, "10 digit number required")
                Exit Sub
            End If

            If (sendUDPglobal(Me.txtMsg.Text, Me.txtIP.Text) = True) Then
                Me.lblMsg.Text = "Sent: " & Me.txtMsg.Text
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Me.btnEdit.Visible = False
        Me.txtMsg.Text = Replace(Me.txtMsg.Text, "VAL", "INI") & "=" & xMessage
        Me.txtMsg.Select(Me.txtMsg.TextLength, 0)
        Me.txtMsg.Focus()
    End Sub

    Private Sub cls_evPrintText(ByVal szPrefix As String, ByVal szText As String, ByVal color__1 As System.Drawing.Color) Handles cls.evPrintText

        'DS1307 Unix: 1387808224
        If Microsoft.VisualBasic.Left(szText, 12) = "DS1307 Unix:" Then
            Dim lngUnix As Long = CLng(Microsoft.VisualBasic.Right(szText, 10))
            szText = szText & " : " & mimicDate(lngUnix)
            szText = szText & " " & mimicTime(lngUnix)
        End If

        If szText = "CPU Temp: 255" Then szText = "CPU Temp not available (255˚C)"
        If szText.IndexOf("CPU Temp: ") > -1 Then szText = szText & "˚C"
        If szText.IndexOf("138846240") > -1 Then szText = "DS1307 RTC not available"

        Dim strMsg As String = szPrefix & szText & ControlChars.Lf
        If strMsg.IndexOf(";") = -1 Then PrintText(strMsg, color__1)
        If (strMsg.IndexOf(";") > -1 And color__1 <> Color.Green) Then printSD_ini(strMsg, color__1)

    End Sub
    Private Sub PrintText(ByVal strMsg As String, ByVal color__1 As Color)

        If Log.InvokeRequired Then
            Dim delg As New PrintTextDelegate(AddressOf PrintText)
            Dim ary(1) As Object
            ary(0) = strMsg
            ary(1) = color__1
            Me.Invoke(delg, ary)
        Else
            Log.SelectionColor = color__1
            Log.SelectedText = strMsg
            Log.SelectionStart = Log.Text.Length
            Log.ScrollToCaret()
            'Un uncoloured copy of the RTB so that edits do not upset the color formatting
            logCopy.SelectedText = strMsg
            logCopy.SelectionStart = Log.Text.Length
            logCopy.ScrollToCaret()
            Log.Refresh()
        End If

    End Sub
    Private Sub printSD_ini(ByVal strMsg As String, ByVal color__1 As Color)

        If rtbMimicsINI.InvokeRequired Then
            Dim delg As New printSDiniDelegate(AddressOf printSD_ini)
            Dim ary(1) As Object
            ary(0) = strMsg
            ary(1) = color__1
            Me.Invoke(delg, ary)
        Else
            Me.rtbMimicsINI.SelectionColor = color__1
            Me.rtbMimicsINI.SelectedText = Replace(strMsg, "Received: ", "")
            Me.rtbMimicsINI.SelectionStart = Me.rtbMimicsINI.Text.Length
            Me.rtbMimicsINI.Refresh()
        End If

    End Sub

    Private Sub lstViewINI_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstViewINI.DoubleClick
        sendXmessage()
    End Sub

    Private Sub lstView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstViewINI.SelectedIndexChanged
        xMessage = Nothing
        Dim n As Integer = lstViewINI.FocusedItem.Index()
        Me.txtMsg.Text = lstViewINI.Items(n).SubItems(1).Text
        Me.txtMsg.Focus()
        Me.txtMsg.SelectionStart = Me.txtMsg.Text.Length + 1
    End Sub

    Private Sub lstViewFunctions_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstViewFunctions.DoubleClick
        sendXmessage()
    End Sub

    Private Sub lstView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstViewFunctions.SelectedIndexChanged
        xMessage = Nothing
        Dim n As Integer = lstViewFunctions.FocusedItem.Index()
        Me.txtMsg.Text = lstViewFunctions.Items(n).SubItems(1).Text
        Me.txtMsg.Focus()
        Me.txtMsg.SelectionStart = Me.txtMsg.Text.Length + 1
    End Sub

    Private Sub lstViewOutputs_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstViewOutputs.DoubleClick
        sendXmessage()
    End Sub

    Private Sub lstViewInputs_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstViewOutputs.SelectedIndexChanged
        xMessage = Nothing
        Dim n As Integer = lstViewOutputs.FocusedItem.Index()
        Me.txtMsg.Text = lstViewOutputs.Items(n).SubItems(1).Text
        Me.txtMsg.Focus()
        Me.txtMsg.SelectionStart = Me.txtMsg.Text.Length + 1
    End Sub

    Private Sub sendTCPmsg(ByVal strMsg As String)

        If Me.txtMsg.Text.IndexOf("xVAL:") > -1 Then Me.btnEdit.Visible = True

        If Microsoft.VisualBasic.Right(Me.txtIP.Text, 1) = "." Then
            MsgBox("Invalid destination IP address", MsgBoxStyle.Critical, "Invalid: " & Me.txtIP.Text)
            Exit Sub
        End If
        If Len(Me.txtIP.Text) = 0 Then
            MsgBox("Enter destination Mimics unit", MsgBoxStyle.Critical, "Unknown destination")
            Exit Sub
        End If
        If Len(strMsg) = 0 Then
            MsgBox("Enter a valid instruction for tranmission to Mimics.", MsgBoxStyle.Critical, "Unknown instruction")
            Exit Sub
        End If
        If (strMsg.IndexOf("_SMS=") > -1 And strMsg.IndexOf("True") = -1 And strMsg.IndexOf("False") = -1) Then
            MsgBox("Option can only be set as True or False.", MsgBoxStyle.Critical, "Invalid data")
            Exit Sub
        End If

        'ini file line format address format:
        If strMsg.IndexOf("xINI") > -1 And Microsoft.VisualBasic.Right(strMsg, 1) <> ";" Then
            'Missing end ";" - append it in and carry on
            strMsg += ";"
        End If
        If strMsg.IndexOf("xINI") > -1 And strMsg.IndexOf("=") = -1 Then
            'No = sign present - cannot complete the instruction
            MsgBox("Invalid ini file line format.", MsgBoxStyle.Critical, "no = sign in line")
            Exit Sub
        End If
        If strMsg.IndexOf("xINI") > -1 And strMsg.IndexOf("?") > -1 Then
            'Digital input not specified
            MsgBox("Digital input must be specified.", MsgBoxStyle.Critical, "Range D8 - D15")
            Exit Sub
        End If

        If (strMsg.IndexOf("xINI:localIP=") > -1 And Len(strMsg) < 17) Then
            'e.g. xINIlocalIP=73;
            MsgBox("Invalid IP address format", MsgBoxStyle.Critical, "IP range " & CStr(baseIP) & " - " & CStr(baseIP + 49))
            Exit Sub
        End If

        If (strMsg.IndexOf("xINI:smsNumber=") > -1 And Len(strMsg) < 26) Then
            'e.g. xINIsmsNumber=082444779;
            MsgBox("Invalid sms number format", MsgBoxStyle.Critical, "10 digit number required")
            Exit Sub
        End If

        If (strMsg.IndexOf("xDB:TCPA_CH_") > -1 And strMsg.IndexOf("=") = -1) Then
            MsgBox("Invalid TCPA calibrate message format", MsgBoxStyle.Critical, "Equals (=) sign missing")
            Exit Sub
        End If

        cls.TCP_Send(Me.txtIP.Text, 44100, strMsg)

    End Sub

    Private Function parseMsg(ByVal strMsg As String) As String
        Try
            Dim strResult As String = Nothing
            Dim n As Integer = charCount(strMsg, ";")
            Dim x As Integer = strMsg.IndexOf(";")
            If (x = -1) Then Return strMsg
            If (n = 0) Then Return strMsg

            strResult = Trim(Replace(Microsoft.VisualBasic.Left(strMsg, x + 1), "Received:", ""))
            strMsg = Trim(Microsoft.VisualBasic.Right(strMsg, Len(strMsg) - x - 1))


            Return strResult

        Catch ex As Exception

        End Try

    End Function
    Private Function charCount(ByVal strLine As String, ByVal strChar As Char) As Integer

        Try
            Dim n, x As Integer
            For n = 1 To Len(strLine)
                If (Mid(strLine, n, 1) = strChar) Then x += 1
            Next

            Return x
        Catch ex As Exception

        End Try

    End Function

    Private Sub btnGet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGet.Click
        'Get the ini file from mimics uSD card
        rtbMimicsINI.Clear()
        cls.TCP_Send(Me.txtIP.Text, 44100, "xPrintINI")
    End Sub

    Private Sub btnSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSet.Click

        'Send a new ini file to mimics uSD card
        Dim strRichText As String = Me.rtbMimicsINI.Text

        'Validity checks for ini file
        If (Microsoft.VisualBasic.Left(strRichText, 20) <> "[CMM mimics Config];") Then
            MsgBox("Incorrect format for Mimics SD ini file." & vbCrLf & _
                   "Header must start with [CMM mimics Config];", MsgBoxStyle.Critical, "[CMM mimics Config];")
            Exit Sub
        End If
        If (Microsoft.VisualBasic.Right(strRichText, 10).IndexOf("[END];") = -1) Then
            MsgBox("Incorrect format for Mimics SD ini file. " & vbCrLf & _
                   "End of file must be with [END];", MsgBoxStyle.Critical, "[END];")
            Exit Sub
        End If
        If (strRichText.IndexOf("localIP") = -1) Then
            MsgBox("There is no localIP entry in the file.", MsgBoxStyle.Critical, "Invalid data - no IP address")
            Exit Sub
        End If
        If (Mid(strRichText, strRichText.Length(), 1) <> Chr(10)) Then
            Me.rtbMimicsINI.Text = Me.rtbMimicsINI.Text & Chr(10)
            'MsgBox("The file does not end with a <CR><LF>. Please add it.", MsgBoxStyle.Critical, "Invalid data - no <CR><LF>.")
            'Exit Sub
        End If

        Log.SelectionColor = Color.Green
        Log.SelectedText = "Sent: Updated Mimics ini file..." & vbCrLf
        Log.SelectionStart = Log.Text.Length
        Log.ScrollToCaret()

        Me.rtbMimicsINI.SelectionStart = 0
        Me.rtbMimicsINI.ScrollToCaret()

        Dim strLine As String = Nothing

        sendRTBtcp()

        

    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click

        Dim OFD As New OpenFileDialog
        OFD.InitialDirectory = "C:\CMM Mimics\"
        OFD.Title = "Select ini file to import"
        OFD.Filter = "INI files (*.ini)|*.ini|Text files (*.txt)|*.txt|All files (*.*)|*.*"
        OFD.ShowDialog()
        Dim strFilename As String = OFD.SafeFileName
        Dim strFullFilename As String = OFD.FileName

        If Len(strFilename) = 0 Then Exit Sub
        If Not IO.File.Exists(strFullFilename) Then Exit Sub

        Dim strLine As String
        Me.rtbMimicsINI.Clear()
        Dim sr As New IO.StreamReader(strFullFilename)
        Do While sr.Peek > -1
            strLine = sr.ReadLine
            Me.rtbMimicsINI.SelectionColor = Color.Black
            Me.rtbMimicsINI.SelectedText = strLine & vbCr
            Me.rtbMimicsINI.SelectionStart = Me.rtbMimicsINI.Text.Length
            Me.rtbMimicsINI.Refresh()
        Loop

    End Sub

    Private Sub btnClearRTB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearRTB.Click
        Me.rtbMimicsINI.Clear()
    End Sub
    Private Sub sendXmessage()

        If (Me.txtIP.Text = "255.255.255.255") Then
            Log.SelectionColor = Color.Black
            Log.SelectedText = "There will be no reply from a general broadcast to all units" & ControlChars.Lf
            Log.SelectionStart = Log.Text.Length
            Log.ScrollToCaret()
            UDP_Transmit()
        Else
            Dim strMsg As String = Me.txtMsg.Text
            'If (Microsoft.VisualBasic.Left(strMsg, 7) = "xRS232:") Then strMsg = strMsg + vbCr
            sendTCPmsg(strMsg)
        End If

    End Sub

    Private Sub sendRTBtcp()

        Dim strRichText As String = Me.rtbMimicsINI.Text
        Dim strLine As String
        Dim strTilde As String = "~"

        'Divide the RTB message into line by line for TCP/IP transmission
        'Each line ends with <LF> chr(10)
        'Prefix each line with the ~ character so Mimics identifies it as an ini line
        Dim x As Integer = 1
        Dim z As Integer = 1
        Dim c As Char = Nothing
        For n As Integer = 1 To strRichText.Length()
            strLine = Mid(strRichText, z, x)
            c = Mid(strRichText, n, 1)
            If (c = Chr(13) Or c = Chr(10)) Then 'line terminates with <LF>
                If (z = 1) Then strTilde = "~~" Else strTilde = "~" 'first line
                strLine = strTilde & strLine ' transmit this line
                cls.TCP_Send(Me.txtIP.Text, 44100, strLine)
                strLine = Nothing
                z = n + 1
                x = 0
            End If
            x = x + 1
        Next
        cls.TCP_Send(Me.txtIP.Text, 44100, "~~")
    End Sub

    Private Sub pleaseWait()

        Me.Timer1.Enabled = True
        Me.Timer2.Enabled = True

        Me.Timer1.Interval = 1000
        Me.Timer2.Interval = 100

        pBar1.Minimum = 0
        pBar1.Maximum = 1000
        Me.pBar1.Value = 0
        Me.pBar1.Step = 11

        pnlWait.Visible = True
        Windows.Forms.Cursor.Hide()

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Dim strValue As String = "New ini file received and will be written to EEPROM"

        If intCount = 0 Then
            Me.pnlWait.Visible = False
            intCount = 11
            Me.Timer1.Enabled = False
            Me.Timer2.Enabled = False

            Dim i As Integer

            If Me.logCopy.Text.IndexOf(strValue) > -1 Then
                Me.logCopy.Text = Replace(Me.logCopy.Text, strValue, "New ini file received and will be written to_EEPROM")
            Else
                Log.SelectionColor = Color.Red
                Log.SelectedText = "EEPROM write error timeout - try again." & vbCrLf
                Log.SelectionStart = Log.Text.Length
                Log.ScrollToCaret()
            End If

            Windows.Forms.Cursor.Show()
            'MsgBox("Cursor show.", MsgBoxStyle.Exclamation, "Show cursor")
        End If

        If (intCount <= 1) Then Windows.Forms.Cursor.Show()

        Me.lblCount.Text = CStr(intCount)
        intCount = intCount - 1

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Me.pBar1.PerformStep()
        Me.pBar1.Refresh()
    End Sub
   
    Private Sub logCopy_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles logCopy.TextChanged

        Dim strValue As String = "Partial section of ini file received"

        If Me.logCopy.Text.IndexOf(strValue) > -1 Then
            logCopy.Text = Replace(logCopy.Text, strValue, "Partial_section of ini file received")
            pleaseWait()
        End If

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        If Me.logCopy.Visible = True Then
            Me.logCopy.Visible = False

        Else
            Me.logCopy.Visible = True

        End If

    End Sub

    Private Sub btnNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNow.Click

        Dim strDateTime As String = Me.DateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss")
        Me.txtMsg.Text = "xSET:HadecoCycleStart:" & strDateTime
        sendXmessage()

    End Sub

End Class