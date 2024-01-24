using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise
{
    public class Client : Personne
    {
        public int NumeroClient { get; set; }
        public List<Achat> HistoriqueAchats { get; set; } = new List<Achat>();
    }

}
