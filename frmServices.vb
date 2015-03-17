Imports System.Windows.Forms
Imports System.IO
Imports System.Text
Imports System.ServiceProcess
Imports System.Security.Principal

Public Class frmServices
    Private strServiceName As String = "mimicsUDP.Service"
    Private Sub frmServices_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ReadSvcs()
    End Sub
    Sub ReadSvcs()

        Dim ListSvcs() As ServiceProcess.ServiceController
        Dim SingleSvc As ServiceProcess.ServiceController
        Dim LVW As ListViewItem

        ListSvcs = SingleSvc.GetServices

        lvwServices.Items.Clear()
        Try
            For Each SingleSvc In ListSvcs
                If (SingleSvc.ServiceName = strServiceName) Then
                    If (SingleSvc.Status.ToString.IndexOf("Running") > -1) Then
                        lblStatus.Text = "Mimics UDP Service running"
                        lblStatus.ForeColor = Color.Green
                        Me.cmdStop.Enabled = True
                        Me.cmdStart.Enabled = False
                    ElseIf (SingleSvc.Status.ToString.IndexOf("Stopped") > -1) Then
                        lblStatus.Text = "Mimics UDP Service stopped"
                        lblStatus.ForeColor = Color.Red
                        Me.cmdStop.Enabled = False
                        Me.cmdStart.Enabled = True
                    ElseIf (SingleSvc.Status.ToString.IndexOf("Start") > -1) Then
                        lblStatus.Text = "Mimics UDP Service starting"
                        lblStatus.ForeColor = Color.Green
                        Me.cmdStop.Enabled = True
                        Me.cmdStart.Enabled = False
                    Else
                        lblStatus.Text = Nothing
                        lblStatus.ForeColor = Color.Black
                    End If
                    LVW = lvwServices.Items.Add(SingleSvc.DisplayName)
                    LVW.SubItems.Add(SingleSvc.ServiceName)
                    LVW.SubItems.Add(SingleSvc.Status.ToString)
                    LVW.SubItems.Add(SingleSvc.ServiceType.ToString)
                End If
            Next
        Catch e As Exception
            MessageBox.Show("Could not initialize Windows Service engine.  Restarting your computer may work", "Fatal Error: " & e.Source)
        End Try
    End Sub

    Private Sub lvwServices_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If lvwServices.SelectedItems(0).Text <> "" Then
            Select Case lvwServices.SelectedItems(0).SubItems(2).Text
                Case "Stopped"
                    cmdStop.Enabled = False
                    cmdStart.Enabled = True

                Case "Running"
                    cmdStop.Enabled = True
                    cmdStart.Enabled = False

                Case Else
                    cmdStop.Enabled = False
                    cmdStart.Enabled = False
            End Select
        End If
    End Sub

    Private Sub cmdStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If lvwServices.SelectedItems(0).Text <> "" Then
            'Call StopService(lvwServices.SelectedItems(0).SubItems(1).Text)
            Call StopService()
        End If
    End Sub

    Sub StopService()

        Try
            Dim service As ServiceController = New ServiceController(strServiceName)

            If ((service.Status.Equals(ServiceControllerStatus.Stopped)) Or _
                (service.Status.Equals(ServiceControllerStatus.StopPending))) Then
                MsgBox("mimics service already stopped.", MsgBoxStyle.Exclamation, "Stop service")
            Else
                service.Stop()
            End If

        Catch e As Exception
            MessageBox.Show("Could not stop service.  Ensure it is not disabled. User account control?", "Fatal Error: " & e.Source)
        End Try
    End Sub

    Sub StartService()

        Try
            Dim service As ServiceController = New ServiceController(strServiceName)

            If ((service.Status.Equals(ServiceControllerStatus.Running)) Or _
                (service.Status.Equals(ServiceControllerStatus.StartPending))) Then
                MsgBox("mimics service already started.", MsgBoxStyle.Exclamation, "Start service")
            Else
                service.Start()
            End If

        Catch e As Exception
            MessageBox.Show("Could not start service.  Ensure it is not disabled. User account control?", "Fatal Error: " & e.Source)
        End Try
    End Sub

    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If lvwServices.SelectedItems(0).Text <> "" Then
            'Call StartService(lvwServices.SelectedItems(0).SubItems(1).Text)
            Call StartService()
        End If
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call ReadSvcs()
    End Sub

    Private Sub cmdStop_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStop.Click
        Call StopService()
        Call ReadSvcs()
    End Sub

   
    Private Sub cmdStart_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
        Call StartService()
        Call ReadSvcs()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Call StopService()
        Delay(2)
        Call StartService()
        Call ReadSvcs()
        System.Windows.Forms.Cursor.Current = Cursors.Default

    End Sub

    Private Sub cmdRefresh_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        ReadSvcs()
    End Sub

    Private Sub tmrRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrRefresh.Tick
        ReadSvcs()
    End Sub
  
End Class