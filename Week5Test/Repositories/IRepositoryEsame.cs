using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week5Test.Entities;

namespace Week5Test.Repositories
{
    public interface IRepositoryEsame : IRepository<Esame>
    {
        public IList<Esame> EsamiPerStudente(int value);

        public void Add();
    }
}
