Public Class Form6
    Private EnChargementCommandes As Boolean = False

    Private Function EssayerLireNumCommande(ByRef numCommande As Integer) As Boolean
        numCommande = 0

        If CBCommandes.SelectedValue Is Nothing Then
            Return False
        End If

        ' During data binding, SelectedValue can temporarily be a DataRowView.
        If TypeOf CBCommandes.SelectedValue Is DataRowView Then
            Return False
        End If

        Return Integer.TryParse(CBCommandes.SelectedValue.ToString(), numCommande)
    End Function

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ChargerCommandes()
        Button2_Click(Nothing, Nothing)
    End Sub

    Private Sub ChargerCommandes()
        Try
            EnChargementCommandes = True
            open()
            cmd.CommandText = "SELECT num_commande, date_commande, code_clt FROM commande ORDER BY num_commande DESC"
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()

            Dim dt As New DataTable()
            dt.Load(dr)
            dr.Close()

            CBCommandes.DataSource = dt
            CBCommandes.DisplayMember = "num_commande"
            CBCommandes.ValueMember = "num_commande"

        Catch ex As Exception
            MessageBox.Show("Erreur chargement commandes: " & ex.Message)
        Finally
            EnChargementCommandes = False
            con.Close()
        End Try
    End Sub

    Private Sub CBCommandes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBCommandes.SelectedIndexChanged
        If EnChargementCommandes Then Exit Sub

        Dim numCommande As Integer
        If EssayerLireNumCommande(numCommande) Then
            ChargerLignesCommande(numCommande)
            ChargerBonsLivraison(numCommande)
        End If
    End Sub

    Private Sub ChargerLignesCommande(ByVal numCommande As Integer)
        Try
            open()
            cmd.CommandText = "SELECT lc.num_ligne, lc.code_prod, lc.quantite, lc.prix_unit, lc.total_ligne " &
                             "FROM lignecommande lc WHERE lc.num_commande = @num_commande"
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@num_commande", numCommande)
            dr = cmd.ExecuteReader()

            DGLignesCommande.DataSource = Nothing
            Dim dt As New DataTable()
            dt.Load(dr)
            DGLignesCommande.DataSource = dt
            dr.Close()

        Catch ex As Exception
            MessageBox.Show("Erreur chargement lignes: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub ChargerBonsLivraison(ByVal numCommande As Integer)
        Try
            open()
            cmd.CommandText = "SELECT num_liv, date_liv, code_clt, num_commande FROM bon_livraison " &
                             "WHERE num_commande = @num_commande ORDER BY num_liv DESC"
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@num_commande", numCommande)
            dr = cmd.ExecuteReader()

            DGBonsLivraison.DataSource = Nothing
            Dim dt As New DataTable()
            dt.Load(dr)
            DGBonsLivraison.DataSource = dt
            dr.Close()

        Catch ex As Exception
            MessageBox.Show("Erreur chargement bons livraison: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Générer BL (Generate Delivery Note)
        If CBCommandes.SelectedIndex < 0 Then
            MsgBox("Veuillez sélectionner une commande", MsgBoxStyle.Information, "Validation")
            Exit Sub
        End If

        Dim numCommande As Integer
        If Not EssayerLireNumCommande(numCommande) Then
            MsgBox("Veuillez sélectionner une commande valide", MsgBoxStyle.Information, "Validation")
            Exit Sub
        End If

        Try
            ' Get next available BL number
            open()
            cmd.CommandText = "SELECT MAX(num_liv) FROM bon_livraison"
            cmd.Connection = con
            Dim result = cmd.ExecuteScalar()
            Dim numBL As Integer = If(result Is Nothing OrElse IsDBNull(result), 1, CInt(result) + 1)
            con.Close()

            ' Get client from commande
            open()
            cmd.CommandText = "SELECT code_clt FROM commande WHERE num_commande = @num"
            cmd.Connection = con
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@num", numCommande)
            dr = cmd.ExecuteReader()
            
            Dim clientCode As String = ""
            If dr.Read() Then
                clientCode = dr("code_clt").ToString()
            End If
            dr.Close()
            con.Close()

            If clientCode = "" Then
                MsgBox("Client not found in order", MsgBoxStyle.Information, "Erreur")
                Exit Sub
            End If

            ' Create BON_LIVRAISON record linked to COMMANDE
            open()
            cmd.CommandText = "INSERT INTO bon_livraison (num_liv, date_liv, code_clt, num_commande) " &
                             "VALUES (@num_liv, @date, @client, @commande)"
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@num_liv", numBL)
            cmd.Parameters.AddWithValue("@date", Today)
            cmd.Parameters.AddWithValue("@client", clientCode)
            cmd.Parameters.AddWithValue("@commande", numCommande)
            cmd.ExecuteNonQuery()

            MsgBox("Bon de Livraison " & numBL & " généré avec succès!", MsgBoxStyle.Information, "Succès")
            ChargerBonsLivraison(numCommande)

        Catch ex As Exception
            MessageBox.Show("Erreur génération BL: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Rafraîchir (Refresh)
        ChargerCommandes()
        Dim numCommande As Integer
        If EssayerLireNumCommande(numCommande) Then
            ChargerLignesCommande(numCommande)
            ChargerBonsLivraison(numCommande)
        Else
            DGLignesCommande.DataSource = Nothing
            DGBonsLivraison.DataSource = Nothing
        End If
    End Sub

End Class
