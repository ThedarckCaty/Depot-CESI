using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise
{
    public class Client : Personne
    {
        public int NumeroClient { get; }
        public List<Achat> HistoriqueAchats { get; set; } = new List<Achat>();

        public Client()
        {
        }

        public Client(string nom, string prenom, string adresse, string telephone, int numeroClient)
       : base(nom, prenom, adresse, telephone)
        {
            NumeroClient = numeroClient;
        }

        public Client(string nom, string prenom, string adresse, string telephone, int numeroClient, List<Achat> historiqueAchats)
        : base(nom, prenom, adresse, telephone)
        {
            NumeroClient = numeroClient;
            HistoriqueAchats = historiqueAchats;
        }
    }

}
