using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetHopital
{
    class Admin
    {
        public static void SupprimerPatient()
        {
            Console.WriteLine("Saisissez un identifiant: ");
            int id;
            while (!Int32.TryParse(Console.ReadLine(), out id)) ;
            DaoPatient daoPatient = new DaoPatient();
            daoPatient.Delete(id);
            Console.WriteLine("Patient numero " + id + " supprimé");
        }
        public static void UpdatePatient()
        {
            Console.WriteLine("Saisissez un identifiant: ");
            int id;
            while (!Int32.TryParse(Console.ReadLine(), out id)) ;
            DaoPatient daoPatient = new DaoPatient();
            Patient p = daoPatient.SelectById(id);
            Console.WriteLine("Modification de: " + p);
            Console.WriteLine("Veuillez saisir le nouveau nom:");
            p.Nom = Console.ReadLine();
            Console.WriteLine("Veuillez saisir le nouveau prénom:");
            p.Prenom = Console.ReadLine();
            Console.WriteLine("Veuillez saisir le nouvel age du patient:");
            int age;
            while (!Int32.TryParse(Console.ReadLine(), out age) && age < 0) ;
            p.Age = age;
            Console.WriteLine("Saisissez le nouveau numéro de téléphone: ");
            string telephone = Console.ReadLine();
            Console.WriteLine("Saisissez la nouvelle adresse: ");
            string adresse = Console.ReadLine();
            p.Telephone = telephone;
            p.Adresse = adresse;
            daoPatient.Update(p);
        }
        public static void AfficherAllPatients()
        {
            DaoPatient daoPatient = new DaoPatient();
            List<Patient> patients = daoPatient.SelectAll();
            Console.WriteLine("Liste de tous les patients:");
            foreach (Patient p in patients)
                Console.WriteLine("\t" + p);
        }
        public static void AfficherPatientById()
        {
            DaoPatient daoPatient = new DaoPatient();
            Console.WriteLine("Saisissez un identifiant: ");
            int id;
            while (!Int32.TryParse(Console.ReadLine(), out id)) ;
            Patient p = daoPatient.SelectById(id);
            if (p.Id == id)
                Console.WriteLine(p);
            else
                Console.WriteLine("Pas de patient avec cet identifiant");
        }

        public static void AfficherNombreVisiteSalleMedecin()
        {
            DaoVisite daoVisite = new DaoVisite();
            Console.WriteLine("Nombre de visites par salle et par Médecin :");
            Console.WriteLine(daoVisite.SelectNbVisiteSalleMedecin());
        }

        public static void AfficherNombreVisiteMedecin()
        {
            Console.WriteLine("Veuillez saisir le nom du medecin:");
            string medecin = Console.ReadLine();
            DaoVisite daoVisite = new DaoVisite();
            Console.WriteLine(daoVisite.SelectNbVisiteMedecin(medecin));
        }
        public static void AfficherNombreVisiteMedecinDate()
        {
            Console.WriteLine("Veuillez saisir le nom du medecin:");
            string medecin = Console.ReadLine();
            Console.WriteLine("Veuillez saisir la date Min:");
            string dateMin = Console.ReadLine();
            Console.WriteLine("Veuillez saisir la date Max:");
            string dateMax = Console.ReadLine();

            DaoVisite daoVisite = new DaoVisite();
            Console.WriteLine(daoVisite.SelectNbVisiteMedecinDate(medecin, dateMin, dateMax));

        }
    }
}
