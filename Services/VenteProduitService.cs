using Entreprise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise.Services
{


    public class VenteProduitService : IVendable
    {

        private readonly SQLiteDbContext dbContext;

        public VenteProduitService(SQLiteDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void GestionAchat(Achat achat, int quantiteProduitDispo)
        {
            int nouvelleQuantite = (quantiteProduitDispo - achat.QuantiteProduitAchete);
            string reference = achat.Reference;
            Console.WriteLine(reference);
            Console.ReadLine();

            // Utiliser un paramètre pour éviter les problèmes de sécurité (SQL injection)
            string sql2 = "UPDATE Produits SET Quantite = @NouvelleQuantite WHERE Reference = @Reference;";

            // Utiliser un objet anonyme pour fournir les valeurs des paramètres
            dbContext.Execute(sql2, new { NouvelleQuantite = nouvelleQuantite, Reference = reference });

            Console.Clear();
            Console.WriteLine("Achat réalisé avec succès.");
            Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
            Console.ReadLine();
            Console.Clear();
        }

    }
}
