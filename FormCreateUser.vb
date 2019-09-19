Public Class FormCreateUser
    Dim img As String
    Private Sub FormCreateUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBoxOrgPass.UseSystemPasswordChar = Not CheckBox1.Checked
        TextBoxRetypedPass.UseSystemPasswordChar = Not CheckBox1.Checked
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        TextBoxOrgPass.UseSystemPasswordChar = Not CheckBox1.Checked
        TextBoxRetypedPass.UseSystemPasswordChar = Not CheckBox1.Checked
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Title = "Select Image"
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.Filter = "Jpg(*.jpg)|*.jpg|Png(*.Png)|*.png|Bmp(*.bmp)|*.bmp"
        OpenFileDialog1.ShowDialog()
        PictureBox1.Image = Drawing.Image.FromFile(OpenFileDialog1.FileName)
        img = OpenFileDialog1.FileName
    End Sub

    Private Sub ButtonCreateUser_Click(sender As Object, e As EventArgs) Handles ButtonCreateUser.Click
        Dim User As String
        Dim Pass As String
        If TextBoxOrgPass.Text = TextBoxRetypedPass.Text Then
            User = TextBoxUser.Text
            Pass = TextBoxOrgPass.Text
            Dim Token As New Costing_Helper.SecurityControl
            Token.CreateUser(TextBoxUser.Text, TextBoxOrgPass.Text, img)

        Else
            MsgBox("Re-Typed Password is Not Matching")
            CheckBox1.Select()
        End If



    End Sub

    Private Sub TextBoxRetypedPass_TextChanged(sender As Object, e As EventArgs) Handles TextBoxRetypedPass.LostFocus
        If TextBoxOrgPass.Text = TextBoxRetypedPass.Text Then
            Label5.Text = "Password Okey"
        Else
            Label5.Text = "Re-typed Password Not Matching "
        End If


    End Sub
End Class