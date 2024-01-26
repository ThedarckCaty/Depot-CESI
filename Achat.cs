using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise
{
    public class Achat
    {
        public int NumeroAchat { get; set; }
        public string Reference { get; set; }
        public string Nom {  get; set; }
        public int IdClient { get; set; }
        public int QuantiteProduitAchete { get; set; }
        public DateTime DateAchat { get; set; }

        public Achat()
        {

        }
        public Achat(string referenceProduit, string nomProduit, int idclient, int quantiteProduitAchete, DateTime dateAchatProduit)
        {
            Reference = referenceProduit;
            Nom = nomProduit;
            IdClient = idclient;
            QuantiteProduitAchete = quantiteProduitAchete;
            DateAchat = dateAchatProduit;
        }

        public Achat(int numeroAchat, string referenceProduit, string nomProduit, int idclient, int quantiteProduitAchete, DateTime dateAchatProduit)
        {
            NumeroAchat = numeroAchat;
            Reference = referenceProduit;
            Nom = nomProduit;
            IdClient = idclient;
            QuantiteProduitAchete = quantiteProduitAchete;
            DateAchat = dateAchatProduit;
        }
    }


}


