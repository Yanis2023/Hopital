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

        public Queue<Tuple<Patient, DateTime>> FileAttente { get; private set; }
        public DaoPatient DaoPatient { get; private set; }
        public DaoVisite DaoVisite { get; private set; }
        public List<Salle> Salles { get => salles; set => salles = value; }
        public int SalleActive { get => salleActive; set => salleActive = value; }

        public void SendNewPatient()
        {
            if (FileAttente.Count > 0)
            {
                Tuple<Patient, DateTime> tuple = FileAttente.Dequeue();
                salles[SalleActive].PatientActuel = tuple.Item1;
                salles[SalleActive].ArriveePatient = tuple.Item2;
                Console.WriteLine("sent " + salles[SalleActive].PatientActuel + "en salle");
            }
            else
                Console.WriteLine("Plus de patient dans la file d'attente");
        }

        private Hopital()
        {
            FileAttente = new Queue<Tuple<Patient, DateTime>>();
            salles = new List<Salle>();
        }
    }
}
