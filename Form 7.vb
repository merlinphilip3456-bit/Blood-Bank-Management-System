Imports Microsoft.Data.SqlClient
Imports Windows.Win32.System

Public Class Form7
    Dim query As String = "UPDATE Donor_Requests 
SET Status='Approved', AdminMessage='You are added as a Donor'
WHERE RequestID=@id"
    Dim query1 As String = "UPDATE Recipient_Requests 
SET Status='Approved', AdminMessage='You are added as a Recipient'
WHERE RequestID=@id"
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDonorRequests()
        LoadRecipientRequests()
        LoadEmergencyRequests()
    End Sub
    Private Sub LoadDonorRequests()

        Dim cs As String = "Data Source=localhost;Initial Catalog=bloba;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"

        Try
            Using con As New SqlConnection(cs)

                Dim query As String = "SELECT * FROM Donor_Requests ORDER BY RequestDate DESC"

                Using da As New SqlDataAdapter(query, con)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    DataGridView2.DataSource = dt   ' 🔹 Donor Request Grid
                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadDonorRequests()
        LoadRecipientRequests()
        LoadEmergencyRequests()
    End Sub
    Private Sub LoadRecipientRequests()

        Dim cs As String = "Data Source=localhost;Initial Catalog=bloba;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"

        Try
            Using con As New SqlConnection(cs)

                Dim query As String = "SELECT * FROM Recipient_Requests ORDER BY RequestDate DESC"

                Using da As New SqlDataAdapter(query, con)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    DataGridView3.DataSource = dt   ' 🔹 Recipient Request Grid
                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        If DataGridView3.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a request")
            Exit Sub
        End If

        Dim id As Integer = Convert.ToInt32(DataGridView3.SelectedRows(0).Cells("RequestID").Value)

        Dim cs As String = "Data Source=localhost;Initial Catalog=bloba;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"

        Try
            Using con As New SqlConnection(cs)

                con.Open()

                ' 🔹 1. Update Status + Message
                Dim updateQuery As String = "UPDATE Recipient_Requests 
                                        SET Status='Approved', 
                                            AdminMessage='Request Approved. Visit hospital'
                                        WHERE RequestID=@id"

                Using cmdUpdate As New SqlCommand(updateQuery, con)
                    cmdUpdate.Parameters.AddWithValue("@id", id)
                    cmdUpdate.ExecuteNonQuery()
                End Using

                ' 🔹 2. Insert into main Recipient table
                Dim insertQuery As String = "INSERT INTO Recipient (RecipientName, Age, Gender, BloodGroup, ContactNumber, HospitalName, UnitsRequired)
                                        SELECT Name, Age, Gender, BloodGroup, Contact, Hospital, UnitsRequired
                                        FROM Recipient_Requests
                                        WHERE RequestID=@id"

                Using cmdInsert As New SqlCommand(insertQuery, con)
                    cmdInsert.Parameters.AddWithValue("@id", id)
                    cmdInsert.ExecuteNonQuery()
                End Using

                MessageBox.Show("Recipient Approved Successfully")

            End Using

            ' 🔹 3. Refresh grid
            LoadRecipientRequests()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        If DataGridView2.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a request")
            Exit Sub
        End If

        Dim id As Integer = Convert.ToInt32(DataGridView2.SelectedRows(0).Cells("RequestID").Value)

        Dim cs As String = "Data Source=localhost;Initial Catalog=bloba;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"

        Try
            Using con As New SqlConnection(cs)

                con.Open()

                ' 🔹 Update status
                Dim updateQuery As String = "UPDATE Donor_Requests 
                                        SET Status='Approved', 
                                            AdminMessage='You are eligible to donate'
                                        WHERE RequestID=@id"

                Using cmdUpdate As New SqlCommand(updateQuery, con)
                    cmdUpdate.Parameters.AddWithValue("@id", id)
                    cmdUpdate.ExecuteNonQuery()
                End Using

                ' 🔹 Insert into main Donor table
                Dim insertQuery As String = "INSERT INTO Donor (DonorName, Age, Gender, BloodGroup, ContactNumber, Address)
                                        SELECT DonorName, Age, Gender, BloodGroup, Contact, Address
                                        FROM Donor_Requests
                                        WHERE RequestID=@id"

                Using cmdInsert As New SqlCommand(insertQuery, con)
                    cmdInsert.Parameters.AddWithValue("@id", id)
                    cmdInsert.ExecuteNonQuery()
                End Using

                MessageBox.Show("Donor Approved Successfully")

            End Using

            ' 🔹 Refresh grid
            LoadDonorRequests()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub
    Sub LoadEmergencyRequests()

        Dim cs As String = "Data Source=localhost;Initial Catalog=bloba;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"

        Try
            Using con As New SqlConnection(cs)

                Dim da As New SqlDataAdapter("SELECT RequestID, BloodGroup, UnitsRequired, Status FROM Emergency_Requests", con)

                Dim dt As New DataTable
                da.Fill(dt)

                DataGridView1.DataSource = dt

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub btnApproveEmergency_Click(sender As Object, e As EventArgs) Handles btnApproveEmergency.Click

        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Select a request first")
            Exit Sub
        End If

        Dim id As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("RequestID").Value)

        Dim cs As String = "Data Source=localhost;Initial Catalog=bloba;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"

        Try
            Using con As New SqlConnection(cs)

                Dim query As String = "
            UPDATE Emergency_Requests
            SET Status = 'Approved',
                AdminMessage = 'Blood is available. Please visit hospital immediately.'
            WHERE RequestID = @id"

                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", id)

                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using

            End Using

            MessageBox.Show("Emergency Request Approved")

            LoadEmergencyRequests()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
End Class
