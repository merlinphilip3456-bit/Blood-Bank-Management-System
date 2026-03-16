Imports System.Reflection.Emit
Imports Microsoft.Data.SqlClient.DataClassification

Public Class Dashboard

    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "Welcome to Blood Bank Management System"
    End Sub

    ' ----- Donor Management -----
    Private Sub Donor_Click(sender As Object, e As EventArgs) Handles DonorToolStripMenuItem.Click
        Dim d As New DonorForm()
        d.Show()
    End Sub


    ' ----- Recipient Management -----
    Private Sub mnuRecipient_Click(sender As Object, e As EventArgs) Handles mnuRecipient.Click
        Dim r As New RecipientForm()
        r.Show()
    End Sub

    ' ----- Blood Stock -----
    Private Sub mnuStock_Click(sender As Object, e As EventArgs) Handles mnuStock.Click
        Dim b As New BloodStockForm()
        b.Show()
    End Sub

    ' ----- Blood Requests -----


    ' ⭐ UNIQUE MODULE
    Private Sub mnuEmergency_Click(sender As Object, e As EventArgs) Handles mnuEmergency.Click
        Dim eAlert As New Form13()
        eAlert.Show()
    End Sub
    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click

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


    ' ----- Exit -----
    Private Sub mnuExit_Click(sender As Object, e As EventArgs)
        Application.Exit
    End Sub


End Class





