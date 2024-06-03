using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetHopital
{
    class Hopital
    {
        private List<Salle> salles;
        private int salleActive;
        private static Hopital instance = null;
        public static Hopital Instance
        {
            get
            {
                if (instance == null)
                    instance = new Hopital();
                return instance;
            }
        }

        public Queue<Patient> FileAttente { get; private set; }
        public DaoPatient DaoPatient { get; private set; }
        public DaoVisite DaoVisite { get; private set; }
        public List<Salle> Salles { get => salles; set => salles = value; }
        public int SalleActive { get => salleActive; set => salleActive = value; }

        public void SendNewPatient()
        {
            salles[SalleActive].PatientActuel = FileAttente.Dequeue();
            Console.WriteLine("sent " + salles[SalleActive].PatientActuel + "en salle");
        }

        private Hopital()
        {
            FileAttente = new Queue<Patient>();
            salles = new List<Salle>();
        }
    }
}
