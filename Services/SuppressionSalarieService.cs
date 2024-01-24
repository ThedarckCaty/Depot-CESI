using Entreprise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise.Services
{
    public class SuppressionSalarieService : ISupprimableSalarie
    {
        private readonly LiteDbContext dbContext;

        public SuppressionSalarieService(LiteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void SupprimerSalarie(long idSalarie)
        {
            var collection = dbContext.Salaries;

            var salarieASupprimer = collection.FindOne(x => x.Id == idSalarie);
            if (salarieASupprimer != null)
            {
                // Supprimez le salarié de la collection
                collection.Delete(idSalarie);

                // Mettez à jour l'objet Entreprise (si nécessaire)
                var entreprise = dbContext.Entreprise;
                if (entreprise != null)
                {
                    entreprise.Salaries.Remove(salarieASupprimer);
                    dbContext.Entreprise = entreprise;
                }
                Console.Clear();
                Console.WriteLine($"Salarié avec l'ID {idSalarie} supprimé avec succès.");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Salarié avec l'ID {idSalarie} non trouvé.");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
