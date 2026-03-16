Imports Microsoft.Data.SqlClient
Public Class BloodStockForm
    Dim con As New SqlConnection(
    "Data Source=localhost;Initial Catalog=bloba;Integrated Security=True;Encrypt=True;Trust Server Certificate=True")
    Private Sub BloodStockForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbBloodGroup.Items.AddRange({"A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"})
    End Sub
    Private Sub btnAddStock_Click(sender As Object, e As EventArgs) Handles btnAddStock.Click

        If cmbBloodGroup.Text = "" Or txtUnits.Text = "" Then
            MessageBox.Show("Enter all details")
            Exit Sub
        End If

        Try
            con.Open()

            Dim cmd As New SqlCommand("UpdateBloodStock", con)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@BloodGroup", cmbBloodGroup.Text)
            cmd.Parameters.AddWithValue("@Units", Convert.ToInt32(txtUnits.Text))

            cmd.ExecuteNonQuery()

            MessageBox.Show("Blood stock updated successfully")

            con.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub btnViewStock_Click(sender As Object, e As EventArgs) Handles btnViewStock.Click

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