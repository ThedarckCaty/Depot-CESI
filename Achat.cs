using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise
{
    public class Achat
    {
        public int NumeroAchat { get; }
        public List<Produit> ProduitsAchetes { get; set; } = new List<Produit>();
        public Client Client { get; set; }
        public DateTime DateAchat { get; set; }

        public Achat(int numeroAchat, List<Produit> produitsAchetes, Client client, DateTime dateAchat)
        {
            NumeroAchat = numeroAchat;
            ProduitsAchetes = produitsAchetes ?? new List<Produit>();
            Client = client;
            DateAchat = dateAchat;
        }
    }


}
