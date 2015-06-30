<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmControl))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnTransmit = New System.Windows.Forms.ToolStripButton
        Me.btnExit = New System.Windows.Forms.ToolStripButton
        Me.cmbIP = New System.Windows.Forms.ComboBox
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnStatus = New System.Windows.Forms.Button
        Me.pnl0 = New System.Windows.Forms.Panel
        Me.BTN4 = New System.Windows.Forms.Button
        Me.BTN3 = New System.Windows.Forms.Button
        Me.BTN2 = New System.Windows.Forms.Button
        Me.BTN1 = New System.Windows.Forms.Button
        Me.tmrRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.pnlError = New System.Windows.Forms.Panel
        Me.Button6 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.lblError = New System.Windows.Forms.Label
        Me.rtbError = New System.Windows.Forms.RichTextBox
        Me.tmrActionSQL = New System.Windows.Forms.Timer(Me.components)
        Me.lblDateTimeStamp = New System.Windows.Forms.Label
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.pnl1 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.lblAutoMan_3 = New System.Windows.Forms.Label
        Me.lblAutoMan_2 = New System.Windows.Forms.Label
        Me.lblAutoMan_1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblVal_2 = New System.Windows.Forms.Label
        Me.lblVal_3 = New System.Windows.Forms.Label
        Me.lblVal_1 = New System.Windows.Forms.Label
        Me.btnAutoStep_3 = New System.Windows.Forms.Button
        Me.btnAutoStep_2 = New System.Windows.Forms.Button
        Me.txtStep_3 = New System.Windows.Forms.TextBox
        Me.txtStep_2 = New System.Windows.Forms.TextBox
        Me.TrackBar3 = New System.Windows.Forms.TrackBar
        Me.TrackBar2 = New System.Windows.Forms.TrackBar
        Me.btnAutoStep_1 = New System.Windows.Forms.Button
        Me.txtStep_1 = New System.Windows.Forms.TextBox
        Me.TrackBar1 = New System.Windows.Forms.TrackBar
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblHeatAuto = New System.Windows.Forms.Label
        Me.chkInblaas = New System.Windows.Forms.CheckBox
        Me.btnAutoHeat = New System.Windows.Forms.Button
        Me.chkElement = New System.Windows.Forms.CheckBox
        Me.chkBoiler = New System.Windows.Forms.CheckBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnElement = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblFansAuto = New System.Windows.Forms.Label
        Me.btnAutoFans = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.optFanSpeed_4 = New System.Windows.Forms.RadioButton
        Me.optFanSpeed_3 = New System.Windows.Forms.RadioButton
        Me.optFanSpeed_2 = New System.Windows.Forms.RadioButton
        Me.optFanSpeed_1 = New System.Windows.Forms.RadioButton
        Me.Button3 = New System.Windows.Forms.Button
        Me.pnlLog = New System.Windows.Forms.Panel
        Me.chkClearLog = New System.Windows.Forms.CheckBox
        Me.Button4 = New System.Windows.Forms.Button
        Me.rtbLog = New System.Windows.Forms.RichTextBox
        Me.btnErrorReset = New System.Windows.Forms.Button
        Me.TabControl = New System.Windows.Forms.TabControl
        Me.lblHadecoCycleStart = New System.Windows.Forms.Label
        Me.tmrDlayTCP = New System.Windows.Forms.Timer(Me.components)
        Me.ToolStrip1.SuspendLayout()
        Me.pnl0.SuspendLayout()
        Me.pnlError.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.pnl1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.TrackBar3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlLog.SuspendLayout()
        Me.TabControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnTransmit, Me.btnExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(327, 51)
        Me.ToolStrip1.TabIndex = 5
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
        Me.btnTransmit.Text = "Reset"
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
        'cmbIP
        '
        Me.cmbIP.FormattingEnabled = True
        Me.cmbIP.Location = New System.Drawing.Point(0, 54)
        Me.cmbIP.Name = "cmbIP"
        Me.cmbIP.Size = New System.Drawing.Size(162, 21)
        Me.cmbIP.TabIndex = 6
        '
        'btnRefresh
        '
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefresh.Location = New System.Drawing.Point(168, 54)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(39, 37)
        Me.btnRefresh.TabIndex = 109
        Me.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "delete.png")
        Me.ImageList1.Images.SetKeyName(1, "check.png")
        Me.ImageList1.Images.SetKeyName(2, "lightbulb_on.png")
        Me.ImageList1.Images.SetKeyName(3, "btn_img.ico")
        Me.ImageList1.Images.SetKeyName(4, "Check_In.jpg")
        Me.ImageList1.Images.SetKeyName(5, "check in_II.jpg")
        Me.ImageList1.Images.SetKeyName(6, "icon_Graphic.ico")
        Me.ImageList1.Images.SetKeyName(7, "check_in.png")
        '
        'btnStatus
        '
        Me.btnStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnStatus.ImageIndex = 1
        Me.btnStatus.ImageList = Me.ImageList1
        Me.btnStatus.Location = New System.Drawing.Point(216, 55)
        Me.btnStatus.Name = "btnStatus"
        Me.btnStatus.Size = New System.Drawing.Size(30, 36)
        Me.btnStatus.TabIndex = 111
        Me.btnStatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnStatus.UseVisualStyleBackColor = True
        '
        'pnl0
        '
        Me.pnl0.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnl0.BackColor = System.Drawing.Color.Transparent
        Me.pnl0.Controls.Add(Me.BTN4)
        Me.pnl0.Controls.Add(Me.BTN3)
        Me.pnl0.Controls.Add(Me.BTN2)
        Me.pnl0.Controls.Add(Me.BTN1)
        Me.pnl0.Location = New System.Drawing.Point(1, 104)
        Me.pnl0.Name = "pnl0"
        Me.pnl0.Size = New System.Drawing.Size(326, 30)
        Me.pnl0.TabIndex = 113
        '
        'BTN4
        '
        Me.BTN4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN4.ImageList = Me.ImageList1
        Me.BTN4.Location = New System.Drawing.Point(268, 3)
        Me.BTN4.Name = "BTN4"
        Me.BTN4.Size = New System.Drawing.Size(29, 23)
        Me.BTN4.TabIndex = 29
        Me.BTN4.UseVisualStyleBackColor = False
        '
        'BTN3
        '
        Me.BTN3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN3.ImageList = Me.ImageList1
        Me.BTN3.Location = New System.Drawing.Point(183, 3)
        Me.BTN3.Name = "BTN3"
        Me.BTN3.Size = New System.Drawing.Size(29, 23)
        Me.BTN3.TabIndex = 28
        Me.BTN3.UseVisualStyleBackColor = False
        '
        'BTN2
        '
        Me.BTN2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN2.ImageList = Me.ImageList1
        Me.BTN2.Location = New System.Drawing.Point(98, 3)
        Me.BTN2.Name = "BTN2"
        Me.BTN2.Size = New System.Drawing.Size(29, 23)
        Me.BTN2.TabIndex = 27
        Me.BTN2.UseVisualStyleBackColor = False
        '
        'BTN1
        '
        Me.BTN1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN1.ImageList = Me.ImageList1
        Me.BTN1.Location = New System.Drawing.Point(13, 3)
        Me.BTN1.Name = "BTN1"
        Me.BTN1.Size = New System.Drawing.Size(29, 23)
        Me.BTN1.TabIndex = 26
        Me.BTN1.UseVisualStyleBackColor = False
        '
        'tmrRefresh
        '
        Me.tmrRefresh.Enabled = True
        Me.tmrRefresh.Interval = 10000
        '
        'pnlError
        '
        Me.pnlError.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlError.Controls.Add(Me.Button6)
        Me.pnlError.Controls.Add(Me.Button5)
        Me.pnlError.Controls.Add(Me.lblError)
        Me.pnlError.Controls.Add(Me.rtbError)
        Me.pnlError.Location = New System.Drawing.Point(1, 296)
        Me.pnlError.Name = "pnlError"
        Me.pnlError.Size = New System.Drawing.Size(308, 117)
        Me.pnlError.TabIndex = 114
        Me.pnlError.Visible = False
        '
        'Button6
        '
        Me.Button6.BackgroundImage = Global.mimics.My.Resources.Resources.btnExit
        Me.Button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button6.Location = New System.Drawing.Point(282, -2)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(24, 23)
        Me.Button6.TabIndex = 4
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.BackgroundImage = Global.mimics.My.Resources.Resources.btnExit
        Me.Button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button5.Location = New System.Drawing.Point(851, -2)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(24, 23)
        Me.Button5.TabIndex = 2
        Me.Button5.UseVisualStyleBackColor = True
        '
        'lblError
        '
        Me.lblError.AutoSize = True
        Me.lblError.Location = New System.Drawing.Point(3, 3)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(37, 13)
        Me.lblError.TabIndex = 1
        Me.lblError.Text = "Errors:"
        '
        'rtbError
        '
        Me.rtbError.Location = New System.Drawing.Point(1, 20)
        Me.rtbError.Name = "rtbError"
        Me.rtbError.Size = New System.Drawing.Size(297, 92)
        Me.rtbError.TabIndex = 0
        Me.rtbError.Text = ""
        '
        'tmrActionSQL
        '
        Me.tmrActionSQL.Enabled = True
        '
        'lblDateTimeStamp
        '
        Me.lblDateTimeStamp.AutoSize = True
        Me.lblDateTimeStamp.Location = New System.Drawing.Point(49, 140)
        Me.lblDateTimeStamp.Name = "lblDateTimeStamp"
        Me.lblDateTimeStamp.Size = New System.Drawing.Size(83, 13)
        Me.lblDateTimeStamp.TabIndex = 1
        Me.lblDateTimeStamp.Text = "DateTimeStamp"
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.LightBlue
        Me.TabPage1.Controls.Add(Me.pnl1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(323, 425)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Data"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'pnl1
        '
        Me.pnl1.Controls.Add(Me.Panel3)
        Me.pnl1.Controls.Add(Me.Panel2)
        Me.pnl1.Controls.Add(Me.Panel1)
        Me.pnl1.Controls.Add(Me.pnlError)
        Me.pnl1.Controls.Add(Me.pnlLog)
        Me.pnl1.Location = New System.Drawing.Point(2, 6)
        Me.pnl1.Name = "pnl1"
        Me.pnl1.Size = New System.Drawing.Size(315, 422)
        Me.pnl1.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Azure
        Me.Panel3.Controls.Add(Me.lblAutoMan_3)
        Me.Panel3.Controls.Add(Me.lblAutoMan_2)
        Me.Panel3.Controls.Add(Me.lblAutoMan_1)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.lblVal_2)
        Me.Panel3.Controls.Add(Me.lblVal_3)
        Me.Panel3.Controls.Add(Me.lblVal_1)
        Me.Panel3.Controls.Add(Me.btnAutoStep_3)
        Me.Panel3.Controls.Add(Me.btnAutoStep_2)
        Me.Panel3.Controls.Add(Me.txtStep_3)
        Me.Panel3.Controls.Add(Me.txtStep_2)
        Me.Panel3.Controls.Add(Me.TrackBar3)
        Me.Panel3.Controls.Add(Me.TrackBar2)
        Me.Panel3.Controls.Add(Me.btnAutoStep_1)
        Me.Panel3.Controls.Add(Me.txtStep_1)
        Me.Panel3.Controls.Add(Me.TrackBar1)
        Me.Panel3.Location = New System.Drawing.Point(4, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(306, 135)
        Me.Panel3.TabIndex = 55
        '
        'lblAutoMan_3
        '
        Me.lblAutoMan_3.AutoSize = True
        Me.lblAutoMan_3.Location = New System.Drawing.Point(235, 104)
        Me.lblAutoMan_3.Name = "lblAutoMan_3"
        Me.lblAutoMan_3.Size = New System.Drawing.Size(29, 13)
        Me.lblAutoMan_3.TabIndex = 64
        Me.lblAutoMan_3.Text = "Auto"
        '
        'lblAutoMan_2
        '
        Me.lblAutoMan_2.AutoSize = True
        Me.lblAutoMan_2.Location = New System.Drawing.Point(235, 69)
        Me.lblAutoMan_2.Name = "lblAutoMan_2"
        Me.lblAutoMan_2.Size = New System.Drawing.Size(29, 13)
        Me.lblAutoMan_2.TabIndex = 63
        Me.lblAutoMan_2.Text = "Auto"
        '
        'lblAutoMan_1
        '
        Me.lblAutoMan_1.AutoSize = True
        Me.lblAutoMan_1.Location = New System.Drawing.Point(235, 33)
        Me.lblAutoMan_1.Name = "lblAutoMan_1"
        Me.lblAutoMan_1.Size = New System.Drawing.Size(29, 13)
        Me.lblAutoMan_1.TabIndex = 62
        Me.lblAutoMan_1.Text = "Auto"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 99)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 13)
        Me.Label5.TabIndex = 61
        Me.Label5.Text = "Solar"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 13)
        Me.Label4.TabIndex = 60
        Me.Label4.Text = "Binne"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 59
        Me.Label3.Text = "Buiten"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 58
        Me.Label2.Text = "Vents"
        '
        'lblVal_2
        '
        Me.lblVal_2.AutoSize = True
        Me.lblVal_2.Location = New System.Drawing.Point(132, 128)
        Me.lblVal_2.Name = "lblVal_2"
        Me.lblVal_2.Size = New System.Drawing.Size(34, 13)
        Me.lblVal_2.TabIndex = 57
        Me.lblVal_2.Text = "Val_2"
        Me.lblVal_2.Visible = False
        '
        'lblVal_3
        '
        Me.lblVal_3.AutoSize = True
        Me.lblVal_3.Location = New System.Drawing.Point(191, 128)
        Me.lblVal_3.Name = "lblVal_3"
        Me.lblVal_3.Size = New System.Drawing.Size(34, 13)
        Me.lblVal_3.TabIndex = 56
        Me.lblVal_3.Text = "Val_3"
        Me.lblVal_3.Visible = False
        '
        'lblVal_1
        '
        Me.lblVal_1.AutoSize = True
        Me.lblVal_1.Location = New System.Drawing.Point(75, 128)
        Me.lblVal_1.Name = "lblVal_1"
        Me.lblVal_1.Size = New System.Drawing.Size(34, 13)
        Me.lblVal_1.TabIndex = 55
        Me.lblVal_1.Text = "Val_1"
        Me.lblVal_1.Visible = False
        '
        'btnAutoStep_3
        '
        Me.btnAutoStep_3.Location = New System.Drawing.Point(265, 101)
        Me.btnAutoStep_3.Name = "btnAutoStep_3"
        Me.btnAutoStep_3.Size = New System.Drawing.Size(37, 20)
        Me.btnAutoStep_3.TabIndex = 54
        Me.btnAutoStep_3.Text = "Auto"
        Me.btnAutoStep_3.UseVisualStyleBackColor = True
        Me.btnAutoStep_3.Visible = False
        '
        'btnAutoStep_2
        '
        Me.btnAutoStep_2.Location = New System.Drawing.Point(265, 66)
        Me.btnAutoStep_2.Name = "btnAutoStep_2"
        Me.btnAutoStep_2.Size = New System.Drawing.Size(37, 20)
        Me.btnAutoStep_2.TabIndex = 53
        Me.btnAutoStep_2.Text = "Auto"
        Me.btnAutoStep_2.UseVisualStyleBackColor = True
        Me.btnAutoStep_2.Visible = False
        '
        'txtStep_3
        '
        Me.txtStep_3.Location = New System.Drawing.Point(195, 101)
        Me.txtStep_3.Name = "txtStep_3"
        Me.txtStep_3.Size = New System.Drawing.Size(36, 20)
        Me.txtStep_3.TabIndex = 52
        Me.txtStep_3.Text = "--%"
        '
        'txtStep_2
        '
        Me.txtStep_2.Location = New System.Drawing.Point(195, 66)
        Me.txtStep_2.Name = "txtStep_2"
        Me.txtStep_2.Size = New System.Drawing.Size(36, 20)
        Me.txtStep_2.TabIndex = 51
        Me.txtStep_2.Text = "--%"
        '
        'TrackBar3
        '
        Me.TrackBar3.LargeChange = 0
        Me.TrackBar3.Location = New System.Drawing.Point(40, 99)
        Me.TrackBar3.Maximum = 100
        Me.TrackBar3.Name = "TrackBar3"
        Me.TrackBar3.Size = New System.Drawing.Size(155, 45)
        Me.TrackBar3.SmallChange = 0
        Me.TrackBar3.TabIndex = 50
        Me.TrackBar3.TickFrequency = 100
        Me.TrackBar3.TickStyle = System.Windows.Forms.TickStyle.None
        Me.TrackBar3.Value = 50
        '
        'TrackBar2
        '
        Me.TrackBar2.LargeChange = 0
        Me.TrackBar2.Location = New System.Drawing.Point(40, 62)
        Me.TrackBar2.Maximum = 100
        Me.TrackBar2.Name = "TrackBar2"
        Me.TrackBar2.Size = New System.Drawing.Size(155, 45)
        Me.TrackBar2.SmallChange = 0
        Me.TrackBar2.TabIndex = 49
        Me.TrackBar2.TickFrequency = 100
        Me.TrackBar2.TickStyle = System.Windows.Forms.TickStyle.None
        Me.TrackBar2.Value = 50
        '
        'btnAutoStep_1
        '
        Me.btnAutoStep_1.Location = New System.Drawing.Point(265, 30)
        Me.btnAutoStep_1.Name = "btnAutoStep_1"
        Me.btnAutoStep_1.Size = New System.Drawing.Size(37, 20)
        Me.btnAutoStep_1.TabIndex = 48
        Me.btnAutoStep_1.Text = "Auto"
        Me.btnAutoStep_1.UseVisualStyleBackColor = True
        Me.btnAutoStep_1.Visible = False
        '
        'txtStep_1
        '
        Me.txtStep_1.Location = New System.Drawing.Point(195, 30)
        Me.txtStep_1.Name = "txtStep_1"
        Me.txtStep_1.Size = New System.Drawing.Size(36, 20)
        Me.txtStep_1.TabIndex = 47
        Me.txtStep_1.Text = "--%"
        '
        'TrackBar1
        '
        Me.TrackBar1.LargeChange = 0
        Me.TrackBar1.Location = New System.Drawing.Point(40, 26)
        Me.TrackBar1.Maximum = 100
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(155, 45)
        Me.TrackBar1.SmallChange = 0
        Me.TrackBar1.TabIndex = 46
        Me.TrackBar1.TickFrequency = 100
        Me.TrackBar1.TickStyle = System.Windows.Forms.TickStyle.None
        Me.TrackBar1.Value = 50
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Azure
        Me.Panel2.Controls.Add(Me.lblHeatAuto)
        Me.Panel2.Controls.Add(Me.chkInblaas)
        Me.Panel2.Controls.Add(Me.btnAutoHeat)
        Me.Panel2.Controls.Add(Me.chkElement)
        Me.Panel2.Controls.Add(Me.chkBoiler)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.btnElement)
        Me.Panel2.Location = New System.Drawing.Point(162, 141)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(147, 152)
        Me.Panel2.TabIndex = 54
        '
        'lblHeatAuto
        '
        Me.lblHeatAuto.AutoSize = True
        Me.lblHeatAuto.Location = New System.Drawing.Point(95, 5)
        Me.lblHeatAuto.Name = "lblHeatAuto"
        Me.lblHeatAuto.Size = New System.Drawing.Size(29, 13)
        Me.lblHeatAuto.TabIndex = 64
        Me.lblHeatAuto.Text = "Auto"
        '
        'chkInblaas
        '
        Me.chkInblaas.AutoSize = True
        Me.chkInblaas.Location = New System.Drawing.Point(24, 74)
        Me.chkInblaas.Name = "chkInblaas"
        Me.chkInblaas.Size = New System.Drawing.Size(60, 17)
        Me.chkInblaas.TabIndex = 63
        Me.chkInblaas.Text = "Inblaas"
        Me.chkInblaas.UseVisualStyleBackColor = True
        '
        'btnAutoHeat
        '
        Me.btnAutoHeat.Location = New System.Drawing.Point(97, 28)
        Me.btnAutoHeat.Name = "btnAutoHeat"
        Me.btnAutoHeat.Size = New System.Drawing.Size(42, 23)
        Me.btnAutoHeat.TabIndex = 62
        Me.btnAutoHeat.Text = "Auto"
        Me.btnAutoHeat.UseVisualStyleBackColor = True
        Me.btnAutoHeat.Visible = False
        '
        'chkElement
        '
        Me.chkElement.AutoSize = True
        Me.chkElement.Location = New System.Drawing.Point(24, 51)
        Me.chkElement.Name = "chkElement"
        Me.chkElement.Size = New System.Drawing.Size(64, 17)
        Me.chkElement.TabIndex = 61
        Me.chkElement.Text = "Element"
        Me.chkElement.UseVisualStyleBackColor = True
        '
        'chkBoiler
        '
        Me.chkBoiler.AutoSize = True
        Me.chkBoiler.Location = New System.Drawing.Point(24, 28)
        Me.chkBoiler.Name = "chkBoiler"
        Me.chkBoiler.Size = New System.Drawing.Size(52, 17)
        Me.chkBoiler.TabIndex = 60
        Me.chkBoiler.Text = "Boiler"
        Me.chkBoiler.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 5)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 13)
        Me.Label7.TabIndex = 59
        Me.Label7.Text = "Heating"
        '
        'btnElement
        '
        Me.btnElement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnElement.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnElement.ImageIndex = 0
        Me.btnElement.ImageList = Me.ImageList1
        Me.btnElement.Location = New System.Drawing.Point(3, 126)
        Me.btnElement.Name = "btnElement"
        Me.btnElement.Size = New System.Drawing.Size(75, 23)
        Me.btnElement.TabIndex = 49
        Me.btnElement.Text = "Heat Off"
        Me.btnElement.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btnElement.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Azure
        Me.Panel1.Controls.Add(Me.lblFansAuto)
        Me.Panel1.Controls.Add(Me.btnAutoFans)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.optFanSpeed_4)
        Me.Panel1.Controls.Add(Me.optFanSpeed_3)
        Me.Panel1.Controls.Add(Me.optFanSpeed_2)
        Me.Panel1.Controls.Add(Me.optFanSpeed_1)
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Location = New System.Drawing.Point(4, 141)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(155, 152)
        Me.Panel1.TabIndex = 49
        '
        'lblFansAuto
        '
        Me.lblFansAuto.AutoSize = True
        Me.lblFansAuto.Location = New System.Drawing.Point(107, 5)
        Me.lblFansAuto.Name = "lblFansAuto"
        Me.lblFansAuto.Size = New System.Drawing.Size(29, 13)
        Me.lblFansAuto.TabIndex = 61
        Me.lblFansAuto.Text = "Auto"
        '
        'btnAutoFans
        '
        Me.btnAutoFans.Location = New System.Drawing.Point(110, 28)
        Me.btnAutoFans.Name = "btnAutoFans"
        Me.btnAutoFans.Size = New System.Drawing.Size(42, 23)
        Me.btnAutoFans.TabIndex = 60
        Me.btnAutoFans.Text = "Auto"
        Me.btnAutoFans.UseVisualStyleBackColor = True
        Me.btnAutoFans.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 5)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 13)
        Me.Label6.TabIndex = 59
        Me.Label6.Text = "Fans"
        '
        'optFanSpeed_4
        '
        Me.optFanSpeed_4.AutoSize = True
        Me.optFanSpeed_4.Location = New System.Drawing.Point(26, 96)
        Me.optFanSpeed_4.Name = "optFanSpeed_4"
        Me.optFanSpeed_4.Size = New System.Drawing.Size(65, 17)
        Me.optFanSpeed_4.TabIndex = 53
        Me.optFanSpeed_4.TabStop = True
        Me.optFanSpeed_4.Text = "Speed 4"
        Me.optFanSpeed_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optFanSpeed_4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.optFanSpeed_4.UseVisualStyleBackColor = True
        '
        'optFanSpeed_3
        '
        Me.optFanSpeed_3.AutoSize = True
        Me.optFanSpeed_3.Location = New System.Drawing.Point(26, 73)
        Me.optFanSpeed_3.Name = "optFanSpeed_3"
        Me.optFanSpeed_3.Size = New System.Drawing.Size(65, 17)
        Me.optFanSpeed_3.TabIndex = 52
        Me.optFanSpeed_3.TabStop = True
        Me.optFanSpeed_3.Text = "Speed 3"
        Me.optFanSpeed_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optFanSpeed_3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.optFanSpeed_3.UseVisualStyleBackColor = True
        '
        'optFanSpeed_2
        '
        Me.optFanSpeed_2.AutoSize = True
        Me.optFanSpeed_2.Location = New System.Drawing.Point(26, 50)
        Me.optFanSpeed_2.Name = "optFanSpeed_2"
        Me.optFanSpeed_2.Size = New System.Drawing.Size(65, 17)
        Me.optFanSpeed_2.TabIndex = 51
        Me.optFanSpeed_2.TabStop = True
        Me.optFanSpeed_2.Text = "Speed 2"
        Me.optFanSpeed_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optFanSpeed_2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.optFanSpeed_2.UseVisualStyleBackColor = True
        '
        'optFanSpeed_1
        '
        Me.optFanSpeed_1.AutoSize = True
        Me.optFanSpeed_1.Location = New System.Drawing.Point(26, 27)
        Me.optFanSpeed_1.Name = "optFanSpeed_1"
        Me.optFanSpeed_1.Size = New System.Drawing.Size(65, 17)
        Me.optFanSpeed_1.TabIndex = 50
        Me.optFanSpeed_1.TabStop = True
        Me.optFanSpeed_1.Text = "Speed 1"
        Me.optFanSpeed_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optFanSpeed_1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.optFanSpeed_1.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button3.ImageIndex = 0
        Me.Button3.ImageList = Me.ImageList1
        Me.Button3.Location = New System.Drawing.Point(6, 126)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(84, 23)
        Me.Button3.TabIndex = 49
        Me.Button3.Text = "Fans Off"
        Me.Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.Button3.UseVisualStyleBackColor = True
        '
        'pnlLog
        '
        Me.pnlLog.BackColor = System.Drawing.Color.Silver
        Me.pnlLog.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlLog.Controls.Add(Me.chkClearLog)
        Me.pnlLog.Controls.Add(Me.Button4)
        Me.pnlLog.Controls.Add(Me.rtbLog)
        Me.pnlLog.Controls.Add(Me.btnErrorReset)
        Me.pnlLog.Location = New System.Drawing.Point(1, 296)
        Me.pnlLog.Name = "pnlLog"
        Me.pnlLog.Size = New System.Drawing.Size(308, 117)
        Me.pnlLog.TabIndex = 108
        '
        'chkClearLog
        '
        Me.chkClearLog.AutoSize = True
        Me.chkClearLog.Checked = True
        Me.chkClearLog.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkClearLog.Location = New System.Drawing.Point(3, 2)
        Me.chkClearLog.Name = "chkClearLog"
        Me.chkClearLog.Size = New System.Drawing.Size(122, 17)
        Me.chkClearLog.TabIndex = 4
        Me.chkClearLog.Text = "New entry clears log"
        Me.chkClearLog.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.BackgroundImage = Global.mimics.My.Resources.Resources.btnExit
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button4.Location = New System.Drawing.Point(282, -2)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(24, 23)
        Me.Button4.TabIndex = 3
        Me.Button4.UseVisualStyleBackColor = True
        '
        'rtbLog
        '
        Me.rtbLog.Location = New System.Drawing.Point(1, 20)
        Me.rtbLog.Name = "rtbLog"
        Me.rtbLog.Size = New System.Drawing.Size(297, 92)
        Me.rtbLog.TabIndex = 0
        Me.rtbLog.Text = ""
        '
        'btnErrorReset
        '
        Me.btnErrorReset.BackgroundImage = Global.mimics.My.Resources.Resources.btnExit
        Me.btnErrorReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnErrorReset.Location = New System.Drawing.Point(851, -2)
        Me.btnErrorReset.Name = "btnErrorReset"
        Me.btnErrorReset.Size = New System.Drawing.Size(24, 23)
        Me.btnErrorReset.TabIndex = 2
        Me.btnErrorReset.UseVisualStyleBackColor = True
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.TabPage1)
        Me.TabControl.Location = New System.Drawing.Point(0, 140)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(331, 451)
        Me.TabControl.TabIndex = 110
        '
        'lblHadecoCycleStart
        '
        Me.lblHadecoCycleStart.AutoSize = True
        Me.lblHadecoCycleStart.Location = New System.Drawing.Point(13, 83)
        Me.lblHadecoCycleStart.Name = "lblHadecoCycleStart"
        Me.lblHadecoCycleStart.Size = New System.Drawing.Size(74, 13)
        Me.lblHadecoCycleStart.TabIndex = 114
        Me.lblHadecoCycleStart.Text = "Hadeco Cycle"
        Me.lblHadecoCycleStart.Visible = False
        '
        'tmrDlayTCP
        '
        Me.tmrDlayTCP.Interval = 750
        '
        'frmControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(327, 590)
        Me.Controls.Add(Me.lblHadecoCycleStart)
        Me.Controls.Add(Me.lblDateTimeStamp)
        Me.Controls.Add(Me.pnl0)
        Me.Controls.Add(Me.btnStatus)
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.cmbIP)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmControl"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Mimics Control"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnl0.ResumeLayout(False)
        Me.pnlError.ResumeLayout(False)
        Me.pnlError.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.pnl1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.TrackBar3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlLog.ResumeLayout(False)
        Me.pnlLog.PerformLayout()
        Me.TabControl.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnTransmit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbIP As System.Windows.Forms.ComboBox
    Private WithEvents btnRefresh As System.Windows.Forms.Button
    Private WithEvents btnStatus As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents pnl0 As System.Windows.Forms.Panel
    Private WithEvents BTN4 As System.Windows.Forms.Button
    Private WithEvents BTN3 As System.Windows.Forms.Button
    Private WithEvents BTN2 As System.Windows.Forms.Button
    Private WithEvents BTN1 As System.Windows.Forms.Button
    Friend WithEvents tmrRefresh As System.Windows.Forms.Timer
    Friend WithEvents pnlError As System.Windows.Forms.Panel
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents rtbError As System.Windows.Forms.RichTextBox
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents tmrActionSQL As System.Windows.Forms.Timer
    Friend WithEvents lblDateTimeStamp As System.Windows.Forms.Label
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents pnl1 As System.Windows.Forms.Panel
    Friend WithEvents pnlLog As System.Windows.Forms.Panel
    Friend WithEvents chkClearLog As System.Windows.Forms.CheckBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents rtbLog As System.Windows.Forms.RichTextBox
    Friend WithEvents btnErrorReset As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblVal_2 As System.Windows.Forms.Label
    Friend WithEvents lblVal_3 As System.Windows.Forms.Label
    Friend WithEvents lblVal_1 As System.Windows.Forms.Label
    Friend WithEvents btnAutoStep_3 As System.Windows.Forms.Button
    Friend WithEvents btnAutoStep_2 As System.Windows.Forms.Button
    Friend WithEvents txtStep_3 As System.Windows.Forms.TextBox
    Friend WithEvents txtStep_2 As System.Windows.Forms.TextBox
    Friend WithEvents TrackBar3 As System.Windows.Forms.TrackBar
    Friend WithEvents TrackBar2 As System.Windows.Forms.TrackBar
    Friend WithEvents txtStep_1 As System.Windows.Forms.TextBox
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkInblaas As System.Windows.Forms.CheckBox
    Friend WithEvents btnAutoHeat As System.Windows.Forms.Button
    Friend WithEvents chkElement As System.Windows.Forms.CheckBox
    Friend WithEvents chkBoiler As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnElement As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnAutoFans As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents optFanSpeed_4 As System.Windows.Forms.RadioButton
    Friend WithEvents optFanSpeed_3 As System.Windows.Forms.RadioButton
    Friend WithEvents optFanSpeed_2 As System.Windows.Forms.RadioButton
    Friend WithEvents optFanSpeed_1 As System.Windows.Forms.RadioButton
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents lblHadecoCycleStart As System.Windows.Forms.Label
    Friend WithEvents tmrDlayTCP As System.Windows.Forms.Timer
    Friend WithEvents lblAutoMan_2 As System.Windows.Forms.Label
    Friend WithEvents lblAutoMan_1 As System.Windows.Forms.Label
    Friend WithEvents btnAutoStep_1 As System.Windows.Forms.Button
    Friend WithEvents lblAutoMan_3 As System.Windows.Forms.Label
    Friend WithEvents lblHeatAuto As System.Windows.Forms.Label
    Friend WithEvents lblFansAuto As System.Windows.Forms.Label
End Class
