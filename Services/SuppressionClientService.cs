using Entreprise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise.Services
{

    public class SuppressionClientService : ISupprimableClient
    {
        private readonly SQLiteDbContext dbContext;

        public SuppressionClientService(SQLiteDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void SupprimerClient(long idClient)
        {
            dbContext.Execute("DELETE FROM Clients WHERE Id = @Id", new { Id = idClient });

            Console.Clear();
            Console.WriteLine($"Client avec l'ID {idClient} supprimé avec succès.");
        }
    }
}
