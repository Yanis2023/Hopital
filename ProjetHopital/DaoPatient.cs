using System;
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
            string connexionString = @"Data Source=PC-YOANN-23\SQLEXPRESS;Initial Catalog=ajc;Integrated Security=True";
            string sql = "select * from patient";

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
        public Patient SelectByRef(int id)
        {
            Patient p = new Patient();
            string connexionString = @"Data Source=PC-YOANN-23\SQLEXPRESS;Initial Catalog=ajc;Integrated Security=True";
            string sql = "select * from articles where id=" + id;

            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = new SqlCommand(sql, connexion);

            connexion.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                //result += reader["reference"] + "\t" + reader["marque"]
                //   + "\t" + reader["prix"] + "\n";
                p = new Patient(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                                 reader.GetInt32(3), reader.GetString(4), reader.GetString(5));
            }
            connexion.Close();
            return p;
        }
        public void Insert(Patient p)
        {
            string connexionString = @"Data Source=PC-YOANN-23\SQLEXPRESS;Initial Catalog=ajc;Integrated Security=True";
            string sql = "insert into articles values (@reference,@marque,@prix)";

            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = connexion.CreateCommand();
            command.CommandText = sql;
            command.Parameters.Add("reference", SqlDbType.Int).Value = p.Id;
            command.Parameters.Add("marque", SqlDbType.NVarChar).Value = p.Nom;
            command.Parameters.Add("prix", SqlDbType.NVarChar).Value = p.Prenom;
            command.Parameters.Add("prix", SqlDbType.NVarChar).Value = p.Age;
            command.Parameters.Add("prix", SqlDbType.NVarChar).Value = p.Adresse;
            command.Parameters.Add("prix", SqlDbType.NVarChar).Value = p.Telephone;

            connexion.Open();
            // Excecution de la requête
            command.ExecuteNonQuery();
            Console.WriteLine("Insertion article ok");
            connexion.Close();
        }

        public void Update(Patient p)
        {
            string connexionString = @"Data Source=PC-YOANN-23\SQLEXPRESS;Initial Catalog=ajc;Integrated Security=True";
            string sql = "update articles set marque=@marque ,prix=@prix where reference=@reference";

            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = connexion.CreateCommand();
            command.CommandText = sql;
            command.Parameters.Add("reference", SqlDbType.Int).Value = p.Id;
            command.Parameters.Add("marque", SqlDbType.NVarChar).Value = p.Nom;
            command.Parameters.Add("prix", SqlDbType.NVarChar).Value = p.Prenom;
            command.Parameters.Add("prix", SqlDbType.NVarChar).Value = p.Age;
            command.Parameters.Add("prix", SqlDbType.NVarChar).Value = p.Adresse;
            command.Parameters.Add("prix", SqlDbType.NVarChar).Value = p.Telephone;

            connexion.Open();
            // Excecution de la requête
            command.ExecuteNonQuery();
            Console.WriteLine("Mise à jour Patient ok");
            //Console.WriteLine(sql);
            connexion.Close();
        }

        public void Delete(int id)
        {
            string connexionString = @"Data Source=PC-YOANN-23\SQLEXPRESS;Initial Catalog=ajc;Integrated Security=True";
            string sql = "delete from articles where id=" + id;

            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = new SqlCommand(sql, connexion);

            connexion.Open();

            // execution de la requete
            command.ExecuteNonQuery();
            Console.WriteLine("Suppression patient ok");

            connexion.Close();

        }
    }
}
