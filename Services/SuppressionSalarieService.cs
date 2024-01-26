using Entreprise.Interfaces;
using System;
using Dapper;
using System.Net.Sockets;

namespace Entreprise.Services
{
    public class SuppressionSalarieService : ISupprimableSalarie
    {
        private readonly SQLiteDbContext dbContext;

        public SuppressionSalarieService(SQLiteDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void SupprimerSalarie(long idSalarie)
        {
            dbContext.Execute("DELETE FROM Salaries WHERE Id = @Id", new { Id = idSalarie });

            Console.Clear();
            Console.WriteLine($"Salarié avec l'ID {idSalarie} supprimé avec succès.");
        }
    }
}
