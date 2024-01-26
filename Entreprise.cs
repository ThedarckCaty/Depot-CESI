using LiteDB;
using System.Collections.Generic;

namespace Entreprise
{
    public class Entreprise
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string SIRET { get; set; }
        public string Adresse { get; set; }

        

        public Entreprise()
        {
            Id = 1;
            Nom = "CESI - (Cesi)";
            SIRET = "77572257201109";
            Adresse = "TOUR PB5, 1 AVENUE DU GENERAL DE GAULLE, 92800 PUTEAUX";
        }
    }
}
