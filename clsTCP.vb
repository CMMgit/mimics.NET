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

Public Class clsTCP

    Public Event evPrintText(ByVal szPrefix As String, ByVal szText As String, ByVal color__1 As Color)

    Private Const szError As String = "Error: "
    Private Const szNone As String = ""
    Private Const szInfo As String = "Status: "
    Private Const szSend As String = "Sent: "
    Private Const szReceived As String = "Received: "
    Private tcpClient As TcpClient = Nothing
    Private nwStream As NetworkStream = Nothing
    Private thrd As Thread = Nothing

    Public Sub TCP_Send(ByVal strIP As String, ByVal intPort As Integer, ByVal strMsg As String)

        Dim portNbr As Integer = Convert.ToInt32(intPort)

        Try
            ' see if we need to connect to a server
            If nwStream Is Nothing OrElse tcpClient Is Nothing OrElse Not tcpClient.Connected Then
                PrintText(("Attempting to connect to: " + strIP), Color.Orange)
                Try
                    thrd = New Thread(AddressOf ListenForResponse)
                    '***************************************************************
                    tcpClient = New System.Net.Sockets.TcpClient
                    Dim result = tcpClient.BeginConnect(strIP, portNbr, Nothing, Nothing)
                    result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1))
                    If Not tcpClient.Connected Then
                        Throw New Exception("Failed to connect.")
                    End If

                    ' we have connected
                    tcpClient.EndConnect(result)
                    '***************************************************************
                    'tcpClient = New System.Net.Sockets.TcpClient
                    'If (tcpClient.Connected = True) Then tcpClient.Close()
                    'tcpClient.SendTimeout = 1 'mS
                    'tcpClient.ReceiveTimeout = 1 'mS
                    'tcpClient.Connect(strIP, portNbr)

                    nwStream = tcpClient.GetStream()

                    thrd.Start(Me)
                Catch
                    If nwStream IsNot Nothing Then
                        nwStream.Close()
                    End If
                    If tcpClient IsNot Nothing Then
                        tcpClient.Close()
                    End If
                    PrintText(("Unable to connect to" + strIP), Color.Red)
                    Return
                End Try
            End If
        Catch ex As Exception
            PrintText(("TCP/IP error: " & ex.Message), Color.Red)
            'MsgBox(ex.Message)
        End Try

        ' we are connected at this point
        ' go ahead and send the string.
        Try
            Dim ascii As Encoding = Encoding.ASCII
            Dim encodedBytes As [Byte]() = ascii.GetBytes(strMsg)

            nwStream.Write(encodedBytes, 0, encodedBytes.Length)
            PrintText(strMsg, Color.Green)
        Catch
            PrintText("Unable to send string " + strMsg, Color.Red)
            Return
        End Try

    End Sub

    Private Sub PrintText(ByVal szText As String, ByVal color__1 As Color)
        Dim szPrefix As String = szNone

        If color__1 = Color.Red Then
            szPrefix = szError
        ElseIf color__1 = Color.Orange Then
            szPrefix = szInfo
        ElseIf color__1 = Color.Blue Then
            szPrefix = szReceived
        ElseIf color__1 = Color.Green Then
            szPrefix = szSend
        End If

        If (szText = "~~") Then szText = "updated ini file to EEPROM"
        RaiseEvent evPrintText(szPrefix, szText, color__1)

    End Sub

    Private Sub ListenForResponse(ByVal obj As [Object])

        Dim [me] As clsTCP = DirectCast(obj, clsTCP)
        Dim rgbIn As [Byte]() = New [Byte](1023) {}
        Dim cbRead As Integer = 0

        PrintText("Listening...", Color.Black)

        Try
            While (InlineAssignHelper(cbRead, [me].nwStream.Read(rgbIn, 0, rgbIn.Length))) > 0
                Try
                    Dim ascii As Encoding = Encoding.ASCII
                    Dim sz As String = ascii.GetString(rgbIn, 0, cbRead)
                    Dim strHEX As String = ByteArrayToString(rgbIn)
                    strHEX = strHEX.ToUpper

                    'If returned byte array is >= 90 it means that it is a TCP/IP checkIn message
                    If (Len(sz) >= 90) Then sz = strHEX

                    PrintText(sz, Color.Blue)

                Catch
                    PrintText("Unrecognized string: " & rgbIn.ToString(), Color.Red)
                End Try
            End While
        Catch
        Finally
            ' kill our stream and connection
            If [me].nwStream IsNot Nothing Then
                [me].nwStream.Close()
            End If

            If [me].tcpClient IsNot Nothing Then
                [me].tcpClient.Close()
            End If
        End Try

        'PrintTextSafe("Ending listening", Color.Black)
    End Sub

    Private Sub TCPEchoClient_Close(ByVal sender As Object, ByVal e As EventArgs)

        ' Kill our stream and kill the waiting thread
        If nwStream IsNot Nothing Then
            nwStream.Close()
        End If

        If tcpClient IsNot Nothing Then
            tcpClient.Close()
        End If

    End Sub

    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
        target = value
        Return value
    End Function
    Private Function ByteArrayToString(ByVal ba As Byte()) As String
        Dim hex As String = BitConverter.ToString(ba)
        Return hex.Replace("-", "")
    End Function
   
End Class
