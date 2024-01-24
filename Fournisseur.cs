using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise
{
    public class Fournisseur : Personne
    {
        public int NumeroFournisseur { get; set; }
        public List<Produit> ProduitsFournis { get; set; } = new List<Produit>();
    }

}
