using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATESTAT_LIBRARIE
{
    class Carte
    {
        public string titlu;
        public string autor;
        public double pret;
        public int cantitate;
       
        
        //methods
        public Carte()
        {
            titlu = "";
            autor = "";
            pret = 0;
            cantitate = 0;
        }
        public Carte(string t,string au,double p)
        {
            titlu = t;
            autor = au;
            pret = p;
        }
        public string InformatiiCarte()
        {
            string text;
            text = titlu + " " + autor + " pret=" + pret;
            return text;
        }
        public string InformatiiCarteCantitate()
        { 
            string text;
            text = titlu + " " + autor + " " + cantitate.ToString()+" bucati cu " + " pretul unitar: " + pret.ToString();
            return text;
        }

    }
}
