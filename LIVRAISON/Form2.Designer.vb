<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ajouter_clt
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CBCode = New System.Windows.Forms.ComboBox()
        Me.Txt_nom = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_prenom = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_adresse = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Txt_telephone = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.DGClient = New System.Windows.Forms.DataGridView()
        Me.ColNumClient = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNom = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColPrenom = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColAdresse = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColTelephone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DGClient, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(324, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 31)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Client"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(83, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 18)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Num Client"
        '
        'CBCode
        '
        Me.CBCode.FormattingEnabled = True
        Me.CBCode.Location = New System.Drawing.Point(274, 93)
        Me.CBCode.Name = "CBCode"
        Me.CBCode.Size = New System.Drawing.Size(140, 24)
        Me.CBCode.TabIndex = 2
        '
        'Txt_nom
        '
        Me.Txt_nom.Location = New System.Drawing.Point(274, 137)
        Me.Txt_nom.Name = "Txt_nom"
        Me.Txt_nom.Size = New System.Drawing.Size(140, 22)
        Me.Txt_nom.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(83, 139)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 18)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Nom"
        '
        'Txt_prenom
        '
        Me.Txt_prenom.Location = New System.Drawing.Point(274, 181)
        Me.Txt_prenom.Name = "Txt_prenom"
        Me.Txt_prenom.Size = New System.Drawing.Size(140, 22)
        Me.Txt_prenom.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(83, 183)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 18)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Prénom"
        '
        'Txt_adresse
        '
        Me.Txt_adresse.Location = New System.Drawing.Point(274, 225)
        Me.Txt_adresse.Name = "Txt_adresse"
        Me.Txt_adresse.Size = New System.Drawing.Size(140, 22)
        Me.Txt_adresse.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(83, 227)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 18)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Adresse"
        '
        'Txt_telephone
        '
        Me.Txt_telephone.Location = New System.Drawing.Point(274, 269)
        Me.Txt_telephone.Name = "Txt_telephone"
        Me.Txt_telephone.Size = New System.Drawing.Size(140, 22)
        Me.Txt_telephone.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(83, 271)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 18)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Téléphone"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Cyan
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(563, 128)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(121, 44)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Ajouter"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(563, 195)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(121, 44)
        Me.Button2.TabIndex = 12
        Me.Button2.Text = "Modifier"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'DGClient
        '
        Me.DGClient.BackgroundColor = System.Drawing.Color.Silver
        Me.DGClient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGClient.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColNumClient, Me.ColNom, Me.ColPrenom, Me.ColAdresse, Me.ColTelephone})
        Me.DGClient.Location = New System.Drawing.Point(47, 323)
        Me.DGClient.Name = "DGClient"
        Me.DGClient.RowHeadersWidth = 20
        Me.DGClient.RowTemplate.Height = 24
        Me.DGClient.Size = New System.Drawing.Size(637, 117)
        Me.DGClient.TabIndex = 13
        '
        'ColNumClient
        '
        Me.ColNumClient.HeaderText = "Num Client"
        Me.ColNumClient.Name = "ColNumClient"
        Me.ColNumClient.Width = 115
        '
        'ColNom
        '
        Me.ColNom.HeaderText = "Nom"
        Me.ColNom.Name = "ColNom"
        Me.ColNom.Width = 110
        '
        'ColPrenom
        '
        Me.ColPrenom.HeaderText = "Prénom"
        Me.ColPrenom.Name = "ColPrenom"
        Me.ColPrenom.Width = 110
        '
        'ColAdresse
        '
        Me.ColAdresse.HeaderText = "Adresse"
        Me.ColAdresse.Name = "ColAdresse"
        Me.ColAdresse.Width = 140
        '
        'ColTelephone
        '
        Me.ColTelephone.HeaderText = "Téléphone"
        Me.ColTelephone.Name = "ColTelephone"
        Me.ColTelephone.Width = 120
        '
        'ajouter_clt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(742, 465)
        Me.Controls.Add(Me.DGClient)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Txt_telephone)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Txt_adresse)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Txt_prenom)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Txt_nom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CBCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "ajouter_clt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormClient"
        CType(Me.DGClient, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CBCode As System.Windows.Forms.ComboBox
    Friend WithEvents Txt_nom As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Txt_prenom As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Txt_adresse As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Txt_telephone As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents DGClient As System.Windows.Forms.DataGridView
    Friend WithEvents ColNumClient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColNom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPrenom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColAdresse As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColTelephone As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
