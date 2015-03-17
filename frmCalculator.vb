Imports System.Windows.Forms.DataVisualization.Charting

Public Class frmCalculator

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim intCount As Integer = CInt(Me.txtCount.Text)
        Dim oldData As Integer = CInt(Me.txtOld.Text)
        Dim newData As Integer = CInt(Me.txtNext.Text)
        Dim Result As Double

        If (intCount = 0) Then intCount = 2
        If (oldData = 0) Then oldData = newData
        Result = (((intCount - 1) * oldData) + newData) / intCount
        Me.lblResult.Text = CStr(Result)

        'Exponential moving average
        Dim K As Double = (2 / (1 + intCount))
        Dim EMA As Double = (K * (newData - oldData)) + oldData
        Me.lblResult2.Text = CStr(EMA)

        'Where:
        'X = Current EMA (i.e. EMA to be calculated) //EMA
        'C = Current original data value             //newData
        'K = Smoothing Constant                      //K
        'P = Previous EMA                            //oldData
        'K = Smoothing Constant = 2 / (1 + n)        //K

        intCount = intCount + 1
        Me.txtCount.Text = CStr(intCount)
        Me.txtOld.Text = CStr(Result)
        Me.txtNext.Text = CStr(CInt(Me.txtNext.Text) + 1)
        Me.txtNext.Focus()

    End Sub
    Private Function movAvg(ByVal oldAverage As Double, ByVal count As Integer, ByVal newValue As Double) As Double

        Dim n As Integer = count - 1
        Return (oldAverage * (n - 1) / n + newValue / n)

    End Function
    Private Function EMAmovAvg(ByVal oldAverage As Double, ByVal count As Integer, ByVal newValue As Double) As Double

        Dim K As Double = (2 / (1 + count))
        Dim EMA As Double = (K * (newValue - oldAverage)) + oldAverage

    End Function

    'Dim intCount As Integer = CInt(Me.txtCount.Text)
    'Dim Average As double
    'Dim oldCount As Integer = intCount - 1
    'Dim nextCount As Integer = intCount
    'Dim oldData As Integer = CInt(Me.txtOld.Text)
    'Dim nextData As Integer = CInt(Me.txtNext.Text)

    '    Average = ((oldCount * oldData) + nextData) / nextCount
    '    Me.lblResult.Text = CStr(Average)
    '    Me.lblResult2.Text = CStr(movAvg(Average, intCount, nextData))

    '    intCount = intCount + 1
    '    Me.txtCount.Text = CStr(intCount)
    '    Me.txtOld.Text = CStr(Average)
    '    Me.txtNext.Text = Nothing
    '    Me.txtNext.Focus()

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Try
            Dim n As Integer
            Dim intNewData As Integer
            Dim RandomClass As New Random()
            Dim intStartData = 200
            Dim intResult As Integer = 0
            Dim K As Double
            Dim EMA As Double

            For n = 1 To 255
                intNewData = RandomClass.Next(80, 160)
                intResult = ((((n - 1) * intStartData) + intNewData) / n)
                K = (2 / (1 + n))
                EMA = (K * (intNewData - intStartData)) + intStartData
                intStartData = intResult
                Me.lblResult2.Text = CStr(intResult)
                Me.lblResult3.Text = CStr(EMA)
            Next



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim oldData As Integer = CInt(Me.txtOld.Text)
        Dim newData As Integer = CInt(Me.txtNext.Text)

        If (newData >= oldData) Then
            If (((newData - oldData) / oldData) * 100 > 3) Then
                newData = ((oldData * 103) / 100)
                Me.txtNext.Text = CStr(newData)
            End If
        Else
            If (((oldData - newData) / oldData) * 100 > 3) Then
                newData = ((oldData * 97) / 100)
                Me.txtNext.Text = CStr(newData)
            End If
        End If

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'Me.Chart1.Series("AGE").Points.AddXY("Mark", 33)

        Chart1.Series.Clear()
        Chart1.Titles.Add("CMM Mimics trend monitoring")
        'Create a new series and add data points to it.
        Dim s As New Series
        s.Name = "Px"
        'Change to a line graph.
        s.ChartType = SeriesChartType.Line
        s.Points.AddXY("1990", 27)
        s.Points.AddXY("1991", 15)
        s.Points.AddXY("1992", 17)
        'Add the series to the Chart1 control.
        Chart1.Series.Add(s)
    End Sub
End Class