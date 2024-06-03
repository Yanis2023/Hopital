using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetHopital
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue à l'hopital");
            Login();
        }

        static void Login()
        {
            Console.WriteLine("login:");
            string login = Console.ReadLine();
            Console.WriteLine("mdp:");
            string mdp = Console.ReadLine();
            string nom;
            int metier;
            if (DaoAuthentification.Login(login, mdp, out nom, out metier))
            {
                if (metier == 0) //secretaire
                    InterfaceSecretaire();
                else //medecin
                    InterfaceMedecin();
            }
            else
                Console.WriteLine("Erreur de login ou de mot de passe");
            Login();
        }

        static void InterfaceSecretaire()
        {
            Console.WriteLine("Bienvenue dans l'interface Secrétaire\n______________________________________");
            int choix = -1;
            while (choix != 8)
            {
                Console.WriteLine("1 - Rajouter un patient\n2 - Sauvegarder la liste d'attente\n3 - Charger la liste d'attente\n" +
                    "4 - Nouvelle journée\n5 - Afficher les visites d'un patient\n6 - Afficher toutes les visites\n" +
                    "7 - Afficher toutes les visites d'un médecin\n8 - Quitter l'interface secrétaire\nVeuillez entrer votre choix: ");
                while (!Int32.TryParse(Console.ReadLine(), out choix) && (choix < 1 || choix > 8)) ;
                if (choix == 1)
                    AjouterPatient();
                //do redirections there
            }
            Console.WriteLine("Fermeture interface Secrétaire");
        }

        static void AjouterPatient()
        {
            Console.WriteLine("Veuillez saisir un identifiant:");
            int id;
            while (!Int32.TryParse(Console.ReadLine(), out id));
            Patient p = (new DaoPatient()).SelectById(id);
            //TODO check si id existe dans la db
            if (p.Id == id)
                Console.WriteLine("Patient: [" + p + "]");
            else
            {
                Console.WriteLine("Veuillez saisir le nom:");
                p.Nom = Console.ReadLine();
                Console.WriteLine("Veuillez saisir le prénom:");
                p.Prenom = Console.ReadLine();
                Console.WriteLine("Veuillez saisir l'age du patient");
                int age;
                while (!Int32.TryParse(Console.ReadLine(), out age) && age < 0) ;
                p.Age = age;
                Console.WriteLine("Voulez vous renseigner l'adresse et le numéro de téléphone du patient ? o/n");
                char choice;
                while (!Char.TryParse(Console.ReadLine(), out choice)) ;
                if (choice == 'o' || choice == 'O')
                {
                    Console.WriteLine("Veuillez saisir l'adresse");
                    p.Adresse = Console.ReadLine();
                    Console.WriteLine("Veuillez saisir le numéro de téléphone");
                    int tel;
                    while (!Int32.TryParse(Console.ReadLine(), out tel) && tel.ToString().Length != 9) ;
                    p.Telephone = "0" + tel.ToString();
                }
                else
                {
                    p.Adresse = "";
                    p.Telephone = "";
                }
                (new DaoPatient()).Insert(p);
            }
        }

        //interface Médecin

        static void InterfaceMedecin()
        {
            Console.WriteLine("Bienvenue dans l'interface Médecin\n___________________________________");
            int choix = -1;
            while (choix != 5)
            {
                Console.WriteLine(@"1 - Afficher l'état de la file d'attente\n
                2 - Ajouter une ordonnance au patient actuel\n
                3 - Sauvegarde de la BDD de la liste des visites\n
                4 - Rendre la salle disponible\n
                5 - Quitter l'interface médecin\n
                Veuillez entrer votre choix: ");
            while (!Int32.TryParse(Console.ReadLine(), out choix) && (choix < 1 || choix > 5)) ;
                //do redirections there
            }
            Console.WriteLine("Fermeture interface Médecin");
        }
    }
}
