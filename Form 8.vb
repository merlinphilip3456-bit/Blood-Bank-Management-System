Imports Microsoft.Data.SqlClient
Imports Windows.Win32.System

Public Class Form8
    Dim con As New SqlConnection(
    "Data Source=localhost;Initial Catalog=bloba;Integrated Security=True;Encrypt=True;Trust Server Certificate=True")
    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub btnViewStock_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            Dim da As New SqlDataAdapter("SELECT * FROM BloodStock", con)
            Dim dt As New DataTable

            da.Fill(dt)

            DataGridView1.DataSource = dt

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
End Class
