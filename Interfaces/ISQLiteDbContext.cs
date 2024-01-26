using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entreprise.Interfaces
{
    public interface ISQLiteDbContext
    {
        void Execute(string sql, object parameters);
    }

}
