Public Class ajouter_clt

    Private Sub ChargerClients()
        Try
            DGClient.Rows.Clear()
            open()
            cmd.Connection = con
            cmd.CommandText = "SELECT code_clt, nom, prenom, adresse, telephone FROM client ORDER BY code_clt"
            Dim dr As SqlClient.SqlDataReader = cmd.ExecuteReader()
            
            While dr.Read()
                DGClient.Rows.Add(dr("code_clt"), dr("nom"), dr("prenom"), dr("adresse"), dr("telephone"))
            End While
            
            dr.Close()
        Catch ex As Exception
            MessageBox.Show("Erreur lors du chargement des clients: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub ajouter_clt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ChargerClients()
    End Sub

    Private Function ValiderChamps() As Boolean
        If Txt_code.Text.Trim() = "" OrElse Txt_nom.Text.Trim() = "" OrElse Txt_prenom.Text.Trim() = "" OrElse Txt_adresse.Text.Trim() = "" OrElse Txt_telephone.Text.Trim() = "" Then
            MessageBox.Show("Veuillez remplir tous les champs.")
            Return False
        End If

        Dim codeClient As Decimal
        Dim telephone As Decimal
        If Not Decimal.TryParse(Txt_code.Text.Trim(), codeClient) Then
            MessageBox.Show("Code client invalide.")
            Txt_code.Focus()
            Return False
        End If

        If Not Decimal.TryParse(Txt_telephone.Text.Trim(), telephone) Then
            MessageBox.Show("Telephone invalide.")
            Txt_telephone.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub ViderChamps()
        Txt_code.Text = ""
        Txt_nom.Text = ""
        Txt_prenom.Text = ""
        Txt_adresse.Text = ""
        Txt_telephone.Text = ""
        Txt_code.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Not ValiderChamps() Then
            Exit Sub
        End If

        Try
            open()
            cmd.Connection = con
            cmd.CommandText = "INSERT INTO client(code_clt, nom, prenom, adresse, telephone) VALUES (@code_clt, @nom, @prenom, @adresse, @telephone)"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@code_clt", CDec(Txt_code.Text.Trim()))
            cmd.Parameters.AddWithValue("@nom", Txt_nom.Text.Trim())
            cmd.Parameters.AddWithValue("@prenom", Txt_prenom.Text.Trim())
            cmd.Parameters.AddWithValue("@adresse", Txt_adresse.Text.Trim())
            cmd.Parameters.AddWithValue("@telephone", CDec(Txt_telephone.Text.Trim()))
            cmd.ExecuteNonQuery()
            MessageBox.Show("client ajouté avec succés")
            ViderChamps()
            ChargerClients()

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
            cmd.CommandText = "UPDATE client SET nom = @nom, prenom = @prenom, adresse = @adresse, telephone = @telephone WHERE code_clt = @code_clt"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@nom", Txt_nom.Text.Trim())
            cmd.Parameters.AddWithValue("@prenom", Txt_prenom.Text.Trim())
            cmd.Parameters.AddWithValue("@adresse", Txt_adresse.Text.Trim())
            cmd.Parameters.AddWithValue("@telephone", CDec(Txt_telephone.Text.Trim()))
            cmd.Parameters.AddWithValue("@code_clt", CDec(Txt_code.Text.Trim()))

            Dim lignesMaj As Integer = cmd.ExecuteNonQuery()
            If lignesMaj > 0 Then
                ChargerClients()
                MessageBox.Show("client modifié avec succés")
                ViderChamps()
            Else
                MessageBox.Show("Aucun client trouvé avec ce code.")
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