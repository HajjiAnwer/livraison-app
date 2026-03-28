<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ajouter_prod
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
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CBCodeProd = New System.Windows.Forms.ComboBox()
        Me.Txt_quantiteprod = New System.Windows.Forms.TextBox()
        Me.Txt_prixprod = New System.Windows.Forms.TextBox()
        Me.Txt_nomprod = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.DGProduit = New System.Windows.Forms.DataGridView()
        Me.ColCodeProduit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDesignation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColPrix = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DGProduit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(318, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 31)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Produit"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(80, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 18)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Code produit"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(80, 225)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 18)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Quantité"
        Me.Label2.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(80, 141)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 18)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Designation"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(80, 187)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(30, 18)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Prix"
        '
        'CBCodeProd
        '
        Me.CBCodeProd.FormattingEnabled = True
        Me.CBCodeProd.Location = New System.Drawing.Point(276, 93)
        Me.CBCodeProd.Name = "CBCodeProd"
        Me.CBCodeProd.Size = New System.Drawing.Size(140, 24)
        Me.CBCodeProd.TabIndex = 2
        '
        'Txt_quantiteprod
        '
        Me.Txt_quantiteprod.Location = New System.Drawing.Point(276, 223)
        Me.Txt_quantiteprod.Name = "Txt_quantiteprod"
        Me.Txt_quantiteprod.Size = New System.Drawing.Size(140, 22)
        Me.Txt_quantiteprod.TabIndex = 6
        Me.Txt_quantiteprod.Text = "0"
        Me.Txt_quantiteprod.Visible = False
        '
        'Txt_prixprod
        '
        Me.Txt_prixprod.Location = New System.Drawing.Point(276, 185)
        Me.Txt_prixprod.Name = "Txt_prixprod"
        Me.Txt_prixprod.Size = New System.Drawing.Size(140, 22)
        Me.Txt_prixprod.TabIndex = 5
        '
        'Txt_nomprod
        '
        Me.Txt_nomprod.Location = New System.Drawing.Point(276, 139)
        Me.Txt_nomprod.Name = "Txt_nomprod"
        Me.Txt_nomprod.Size = New System.Drawing.Size(140, 22)
        Me.Txt_nomprod.TabIndex = 4
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Cyan
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(561, 128)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(121, 44)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Ajouter"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(561, 195)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(121, 44)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "Modifier"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'DGProduit
        '
        Me.DGProduit.BackgroundColor = System.Drawing.Color.Silver
        Me.DGProduit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGProduit.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColCodeProduit, Me.ColDesignation, Me.ColPrix})
        Me.DGProduit.Location = New System.Drawing.Point(84, 281)
        Me.DGProduit.Name = "DGProduit"
        Me.DGProduit.RowHeadersWidth = 20
        Me.DGProduit.RowTemplate.Height = 24
        Me.DGProduit.Size = New System.Drawing.Size(466, 117)
        Me.DGProduit.TabIndex = 10
        '
        'ColCodeProduit
        '
        Me.ColCodeProduit.HeaderText = "Code Produit"
        Me.ColCodeProduit.Name = "ColCodeProduit"
        Me.ColCodeProduit.Width = 130
        '
        'ColDesignation
        '
        Me.ColDesignation.HeaderText = "Designation"
        Me.ColDesignation.Name = "ColDesignation"
        Me.ColDesignation.Width = 140
        '
        'ColPrix
        '
        Me.ColPrix.HeaderText = "Prix"
        Me.ColPrix.Name = "ColPrix"
        Me.ColPrix.Width = 120
        '
        'ajouter_prod
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(742, 465)
        Me.Controls.Add(Me.DGProduit)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Txt_nomprod)
        Me.Controls.Add(Me.Txt_prixprod)
        Me.Controls.Add(Me.Txt_quantiteprod)
        Me.Controls.Add(Me.CBCodeProd)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label5)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "ajouter_prod"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormProduit"
        CType(Me.DGProduit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CBCodeProd As System.Windows.Forms.ComboBox
    Friend WithEvents Txt_quantiteprod As System.Windows.Forms.TextBox
    Friend WithEvents Txt_prixprod As System.Windows.Forms.TextBox
    Friend WithEvents Txt_nomprod As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents DGProduit As System.Windows.Forms.DataGridView
    Friend WithEvents ColCodeProduit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDesignation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPrix As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
