using Entreprise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise
{
    public class Produit
    {
        public string Reference { get; set; }
        public string Nom { get; set; }
        public float Prix { get; set; }
        public Fournisseur Fournisseur { get; set; }
    }


}
