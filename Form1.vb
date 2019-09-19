Imports System.Runtime.Serialization
Imports System.IO
Public Class Form1
    'serializing the object
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim srObj As New SerializeObject()
            srObj.FirstName = TextBox1.Text.ToString
            srObj.LastName = TextBox2.Text.ToString
            srObj.Email = TextBox3.Text.ToString
            If (TextBox4.Text = "") Then
                srObj.srInt = 0
            Else
                srObj.srInt = TextBox4.Text
            End If
            Dim formatter As IFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            Dim fileStream As Stream = New FileStream("C:\Users\User\source\repos\Object Serilization\Object Serilization\bin\Debug\Bin\AppData\SerializeFile.bin", FileMode.Create, FileAccess.Write, FileShare.None)
            formatter.Serialize(fileStream, srObj)
            fileStream.Close()
            MsgBox("Object Serialized !!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    ' De-serializing the object
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim formatter As IFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        Dim serialStream As Stream = New FileStream("C:\Users\User\source\repos\Object Serilization\Object Serilization\bin\Debug\Bin\AppData\SerializeFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim srObj As SerializeObject = DirectCast(formatter.Deserialize(serialStream), SerializeObject)
        serialStream.Close()
        MsgBox(srObj.FirstName + " " + srObj.LastName.ToString())

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As EventArgs) Handles Button2.Click
        Try


            Dim formatter As IFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            Dim serialStream As Stream = New FileStream("C:\Users\User\source\repos\Object Serilization\Object Serilization\bin\Debug\Bin\AppData\SerializeFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read)
            Dim srObj As SerializeObject = DirectCast(formatter.Deserialize(serialStream), SerializeObject)
            serialStream.Close()
            Dim i As Integer = DataGridView1.Rows.Add()
            DataGridView1.Rows(i).Cells(0).Value = srObj.FirstName
            DataGridView1.Rows(i).Cells(1).Value = srObj.LastName
            DataGridView1.Rows(i).Cells(2).Value = srObj.Email
            DataGridView1.Rows(i).Cells(3).Value = srObj.srInt

        Catch ex As Exception

        End Try

    End Sub
    Dim DataT As New DataTable
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        'DataT.Columns.Add("First Name")
        'DataT.Columns.Add("Last Name")
        'DataT.Columns.Add("Email Name")
        'DataT.Columns.Add("Mobile No.")
        'DataGridView1.Columns.Clear()
        'DataGridView1.DataSource = DataT
        Dim i As Integer = DataGridView1.Rows.Add()
        DataGridView1.Rows(i).Cells(0).Value = TextBox1.Text
        DataGridView1.Rows(i).Cells(1).Value = TextBox2.Text
        DataGridView1.Rows(i).Cells(2).Value = TextBox3.Text
        DataGridView1.Rows(i).Cells(3).Value = TextBox4.Text





    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim DT As New DataTable


        Dim fmtrr As IFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        Dim serialStream As Stream = New FileStream("C:\Users\User\source\repos\Object Serilization\Object Serilization\bin\Debug\Bin\AppData\SerializeleTable.Tango", FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim srObj As SerializeTable = DirectCast(fmtrr.Deserialize(serialStream), SerializeTable)
        serialStream.Close()
        DT = srObj.Datat



        'DT.Columns.Add("First Name")
        'DT.Columns.Add("Last Name")
        'DT.Columns.Add("Email Name")
        'DT.Columns.Add("Mobile No.")
        Dim i As Integer = srObj.Datat.Rows.Count - 1









        For x = 1 To DataGridView1.Rows.Count - 1

                DT.Rows.Add()
                DT(i)(0) = DataGridView1.Rows(x - 1).Cells(0).Value
                DT(i)(1) = DataGridView1.Rows(x - 1).Cells(1).Value
                DT(i)(2) = DataGridView1.Rows(x - 1).Cells(2).Value
                DT(i)(3) = DataGridView1.Rows(x - 1).Cells(3).Value

            Next

        Dim FRM As New Form
        Dim DGV As New DataGridView
        DGV.Dock = DockStyle.Fill
        FRM.Controls.Add(DGV)
        DGV.DataSource = DT
        FRM.Show()

        Dim mrObj As New SerializeTable
        mrObj.Datat = DT
        Dim formatter As IFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        Dim fileStream As Stream = New FileStream("C:\Users\User\source\repos\Object Serilization\Object Serilization\bin\Debug\Bin\AppData\SerializeleTable.Tango", FileMode.Create, FileAccess.Write, FileShare.None)
        formatter.Serialize(fileStream, mrObj)
        fileStream.Close()
        MsgBox("Object Serialized !!")

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim Frm As New Form
        Dim DGV As New DataGridView
        Frm.Controls.Add(DGV)
        Dim dt As New DataTable
        Dim formatter As IFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        Dim serialStream As Stream = New FileStream("C:\Users\User\source\repos\Object Serilization\Object Serilization\bin\Debug\Bin\AppData\SerializeleTable.Tango", FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim srObj As SerializeTable = DirectCast(formatter.Deserialize(serialStream), SerializeTable)
        serialStream.Close()
        dt = srObj.Datat
        MsgBox("Ok")
        DGV.DataSource = dt
        DGV.Dock = DockStyle.Fill
        Frm.WindowState = FormWindowState.Maximized
        Frm.Show()
    End Sub
End Class
'specimen class for serialization
<Serializable()>
Public Class SerializeObject
    Public FirstName As String = Nothing
    Public LastName As String = Nothing
    Public Email As String = Nothing
    Public srInt As Int64 = 0



End Class

<Serializable()>
Public Class SerializeTable

    Public Datat As DataTable

End Class
