<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServices
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmServices))
        Me.lvwServices = New System.Windows.Forms.ListView
        Me.lchCaptions = New System.Windows.Forms.ColumnHeader
        Me.lchNames = New System.Windows.Forms.ColumnHeader
        Me.lchState = New System.Windows.Forms.ColumnHeader
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.cmdStart = New System.Windows.Forms.Button
        Me.cmdStop = New System.Windows.Forms.Button
        Me.tmrSvc = New System.Windows.Forms.Timer(Me.components)
        Me.btnReset = New System.Windows.Forms.Button
        Me.lblStatus = New System.Windows.Forms.Label
        Me.tmrRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lvwServices
        '
        Me.lvwServices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lchCaptions, Me.lchNames, Me.lchState})
        Me.lvwServices.FullRowSelect = True
        Me.lvwServices.GridLines = True
        Me.lvwServices.Location = New System.Drawing.Point(14, 118)
        Me.lvwServices.Name = "lvwServices"
        Me.lvwServices.Size = New System.Drawing.Size(424, 55)
        Me.lvwServices.TabIndex = 1
        Me.lvwServices.UseCompatibleStateImageBehavior = False
        Me.lvwServices.View = System.Windows.Forms.View.Details
        '
        'lchCaptions
        '
        Me.lchCaptions.Text = "Service"
        Me.lchCaptions.Width = 143
        '
        'lchNames
        '
        Me.lchNames.Text = "Real Names"
        Me.lchNames.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.lchNames.Width = 154
        '
        'lchState
        '
        Me.lchState.Text = "State"
        Me.lchState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.lchState.Width = 121
        '
        'cmdRefresh
        '
        Me.cmdRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdRefresh.Location = New System.Drawing.Point(15, 68)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(84, 24)
        Me.cmdRefresh.TabIndex = 4
        Me.cmdRefresh.Text = "Refresh"
        '
        'cmdStart
        '
        Me.cmdStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdStart.Location = New System.Drawing.Point(354, 68)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.Size = New System.Drawing.Size(84, 24)
        Me.cmdStart.TabIndex = 3
        Me.cmdStart.Text = "Start Service"
        '
        'cmdStop
        '
        Me.cmdStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdStop.Location = New System.Drawing.Point(241, 68)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.Size = New System.Drawing.Size(84, 24)
        Me.cmdStop.TabIndex = 2
        Me.cmdStop.Text = "Stop Service"
        '
        'tmrSvc
        '
        Me.tmrSvc.Enabled = True
        Me.tmrSvc.Interval = 300
        '
        'btnReset
        '
        Me.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnReset.Location = New System.Drawing.Point(128, 68)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(89, 24)
        Me.btnReset.TabIndex = 6
        Me.btnReset.Text = "Restart Service"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(14, 18)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(62, 20)
        Me.lblStatus.TabIndex = 7
        Me.lblStatus.Text = "Status"
        '
        'tmrRefresh
        '
        Me.tmrRefresh.Enabled = True
        Me.tmrRefresh.Interval = 3000
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(148, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "(Disable user account control)"
        '
        'frmServices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 195)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.cmdRefresh)
        Me.Controls.Add(Me.cmdStart)
        Me.Controls.Add(Me.cmdStop)
        Me.Controls.Add(Me.lvwServices)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmServices"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Services"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lvwServices As System.Windows.Forms.ListView
    Friend WithEvents lchCaptions As System.Windows.Forms.ColumnHeader
    Friend WithEvents lchNames As System.Windows.Forms.ColumnHeader
    Friend WithEvents lchState As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents cmdStart As System.Windows.Forms.Button
    Friend WithEvents cmdStop As System.Windows.Forms.Button
    Friend WithEvents tmrSvc As System.Windows.Forms.Timer
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents tmrRefresh As System.Windows.Forms.Timer
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
