﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjetHopital
{
    class DaoPatient
    {
        public List<Patient> SelectAll()
        {
            List<Patient> liste = new List<Patient>();
            string connexionString = InfoSql.CONNEXION_INFO;
            string sql = "USE Hopital;SELECT * FROM patients";

            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = new SqlCommand(sql, connexion);

            connexion.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                liste.Add(new Patient(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), 
                                 reader.GetInt32(3), reader.GetString(4), reader.GetString(5)));
            }

            connexion.Close();
            return liste;
        }
        public Patient SelectById(int id)
        {
            Patient p = new Patient();
            string connexionString = InfoSql.CONNEXION_INFO;

            string sql = "USE Hopital;SELECT * FROM patients WHERE id=" + id;

            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = new SqlCommand(sql, connexion);

            connexion.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {

                p = new Patient(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                                 reader.GetInt32(3), reader.GetString(4), reader.GetString(5));
            }
            connexion.Close();
            return p;
        }
        public void Insert(Patient p)
        {
            string connexionString = InfoSql.CONNEXION_INFO;

            string sql = "USE Hopital;INSERT INTO patients VALUES (@nom,@prenom,@age,@adresse,@telephone)";

            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = connexion.CreateCommand();
            command.CommandText = sql;         
            command.Parameters.Add("nom", SqlDbType.NVarChar).Value = p.Nom;
            command.Parameters.Add("prenom", SqlDbType.NVarChar).Value = p.Prenom;
            command.Parameters.Add("age", SqlDbType.Int).Value = p.Age;
            command.Parameters.Add("adresse", SqlDbType.NVarChar).Value = p.Adresse;
            command.Parameters.Add("telephone", SqlDbType.NVarChar).Value = p.Telephone;

            connexion.Open();
            // Excecution de la requête
            command.ExecuteNonQuery();
            command = connexion.CreateCommand();
            command.CommandText = "USE Hopital;SELECT id FROM patients WHERE nom = @nom AND prenom=@prenom";
            command.Parameters.Add("nom", SqlDbType.NVarChar).Value = p.Nom;
            command.Parameters.Add("prenom", SqlDbType.NVarChar).Value = p.Prenom;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                p.Id = reader.GetInt32(0);
            Console.WriteLine("Insertion patient ok");

            connexion.Close();
        }

        public void Update(Patient p)
        {
            string connexionString = InfoSql.CONNEXION_INFO;
            string sql = "USE Hopital;UPDATE patients SET nom=@nom ,prenom=@prenom, age=@age, adresse=@adresse, telephone=@telephone WHERE id=@id";

            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = connexion.CreateCommand();
            command.CommandText = sql;
            command.Parameters.Add("id", SqlDbType.Int).Value = p.Id;
            command.Parameters.Add("nom", SqlDbType.NVarChar).Value = p.Nom;
            command.Parameters.Add("prenom", SqlDbType.NVarChar).Value = p.Prenom;
            command.Parameters.Add("age", SqlDbType.NVarChar).Value = p.Age;
            command.Parameters.Add("adresse", SqlDbType.NVarChar).Value = p.Adresse;
            command.Parameters.Add("telephone", SqlDbType.NVarChar).Value = p.Telephone;

            connexion.Open();
            // Excecution de la requête
            command.ExecuteNonQuery();
            Console.WriteLine("Mise à jour Patients ok");
            //Console.WriteLine(sql);
            connexion.Close();
        }

        public void Delete(int id)
        {
            string connexionString = InfoSql.CONNEXION_INFO;
            string sql = "USE Hopital;DELETE FROM patients WHERE id=" + id;

            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = new SqlCommand(sql, connexion);

            connexion.Open();

            // execution de la requete
            command.ExecuteNonQuery();
            Console.WriteLine("Suppression patients ok");

            connexion.Close();

        }
    }
}
