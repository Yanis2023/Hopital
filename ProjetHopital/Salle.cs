using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetHopital
{
    class Salle
    {
        public const int quotat = 5;
        public const int prixConsult = 23;
        private int num;
        private string medecinActuel;
        private List<Visite> visitesFaites;
        private Patient patientActuel;
        private DateTime arriveePatient;
        public string MedecinActuel { get => medecinActuel; set => medecinActuel = value; }
        public int Num { get => num; }
        public Patient PatientActuel { get => patientActuel; set => patientActuel = value; }
        public DateTime ArriveePatient { get => arriveePatient; set => arriveePatient = value; }

        public Salle(int num, string nomMedecin = "")
        {
            this.num = num;
            MedecinActuel = nomMedecin;
            visitesFaites = new List<Visite>();
        }
        public void SauvegarderVisites()
        {
            DaoVisite dv = new DaoVisite();
            foreach (Visite v in visitesFaites)
                dv.Insert(v);
            visitesFaites.Clear();
        }
        public void RendreDispo()
        {
            if (patientActuel != null)
            {
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " num=" + num);
                visitesFaites.Add(new Visite(0, patientActuel.Id, medecinActuel, DateTime.Now.ToString("dd/MM/yyyy HH:mm"), num, prixConsult, DateTime.Now.Subtract(arriveePatient).TotalMinutes));
            }
            patientActuel = null;
            if (visitesFaites.Count >= quotat)
                SauvegarderVisites();
            Hopital.Instance.SendNewPatient();
        }
    }
}
