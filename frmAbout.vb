Public Class frmAbout

    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim strText As String = "Commercial Micro Maintenance cc" + vbNewLine _
        & "Copyright 2013 - 2015" + vbNewLine _
        & " " + vbNewLine _
        & "www.cmmsa.co.za" + vbNewLine _
        & "support@cmmsa.co.za" + vbNewLine _
        & " " + vbNewLine _
        & "Ian Billing" + vbNewLine _
        & "ibilling@cmmsa.co.za" + vbNewLine _
        & "+27 82 444 7794" + vbNewLine _
        & " " + vbNewLine _
        & strVersion

        'If blnAppMultiple = True Then
        ' strText = "This application is already running." + vbNewLine _
        '& "Multiple app not allowed - exiting application."
        ' End If

        RTB.Text = strText

    End Sub

    Private Sub frmAbout_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

        Dim MyImage As Image = Global.mimics.My.Resources.Resources.imgAbout
        e.Graphics.DrawImage(MyImage, 0, 0)

    End Sub
End Class