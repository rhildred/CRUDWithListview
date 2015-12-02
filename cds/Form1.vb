Imports System.Data.SqlClient

Public Class Form1
    Dim connString As String = "Server=.\SQLEXPRESS;Database=cds;Trusted_Connection=True;"
    Dim cnn As New SqlConnection(connString)
    Dim lItems As New List(Of Cd)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lvCDs.Clear()
        lvCDs.View = View.Details   'Full detailed version of ListView control 
        lvCDs.GridLines = True       'Pretty gridlines
        lvCDs.FullRowSelect = True   'Enables row selectability
        lvCDs.MultiSelect = False     'Set this to False for one row selectability

        'Set up column labels here. 
        lvCDs.Columns.Add("Title", 150, HorizontalAlignment.Left)
        lvCDs.Columns.Add("Artist", 150, HorizontalAlignment.Left)

        cnn.Open()
        Dim cmd As New SqlCommand
        cmd.CommandText = "SELECT id, title, lastname FROM cds"
        cmd.Connection = cnn

        Dim rdr As SqlDataReader = cmd.ExecuteReader()
        While rdr.Read
            Dim oCd As New Cd(rdr(0), rdr(1), rdr(2))
            lItems.Add(oCd)
            Dim lvi As New ListViewItem(rdr(1).ToString())
            lvi.SubItems.Add(rdr(2))
            lvCDs.Items.Add(lvi)
        End While


    End Sub

    Private Sub lvCDs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvCDs.SelectedIndexChanged
        Dim oCd As Cd = lItems(lvCDs.SelectedIndices(0))
        txtArtist.Text = oCd.sArtist
        txtTitle.Text = oCd.sTitle
    End Sub
End Class
