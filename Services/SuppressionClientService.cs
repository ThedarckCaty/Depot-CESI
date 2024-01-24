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
        private readonly LiteDbContext dbContext;

        public SuppressionClientService(LiteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void SupprimerClient(long idClient)
        {
            var collection = dbContext.Clients;

            var clientASupprimer = collection.FindOne(x => x.Id == idClient);
            if (clientASupprimer != null)
            {
                // Supprimez le client de la collection
                collection.Delete(idClient);

                // Mettez à jour l'objet Entreprise (si nécessaire)
                var entreprise = dbContext.Entreprise;
                if (entreprise != null)
                {
                    entreprise.Clients.Remove(clientASupprimer);
                    dbContext.Entreprise = entreprise;
                }
                Console.Clear();
                Console.WriteLine($"Client avec l'ID {idClient} supprimé avec succès.");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Client avec l'ID {idClient} non trouvé.");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
