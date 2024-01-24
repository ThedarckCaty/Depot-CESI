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

        // Autres propriétés et méthodes spécifiques à la classe Produit

        public void Vendre()
        {
            // Logique de vente spécifique pour un produit
            Console.WriteLine($"Le produit {Nom} a été vendu !");
        }
    }


}
