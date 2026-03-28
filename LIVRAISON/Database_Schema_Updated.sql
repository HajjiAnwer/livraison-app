-- SQL Schema Updates for COMMANDE - BON_LIVRAISON Association (1 to *)
-- This establishes the relationship: One COMMANDE can generate multiple BON_LIVRAISON records

-- ALTER TABLE bon_livraison - Add foreign key to link to COMMANDE
-- (Run this in SQL Server Management Studio)

-- Step 1: Add column to bon_livraison table if not exists
ALTER TABLE bon_livraison
ADD num_commande INT NULL;

-- Step 2: Add foreign key constraint
ALTER TABLE bon_livraison
ADD CONSTRAINT FK_BL_Commande 
FOREIGN KEY (num_commande) REFERENCES commande(num_commande);

-- Verify the schema:
-- SELECT * FROM bon_livraison;
-- SELECT * FROM commande;
-- SELECT * FROM lignelivraison;
-- SELECT * FROM lignecommande;

-- Expected relationship:
-- COMMANDE (1) ---> (0..*) BON_LIVRAISON
-- COMMANDE.num_commande (PK) ---> BON_LIVRAISON.num_commande (FK)
