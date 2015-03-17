Imports System
Imports System.Net
Imports System.Text
Imports System.Net.Sockets
Imports System.Threading

Public Class clsUDP

    'The form that owns the Value variable.
    Private m_MyForm As frmMonitor
    'Define a delegate type for the form's actionInput method.
    Private Delegate Sub actionInputDelegateType(ByVal sz As String)
    'Declare a delegate variable to point to the form's DisplayValue method.
    Private m_actionInputDelegate As actionInputDelegateType

    Public Sub New(ByVal my_form As frmMonitor)

        m_MyForm = my_form

        ' Initialize the delegate variable to point to the form's actionInput method.
        m_actionInputDelegate = AddressOf m_MyForm.actionInput 'DisplayValue
    End Sub
    Public Sub rebroadcastUDP()

        'Technically multicasting addresses must be between 224.0.0.0 to 239.255.255.255, but 224.0.0.0 to 224.0.0.255 is reserved 
        'for routing info so you should really only use 224.0.1.0 to 239.255.255.255

        Try
            'Set up a UDP listener on port 44300 for incoming control panel messages
            Dim udpListener As New UdpClient()
            Dim listeningEndPoint As New IPEndPoint(IPAddress.Any, 44300)
            udpListener.Client.Bind(listeningEndPoint) ''
            udpListener.Client.ReceiveTimeout = -1

            'Main Listening loop
            Do
                Dim rgbIn As [Byte]() = Nothing

                'Listen for an incoming datagram
                If (InlineAssignHelper(rgbIn, udpListener.Receive(listeningEndPoint))) IsNot Nothing Then
                    Try
                        '--------------------------ACTION THE RECEIVED DATAGRAM - NO REBROADCAST----------------------
                        Dim strHEX As String = ByteArrayToString(rgbIn)
                        strHEX = strHEX.ToUpper
                        If (Mid(strHEX, 4, 1) = "x") Then xMessage = Microsoft.VisualBasic.Right(strHEX, Len(strHEX) - 4)
                        SyncLock m_MyForm
                            If m_MyForm.InvokeRequired() Then
                                m_MyForm.Invoke(m_actionInputDelegate, strHEX)
                            End If
                        End SyncLock
                        '---------------------------------------------------------------------------------------------
                        '--------------------------REBROADCAST--------------------------------------------------------
                        ''If UDP data was received re broadcast out the UDP message (multicast) for any other instances to read
                        'Dim udpclient As New UdpClient()
                        'Dim multicastaddress As IPAddress = IPAddress.Parse("224.0.1.0") '239.0.0.222")
                        'udpclient.JoinMulticastGroup(multicastaddress)
                        'Dim remoteep As New IPEndPoint(multicastaddress, 44301) '2222)
                        'udpclient.Send(rgbIn, rgbIn.Length, remoteep)
                        '----------------------------------------------------------------------------------------------
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, "Main UDP Rebroadcast Listening loop")
                    End Try
                End If
            Loop While True
            '----------------------------------------------------------------------------------------------------

        Catch ex As Exception
            ' An unexpected error.
            Debug.WriteLine("Unexpected error in thread " & ex.Message)
        End Try
    End Sub
    Public Sub listenUDP()

        'This is a multicast UDP listening routine
        'Mimics messages will have been received on port 44300 and rebroadcast as multicast on port 44301
        'Those packets are received here by enrolling in the the multicast group
        'Technically multicasting addresses must be between 224.0.0.0 to 239.255.255.255, but 224.0.0.0 to 224.0.0.255 is reserved 
        'for routing info so you should really only use 224.0.1.0 to 239.255.255.255

        Try
            Dim client As New UdpClient()
            client.ExclusiveAddressUse = False
            Dim localEp As New IPEndPoint(IPAddress.Any, 44301) '2222)
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, True)
            client.ExclusiveAddressUse = False
            client.Client.Bind(localEp)
            Dim multicastaddress As IPAddress = IPAddress.Parse("224.0.1.0") '239.0.0.222")
            client.JoinMulticastGroup(multicastaddress)

            Do
                Dim data As [Byte]() = Nothing
                'Listen for an incoming rebroadcast datagram
                If (InlineAssignHelper(data, client.Receive(localEp))) IsNot Nothing Then
                    Try
                        Dim strHEX As String = ByteArrayToString(data)
                        strHEX = strHEX.ToUpper
                        If (Mid(strHEX, 4, 1) = "x") Then xMessage = Microsoft.VisualBasic.Right(strHEX, Len(strHEX) - 4)
                        SyncLock m_MyForm
                            If m_MyForm.InvokeRequired() Then
                                m_MyForm.Invoke(m_actionInputDelegate, strHEX)
                            End If
                        End SyncLock
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, "listenUDP Listening loop")
                    End Try
                End If
            Loop While True

        Catch ex As Exception
            ' An unexpected error.
            Debug.WriteLine("Unexpected error in thread " & ex.Message)
        End Try
    End Sub

    Private Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T

        'var sr = new StreamReader(filePath);
        'string line;
        'while ((line = sr.ReadLine()) != null)
        '{
        '// do something with line
        '}
        'is the C# equivalent of
        'Dim sr As New StreamReader(filePath)
        'Dim line As String

        'While InlineAssignHelper(line, sr.ReadLine()) IsNot Nothing
        '    ' do something with line
        'End While

        target = value
        Return value
    End Function

    Private Function ByteArrayToString(ByVal ba As Byte()) As String
        Dim hex As String = BitConverter.ToString(ba)
        Return hex.Replace("-", "")
    End Function
End Class
