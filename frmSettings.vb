Imports System.IO

Public Class frmSettings
    Private Sub frmSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        btnSave = fixButtonImage(btnSave)
        btnExit = fixButtonImage(btnExit)
        ToolStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        readINI()
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub readINI()

        Me.rtBox.Clear()

        Dim strFilename As String = "C:\CMM Mimics\mimics.ini"
        If Not File.Exists(strFilename) Then
            rtBox.Text = "No initialisation file found"
            Exit Sub
        End If

        rtBox.LoadFile(strFilename, RichTextBoxStreamType.PlainText)


    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            Dim n As Integer
            Dim strFilename As String = "C:\CMM Mimics\mimics.ini"
            Dim strBCKfilename As String = "C:\CMM Mimics\" & Now.ToString("yyyyMMddHHmmss") & ".ini"
            If File.Exists(strFilename) Then File.Copy(strFilename, strBCKfilename)
            If File.Exists(strFilename) Then File.Delete(strFilename)
            rtBox.SaveFile(strFilename, RichTextBoxStreamType.PlainText)
            MsgBox("Initialisation file saved.", MsgBoxStyle.Information, "Mimics.ini")
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class