<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMonitor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMonitor))
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Mimics unit not broadcasting")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Mimics unit connected ")
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Mimics unit connected - notifications", 2, 2)
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Mimics unit connected > 3 mins ago")
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TreeViewKey = New System.Windows.Forms.TreeView
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblIP = New System.Windows.Forms.Label
        Me.treeView1 = New System.Windows.Forms.TreeView
        Me.lblIdent1 = New System.Windows.Forms.Label
        Me.lblDisconnect = New System.Windows.Forms.Label
        Me.lblDateTimeStamp = New System.Windows.Forms.Label
        Me.tmrRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.lblRefresh = New System.Windows.Forms.Label
        Me.chkAuto = New System.Windows.Forms.CheckBox
        Me.treeView2 = New System.Windows.Forms.TreeView
        Me.treeView3 = New System.Windows.Forms.TreeView
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblName = New System.Windows.Forms.Label
        Me.chkIPs = New System.Windows.Forms.CheckBox
        Me.TabControl = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.lstViewE = New System.Windows.Forms.ListView
        Me.ColumnHeader9 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader10 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader11 = New System.Windows.Forms.ColumnHeader
        Me.lstViewF = New System.Windows.Forms.ListView
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader8 = New System.Windows.Forms.ColumnHeader
        Me.lstViewD = New System.Windows.Forms.ListView
        Me.Axis = New System.Windows.Forms.ColumnHeader
        Me.Max = New System.Windows.Forms.ColumnHeader
        Me.Min = New System.Windows.Forms.ColumnHeader
        Me.lstViewC = New System.Windows.Forms.ListView
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.lstViewB = New System.Windows.Forms.ListView
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.lstViewA = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.rtbStatus = New System.Windows.Forms.RichTextBox
        Me.pnlError = New System.Windows.Forms.Panel
        Me.btnErrorReset = New System.Windows.Forms.Button
        Me.lblError = New System.Windows.Forms.Label
        Me.rtbError = New System.Windows.Forms.RichTextBox
        Me.tmrLoad = New System.Windows.Forms.Timer(Me.components)
        Me.tmrUDP = New System.Windows.Forms.Timer(Me.components)
        Me.lblUDP = New System.Windows.Forms.Button
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.TabControl.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.pnlError.SuspendLayout()
        Me.SuspendLayout()
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
        'TreeViewKey
        '
        Me.TreeViewKey.ImageIndex = 0
        Me.TreeViewKey.ImageList = Me.ImageList1
        Me.TreeViewKey.Location = New System.Drawing.Point(12, 23)
        Me.TreeViewKey.Name = "TreeViewKey"
        TreeNode5.ForeColor = System.Drawing.Color.Red
        TreeNode5.ImageIndex = 0
        TreeNode5.Name = "Node0"
        TreeNode5.SelectedImageKey = "explorer_exe_Ico62_ico_Ico1.ico"
        TreeNode5.Text = "Mimics unit not broadcasting"
        TreeNode6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        TreeNode6.ImageKey = "check.png"
        TreeNode6.Name = "Node1"
        TreeNode6.SelectedImageKey = "check.png"
        TreeNode6.Text = "Mimics unit connected "
        TreeNode7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        TreeNode7.ImageIndex = 2
        TreeNode7.Name = "Node2"
        TreeNode7.SelectedImageIndex = 2
        TreeNode7.Text = "Mimics unit connected - notifications"
        TreeNode8.ForeColor = System.Drawing.Color.DarkSeaGreen
        TreeNode8.ImageKey = "check_in.png"
        TreeNode8.Name = "Node0"
        TreeNode8.SelectedImageKey = "check_in.png"
        TreeNode8.Text = "Mimics unit connected > 3 mins ago"
        Me.TreeViewKey.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode5, TreeNode6, TreeNode7, TreeNode8})
        Me.TreeViewKey.Scrollable = False
        Me.TreeViewKey.SelectedImageIndex = 0
        Me.TreeViewKey.Size = New System.Drawing.Size(221, 69)
        Me.TreeViewKey.TabIndex = 6
        Me.TreeViewKey.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Key:"
        '
        'lblIP
        '
        Me.lblIP.AutoSize = True
        Me.lblIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIP.ForeColor = System.Drawing.Color.Black
        Me.lblIP.Location = New System.Drawing.Point(769, 2)
        Me.lblIP.Name = "lblIP"
        Me.lblIP.Size = New System.Drawing.Size(20, 17)
        Me.lblIP.TabIndex = 11
        Me.lblIP.Text = "IP"
        '
        'treeView1
        '
        Me.treeView1.FullRowSelect = True
        Me.treeView1.HideSelection = False
        Me.treeView1.ImageIndex = 0
        Me.treeView1.ImageList = Me.ImageList1
        Me.treeView1.Location = New System.Drawing.Point(6, 113)
        Me.treeView1.Name = "treeView1"
        Me.treeView1.SelectedImageIndex = 0
        Me.treeView1.Size = New System.Drawing.Size(141, 473)
        Me.treeView1.TabIndex = 0
        '
        'lblIdent1
        '
        Me.lblIdent1.AutoSize = True
        Me.lblIdent1.Location = New System.Drawing.Point(452, 97)
        Me.lblIdent1.Name = "lblIdent1"
        Me.lblIdent1.Size = New System.Drawing.Size(0, 13)
        Me.lblIdent1.TabIndex = 74
        '
        'lblDisconnect
        '
        Me.lblDisconnect.AutoSize = True
        Me.lblDisconnect.ForeColor = System.Drawing.Color.Red
        Me.lblDisconnect.Location = New System.Drawing.Point(440, 110)
        Me.lblDisconnect.Name = "lblDisconnect"
        Me.lblDisconnect.Size = New System.Drawing.Size(73, 13)
        Me.lblDisconnect.TabIndex = 78
        Me.lblDisconnect.Text = "Disconnected"
        Me.lblDisconnect.Visible = False
        '
        'lblDateTimeStamp
        '
        Me.lblDateTimeStamp.AutoSize = True
        Me.lblDateTimeStamp.ForeColor = System.Drawing.Color.Green
        Me.lblDateTimeStamp.Location = New System.Drawing.Point(509, 62)
        Me.lblDateTimeStamp.Name = "lblDateTimeStamp"
        Me.lblDateTimeStamp.Size = New System.Drawing.Size(140, 13)
        Me.lblDateTimeStamp.TabIndex = 86
        Me.lblDateTimeStamp.Text = "12:00:00           2013/01/01"
        '
        'tmrRefresh
        '
        Me.tmrRefresh.Interval = 10000
        '
        'lblRefresh
        '
        Me.lblRefresh.AutoSize = True
        Me.lblRefresh.Location = New System.Drawing.Point(779, 23)
        Me.lblRefresh.Name = "lblRefresh"
        Me.lblRefresh.Size = New System.Drawing.Size(90, 13)
        Me.lblRefresh.TabIndex = 10
        Me.lblRefresh.Text = "Refresh: 10 secs."
        '
        'chkAuto
        '
        Me.chkAuto.AutoSize = True
        Me.chkAuto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAuto.Location = New System.Drawing.Point(403, 28)
        Me.chkAuto.Name = "chkAuto"
        Me.chkAuto.Size = New System.Drawing.Size(81, 17)
        Me.chkAuto.TabIndex = 3
        Me.chkAuto.Text = "Auto Select"
        Me.chkAuto.UseVisualStyleBackColor = True
        '
        'treeView2
        '
        Me.treeView2.FullRowSelect = True
        Me.treeView2.HideSelection = False
        Me.treeView2.ImageIndex = 0
        Me.treeView2.ImageList = Me.ImageList1
        Me.treeView2.Location = New System.Drawing.Point(153, 117)
        Me.treeView2.Name = "treeView2"
        Me.treeView2.SelectedImageIndex = 0
        Me.treeView2.Size = New System.Drawing.Size(141, 273)
        Me.treeView2.TabIndex = 1
        '
        'treeView3
        '
        Me.treeView3.FullRowSelect = True
        Me.treeView3.HideSelection = False
        Me.treeView3.ImageIndex = 0
        Me.treeView3.ImageList = Me.ImageList1
        Me.treeView3.Location = New System.Drawing.Point(153, 396)
        Me.treeView3.Name = "treeView3"
        Me.treeView3.SelectedImageIndex = 0
        Me.treeView3.Size = New System.Drawing.Size(141, 190)
        Me.treeView3.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 92
        Me.Label4.Text = "Extruders"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(156, 97)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 93
        Me.Label5.Text = "Mixers"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.Blue
        Me.lblName.Location = New System.Drawing.Point(507, 28)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(88, 26)
        Me.lblName.TabIndex = 100
        Me.lblName.Text = "Name?"
        '
        'chkIPs
        '
        Me.chkIPs.AutoSize = True
        Me.chkIPs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIPs.Location = New System.Drawing.Point(403, 51)
        Me.chkIPs.Name = "chkIPs"
        Me.chkIPs.Size = New System.Drawing.Size(43, 17)
        Me.chkIPs.TabIndex = 4
        Me.chkIPs.Text = "IP's"
        Me.chkIPs.UseVisualStyleBackColor = True
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.TabPage1)
        Me.TabControl.Controls.Add(Me.TabPage2)
        Me.TabControl.Location = New System.Drawing.Point(339, 113)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(574, 478)
        Me.TabControl.TabIndex = 9
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.LightBlue
        Me.TabPage1.Controls.Add(Me.lstViewE)
        Me.TabPage1.Controls.Add(Me.lstViewF)
        Me.TabPage1.Controls.Add(Me.lstViewD)
        Me.TabPage1.Controls.Add(Me.lstViewC)
        Me.TabPage1.Controls.Add(Me.lstViewB)
        Me.TabPage1.Controls.Add(Me.lstViewA)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(566, 452)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Data"
        '
        'lstViewE
        '
        Me.lstViewE.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstViewE.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader9, Me.ColumnHeader10, Me.ColumnHeader11})
        Me.lstViewE.GridLines = True
        Me.lstViewE.Location = New System.Drawing.Point(403, 309)
        Me.lstViewE.Name = "lstViewE"
        Me.lstViewE.Scrollable = False
        Me.lstViewE.Size = New System.Drawing.Size(146, 90)
        Me.lstViewE.TabIndex = 111
        Me.lstViewE.UseCompatibleStateImageBehavior = False
        Me.lstViewE.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Axis"
        Me.ColumnHeader9.Width = 43
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Max"
        Me.ColumnHeader10.Width = 50
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "Min"
        Me.ColumnHeader11.Width = 50
        '
        'lstViewF
        '
        Me.lstViewF.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstViewF.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader7, Me.ColumnHeader8})
        Me.lstViewF.GridLines = True
        Me.lstViewF.Location = New System.Drawing.Point(399, 3)
        Me.lstViewF.Name = "lstViewF"
        Me.lstViewF.Scrollable = False
        Me.lstViewF.Size = New System.Drawing.Size(160, 300)
        Me.lstViewF.TabIndex = 110
        Me.lstViewF.UseCompatibleStateImageBehavior = False
        Me.lstViewF.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Source"
        Me.ColumnHeader7.Width = 112
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Data"
        Me.ColumnHeader8.Width = 47
        '
        'lstViewD
        '
        Me.lstViewD.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstViewD.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Axis, Me.Max, Me.Min})
        Me.lstViewD.GridLines = True
        Me.lstViewD.Location = New System.Drawing.Point(247, 309)
        Me.lstViewD.Name = "lstViewD"
        Me.lstViewD.Scrollable = False
        Me.lstViewD.Size = New System.Drawing.Size(146, 90)
        Me.lstViewD.TabIndex = 109
        Me.lstViewD.UseCompatibleStateImageBehavior = False
        Me.lstViewD.View = System.Windows.Forms.View.Details
        '
        'Axis
        '
        Me.Axis.Text = "Axis"
        Me.Axis.Width = 43
        '
        'Max
        '
        Me.Max.Text = "Max"
        Me.Max.Width = 50
        '
        'Min
        '
        Me.Min.Text = "Min"
        Me.Min.Width = 50
        '
        'lstViewC
        '
        Me.lstViewC.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstViewC.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader5, Me.ColumnHeader6})
        Me.lstViewC.GridLines = True
        Me.lstViewC.Location = New System.Drawing.Point(3, 309)
        Me.lstViewC.Name = "lstViewC"
        Me.lstViewC.Scrollable = False
        Me.lstViewC.Size = New System.Drawing.Size(228, 90)
        Me.lstViewC.TabIndex = 2
        Me.lstViewC.UseCompatibleStateImageBehavior = False
        Me.lstViewC.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Source"
        Me.ColumnHeader5.Width = 116
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Data"
        Me.ColumnHeader6.Width = 107
        '
        'lstViewB
        '
        Me.lstViewB.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstViewB.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lstViewB.GridLines = True
        Me.lstViewB.Location = New System.Drawing.Point(169, 3)
        Me.lstViewB.Name = "lstViewB"
        Me.lstViewB.Scrollable = False
        Me.lstViewB.Size = New System.Drawing.Size(224, 300)
        Me.lstViewB.TabIndex = 1
        Me.lstViewB.UseCompatibleStateImageBehavior = False
        Me.lstViewB.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Source"
        Me.ColumnHeader3.Width = 141
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Data"
        Me.ColumnHeader4.Width = 79
        '
        'lstViewA
        '
        Me.lstViewA.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstViewA.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lstViewA.GridLines = True
        Me.lstViewA.Location = New System.Drawing.Point(3, 3)
        Me.lstViewA.Name = "lstViewA"
        Me.lstViewA.Scrollable = False
        Me.lstViewA.Size = New System.Drawing.Size(160, 300)
        Me.lstViewA.TabIndex = 0
        Me.lstViewA.UseCompatibleStateImageBehavior = False
        Me.lstViewA.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Source"
        Me.ColumnHeader1.Width = 112
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Data"
        Me.ColumnHeader2.Width = 47
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.LightBlue
        Me.TabPage2.Controls.Add(Me.btnUpdate)
        Me.TabPage2.Controls.Add(Me.rtbStatus)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(566, 452)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Status"
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(7, 6)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(51, 25)
        Me.btnUpdate.TabIndex = 105
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'rtbStatus
        '
        Me.rtbStatus.BackColor = System.Drawing.Color.LightSteelBlue
        Me.rtbStatus.Location = New System.Drawing.Point(4, 38)
        Me.rtbStatus.Name = "rtbStatus"
        Me.rtbStatus.Size = New System.Drawing.Size(454, 363)
        Me.rtbStatus.TabIndex = 104
        Me.rtbStatus.Text = ""
        '
        'pnlError
        '
        Me.pnlError.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlError.Controls.Add(Me.btnErrorReset)
        Me.pnlError.Controls.Add(Me.lblError)
        Me.pnlError.Controls.Add(Me.rtbError)
        Me.pnlError.Location = New System.Drawing.Point(12, 576)
        Me.pnlError.Name = "pnlError"
        Me.pnlError.Size = New System.Drawing.Size(877, 10)
        Me.pnlError.TabIndex = 107
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
        'tmrLoad
        '
        '
        'tmrUDP
        '
        Me.tmrUDP.Interval = 500
        '
        'lblUDP
        '
        Me.lblUDP.BackColor = System.Drawing.Color.Transparent
        Me.lblUDP.BackgroundImage = Global.mimics.My.Resources.Resources.check_in_II
        Me.lblUDP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.lblUDP.Location = New System.Drawing.Point(875, 40)
        Me.lblUDP.Name = "lblUDP"
        Me.lblUDP.Size = New System.Drawing.Size(38, 32)
        Me.lblUDP.TabIndex = 13
        Me.lblUDP.UseVisualStyleBackColor = False
        Me.lblUDP.Visible = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefresh.ImageList = Me.ImageList1
        Me.btnRefresh.Location = New System.Drawing.Point(339, 23)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(55, 49)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(875, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(38, 34)
        Me.btnClose.TabIndex = 12
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmMonitor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(913, 590)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblUDP)
        Me.Controls.Add(Me.pnlError)
        Me.Controls.Add(Me.lblDateTimeStamp)
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.chkIPs)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.treeView3)
        Me.Controls.Add(Me.treeView2)
        Me.Controls.Add(Me.chkAuto)
        Me.Controls.Add(Me.lblRefresh)
        Me.Controls.Add(Me.lblDisconnect)
        Me.Controls.Add(Me.lblIdent1)
        Me.Controls.Add(Me.treeView1)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblIP)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TreeViewKey)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMonitor"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Mimics Monitor"
        Me.TabControl.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.pnlError.ResumeLayout(False)
        Me.pnlError.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents TreeViewKey As System.Windows.Forms.TreeView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblIP As System.Windows.Forms.Label
    Private WithEvents btnClose As System.Windows.Forms.Button
    Private WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents treeView1 As System.Windows.Forms.TreeView
    Friend WithEvents lblIdent1 As System.Windows.Forms.Label
    Friend WithEvents lblDisconnect As System.Windows.Forms.Label
    Friend WithEvents lblDateTimeStamp As System.Windows.Forms.Label
    Friend WithEvents tmrRefresh As System.Windows.Forms.Timer
    Friend WithEvents lblRefresh As System.Windows.Forms.Label
    Friend WithEvents chkAuto As System.Windows.Forms.CheckBox
    Friend WithEvents treeView2 As System.Windows.Forms.TreeView
    Friend WithEvents treeView3 As System.Windows.Forms.TreeView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents chkIPs As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents lstViewC As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstViewB As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstViewA As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents rtbStatus As System.Windows.Forms.RichTextBox
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents pnlError As System.Windows.Forms.Panel
    Friend WithEvents btnErrorReset As System.Windows.Forms.Button
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents rtbError As System.Windows.Forms.RichTextBox
    Friend WithEvents lstViewD As System.Windows.Forms.ListView
    Friend WithEvents Axis As System.Windows.Forms.ColumnHeader
    Friend WithEvents Max As System.Windows.Forms.ColumnHeader
    Friend WithEvents Min As System.Windows.Forms.ColumnHeader
    Friend WithEvents tmrLoad As System.Windows.Forms.Timer
    Friend WithEvents tmrUDP As System.Windows.Forms.Timer
    Friend WithEvents lblUDP As System.Windows.Forms.Button
    Friend WithEvents lstViewF As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstViewE As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
End Class
