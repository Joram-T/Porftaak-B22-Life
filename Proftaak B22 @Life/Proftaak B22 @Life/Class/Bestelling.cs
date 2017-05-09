using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life.Class
{
    class Bestelling
    {
        public int Id { get; }
        public int Klant { get; }
        public int Medewerker { get; }
        public DateTime Besteldatum { get; }
        public DateTime Leverdatum { get; set; }
        public DateTime Betaaldatum { get; set; }

     public Bestelling(int id, int klant, int medewerker, DateTime besteldatum, DateTime leverdatum, DateTime betaaldatum)
        {
            this.Id = id;
            this.Klant = klant;
            this.Medewerker = medewerker;
            this.Besteldatum = besteldatum;
            this.Leverdatum = leverdatum;
            this.Betaaldatum = betaaldatum;
        }

     public Bestelling(int id, int klant, int medewerker, DateTime besteldatum, DateTime leverdatum)
        {
            this.Id = id;
            this.Klant = klant;
            this.Medewerker = medewerker;
            this.Besteldatum = besteldatum;
            this.Leverdatum = leverdatum;
        }

     public Bestelling(int id, int klant, int medewerker, DateTime besteldatum)
        {
            this.Id = id;
            this.Klant = klant;
            this.Medewerker = medewerker;
            this.Besteldatum = besteldatum;
        }

        public override string ToString()
        {
            return this.Id.ToString()+ ", " + Besteldatum.ToString("dd/MM/yyyy"); ;
        }
    }
}
