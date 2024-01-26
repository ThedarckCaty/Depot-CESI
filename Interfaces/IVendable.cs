using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise.Interfaces
{


    public interface IVendable
    {
        void GestionAchat(Achat achat, int quantiteProduitDispo);
    }
}
