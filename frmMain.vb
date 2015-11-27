Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Net
Imports System.Net.Sockets

Public Class frmMain
    Public frmMonitor As New frmMonitor

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            'Commented out because it has been set as a "Single instance application" in project properties
            'If Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1 Then
            ' blnAppMultiple = True
            ' Application.Exit()
            ' End If

            frmMonitor.MdiParent = Me
            StripMain.ImageScalingSize = New System.Drawing.Size(40, 40)
            Me.Text = "CMM Mimics : " & strVersion

            For Each ctl As Control In Me.Controls
                If TypeOf ctl Is MdiClient Then
                    AddHandler ctl.Paint, AddressOf MDIControlPaint
                    AddHandler ctl.SizeChanged, AddressOf MDIControlResize
                    ctl.BackColor = Color.White
                    Exit For
                End If
            Next

            strCustomer = Trim(GetIni("Customer"))
            If Len(strCustomer) = 0 Then strCustomer = "MBSA"

            If (strCustomer = "MBSA") Then
                Me.StripMain.Items(1).Visible = False
            End If

            Me.tmrLoad.Enabled = True

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnReceive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceive.Click

        For Each Form In Me.MdiChildren
            If Form.Name <> "frmMonitor" Then Form.Close()
        Next

        frmMonitor.Visible = False
        frmMonitor.tmrRefresh.Enabled = False

        Dim frm As New frmReceive
        frm.MdiParent = Me
        frm.Show()

        
    End Sub
    Private Sub btnTransmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransmit.Click


        For Each Form In Me.MdiChildren
            If Form.Name <> "frmMonitor" Then Form.Close()
        Next
        frmMonitor.Visible = False
        frmMonitor.tmrRefresh.Enabled = False

        Dim frm As New frmTransmit
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
        Application.Exit()
        Environment.Exit(0)
    End Sub

    Private Sub tsServices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmServices
        frm.ShowDialog()
    End Sub

    Private Sub tmrLoad_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrLoad.Tick
        Try
            Me.tmrLoad.Enabled = False

            'strCustomer = Trim(GetIni("Customer"))
            'If Len(strCustomer) = 0 Then strCustomer = "MBSA"

            'If this is Ian's  development machine then allow Customer setting
            'If (File.Exists("C:\CMM Mimics\zzDevMachine.ini")) Then
            '    Dim frm As New frmDev
            '    frm.ShowDialog()
            'End If

            strCustomer = "MBSA"
            startUp()

            frmMonitor.Visible = True
            frmMonitor.Location = New Point(0, 0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmAbout
        frm.ShowDialog()
    End Sub

    Private Sub btnControl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnControl.Click

        For Each Form In Me.MdiChildren
            If Form.Name <> "frmMonitor" Then Form.Close()
        Next

        frmMonitor.Visible = True
        frmMonitor.Location = New Point(0, 0)
        frmMonitor.tmrRefresh.Enabled = True

    End Sub
    Private Sub MDIControlPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        Try
            Dim MyImage As Image = Global.mimics.My.Resources.Resources.logo_Mimics
            Dim MyTop As Integer = 0
            Dim MyLeft As Integer = 0
            If MyImage.Height < Me.Height Then
                MyTop = Convert.ToInt32((Me.ClientSize.Height - MyImage.Height) / 2)
            End If
            If MyImage.Width < Me.Width Then
                MyLeft = Convert.ToInt32((Me.ClientSize.Width - MyImage.Width) / 2)
            End If
            e.Graphics.DrawImage(MyImage, MyLeft, MyTop)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub MDIControlResize(ByVal sender As Object, ByVal e As System.EventArgs)
        CType(sender, MdiClient).Invalidate()
    End Sub

    Private Sub btnServices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnServices.Click
        Dim frm As New frmServices
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub btnAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbout.Click
        Dim frm As New frmAbout
        frm.ShowDialog()
    End Sub

    Private Sub btnSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettings.Click

        frmMonitor.Visible = False
        Dim frm As New frmSettings
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub btnControlNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnControlNew.Click

        For Each Form In Me.MdiChildren
            If Form.Name = "frmControl" Then Form.Close()
        Next

        Dim frm As New frmControl
        frm.MdiParent = Me
        frm.Show()
    End Sub

   
End Class
