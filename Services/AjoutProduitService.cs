using Entreprise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise.Services
{


    public class AjoutProduitService : IAjoutableProduit
    {

        private readonly SQLiteDbContext dbContext;

        public AjoutProduitService(SQLiteDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void AjouterProduit(Produit nouveauProduit)
        {
            string sql = @"
                INSERT INTO Produits (Reference, IdFournisseur, Nom, Prix, Quantite)
                VALUES (@Reference, @IdFournisseur, @Nom, @Prix, @Quantite)"
            ;

            dbContext.Execute(sql, nouveauProduit);

            Console.Clear();
            Console.WriteLine("Produit ajouté avec succès.");
            Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
