using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetHopital
{
    class Patient
    {
        private int id;
        private string nom;
        private string prenom;
        private int age;
        private string adresse;
        private string telephone;

        public Patient() { }
        public Patient(int id, string nom, string prenom, int age, string adresse, string telephone)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.age = age;
            this.adresse = adresse;
            this.telephone = telephone;
        }

        public override string ToString()
        {
            string result = "";

            result += nom + " ";
            result += prenom + " ";
            result += age + " ";
            result += adresse + " ";
            result += telephone + " ";

            return result;
        }
    }
}
