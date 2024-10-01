using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace palmaEtterem0930
{
    internal class Palma
    {
        public string Nev { get; set; }
        public string Tipus { get; set; }
        public bool Verseny { get; set; }
        public int Ar { get; set; }
        public string Ertekesites { get; set; }

        public Palma(string sor)
        {
            var s = sor.Split(";");
            Nev = s[0];
            Tipus = s[1];
            Verseny = Convert.ToBoolean(s[2]);
            Ar = Convert.ToInt32(s[3]);
            Ertekesites = s[4];
        }
    }
}
