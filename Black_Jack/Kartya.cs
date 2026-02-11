using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKT_bunbarlang.Black_Jack
{
    public class Kartya
    {
        private string nev;
        private string szin;
        private int ertek;

        public Kartya(string nev, string szin, int ertek)
        {
            this.nev = nev;
            this.szin = szin;
            this.ertek = ertek;
        }

        public string Nev { get => nev; set => nev = value; }
        public string Szin { get => szin; set => szin = value; }
        public int Ertek { get => ertek; set => ertek = value; }

        public override string ToString()
        {
            return $"{this.nev}";
        }
    }
}
