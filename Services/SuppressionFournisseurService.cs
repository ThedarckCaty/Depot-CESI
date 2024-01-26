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
            //dbContext.SupdbFournisseur(idFournisseur);
            dbContext.Execute("DELETE FROM Fournisseurs WHERE Id = @Id", new { Id = idFournisseur });


            Console.Clear();
            Console.WriteLine($"Fournisseur avec l'ID {idFournisseur} supprimé avec succès.");
        }
    }
}