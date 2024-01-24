using Entreprise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise.Services
{


    public class AjoutFournisseurService : IAjoutableFournisseur
    {

        private readonly LiteDbContext dbContext;

        public AjoutFournisseurService(LiteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AjouterFournisseur(Fournisseur fournisseur)
        {
            if (fournisseur != null)
            {
                dbContext.Fournisseurs.Insert(fournisseur);
                Console.Clear();
                Console.WriteLine($"Fournisseur {fournisseur.Prenom} {fournisseur.Nom} ajouté avec succès.");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Erreur : Le fournisseur est null.");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
