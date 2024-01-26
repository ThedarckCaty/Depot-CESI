using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise
{
    public class Fournisseur : Personne
    {


        public int NumeroFournisseur { get; }

        public Fournisseur()
        {
        }

        public Fournisseur(int numeroFournisseur, string nom, string prenom, string adresse, string telephone)
        : base(nom, prenom, adresse, telephone)
        {
            NumeroFournisseur = numeroFournisseur;
        }
    }

}
