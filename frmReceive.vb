﻿Imports System.Net
Imports System.Net.Sockets

Imports System.IO
Imports MySql.Data
Imports MySql.Data.MySqlClient

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Threading
Imports System.Reflection
Imports System.Windows.Forms.DataVisualization.Charting

Public Class frmReceive

    Private inc As Integer = 1
    Private intPollIndex As Integer

    Private MySqlConRx As MySqlConnection

    Private dbset As New DataSet
    Private dbTable As DataTable

    Private intDGscrollIndex As Integer
    Private blnDGscroll As Boolean = False
    Private blnEnableRadioButtons As Boolean = False

    Private blnEnableChart = False
    Private inRecurs As Boolean = False

    Private Sub frmReceive_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Width = 1382
            Me.Height = 478

            btnReceive = fixButtonImage(Me.btnReceive)
            btnReset = fixButtonImage(Me.btnReset)
            btnExit = fixButtonImage(Me.btnExit)
            btnGraph = fixButtonImage(Me.btnGraph)

            ToolStrip1.ImageScalingSize = New Size(40, 40)

            Dim MysqlConnString As String = GetIni("MysqlConnString")
            MySqlConRx = New MySqlClient.MySqlConnection(MysqlConnString)
            'MySqlConRx.Open()

            lblListening.Visible = False
            lblListening_2.Visible = False

            tmrPoll.Enabled = False
            tmrLabel.Enabled = True
            tmrDGsort.Enabled = True

            Dim strTable As String = "cmm.tblmimics_holding"
            If Me.chkData.Checked = True Then strTable = "cmm.tblmimics_holding"
            If Me.chkStatus.Checked = True Then strTable = "cmm.tblmimics_status"

            Me.lblDBsize.Text = "MySQL main database size: " & dbSize() & " MB"
            Me.lblRecords.Text = dbRecords(strTable)

            populateCombo() 'This will trigger FillDataGrid()
            Me.cmbIP.Text = "All ip's"

            blnEnableChart = False

            Me.txtRecordCount.Text = 3600
            Me.cmbSource_1.Text = "A6"
            Me.cmbSource_2.Text = "A8"
            Me.scale_1.Text = "100"
            Me.scale_2.Text = "100"
            initChart()

            blnEnableChart = True

        Catch ex As Exception
            errorPanelsource("Form load")
            errorPanel(ex.Message)
        End Try

    End Sub
    Private Sub frmReceive_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try
            If (MySqlConRx.State <> ConnectionState.Closed) Then MySqlConRx.Close()
        Catch ex As Exception
            errorPanelsource("Form closed")
            errorPanel(ex.Message)
        End Try

    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click

        DG.DataSource = Nothing
        Me.lblDBsize.Text = Nothing
        Me.lblRecords.Text = Nothing

    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnReceive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceive.Click
        Try
            If Me.btnReceive.Checked = True Then
                tmrPoll.Enabled = True
                tmrLabel.Enabled = False
                lblListening.Visible = True
                lblListening_2.Visible = True
                lblNotListening.Visible = False
                lblNotListening_2.Visible = False
            ElseIf Me.btnReceive.Checked = False Then
                tmrPoll.Enabled = False
                tmrLabel.Enabled = True
                lblListening.Visible = False
                lblListening_2.Visible = False
                lblNotListening.Visible = True
                lblNotListening_2.Visible = True
            End If
        Catch ex As Exception
            errorPanelsource("Receive btn")
            errorPanel(ex.Message)
        End Try
    End Sub
    Private Sub DTP_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTP.ValueChanged
        Try
            tmrPoll.Enabled = False
            tmrLabel.Enabled = True
            lblListening.Visible = False
            lblNotListening.Visible = True
            lblListening_2.Visible = False
            lblNotListening_2.Visible = True

            Dim strResult As String = CDate(DTP.Value).ToString("yyyy-MM-dd")
            FillDataGrid(strResult)

        Catch ex As Exception
            errorPanelsource("DTP_valueChanged")
            errorPanel(ex.Message)
        End Try

    End Sub
    Private Sub FillDataGrid(ByVal strDate As String)
        Try

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Dim strTable As String = "cmm.tblmimics_holding"
            If Me.chkData.Checked = True Then strTable = "cmm.tblmimics_holding"
            If Me.chkStatus.Checked = True Then strTable = "cmm.tblmimics_status"

            Dim strIP As String
            If (Me.cmbIP.Text.IndexOf("All") > -1 Or Len(Me.cmbIP.Text) = 0) Then
                sql = "SELECT * FROM " & strTable & " WHERE (datDate = '" & strDate & "')"
            Else
                sql = "SELECT * FROM " & strTable & " WHERE (datDate = '" & strDate & "') AND strDevice = '" & Me.cmbIP.Text & "'"
            End If

            If (MySqlConRx.State <> ConnectionState.Open) Then MySqlConRx.Open()
            Dim dbAdap As New MySqlDataAdapter(sql, MySqlConRx)
            
            dbAdap.SelectCommand.CommandText = sql
            Dim cmdBld As New MySqlCommandBuilder(dbAdap)

            dbset.Tables.Clear()
            dbAdap.Fill(dbset, "tblCMM")
            dbTable = dbset.Tables("tblCMM")

            'Get the highest index to start polling from
            sql = "SELECT Max(ID) FROM " & strTable
            Dim sqlCmd As New MySqlCommand(sql, MySqlConRx)
            intPollIndex = CInt(sqlCmd.ExecuteScalar().ToString)
            sqlCmd = Nothing

            'Bind the DataSet to the DataGrid using the DataGrid's DataSource property.
            DG.DataSource = dbset.Tables("tblCMM")
            DG.Columns(0).Width = 50
            DG.Columns(1).Width = 70
            DG.Columns(2).Width = 55
            DG.Columns(3).Width = 80
            DG.Columns(0).HeaderText = "ID"
            DG.Columns(1).HeaderText = "Date"
            DG.Columns(2).HeaderText = "Time"
            DG.Columns(3).HeaderText = "Device IP"
            If Me.chkData.Checked = True Then
                Dim n As Integer
                For n = 4 To 20
                    DG.Columns(n).Width = 35
                Next
                For n = 21 To 44
                    DG.Columns(n).Width = 26
                    DG.Columns(n).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                Next
                'Accelerometer axis
                For n = 48 To 59
                    DG.Columns(n).Width = 49
                    DG.Columns(n).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                Next
                'Digital outputs
                For n = 25 To 32
                    DG.Columns(n).HeaderText = "O" & CStr(n - 24)
                Next
                'Digital inputs
                For n = 37 To 44
                    DG.Columns(n).HeaderText = "In" & CStr(n - 36)
                Next
                'Dim column As DataGridViewColumn = DataGridView.Columns(0)
                'column.Width = 60
            ElseIf Me.chkStatus.Checked = True Then
                DG.Columns(4).HeaderText = "Status"
                DG.Columns(4).Width = 528
            End If

            'Scroll to the last row of the DG
            'If on form load it will not work - the Timer3 will do it 500mS later
            intDGscrollIndex = DG.RowCount - 1
            DG.FirstDisplayedScrollingRowIndex = intDGscrollIndex

            Me.lblRecords.Text = dbRecords(strTable)

            System.Windows.Forms.Cursor.Current = Cursors.Default

            If (MySqlConRx.State <> ConnectionState.Closed) Then MySqlConRx.Close()

        Catch ex As Exception
            If (MySqlConRx.State <> ConnectionState.Closed) Then MySqlConRx.Close()
            errorPanelsource("FillDataGrid")
            errorPanel(ex.Message)
        End Try

    End Sub
    Private Sub pollMySql()

        Try
            Dim strTable As String = "cmm.tblmimics_holding"
            If Me.chkData.Checked = True Then strTable = "cmm.tblmimics_holding"
            If Me.chkStatus.Checked = True Then strTable = "cmm.tblmimics_status"

            Dim strIP As String
            If (Me.cmbIP.Text.IndexOf("All") > -1 Or Len(Me.cmbIP.Text) = 0) Then
                sql = "SELECT * FROM " & strTable & " WHERE (ID > '" & intPollIndex & "')"
            Else
                sql = "SELECT * FROM " & strTable & " WHERE (ID > '" & intPollIndex & "') AND strDevice = '" & Me.cmbIP.Text & "'"
            End If

            'First import all new rows to a new data table in the dataset
            If (MySqlConRx.State <> ConnectionState.Open) Then MySqlConRx.Open()
            Dim dbAdap As New MySqlDataAdapter(sql, MySqlConRx)

            dbAdap.SelectCommand.CommandText = sql
            Dim cmdBld As New MySqlCommandBuilder(dbAdap)

            dbAdap.Fill(dbset, "tblNew")
            dbTable = dbset.Tables("tblNew")
            dbAdap = Nothing
            
            'Bind the DataSet to the DataGrid using the DataGrid's DataSource property. 
            DG.DataSource = dbset.Tables("tblNew")
            DG.FirstDisplayedScrollingRowIndex = DG.RowCount - 1

            'Set a new poll index for the next poll
            sql = "SELECT Max(ID) FROM " & strTable
            Dim sqlCmd As New MySqlCommand(sql, MySqlConRx)
            intPollIndex = CInt(sqlCmd.ExecuteScalar().ToString)
            sqlCmd = Nothing
            If (MySqlConRx.State <> ConnectionState.Closed) Then MySqlConRx.Close()

            lblListening.Visible = Not lblListening.Visible
            lblListening_2.Visible = Not lblListening_2.Visible

            Me.lblDBsize.Text = "MySQL database size: " & dbSize() & " MB"
            Me.lblRecords.Text = dbRecords(strTable)

        Catch ex As Exception
            If (MySqlConRx.State <> ConnectionState.Closed) Then MySqlConRx.Close()
            errorPanelsource("PollMySql")
            errorPanel(ex.Message)
        End Try

    End Sub

    Private Sub tmrPoll_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrPoll.Tick
        Try
            pollMySql()

        Catch ex As Exception
            errorPanelsource("tmrPoll")
            errorPanel(ex.Message)
        End Try

    End Sub

    Private Sub tmrLabel_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrLabel.Tick
        Try
            lblNotListening.Visible = Not lblNotListening.Visible
            lblNotListening_2.Visible = Not lblNotListening_2.Visible
        Catch ex As Exception
            errorPanelsource("tmrLabel")
            errorPanel(ex.Message)
        End Try

    End Sub

    Private Sub tmrDGsort_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDGsort.Tick
        Try
            If (intDGscrollIndex > 0) Then DG.FirstDisplayedScrollingRowIndex = intDGscrollIndex
            tmrDGsort.Enabled = False
            blnEnableRadioButtons = True
        Catch ex As Exception
            errorPanelsource("tmrDGsort")
            errorPanel(ex.Message)
        End Try

    End Sub
    Private Sub populateCombo()

        Try
            Dim x As Integer
            Me.cmbIP.Items.Clear()
            Me.cmbIP.Items.Add("All ip's")
            For x = 0 To 50
                Me.cmbIP.Items.Add(strSubnet & CStr(baseIP + x))
                'Me.cmbIP_2.Items.Add(strSubnet & CStr(baseIP + x))
            Next

        Catch ex As Exception
            errorPanelsource("populateCombo")
            errorPanel(ex.Message)
        End Try
    End Sub

    Private Sub cmbIP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbIP.SelectedIndexChanged
        Dim strResult As String = CDate(DTP.Value).ToString("yyyy-MM-dd")
        FillDataGrid(strResult)
    End Sub

    Private Sub chkData_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkData.CheckedChanged
        If blnEnableRadioButtons = False Then Exit Sub
        Dim strResult As String = CDate(DTP.Value).ToString("yyyy-MM-dd")
        FillDataGrid(strResult)
    End Sub

    Private Sub chkStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStatus.CheckedChanged
        If blnEnableRadioButtons = False Then Exit Sub
        Dim strResult As String = CDate(DTP.Value).ToString("yyyy-MM-dd")
        FillDataGrid(strResult)
    End Sub

    Private Sub btnErrorReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnErrorReset.Click
        Me.rtbError.Text = Nothing
        Me.pnlError.Visible = False
    End Sub
    Private Sub errorPanelsource(ByVal strMsg As String)
        rtbError.SelectionColor = Color.Blue
        rtbError.SelectedText = strMsg & vbCrLf
        rtbError.SelectionStart = rtbError.Text.Length
        rtbError.ScrollToCaret()
    End Sub
    Private Sub errorPanel(ByVal strMsg As String)
        rtbError.SelectionColor = Color.Red
        rtbError.SelectedText = strMsg & vbCrLf
        rtbError.SelectionStart = rtbError.Text.Length
        rtbError.ScrollToCaret()
        pnlError.Visible = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        tmrChart.Enabled = False
        Me.pnlGraph.Visible = False
        Me.Width = 1382
        Me.Height = 478
    End Sub

    Private Sub initChart()
        Try

            'Initialise chart area
            Dim area As New ChartArea
            area = Chart1.ChartAreas(0)
            area.AxisY.Interval = 25
            area.AxisX.MajorGrid.Enabled = True
            area.AxisX.IntervalType = DateTimeIntervalType.Seconds
            area.AxisX.LabelStyle.Format = "HH:mm:ss"

            'Clear out any series added ar design time
            Chart1.Series.Clear()

            ''Add the different series we want to see
 
            Dim seriesOne As New Series
            seriesOne.Name = Me.cmbSource_1.Text
            seriesOne.BorderWidth = 2
            seriesOne.Color = Color.Black
            seriesOne.ChartType = SeriesChartType.Line
            seriesOne.XValueType = ChartValueType.DateTime
            Chart1.Series.Add(seriesOne)

            Dim seriesTwo As New Series
            seriesTwo.Name = Me.cmbSource_2.Text
            seriesTwo.BorderWidth = 2
            seriesTwo.Color = Color.Red
            seriesTwo.ChartType = SeriesChartType.Line
            seriesTwo.XValueType = ChartValueType.DateTime
            Chart1.Series.Add(seriesTwo)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub refreshChart()

        If (blnEnableChart = False) Then Exit Sub
        If (Me.cmbIP.Text = "All ip's") Then Exit Sub
        Try
            If (Len(Me.txtRecordCount.Text) = 0) Then Exit Sub

            If (Me.txtMax.Text <> "Auto" And Len(Me.txtMax.Text) > 0) Then
                Chart1.ChartAreas(0).AxisY.Maximum = CInt(Me.txtMax.Text)
            End If

            If (Me.txtMax.Text = "Auto") Then
                Chart1.ChartAreas(0).AxisY.Maximum = Double.NaN
            End If

            '************************************************************************************
            'Get max ID number in order to set the start point
            If (MySqlConRx.State <> ConnectionState.Open) Then MySqlConRx.Open()

            sql = "SELECT Max(ID) FROM cmm.tblmimics"
            Dim sqlCmd As New MySqlCommand(sql, MySqlConRx)
            Dim maxID As Integer = CInt(sqlCmd.ExecuteScalar().ToString)
            sqlCmd = Nothing

            'Only by changing the record count will the Y axis change
            If (inc = 1) Then
                inc = -1
            Else
                inc = 1
            End If
            Dim startID As Integer = (maxID - CInt(Me.txtRecordCount.Text) + inc)
            sql = "SELECT  * FROM cmm.tblmimics WHERE ID > " & startID & " AND strDevice = '" & Me.cmbIP.Text & "' ORDER BY ID"
           
            Dim dbAdap As New MySqlDataAdapter(sql, MySqlConRx)
            dbAdap.SelectCommand.CommandText = sql
            Dim cmdBld As New MySqlCommandBuilder(dbAdap)
            Dim dataSet As New DataSet
            dbAdap.Fill(dataSet, "tblChart")
            Dim tbl As New DataTable
            tbl = dataSet.Tables("tblChart")
            dbAdap = Nothing
            If (MySqlConRx.State <> ConnectionState.Closed) Then MySqlConRx.Close()

            Dim n, intY_1, intY_2 As Integer
            Dim lngX As Long
            Dim dblTime As Double
            Dim tsTime As TimeSpan
            Dim startTime As TimeSpan
            Dim datTime As DateTime
            Dim strTime As String

            'Clear the points for the series about to be redrawn
            Dim srs As Series
            For Each srs In Chart1.Series
                If srs.Name = Me.cmbSource_1.Text Then srs.Points.Clear()
                If srs.Name = Me.cmbSource_2.Text Then srs.Points.Clear()
            Next

            Dim row As DataRow
            For Each row In tbl.Rows
                tsTime = row.Item("strTime")
                If (n = 0) Then startTime = tsTime
                dblTime = Math.Round((tsTime.Hours) + (tsTime.Minutes / 60) + (tsTime.Seconds / 3600), 1)
                datTime = CDate(printDigits(tsTime.Hours.ToString) & ":" & printDigits(tsTime.Minutes.ToString) & ":" & printDigits(tsTime.Seconds.ToString))

                lngX = CLng(row.Item("ID"))
                intY_1 = CInt(row.Item(Me.cmbSource_1.Text))
                intY_2 = CInt(row.Item(Me.cmbSource_2.Text))
                intY_1 = intY_1 * (CInt(scale_1.Text) / 100)
                intY_2 = intY_2 * (CInt(scale_2.Text) / 100)
                For Each srs In Chart1.Series
                    If srs.Name = Me.cmbSource_1.Text Then srs.Points.AddXY(datTime, intY_1)
                    If srs.Name = Me.cmbSource_2.Text Then srs.Points.AddXY(datTime, intY_2)
                Next


                'If (inc = 1) Then s.Points.AddXY(lngX, intY)
                'If (inc = -1) Then s.Points.AddXY(dblTime, intY)
                n = n + 1
            Next

            Dim strStartTime, strEndTime As String
            strStartTime = printDigits(startTime.Hours.ToString) & ":" & printDigits(startTime.Minutes.ToString) & ":" & printDigits(startTime.Seconds.ToString)
            strEndTime = printDigits(tsTime.Hours.ToString) & ":" & printDigits(tsTime.Minutes.ToString) & ":" & printDigits(tsTime.Seconds.ToString)
            lblPeriod.Text = strStartTime & " - " & strEndTime
            '************************************************************************************

        Catch ex As Exception
            If (MySqlConRx.State <> ConnectionState.Closed) Then MySqlConRx.Close()
            errorPanelsource("refreshChart")
            errorPanel(ex.Message)
        End Try
    End Sub
    Private Sub cmbSource_2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSource_1.SelectedIndexChanged
        initChart()
        refreshChart()
    End Sub
    Private Sub txtRecordCount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRecordCount.TextChanged
        refreshChart()
    End Sub
    Private Sub tmrChart_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrChart.Tick
        refreshChart()
    End Sub
    Private Function printDigits(ByVal strValue As String) As String
        If Len(strValue) = 1 Then Return "0" & strValue
        Return strValue
    End Function
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.txtMax.Text = "Auto"
    End Sub
    Private Sub btnGraph_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraph.Click
        refreshChart()
        Me.pnlGraph.Visible = True
        Me.Width = 1382
        Me.Height = 767
        tmrChart.Enabled = True
    End Sub
    Private Sub TrackBar1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TrackBar1.ValueChanged
        Me.txtRecordCount.Text = TrackBar1.Value.ToString
    End Sub

    Private Sub cmbSource_2_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSource_2.SelectedIndexChanged
        initChart()
        refreshChart()
    End Sub
    Private Function dbSize()

        Try
            sql = "SELECT sum(data_length + index_length) / 1024 / 1024 'dbSize'" _
            & " FROM information_schema.TABLES" _
            & " GROUP BY table_schema;"

            If (MySqlConRx.State <> ConnectionState.Open) Then MySqlConRx.Open()
            Dim sqlCmd As New MySqlCommand(sql, MySqlConRx)
            Dim strResult As String = sqlCmd.ExecuteScalar().ToString
            sqlCmd = Nothing
            If (MySqlConRx.State <> ConnectionState.Closed) Then MySqlConRx.Close()


            Return strResult

        Catch ex As Exception
            errorPanelsource("dbSize")
            errorPanel(ex.Message)
        End Try

    End Function

    Private Function dbRecords(ByVal strTable As String)

        Try

            sql = "SELECT COUNT(*) FROM " & strTable & ";"

            If (MySqlConRx.State <> ConnectionState.Open) Then MySqlConRx.Open()

            Dim sqlCmd As New MySqlCommand(sql, MySqlConRx)
            Dim strResult As String = sqlCmd.ExecuteScalar().ToString
            sqlCmd = Nothing
            If (MySqlConRx.State <> ConnectionState.Closed) Then MySqlConRx.Close()

            Return strResult & " records in holding table."

        Catch ex As Exception
            errorPanelsource("dbRecords")
            errorPanel(ex.Message)
        End Try

    End Function

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll

    End Sub

    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub Chart1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chart1.Click

    End Sub

    Private Sub cmbIP_2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class