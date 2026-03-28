Public Class ajouter_bon_liv

    Private EnMiseAJourClient As Boolean = False
    Private EnMiseAJourProduit As Boolean = False

    Private Sub AssurerSchemaBonLivraison()
        Try
            open()
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.CommandText = _
                "IF OBJECT_ID('dbo.bon_livraison', 'U') IS NULL " & _
                "BEGIN " & _
                "CREATE TABLE bon_livraison (" & _
                "num_liv INT PRIMARY KEY, " & _
                "date_liv DATE NOT NULL, " & _
                "code_clt DECIMAL(18,0) NOT NULL, " & _
                "num_commande INT NULL" & _
                ") " & _
                "END; " & _
                "IF COL_LENGTH('dbo.bon_livraison', 'num_commande') IS NULL " & _
                "BEGIN " & _
                "ALTER TABLE bon_livraison ADD num_commande INT NULL " & _
                "END"
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Erreur preparation schema bon livraison: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub PopulerCodesClientsBL()
        Try
            Dim texteSaisi As String = CBCodeClt.Text.Trim()
            CBCodeClt.Items.Clear()

            open()
            cmd.Connection = con
            cmd.CommandText = "SELECT code_clt FROM client ORDER BY code_clt"
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            dr = cmd.ExecuteReader()

            While dr.Read()
                CBCodeClt.Items.Add(dr("code_clt").ToString())
            End While

            dr.Close()

            If texteSaisi <> "" Then
                CBCodeClt.Text = texteSaisi
                CBCodeClt.SelectionStart = CBCodeClt.Text.Length
                CBCodeClt.SelectionLength = 0
            End If
        Catch ex As Exception
            MessageBox.Show("Erreur lors du chargement des codes client: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub PopulerCodesProduitsBL()
        Try
            Dim texteSaisi As String = CBCodeProd.Text.Trim()
            CBCodeProd.Items.Clear()

            open()
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT code_prod FROM produit ORDER BY code_prod"
            dr = cmd.ExecuteReader()

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

    Private Sub PopulerCommandesBL()
        Try
            CBCommande.Items.Clear()
            open()
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT num_commande FROM commande ORDER BY num_commande DESC"
            dr = cmd.ExecuteReader()

            CBCommande.Items.Add("")  ' Add empty option for optional selection
            While dr.Read()
                CBCommande.Items.Add(dr("num_commande").ToString())
            End While

            dr.Close()
            CBCommande.SelectedIndex = 0  ' Select empty by default
        Catch ex As Exception
            ' No error display - commandes are optional
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub ChargerBonLivraisonExistants()
        Try
            DG1.Rows.Clear()

            open()
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT l.num_ligne, l.code_prod, ISNULL(p.nom_prod, '') AS nom_prod, ISNULL(p.prix_prod, 0) AS prix_prod, l.quantite, l.total_ligne FROM lignelivraison l LEFT JOIN produit p ON p.code_prod = l.code_prod ORDER BY l.num_liv, l.num_ligne"
            dr = cmd.ExecuteReader()

            While dr.Read()
                DG1.Rows.Add(dr("num_ligne").ToString(), dr("code_prod").ToString(), dr("nom_prod").ToString(), dr("prix_prod").ToString(), dr("quantite").ToString(), dr("total_ligne").ToString())
            End While

            dr.Close()
        Catch ex As Exception
            MessageBox.Show("Erreur lors du chargement des bons livraison: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub ViderChampsSaisieBL()
        Txtadresse.Text = ""
        CBCodeProd.Text = ""
        Txtligne.Text = ""
        Txt_total_liv.Text = "0"
        Txt_num_liv.Text = "1"
        Txt_nom.Text = ""
        Txtnub1.Text = ""
        Txtprix.Text = ""
        Txtqun.Text = ""
        Txttel.Text = ""
        CBCodeClt.Text = ""
        CBclt.SelectedIndex = -1
        CBproduit.SelectedIndex = -1
        InitialiserNumeroBL()
    End Sub

    Private Sub InitialiserNumeroBL()
        Try
            open()
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT ISNULL(MAX(num_liv), 0) + 1 FROM lignelivraison"
            Txtnub1.Text = cmd.ExecuteScalar().ToString()
        Catch ex As Exception
            Txtnub1.Text = "1"
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub AutoRemplirClientParCode()
        If CBCodeClt.Text.Trim() = "" Then
            EnMiseAJourClient = True
            Txt_nom.Text = ""
            Txtadresse.Text = ""
            Txttel.Text = ""
            CBclt.Text = ""
            EnMiseAJourClient = False
            Exit Sub
        End If

        Dim codeClient As Decimal
        If Not Decimal.TryParse(CBCodeClt.Text.Trim(), codeClient) Then
            EnMiseAJourClient = True
            Txt_nom.Text = ""
            Txtadresse.Text = ""
            Txttel.Text = ""
            CBclt.Text = ""
            EnMiseAJourClient = False
            Exit Sub
        End If

        Try
            Dim prenomClient As String = ""

            open()
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT nom, prenom, adresse, telephone FROM client WHERE code_clt = @code_clt"
            cmd.Parameters.AddWithValue("@code_clt", codeClient)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                prenomClient = dr("prenom").ToString()

                EnMiseAJourClient = True
                Txt_nom.Text = dr("nom").ToString()
                Txtadresse.Text = dr("adresse").ToString()
                Txttel.Text = dr("telephone").ToString()
                EnMiseAJourClient = False
            Else
                EnMiseAJourClient = True
                Txt_nom.Text = ""
                Txtadresse.Text = ""
                Txttel.Text = ""
                CBclt.Text = ""
                EnMiseAJourClient = False
            End If

            dr.Close()

            If prenomClient <> "" Then
                EnMiseAJourClient = True
                CBclt.Text = prenomClient
                EnMiseAJourClient = False
            End If
        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
        Finally
            EnMiseAJourClient = False
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub AutoRemplirProduitParCode()
        If CBCodeProd.Text.Trim() = "" Then
            EnMiseAJourProduit = True
            CBproduit.Text = ""
            Txtprix.Text = ""
            EnMiseAJourProduit = False
            Exit Sub
        End If

        Dim codeProduit As Decimal
        If Not Decimal.TryParse(CBCodeProd.Text.Trim(), codeProduit) Then
            EnMiseAJourProduit = True
            CBproduit.Text = ""
            Txtprix.Text = ""
            EnMiseAJourProduit = False
            Exit Sub
        End If

        Try
            open()
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT nom_prod, prix_prod FROM produit WHERE code_prod = @code_prod"
            cmd.Parameters.AddWithValue("@code_prod", codeProduit)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                EnMiseAJourProduit = True
                CBproduit.Text = dr("nom_prod").ToString()
                Txtprix.Text = dr("prix_prod").ToString()
                EnMiseAJourProduit = False
            Else
                EnMiseAJourProduit = True
                CBproduit.Text = ""
                Txtprix.Text = ""
                EnMiseAJourProduit = False
            End If

            dr.Close()
        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
        Finally
            EnMiseAJourProduit = False
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Function LireDecimal(ByVal valeur As String, ByRef resultat As Decimal) As Boolean
        Return Decimal.TryParse(valeur, resultat)
    End Function

    Private Sub CalculerTotalLigne()
        Dim prix As Decimal
        Dim quantite As Decimal
        If LireDecimal(Txtprix.Text.Trim(), prix) AndAlso LireDecimal(Txtqun.Text.Trim(), quantite) Then
            Txtligne.Text = (prix * quantite).ToString()
        Else
            Txtligne.Text = ""
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_total_liv.TextChanged

    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCodeClt.TextChanged

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim quantite As Decimal
        Dim totalLigne As Decimal
        Dim numLigne As Integer
        Dim codeProduit As Integer

        If CBproduit.SelectedItem Is Nothing Then
            MessageBox.Show("Veuillez choisir un produit.")
            Exit Sub
        End If

        If Txtnub1.Text.Trim() = "" Then
            InitialiserNumeroBL()
        End If

        If Not Integer.TryParse(Txt_num_liv.Text.Trim(), numLigne) Then
            MessageBox.Show("Numero de ligne invalide.")
            Txt_num_liv.Focus()
            Exit Sub
        End If

        If Not Integer.TryParse(CBCodeProd.Text.Trim(), codeProduit) Then
            MessageBox.Show("Code produit invalide.")
            CBCodeProd.Focus()
            Exit Sub
        End If

        If Not LireDecimal(Txtqun.Text.Trim(), quantite) Then
            MessageBox.Show("Quantite invalide.")
            Txtqun.Focus()
            Exit Sub
        End If

        If Not LireDecimal(Txtligne.Text.Trim(), totalLigne) Then
            MessageBox.Show("Total ligne invalide.")
            Txtligne.Focus()
            Exit Sub
        End If

        ' Stage line in grid; DB insert happens only on Ajouter BL.
        DG1.Rows.Add(numLigne, codeProduit, CBproduit.Text.Trim(), Txtprix.Text.Trim(), quantite, totalLigne)

        Txt_total_liv.Text = CDec(Txt_total_liv.Text) + CDec(Txtligne.Text)
        Txtprix.Text = ""
        CBCodeProd.Text = ""
        Txtqun.Text = ""
        Txtligne.Text = ""
        CBproduit.SelectedIndex = -1
        Txt_num_liv.Text = CInt(Txt_num_liv.Text) + 1
    End Sub

    Private Sub ajouter_bon_liv_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AssurerSchemaBonLivraison()
        chargerclients(CBclt)
        Button2.Text = "Ajouter BL"
        Button1.Text = "Ajouter Ligne"

        CBCodeClt.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        CBCodeClt.AutoCompleteSource = AutoCompleteSource.ListItems
        PopulerCodesClientsBL()

        chargerproduit(CBproduit)
        CBCodeProd.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        CBCodeProd.AutoCompleteSource = AutoCompleteSource.ListItems
        PopulerCodesProduitsBL()

        PopulerCommandesBL()

        DG1.Rows.Clear()
        InitialiserNumeroBL()

        Txt_num_liv.Text = "1"
        Txt_total_liv.Text = "0"
    End Sub

    Private Sub CBCodeClt_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCodeClt.SelectionChangeCommitted
        AutoRemplirClientParCode()
    End Sub

    Private Sub CBCodeClt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCodeClt.Click
        If CBCodeClt.Items.Count > 0 Then
            CBCodeClt.DroppedDown = True
        End If
    End Sub

    Private Sub CBCodeClt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCodeClt.Leave
        If CBCodeClt.FindStringExact(CBCodeClt.Text.Trim()) >= 0 Then
            AutoRemplirClientParCode()
        End If
    End Sub

    Private Sub CBCodeProd_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCodeProd.SelectionChangeCommitted
        AutoRemplirProduitParCode()
    End Sub

    Private Sub CBCodeProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCodeProd.Click
        If CBCodeProd.Items.Count > 0 Then
            CBCodeProd.DroppedDown = True
        End If
    End Sub

    Private Sub CBCodeProd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCodeProd.Leave
        If CBCodeProd.FindStringExact(CBCodeProd.Text.Trim()) >= 0 Then
            AutoRemplirProduitParCode()
        End If
    End Sub

    Private Sub CBclt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBclt.SelectedIndexChanged
        If EnMiseAJourClient Then
            Exit Sub
        End If

        If CBclt.SelectedItem Is Nothing Then
            Exit Sub
        End If

        Try
            open()
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT code_clt, nom, adresse, telephone FROM client WHERE prenom = @prenom"
            cmd.Parameters.AddWithValue("@prenom", CBclt.SelectedItem.ToString())
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                EnMiseAJourClient = True
                CBCodeClt.Text = dr("code_clt").ToString()
                Txt_nom.Text = dr("nom").ToString()
                Txtadresse.Text = dr("adresse").ToString()
                Txttel.Text = dr("telephone").ToString()
                EnMiseAJourClient = False
            End If

            dr.Close()
        Catch ex As Exception
            MessageBox.Show("erruerr" & ex.Message)
        Finally
            EnMiseAJourClient = False
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub CBproduit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBproduit.SelectedIndexChanged
        If EnMiseAJourProduit Then
            Exit Sub
        End If

        If CBproduit.SelectedItem Is Nothing Then
            Exit Sub
        End If

        Try
            open()
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT code_prod, prix_prod FROM produit WHERE nom_prod = @nom_prod"
            cmd.Parameters.AddWithValue("@nom_prod", CBproduit.SelectedItem.ToString())
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                EnMiseAJourProduit = True
                CBCodeProd.Text = dr("code_prod").ToString()
                Txtprix.Text = dr("prix_prod").ToString()
                EnMiseAJourProduit = False
            End If

            dr.Close()
        Catch ex As Exception
            MessageBox.Show("erruerr" & ex.Message)
        Finally
            EnMiseAJourProduit = False
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub Txtligne_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txtqun.TextChanged, Txtprix.TextChanged
        CalculerTotalLigne()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim numLiv As Integer
        Dim codeClient As Decimal
        Dim numCommande As Integer
        Dim nbLignes As Integer = 0

        For Each row As DataGridViewRow In DG1.Rows
            If Not row.IsNewRow Then
                nbLignes += 1
            End If
        Next

        If nbLignes = 0 Then
            MessageBox.Show("Veuillez d'abord ajouter au moins une ligne.")
            Exit Sub
        End If

        If Not Integer.TryParse(Txtnub1.Text.Trim(), numLiv) Then
            MessageBox.Show("Numero BL invalide.")
            Txtnub1.Focus()
            Exit Sub
        End If

        If Not Decimal.TryParse(CBCodeClt.Text.Trim(), codeClient) Then
            MessageBox.Show("Code client invalide.")
            CBCodeClt.Focus()
            Exit Sub
        End If

        If CBCommande.SelectedIndex <= 0 OrElse Not Integer.TryParse(CBCommande.SelectedItem.ToString(), numCommande) Then
            MessageBox.Show("Veuillez selectionner une commande avant d'ajouter le BL.")
            CBCommande.Focus()
            Exit Sub
        End If

        Try
            open()
            Dim trans As SqlClient.SqlTransaction = con.BeginTransaction()
            cmd.Connection = con
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text

            ' Insert BL header only now (on Ajouter BL)
            cmd.Parameters.Clear()
            cmd.CommandText = "IF NOT EXISTS (SELECT 1 FROM bon_livraison WHERE num_liv = @num_liv) " & _
                             "BEGIN INSERT INTO bon_livraison (num_liv, date_liv, code_clt, num_commande) VALUES (@num_liv, @date_liv, @code_clt, @num_commande) END " & _
                             "ELSE BEGIN UPDATE bon_livraison SET date_liv=@date_liv, code_clt=@code_clt, num_commande=@num_commande WHERE num_liv=@num_liv END"
            cmd.Parameters.AddWithValue("@num_liv", numLiv)
            cmd.Parameters.AddWithValue("@date_liv", DateTimePicker1.Value.Date)
            cmd.Parameters.AddWithValue("@code_clt", codeClient)
            cmd.Parameters.AddWithValue("@num_commande", numCommande)
            cmd.ExecuteNonQuery()

            ' Insert all staged lines
            For Each row As DataGridViewRow In DG1.Rows
                If row.IsNewRow Then
                    Continue For
                End If

                Dim ligne As Integer = Convert.ToInt32(row.Cells(0).Value)
                Dim codeProd As Integer = Convert.ToInt32(row.Cells(1).Value)
                Dim qte As Decimal = Convert.ToDecimal(row.Cells(4).Value)
                Dim total As Decimal = Convert.ToDecimal(row.Cells(5).Value)

                cmd.Parameters.Clear()
                cmd.CommandText = "INSERT INTO lignelivraison (num_ligne, quantite, total_ligne, num_liv, code_prod) VALUES (@num_ligne, @quantite, @total_ligne, @num_liv, @code_prod)"
                cmd.Parameters.AddWithValue("@num_ligne", ligne)
                cmd.Parameters.AddWithValue("@quantite", qte)
                cmd.Parameters.AddWithValue("@total_ligne", total)
                cmd.Parameters.AddWithValue("@num_liv", numLiv)
                cmd.Parameters.AddWithValue("@code_prod", codeProd)
                cmd.ExecuteNonQuery()
            Next

            trans.Commit()
            MessageBox.Show("Bon de livraison enregistre avec succes.")
            DG1.Rows.Clear()
            ViderChampsSaisieBL()

        Catch ex As Exception
            MessageBox.Show("Erreur lors de l'enregistrement du BL: " & ex.Message)
        Finally
            cmd.Transaction = Nothing
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

End Class
