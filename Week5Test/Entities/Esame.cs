using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week5Test.Entities
{
    public class Esame
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int CFU { get; set; }
        public DateTime DataE { get; set; }
        public int Votazione { get; set; }
        public bool Passato { get { return Calcolo(); } }
        public int StudenteID { get; set; }

        public bool Calcolo()
        {
            if (Votazione > 17) return true;
            else return false;
        }
    }
}
