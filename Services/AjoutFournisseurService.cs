using Entreprise.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise.Services
{


    public class AjoutFournisseurService : IAjoutableFournisseur
    {

        private readonly SQLiteDbContext dbContext;

        public AjoutFournisseurService(SQLiteDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void AjouterFournisseur(Fournisseur nouveauFournisseur)
        {
            string sql = @"
                INSERT INTO Fournisseurs (Nom, Prenom, Adresse, Telephone, NumeroFournisseur)
                VALUES (@Nom, @Prenom, @Adresse, @Telephone, @NumeroFournisseur)"
            ;

            dbContext.Execute(sql, nouveauFournisseur);

            Console.Clear();
            Console.WriteLine("Fournisseur ajouté avec succès.");
            Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
