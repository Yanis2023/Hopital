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
                if (metier == -1) //admin
                    InterfaceAdmin();
                else if (metier == 0) //secretaire
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

        static void InterfaceAdmin()
        {

            Console.WriteLine("Bienvenue dans l'interface Admin\n_________________________________");
            int choix = -1;
            while (choix != 6)
            {
                Console.WriteLine("1 - Rajouter un nouveau patient\n2 - Supprimer un patient selon son id\n3 - Modifier toutes les infos du patient depuis son id(sauf son id)\n" +
                    "4 - Afficher la liste de tout les patient\n5 - Afficher un patient selon son id\n6 - Quitter l'interface admin");
                while (!Int32.TryParse(Console.ReadLine(), out choix) && (choix < 1 || choix > 6));
                switch (choix)
                {
                    case 1:
                        AjouterPatient(true);
                        break;
                    case 2:
                        Admin.SupprimerPatient();
                        break;
                    case 3:
                        Admin.UpdatePatient();
                        break;
                    case 4:
                        Admin.AfficherAllPatients();
                        break;
                    case 5:
                        Admin.AfficherPatientById();
                        break;
                }
            }
        }
        static void InterfaceSecretaire()
        {
            Console.WriteLine("Bienvenue dans l'interface Secrétaire\n______________________________________");
            int choix = -1;
            while (choix != 10)
            {
                Console.WriteLine("1 - Rajouter un patient\n2 -  Sauvegarder la liste d'attente\n3 - Charger/Afficher la liste d'attente\n" +
                    "4 - Nouvelle journée\n5 - Afficher les visites d'un patient\n6 - Afficher toutes les visites\n" +
                    "7 - Afficher le prochain patient\n" +
                    "8 - Afficher toutes les visites d'un médecin\n9 - Mettre à jour patient\n10 - Quitter l'interface secrétaire\nVeuillez entrer votre choix: ");
                while (!Int32.TryParse(Console.ReadLine(), out choix) && (choix < 1 || choix > 10)) ;
                if (choix == 10)
                    break;

                switch (choix)
                {
                    case 1:
                        AjouterPatient();
                        break;
                    case 2:
                        SauvegarderFileDAttente();
                        break;
                    case 3:
                        AfficherFileAttente();
                        break;
                    case 4:
                        
                        break;
                    case 5:
                        AfficherVisitesPatient();
                        break;
                    case 6:
                        AfficherToutesLesVisites();
                        break;
                    case 7:
                        AfficherProchainPatient();
                        break;
                    case 8:
                        AfficherToutesVisitesDUnMedecin();
                        break;
                    case 9:               
                        MettreAJourPatient();
                        break;
                }
            }
            Console.WriteLine("Fermeture interface Secrétaire");
        }

        static void AjouterPatient(bool isAdmin = false)
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
            if (!isAdmin)
            {
                hopital.FileAttente.Enqueue(p);

                string dateHeureArrivee = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                using (StreamWriter sw = new StreamWriter("patients.txt", true))
                {
                    sw.WriteLine($"{p.Id} {dateHeureArrivee}");
                }

                Console.WriteLine("Patient ajouté à la file d'attente avec succès.");
            }
        }


        private static void SauvegarderFileDAttente()
        {
            StreamWriter sw = new StreamWriter("patients.txt", false);
                
            foreach (Patient p in hopital.FileAttente)
            {
                string dateHeureArrivee = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                sw.WriteLine($"{p.Id} {dateHeureArrivee}");
                Console.WriteLine($"Patient ID: {p.Id}, Nom: {p.Nom}, Prénom: {p.Prenom}, Age: {p.Age}, Adresse: {p.Adresse}, Téléphone: {p.Telephone}, Date d'arrivée: {dateHeureArrivee}");
            }
                Console.WriteLine("Sauvegarde réussie.");
        }


        private static void AfficherFileAttente()
        {
            Console.WriteLine("Liste des patients dans la file d'attente :");

            foreach (Patient patient in hopital.FileAttente)
            {
                Console.WriteLine($"ID: {patient.Id}, Nom: {patient.Nom}, Prénom: {patient.Prenom}, Âge: {patient.Age}, Adresse: {patient.Adresse}, Téléphone: {patient.Telephone}");
            }
        }

        private static void AfficherProchainPatient()
        {
            Console.WriteLine("Prochain patient:");
            Console.WriteLine(hopital.FileAttente.Peek());
        }

        public static void MettreAJourPatient()
        {       
            Console.WriteLine("Saisissez un identifiant : ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Saisissez le nouveau numéro de téléphone : ");
            string telephone = Console.ReadLine();
            Console.WriteLine("Saisissez la nouvelle adresse : ");
            string adresse = Console.ReadLine();

            DaoPatient daoPatient = new DaoPatient();
            Patient p = new Patient();

            p = daoPatient.SelectById(id);
            if(telephone != "")
                p.Telephone = telephone;
            if(adresse != "")
                p.Adresse = adresse;
           
            daoPatient.Update(p);
        }

        private static void AfficherVisitesPatient()
        {
            Console.WriteLine("Saisissez un identifiant : ");
            int id = Convert.ToInt32(Console.ReadLine());
          
            DaoVisite daoVisite = new DaoVisite();
           
            Console.WriteLine(daoVisite.SelectByIdPatient(id).ToString());
                  
        }


        private static void AfficherToutesLesVisites()
        {
            DaoVisite daoVisite = new DaoVisite();
            List<Visite> visites = daoVisite.SelectAll();

            if (visites.Count == 0)
            {
                Console.WriteLine("Aucune visite trouvée.");
            }
            else
            {
                Console.WriteLine("Liste de toutes les visites :");
                foreach (Visite visite in visites)
                {
                    Console.WriteLine($"ID Visite: {visite.IdVisite}, Patient ID: {visite.IdPatient}, Médecin: {visite.NomMedecin}, Date: {visite.Date}, Tarif: {visite.Tarif}");
                }
            }
        }

        private static void AfficherToutesVisitesDUnMedecin()
        {
            Console.WriteLine("Veuillez saisir le nom du médecin:");
            string nomMedecin = Console.ReadLine();

            DaoVisite daoVisite = new DaoVisite();
            List<Visite> visites = daoVisite.SelectByMedecin(nomMedecin);

            if (visites.Count == 0)
            {
                Console.WriteLine("Aucune visite trouvée pour le médecin: " + nomMedecin);
            }
            else
            {
                Console.WriteLine("Liste de toutes les visites du médecin " + nomMedecin + " :");
                foreach (Visite visite in visites)
                {
                    Console.WriteLine($"ID Visite: {visite.IdVisite}, Patient ID: {visite.IdPatient}, Date: {visite.Date}, Numéro de Salle: {visite.NumSalle}, Tarif: {visite.Tarif}");
                }
            }
        }










        //interface Médecin

        static void InterfaceMedecin()
        {
            Console.WriteLine("Bienvenue dans l'interface Médecin\n___________________________________");
            int choix = -1;
            while (choix != 5)
            {
                Console.WriteLine("1 - Afficher l'état de la file d'attente\n2 - Ajouter une ordonnance au patient actuel\n" +
                    "3 - Sauvegarde de la BDD de la liste des visites\n4 - Rendre la salle disponible\n" +
                    "5 - Quitter l'interface médecin\nVeuillez entrer votre choix: ");
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
