<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ClientToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AjouterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProduitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AjouterProduitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BonDeLivraisonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AjouterToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CommandeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AjouterCommandeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GestionBLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClientToolStripMenuItem, Me.ProduitToolStripMenuItem, Me.BonDeLivraisonToolStripMenuItem, Me.CommandeToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(630, 28)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ClientToolStripMenuItem
        '
        Me.ClientToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AjouterToolStripMenuItem})
        Me.ClientToolStripMenuItem.Name = "ClientToolStripMenuItem"
        Me.ClientToolStripMenuItem.Size = New System.Drawing.Size(57, 24)
        Me.ClientToolStripMenuItem.Text = "client"
        '
        'AjouterToolStripMenuItem
        '
        Me.AjouterToolStripMenuItem.Name = "AjouterToolStripMenuItem"
        Me.AjouterToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.AjouterToolStripMenuItem.Text = "ajouter"
        '
        'ProduitToolStripMenuItem
        '
        Me.ProduitToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AjouterProduitToolStripMenuItem})
        Me.ProduitToolStripMenuItem.Name = "ProduitToolStripMenuItem"
        Me.ProduitToolStripMenuItem.Size = New System.Drawing.Size(70, 24)
        Me.ProduitToolStripMenuItem.Text = "produit"
        '
        'AjouterProduitToolStripMenuItem
        '
        Me.AjouterProduitToolStripMenuItem.Name = "AjouterProduitToolStripMenuItem"
        Me.AjouterProduitToolStripMenuItem.Size = New System.Drawing.Size(178, 24)
        Me.AjouterProduitToolStripMenuItem.Text = "ajouter produit"
        '
        'BonDeLivraisonToolStripMenuItem
        '
        Me.BonDeLivraisonToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AjouterToolStripMenuItem1})
        Me.BonDeLivraisonToolStripMenuItem.Name = "BonDeLivraisonToolStripMenuItem"
        Me.BonDeLivraisonToolStripMenuItem.Size = New System.Drawing.Size(127, 24)
        Me.BonDeLivraisonToolStripMenuItem.Text = "bon de livraison"
        '
        'AjouterToolStripMenuItem1
        '
        Me.AjouterToolStripMenuItem1.Name = "AjouterToolStripMenuItem1"
        Me.AjouterToolStripMenuItem1.Size = New System.Drawing.Size(152, 24)
        Me.AjouterToolStripMenuItem1.Text = "ajouter"
        '
        'CommandeToolStripMenuItem
        '
        Me.CommandeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AjouterCommandeToolStripMenuItem, Me.GestionBLToolStripMenuItem})
        Me.CommandeToolStripMenuItem.Name = "CommandeToolStripMenuItem"
        Me.CommandeToolStripMenuItem.Size = New System.Drawing.Size(95, 24)
        Me.CommandeToolStripMenuItem.Text = "commande"
        '
        'AjouterCommandeToolStripMenuItem
        '
        Me.AjouterCommandeToolStripMenuItem.Name = "AjouterCommandeToolStripMenuItem"
        Me.AjouterCommandeToolStripMenuItem.Size = New System.Drawing.Size(132, 24)
        Me.AjouterCommandeToolStripMenuItem.Text = "ajouter"
        '
        'GestionBLToolStripMenuItem
        '
        Me.GestionBLToolStripMenuItem.Name = "GestionBLToolStripMenuItem"
        Me.GestionBLToolStripMenuItem.Size = New System.Drawing.Size(143, 24)
        Me.GestionBLToolStripMenuItem.Text = "gestion BL"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(630, 475)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ClientToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AjouterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProduitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AjouterProduitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BonDeLivraisonToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AjouterToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CommandeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AjouterCommandeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GestionBLToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
