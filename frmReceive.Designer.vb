<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReceive
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReceive))
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnReceive = New System.Windows.Forms.ToolStripButton
        Me.btnGraph = New System.Windows.Forms.ToolStripButton
        Me.btnReset = New System.Windows.Forms.ToolStripButton
        Me.btnExit = New System.Windows.Forms.ToolStripButton
        Me.lstBox = New System.Windows.Forms.ListBox
        Me.lblListening = New System.Windows.Forms.Label
        Me.tmrPoll = New System.Windows.Forms.Timer(Me.components)
        Me.lblNotListening = New System.Windows.Forms.Label
        Me.tmrLabel = New System.Windows.Forms.Timer(Me.components)
        Me.lblDBsize = New System.Windows.Forms.Label
        Me.DTP = New System.Windows.Forms.DateTimePicker
        Me.DG = New System.Windows.Forms.DataGridView
        Me.tmrDGsort = New System.Windows.Forms.Timer(Me.components)
        Me.lblRecords = New System.Windows.Forms.Label
        Me.cmbIP = New System.Windows.Forms.ComboBox
        Me.chkData = New System.Windows.Forms.RadioButton
        Me.chkStatus = New System.Windows.Forms.RadioButton
        Me.lblNotListening_2 = New System.Windows.Forms.Label
        Me.lblListening_2 = New System.Windows.Forms.Label
        Me.pnlError = New System.Windows.Forms.Panel
        Me.btnErrorReset = New System.Windows.Forms.Button
        Me.lblError = New System.Windows.Forms.Label
        Me.rtbError = New System.Windows.Forms.RichTextBox
        Me.pnlGraph = New System.Windows.Forms.Panel
        Me.chkScroll = New System.Windows.Forms.CheckBox
        Me.pnlChartVariables = New System.Windows.Forms.Panel
        Me.txtRecordSel = New System.Windows.Forms.TextBox
        Me.txtDateSel = New System.Windows.Forms.TextBox
        Me.txtDateOrRecords = New System.Windows.Forms.TextBox
        Me.btnSubmit = New System.Windows.Forms.Button
        Me.lblNoIP = New System.Windows.Forms.Label
        Me.txtTime = New System.Windows.Forms.TextBox
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.scale_2 = New System.Windows.Forms.TextBox
        Me.scale_1 = New System.Windows.Forms.TextBox
        Me.cmbSource_2 = New System.Windows.Forms.ComboBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtMax = New System.Windows.Forms.TextBox
        Me.lblPeriod = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtRecordCount = New System.Windows.Forms.TextBox
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart
        Me.cmbSource_1 = New System.Windows.Forms.ComboBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.TrackBar1 = New System.Windows.Forms.TrackBar
        Me.tmrChart = New System.Windows.Forms.Timer(Me.components)
        Me.lblPollIndex = New System.Windows.Forms.Label
        Me.tmrSubmit = New System.Windows.Forms.Timer(Me.components)
        Me.txtDateTime = New System.Windows.Forms.TextBox
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlError.SuspendLayout()
        Me.pnlGraph.SuspendLayout()
        Me.pnlChartVariables.SuspendLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnReceive, Me.btnGraph, Me.btnReset, Me.btnExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1372, 51)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnReceive
        '
        Me.btnReceive.AutoSize = False
        Me.btnReceive.CheckOnClick = True
        Me.btnReceive.Image = CType(resources.GetObject("btnReceive.Image"), System.Drawing.Image)
        Me.btnReceive.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnReceive.Margin = New System.Windows.Forms.Padding(0, 1, 8, 2)
        Me.btnReceive.Name = "btnReceive"
        Me.btnReceive.Size = New System.Drawing.Size(48, 48)
        Me.btnReceive.Text = "Poll"
        Me.btnReceive.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnReceive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnReceive.ToolTipText = "Poll MySql database for received data"
        '
        'btnGraph
        '
        Me.btnGraph.AutoSize = False
        Me.btnGraph.Image = CType(resources.GetObject("btnGraph.Image"), System.Drawing.Image)
        Me.btnGraph.ImageTransparentColor = System.Drawing.Color.White
        Me.btnGraph.Margin = New System.Windows.Forms.Padding(0, 1, 8, 2)
        Me.btnGraph.Name = "btnGraph"
        Me.btnGraph.Size = New System.Drawing.Size(48, 48)
        Me.btnGraph.Text = "Graph"
        Me.btnGraph.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGraph.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnGraph.ToolTipText = "Display graph of selected channel"
        '
        'btnReset
        '
        Me.btnReset.AutoSize = False
        Me.btnReset.Image = CType(resources.GetObject("btnReset.Image"), System.Drawing.Image)
        Me.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnReset.Margin = New System.Windows.Forms.Padding(0, 1, 8, 2)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(48, 48)
        Me.btnReset.Text = "Clear"
        Me.btnReset.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnReset.ToolTipText = "Receive data from mimics"
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
        'lstBox
        '
        Me.lstBox.FormattingEnabled = True
        Me.lstBox.Location = New System.Drawing.Point(35, 80)
        Me.lstBox.Name = "lstBox"
        Me.lstBox.Size = New System.Drawing.Size(77, 69)
        Me.lstBox.TabIndex = 4
        '
        'lblListening
        '
        Me.lblListening.AutoSize = True
        Me.lblListening.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblListening.ForeColor = System.Drawing.Color.Green
        Me.lblListening.Location = New System.Drawing.Point(12, 425)
        Me.lblListening.Name = "lblListening"
        Me.lblListening.Size = New System.Drawing.Size(134, 19)
        Me.lblListening.TabIndex = 5
        Me.lblListening.Text = "Polling MySql ....."
        '
        'tmrPoll
        '
        Me.tmrPoll.Interval = 500
        '
        'lblNotListening
        '
        Me.lblNotListening.AutoSize = True
        Me.lblNotListening.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotListening.ForeColor = System.Drawing.Color.Red
        Me.lblNotListening.Location = New System.Drawing.Point(12, 425)
        Me.lblNotListening.Name = "lblNotListening"
        Me.lblNotListening.Size = New System.Drawing.Size(91, 19)
        Me.lblNotListening.TabIndex = 6
        Me.lblNotListening.Text = "Not Polling!"
        '
        'tmrLabel
        '
        Me.tmrLabel.Interval = 500
        '
        'lblDBsize
        '
        Me.lblDBsize.AutoSize = True
        Me.lblDBsize.Location = New System.Drawing.Point(628, 9)
        Me.lblDBsize.Name = "lblDBsize"
        Me.lblDBsize.Size = New System.Drawing.Size(39, 13)
        Me.lblDBsize.TabIndex = 7
        Me.lblDBsize.Text = "dbSize"
        '
        'DTP
        '
        Me.DTP.Location = New System.Drawing.Point(317, 2)
        Me.DTP.Name = "DTP"
        Me.DTP.Size = New System.Drawing.Size(118, 20)
        Me.DTP.TabIndex = 9
        '
        'DG
        '
        Me.DG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG.Dock = System.Windows.Forms.DockStyle.Top
        Me.DG.Location = New System.Drawing.Point(0, 51)
        Me.DG.Name = "DG"
        Me.DG.Size = New System.Drawing.Size(1372, 371)
        Me.DG.TabIndex = 11
        '
        'tmrDGsort
        '
        Me.tmrDGsort.Enabled = True
        Me.tmrDGsort.Interval = 500
        '
        'lblRecords
        '
        Me.lblRecords.AutoSize = True
        Me.lblRecords.Location = New System.Drawing.Point(628, 32)
        Me.lblRecords.Name = "lblRecords"
        Me.lblRecords.Size = New System.Drawing.Size(47, 13)
        Me.lblRecords.TabIndex = 12
        Me.lblRecords.Text = "Records"
        '
        'cmbIP
        '
        Me.cmbIP.FormattingEnabled = True
        Me.cmbIP.Location = New System.Drawing.Point(317, 24)
        Me.cmbIP.Name = "cmbIP"
        Me.cmbIP.Size = New System.Drawing.Size(118, 21)
        Me.cmbIP.TabIndex = 13
        '
        'chkData
        '
        Me.chkData.AutoSize = True
        Me.chkData.Checked = True
        Me.chkData.Location = New System.Drawing.Point(460, 4)
        Me.chkData.Name = "chkData"
        Me.chkData.Size = New System.Drawing.Size(78, 17)
        Me.chkData.TabIndex = 14
        Me.chkData.TabStop = True
        Me.chkData.Text = "Show Data"
        Me.chkData.UseVisualStyleBackColor = True
        '
        'chkStatus
        '
        Me.chkStatus.AutoSize = True
        Me.chkStatus.Location = New System.Drawing.Point(460, 27)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.Size = New System.Drawing.Size(85, 17)
        Me.chkStatus.TabIndex = 15
        Me.chkStatus.Text = "Show Status"
        Me.chkStatus.UseVisualStyleBackColor = True
        '
        'lblNotListening_2
        '
        Me.lblNotListening_2.AutoSize = True
        Me.lblNotListening_2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotListening_2.ForeColor = System.Drawing.Color.Red
        Me.lblNotListening_2.Location = New System.Drawing.Point(905, 4)
        Me.lblNotListening_2.Name = "lblNotListening_2"
        Me.lblNotListening_2.Size = New System.Drawing.Size(91, 19)
        Me.lblNotListening_2.TabIndex = 16
        Me.lblNotListening_2.Text = "Not Polling!"
        '
        'lblListening_2
        '
        Me.lblListening_2.AutoSize = True
        Me.lblListening_2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblListening_2.ForeColor = System.Drawing.Color.Green
        Me.lblListening_2.Location = New System.Drawing.Point(905, 4)
        Me.lblListening_2.Name = "lblListening_2"
        Me.lblListening_2.Size = New System.Drawing.Size(134, 19)
        Me.lblListening_2.TabIndex = 17
        Me.lblListening_2.Text = "Polling MySql ....."
        '
        'pnlError
        '
        Me.pnlError.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlError.Controls.Add(Me.btnErrorReset)
        Me.pnlError.Controls.Add(Me.lblError)
        Me.pnlError.Controls.Add(Me.rtbError)
        Me.pnlError.Location = New System.Drawing.Point(184, 288)
        Me.pnlError.Name = "pnlError"
        Me.pnlError.Size = New System.Drawing.Size(877, 134)
        Me.pnlError.TabIndex = 18
        Me.pnlError.Visible = False
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
        Me.rtbError.Location = New System.Drawing.Point(4, 19)
        Me.rtbError.Name = "rtbError"
        Me.rtbError.Size = New System.Drawing.Size(862, 104)
        Me.rtbError.TabIndex = 0
        Me.rtbError.Text = ""
        '
        'pnlGraph
        '
        Me.pnlGraph.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.pnlGraph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlGraph.Controls.Add(Me.chkScroll)
        Me.pnlGraph.Controls.Add(Me.pnlChartVariables)
        Me.pnlGraph.Controls.Add(Me.btnSubmit)
        Me.pnlGraph.Controls.Add(Me.lblNoIP)
        Me.pnlGraph.Controls.Add(Me.txtTime)
        Me.pnlGraph.Controls.Add(Me.DateTimePicker1)
        Me.pnlGraph.Controls.Add(Me.Label4)
        Me.pnlGraph.Controls.Add(Me.Label3)
        Me.pnlGraph.Controls.Add(Me.scale_2)
        Me.pnlGraph.Controls.Add(Me.scale_1)
        Me.pnlGraph.Controls.Add(Me.cmbSource_2)
        Me.pnlGraph.Controls.Add(Me.Button3)
        Me.pnlGraph.Controls.Add(Me.Label2)
        Me.pnlGraph.Controls.Add(Me.txtMax)
        Me.pnlGraph.Controls.Add(Me.lblPeriod)
        Me.pnlGraph.Controls.Add(Me.Label1)
        Me.pnlGraph.Controls.Add(Me.txtRecordCount)
        Me.pnlGraph.Controls.Add(Me.Chart1)
        Me.pnlGraph.Controls.Add(Me.cmbSource_1)
        Me.pnlGraph.Controls.Add(Me.Button2)
        Me.pnlGraph.Controls.Add(Me.TrackBar1)
        Me.pnlGraph.Location = New System.Drawing.Point(0, 447)
        Me.pnlGraph.Name = "pnlGraph"
        Me.pnlGraph.Size = New System.Drawing.Size(1372, 290)
        Me.pnlGraph.TabIndex = 20
        Me.pnlGraph.Visible = False
        '
        'chkScroll
        '
        Me.chkScroll.AutoSize = True
        Me.chkScroll.Checked = True
        Me.chkScroll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkScroll.Location = New System.Drawing.Point(1254, 118)
        Me.chkScroll.Name = "chkScroll"
        Me.chkScroll.Size = New System.Drawing.Size(52, 17)
        Me.chkScroll.TabIndex = 38
        Me.chkScroll.Text = "Scroll"
        Me.chkScroll.UseVisualStyleBackColor = True
        '
        'pnlChartVariables
        '
        Me.pnlChartVariables.Controls.Add(Me.txtDateTime)
        Me.pnlChartVariables.Controls.Add(Me.txtRecordSel)
        Me.pnlChartVariables.Controls.Add(Me.txtDateSel)
        Me.pnlChartVariables.Controls.Add(Me.txtDateOrRecords)
        Me.pnlChartVariables.Location = New System.Drawing.Point(1235, 167)
        Me.pnlChartVariables.Name = "pnlChartVariables"
        Me.pnlChartVariables.Size = New System.Drawing.Size(123, 107)
        Me.pnlChartVariables.TabIndex = 37
        '
        'txtRecordSel
        '
        Me.txtRecordSel.Location = New System.Drawing.Point(3, 84)
        Me.txtRecordSel.Name = "txtRecordSel"
        Me.txtRecordSel.Size = New System.Drawing.Size(75, 20)
        Me.txtRecordSel.TabIndex = 3
        '
        'txtDateSel
        '
        Me.txtDateSel.Location = New System.Drawing.Point(3, 29)
        Me.txtDateSel.Name = "txtDateSel"
        Me.txtDateSel.Size = New System.Drawing.Size(100, 20)
        Me.txtDateSel.TabIndex = 1
        '
        'txtDateOrRecords
        '
        Me.txtDateOrRecords.Location = New System.Drawing.Point(3, 3)
        Me.txtDateOrRecords.Name = "txtDateOrRecords"
        Me.txtDateOrRecords.Size = New System.Drawing.Size(75, 20)
        Me.txtDateOrRecords.TabIndex = 0
        '
        'btnSubmit
        '
        Me.btnSubmit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSubmit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSubmit.Location = New System.Drawing.Point(1254, 141)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(50, 23)
        Me.btnSubmit.TabIndex = 36
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'lblNoIP
        '
        Me.lblNoIP.AutoSize = True
        Me.lblNoIP.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoIP.ForeColor = System.Drawing.Color.Red
        Me.lblNoIP.Location = New System.Drawing.Point(480, 127)
        Me.lblNoIP.Name = "lblNoIP"
        Me.lblNoIP.Size = New System.Drawing.Size(168, 19)
        Me.lblNoIP.TabIndex = 22
        Me.lblNoIP.Text = "No IP adress selected!!"
        Me.lblNoIP.Visible = False
        '
        'txtTime
        '
        Me.txtTime.Location = New System.Drawing.Point(410, 3)
        Me.txtTime.Name = "txtTime"
        Me.txtTime.Size = New System.Drawing.Size(68, 20)
        Me.txtTime.TabIndex = 33
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(317, 3)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(87, 20)
        Me.DateTimePicker1.TabIndex = 32
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(121, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(20, 17)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "%"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(271, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 17)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "%"
        '
        'scale_2
        '
        Me.scale_2.Location = New System.Drawing.Point(227, 3)
        Me.scale_2.Name = "scale_2"
        Me.scale_2.Size = New System.Drawing.Size(38, 20)
        Me.scale_2.TabIndex = 25
        '
        'scale_1
        '
        Me.scale_1.Location = New System.Drawing.Point(77, 3)
        Me.scale_1.Name = "scale_1"
        Me.scale_1.Size = New System.Drawing.Size(38, 20)
        Me.scale_1.TabIndex = 24
        '
        'cmbSource_2
        '
        Me.cmbSource_2.FormattingEnabled = True
        Me.cmbSource_2.Items.AddRange(New Object() {"A0", "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "A10"})
        Me.cmbSource_2.Location = New System.Drawing.Point(164, 3)
        Me.cmbSource_2.Name = "cmbSource_2"
        Me.cmbSource_2.Size = New System.Drawing.Size(57, 21)
        Me.cmbSource_2.TabIndex = 23
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.SystemColors.Control
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(1145, 5)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(30, 23)
        Me.Button3.TabIndex = 22
        Me.Button3.Text = "Auto"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1009, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 17)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Max Y axis"
        '
        'txtMax
        '
        Me.txtMax.Location = New System.Drawing.Point(1089, 8)
        Me.txtMax.Name = "txtMax"
        Me.txtMax.Size = New System.Drawing.Size(50, 20)
        Me.txtMax.TabIndex = 11
        Me.txtMax.Text = "Auto"
        '
        'lblPeriod
        '
        Me.lblPeriod.AutoSize = True
        Me.lblPeriod.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPeriod.Location = New System.Drawing.Point(1181, 8)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(133, 17)
        Me.lblPeriod.TabIndex = 10
        Me.lblPeriod.Text = "11:59:23 - 12:45:36"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(552, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 17)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Records"
        '
        'txtRecordCount
        '
        Me.txtRecordCount.Location = New System.Drawing.Point(484, 3)
        Me.txtRecordCount.Name = "txtRecordCount"
        Me.txtRecordCount.Size = New System.Drawing.Size(62, 20)
        Me.txtRecordCount.TabIndex = 7
        '
        'Chart1
        '
        Me.Chart1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Chart1.BorderlineColor = System.Drawing.Color.Gray
        Me.Chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid
        Me.Chart1.BorderlineWidth = 5
        ChartArea1.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.AxisX.IsLabelAutoFit = False
        ChartArea1.AxisX.IsMarginVisible = False
        ChartArea1.BackColor = System.Drawing.Color.LemonChiffon
        ChartArea1.InnerPlotPosition.Auto = False
        ChartArea1.InnerPlotPosition.Height = 84.27526!
        ChartArea1.InnerPlotPosition.Width = 94.59806!
        ChartArea1.InnerPlotPosition.X = 4.79357!
        ChartArea1.InnerPlotPosition.Y = 3.90957!
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(3, 30)
        Me.Chart1.Name = "Chart1"
        Series1.BackSecondaryColor = System.Drawing.Color.White
        Series1.BorderWidth = 3
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series1.Color = System.Drawing.Color.Black
        Series1.Legend = "Legend1"
        Series1.Name = "Px"
        Series1.YValuesPerPoint = 2
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(1362, 253)
        Me.Chart1.TabIndex = 6
        Me.Chart1.Text = "Chart1"
        '
        'cmbSource_1
        '
        Me.cmbSource_1.FormattingEnabled = True
        Me.cmbSource_1.Items.AddRange(New Object() {"A0", "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "A10"})
        Me.cmbSource_1.Location = New System.Drawing.Point(14, 3)
        Me.cmbSource_1.Name = "cmbSource_1"
        Me.cmbSource_1.Size = New System.Drawing.Size(57, 21)
        Me.cmbSource_1.TabIndex = 3
        '
        'Button2
        '
        Me.Button2.BackgroundImage = Global.mimics.My.Resources.Resources.btnExit
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.Location = New System.Drawing.Point(1334, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(24, 23)
        Me.Button2.TabIndex = 2
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TrackBar1
        '
        Me.TrackBar1.LargeChange = 100
        Me.TrackBar1.Location = New System.Drawing.Point(619, 7)
        Me.TrackBar1.Maximum = 10000
        Me.TrackBar1.Minimum = 10
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(318, 45)
        Me.TrackBar1.SmallChange = 100
        Me.TrackBar1.TabIndex = 31
        Me.TrackBar1.TickFrequency = 100
        Me.TrackBar1.TickStyle = System.Windows.Forms.TickStyle.None
        Me.TrackBar1.Value = 3600
        '
        'tmrChart
        '
        Me.tmrChart.Interval = 1500
        '
        'lblPollIndex
        '
        Me.lblPollIndex.AutoSize = True
        Me.lblPollIndex.Location = New System.Drawing.Point(807, 32)
        Me.lblPollIndex.Name = "lblPollIndex"
        Me.lblPollIndex.Size = New System.Drawing.Size(49, 13)
        Me.lblPollIndex.TabIndex = 21
        Me.lblPollIndex.Text = "pollIndex"
        '
        'tmrSubmit
        '
        Me.tmrSubmit.Interval = 350
        '
        'txtDateTime
        '
        Me.txtDateTime.Location = New System.Drawing.Point(4, 55)
        Me.txtDateTime.Name = "txtDateTime"
        Me.txtDateTime.Size = New System.Drawing.Size(99, 20)
        Me.txtDateTime.TabIndex = 4
        '
        'frmReceive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1372, 735)
        Me.Controls.Add(Me.lblPollIndex)
        Me.Controls.Add(Me.pnlGraph)
        Me.Controls.Add(Me.pnlError)
        Me.Controls.Add(Me.lblListening_2)
        Me.Controls.Add(Me.lblNotListening_2)
        Me.Controls.Add(Me.chkStatus)
        Me.Controls.Add(Me.chkData)
        Me.Controls.Add(Me.cmbIP)
        Me.Controls.Add(Me.lblRecords)
        Me.Controls.Add(Me.DG)
        Me.Controls.Add(Me.DTP)
        Me.Controls.Add(Me.lblDBsize)
        Me.Controls.Add(Me.lblNotListening)
        Me.Controls.Add(Me.lblListening)
        Me.Controls.Add(Me.lstBox)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmReceive"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Mimics receive"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlError.ResumeLayout(False)
        Me.pnlError.PerformLayout()
        Me.pnlGraph.ResumeLayout(False)
        Me.pnlGraph.PerformLayout()
        Me.pnlChartVariables.ResumeLayout(False)
        Me.pnlChartVariables.PerformLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnReceive As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents lstBox As System.Windows.Forms.ListBox
    Friend WithEvents lblListening As System.Windows.Forms.Label
    Friend WithEvents tmrPoll As System.Windows.Forms.Timer
    Friend WithEvents lblNotListening As System.Windows.Forms.Label
    Friend WithEvents tmrLabel As System.Windows.Forms.Timer
    Friend WithEvents btnReset As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblDBsize As System.Windows.Forms.Label
    Friend WithEvents DTP As System.Windows.Forms.DateTimePicker
    Friend WithEvents DG As System.Windows.Forms.DataGridView
    Friend WithEvents tmrDGsort As System.Windows.Forms.Timer
    Friend WithEvents lblRecords As System.Windows.Forms.Label
    Friend WithEvents cmbIP As System.Windows.Forms.ComboBox
    Friend WithEvents chkData As System.Windows.Forms.RadioButton
    Friend WithEvents chkStatus As System.Windows.Forms.RadioButton
    Friend WithEvents lblNotListening_2 As System.Windows.Forms.Label
    Friend WithEvents lblListening_2 As System.Windows.Forms.Label
    Friend WithEvents pnlError As System.Windows.Forms.Panel
    Friend WithEvents rtbError As System.Windows.Forms.RichTextBox
    Friend WithEvents btnErrorReset As System.Windows.Forms.Button
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents pnlGraph As System.Windows.Forms.Panel
    Friend WithEvents cmbSource_1 As System.Windows.Forms.ComboBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtRecordCount As System.Windows.Forms.TextBox
    Friend WithEvents lblPeriod As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMax As System.Windows.Forms.TextBox
    Friend WithEvents tmrChart As System.Windows.Forms.Timer
    Friend WithEvents btnGraph As System.Windows.Forms.ToolStripButton
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents cmbSource_2 As System.Windows.Forms.ComboBox
    Friend WithEvents scale_2 As System.Windows.Forms.TextBox
    Friend WithEvents scale_1 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtTime As System.Windows.Forms.TextBox
    Friend WithEvents lblPollIndex As System.Windows.Forms.Label
    Friend WithEvents lblNoIP As System.Windows.Forms.Label
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents pnlChartVariables As System.Windows.Forms.Panel
    Friend WithEvents txtRecordSel As System.Windows.Forms.TextBox
    Friend WithEvents txtDateSel As System.Windows.Forms.TextBox
    Friend WithEvents txtDateOrRecords As System.Windows.Forms.TextBox
    Friend WithEvents tmrSubmit As System.Windows.Forms.Timer
    Friend WithEvents chkScroll As System.Windows.Forms.CheckBox
    Friend WithEvents txtDateTime As System.Windows.Forms.TextBox
End Class
