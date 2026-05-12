Imports System.Reflection.Emit
Imports Microsoft.Data.SqlClient.DataClassification

Public Class UserDashBoard

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    ' ----- Donor Management -----
    Private Sub Donor_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim d As New Form4()
        d.Show()
    End Sub


    ' ----- Recipient Management -----
    Private Sub Recipient_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim r As New Form6()
        r.Show()
    End Sub

    ' ----- Blood Stock -----
    Private Sub BloodStock_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim b As New Form8()
        b.Show()
    End Sub

    ' ----- Blood Requests -----


    ' ⭐ UNIQUE MODULE
    Private Sub Emergency_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim eAlert As New Form13()
        eAlert.Show()
    End Sub
    Private Sub Logout_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Dim result As DialogResult
        result = MessageBox.Show("Are you sure you want to logout?",
                             "Logout Confirmation",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Dim loginForm As New Form1  ' Replace with your login form name
            loginForm.Show()
            Me.Close()   ' Closes current form (Dashboard/Main Form)
        End If

    End Sub
End Class




