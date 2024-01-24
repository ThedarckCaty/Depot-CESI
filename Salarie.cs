using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise
{
    public class Salarie : Personne
    {
        public string Departement { get; set; }
        public string Poste { get; set; }
        public float Salaire { get; set; }

        // Propriété calculée pour le matricule
        public string Matricule
        {
            get; set;
        }

    }
}

