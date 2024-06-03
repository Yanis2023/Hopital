using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetHopital
{
    class Visite
    {
        private int idVisite;
        private int idPatient;
        private string nomMedecin;
        private string date;
        private int numSalle;
        private double tarif;

        public Visite() { } 
        public Visite(int idVisite, int idPatient, string nomMedecin, string date, int numSalle, double tarif)
        {
            this.idVisite = idVisite;
            this.idPatient = idPatient;
            this.nomMedecin = nomMedecin;
            this.date = date;
            this.numSalle = numSalle;
            this.tarif = tarif;
        }

        public override string ToString()
        {
            string result = "";

            result += idPatient + " ";
            result += nomMedecin + " ";
            result += date + " ";
            result += numSalle + " ";
            result += tarif + " ";

            return result;
        }
    }
}
