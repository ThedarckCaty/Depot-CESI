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

        public Produit()
        {

        }

        public Produit(string reference,int idfournisseur, string nom, float prix, int quantite)
        {
            Reference = reference;
            IdFournisseur = idfournisseur;
            Nom = nom;
            Prix = prix;
            Quantite = quantite;
        }


        public int Id { get; set; }
        public string Reference { get; set; }

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
        private float _prix;

        public float Prix
        {
            get { return _prix; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Le prix doit être supérieur à zéro.");
                }
                _prix = value;
            }
        }

        private int _quantite;
        public int Quantite
        {
            get { return _quantite; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("La quantité doit être supérieur ou égale a zéro.");
                }
                _quantite = value;
            }
        }


        private float _idfournisseur;

        public float IdFournisseur
        {
            get { return _idfournisseur; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("L'id fournisseur doit être supérieur à zéro.");
                }
                _idfournisseur = value;
            }
        }

        private Fournisseur _fournisseur;

        public Fournisseur Fournisseur
        {
            get { return _fournisseur; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Le fournisseur ne peut pas être nul.");
                }
                _fournisseur = value;
            }
        }


    }


}
