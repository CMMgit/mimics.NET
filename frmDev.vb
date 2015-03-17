Public Class frmDev

    Private Sub frmDev_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New System.Drawing.Point(10.5)
        Me.cmbCustomer.Text = strCustomer
        
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        strCustomer = Me.cmbCustomer.Text
        Me.Close()

    End Sub
End Class