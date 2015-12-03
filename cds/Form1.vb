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
        cmd.CommandText = "SELECT id, title, artist FROM cds"
        cmd.Connection = cnn

        Dim rdr As SqlDataReader = cmd.ExecuteReader()
        While rdr.Read
            Dim oCd As New Cd(rdr(0), rdr(1), rdr(2))
            lItems.Add(oCd)
            Dim lvi As New ListViewItem(rdr(1).ToString())
            lvi.SubItems.Add(rdr(2))
            lvCDs.Items.Add(lvi)
        End While
        rdr.Close()


    End Sub

    Private Sub lvCDs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvCDs.SelectedIndexChanged
        If lvCDs.SelectedItems.Count > 0 Then
            Dim oCd As Cd = lItems(lvCDs.SelectedIndices(0))
            txtArtist.Text = oCd.sArtist
            txtTitle.Text = oCd.sTitle
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cmd As New SqlCommand
        cmd.CommandText = "INSERT INTO cds(title, artist) OUTPUT INSERTED.ID VALUES(@title, @artist)"
        cmd.Connection = cnn
        cmd.Parameters.AddWithValue("title", txtTitle.Text)
        cmd.Parameters.AddWithValue("artist", txtArtist.Text)
        Dim nId As Integer = cmd.ExecuteScalar()
        Dim oCd As New Cd(nId, txtTitle.Text, txtArtist.Text)
        lItems.Add(oCd)
        Dim lvItem As New ListViewItem(txtTitle.Text)
        lvItem.SubItems.Add(txtArtist.Text)
        lvCDs.Items.Add(lvItem)
    End Sub
End Class
