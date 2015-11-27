<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransmit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTransmit))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnTransmit = New System.Windows.Forms.ToolStripButton
        Me.btnExit = New System.Windows.Forms.ToolStripButton
        Me.txtMsg = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtIP = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnAll = New System.Windows.Forms.Button
        Me.btnSend = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.lblMsg = New System.Windows.Forms.Label
        Me.btnEdit = New System.Windows.Forms.Button
        Me.Log = New System.Windows.Forms.RichTextBox
        Me.TabControl = New System.Windows.Forms.TabControl
        Me.Tab1 = New System.Windows.Forms.TabPage
        Me.lstViewINI = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.Tab2 = New System.Windows.Forms.TabPage
        Me.lstViewFunctions = New System.Windows.Forms.ListView
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.Tab3 = New System.Windows.Forms.TabPage
        Me.lstViewOutputs = New System.Windows.Forms.ListView
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader8 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader9 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader10 = New System.Windows.Forms.ColumnHeader
        Me.Tab5 = New System.Windows.Forms.TabPage
        Me.Button4 = New System.Windows.Forms.Button
        Me.logCopy = New System.Windows.Forms.RichTextBox
        Me.pnlWait = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblCount = New System.Windows.Forms.Label
        Me.pBar1 = New System.Windows.Forms.ProgressBar
        Me.btnClearRTB = New System.Windows.Forms.Button
        Me.btnImport = New System.Windows.Forms.Button
        Me.btnSet = New System.Windows.Forms.Button
        Me.btnGet = New System.Windows.Forms.Button
        Me.rtbMimicsINI = New System.Windows.Forms.RichTextBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.btnNow = New System.Windows.Forms.Button
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.Label4 = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        Me.TabControl.SuspendLayout()
        Me.Tab1.SuspendLayout()
        Me.Tab2.SuspendLayout()
        Me.Tab3.SuspendLayout()
        Me.Tab5.SuspendLayout()
        Me.pnlWait.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnTransmit, Me.btnExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1037, 51)
        Me.ToolStrip1.TabIndex = 4
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnTransmit
        '
        Me.btnTransmit.AutoSize = False
        Me.btnTransmit.Image = CType(resources.GetObject("btnTransmit.Image"), System.Drawing.Image)
        Me.btnTransmit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnTransmit.Margin = New System.Windows.Forms.Padding(0, 1, 8, 2)
        Me.btnTransmit.Name = "btnTransmit"
        Me.btnTransmit.Size = New System.Drawing.Size(48, 48)
        Me.btnTransmit.Text = "Transmit"
        Me.btnTransmit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnTransmit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnTransmit.ToolTipText = "Transmit message to mimics"
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
        Me.btnExit.ToolTipText = "Close transmit form"
        '
        'txtMsg
        '
        Me.txtMsg.Location = New System.Drawing.Point(15, 132)
        Me.txtMsg.Name = "txtMsg"
        Me.txtMsg.Size = New System.Drawing.Size(305, 20)
        Me.txtMsg.TabIndex = 5
        Me.txtMsg.Text = "xBTN:1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 116)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(203, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Enter instruction below and click Transmit"
        '
        'txtIP
        '
        Me.txtIP.Location = New System.Drawing.Point(15, 73)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.Size = New System.Drawing.Size(136, 20)
        Me.txtIP.TabIndex = 7
        Me.txtIP.Text = "192.168.0."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(139, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Enter destination IP address"
        '
        'btnAll
        '
        Me.btnAll.Location = New System.Drawing.Point(158, 69)
        Me.btnAll.Name = "btnAll"
        Me.btnAll.Size = New System.Drawing.Size(57, 23)
        Me.btnAll.TabIndex = 9
        Me.btnAll.Text = "All"
        Me.btnAll.UseVisualStyleBackColor = True
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(263, 555)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(57, 23)
        Me.btnSend.TabIndex = 10
        Me.btnSend.Text = "Transmit"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(15, 555)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(57, 23)
        Me.btnClear.TabIndex = 11
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'lblMsg
        '
        Me.lblMsg.AutoSize = True
        Me.lblMsg.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.lblMsg.Location = New System.Drawing.Point(12, 158)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(128, 13)
        Me.lblMsg.TabIndex = 12
        Me.lblMsg.Text = "xMessage sent  to Mimics"
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(83, 555)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(57, 23)
        Me.btnEdit.TabIndex = 15
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'Log
        '
        Me.Log.Location = New System.Drawing.Point(0, 179)
        Me.Log.Name = "Log"
        Me.Log.Size = New System.Drawing.Size(320, 370)
        Me.Log.TabIndex = 19
        Me.Log.Text = ""
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.Tab1)
        Me.TabControl.Controls.Add(Me.Tab2)
        Me.TabControl.Controls.Add(Me.Tab3)
        Me.TabControl.Controls.Add(Me.Tab5)
        Me.TabControl.Location = New System.Drawing.Point(326, 124)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(704, 425)
        Me.TabControl.TabIndex = 20
        '
        'Tab1
        '
        Me.Tab1.Controls.Add(Me.lstViewINI)
        Me.Tab1.Location = New System.Drawing.Point(4, 22)
        Me.Tab1.Name = "Tab1"
        Me.Tab1.Padding = New System.Windows.Forms.Padding(3)
        Me.Tab1.Size = New System.Drawing.Size(696, 399)
        Me.Tab1.TabIndex = 0
        Me.Tab1.Text = "ini Settings"
        Me.Tab1.UseVisualStyleBackColor = True
        '
        'lstViewINI
        '
        Me.lstViewINI.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstViewINI.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lstViewINI.FullRowSelect = True
        Me.lstViewINI.GridLines = True
        Me.lstViewINI.Location = New System.Drawing.Point(3, 3)
        Me.lstViewINI.Name = "lstViewINI"
        Me.lstViewINI.Scrollable = False
        Me.lstViewINI.Size = New System.Drawing.Size(690, 470)
        Me.lstViewINI.TabIndex = 14
        Me.lstViewINI.UseCompatibleStateImageBehavior = False
        Me.lstViewINI.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Description"
        Me.ColumnHeader1.Width = 174
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Instruction"
        Me.ColumnHeader2.Width = 141
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Values"
        Me.ColumnHeader3.Width = 176
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Example"
        Me.ColumnHeader4.Width = 325
        '
        'Tab2
        '
        Me.Tab2.BackColor = System.Drawing.Color.RoyalBlue
        Me.Tab2.BackgroundImage = Global.mimics.My.Resources.Resources.icon_Graphic
        Me.Tab2.Controls.Add(Me.lstViewFunctions)
        Me.Tab2.Location = New System.Drawing.Point(4, 22)
        Me.Tab2.Name = "Tab2"
        Me.Tab2.Padding = New System.Windows.Forms.Padding(3)
        Me.Tab2.Size = New System.Drawing.Size(696, 399)
        Me.Tab2.TabIndex = 1
        Me.Tab2.Text = "Functions"
        Me.Tab2.UseVisualStyleBackColor = True
        '
        'lstViewFunctions
        '
        Me.lstViewFunctions.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstViewFunctions.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader5, Me.ColumnHeader6})
        Me.lstViewFunctions.FullRowSelect = True
        Me.lstViewFunctions.GridLines = True
        Me.lstViewFunctions.Location = New System.Drawing.Point(6, 3)
        Me.lstViewFunctions.Name = "lstViewFunctions"
        Me.lstViewFunctions.Scrollable = False
        Me.lstViewFunctions.Size = New System.Drawing.Size(409, 387)
        Me.lstViewFunctions.TabIndex = 19
        Me.lstViewFunctions.UseCompatibleStateImageBehavior = False
        Me.lstViewFunctions.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Description"
        Me.ColumnHeader5.Width = 224
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Instruction"
        Me.ColumnHeader6.Width = 178
        '
        'Tab3
        '
        Me.Tab3.Controls.Add(Me.lstViewOutputs)
        Me.Tab3.Location = New System.Drawing.Point(4, 22)
        Me.Tab3.Name = "Tab3"
        Me.Tab3.Size = New System.Drawing.Size(696, 399)
        Me.Tab3.TabIndex = 2
        Me.Tab3.Text = "I / O"
        Me.Tab3.UseVisualStyleBackColor = True
        '
        'lstViewOutputs
        '
        Me.lstViewOutputs.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstViewOutputs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10})
        Me.lstViewOutputs.FullRowSelect = True
        Me.lstViewOutputs.GridLines = True
        Me.lstViewOutputs.Location = New System.Drawing.Point(2, 3)
        Me.lstViewOutputs.Name = "lstViewOutputs"
        Me.lstViewOutputs.Scrollable = False
        Me.lstViewOutputs.Size = New System.Drawing.Size(691, 393)
        Me.lstViewOutputs.TabIndex = 15
        Me.lstViewOutputs.UseCompatibleStateImageBehavior = False
        Me.lstViewOutputs.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Description"
        Me.ColumnHeader7.Width = 224
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Instruction"
        Me.ColumnHeader8.Width = 121
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Values"
        Me.ColumnHeader9.Width = 176
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Example"
        Me.ColumnHeader10.Width = 325
        '
        'Tab5
        '
        Me.Tab5.Controls.Add(Me.Button4)
        Me.Tab5.Controls.Add(Me.logCopy)
        Me.Tab5.Controls.Add(Me.pnlWait)
        Me.Tab5.Controls.Add(Me.btnClearRTB)
        Me.Tab5.Controls.Add(Me.btnImport)
        Me.Tab5.Controls.Add(Me.btnSet)
        Me.Tab5.Controls.Add(Me.btnGet)
        Me.Tab5.Controls.Add(Me.rtbMimicsINI)
        Me.Tab5.Location = New System.Drawing.Point(4, 22)
        Me.Tab5.Name = "Tab5"
        Me.Tab5.Size = New System.Drawing.Size(696, 399)
        Me.Tab5.TabIndex = 4
        Me.Tab5.Text = "eePROM"
        Me.Tab5.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.Button4.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Location = New System.Drawing.Point(655, 220)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(20, 22)
        Me.Button4.TabIndex = 24
        Me.Button4.UseVisualStyleBackColor = True
        '
        'logCopy
        '
        Me.logCopy.Location = New System.Drawing.Point(309, 215)
        Me.logCopy.Name = "logCopy"
        Me.logCopy.Size = New System.Drawing.Size(366, 175)
        Me.logCopy.TabIndex = 25
        Me.logCopy.Text = ""
        Me.logCopy.Visible = False
        '
        'pnlWait
        '
        Me.pnlWait.BackgroundImage = Global.mimics.My.Resources.Resources.CmmMimicsLogo
        Me.pnlWait.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlWait.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlWait.Controls.Add(Me.Label3)
        Me.pnlWait.Controls.Add(Me.lblCount)
        Me.pnlWait.Controls.Add(Me.pBar1)
        Me.pnlWait.Location = New System.Drawing.Point(304, 13)
        Me.pnlWait.Name = "pnlWait"
        Me.pnlWait.Size = New System.Drawing.Size(306, 180)
        Me.pnlWait.TabIndex = 16
        Me.pnlWait.UseWaitCursor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label3.Location = New System.Drawing.Point(152, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(130, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Writing to EEPROM..."
        Me.Label3.UseWaitCursor = True
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCount.ForeColor = System.Drawing.Color.Red
        Me.lblCount.Location = New System.Drawing.Point(193, 41)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(0, 36)
        Me.lblCount.TabIndex = 1
        Me.lblCount.UseWaitCursor = True
        '
        'pBar1
        '
        Me.pBar1.Location = New System.Drawing.Point(3, 155)
        Me.pBar1.Name = "pBar1"
        Me.pBar1.Size = New System.Drawing.Size(296, 23)
        Me.pBar1.TabIndex = 0
        Me.pBar1.UseWaitCursor = True
        '
        'btnClearRTB
        '
        Me.btnClearRTB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearRTB.Image = CType(resources.GetObject("btnClearRTB.Image"), System.Drawing.Image)
        Me.btnClearRTB.Location = New System.Drawing.Point(616, 164)
        Me.btnClearRTB.Name = "btnClearRTB"
        Me.btnClearRTB.Size = New System.Drawing.Size(59, 50)
        Me.btnClearRTB.TabIndex = 4
        Me.btnClearRTB.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImport.Image = CType(resources.GetObject("btnImport.Image"), System.Drawing.Image)
        Me.btnImport.Location = New System.Drawing.Point(616, 107)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(59, 54)
        Me.btnImport.TabIndex = 3
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'btnSet
        '
        Me.btnSet.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSet.Image = CType(resources.GetObject("btnSet.Image"), System.Drawing.Image)
        Me.btnSet.Location = New System.Drawing.Point(616, 60)
        Me.btnSet.Name = "btnSet"
        Me.btnSet.Size = New System.Drawing.Size(59, 44)
        Me.btnSet.TabIndex = 2
        Me.btnSet.Text = "Tx   "
        Me.btnSet.UseVisualStyleBackColor = True
        '
        'btnGet
        '
        Me.btnGet.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGet.Image = CType(resources.GetObject("btnGet.Image"), System.Drawing.Image)
        Me.btnGet.Location = New System.Drawing.Point(616, 13)
        Me.btnGet.Name = "btnGet"
        Me.btnGet.Size = New System.Drawing.Size(59, 44)
        Me.btnGet.TabIndex = 1
        Me.btnGet.Text = "Rx   "
        Me.btnGet.UseVisualStyleBackColor = True
        '
        'rtbMimicsINI
        '
        Me.rtbMimicsINI.BackColor = System.Drawing.Color.LightYellow
        Me.rtbMimicsINI.Location = New System.Drawing.Point(0, 9)
        Me.rtbMimicsINI.Name = "rtbMimicsINI"
        Me.rtbMimicsINI.Size = New System.Drawing.Size(693, 387)
        Me.rtbMimicsINI.TabIndex = 0
        Me.rtbMimicsINI.Text = ""
        '
        'Timer1
        '
        '
        'Timer2
        '
        '
        'btnNow
        '
        Me.btnNow.Location = New System.Drawing.Point(587, 85)
        Me.btnNow.Name = "btnNow"
        Me.btnNow.Size = New System.Drawing.Size(102, 23)
        Me.btnNow.TabIndex = 21
        Me.btnNow.Text = "Set new date time"
        Me.btnNow.UseVisualStyleBackColor = True
        Me.btnNow.Visible = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(330, 88)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(241, 20)
        Me.DateTimePicker1.TabIndex = 22
        Me.DateTimePicker1.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(323, 67)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(248, 18)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Reset Hadeco Cycle Start Date:"
        Me.Label4.Visible = False
        '
        'frmTransmit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1037, 590)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.btnNow)
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.Log)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.btnAll)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtIP)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtMsg)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTransmit"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Transmit mimics"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.TabControl.ResumeLayout(False)
        Me.Tab1.ResumeLayout(False)
        Me.Tab2.ResumeLayout(False)
        Me.Tab3.ResumeLayout(False)
        Me.Tab5.ResumeLayout(False)
        Me.pnlWait.ResumeLayout(False)
        Me.pnlWait.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnTransmit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtMsg As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtIP As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnAll As System.Windows.Forms.Button
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents Log As System.Windows.Forms.RichTextBox
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents Tab1 As System.Windows.Forms.TabPage
    Friend WithEvents Tab2 As System.Windows.Forms.TabPage
    Friend WithEvents lstViewINI As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstViewFunctions As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Tab3 As System.Windows.Forms.TabPage
    Friend WithEvents lstViewOutputs As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Tab5 As System.Windows.Forms.TabPage
    Friend WithEvents rtbMimicsINI As System.Windows.Forms.RichTextBox
    Friend WithEvents btnSet As System.Windows.Forms.Button
    Friend WithEvents btnGet As System.Windows.Forms.Button
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents btnClearRTB As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents pnlWait As System.Windows.Forms.Panel
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents pBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents logCopy As System.Windows.Forms.RichTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnNow As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
