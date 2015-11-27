<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.StripMain = New System.Windows.Forms.ToolStrip
        Me.btnControl = New System.Windows.Forms.ToolStripButton
        Me.btnControlNew = New System.Windows.Forms.ToolStripButton
        Me.btnReceive = New System.Windows.Forms.ToolStripButton
        Me.btnTransmit = New System.Windows.Forms.ToolStripButton
        Me.btnServices = New System.Windows.Forms.ToolStripButton
        Me.btnSettings = New System.Windows.Forms.ToolStripButton
        Me.btnAbout = New System.Windows.Forms.ToolStripButton
        Me.btnExit = New System.Windows.Forms.ToolStripButton
        Me.tmrLoad = New System.Windows.Forms.Timer(Me.components)
        Me.StripMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'StripMain
        '
        Me.StripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnControl, Me.btnControlNew, Me.btnReceive, Me.btnTransmit, Me.btnServices, Me.btnSettings, Me.btnAbout, Me.btnExit})
        Me.StripMain.Location = New System.Drawing.Point(0, 0)
        Me.StripMain.Name = "StripMain"
        Me.StripMain.Size = New System.Drawing.Size(993, 51)
        Me.StripMain.TabIndex = 2
        Me.StripMain.Text = "ToolStrip1"
        '
        'btnControl
        '
        Me.btnControl.AutoSize = False
        Me.btnControl.Image = Global.mimics.My.Resources.Resources.controll_icon
        Me.btnControl.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnControl.Margin = New System.Windows.Forms.Padding(0, 1, 8, 2)
        Me.btnControl.Name = "btnControl"
        Me.btnControl.Size = New System.Drawing.Size(50, 48)
        Me.btnControl.Text = "Monitor"
        Me.btnControl.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnControl.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btnControlNew
        '
        Me.btnControlNew.AutoSize = False
        Me.btnControlNew.Image = CType(resources.GetObject("btnControlNew.Image"), System.Drawing.Image)
        Me.btnControlNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnControlNew.Margin = New System.Windows.Forms.Padding(0, 1, 8, 2)
        Me.btnControlNew.Name = "btnControlNew"
        Me.btnControlNew.Size = New System.Drawing.Size(50, 48)
        Me.btnControlNew.Text = "Control"
        Me.btnControlNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnControlNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btnReceive
        '
        Me.btnReceive.AutoSize = False
        Me.btnReceive.Image = CType(resources.GetObject("btnReceive.Image"), System.Drawing.Image)
        Me.btnReceive.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnReceive.Margin = New System.Windows.Forms.Padding(0, 1, 8, 2)
        Me.btnReceive.Name = "btnReceive"
        Me.btnReceive.Size = New System.Drawing.Size(48, 48)
        Me.btnReceive.Text = "MySQL"
        Me.btnReceive.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnReceive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnReceive.ToolTipText = "Receive data from mimics"
        '
        'btnTransmit
        '
        Me.btnTransmit.AutoSize = False
        Me.btnTransmit.BackColor = System.Drawing.SystemColors.Control
        Me.btnTransmit.Image = CType(resources.GetObject("btnTransmit.Image"), System.Drawing.Image)
        Me.btnTransmit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnTransmit.Margin = New System.Windows.Forms.Padding(0, 1, 8, 2)
        Me.btnTransmit.Name = "btnTransmit"
        Me.btnTransmit.Size = New System.Drawing.Size(48, 48)
        Me.btnTransmit.Text = "Transmit"
        Me.btnTransmit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnTransmit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnTransmit.ToolTipText = "Transmit data to mimics"
        '
        'btnServices
        '
        Me.btnServices.AutoSize = False
        Me.btnServices.Image = CType(resources.GetObject("btnServices.Image"), System.Drawing.Image)
        Me.btnServices.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnServices.Name = "btnServices"
        Me.btnServices.Size = New System.Drawing.Size(53, 48)
        Me.btnServices.Text = "Services"
        Me.btnServices.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btnSettings
        '
        Me.btnSettings.AutoSize = False
        Me.btnSettings.Image = CType(resources.GetObject("btnSettings.Image"), System.Drawing.Image)
        Me.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Size = New System.Drawing.Size(53, 48)
        Me.btnSettings.Text = "Settings"
        Me.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btnAbout
        '
        Me.btnAbout.AutoSize = False
        Me.btnAbout.Image = CType(resources.GetObject("btnAbout.Image"), System.Drawing.Image)
        Me.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(44, 48)
        Me.btnAbout.Text = "About"
        Me.btnAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btnExit
        '
        Me.btnExit.AutoSize = False
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnExit.Margin = New System.Windows.Forms.Padding(0, 1, 8, 2)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 48)
        Me.btnExit.Text = "Exit"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnExit.ToolTipText = "Exit mimics application"
        '
        'tmrLoad
        '
        Me.tmrLoad.Interval = 1500
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(993, 514)
        Me.Controls.Add(Me.StripMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "CMM mimics"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StripMain.ResumeLayout(False)
        Me.StripMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StripMain As System.Windows.Forms.ToolStrip
    Friend WithEvents btnReceive As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnTransmit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnControl As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tmrLoad As System.Windows.Forms.Timer
    Friend WithEvents btnServices As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnAbout As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSettings As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnControlNew As System.Windows.Forms.ToolStripButton

End Class
