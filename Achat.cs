using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise
{
    public class Achat
    {
        public int NumeroAchat { get; set; }
        public List<Produit> ProduitsAchetes { get; set; } = new List<Produit>();
        public Client Client { get; set; }
        public DateTime DateAchat { get; set; }
    }

}
