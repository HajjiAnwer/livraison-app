# COMMANDE – BON_LIVRAISON Association (1 to *)

## Overview
A validated COMMANDE (Order) can be delivered in one or more BON_LIVRAISON (Delivery Notes).
- **Cardinality**: 1 to *
- **Relationship**: Génération (generates)
- **Direction**: COMMANDE (1) generates (0..*) BON_LIVRAISON

## Database Schema

### Tables:
```sql
-- COMMANDE table
CREATE TABLE commande (
    num_commande INT PRIMARY KEY,
    date_commande DATE,
    code_clt VARCHAR(50),
    FOREIGN KEY (code_clt) REFERENCES client(code_clt)
);

-- BON_LIVRAISON table (with COMMANDE link)
CREATE TABLE bon_livraison (
    num_liv INT PRIMARY KEY,
    date_liv DATE,
    code_clt VARCHAR(50),
    num_commande INT NULL,  -- OPTIONAL link to COMMANDE
    FOREIGN KEY (code_clt) REFERENCES client(code_clt),
    FOREIGN KEY (num_commande) REFERENCES commande(num_commande)
);

-- LIGNECOMMANDE - Order lines
CREATE TABLE lignecommande (
    num_ligne INT PRIMARY KEY IDENTITY(1,1),
    code_prod VARCHAR(50),
    quantite DECIMAL(10,2),
    prix_unit DECIMAL(10,2),
    total_ligne DECIMAL(10,2),
    num_commande INT,
    FOREIGN KEY (code_prod) REFERENCES produit(code_prod),
    FOREIGN KEY (num_commande) REFERENCES commande(num_commande)
);

-- LIGNELIVRAISON - Delivery note lines
CREATE TABLE lignelivraison (
    num_ligne INT PRIMARY KEY IDENTITY(1,1),
    quantite DECIMAL(10,2),
    total_ligne DECIMAL(10,2),
    num_liv INT,
    code_prod VARCHAR(50),
    FOREIGN KEY (num_liv) REFERENCES bon_livraison(num_liv),
    FOREIGN KEY (code_prod) REFERENCES produit(code_prod)
);
```

## Implementation

### Form5 (COMMANDE)
- **Purpose**: Create and manage orders with multiple order lines
- **Key Features**:
  - Client selection via code dropdown (with autocomplete)
  - Product selection via code dropdown (with autocomplete)
  - Auto-fill client and product details
  - Line-by-line order entry ("Ajouter Ligne")
  - Submit order ("Passer Commande") to save to DB
  - Existing orders displayed in DataGridView
  - Auto-generated order number

### Form4 Modified (BON_LIVRAISON)
- **New Feature**: Optional COMMANDE linking
- **Changes**:
  - Added `CBCommande` ComboBox to select which COMMANDE this delivery is for
  - Added `LabelCommande` Label "Commande (Opt)" - selection is optional
  - When "Ajouter Ligne" is clicked:
    - If BL doesn't exist, creates "bon_livraison" record with:
      - num_liv (BL number)
      - date_liv (today)
      - code_clt (client code)
      - num_commande (if COMMANDE selected, optional)
    - Adds line to lignelivraison table

### Form6 (New - Manage COMMANDE-BON_LIVRAISON Relationship)
- **Purpose**: View orders and generate delivery notes from them
- **Features**:
  - Dropdown to select a COMMANDE
  - Display all lines from that order (LIGNECOMMANDE)
  - Display all delivery notes linked to that order (BON_LIVRAISON with num_commande)
  - "Générer BL" button to quickly create a new BL linked to the selected COMMANDE
  - "Rafraîchir" button to reload data
  - Shows 1-to-* relationship: 1 COMMANDE → multiple BON_LIVRAISON

## Workflow

### Order Entry (Form5):
1. Select client by code → auto-fill client details
2. Select products and quantities → add lines one by one
3. Click "Passer Commande" → saves COMMANDE and all LIGNECOMMANDE records

### Delivery Note Entry (Form4 or Form6):

**Option A - Direct Entry (Form4)**:
1. Optionally select a COMMANDE (optional)
2. Manually enter/select client and products
3. Click "Ajouter Ligne" → creates BON_LIVRAISON linked to COMMANDE (if selected)
4. Add more lines as needed

**Option B - From Existing Order (Form6)**:
1. Select COMMANDE from dropdown
2. View all order lines for context
3. View all previous delivery notes for this order
4. Click "Générer BL" → creates new BON_LIVRAISON linked to that COMMANDE
5. Go to Form4 to add line items to the new BL

## Key Design Decisions

1. **COMMANDE linkage is optional** in Form4:
   - You can create delivery notes without linking to an order
   - Or link delivery notes to existing orders for traceability

2. **Automatic BL creation** when first line added:
   - First "Ajouter Ligne" in a new BL number automatically creates bon_livraison record
   - Subsequent lines under same BL number just add to lignelivraison

3. **Immediate persistence**:
   - Orders saved immediately upon creation
   - Delivery notes created immediately when first line added
   - No temporary states or pending saves

4. **Event guards prevent cascading**:
   - Using EnMiseAJourClient and EnMiseAJourProduit flags
   - Prevents recursive updates when auto-filling dependent fields

5. **Safe database connections**:
   - All DB operations wrapped in Try...Finally
   - Guaranteed con.Close() even on errors
   - No re-entrant connection issues

## Testing Checklist

- [ ] SQL schema updated with bon_livraison.num_commande FK
- [ ] Form5 (COMMANDE) populates and saves orders
- [ ] Form4 COMMANDE dropdown populates correctly
- [ ] Form4 can create BL linked to selected COMMANDE
- [ ] Form4 can still create BL without selecting COMMANDE (optional)
- [ ] Form6 shows orders and linked delivery notes
- [ ] Form6 "Générer BL" creates new linked BL successfully
- [ ] Data persists across form reopens
- [ ] No connection errors ("ConnectionString" popup)
- [ ] Dropdowns have working autocomplete

## Files Modified/Created

- **Form5.Designer.vb** - NEW: COMMANDE form layout
- **Form5.vb** - NEW: COMMANDE business logic
- **Form5.resx** - NEW: Resources
- **Form6.Designer.vb** - NEW: Relationship manager layout
- **Form6.vb** - NEW: Relationship manager logic
- **Form6.resx** - NEW: Resources
- **Form4.Designer.vb** - MODIFIED: Added CBCommande ComboBox, LabelCommande Label
- **Form4.vb** - MODIFIED: AddedPopulerCommandesBL() method, updated Button1_Click to create linked BL records
- **Database_Schema_Updated.sql** - NEW: SQL migration script to add FK
