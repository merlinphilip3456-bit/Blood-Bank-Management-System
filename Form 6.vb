Imports Microsoft.Data.SqlClient
Imports Windows.Win32.System

Public Class Form6
    Dim query As String = "SELECT Status, AdminMessage FROM Recipient_Requests WHERE Contact=@contact"
    Dim con As New SqlConnection("Data Source=localhost;Initial Catalog=bloba;Integrated Security=True;Encrypt=True;Trust Server Certificate=True")
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbGender.Items.Add("Male")
        cmbGender.Items.Add("Female")

        cmbBloodGroup.Items.AddRange({"A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"})
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim cs As String = "Data Source=localhost;Initial Catalog=bloba;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"

        Try
            Using con As New SqlConnection(cs)

                Dim query As String = "INSERT INTO Recipient_Requests 
                (Name, Age, Gender, BloodGroup, Contact, Hospital, UnitsRequired)
                VALUES (@name, @age, @gender, @bg, @contact, @hospital, @units)"

                Using cmd As New SqlCommand(query, con)

                    cmd.Parameters.AddWithValue("@name", txtName.Text)
                    cmd.Parameters.AddWithValue("@age", txtAge.Text)
                    cmd.Parameters.AddWithValue("@gender", cmbGender.Text)
                    cmd.Parameters.AddWithValue("@bg", cmbBloodGroup.Text)
                    cmd.Parameters.AddWithValue("@contact", txtContact.Text)
                    cmd.Parameters.AddWithValue("@hospital", txtHospital.Text)
                    cmd.Parameters.AddWithValue("@units", txtUnits.Text)

                    con.Open()
                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Recipient request sent to Admin successfully!")

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            ' 🔹 EXISTING CODE (keep this)
            Dim da As New SqlDataAdapter("SELECT * FROM Recipient", con)
            Dim dt As New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            ' 🔥 NEW CODE (get latest request status)
            Dim query As String = "SELECT TOP 1 Status, AdminMessage 
                              FROM Recipient_Requests 
                              WHERE Contact=@contact 
                              ORDER BY RequestDate DESC"

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@contact", txtContact.Text)

                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        MessageBox.Show("Status: " & reader("Status").ToString() & vbCrLf &
                                    "Message: " & reader("AdminMessage").ToString())
                    Else
                        MessageBox.Show("No request found")
                    End If
                End Using

                con.Close()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


End Class
