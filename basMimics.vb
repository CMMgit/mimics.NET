Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports MySql.Data
Imports System.Threading
Imports System.Net.Sockets
Imports MySql.Data.MySqlClient


'Notes:
'Philosophy:
'The service runs to receive the UDP data stream, sent every second, and write it to the MySql database.
'The frmReceive is updated always and only directly from the MySql database 
'The frmMonitor is updated from the UDP datastream (which will also be written to MySql) sent on I/O change or checkIn request,
'The frmMonitor can be updated from the MySql database to show most recent data if the Mimics unit is offline and cannot
'The frmControl is only updated from TCP/IP datastream received
'be connected via TCP/IP link
'Version 1.19   29/11/2013 Added status messaging to MySql
'Version 1.20   11/12/2013 Added TCP/IP comms functionality
'Version 1.21   08/01/2014 Expanded to 50 IP addresses
'Version 1.25   27/03/2014 Added peripherals
'Version 1.28   28/03/2014 Added extra polling labels 
'Version 1.29   30/02/2014 Added array for peripherals
'Version 1.30   31/02/2014 Form handling improve
'Version 2.00   15/05/2014 Handling for new boards
'Version 2.01   21/05/2014 eeprom receiving and writing
'Version 2.02   24/05/2014 ini lookup changed to global array 
'Version 2.03   28/05/2014 Added 7200 secs to time  
'Version 2.04   19/06/2014 TCPA calibration
'Version 2.05   14/07/2014 Multiple instance app & UDP bind to any address
'Version 2.06   16/07/2014 Simplified UDP listener on port 44300
'Version 2.10   18/08/2014 TCP/IP timeouts corrected (shortened)
'Version 2.14   26/08/2014 Blocked old style data coming in
'Version 2.15   05/09/2014 Refined chart (graph) series
'Version 2.16   12/09/2014 Added exclusions for server on 192.168.1.137
'Version 2.17   14/09/2014 Added UDP rebroadcast to port 2222 and listen now on 2222 for control panel
'Version 2.18   15/09/2014 Changed control panel address range x.x.x.90 - x.x.x.139
'Version 2.19   22/09/2014 Canned UDP Rx for control panel. Now Control Panel is updated from MySQL 
'Version 2.20   25/09/2014 Added tblmimics_holding coding
'Version 2.21   26/09/2014 Put a UDP listner back on frmControlMySQL to react to broadcasts when need be
'Version 2.22   09/11/2014 Added frmMonitor
'Version 2.23   10/11/2014 Added lstViewF for output monitoring
'Version 2.24   11/11/2014 frmControl handling
'Version 2.25   06/01/2015 Populate lstViewE for second accelerometer
'Version 2.26   14/01/2015 Minor change to monitor parse accelerometer
'Version 2.27   11/02/2015 Revamp of received UDP datagram handling

'Masterbatch  Range 192.168.1.110/255.255.255.0-192.168.1.149/255.255.255.0
'Gateway – 192.168.1.130
'Exclude server on 192.168.1.137

'Setting width: TableRow.Width = new Unit("25%")

Module basMimics

    '***********************************************************************
    Public strVersion As String = "Version 2.29 : 26/02/2015"
    Public baseIP As Integer = 90
    Public blnAppMultiple As Boolean = False
    Public sql As String = Nothing
    Public blnTransmitFormLoaded = False
    Public m_udpClient_global As UdpClient = Nothing
    Public xMessage As String = Nothing
    Public strSubnet As String
    Public strSelectedIP As String = CStr(baseIP)
    Public blnPrintSD As Boolean = False
    Public strCurrentIP As String
    Public strCustomer
    Public blnFractions As Boolean
    Public blnUDP As Boolean = False
    Public strUDP_IP As String
    
    Public blnControlActionUDP As Boolean = False
    Public blnControlActionTCP As Boolean = True

    'Array of lables for chart series
    Public aryChartLabel(20)

    'Array of labels for peripherals
    Public aryPeripherals(49, 1) As String

    'Analog inputs sets of different labels
    'Tell a node which labels set to load from here
    Public aryAnalogLabels_1(15) As String
    Public aryAnalogLabels_2(15) As String
    Public aryAnalogLabels_3(15) As String
    Public aryAnalogLabels_4(15) As String
    Public aryAnalogLabels_5(15) As String

    'Digital inputs sets of different labels and labels for 1 and 0
    'Tell a node which labels set to load from here
    Public aryDigitalLabels_1(11, 2) As String
    Public aryDigitalLabels_2(11, 2) As String
    Public aryDigitalLabels_3(11, 2) As String
    Public aryDigitalLabels_4(11, 2) As String
    Public aryDigitalLabels_5(11, 2) As String

    'Digital inputs sets of different labels and labels for 1 and 0
    'Tell a node which labels set to load from here
    Public aryDigitalOutLabels_1(11, 2) As String
    Public aryDigitalOutLabels_2(11, 2) As String
    Public aryDigitalOutLabels_3(11, 2) As String
    Public aryDigitalOutLabels_4(11, 2) As String
    Public aryDigitalOutLabels_5(11, 2) As String

    'Profiles for which digital inputs are active red
    Public aryActiveRedInput_1(11) As String
    Public aryActiveRedInput_2(11) As String
    Public aryActiveRedInput_3(11) As String
    Public aryActiveRedInput_4(11) As String
    Public aryActiveRedInput_5(11) As String

    'Profiles for which digital inputs are active red
    Public aryActiveRedOutput_1(11) As String
    Public aryActiveRedOutput_2(11) As String
    Public aryActiveRedOutput_3(11) As String
    Public aryActiveRedOutput_4(11) As String
    Public aryActiveRedOutput_5(11) As String

    Public Sub startUp()
        Try

            If strCustomer = "MBSA" Then
                baseIP = 90
                File.Copy("C:\CMM Mimics\Customers\MBSA\mimics.ini", "C:\CMM Mimics\mimics.ini", True)
            ElseIf strCustomer = "Hadeco" Then
                baseIP = 172
                File.Copy("C:\CMM Mimics\Customers\HADECO\mimics.ini", "C:\CMM Mimics\mimics.ini", True)
            End If

            setupArrays()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub setupArrays()

        'LABEL SET 1 - EXTRUDER
        'Setup the row names for the listviews
        aryAnalogLabels_1(0) = "Motor Current"
        aryAnalogLabels_1(1) = "Hopper Current"
        aryAnalogLabels_1(2) = "Screw Shaft RPM"
        aryAnalogLabels_1(3) = "Hopper RPM"
        aryAnalogLabels_1(4) = "Pelitizer RPM"
        aryAnalogLabels_1(5) = "Head Pressure"
        aryAnalogLabels_1(6) = "Head Temp"
        aryAnalogLabels_1(7) = "Temperature #1"
        aryAnalogLabels_1(8) = "Temperature #2"
        aryAnalogLabels_1(9) = "Temperature #3"
        aryAnalogLabels_1(10) = "Temperature #4"
        aryAnalogLabels_1(11) = "Temperature#5"
        aryAnalogLabels_1(12) = "Temperature #6"
        aryAnalogLabels_1(13) = "Temperature #7"
        aryAnalogLabels_1(14) = "Temperature #8"
        aryAnalogLabels_1(15) = "Water Bath Temp"

        '2 = Title label, 1 = HIGH label, 0 = LOW label
        aryDigitalLabels_1(0, 2) = "EMERGENCY STOP" 'BTN1 Label
        aryDigitalLabels_1(0, 1) = "RELEASED"       'BTN1 HIGH
        aryDigitalLabels_1(0, 0) = "PUSH"           'BTN1 LOW
        aryDigitalLabels_1(1, 2) = "BTN2"           'BTN2 Label
        aryDigitalLabels_1(1, 1) = "RELEASED"       'BTN2 HIGH
        aryDigitalLabels_1(1, 0) = "PUSH"           'BTN2 LOW
        aryDigitalLabels_1(2, 2) = "BTN3"           'BTN3 Label
        aryDigitalLabels_1(2, 1) = "RELEASED"       'BTN3 HIGH
        aryDigitalLabels_1(2, 0) = "PUSH"           'BTN3 LOW
        aryDigitalLabels_1(3, 2) = "BTN4"           'BTN4 Label
        aryDigitalLabels_1(3, 1) = "RELEASED"       'BTN4 HIGH
        aryDigitalLabels_1(3, 0) = "PUSH"           'BTN4 LOW

        aryDigitalLabels_1(4, 2) = "Drive"          'D8 Label
        aryDigitalLabels_1(4, 1) = "OFF"            'D8 HIGH
        aryDigitalLabels_1(4, 0) = "ON"             'D8 LOW
        aryDigitalLabels_1(5, 2) = "Hopper"         'D9 Label
        aryDigitalLabels_1(5, 1) = "OFF"            'D9 HIGH
        aryDigitalLabels_1(5, 0) = "ON"             'D9 LOW
        aryDigitalLabels_1(6, 2) = "Head Pos"       'D10 Label
        aryDigitalLabels_1(6, 1) = "CLOSED"         'D10 HIGH
        aryDigitalLabels_1(6, 0) = "OPEN"           'D10 LOW
        aryDigitalLabels_1(7, 2) = "Pelitizer"      'D11 Label
        aryDigitalLabels_1(7, 1) = "OFF"            'D11 HIGH
        aryDigitalLabels_1(7, 0) = "ON"             'D11 LOW
        aryDigitalLabels_1(8, 2) = "Classifier"     'D12 Label
        aryDigitalLabels_1(8, 1) = "OFF"            'D12 HIGH
        aryDigitalLabels_1(8, 0) = "ON"             'D12 LOW
        aryDigitalLabels_1(9, 2) = Nothing          'D13 Label
        aryDigitalLabels_1(9, 1) = Nothing          'D13 HIGH
        aryDigitalLabels_1(9, 0) = Nothing          'D13 LOW
        aryDigitalLabels_1(10, 2) = Nothing         'D14 Label
        aryDigitalLabels_1(10, 1) = Nothing         'D14 HIGH
        aryDigitalLabels_1(10, 0) = Nothing         'D14 LOW
        aryDigitalLabels_1(11, 2) = Nothing         'D15 Label
        aryDigitalLabels_1(11, 1) = Nothing         'D15 HIGH
        aryDigitalLabels_1(11, 0) = Nothing         'D15 LOW

        aryActiveRedInput_1(0) = "0"
        aryActiveRedInput_1(1) = "0"
        aryActiveRedInput_1(2) = "0"
        aryActiveRedInput_1(3) = "0"
        aryActiveRedInput_1(4) = "0"
        aryActiveRedInput_1(5) = "0"
        aryActiveRedInput_1(6) = "0"
        aryActiveRedInput_1(7) = "0"
        aryActiveRedInput_1(8) = "0"
        aryActiveRedInput_1(9) = "0"
        aryActiveRedInput_1(10) = "0"
        aryActiveRedInput_1(11) = "0"

        '2 = Title label, 1 = HIGH label, 0 = LOW label
        aryDigitalOutLabels_1(0, 2) = "LED1"            'LED1 Label
        aryDigitalOutLabels_1(0, 1) = "ON"              'LED1 HIGH
        aryDigitalOutLabels_1(0, 0) = "OFF"             'LED1 LOW
        aryDigitalOutLabels_1(1, 2) = "LED2"            'LED2 Label
        aryDigitalOutLabels_1(1, 1) = "ON"              'LED2 HIGH
        aryDigitalOutLabels_1(1, 0) = "OFF"             'LED2 LOW
        aryDigitalOutLabels_1(2, 2) = "LED3"            'LED3 Label
        aryDigitalOutLabels_1(2, 1) = "ON"              'LED3 HIGH
        aryDigitalOutLabels_1(2, 0) = "OFF"             'LED3 LOW
        aryDigitalOutLabels_1(3, 2) = "LED4"            'LED4 Label
        aryDigitalOutLabels_1(3, 1) = "ON"              'LED4 HIGH
        aryDigitalOutLabels_1(3, 0) = "OFF"             'LED4 LOW

        aryDigitalOutLabels_1(4, 2) = "Dig Out 1"       'D0 Label
        aryDigitalOutLabels_1(4, 1) = "OFF"             'D0 HIGH
        aryDigitalOutLabels_1(4, 0) = "ON"              'D0 LOW
        aryDigitalOutLabels_1(5, 2) = "Dig Out 2"       'D1 Label
        aryDigitalOutLabels_1(5, 1) = "OFF"             'D1 HIGH
        aryDigitalOutLabels_1(5, 0) = "ON"              'D1 LOW
        aryDigitalOutLabels_1(6, 2) = "Dig Out 3"       'D2 Label
        aryDigitalOutLabels_1(6, 1) = "CLOSED"          'D2 HIGH
        aryDigitalOutLabels_1(6, 0) = "OPEN"            'D2 LOW
        aryDigitalOutLabels_1(7, 2) = "Dig Out 4"       'D3 Label
        aryDigitalOutLabels_1(7, 1) = "OFF"             'D3 HIGH
        aryDigitalOutLabels_1(7, 0) = "ON"              'D3 LOW
        aryDigitalOutLabels_1(8, 2) = "Dig Out 5"       'D4 Label
        aryDigitalOutLabels_1(8, 1) = "OFF"             'D4 HIGH
        aryDigitalOutLabels_1(8, 0) = "ON"              'D4 LOW
        aryDigitalOutLabels_1(9, 2) = "Dig Out 6"       'D5 Label"
        aryDigitalOutLabels_1(9, 1) = "OFF"             'D5 HIGH
        aryDigitalOutLabels_1(9, 0) = "ON"              'D5 LOW
        aryDigitalOutLabels_1(10, 2) = "Dig Out 7"      'D6 Label"
        aryDigitalOutLabels_1(10, 1) = "OFF"            'D6 HIGH
        aryDigitalOutLabels_1(10, 0) = "ON"             'D6 LOW"
        aryDigitalOutLabels_1(11, 2) = "Dig Out 8"      'D7 Label
        aryDigitalOutLabels_1(11, 1) = "OFF"            'D7 HIGH
        aryDigitalOutLabels_1(11, 0) = "ON"             'D7 LOW

        aryActiveRedOutput_1(0) = "1"
        aryActiveRedOutput_1(1) = "1"
        aryActiveRedOutput_1(2) = "1"
        aryActiveRedOutput_1(3) = "1"
        aryActiveRedOutput_1(4) = "1"
        aryActiveRedOutput_1(5) = "1"
        aryActiveRedOutput_1(6) = "1"
        aryActiveRedOutput_1(7) = "1"
        aryActiveRedOutput_1(8) = "1"
        aryActiveRedOutput_1(9) = "1"
        aryActiveRedOutput_1(10) = "1"
        aryActiveRedOutput_1(11) = "1"

        '************************************************************************************************************************

        'LABEL SET 2 - MIXER
        aryAnalogLabels_2(0) = "Motor Current(1)"
        aryAnalogLabels_2(1) = "Mixer Temp(1)"
        aryAnalogLabels_2(2) = Nothing
        aryAnalogLabels_2(3) = "Motor Current(2)"
        aryAnalogLabels_2(4) = "Mixer Temp(2)"
        aryAnalogLabels_2(5) = Nothing
        aryAnalogLabels_2(6) = Nothing
        aryAnalogLabels_2(7) = Nothing
        aryAnalogLabels_2(8) = Nothing
        aryAnalogLabels_2(9) = Nothing
        aryAnalogLabels_2(10) = Nothing
        aryAnalogLabels_2(11) = Nothing
        aryAnalogLabels_2(12) = Nothing
        aryAnalogLabels_2(13) = Nothing
        aryAnalogLabels_2(14) = Nothing
        aryAnalogLabels_2(15) = Nothing

        '2 = Title label, 1 = HIGH label, 0 = LOW value
        aryDigitalLabels_2(0, 2) = "EMERGENCY STOP 1" 'BTN1 Label
        aryDigitalLabels_2(0, 1) = "RELEASED"       'BTN1 HIGH
        aryDigitalLabels_2(0, 0) = "PUSH"           'BTN1 LOW
        aryDigitalLabels_2(1, 2) = "BTN2"           'BTN2 Label
        aryDigitalLabels_2(1, 1) = "RELEASED"       'BTN2 HIGH
        aryDigitalLabels_2(1, 0) = "PUSH"           'BTN2 LOW
        aryDigitalLabels_2(2, 2) = "BTN3"           'BTN3 Label
        aryDigitalLabels_2(2, 1) = "RELEASED"       'BTN3 HIGH
        aryDigitalLabels_2(2, 0) = "PUSH"           'BTN3 LOW
        aryDigitalLabels_2(3, 2) = "EMERGENCY STOP 2" 'BTN4 Label
        aryDigitalLabels_2(3, 1) = "RELEASED"       'BTN4 HIGH
        aryDigitalLabels_2(3, 0) = "PUSH"           'BTN4 LOW

        aryDigitalLabels_2(4, 2) = "Status(1)"      'D8 Label
        aryDigitalLabels_2(4, 1) = "STOPPED"        'D8 HIGH
        aryDigitalLabels_2(4, 0) = "RUNNING"        'D8 LOW
        aryDigitalLabels_2(5, 2) = "Lid(1)"         'D9 Label
        aryDigitalLabels_2(5, 1) = "OPEN"           'D9 HIGH
        aryDigitalLabels_2(5, 0) = "CLOSED"         'D9 LOW
        aryDigitalLabels_2(6, 2) = "Dump Shoot(1)"  'D10 Label
        aryDigitalLabels_2(6, 1) = "OPEN"           'D10 HIGH
        aryDigitalLabels_2(6, 0) = "SHUT"           'D10 LOW
        aryDigitalLabels_2(7, 2) = "Mixer Speed(1)" 'D11 Label
        aryDigitalLabels_2(7, 1) = "LOW"           'D11 HIGH
        aryDigitalLabels_2(7, 0) = "HIGH"            'D11 LOW
        aryDigitalLabels_2(8, 2) = "Status(2)"      'D12 Label
        aryDigitalLabels_2(8, 1) = "STOPPED"        'D12 HIGH
        aryDigitalLabels_2(8, 0) = "RUNNING"        'D12 LOW
        aryDigitalLabels_2(9, 2) = "Lid(2)"         'D13 Label
        aryDigitalLabels_2(9, 1) = "OPEN"           'D13 HIGH
        aryDigitalLabels_2(9, 0) = "CLOSE"          'D13 LOW
        aryDigitalLabels_2(10, 2) = "Dump Shoot(2)" 'D14 Label
        aryDigitalLabels_2(10, 1) = "OPEN"          'D14 HIGH
        aryDigitalLabels_2(10, 0) = "SHUT"          'D14 LOW
        aryDigitalLabels_2(11, 2) = "Mixer Speed(2)" 'D15 Label
        aryDigitalLabels_2(11, 1) = "LOW"          'D15 HIGH
        aryDigitalLabels_2(11, 0) = "HIGH"         'D15 LOW

        aryActiveRedInput_2(0) = "0"
        aryActiveRedInput_2(1) = "0"
        aryActiveRedInput_2(2) = "0"
        aryActiveRedInput_2(3) = "0"
        aryActiveRedInput_2(4) = "0"
        aryActiveRedInput_2(5) = "0"
        aryActiveRedInput_2(6) = "0"
        aryActiveRedInput_2(7) = "0"
        aryActiveRedInput_2(8) = "0"
        aryActiveRedInput_2(9) = "0"
        aryActiveRedInput_2(10) = "0"
        aryActiveRedInput_2(11) = "0"

        '2 = Title label, 1 = HIGH label, 0 = LOW label
        aryDigitalOutLabels_2(0, 2) = "LED1"            'LED1 Label
        aryDigitalOutLabels_2(0, 1) = "ON"              'LED1 HIGH
        aryDigitalOutLabels_2(0, 0) = "OFF"             'LED1 LOW
        aryDigitalOutLabels_2(1, 2) = "LED2"            'LED2 Label
        aryDigitalOutLabels_2(1, 1) = "ON"              'LED2 HIGH
        aryDigitalOutLabels_2(1, 0) = "OFF"             'LED2 LOW
        aryDigitalOutLabels_2(2, 2) = "LED3"            'LED3 Label
        aryDigitalOutLabels_2(2, 1) = "ON"              'LED3 HIGH
        aryDigitalOutLabels_2(2, 0) = "OFF"             'LED3 LOW
        aryDigitalOutLabels_2(3, 2) = "LED4"            'LED4 Label
        aryDigitalOutLabels_2(3, 1) = "ON"              'LED4 HIGH
        aryDigitalOutLabels_2(3, 0) = "OFF"             'LED4 LOW

        aryDigitalOutLabels_2(4, 2) = "Dig Out 1"       'D0 Label
        aryDigitalOutLabels_2(4, 1) = "OFF"             'D0 HIGH
        aryDigitalOutLabels_2(4, 0) = "ON"              'D0 LOW
        aryDigitalOutLabels_2(5, 2) = "Dig Out 2"       'D1 Label
        aryDigitalOutLabels_2(5, 1) = "OFF"             'D1 HIGH
        aryDigitalOutLabels_2(5, 0) = "ON"              'D1 LOW
        aryDigitalOutLabels_2(6, 2) = "Dig Out 3"       'D2 Label
        aryDigitalOutLabels_2(6, 1) = "CLOSED"          'D2 HIGH
        aryDigitalOutLabels_2(6, 0) = "OPEN"            'D2 LOW
        aryDigitalOutLabels_2(7, 2) = "Dig Out 4"       'D3 Label
        aryDigitalOutLabels_2(7, 1) = "OFF"             'D3 HIGH
        aryDigitalOutLabels_2(7, 0) = "ON"              'D3 LOW
        aryDigitalOutLabels_2(8, 2) = "Dig Out 5"       'D4 Label
        aryDigitalOutLabels_2(8, 1) = "OFF"             'D4 HIGH
        aryDigitalOutLabels_2(8, 0) = "ON"              'D4 LOW
        aryDigitalOutLabels_2(9, 2) = "Dig Out 6"       'D5 Label"
        aryDigitalOutLabels_2(9, 1) = "OFF"             'D5 HIGH
        aryDigitalOutLabels_2(9, 0) = "ON"              'D5 LOW
        aryDigitalOutLabels_2(10, 2) = "Dig Out 7"      'D6 Label"
        aryDigitalOutLabels_2(10, 1) = "OFF"            'D6 HIGH
        aryDigitalOutLabels_2(10, 0) = "ON"             'D6 LOW"
        aryDigitalOutLabels_2(11, 2) = "Dig Out 8"      'D7 Label
        aryDigitalOutLabels_2(11, 1) = "OFF"            'D7 HIGH
        aryDigitalOutLabels_2(11, 0) = "ON"             'D7 LOW

        aryActiveRedOutput_2(0) = "1"
        aryActiveRedOutput_2(1) = "1"
        aryActiveRedOutput_2(2) = "1"
        aryActiveRedOutput_2(3) = "1"
        aryActiveRedOutput_2(4) = "1"
        aryActiveRedOutput_2(5) = "1"
        aryActiveRedOutput_2(6) = "1"
        aryActiveRedOutput_2(7) = "1"
        aryActiveRedOutput_2(8) = "1"
        aryActiveRedOutput_2(9) = "1"
        aryActiveRedOutput_2(10) = "1"
        aryActiveRedOutput_2(11) = "1"

        '************************************************************************************************************************

        'LABEL SET 3 - HOME
        aryAnalogLabels_3(0) = "Analog 0"
        aryAnalogLabels_3(1) = "Analog 1"
        aryAnalogLabels_3(2) = "Analog 2"
        aryAnalogLabels_3(3) = "Analog 3"
        aryAnalogLabels_3(4) = "Analog 4"
        aryAnalogLabels_3(5) = "Analog 5"
        aryAnalogLabels_3(6) = "Analog 6"
        aryAnalogLabels_3(7) = "Analog 7"
        aryAnalogLabels_3(8) = "Analog 8"
        aryAnalogLabels_3(9) = "Analog 9"
        aryAnalogLabels_3(10) = "Analog 10"
        aryAnalogLabels_3(11) = "Analog 11"
        aryAnalogLabels_3(12) = "Analog 12"
        aryAnalogLabels_3(13) = "Analog 13"
        aryAnalogLabels_3(14) = "Analog 14"
        aryAnalogLabels_3(15) = "Analog 15"

        '2 = Title label, 1 = HIGH label, 0 = LOW value
        aryDigitalLabels_3(0, 2) = "BTN1"           'BTN1 Label
        aryDigitalLabels_3(0, 1) = "RELEASED"       'BTN1 HIGH
        aryDigitalLabels_3(0, 0) = "PUSH"           'BTN1 LOW
        aryDigitalLabels_3(1, 2) = "TV Backlight"   'BTN2 Label
        aryDigitalLabels_3(1, 1) = "RELEASED"       'BTN2 HIGH
        aryDigitalLabels_3(1, 0) = "PUSH"           'BTN2 LOW
        aryDigitalLabels_3(2, 2) = "Lounge Speakers" 'BTN3 Label
        aryDigitalLabels_3(2, 1) = "RELEASED"       'BTN3 HIGH
        aryDigitalLabels_3(2, 0) = "PUSH"           'BTN3 LOW
        aryDigitalLabels_3(3, 2) = "Patio Speakers" 'BTN4 Label
        aryDigitalLabels_3(3, 1) = "RELEASED"       'BTN4 HIGH
        aryDigitalLabels_3(3, 0) = "PUSH"           'BTN4 LOW

        aryDigitalLabels_3(4, 2) = "Alarm"          'D8 Label
        aryDigitalLabels_3(4, 1) = "Quiet"          'D8 HIGH
        aryDigitalLabels_3(4, 0) = "TRIGGER"        'D8 LOW
        aryDigitalLabels_3(5, 2) = "Alarm Status"   'D9 Label
        aryDigitalLabels_3(5, 1) = "Disarmed"       'D9 HIGH
        aryDigitalLabels_3(5, 0) = "Armed"          'D9 LOW
        aryDigitalLabels_3(6, 2) = "Main gate"      'D10 Label
        aryDigitalLabels_3(6, 1) = "Closed"         'D10 HIGH
        aryDigitalLabels_3(6, 0) = "Open"           'D10 LOW
        aryDigitalLabels_3(7, 2) = "Power"          'D11 Label
        aryDigitalLabels_3(7, 1) = "ON"             'D11 HIGH
        aryDigitalLabels_3(7, 0) = "FAIL"           'D11 LOW
        aryDigitalLabels_3(8, 2) = "Panic button"   'D12 Label
        aryDigitalLabels_3(8, 1) = "Quiet"          'D12 HIGH
        aryDigitalLabels_3(8, 0) = "PANIC"          'D12 LOW
        aryDigitalLabels_3(9, 2) = "Fence"          'D13 Label
        aryDigitalLabels_3(9, 1) = "Quiet"          'D13 HIGH
        aryDigitalLabels_3(9, 0) = "TRIGGER"        'D13 LOW
        aryDigitalLabels_3(10, 2) = "Fence"         'D14 Label
        aryDigitalLabels_3(10, 1) = "Disarmed"      'D14 HIGH
        aryDigitalLabels_3(10, 0) = "Armed"         'D14 LOW
        aryDigitalLabels_3(11, 2) = "Input 8 (D15)" 'D15  Label
        aryDigitalLabels_3(11, 1) = "HIGH"          'D15 HIGH
        aryDigitalLabels_3(11, 0) = "LOW"           'D15 LOW

        aryActiveRedInput_3(0) = "0"
        aryActiveRedInput_3(1) = "0"
        aryActiveRedInput_3(2) = "0"
        aryActiveRedInput_3(3) = "0"
        aryActiveRedInput_3(4) = "0"
        aryActiveRedInput_3(5) = "0"
        aryActiveRedInput_3(6) = "0"
        aryActiveRedInput_3(7) = "0"
        aryActiveRedInput_3(8) = "0"
        aryActiveRedInput_3(9) = "0"
        aryActiveRedInput_3(10) = "1" 'Fence disarmed
        aryActiveRedInput_3(11) = "0"

        '2 = Title label, 1 = HIGH label, 0 = LOW label
        aryDigitalOutLabels_3(0, 2) = "LED1"            'LED1 Label
        aryDigitalOutLabels_3(0, 1) = "ON"              'LED1 HIGH
        aryDigitalOutLabels_3(0, 0) = "OFF"             'LED1 LOW
        aryDigitalOutLabels_3(1, 2) = "LED2"            'LED2 Label
        aryDigitalOutLabels_3(1, 1) = "ON"              'LED2 HIGH
        aryDigitalOutLabels_3(1, 0) = "OFF"             'LED2 LOW
        aryDigitalOutLabels_3(2, 2) = "LED3"            'LED3 Label
        aryDigitalOutLabels_3(2, 1) = "ON"              'LED3 HIGH
        aryDigitalOutLabels_3(2, 0) = "OFF"             'LED3 LOW
        aryDigitalOutLabels_3(3, 2) = "LED4"            'LED4 Label
        aryDigitalOutLabels_3(3, 1) = "ON"              'LED4 HIGH
        aryDigitalOutLabels_3(3, 0) = "OFF"             'LED4 LOW

        aryDigitalOutLabels_3(4, 2) = "Dig Out 1"       'D0 Label
        aryDigitalOutLabels_3(4, 1) = "OFF"             'D0 HIGH
        aryDigitalOutLabels_3(4, 0) = "ON"              'D0 LOW
        aryDigitalOutLabels_3(5, 2) = "Dig Out 2"       'D1 Label
        aryDigitalOutLabels_3(5, 1) = "OFF"             'D1 HIGH
        aryDigitalOutLabels_3(5, 0) = "ON"              'D1 LOW
        aryDigitalOutLabels_3(6, 2) = "Dig Out 3"       'D2 Label
        aryDigitalOutLabels_3(6, 1) = "CLOSED"          'D2 HIGH
        aryDigitalOutLabels_3(6, 0) = "OPEN"            'D2 LOW
        aryDigitalOutLabels_3(7, 2) = "Dig Out 4"       'D3 Label
        aryDigitalOutLabels_3(7, 1) = "OFF"             'D3 HIGH
        aryDigitalOutLabels_3(7, 0) = "ON"              'D3 LOW
        aryDigitalOutLabels_3(8, 2) = "Dig Out 5"       'D4 Label
        aryDigitalOutLabels_3(8, 1) = "OFF"             'D4 HIGH
        aryDigitalOutLabels_3(8, 0) = "ON"              'D4 LOW
        aryDigitalOutLabels_3(9, 2) = "Dig Out 6"       'D5 Label"
        aryDigitalOutLabels_3(9, 1) = "OFF"             'D5 HIGH
        aryDigitalOutLabels_3(9, 0) = "ON"              'D5 LOW
        aryDigitalOutLabels_3(10, 2) = "Dig Out 7"      'D6 Label"
        aryDigitalOutLabels_3(10, 1) = "OFF"            'D6 HIGH
        aryDigitalOutLabels_3(10, 0) = "ON"             'D6 LOW"
        aryDigitalOutLabels_3(11, 2) = "Dig Out 8"      'D7 Label
        aryDigitalOutLabels_3(11, 1) = "OFF"            'D7 HIGH
        aryDigitalOutLabels_3(11, 0) = "ON"             'D7 LOW

        aryActiveRedOutput_3(0) = "1"
        aryActiveRedOutput_3(1) = "1"
        aryActiveRedOutput_3(2) = "1"
        aryActiveRedOutput_3(3) = "1"
        aryActiveRedOutput_3(4) = "1"
        aryActiveRedOutput_3(5) = "1"
        aryActiveRedOutput_3(6) = "1"
        aryActiveRedOutput_3(7) = "1"
        aryActiveRedOutput_3(8) = "1"
        aryActiveRedOutput_3(9) = "1"
        aryActiveRedOutput_3(10) = "1"
        aryActiveRedOutput_3(11) = "1"

        '************************************************************************************************************************

        'LABEL SET 4 - GENERIC & TEST
        aryAnalogLabels_4(0) = "Analog 0"
        aryAnalogLabels_4(1) = "Analog 1"
        aryAnalogLabels_4(2) = "Analog 2"
        aryAnalogLabels_4(3) = "Analog 3"
        aryAnalogLabels_4(4) = "Analog 4"
        aryAnalogLabels_4(5) = "Analog 5"
        aryAnalogLabels_4(6) = "Analog 6"
        aryAnalogLabels_4(7) = "Analog 7"
        aryAnalogLabels_4(8) = "Analog 8"
        aryAnalogLabels_4(9) = "Analog 9"
        aryAnalogLabels_4(10) = "Analog 10"
        aryAnalogLabels_4(11) = "Analog 11"
        aryAnalogLabels_4(12) = "Analog 11"
        aryAnalogLabels_4(13) = "Analog 13"
        aryAnalogLabels_4(14) = "Analog 14"
        aryAnalogLabels_4(15) = "Analog 15"

        '2 = Title label, 1 = HIGH label, 0 = LOW value
        aryDigitalLabels_4(0, 2) = "BTN1"           'BTN1 Label
        aryDigitalLabels_4(0, 1) = "RELEASED"       'BTN1 HIGH
        aryDigitalLabels_4(0, 0) = "PUSH"           'BTN1 LOW
        aryDigitalLabels_4(1, 2) = "BTN2"           'BTN2 Label
        aryDigitalLabels_4(1, 1) = "RELEASED"       'BTN1 HIGH
        aryDigitalLabels_4(1, 0) = "PUSH"           'BTN2 LOW
        aryDigitalLabels_4(2, 2) = "BTN3"           'BTN3 Label
        aryDigitalLabels_4(2, 1) = "RELEASED"       'BTN3 HIGH
        aryDigitalLabels_4(2, 0) = "PUSH"           'BTN3 LOW
        aryDigitalLabels_4(3, 2) = "BTN4"           'BTN4 Label
        aryDigitalLabels_4(3, 1) = "RELEASED"       'BTN4 HIGH
        aryDigitalLabels_4(3, 0) = "PUSH"           'BTN4 LOW

        aryDigitalLabels_4(4, 2) = "Input 1 (D8)"    'D8 Label
        aryDigitalLabels_4(4, 1) = "HIGH"            'D8 HIGH
        aryDigitalLabels_4(4, 0) = "LOW"             'D8 LOW
        aryDigitalLabels_4(5, 2) = "Input 2 (D9)"   'D9 Label
        aryDigitalLabels_4(5, 1) = "HIGH"           'D9 HIGH
        aryDigitalLabels_4(5, 0) = "LOW"            'D9 LOW
        aryDigitalLabels_4(6, 2) = "Input 3 (D10)"  'D10 Label
        aryDigitalLabels_4(6, 1) = "HIGH"           'D10 HIGH
        aryDigitalLabels_4(6, 0) = "LOW"            'D10 LOW
        aryDigitalLabels_4(7, 2) = "Input 4 (D11)"  'D11 Label
        aryDigitalLabels_4(7, 1) = "HIGH"           'D11 HIGH
        aryDigitalLabels_4(7, 0) = "LOW"            'D11 LOW
        aryDigitalLabels_4(8, 2) = "Input 5 (D12)"  'D12 Label
        aryDigitalLabels_4(8, 1) = "HIGH"           'D12 HIGH
        aryDigitalLabels_4(8, 0) = "LOW"            'D12 LOW
        aryDigitalLabels_4(9, 2) = "Input 6 (D13)"  'D13 Label
        aryDigitalLabels_4(9, 1) = "HIGH"           'D13 HIGH
        aryDigitalLabels_4(9, 0) = "LOW"            'D13 LOW
        aryDigitalLabels_4(10, 2) = "Input 7 (D14)" 'D14 Label
        aryDigitalLabels_4(10, 1) = "HIGH"          'D14 HIGH
        aryDigitalLabels_4(10, 0) = "LOW"           'D14 LOW
        aryDigitalLabels_4(11, 2) = "Input 8 (D15)" 'D15  Label
        aryDigitalLabels_4(11, 1) = "HIGH"          'D15 HIGH
        aryDigitalLabels_4(11, 0) = "LOW"           'D15 LOW

        aryActiveRedInput_4(0) = "0"
        aryActiveRedInput_4(1) = "0"
        aryActiveRedInput_4(2) = "0"
        aryActiveRedInput_4(3) = "0"
        aryActiveRedInput_4(4) = "0"
        aryActiveRedInput_4(5) = "0"
        aryActiveRedInput_4(6) = "0"
        aryActiveRedInput_4(7) = "0"
        aryActiveRedInput_4(8) = "0"
        aryActiveRedInput_4(9) = "0"
        aryActiveRedInput_4(10) = "0"
        aryActiveRedInput_4(11) = "0"

        '2 = Title label, 1 = HIGH label, 0 = LOW label
        aryDigitalOutLabels_4(0, 2) = "LED1"            'LED1 Label
        aryDigitalOutLabels_4(0, 1) = "ON"              'LED1 HIGH
        aryDigitalOutLabels_4(0, 0) = "OFF"             'LED1 LOW
        aryDigitalOutLabels_4(1, 2) = "LED2"            'LED2 Label
        aryDigitalOutLabels_4(1, 1) = "ON"              'LED2 HIGH
        aryDigitalOutLabels_4(1, 0) = "OFF"             'LED2 LOW
        aryDigitalOutLabels_4(2, 2) = "LED3"            'LED3 Label
        aryDigitalOutLabels_4(2, 1) = "ON"              'LED3 HIGH
        aryDigitalOutLabels_4(2, 0) = "OFF"             'LED3 LOW
        aryDigitalOutLabels_4(3, 2) = "LED4"            'LED4 Label
        aryDigitalOutLabels_4(3, 1) = "ON"              'LED4 HIGH
        aryDigitalOutLabels_4(3, 0) = "OFF"             'LED4 LOW

        aryDigitalOutLabels_4(4, 2) = "Dig Out 1"       'D0 Label
        aryDigitalOutLabels_4(4, 1) = "OFF"             'D0 HIGH
        aryDigitalOutLabels_4(4, 0) = "ON"              'D0 LOW
        aryDigitalOutLabels_4(5, 2) = "Dig Out 2"       'D1 Label
        aryDigitalOutLabels_4(5, 1) = "OFF"             'D1 HIGH
        aryDigitalOutLabels_4(5, 0) = "ON"              'D1 LOW
        aryDigitalOutLabels_4(6, 2) = "Dig Out 3"       'D2 Label
        aryDigitalOutLabels_4(6, 1) = "CLOSED"          'D2 HIGH
        aryDigitalOutLabels_4(6, 0) = "OPEN"            'D2 LOW
        aryDigitalOutLabels_4(7, 2) = "Dig Out 4"       'D3 Label
        aryDigitalOutLabels_4(7, 1) = "OFF"             'D3 HIGH
        aryDigitalOutLabels_4(7, 0) = "ON"              'D3 LOW
        aryDigitalOutLabels_4(8, 2) = "Dig Out 5"       'D4 Label
        aryDigitalOutLabels_4(8, 1) = "OFF"             'D4 HIGH
        aryDigitalOutLabels_4(8, 0) = "ON"              'D4 LOW
        aryDigitalOutLabels_4(9, 2) = "Dig Out 6"       'D5 Label"
        aryDigitalOutLabels_4(9, 1) = "OFF"             'D5 HIGH
        aryDigitalOutLabels_4(9, 0) = "ON"              'D5 LOW
        aryDigitalOutLabels_4(10, 2) = "Dig Out 7"      'D6 Label"
        aryDigitalOutLabels_4(10, 1) = "OFF"            'D6 HIGH
        aryDigitalOutLabels_4(10, 0) = "ON"             'D6 LOW"
        aryDigitalOutLabels_4(11, 2) = "Dig Out 8"      'D7 Label
        aryDigitalOutLabels_4(11, 1) = "OFF"            'D7 HIGH
        aryDigitalOutLabels_4(11, 0) = "ON"             'D7 LOW

        aryActiveRedOutput_4(0) = "1"
        aryActiveRedOutput_4(1) = "1"
        aryActiveRedOutput_4(2) = "1"
        aryActiveRedOutput_4(3) = "1"
        aryActiveRedOutput_4(4) = "1"
        aryActiveRedOutput_4(5) = "1"
        aryActiveRedOutput_4(6) = "1"
        aryActiveRedOutput_4(7) = "1"
        aryActiveRedOutput_4(8) = "1"
        aryActiveRedOutput_4(9) = "1"
        aryActiveRedOutput_4(10) = "1"
        aryActiveRedOutput_4(11) = "1"

        '************************************************************************************************************************

        'LABEL SET 5 - HADECO COLD ROOM
        aryAnalogLabels_5(0) = "Relative Humidity 1"
        aryAnalogLabels_5(1) = "Relative Humidity 2"
        aryAnalogLabels_5(2) = "Relative Humidity 3"
        aryAnalogLabels_5(3) = "Relative Humidity 4"
        aryAnalogLabels_5(4) = "Relative Humidity 5"
        aryAnalogLabels_5(5) = "Relative Humidity 6"
        aryAnalogLabels_5(6) = "Temperature 1"
        aryAnalogLabels_5(7) = "Temperature 2"
        aryAnalogLabels_5(8) = "Temperature 3"
        aryAnalogLabels_5(9) = "Temperature 4"
        aryAnalogLabels_5(10) = "Temperature 5"
        aryAnalogLabels_5(11) = "Temperature 6"
        aryAnalogLabels_5(12) = "Temperature 7"
        aryAnalogLabels_5(13) = "Binne Louvre"
        aryAnalogLabels_5(14) = "Buiten Louvre"
        aryAnalogLabels_5(15) = "Solar Louvre"

        '2 = Title label, 1 = HIGH label, 0 = LOW value
        aryDigitalLabels_5(0, 2) = "BTN1"           'BTN1 Label
        aryDigitalLabels_5(0, 1) = "RELEASED"       'BTN1 HIGH
        aryDigitalLabels_5(0, 0) = "PUSH"           'BTN1 LOW
        aryDigitalLabels_5(1, 2) = "BTN2"           'BTN2 Label
        aryDigitalLabels_5(1, 1) = "RELEASED"       'BTN1 HIGH
        aryDigitalLabels_5(1, 0) = "PUSH"           'BTN2 LOW
        aryDigitalLabels_5(2, 2) = "BTN3"           'BTN3 Label
        aryDigitalLabels_5(2, 1) = "RELEASED"       'BTN3 HIGH
        aryDigitalLabels_5(2, 0) = "PUSH"           'BTN3 LOW
        aryDigitalLabels_5(3, 2) = "BTN4"           'BTN4 Label
        aryDigitalLabels_5(3, 1) = "RELEASED"       'BTN4 HIGH
        aryDigitalLabels_5(3, 0) = "PUSH"           'BTN4 LOW

        aryDigitalLabels_5(4, 2) = "Main Door"      'D8 Label
        aryDigitalLabels_5(4, 1) = "OPEN"           'D8 HIGH
        aryDigitalLabels_5(4, 0) = "CLOSED"         'D8 LOW
        aryDigitalLabels_5(5, 2) = "Available"      'D9 Label
        aryDigitalLabels_5(5, 1) = "AVLB"           'D9 HIGH
        aryDigitalLabels_5(5, 0) = "AVLB"           'D9 LOW
        aryDigitalLabels_5(6, 2) = "Movement"       'D10 Label
        aryDigitalLabels_5(6, 1) = "MOTION"         'D10 HIGH
        aryDigitalLabels_5(6, 0) = "STILL"          'D10 LOW
        aryDigitalLabels_5(7, 2) = "FANS"           'D11 Label
        aryDigitalLabels_5(7, 1) = "OFF"            'D11 HIGH
        aryDigitalLabels_5(7, 0) = "ON"             'D11 LOW
        aryDigitalLabels_5(8, 2) = "Input 5 (D12)"  'D12 Label
        aryDigitalLabels_5(8, 1) = "HIGH"           'D12 HIGH
        aryDigitalLabels_5(8, 0) = "LOW"            'D12 LOW
        aryDigitalLabels_5(9, 2) = "Input 6 (D13)"  'D13 Label
        aryDigitalLabels_5(9, 1) = "HIGH"           'D13 HIGH
        aryDigitalLabels_5(9, 0) = "LOW"            'D13 LOW
        aryDigitalLabels_5(10, 2) = "Input 7 (D14)" 'D14 Label
        aryDigitalLabels_5(10, 1) = "HIGH"          'D14 HIGH
        aryDigitalLabels_5(10, 0) = "LOW"           'D14 LOW
        aryDigitalLabels_5(11, 2) = "Input 8 (D15)" 'D15  Label
        aryDigitalLabels_5(11, 1) = "HIGH"          'D15 HIGH
        aryDigitalLabels_5(11, 0) = "LOW"           'D15 LOW

        aryActiveRedInput_5(0) = "0"
        aryActiveRedInput_5(1) = "0"
        aryActiveRedInput_5(2) = "0"
        aryActiveRedInput_5(3) = "0"
        aryActiveRedInput_5(4) = "1"
        aryActiveRedInput_5(5) = "0"
        aryActiveRedInput_5(6) = "1"
        aryActiveRedInput_5(7) = "0"
        aryActiveRedInput_5(8) = "0"
        aryActiveRedInput_5(9) = "0"
        aryActiveRedInput_5(10) = "0"
        aryActiveRedInput_5(11) = "0"

        '2 = Title label, 1 = HIGH label, 0 = LOW label
        aryDigitalOutLabels_5(0, 2) = "LED1"            'LED1 Label
        aryDigitalOutLabels_5(0, 1) = "ON"              'LED1 HIGH
        aryDigitalOutLabels_5(0, 0) = "OFF"             'LED1 LOW
        aryDigitalOutLabels_5(1, 2) = "LED2"            'LED2 Label
        aryDigitalOutLabels_5(1, 1) = "ON"              'LED2 HIGH
        aryDigitalOutLabels_5(1, 0) = "OFF"             'LED2 LOW
        aryDigitalOutLabels_5(2, 2) = "LED3"            'LED3 Label
        aryDigitalOutLabels_5(2, 1) = "ON"              'LED3 HIGH
        aryDigitalOutLabels_5(2, 0) = "OFF"             'LED3 LOW
        aryDigitalOutLabels_5(3, 2) = "LED4"            'LED4 Label
        aryDigitalOutLabels_5(3, 1) = "ON"              'LED4 HIGH
        aryDigitalOutLabels_5(3, 0) = "OFF"             'LED4 LOW

        aryDigitalOutLabels_5(4, 2) = "Fan Speed 1"     'D0 Label
        aryDigitalOutLabels_5(4, 1) = "ON"              'D0 HIGH
        aryDigitalOutLabels_5(4, 0) = "OFF"             'D0 LOW
        aryDigitalOutLabels_5(5, 2) = "Fan Speed 2"     'D1 Label
        aryDigitalOutLabels_5(5, 1) = "ON"              'D1 HIGH
        aryDigitalOutLabels_5(5, 0) = "OFF"             'D1 LOW
        aryDigitalOutLabels_5(6, 2) = "Fan Speed 3"     'D2 Label
        aryDigitalOutLabels_5(6, 1) = "ON"              'D2 HIGH
        aryDigitalOutLabels_5(6, 0) = "OFF"             'D2 LOW
        aryDigitalOutLabels_5(7, 2) = "Fan Speed 4"     'D3 Label
        aryDigitalOutLabels_5(7, 1) = "ON"              'D3 HIGH
        aryDigitalOutLabels_5(7, 0) = "OFF"             'D3 LOW
        aryDigitalOutLabels_5(8, 2) = "Boiler"         'D4 Label
        aryDigitalOutLabels_5(8, 1) = "ON"              'D4 HIGH
        aryDigitalOutLabels_5(8, 0) = "OFF"             'D4 LOW
        aryDigitalOutLabels_5(9, 2) = "Element"         'D5 Label"
        aryDigitalOutLabels_5(9, 1) = "ON"              'D5 HIGH
        aryDigitalOutLabels_5(9, 0) = "OFF"             'D5 LOW
        aryDigitalOutLabels_5(10, 2) = "Inblaas"        'D6 Label"
        aryDigitalOutLabels_5(10, 1) = "ON"             'D6 HIGH
        aryDigitalOutLabels_5(10, 0) = "OFF"            'D6 LOW"
        aryDigitalOutLabels_5(11, 2) = "Dig Out 8"      'D7 Label
        aryDigitalOutLabels_5(11, 1) = "ON"             'D7 HIGH
        aryDigitalOutLabels_5(11, 0) = "OFF"            'D7 LOW

        aryActiveRedOutput_5(0) = "1"
        aryActiveRedOutput_5(1) = "1"
        aryActiveRedOutput_5(2) = "1"
        aryActiveRedOutput_5(3) = "1"
        aryActiveRedOutput_5(4) = "1"
        aryActiveRedOutput_5(5) = "1"
        aryActiveRedOutput_5(6) = "1"
        aryActiveRedOutput_5(7) = "1"
        aryActiveRedOutput_5(8) = "1"
        aryActiveRedOutput_5(9) = "1"
        aryActiveRedOutput_5(10) = "1"
        aryActiveRedOutput_5(11) = "1"


        'Peripheral hex codes and labels to match
        'aryPeripherals(0, 0) = "6043ED150000"
        'aryPeripherals(0, 1) = "Pelitizer 1"
        'aryPeripherals(1, 0) = "E80737140000"
        'aryPeripherals(1, 1) = "Pelitizer 2"
        'aryPeripherals(2, 0) = "E80737140000"
        'aryPeripherals(2, 1) = "Pelitizer 3"

        'Look up the loaded HEX codes in the .ini file and their matching labels
        Dim strValue As String
        For n = 0 To 49
            strValue = CStr(n + 1000)
            strValue = Trim(GetIni(strValue))
            If (Len(strValue) >= 14) Then
                aryPeripherals(n, 0) = Trim(Left(strValue, 12))
                aryPeripherals(n, 1) = Mid(strValue, 14, Len(strValue) - 13)
            End If
        Next

        aryChartLabel(0) = "Motor Current"
        aryChartLabel(1) = "A1"
        aryChartLabel(2) = "A2"
        aryChartLabel(3) = "A3"
        aryChartLabel(4) = "A4"
        aryChartLabel(5) = "Head Pressure"
        aryChartLabel(6) = "A6"
        aryChartLabel(7) = "A7"
        aryChartLabel(8) = "Head Temp"
        aryChartLabel(9) = "A9"


    End Sub

    Public Function GetIni(ByVal strItem As String) As String

        Try
            Dim strValue, strParameter, inpLine, strResult, strAppPath As String
            Dim n, x As Integer

            Dim strFilename As String = "mimics.ini"
            If Not File.Exists(strFilename) Then strFilename = "C:\CMM mimics\mimics.ini"

            ' Create an instance of StreamReader to read from a file.
            Dim sr As StreamReader = New StreamReader(strFilename)
            Dim strLine As String

            Do Until strValue = "[END]" Or x > 1000
                strValue = sr.ReadLine
                n = 1
                Do Until Microsoft.VisualBasic.Right(inpLine, 1) = "="
                    inpLine = (Microsoft.VisualBasic.Left(strValue, n))
                    If n > Len(strValue) Then GoTo NextLine
                    n = n + 1
                Loop
                strParameter = (Microsoft.VisualBasic.Left(inpLine, n - 2))
                If (strParameter) = (strItem) Then
                    sr.Close()
                    Return Trim((Microsoft.VisualBasic.Right(strValue, Len(strValue) - n + 1)))
                Else
                    inpLine = "Nothing"
                End If
NextLine:
                x = x + 1
            Loop

            sr.Close()

            Return strResult

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Function

    
    Public Sub Delay(ByVal dblSeconds As Double)

        Dim Start, Finish, TotalTime As Double

        Start = Microsoft.VisualBasic.DateAndTime.Timer
        Finish = Start + dblSeconds  ' Set end time 
        Do While Microsoft.VisualBasic.DateAndTime.Timer < Finish
            ' Do other processing while waiting for intSeconds seconds to elapse.
        Loop

        TotalTime = Microsoft.VisualBasic.DateAndTime.Timer - Start

    End Sub
    Public Function GetIpV4() As String

        Dim myHost As String = Dns.GetHostName
        Dim ipEntry As IPHostEntry = Dns.GetHostEntry(myHost)
        Dim ip As String = ""

        For Each tmpIpAddress As IPAddress In ipEntry.AddressList
            If tmpIpAddress.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                Dim ipAddress As String = tmpIpAddress.ToString
                ip = ipAddress
                Exit For
            End If
        Next

        If ip = "" Then
            Throw New Exception("No 10. IP found!")
        End If

        Return ip

    End Function
    Public Function fixButtonImage(ByVal BTN As System.Windows.Forms.ToolStripButton) As System.Windows.Forms.ToolStripButton
        Try

            'resize the image of the button to the new size
            Dim sourceWidth As Integer = BTN.Image.Width
            Dim sourceHeight As Integer = BTN.Image.Height
            Dim b As New Bitmap(40, 40)
            Dim g As Graphics = Graphics.FromImage(DirectCast(b, Image))
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(BTN.Image, 0, 0, 40, 40)
            g.Dispose()
            Dim myResizedImg As Image = DirectCast(b, Image)
            BTN.Image = myResizedImg
            Return BTN

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Public Function sendUDPglobal(ByVal strMsg As String, ByVal strIP As String) As Boolean

        ' see if we need to get a new udpClient pointing to the server
        If m_udpClient_global Is Nothing Then
            'Update()
            Try
                Dim portNbr As Integer = Convert.ToInt32("44200")
                'strIP = "eastlane.dyndns.org"
                m_udpClient_global = New UdpClient(strIP, portNbr)
            Catch ex As Exception
                If m_udpClient_global IsNot Nothing Then
                    m_udpClient_global.Close()
                    m_udpClient_global = Nothing
                End If
                MsgBox(ex.Message)
                Return False
            End Try
        End If

        ' we resolved our endpoint to the server
        ' go ahead and send the string.
        Try
            Dim ascii As Encoding = Encoding.ASCII
            Dim encodedBytes As [Byte]() = ascii.GetBytes(strMsg)
            m_udpClient_global.Send(encodedBytes, encodedBytes.Length)
            m_udpClient_global.Close()
            m_udpClient_global = Nothing
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try

    End Function
    'Public Function mimicDate(ByVal lngEpochTime As Long)

    '    'unix = 1356998400 ' 01 01 2013 00:00:00
    '    '1375531467 = 3/08/2013 12:04:27
    '    '.Format("{0:dd/MM/yyyy}", DateTime.Now)

    '    Try
    '        Dim baseDate As New DateTime(1970, 1, 1, 0, 0, 0)
    '        Dim datDate As Date = baseDate.ToLocalTime().AddSeconds(lngEpochTime)
    '        Dim strDate As String = datDate.ToString(Format("yyyy/MM/dd"))
    '        If (strDate.IndexOf("/") = -1) Then
    '            strDate = Left(strDate, 4) & "/" & Microsoft.VisualBasic.Mid(strDate, 6, 2) & "/" & Microsoft.VisualBasic.Mid(strDate, 9, 2)
    '        End If

    '        Return strDate

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Function
    'Public Function mimicTime(ByVal lngEpochTime As Long)

    '    'unix = 1356998400 ' 01 01 2013 00:00:00
    '    '1375531467 = 3/08/2013 12:04:27

    '    Try
    '        Dim baseDate As New DateTime(1970, 1, 1, 0, 0, 0)
    '        Dim datDate As Date = baseDate.ToLocalTime().AddSeconds(lngEpochTime)
    '        Dim strTime As String = datDate.ToString(Format("HH:mm:ss"))

    '        Return strTime

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Function
    Public Function mimicDateTime(ByVal lngEpochTime As Long)

        'unix = 1356998400 ' 01 01 2013 00:00:00
        '1375531467 = 3/08/2013 12:04:27

        Try
            Dim baseDate As New DateTime(1970, 1, 1, 0, 0, 0)
            Dim datDate As Date = baseDate.ToLocalTime().AddSeconds(lngEpochTime)
            Dim strDateTime As String = datDate.ToString(Format("yyyy/MM/dd HH:mm:ss"))
            If (strDateTime.IndexOf("/") = -1) Then
                strDateTime = Left(strDateTime, 4) & "/" & Microsoft.VisualBasic.Mid(strDateTime, 6, 2) & "/" & Microsoft.VisualBasic.Mid(strDateTime, 9, 2)
            End If

            Return strDateTime

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Public Function mimicDate(ByVal lngEpochTime As Long)

        'unix = 1356998400 ' 01 01 2013 00:00:00
        '1375531467 = 3/08/2013 12:04:27
        '.Format("{0:dd/MM/yyyy}", DateTime.Now)

        Try
            Dim baseDate As New DateTime(1970, 1, 1, 0, 0, 0)
            Dim datDate As Date = baseDate.ToLocalTime().AddSeconds(lngEpochTime)
            Dim strDate As String = datDate.ToString(Format("yyyy/MM/dd"))
            If (strDate.IndexOf("/") = -1) Then
                strDate = Left(strDate, 4) & "-" & Microsoft.VisualBasic.Mid(strDate, 6, 2) & "-" & Microsoft.VisualBasic.Mid(strDate, 9, 2)
            End If

            Return strDate

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Public Function mimicTime(ByVal lngEpochTime As Long)

        'unix = 1356998400 ' 01 01 2013 00:00:00
        '1375531467 = 3/08/2013 12:04:27

        Try
            Dim baseDate As New DateTime(1970, 1, 1, 0, 0, 0)
            Dim datDate As Date = baseDate.ToLocalTime().AddSeconds(lngEpochTime) ' + 7200)
            Dim strTime As String = datDate.ToString(Format("HH:mm:ss"))

            Return strTime

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Public Function fmt(ByVal strValue As String, ByVal num As Integer) As String

        Try
            If Len(strValue) = 0 Then Return "    "
            If Len(strValue) = 1 Then Return "   " & strValue
            If Len(strValue) = 2 Then Return "  " & strValue
            If Len(strValue) = 3 Then Return " " & strValue
            If Len(strValue) >= 4 Then Return strValue

        Catch ex As Exception
            Return strValue
        End Try

    End Function
    Public Sub WriteToLog(ByVal strMessage As String, ByVal strFilename As String, Optional ByVal blnLine As Boolean = False)

        Try
            strFilename = "C:\CMM mimics\" & strFilename
            Dim sw As StreamWriter

            'Check that the log file exists and create it if it is absent
            If Not File.Exists(strFilename) Then
                sw = New StreamWriter(strFilename)
                sw.WriteLine("CMM mimics log file")
                sw.WriteLine("Created: " & Now.ToString("dd MMM yyyy HH:mm:ss"))
                sw.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                sw.Close()
                sw = Nothing
            End If

            'Write the received message to the log file
            sw = File.AppendText(strFilename)
            sw.WriteLine(Now.ToString("dd MMM yyyy HH:mm:ss") & " : " & strMessage)
            If blnLine = True Then sw.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
            sw.Close()
            sw = Nothing


        Catch ex As Exception
            WriteToLog(ex.ToString(), True)
        End Try

    End Sub
    Public Function dblTime(ByVal strTime As String) As Double
        On Error GoTo Err
        'A wrapper that will return an double value representative of the string time
        'value (eg. "17:45") that is passed to it

        Dim HH, mm, ss As Integer

        If Len(strTime) = 5 Then
            HH = CInt(Left(strTime, 2))
            mm = CInt(Right(strTime, 2))
            Return HH + mm / 60
        ElseIf Len(strTime) = 8 Then
            HH = CInt(Left(strTime, 2))
            mm = CInt(Mid(strTime, 4, 2))
            ss = CInt(Right(strTime, 2))
            Return HH + (mm / 60) + (ss / 3600)
        End If

        If Len(strTime) = 4 And Mid(strTime, 2, 1) = ":" Then
            HH = CInt(Left(strTime, 1))
            mm = CInt(Right(strTime, 2))
            Return HH + mm / 60
        End If

ErrExit:
        Exit Function
Err:

        MsgBox(Err.Description & ":" & Err.Number)
        Resume ErrExit

    End Function
    Public Function dblTime(ByVal datTime As Date) As Double

        'Will do the same as the above, but accepts a date value as its argument

        Dim strTime As String

        strTime = CStr(datTime.ToString("HH:mm"))

        Return CDbl(Left(strTime, 2)) + CDbl(Right(strTime, 2)) / 60

    End Function
    Public Function convert_to_unix(ByVal datDate As Date) As Long

        Try

            Dim uTime As Long
            uTime = (datDate - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Return uTime

        Catch ex As Exception
            MsgBox(Err.Description & ":" & Err.Number)
        End Try

    End Function
    Public Function Hadeco_Cycle_Day(ByVal strDate As String) As String
        Try

            Dim lngPeriod As Long

            If IsDate(strDate) Then
                Dim lngCurrent = convert_to_unix(Now())
                Dim lngStart = convert_to_unix(strDate)
                lngPeriod = (lngCurrent - lngStart)
            Else
                Return Nothing
            End If

            Dim strResult As String

            If (lngPeriod > 864000) Then Return " [x]" '//Invalid
            If (lngPeriod < 0) Then strResult = " [x]" '//Invalid
            If (lngPeriod > 0 And lngPeriod <= 86400) Then strResult = 1 '//'Day 1
            If (lngPeriod > 86400 And lngPeriod <= 172800) Then strResult = 2 '//Day 2
            If (lngPeriod > 172800 And lngPeriod <= 259200) Then strResult = 3 '//Day 3
            If (lngPeriod > 259200 And lngPeriod <= 345600) Then strResult = 4 '//Day 4
            If (lngPeriod > 345600 And lngPeriod <= 432000) Then strResult = 5 '//Day 5
            If (lngPeriod > 432000 And lngPeriod <= 518400) Then strResult = 6 '//Day 6
            If (lngPeriod > 518400 And lngPeriod <= 608400) Then strResult = 7 '//Day 7
            If (lngPeriod > 604800 And lngPeriod <= 691200) Then strResult = 8 '//Day 8
            If (lngPeriod > 691200 And lngPeriod <= 777600) Then strResult = 9 '//Day 9
            If (lngPeriod > 777600 And lngPeriod <= 864000) Then strResult = 10 '//Day 10 

            Return " [Day " & strResult & "]"

        Catch ex As Exception

        End Try
    End Function
End Module
