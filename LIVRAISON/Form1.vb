Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub AjouterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AjouterToolStripMenuItem.Click
        Dim f As New ajouter_clt()
        f.Show()
    End Sub

    Private Sub AjouterProduitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AjouterProduitToolStripMenuItem.Click
        Dim f As New ajouter_prod()
        f.Show()
    End Sub

    Private Sub AjouterToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AjouterToolStripMenuItem1.Click
        Dim f As New ajouter_bon_liv()
        f.Show()
    End Sub

    Private Sub AjouterCommandeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AjouterCommandeToolStripMenuItem.Click
        Dim f As New Form5()
        f.Show()
    End Sub

    Private Sub GestionBLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GestionBLToolStripMenuItem.Click
        Dim f As New Form6()
        f.Show()
    End Sub

End Class