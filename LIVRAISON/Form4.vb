Public Class ajouter_bon_liv

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

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_total_liv.TextChanged

    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_code.TextChanged

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim quantite As Decimal
        Dim totalLigne As Decimal

        If CBproduit.SelectedItem Is Nothing Then
            MessageBox.Show("Veuillez choisir un produit.")
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

        Dim row As String() = {Txt_num_liv.Text.Trim(), Txtcodea.Text.Trim(), CBproduit.SelectedItem.ToString(), Txtprix.Text.Trim(), Txtqun.Text.Trim(), Txtligne.Text.Trim()}
        DG1.Rows.Add(row)
        Txt_total_liv.Text = CDbl(Txt_total_liv.Text) + CDbl(Txtligne.Text)
        Txtprix.Text = ""
        Txtcodea.Text = ""
        Txtqun.Text = ""
        Txtligne.Text = ""
        Txt_num_liv.Text = CInt(Txt_num_liv.Text) + 1

    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ajouter_bon_liv_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        chargerclients(CBclt)
        chargerproduit(CBproduit)
        Txt_num_liv.Text = 1
        Txt_total_liv.Text = 0
    End Sub

    Private Sub CBclt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBclt.SelectedIndexChanged
        Try
            open()
            cmd.Parameters.Clear()

            cmd.CommandText = ("SELECT code_clt, nom, adresse, telephone FROM client WHERE prenom = @prenom")
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@prenom", CBclt.SelectedItem.ToString())
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                Txt_code.Text = dr("code_clt").ToString()
                Txt_nom.Text = dr("nom").ToString()
                Txtadresse.Text = dr("adresse").ToString()
                Txttel.Text = dr("telephone").ToString()


            End If
            dr.Close()
        Catch ex As Exception
            MessageBox.Show("erruerr" & ex.Message)
        End Try
        con.Close()
    End Sub

    Private Sub CBproduit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBproduit.SelectedIndexChanged
        Try
            open()
            cmd.Parameters.Clear()

            cmd.CommandText = ("SELECT code_prod, prix_prod FROM produit WHERE nom_prod  = @nom_prod")
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@nom_prod", CBproduit.SelectedItem.ToString())
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                Txtcodea.Text = dr("code_prod").ToString()
                Txtprix.Text = dr("prix_prod").ToString()
               

            End If
            dr.Close()
        Catch ex As Exception
            MessageBox.Show("erruerr" & ex.Message)
        End Try
        con.Close()
    End Sub

    Private Sub Txtligne_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txtqun.TextChanged, Txtprix.TextChanged
        CalculerTotalLigne()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If DG1.Rows.Count = 0 OrElse (DG1.Rows.Count = 1 AndAlso DG1.Rows(0).IsNewRow) Then
            MessageBox.Show("Ajoutez au moins une ligne avant de sauvegarder.")
            Exit Sub
        End If

        If Txtnub1.Text.Trim() = "" Then
            MessageBox.Show("Veuillez saisir le numero BL.")
            Txtnub1.Focus()
            Exit Sub
        End If

        open()
        cmd.CommandText = "INSERT INTO lignelivraison (num_ligne, quantite, total_ligne, num_liv, code_prod) Values(@num_ligne, @quantite, @total_ligne, @num_liv, @code_prod)"
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        Try
            For Each row As DataGridViewRow In DG1.Rows
                If Not row.IsNewRow Then
                    cmd.Parameters.Clear()


                    cmd.Parameters.AddWithValue("@num_ligne", CInt(row.Cells(0).Value))
                    cmd.Parameters.AddWithValue("@quantite", CDec(row.Cells(4).Value))
                    cmd.Parameters.AddWithValue("@total_ligne", CDec(row.Cells(5).Value))
                    cmd.Parameters.AddWithValue("@num_liv", CInt(Txtnub1.Text.Trim()))
                    cmd.Parameters.AddWithValue("@code_prod", CInt(row.Cells(1).Value))
                    cmd.ExecuteNonQuery()

                End If
            Next
            MessageBox.Show("Commande enregistree avec succes.")
        Catch ex As Exception
            MessageBox.Show("Erreur:" & ex.Message)

            Exit Sub
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try

        DG1.Rows.Clear()
        Txtadresse.Text = ""
        Txtcodea.Text = ""
        Txtligne.Text = ""
        Txt_total_liv.Text = "0"
        Txt_num_liv.Text = "1"
        Txt_nom.Text = ""
        Txtnub1.Text = ""
        Txtprix.Text = ""
        Txtqun.Text = ""
        Txttel.Text = ""
        Txt_code.Text = ""
        CBclt.SelectedIndex = -1
        CBproduit.SelectedIndex = -1


    End Sub

End Class