using Entreprise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise.Services
{


    public class AchatProduitService : IAcheteableProduit
    {

        private readonly SQLiteDbContext dbContext;

        public AchatProduitService(SQLiteDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void AcheterProduit(Achat achat)
        {
            string sql = @"
                INSERT INTO Achats (NumeroAchat, Reference, Nom, IdClient, QuantiteProduitAchete,DateAchat)
                VALUES (NULL, @Reference, @Nom, @IdClient, @QuantiteProduitAchete, @DateAchat)"
            ;

            dbContext.Execute(sql, achat);
            string sql2 = "UPDATE Achats SET NumeroAchat = last_insert_rowid() WHERE Id = last_insert_rowid();";
            dbContext.Execute(sql2, null);
        }
    }
}