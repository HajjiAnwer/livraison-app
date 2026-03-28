Public Class ajouter_prod

    Private Sub PopulerCodesProduits()
        Try
            Dim texteSaisi As String = CBCodeProd.Text.Trim()

            CBCodeProd.Items.Clear()
            open()
            cmd.Connection = con
            cmd.CommandText = "SELECT code_prod FROM produit ORDER BY code_prod"
            Dim dr As SqlClient.SqlDataReader = cmd.ExecuteReader()

            While dr.Read()
                CBCodeProd.Items.Add(dr("code_prod").ToString())
            End While

            dr.Close()

            If texteSaisi <> "" Then
                CBCodeProd.Text = texteSaisi
                CBCodeProd.SelectionStart = CBCodeProd.Text.Length
                CBCodeProd.SelectionLength = 0
            End If
        Catch ex As Exception
            MessageBox.Show("Erreur lors du chargement des codes produit: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub AutoRemplirProduit()
        If CBCodeProd.Text.Trim() = "" Then
            Txt_nomprod.Text = ""
            Txt_prixprod.Text = ""
            Txt_quantiteprod.Text = "0"
            Exit Sub
        End If

        Dim codeProd As Decimal
        If Not Decimal.TryParse(CBCodeProd.Text.Trim(), codeProd) Then
            Txt_nomprod.Text = ""
            Txt_prixprod.Text = ""
            Txt_quantiteprod.Text = "0"
            Exit Sub
        End If

        Try
            open()
            cmd.Connection = con
            cmd.CommandText = "SELECT nom_prod, prix_prod, quantite_ FROM produit WHERE code_prod = @code_prod"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@code_prod", codeProd)
            Dim dr As SqlClient.SqlDataReader = cmd.ExecuteReader()

            If dr.Read() Then
                Txt_nomprod.Text = dr("nom_prod").ToString()
                Txt_prixprod.Text = dr("prix_prod").ToString()
                Txt_quantiteprod.Text = dr("quantite_").ToString()
            Else
                Txt_nomprod.Text = ""
                Txt_prixprod.Text = ""
                Txt_quantiteprod.Text = "0"
            End If

            dr.Close()
        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub CBCodeProd_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCodeProd.SelectionChangeCommitted
        AutoRemplirProduit()
    End Sub

    Private Sub CBCodeProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCodeProd.Click
        If CBCodeProd.Items.Count > 0 Then
            CBCodeProd.DroppedDown = True
        End If
    End Sub

    Private Sub CBCodeProd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCodeProd.Leave
        If CBCodeProd.FindStringExact(CBCodeProd.Text.Trim()) >= 0 Then
            AutoRemplirProduit()
        End If
    End Sub

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
        CBCodeProd.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        CBCodeProd.AutoCompleteSource = AutoCompleteSource.ListItems
        PopulerCodesProduits()
        ChargerProduits()
    End Sub

    Private Function ValiderChamps() As Boolean
        If CBCodeProd.Text.Trim() = "" OrElse Txt_nomprod.Text.Trim() = "" OrElse Txt_prixprod.Text.Trim() = "" OrElse Txt_quantiteprod.Text.Trim() = "" Then
            MessageBox.Show("Veuillez remplir tous les champs.")
            Return False
        End If

        Dim codeProd As Decimal
        Dim prixProd As Decimal
        Dim quantiteProd As Decimal
        If Not Decimal.TryParse(CBCodeProd.Text.Trim(), codeProd) Then
            MessageBox.Show("Code produit invalide.")
            CBCodeProd.Focus()
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
        CBCodeProd.Text = ""
        Txt_nomprod.Text = ""
        Txt_prixprod.Text = ""
        Txt_quantiteprod.Text = "0"
        CBCodeProd.Focus()
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
            cmd.Parameters.AddWithValue("@code_prod", CDec(CBCodeProd.Text.Trim()))
            cmd.Parameters.AddWithValue("@nom_prod", Txt_nomprod.Text.Trim())
            cmd.Parameters.AddWithValue("@prix_prod", CDec(Txt_prixprod.Text.Trim()))
            cmd.Parameters.AddWithValue("@quantite_", CDec(Txt_quantiteprod.Text.Trim()))
            cmd.ExecuteNonQuery()
            MessageBox.Show("PRODUIT ajouté avec succés")
            ViderChamps()

        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try

        Try
            PopulerCodesProduits()
            ChargerProduits()
        Catch ex As Exception
            MessageBox.Show("Erreur lors de la mise à jour de la liste: " & ex.Message)
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
            cmd.Parameters.AddWithValue("@code_prod", CDec(CBCodeProd.Text.Trim()))

            Dim lignesMaj As Integer = cmd.ExecuteNonQuery()
            If lignesMaj > 0 Then
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

        Try
            PopulerCodesProduits()
            ChargerProduits()
        Catch ex As Exception
            MessageBox.Show("Erreur lors de la mise à jour de la liste: " & ex.Message)
        End Try
    End Sub
End Class