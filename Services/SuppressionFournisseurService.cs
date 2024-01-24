using Entreprise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise.Services
{


    public class SuppressionFournisseurService : ISupprimableFournisseur
    {
        private readonly LiteDbContext dbContext;

        public SuppressionFournisseurService(LiteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void SupprimerFournisseur(long idFournisseur)
        {
            var collection = dbContext.Clients;

            var fournisseurASupprimer = collection.FindOne(x => x.Id == idFournisseur);
            if (fournisseurASupprimer != null)
            {
                // Supprimez le fournisseur de la collection
                collection.Delete(idFournisseur);

                // Mettez à jour l'objet Entreprise (si nécessaire)
                var entreprise = dbContext.Entreprise;
                if (entreprise != null)
                {
                    entreprise.Clients.Remove(fournisseurASupprimer);
                    dbContext.Entreprise = entreprise;
                }
                Console.Clear();
                Console.WriteLine($"Client avec l'ID {idFournisseur} supprimé avec succès.");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Client avec l'ID {idFournisseur} non trouvé.");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
