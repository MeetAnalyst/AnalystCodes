Imports System.Net
Imports System.Data
Imports Newtonsoft.Json
Imports System.IO
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.Filter = "Json Files|*.Json"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Multiselect = False
        'OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            TextBox1.Text = OpenFileDialog1.FileName
            Dim jSON As String = File.ReadAllText(TextBox1.Text)
            Dim dS As DataTable = JsonConvert.DeserializeObject(Of DataTable)(jSON)

            If dS.Rows.Count > 0 Then
                DataGridView1.DataSource = dS

            Else
                DataGridView1.Rows.Clear()
            End If

        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '    Dim jSON As String = File.ReadAllText("C:\Users\User\OneDrive - PENVER PRODUCTS LTD\Desktop\Scanner.Json")
        '    Dim dS As DataTable = JsonConvert.DeserializeObject(Of DataTable)(jSON)

        '    If dS.Rows.Count > 0 Then
        '        DataGridView1.DataSource = dS

        '    Else
        '        DataGridView1.Rows.Clear()
        '        End If

    End Sub
End Class
