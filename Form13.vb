Imports Microsoft.Data.SqlClient
Public Class Form13
    Private ReadOnly cs As String =
    "Data Source=localhost;Initial Catalog=bloba;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"
    Private Sub Form13_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbBloodGroup.Items.AddRange({
      "A+", "A-", "B+", "B-",
      "AB+", "AB-", "O+", "O-"
  })

        cmbBloodGroup.SelectedIndex = -1
    End Sub

    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click

        If cmbBloodGroup.Text = "" Or txtUnits.Text = "" Then
            MessageBox.Show("Please fill all fields")
            Exit Sub
        End If

        Dim bloodGroup As String = cmbBloodGroup.Text
        Dim unitsRequested As Integer = Val(txtUnits.Text)

        Try
            Using con As New SqlConnection(cs)

                con.Open()

                ' 🔹 Check stock
                Dim cmd As New SqlCommand(
                "SELECT UnitsAvailable FROM BloodStock WHERE BloodGroup=@bg", con)

                cmd.Parameters.AddWithValue("@bg", bloodGroup)

                Dim result As Object = cmd.ExecuteScalar()

                If result Is Nothing Then
                    MessageBox.Show("Blood group not found!")
                    Exit Sub
                End If

                Dim stock As Integer = CInt(result)

                ' ===== AVAILABLE =====
                If stock >= unitsRequested Then

                    lblStatus.Text = "Available ✔"
                    lblStatus.ForeColor = Color.Green
                    MessageBox.Show("Blood Available!")

                    ' ===== LOW STOCK =====
                ElseIf stock > 0 Then

                    lblStatus.Text = "Low Stock ⚠"
                    lblStatus.ForeColor = Color.Orange

                    MessageBox.Show("Low stock! Emergency alert recorded.")
                    SaveAlert(bloodGroup, unitsRequested, "LOW STOCK")

                    ' ===== NOT AVAILABLE =====
                Else

                    lblStatus.Text = "Not Available ❌"
                    lblStatus.ForeColor = Color.Red

                    MessageBox.Show("Blood not available! Emergency alert sent.")
                    SaveAlert(bloodGroup, unitsRequested, "NOT AVAILABLE")

                End If

            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub
    Private Sub SaveAlert(bg As String, units As Integer, status As String)

        Using con As New SqlConnection(cs)

            con.Open()

            Dim cmd As New SqlCommand(
            "INSERT INTO EmergencyAlerts (BloodGroup, UnitsRequested, Status)
             VALUES (@bg, @units, @status)", con)

            cmd.Parameters.AddWithValue("@bg", bg)
            cmd.Parameters.AddWithValue("@units", units)
            cmd.Parameters.AddWithValue("@status", status)

            cmd.ExecuteNonQuery()

        End Using

    End Sub
End Class