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

        [BsonRef("salaries")]
        public List<Salarie> Salaries { get; set; } = new List<Salarie>();

        [BsonRef("clients")]
        public List<Client> Clients { get; set; } = new List<Client>();

        [BsonRef("fournisseurs")]
        public List<Fournisseur> Fournisseurs { get; set; } = new List<Fournisseur>();

        [BsonRef("produits")]
        public List<Produit> Produits { get; set; } = new List<Produit>();

        [BsonRef("achats")]
        public List<Achat> Achats { get; set; } = new List<Achat>();

        private static Entreprise instance;

        public Entreprise()
        {

        }

        public static Entreprise Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Entreprise();
                }
                return instance;
            }
        }
    }
}
