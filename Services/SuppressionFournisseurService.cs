using Entreprise.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise.Services
{


    public class SuppressionFournisseurService : ISupprimableFournisseur
    {
        private readonly SQLiteDbContext dbContext;

        public SuppressionFournisseurService(SQLiteDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void SupprimerFournisseur(long idFournisseur)
        {
            // Récupérer les références des produits associés au fournisseur
            var referencesProduits = dbContext.GetReferencesProduits(idFournisseur);

            // Supprimer le fournisseur de la table Fournisseurs
            dbContext.Execute("DELETE FROM Fournisseurs WHERE Id = @Id", new { Id = idFournisseur });

            // Supprimer les produits associés au fournisseur
            dbContext.Execute("DELETE FROM Produits WHERE IdFournisseur = @Id", new { Id = idFournisseur });

            // Supprimer les achats associés aux références des produits
            foreach (var reference in referencesProduits)
            {
                dbContext.Execute("DELETE FROM Achats WHERE Reference = @Reference", new { Reference = reference });
            }

            Console.Clear();
            Console.WriteLine($"Fournisseur avec l'ID {idFournisseur} supprimé avec succès.");
        }

    }
}