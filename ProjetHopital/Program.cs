using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetHopital
{
    class Program
    {
        private static Hopital hopital = Hopital.Instance;
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
                {
                    if (hopital.Salles.Count < metier)
                        for (int i = hopital.Salles.Count; i < metier; ++i)
                            hopital.Salles.Add(new Salle(i));
                    Console.WriteLine($"count {hopital.Salles.Count} metier {metier}");
                    hopital.SalleActive = metier - 1;
                    hopital.Salles[hopital.SalleActive].MedecinActuel = nom;
                    InterfaceMedecin();
                }
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
                Console.WriteLine("1 - Rajouter un patient\n2 - Sauvegarder la liste d'attente\n3 - Charger/Afficher la liste d'attente\n" +
                    "4 - Nouvelle journée\n5 - Afficher les visites d'un patient\n6 - Afficher toutes les visites\n" +
                    "7 Afficher le prochain patient\n" +
                    "8 - Afficher toutes les visites d'un médecin\n9 - Quitter l'interface secrétaire\nVeuillez entrer votre choix: ");
                while (!Int32.TryParse(Console.ReadLine(), out choix) && (choix < 1 || choix > 8)) ;
                if (choix == 9)
                    break;

                switch (choix)
                {
                    case 1:
                        AjouterPatient();
                        break;
                    case 2:
                        
                        break;
                    case 3:
                        AfficherFileAttente();
                        break;
                    case 4:
                        
                        break;
                    case 5:
                        
                        break;
                    case 6:
                        
                        break;
                    case 7:
                        AfficherProchainPatient();
                        break;
                    case 8:

                        break;
                }
            }
            Console.WriteLine("Fermeture interface Secrétaire");
        }

        static void AjouterPatient()
        {
            Console.WriteLine("Veuillez saisir un identifiant:");
            int id;
            while (!Int32.TryParse(Console.ReadLine(), out id)) ;
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
            hopital.FileAttente.Enqueue(p);

            string dateHeureArrivee = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            //using (StreamWriter sw = new StreamWriter("patients.txt", true))
            //{
            //    sw.WriteLine($"{p.Id} {dateHeureArrivee}");
            //}

            //Console.WriteLine("Patient ajouté à la file d'attente avec succès.");
        }

        private static void AfficherFileAttente()
        {
            string filePath = "patients.txt";

            Console.WriteLine("Liste des patients dans la file d'attente :");
            if (!File.Exists(filePath))
            {
                foreach (Patient p in hopital.FileAttente)
                    Console.WriteLine(p);
                //Console.WriteLine("Le fichier patients.txt n'existe pas.");
                return;
            }

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    
                    string[] parts = line.Split(' ');


                }
            }
        }

        private static void AfficherProchainPatient()
        {
            Console.WriteLine("Prochain patient:");
            Console.WriteLine(hopital.FileAttente.Peek());
        }

        private static void MettreAJourPatient()
        {
            //TODO
            throw new NotImplementedException();
        }

        private static void AfficherVisitesPatient()
        {
            //TODO
            throw new NotImplementedException();
        }


        private static void AfficherToutesLesVisites()
        {
            //TODO
            throw new NotImplementedException();
        }

        private static void AfficherToutesVisitesDUnMedecin()
        {
            //TODO
            throw new NotImplementedException();
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
                if (choix == 5)
                    break;

                switch (choix)
                {
                    case 1:
                        AfficherFileAttente();
                        break;
                    case 2:

                        break;
                    case 3:
                        hopital.Salles[hopital.SalleActive].SauvegarderVisites();
                        break;
                    case 4:
                        hopital.Salles[hopital.SalleActive].RendreDispo();
                        break;
                }                //do redirections there
            }
            Console.WriteLine("Fermeture interface Médecin");
        }
    }
}
