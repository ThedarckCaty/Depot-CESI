using Entreprise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise.Services
{



    public class AjoutClientService : IAjoutableClient
    {

        private readonly LiteDbContext dbContext;

        public AjoutClientService(LiteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AjouterClient(Client client)
        {
            if (client != null)
            {
                dbContext.Clients.Insert(client);
                Console.Clear();
                Console.WriteLine($"Client {client.Prenom} {client.Nom} ajouté avec succès.");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Erreur : Le client est null.");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }

}
