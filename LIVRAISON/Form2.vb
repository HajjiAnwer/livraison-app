Imports System.Collections.Generic

Public Class ajouter_clt

    Private TousLesCodesClients As New List(Of String)()

    Private Sub PopulerCodesClients()
        Try
            Dim texteSaisi As String = CBCode.Text.Trim()

            CBCode.Items.Clear()
            TousLesCodesClients.Clear()
            open()
            cmd.Connection = con
            cmd.CommandText = "SELECT code_clt FROM client ORDER BY code_clt"
            Dim dr As SqlClient.SqlDataReader = cmd.ExecuteReader()
            
            While dr.Read()
                Dim code As String = dr("code_clt").ToString()
                TousLesCodesClients.Add(code)
                CBCode.Items.Add(code)
            End While
            
            dr.Close()

            If texteSaisi <> "" Then
                CBCode.Text = texteSaisi
                CBCode.SelectionStart = CBCode.Text.Length
                CBCode.SelectionLength = 0
            End If
        Catch ex As Exception
            MessageBox.Show("Erreur lors du chargement des codes: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub AutoRemplirChamps()
        If CBCode.Text = "" Then
            Txt_nom.Text = ""
            Txt_prenom.Text = ""
            Txt_adresse.Text = ""
            Txt_telephone.Text = ""
            Exit Sub
        End If

        Dim codeClient As Decimal
        If Not Decimal.TryParse(CBCode.Text.Trim(), codeClient) Then
            Txt_nom.Text = ""
            Txt_prenom.Text = ""
            Txt_adresse.Text = ""
            Txt_telephone.Text = ""
            Exit Sub
        End If

        Try
            open()
            cmd.Connection = con
            cmd.CommandText = "SELECT code_clt, nom, prenom, adresse, telephone FROM client WHERE code_clt = @code_clt"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@code_clt", codeClient)
            Dim dr As SqlClient.SqlDataReader = cmd.ExecuteReader()
            
            If dr.Read() Then
                Txt_nom.Text = dr("nom").ToString()
                Txt_prenom.Text = dr("prenom").ToString()
                Txt_adresse.Text = dr("adresse").ToString()
                Txt_telephone.Text = dr("telephone").ToString()
            Else
                Txt_nom.Text = ""
                Txt_prenom.Text = ""
                Txt_adresse.Text = ""
                Txt_telephone.Text = ""
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

    Private Sub CBCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCode.SelectionChangeCommitted
        AutoRemplirChamps()
    End Sub

    Private Sub CBCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCode.Click
        If CBCode.Items.Count > 0 Then
            CBCode.DroppedDown = True
        End If
    End Sub

    Private Sub CBCode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCode.Leave
        If CBCode.FindStringExact(CBCode.Text.Trim()) >= 0 Then
            AutoRemplirChamps()
        End If
    End Sub

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
        CBCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        CBCode.AutoCompleteSource = AutoCompleteSource.ListItems
        PopulerCodesClients()
        ChargerClients()
    End Sub

    Private Function ValiderChamps() As Boolean
        If CBCode.Text.Trim() = "" OrElse Txt_nom.Text.Trim() = "" OrElse Txt_prenom.Text.Trim() = "" OrElse Txt_adresse.Text.Trim() = "" OrElse Txt_telephone.Text.Trim() = "" Then
            MessageBox.Show("Veuillez remplir tous les champs.")
            Return False
        End If

        Dim codeClient As Decimal
        Dim telephone As Decimal
        If Not Decimal.TryParse(CBCode.Text.Trim(), codeClient) Then
            MessageBox.Show("Code client invalide.")
            CBCode.Focus()
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
        CBCode.Text = ""
        Txt_nom.Text = ""
        Txt_prenom.Text = ""
        Txt_adresse.Text = ""
        Txt_telephone.Text = ""
        CBCode.Focus()
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
            cmd.Parameters.AddWithValue("@code_clt", CDec(CBCode.Text.Trim()))
            cmd.Parameters.AddWithValue("@nom", Txt_nom.Text.Trim())
            cmd.Parameters.AddWithValue("@prenom", Txt_prenom.Text.Trim())
            cmd.Parameters.AddWithValue("@adresse", Txt_adresse.Text.Trim())
            cmd.Parameters.AddWithValue("@telephone", CDec(Txt_telephone.Text.Trim()))
            cmd.ExecuteNonQuery()
            MessageBox.Show("client ajouté avec succés")
            ViderChamps()

        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try

        ' Refresh data after connection is closed
        Try
            PopulerCodesClients()
            ChargerClients()
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
            cmd.CommandText = "UPDATE client SET nom = @nom, prenom = @prenom, adresse = @adresse, telephone = @telephone WHERE code_clt = @code_clt"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@nom", Txt_nom.Text.Trim())
            cmd.Parameters.AddWithValue("@prenom", Txt_prenom.Text.Trim())
            cmd.Parameters.AddWithValue("@adresse", Txt_adresse.Text.Trim())
            cmd.Parameters.AddWithValue("@telephone", CDec(Txt_telephone.Text.Trim()))
            cmd.Parameters.AddWithValue("@code_clt", CDec(CBCode.Text.Trim()))

            Dim lignesMaj As Integer = cmd.ExecuteNonQuery()
            If lignesMaj > 0 Then
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

        ' Refresh data after connection is closed
        Try
            PopulerCodesClients()
            ChargerClients()
        Catch ex As Exception
            MessageBox.Show("Erreur lors de la mise à jour de la liste: " & ex.Message)
        End Try
    End Sub
End Class