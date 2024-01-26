using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise
{
    public abstract class Personne
    {

        protected Personne()
        {

        }
        protected Personne(string nom, string prenom, string adresse, string telephone)
        {
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            Telephone = telephone;
        }

        protected Personne(int id, string nom, string prenom, string adresse, string telephone)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            Telephone = telephone;
        }

        public int Id { get; set; }

        private string _nom;
        public string Nom
        {
            get { return _nom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Le nom ne peut pas être vide.");
                _nom = value;
            }
        }
        private string _prenom;
        public string Prenom
        {
            get { return _prenom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Le prenom ne peut pas être vide.");
                _prenom = value;
            }
        }

        private string _adresse;
        public string Adresse
        {
            get { return _adresse; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("L'adresse ne peut pas être vide.");
                _adresse = value;
            }
        }

        private string _telephone;
        public string Telephone
        {
            get { return _telephone; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Le numéro de tel. ne peut pas être vide.");
                _telephone = value;
            }
        }
    }

}
