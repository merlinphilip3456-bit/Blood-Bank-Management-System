Imports Microsoft.Data.SqlClient
Public Class RecipientForm
    Dim con As New SqlConnection("Data Source=localhost;Initial Catalog=bloba;Integrated Security=True;Encrypt=True;Trust Server Certificate=True")
    Private Sub RecipientForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbGender.Items.Add("Male")
        cmbGender.Items.Add("Female")

        cmbBloodGroup.Items.AddRange({"A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"})
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try
            con.Open()

            Dim cmd As New SqlCommand("AddRecipient", con)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@RecipientName", txtName.Text)
            cmd.Parameters.AddWithValue("@Gender", cmbGender.Text)
            cmd.Parameters.AddWithValue("@Age", txtAge.Text)
            cmd.Parameters.AddWithValue("@BloodGroup", cmbBloodGroup.Text)
            cmd.Parameters.AddWithValue("@UnitsRequired", txtUnits.Text)
            cmd.Parameters.AddWithValue("@HospitalName", txtHospital.Text)
            cmd.Parameters.AddWithValue("@ContactNumber", txtContact.Text)

            cmd.ExecuteNonQuery()

            MessageBox.Show("Recipient Registered Successfully")

            con.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click

        Try

            Dim da As New SqlDataAdapter("SELECT * FROM Recipient", con)
            Dim dt As New DataTable

            da.Fill(dt)

            DataGridView1.DataSource = dt

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub
End Class