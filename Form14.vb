Imports Microsoft.Data.SqlClient
Public Class DonorForm
    Dim con As New SqlConnection("Data Source=localhost;Initial Catalog=bloba;Integrated Security=True;Encrypt=True;Trust Server Certificate=True")
    Private Sub Form14_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbGender.Items.Add("Male")
        cmbGender.Items.Add("Female")

        cmbBloodGroup.Items.AddRange({"A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"})
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        Try
            con.Open()

            Dim cmd As New SqlCommand("AddDonor", con)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@DonorName", txtName.Text)
            cmd.Parameters.AddWithValue("@Gender", cmbGender.Text)
            cmd.Parameters.AddWithValue("@Age", txtAge.Text)
            cmd.Parameters.AddWithValue("@BloodGroup", cmbBloodGroup.Text)
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text)
            cmd.Parameters.AddWithValue("@ContactNumber", txtContact.Text)
            cmd.Parameters.AddWithValue("@LastDonationDate", dtpLastDonation.Value)

            cmd.ExecuteNonQuery()

            MessageBox.Show("Donor Added Successfully")

            con.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click

        Dim da As New SqlDataAdapter("SELECT * FROM Donor", con)
        Dim dt As New DataTable

        da.Fill(dt)

        DataGridView1.DataSource = dt

    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        Try
            con.Open()

            Dim cmd As New SqlCommand("UpdateDonor", con)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@DonorID", txtDonorID.Text)
            cmd.Parameters.AddWithValue("@DonorName", txtName.Text)
            cmd.Parameters.AddWithValue("@Gender", cmbGender.Text)
            cmd.Parameters.AddWithValue("@Age", txtAge.Text)
            cmd.Parameters.AddWithValue("@BloodGroup", cmbBloodGroup.Text)
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text)
            cmd.Parameters.AddWithValue("@ContactNumber", txtContact.Text)
            cmd.Parameters.AddWithValue("@LastDonationDate", dtpLastDonation.Value)

            cmd.ExecuteNonQuery()

            MessageBox.Show("Donor Updated Successfully")

            con.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Try
            con.Open()

            Dim cmd As New SqlCommand("DeleteDonor", con)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@DonorID", txtDonorID.Text)

            cmd.ExecuteNonQuery()

            MessageBox.Show("Donor Deleted Successfully")

            con.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        txtDonorID.Text = DataGridView1.CurrentRow.Cells(0).Value
        txtName.Text = DataGridView1.CurrentRow.Cells(1).Value
        cmbGender.Text = DataGridView1.CurrentRow.Cells(2).Value
        txtAge.Text = DataGridView1.CurrentRow.Cells(3).Value
        cmbBloodGroup.Text = DataGridView1.CurrentRow.Cells(4).Value
        txtAddress.Text = DataGridView1.CurrentRow.Cells(5).Value
        txtContact.Text = DataGridView1.CurrentRow.Cells(6).Value
        dtpLastDonation.Value = DataGridView1.CurrentRow.Cells(7).Value

    End Sub

End Class