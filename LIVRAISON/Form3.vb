Public Class ajouter_prod

    Private Sub ChargerProduits()
        Try
            DGProduit.Rows.Clear()
            open()
            cmd.Connection = con
            cmd.CommandText = "SELECT code_prod, nom_prod, prix_prod FROM produit ORDER BY code_prod"
            Dim dr As SqlClient.SqlDataReader = cmd.ExecuteReader()
            
            While dr.Read()
                DGProduit.Rows.Add(dr("code_prod"), dr("nom_prod"), dr("prix_prod"))
            End While
            
            dr.Close()
        Catch ex As Exception
            MessageBox.Show("Erreur lors du chargement des produits: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub ajouter_prod_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ChargerProduits()
    End Sub

    Private Function ValiderChamps() As Boolean
        If Txt_code_prod.Text.Trim() = "" OrElse Txt_nomprod.Text.Trim() = "" OrElse Txt_prixprod.Text.Trim() = "" OrElse Txt_quantiteprod.Text.Trim() = "" Then
            MessageBox.Show("Veuillez remplir tous les champs.")
            Return False
        End If

        Dim codeProd As Decimal
        Dim prixProd As Decimal
        Dim quantiteProd As Decimal
        If Not Decimal.TryParse(Txt_code_prod.Text.Trim(), codeProd) Then
            MessageBox.Show("Code produit invalide.")
            Txt_code_prod.Focus()
            Return False
        End If

        If Not Decimal.TryParse(Txt_prixprod.Text.Trim(), prixProd) Then
            MessageBox.Show("Prix produit invalide.")
            Txt_prixprod.Focus()
            Return False
        End If

        If Not Decimal.TryParse(Txt_quantiteprod.Text.Trim(), quantiteProd) Then
            MessageBox.Show("Quantite produit invalide.")
            Txt_quantiteprod.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub ViderChamps()
        Txt_code_prod.Text = ""
        Txt_nomprod.Text = ""
        Txt_prixprod.Text = ""
        Txt_quantiteprod.Text = "0"
        Txt_code_prod.Focus()
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Not ValiderChamps() Then
            Exit Sub
        End If

        Try
            open()
            cmd.Connection = con
            cmd.CommandText = "INSERT INTO produit(code_prod, nom_prod, prix_prod, quantite_) VALUES (@code_prod, @nom_prod, @prix_prod, @quantite_ )"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@code_prod", CDec(Txt_code_prod.Text.Trim()))
            cmd.Parameters.AddWithValue("@nom_prod", Txt_nomprod.Text.Trim())
            cmd.Parameters.AddWithValue("@prix_prod", CDec(Txt_prixprod.Text.Trim()))
            cmd.Parameters.AddWithValue("@quantite_", CDec(Txt_quantiteprod.Text.Trim()))
            cmd.ExecuteNonQuery()
            MessageBox.Show("PRODUIT ajouté avec succés")
            ViderChamps()
            ChargerProduits()

        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Not ValiderChamps() Then
            Exit Sub
        End If

        Try
            open()
            cmd.Connection = con
            cmd.CommandText = "UPDATE produit SET nom_prod = @nom_prod, prix_prod = @prix_prod, quantite_ = @quantite_ WHERE code_prod = @code_prod"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@nom_prod", Txt_nomprod.Text.Trim())
            cmd.Parameters.AddWithValue("@prix_prod", CDec(Txt_prixprod.Text.Trim()))
            cmd.Parameters.AddWithValue("@quantite_", CDec(Txt_quantiteprod.Text.Trim()))
            cmd.Parameters.AddWithValue("@code_prod", CDec(Txt_code_prod.Text.Trim()))

            Dim lignesMaj As Integer = cmd.ExecuteNonQuery()
            If lignesMaj > 0 Then
                ChargerProduits()
                MessageBox.Show("produit modifié avec succés")
                ViderChamps()
            Else
                MessageBox.Show("Aucun produit trouvé avec ce code.")
            End If

        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub
End Class