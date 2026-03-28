Imports System.Data
Imports System.Data.SqlClient
Module Module1
    Public con As New SqlConnection
    Public cmd As New SqlCommand




    Public dr As SqlDataReader
    Public cmdl As SqlCommand

    Public Sub open()
        Dim cs As String = "Data Source=DESKTOP-0QAL8J1;Initial Catalog=livraison;Integrated Security=True;TrustServerCertificate=True"

        If con.State = ConnectionState.Open Then
            Return
        End If

        If con.State = ConnectionState.Broken Then
            con.Close()
        End If

        If con.ConnectionString <> cs Then
            con.ConnectionString = cs
        End If

        con.Open()
        If con.State <> ConnectionState.Open Then
            MsgBox("connection échoué", MsgBoxStyle.Information, "Status")
        End If
    End Sub
    Sub chargerclients(ByVal cb As ComboBox)
        Try
            open()
            cmd.CommandText = ("SELECT prenom FROM client")
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()

            cb.Items.Clear() 'nettoyer les elements esistants

            While dr.Read()
                cb.Items.Add(dr("prenom").ToString()) 'ajouter chaque nom au comboBox
            End While

            dr.Close()
            con.Close()


        Catch ex As Exception
            MessageBox.Show("Erreur:" & ex.Message)

        End Try
    End Sub
    Sub chargerproduit(ByVal cb As ComboBox)
        Try
            open()
            cmd.CommandText = ("SELECT nom_prod FROM produit")
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()

            cb.Items.Clear() 'nettoyer les elements esistants

            While dr.Read()
                cb.Items.Add(dr("nom_prod").ToString()) 'ajouter chaque nom au comboBox
            End While

            dr.Close()
            con.Close()


        Catch ex As Exception
            MessageBox.Show("Erreur:" & ex.Message)

        End Try
    End Sub

    Public Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New Form1())
    End Sub

End Module
