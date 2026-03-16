Imports Microsoft.Data.SqlClient

Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim username As String = TextBox1.Text.Trim()
        Dim password As String = TextBox2.Text.Trim()

        If username = "" OrElse password = "" Then
            MessageBox.Show("Enter username and password", "Validation",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim cs As String =
            "Data Source=localhost;Initial Catalog=bloba;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"

        Dim query As String =
            "SELECT COUNT(*) FROM Login_Users WHERE Username=@username AND Password=@password"

        Try
            Using con As New SqlConnection(cs)
                Using cmd As New SqlCommand(query, con)

                    cmd.Parameters.AddWithValue("@username", username)
                    cmd.Parameters.AddWithValue("@password", password)

                    con.Open()
                    Dim result As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    If result > 0 Then
                        MessageBox.Show("Login Successful", "Success",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information)

                        Dim d As New Dashboard()
                        d.Show()
                        Me.Hide()
                    Else
                        MessageBox.Show("Invalid Username or Password", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                        TextBox2.Clear()
                        TextBox1.Focus()
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim username As String = TextBox1.Text.Trim()
        Dim password As String = TextBox2.Text.Trim()

        If username = "" OrElse password = "" Then
            MessageBox.Show("Enter username and password")
            Exit Sub
        End If

        Dim cs As String =
        "Data Source=localhost;Initial Catalog=bloba;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"

        Try
            Dim checkUser As String = "SELECT COUNT(*) FROM Login_Users WHERE Username=@username"
            Dim insertUser As String = "INSERT INTO Login_Users (Username,Password) VALUES (@username,@password)"

            Using con As New SqlConnection(cs)

                con.Open()

                Dim cmdCheck As New SqlCommand(checkUser, con)
                cmdCheck.Parameters.AddWithValue("@username", username)

                Dim exists As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

                If exists > 0 Then
                    MessageBox.Show("Username already exists")
                    Exit Sub
                End If

                Dim cmdInsert As New SqlCommand(insertUser, con)
                cmdInsert.Parameters.AddWithValue("@username", username)
                cmdInsert.Parameters.AddWithValue("@password", password)

                cmdInsert.ExecuteNonQuery()

                MessageBox.Show("User Registered Successfully")

            End Using


        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub


End Class
