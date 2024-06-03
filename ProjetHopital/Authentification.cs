using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetHopital
{
    class Authentification
    {
        private string login;
        private string password;
        private string nom;
        private string metier;

        public Authentification() { }
        public Authentification(string login, string password, string nom, string metier)
        {
            this.Login = login;
            this.Password = password;
            this.Nom = nom;
            this.Metier = metier;
        }

        public string Login { get => login; set => login = value; }
        public string Password { get => password; set => password = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Metier { get => metier; set => metier = value; }

        public override string ToString()
        {
            string result = "";

            result += Login + " ";
            result += Nom + " ";
            result += Metier + " ";
        
            return result;
        }
    }
}
