using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week5Test.Entities;

namespace Week5Test.Repositories
{
    public interface IRepositoryStudente : IRepository<Studente>
    {

        public void DisconnectedInsert(string s1, string s2, int i);

    }
}
