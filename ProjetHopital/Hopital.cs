using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetHopital
{
    class Hopital
    {

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

        private Hopital()
        {
            FileAttente = new Queue<Patient>();
        }
    }
}
