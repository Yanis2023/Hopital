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
            //TODO DAO LINK
            int authResult = 1; //validation retourne le numero de job ou -1 si echec
            if (authResult == 1) //secretaire
                InterfaceSecretaire();
            else if (authResult > 1) //medecin
                InterfaceMedecin();
            else
                Console.WriteLine("Erreur de login ou de mot de passe");
            //option pour quitter la console ?
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
            Console.WriteLine("Veuillez saisir le nom:");
            Console.WriteLine("Veuillez saisir le prénom:");
            Console.WriteLine("Veuillez saisir l'age du patient");
            Console.WriteLine("Voulez vous renseigner l'adresse et le numéro de téléphone du patient ? o/n");
            Console.WriteLine("Veuillez saisir l'adresse");
            Console.WriteLine("Veuillez saisir le numéro de téléphone");
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
