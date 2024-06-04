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
        private string date;
        private string nomMedecin;
        private int numSalle;
        private decimal tarif;
        private double dureeHopital;

        public Visite() { } 
        public Visite(int idVisite, int idPatient, string nomMedecin, string date, int numSalle, decimal tarif, double dureeHopital)
        {
            this.IdVisite = idVisite;
            this.IdPatient = idPatient;
            this.NomMedecin = nomMedecin;
            this.Date = date;
            this.NumSalle = numSalle;
            this.Tarif = tarif;
            this.dureeHopital = dureeHopital;
        }

        public int IdVisite { get => idVisite; set => idVisite = value; }
        public int IdPatient { get => idPatient; set => idPatient = value; }
        public string NomMedecin { get => nomMedecin; set => nomMedecin = value; }
        public string Date { get => date; set => date = value; }
        public int NumSalle { get => numSalle; set => numSalle = value; }
        public decimal Tarif { get => tarif; set => tarif = value; }
        public double DureeHopital { get => dureeHopital; set => dureeHopital = value; }

        public override string ToString()
        {
            string result = "";

            result += IdPatient + " ";
            result += NomMedecin + " ";
            result += Date + " ";
            result += NumSalle + " ";
            result += Tarif + " ";
            result += DureeHopital + " ";

            return result;
        }
    }
}
