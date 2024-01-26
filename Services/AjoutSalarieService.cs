using Entreprise.Interfaces;
using System;
using Dapper;
using System.Data.Common;

namespace Entreprise.Services
{
    public class AjoutSalarieService : IAjoutableSalarie
    {
        private readonly SQLiteDbContext dbContext;

        public AjoutSalarieService(SQLiteDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void AjouterSalarie(Salarie nouveauSalarie)
        {
            string sql = @"
                INSERT INTO Salaries (Nom, Prenom, Adresse, Telephone, Departement, Poste, Salaire, Matricule)
                VALUES (@Nom, @Prenom, @Adresse, @Telephone, @Departement, @Poste, @Salaire, @Matricule)"
            ;

            dbContext.Execute(sql, nouveauSalarie);

            Console.Clear();
            Console.WriteLine("Salarié ajouté avec succès.");
            Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
