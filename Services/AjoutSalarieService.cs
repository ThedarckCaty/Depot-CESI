using Entreprise;
using Entreprise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise.Services
{
    public class AjoutSalarieService : IAjoutableSalarie
    {

        private readonly LiteDbContext dbContext;

        public AjoutSalarieService(LiteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AjouterSalarie(Salarie salarie)
        {
            if (salarie != null)
            {
                dbContext.Salaries.Insert(salarie);
                Console.Clear();
                Console.WriteLine($"Salarié {salarie.Prenom} {salarie.Nom} ajouté avec succès.");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Erreur : Le salarié est null.");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }

}
