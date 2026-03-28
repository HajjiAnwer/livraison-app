Public Class Form5
    Private EnMiseAJourClient As Boolean = False
    Private EnMiseAJourProduit As Boolean = False

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TxtDateCommande.Text = Today.ToString("yyyy-MM-dd")
        AssurerSchemaCommande()
        PopulerCodesClientsCMD()
        PopulerCodesProduitsCMD()
        InitialiserNumeroCommande()
        ChargerCommandesExistantes()
        Button1.Text = "Ajouter Ligne"
        Button2.Text = "Passer Commande"
    End Sub

    Private Sub AssurerSchemaCommande()
        Try
            open()
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.CommandText = _
                "IF OBJECT_ID('dbo.commande', 'U') IS NULL " & _
                "BEGIN " & _
                "CREATE TABLE commande (" & _
                "num_commande INT PRIMARY KEY, " & _
                "date_commande DATE NOT NULL, " & _
                "code_clt DECIMAL(18,0) NOT NULL, " & _
                "CONSTRAINT FK_commande_client FOREIGN KEY (code_clt) REFERENCES client(code_clt)" & _
                ") " & _
                "END; " & _
                "IF OBJECT_ID('dbo.lignecommande', 'U') IS NULL " & _
                "BEGIN " & _
                "CREATE TABLE lignecommande (" & _
                "num_ligne INT IDENTITY(1,1) PRIMARY KEY, " & _
                "code_prod DECIMAL(18,0) NOT NULL, " & _
                "quantite DECIMAL(18,2) NOT NULL, " & _
                "prix_unit DECIMAL(18,2) NOT NULL, " & _
                "total_ligne DECIMAL(18,2) NOT NULL, " & _
                "num_commande INT NOT NULL, " & _
                "CONSTRAINT FK_lignecommande_produit FOREIGN KEY (code_prod) REFERENCES produit(code_prod), " & _
                "CONSTRAINT FK_lignecommande_commande FOREIGN KEY (num_commande) REFERENCES commande(num_commande)" & _
                ") " & _
                "END"
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Erreur preparation schema commande: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub PopulerCodesClientsCMD()
        Try
            ' Preserve user input
            Dim texteSaisi As String = CBCodeClt.Text.Trim()
            CBCodeClt.Items.Clear()
            CBCodeClt.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            CBCodeClt.AutoCompleteSource = AutoCompleteSource.ListItems

            open()
            cmd.CommandText = "SELECT code_clt FROM client ORDER BY code_clt"
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()

            While dr.Read()
                CBCodeClt.Items.Add(dr("code_clt").ToString())
            End While

            dr.Close()

            ' Restore user input
            CBCodeClt.Text = texteSaisi
                CBCodeClt.SelectionStart = CBCodeClt.Text.Length
                CBCodeClt.SelectionLength = 0

        Catch ex As Exception
            MessageBox.Show("Erreur chargement clients: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub PopulerCodesProduitsCMD()
        Try
            ' Preserve user input
            Dim texteSaisi As String = CBCodeProd.Text.Trim()
            CBCodeProd.Items.Clear()
            CBCodeProd.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            CBCodeProd.AutoCompleteSource = AutoCompleteSource.ListItems

            open()
            cmd.CommandText = "SELECT code_prod FROM produit ORDER BY code_prod"
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()

            While dr.Read()
                CBCodeProd.Items.Add(dr("code_prod").ToString())
            End While

            dr.Close()

            ' Restore user input
            CBCodeProd.Text = texteSaisi
                CBCodeProd.SelectionStart = CBCodeProd.Text.Length
                CBCodeProd.SelectionLength = 0

        Catch ex As Exception
            MessageBox.Show("Erreur chargement produits: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub AutoRemplirClientParCode()
        If EnMiseAJourClient Then Exit Sub
        If CBCodeClt.Text.Trim() = "" Then Exit Sub

        Try
            EnMiseAJourClient = True
            open()
            cmd.CommandText = "SELECT nom, prenom, adresse, telephone FROM client WHERE code_clt = @code"
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@code", CBCodeClt.Text.Trim())
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                TxtNom.Text = dr("nom").ToString()
                TxtPrenom.Text = dr("prenom").ToString()
                TxtAdresse.Text = dr("adresse").ToString()
                TxtTelephone.Text = dr("telephone").ToString()
            End If

            dr.Close()
        Catch ex As Exception
            MessageBox.Show("Erreur auto-remplissage client: " & ex.Message)
        Finally
            con.Close()
            EnMiseAJourClient = False
        End Try
    End Sub

    Private Sub AutoRemplirProduitParCode()
        If EnMiseAJourProduit Then Exit Sub
        If CBCodeProd.Text.Trim() = "" Then Exit Sub

        Try
            EnMiseAJourProduit = True
            open()
            cmd.CommandText = "SELECT nom_prod, prix_prod FROM produit WHERE code_prod = @code"
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@code", CBCodeProd.Text.Trim())
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                CBproduit.Text = dr("nom_prod").ToString()
                Txtprix.Text = dr("prix_prod").ToString()
            End If

            dr.Close()
        Catch ex As Exception
            MessageBox.Show("Erreur auto-remplissage produit: " & ex.Message)
        Finally
            con.Close()
            EnMiseAJourProduit = False
        End Try
    End Sub

    Private Sub InitialiserNumeroCommande()
        Try
            open()
            cmd.CommandText = "SELECT MAX(num_commande) FROM commande"
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            Dim result = cmd.ExecuteScalar()

            If result Is Nothing OrElse IsDBNull(result) Then
                TxtNumCommande.Text = "1"
            Else
                TxtNumCommande.Text = (CInt(result) + 1).ToString()
            End If

        Catch ex As Exception
            TxtNumCommande.Text = "1"
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub ChargerCommandesExistantes()
        Try
            open()
            cmd.CommandText = "SELECT lc.num_ligne, p.code_prod, lc.quantite, lc.prix_unit, lc.total_ligne, lc.num_commande " &
                             "FROM lignecommande lc LEFT JOIN produit p ON lc.code_prod = p.code_prod"
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()

            DG1.DataSource = Nothing
            Dim dt As New DataTable()
            dt.Load(dr)
            DG1.DataSource = dt

            dr.Close()
        Catch ex As Exception
            MessageBox.Show("Erreur chargement commandes: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub ViderChampsSaisieCMD()
        CBCodeClt.Text = ""
        TxtNom.Text = ""
        TxtAdresse.Text = ""
        TxtTelephone.Text = ""
        TxtPrenom.Text = ""
        CBCodeProd.Text = ""
        CBproduit.Text = ""
        Txtprix.Text = ""
        Txtqt.Text = ""
        InitialiserNumeroCommande()
    End Sub

    Private Function AssurerCommandeExiste(ByVal numCommande As Integer, ByVal codeClient As Decimal, ByVal dateCommande As Date) As Boolean
        Try
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.CommandText = "IF NOT EXISTS (SELECT 1 FROM commande WHERE num_commande = @num_commande) " &
                             "BEGIN " &
                             "INSERT INTO commande (num_commande, date_commande, code_clt) VALUES (@num_commande, @date_commande, @code_clt) " &
                             "END " &
                             "ELSE " &
                             "BEGIN " &
                             "UPDATE commande SET date_commande = @date_commande, code_clt = @code_clt WHERE num_commande = @num_commande " &
                             "END"
            cmd.Parameters.AddWithValue("@num_commande", numCommande)
            cmd.Parameters.AddWithValue("@date_commande", dateCommande)
            cmd.Parameters.AddWithValue("@code_clt", codeClient)
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MessageBox.Show("Erreur creation commande: " & ex.Message)
            Return False
        End Try
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Ajouter Ligne
        If CBCodeClt.Text.Trim() = "" Then
            MsgBox("Veuillez saisir le code client", MsgBoxStyle.Information, "Validation")
            Exit Sub
        End If

        If CBCodeProd.Text.Trim() = "" Then
            MsgBox("Veuillez saisir le code produit", MsgBoxStyle.Information, "Validation")
            Exit Sub
        End If

        If Txtqt.Text.Trim() = "" Then
            MsgBox("Veuillez saisir la quantité", MsgBoxStyle.Information, "Validation")
            Exit Sub
        End If

        If TxtNumCommande.Text.Trim() = "" Then
            MsgBox("Veuillez saisir le numero de commande avant d'ajouter une ligne", MsgBoxStyle.Information, "Validation")
            Exit Sub
        End If

        Dim numCommande As Integer
        If Not Integer.TryParse(TxtNumCommande.Text.Trim(), numCommande) Then
            MsgBox("Numero de commande invalide", MsgBoxStyle.Information, "Validation")
            Exit Sub
        End If

        Dim codeClient As Decimal
        If Not Decimal.TryParse(CBCodeClt.Text.Trim(), codeClient) Then
            MsgBox("Code client invalide", MsgBoxStyle.Information, "Validation")
            Exit Sub
        End If

        Try
            Dim quantite As Decimal = LireDecimal(Txtqt.Text)
            Dim prixUnit As Decimal = LireDecimal(Txtprix.Text)
            Dim totalLigne As Decimal = CalculerTotalLigne(quantite, prixUnit)

            open()

            If Not AssurerCommandeExiste(numCommande, codeClient, Today) Then
                Exit Sub
            End If

            cmd.CommandText = "INSERT INTO lignecommande (code_prod, quantite, prix_unit, total_ligne, num_commande) " &
                             "VALUES (@code_prod, @quantite, @prix_unit, @total_ligne, @num_commande)"
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@code_prod", CBCodeProd.Text.Trim())
            cmd.Parameters.AddWithValue("@quantite", quantite)
            cmd.Parameters.AddWithValue("@prix_unit", prixUnit)
            cmd.Parameters.AddWithValue("@total_ligne", totalLigne)
            cmd.Parameters.AddWithValue("@num_commande", numCommande)
            cmd.ExecuteNonQuery()

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            ChargerCommandesExistantes()
            CBCodeProd.Text = ""
            CBproduit.Text = ""
            Txtprix.Text = ""
            Txtqt.Text = ""

        Catch ex As Exception
            MessageBox.Show("Erreur ajout ligne: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Passer Commande
        If CBCodeClt.Text.Trim() = "" Then
            MsgBox("Veuillez saisir le code client", MsgBoxStyle.Information, "Validation")
            Exit Sub
        End If

        If TxtNumCommande.Text.Trim() = "" Then
            MsgBox("Veuillez saisir le numero de commande", MsgBoxStyle.Information, "Validation")
            Exit Sub
        End If

        Dim numCommande As Integer
        If Not Integer.TryParse(TxtNumCommande.Text.Trim(), numCommande) Then
            MsgBox("Numero de commande invalide", MsgBoxStyle.Information, "Validation")
            Exit Sub
        End If

        Dim codeClient As Decimal
        If Not Decimal.TryParse(CBCodeClt.Text.Trim(), codeClient) Then
            MsgBox("Code client invalide", MsgBoxStyle.Information, "Validation")
            Exit Sub
        End If

        Try
            open()
            If Not AssurerCommandeExiste(numCommande, codeClient, Today) Then
                Exit Sub
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            MsgBox("Commande enregistree avec succes!", MsgBoxStyle.Information, "Success")
            ChargerCommandesExistantes()
            ViderChampsSaisieCMD()

        Catch ex As Exception
            MessageBox.Show("Erreur sauvegarde commande: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub CBCodeClt_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CBCodeClt.SelectionChangeCommitted
        AutoRemplirClientParCode()
    End Sub

    Private Sub CBCodeClt_Click(sender As Object, e As EventArgs) Handles CBCodeClt.Click
        If CBCodeClt.Items.Count > 0 Then
            CBCodeClt.DroppedDown = True
        End If
    End Sub

    Private Sub CBCodeClt_Leave(sender As Object, e As EventArgs) Handles CBCodeClt.Leave
        If CBCodeClt.FindStringExact(CBCodeClt.Text.Trim()) >= 0 Then
            AutoRemplirClientParCode()
        End If
    End Sub

    Private Sub CBCodeProd_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CBCodeProd.SelectionChangeCommitted
        AutoRemplirProduitParCode()
    End Sub

    Private Sub CBCodeProd_Click(sender As Object, e As EventArgs) Handles CBCodeProd.Click
        If CBCodeProd.Items.Count > 0 Then
            CBCodeProd.DroppedDown = True
        End If
    End Sub

    Private Sub CBCodeProd_Leave(sender As Object, e As EventArgs) Handles CBCodeProd.Leave
        If CBCodeProd.FindStringExact(CBCodeProd.Text.Trim()) >= 0 Then
            AutoRemplirProduitParCode()
        End If
    End Sub

    Private Sub CBproduit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBproduit.SelectedIndexChanged
        If EnMiseAJourProduit Then Exit Sub
        If CBproduit.SelectedIndex < 0 Then Exit Sub

        Try
            EnMiseAJourProduit = True
            Dim nomProduit = CBproduit.Text

            open()
            cmd.CommandText = "SELECT code_prod, prix_prod FROM produit WHERE nom_prod = @nom"
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@nom", nomProduit)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                CBCodeProd.Text = dr("code_prod").ToString()
                Txtprix.Text = dr("prix_prod").ToString()
            End If

            dr.Close()
        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
        Finally
            con.Close()
            EnMiseAJourProduit = False
        End Try
    End Sub

        Private Function LireDecimal(ByVal valeur As String) As Decimal
            Dim resultat As Decimal
            If Decimal.TryParse(valeur, resultat) Then
                Return resultat
            Else
                Return 0
            End If
        End Function

        Private Function CalculerTotalLigne(ByVal quantite As Decimal, ByVal prixUnit As Decimal) As Decimal
            Return quantite * prixUnit
        End Function

End Class
