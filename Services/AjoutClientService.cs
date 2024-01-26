using Entreprise.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise.Services
{



    public class AjoutClientService : IAjoutableClient
    {

        private readonly SQLiteDbContext dbContext;

        public AjoutClientService(SQLiteDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void AjouterClient(Client nouveauClient)
        {
            string sql = @"
                INSERT INTO Clients (Nom, Prenom, Adresse, Telephone, NumeroClient)
                VALUES (@Nom, @Prenom, @Adresse, @Telephone, @NumeroClient)"
            ;

            dbContext.Execute(sql, nouveauClient);

            Console.Clear();
            Console.WriteLine("Client ajouté avec succès.");
            Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu principal...");
            Console.ReadLine();
            Console.Clear();
        }
    }

}
