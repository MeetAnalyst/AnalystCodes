Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Security.Cryptography
Imports System.Text
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Windows.Forms



Public Class CONTROLS
    ReadOnly con As New SqlConnection With {
    .ConnectionString = ConfigurationManager.ConnectionStrings("SqlCon").ConnectionString.ToString
    }

    Public Function GetDataInDataset(ByVal Query As String) As DataSet

        Dim ds As New DataSet
        Try
            Dim cmd As New SqlCommand(Query, con)
            cmd.CommandType = CommandType.Text
            Dim adp As New SqlDataAdapter With {
                    .SelectCommand = cmd
                    }
            adp.Fill(ds)

        Catch ex As Exception
            MsgBox("No Data Fetched", MsgBoxStyle.ApplicationModal, "Inspector")

        End Try
        Return ds
    End Function
    Public Function GetDataInDataTable(ByVal Query As String) As DataTable
        Dim Dt As New DataTable
        Try

            Dim cmd As New SqlCommand(Query, con) With {
                .CommandType = CommandType.Text
            }
            Dim adp As New SqlDataAdapter With {
                .SelectCommand = cmd
                }
            adp.Fill(Dt)
        Catch EX As Exception
            MsgBox("No Data Fetched", MsgBoxStyle.ApplicationModal, "Inspector")

        End Try
        Return Dt
    End Function

    Public Function TextQueryRunner(ByVal Query As String)
        Dim t As Integer
        Try
            Dim cmd As New SqlCommand(Query, con) With {
                .CommandType = CommandType.Text
            }
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            t = 0
        Catch ex As Exception
            t = 1
        End Try
        Return t
    End Function
    Public Function ComboboxSourceAdd(ByVal DispM As String, ByVal ValM As String, WhereCondition As String, List As ComboBox, ByVal Tabl As String)
        Try

            Dim query As String = "select distinct " & DispM & "," & ValM & " from " & Tabl & " where " & WhereCondition & DispM & "  is not null and " & ValM & " is not null "
            Dim dt As New DataTable
            Dim cmd As New SqlCommand(query, con)
            Dim adp As New SqlDataAdapter
            adp.SelectCommand = cmd
            adp.Fill(dt)
            List.DataSource = dt
            List.DisplayMember = DispM
            List.ValueMember = DispM


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return 0
    End Function

End Class


Public Class SecurityControl
    ReadOnly con As New SqlConnection With {
    .ConnectionString = ConfigurationManager.ConnectionStrings("SqlCon").ConnectionString.ToString
    }

    Private Function Encrypt(ByVal Data As String) As String
        Dim sha512 As New SHA512Managed
        Convert.ToBase64String(sha512.ComputeHash(Encoding.ASCII.GetBytes(Data)))
        Dim eNC_data() As Byte = ASCIIEncoding.ASCII.GetBytes(Data)
        Dim eNC_str As String = Convert.ToBase64String(eNC_data)
        Encrypt = eNC_str
    End Function
    Private Function Decrypt(ByVal Data As String) As String
        Dim dEC_data() As Byte = Convert.FromBase64String(Data)
        Dim dEC_Str As String = ASCIIEncoding.ASCII.GetString(dEC_data)
        Decrypt = dEC_Str
    End Function
    Public Function CreateUser(ByVal User As String, Pass As String)
        Dim usr As String = Encrypt(User)
        Dim pss As String = Encrypt(Pass)
        Dim CMD As SqlCommand = New SqlCommand("iNSERT INTO GatePass(Usr,Pss) values('" & usr & "','" & pss & "')", con) With {
        .CommandType = CommandType.Text
    }
        con.Open()
        CMD.ExecuteNonQuery()
        con.Close()
        Return 0

    End Function
    Public Function Loggin(ByVal user As String, ByVal pass As String)
        Dim t As Integer
        Try
            Dim usr As String = Encrypt(user)
            Dim pss As String = Encrypt(pass)

            Dim CMD As SqlCommand = New SqlCommand("Select * From GatePass where usr = '" & usr & "' and pss = '" & pss & "'", con)
            Dim DT As New DataTable
            CMD.CommandType = CommandType.Text
            Dim adp As New SqlDataAdapter With {
                .SelectCommand = CMD
            }

            adp.Fill(DT)
            If DT.Rows.Count = 1 Then
                Dim ussr As String = Decrypt(DT.Rows(0)(1))
                Dim pssd As String = Decrypt(DT.Rows(0)(2))
                If ussr = user And pssd = pass Then
                    'MsgBox("Logged in")
                    t = 0

                Else
                    t = 1
                End If

            ElseIf DT.Rows.Count = 0 Then
                'MsgBox("Incorrect UserId or Password")
                t = 2

            ElseIf DT.Rows.Count > 1 Then
                'MsgBox("Multiple User Accounts Found unable to verify User")
                t = 3

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return t
    End Function
End Class
Public Class ExcelFunctions
        Dim address As String
        Public Sub ExportDataGrid(DGv As DataGridView)
        Dim savedialog As New SaveFileDialog With {
            .Filter = "Excel Files|*.xlsx"
        }

        If savedialog.ShowDialog() = DialogResult.OK Then
                address = savedialog.FileName.ToString
            End If

            Try

                Dim xlApp As New Excel.Application
                Dim xlWorkBook As Excel.Workbook
                Dim xlWorkSheet As Excel.Worksheet
                Dim misValue As Object = System.Reflection.Missing.Value
                Dim i As Integer
                Dim j As Integer

                xlWorkBook = xlApp.Workbooks.Add(misValue)
                xlWorkSheet = xlWorkBook.Sheets("sheet1")

                For Each col As DataGridViewColumn In DGv.Columns
                    xlWorkSheet.Cells(1, col.Index + 1) = col.HeaderText.ToString

                Next



                For i = 0 To DGv.Rows.Count - 2
                    For j = 0 To DGv.Columns.Count - 1
                        xlWorkSheet.Cells(i + 2, j + 1) =
                        DGv(j, i).Value.ToString()
                    Next
                Next

                For Each col As DataGridViewColumn In DGv.Columns
                    xlWorkSheet.Cells(1, col.Index + 1).Font.Bold = True
                    xlWorkSheet.Cells(1, col.Index + 1).EntireColumn.AutoFit()
                    xlWorkSheet.Cells(1, col.Index + 1).HorizontalAlignment = Excel.Constants.xlCenter
                Next


                xlWorkSheet.SaveAs(address.ToString)
                xlWorkBook.Close()
                xlApp.Quit()

            ReleaseObject(xlApp)
            ReleaseObject(xlWorkBook)
            ReleaseObject(xlWorkSheet)

            MsgBox("You can find the file" + address.ToString)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ReleaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

End Class
