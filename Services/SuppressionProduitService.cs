using Entreprise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Entreprise.Services
{


    public class SuppressionProduitService : ISupprimableProduit
    {
        private readonly SQLiteDbContext dbContext;

        public SuppressionProduitService(SQLiteDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void SupprimerProduit(long idProduit)
{
            // Récupérer la référence en fonction de l'idProduit
            string reference = dbContext.GetReferenceById(idProduit);

    if (reference != null)
    {
        // Supprimer le produit de la table Produits
        dbContext.Execute("DELETE FROM Produits WHERE Id = @Id", new { Id = idProduit });

        // Supprimer les achats associés à la référence dans la table Achats
        dbContext.Execute("DELETE FROM Achats WHERE Reference = @Reference", new { Reference = reference });

        Console.Clear();
        Console.WriteLine($"Produit avec l'ID {idProduit} supprimé avec succès.");
    }
    else
    {
        Console.Clear();
        Console.WriteLine($"Aucun produit trouvé avec l'ID {idProduit}.");
    }
}

    }
}
