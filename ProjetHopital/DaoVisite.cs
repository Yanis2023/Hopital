using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjetHopital
{
    class DaoVisite
    {
        public List<Visite> SelectAll()
        {
            List<Visite> liste = new List<Visite>();
            string connexionString = InfoSql.CONNEXION_INFO;

            string sql = "USE Hopital;SELECT * FROM visites";


            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = new SqlCommand(sql, connexion);

            connexion.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                liste.Add(new Visite(reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2).ToString(), reader.GetString(3),
                                 reader.GetInt32(4), reader.GetDecimal(5)));
            }

            connexion.Close();
            return liste;
        }
        public Visite SelectById(int id)
        {
            Visite v = new Visite();
            string connexionString = InfoSql.CONNEXION_INFO;

            string sql = "USE Hopital;SELECT * FROM visites WHERE id=" + id;


            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = new SqlCommand(sql, connexion);

            connexion.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {

                v = new Visite(reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2).ToString(), reader.GetString(3),
                                 reader.GetInt32(4), reader.GetDecimal(5));
            }
            connexion.Close();
            return v;
        }
        public List<Visite> SelectByIdPatient(int idPatient)
        {
            List<Visite> visites = new List<Visite>();
            string connexionString = InfoSql.CONNEXION_INFO;

            string sql = "USE Hopital; SELECT * FROM visites WHERE IdPatient = @idPatient";

            using (SqlConnection connexion = new SqlConnection(connexionString))
            {
                SqlCommand command = new SqlCommand(sql, connexion);
                command.Parameters.AddWithValue("@idPatient", idPatient);

                connexion.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        visites.Add(new Visite(
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetString(3),
                            reader.GetDateTime(2).ToString(),
                            reader.GetInt32(4),
                            reader.GetDecimal(5)
                        ));
                    }
                }
            }
            return visites;
        }
        public List<Visite> SelectByMedecin(string nomMedecin)
        {
            List<Visite> visites = new List<Visite>();
            string connexionString = InfoSql.CONNEXION_INFO;

            string sql = "USE Hopital; SELECT * FROM visites WHERE medecin = @nomMedecin";

            using (SqlConnection connexion = new SqlConnection(connexionString))
            {
                SqlCommand command = new SqlCommand(sql, connexion);
                command.Parameters.AddWithValue("@nomMedecin", nomMedecin);

                connexion.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        visites.Add(new Visite(
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetString(3),
                            reader.GetDateTime(2).ToString(),
                            reader.GetInt32(4),
                            reader.GetDecimal(5)
                        ));
                    }
                }
            }
            return visites;
        }

        public List<Visite> SelectVisitePatientByMedecin(int id)
        {
            List<Visite> visites = new List<Visite>();
            string connexionString = InfoSql.CONNEXION_INFO;

            string sql = "USE Hopital; SELECT * FROM visites WHERE IdPatient = @id ORDER BY medecin";

            using (SqlConnection connexion = new SqlConnection(connexionString))
            {
                SqlCommand command = new SqlCommand(sql, connexion);
                command.Parameters.AddWithValue("@id", id);

                connexion.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        visites.Add(new Visite(
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetString(3),
                            reader.GetDateTime(2).ToString(),
                            reader.GetInt32(4),
                            reader.GetDecimal(5)
                        ));
                    }
                }
            }
            return visites;
        }


        public List<Visite> SelectVisitePatientByDate(int id)
        {
            List<Visite> visites = new List<Visite>();
            string connexionString = InfoSql.CONNEXION_INFO;

            string sql = "USE Hopital; SELECT * FROM visites WHERE IdPatient = @id ORDER BY date";

            using (SqlConnection connexion = new SqlConnection(connexionString))
            {
                SqlCommand command = new SqlCommand(sql, connexion);
                command.Parameters.AddWithValue("@id", id);

                connexion.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        visites.Add(new Visite(
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetString(3),
                            reader.GetDateTime(2).ToString(),
                            reader.GetInt32(4),
                            reader.GetDecimal(5)
                        ));
                    }
                }
            }
            return visites;
        }

        public string SelectNbVisiteMedecin(string medecin)
        {
            string result = "MEDECIN  NOMBRE_VISITES\n";

            string connexionString = InfoSql.CONNEXION_INFO;

            string sql = "USE Hopital;SELECT medecin, COUNT(medecin) as NbVisites  FROM visites WHERE medecin='" + medecin + "' GROUP BY medecin";

            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = new SqlCommand(sql, connexion);

            connexion.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                result += reader["medecin"] + "\t"  + reader["NbVisites"] + "\n";
            }

            connexion.Close();
            return result;
        }
        public string SelectNbVisiteSalleMedecin()
        {
            string result= "MEDECIN  NUMERO_SALLE  NOMBRE_VISITES\n";
          
            string connexionString = InfoSql.CONNEXION_INFO;

            string sql = "USE Hopital;SELECT medecin, num_salle, COUNT(medecin) as NbVisites FROM visites GROUP BY medecin, num_salle ORDER BY num_salle, medecin ASC";

            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = new SqlCommand(sql, connexion);

            connexion.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                result += reader["medecin"] + "\t" + reader["num_salle"] + "\t" + reader["NbVisites"] +"\n";
            }

            connexion.Close();
            return result;
        }
        public void Insert(Visite v)
        {
            string connexionString = InfoSql.CONNEXION_INFO;

            string sql = "USE Hopital;INSERT INTO visites VALUES (@idPatient,@date,@nomMedecin,@numSalle,@tarif)";

            DateTime dateTime = DateTime.Parse(v.Date);

            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = connexion.CreateCommand();
            command.CommandText = sql;
            command.Parameters.Add("idPatient", SqlDbType.Int).Value = v.IdPatient;
            command.Parameters.Add("date", SqlDbType.DateTime).Value = dateTime ;
            command.Parameters.Add("nomMedecin", SqlDbType.NVarChar).Value = v.NomMedecin;
            command.Parameters.Add("numSalle", SqlDbType.Int).Value = v.NumSalle;
            command.Parameters.Add("tarif", SqlDbType.Decimal).Value = v.Tarif;


            connexion.Open();
            // Excecution de la requête
            command.ExecuteNonQuery();
            Console.WriteLine("Insertion Visite ok");
            connexion.Close();
        }

        public void Update(Visite v)
        {
            string connexionString = InfoSql.CONNEXION_INFO;

            string sql = "USE Hopital;UPDATE visites SET idPatient=@idPatient ,Medecin=@nomMedecin, date=@date, num_Salle=@numSalle, tarif=@tarif WHERE id=@idVisite";


            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = connexion.CreateCommand();
            command.CommandText = sql;
            command.Parameters.Add("idVisite", SqlDbType.Int).Value = v.IdVisite;
            command.Parameters.Add("idPatient", SqlDbType.Int).Value = v.IdPatient;
            command.Parameters.Add("nomMedecin", SqlDbType.NVarChar).Value = v.NomMedecin;
            command.Parameters.Add("date", SqlDbType.Date).Value = v.Date;
            command.Parameters.Add("numSalle", SqlDbType.Int).Value = v.NumSalle;
            command.Parameters.Add("tarif", SqlDbType.Decimal).Value = v.Tarif;

            connexion.Open();
            // Excecution de la requête
            command.ExecuteNonQuery();
            Console.WriteLine("Mise à jour Visite ok");
            //Console.WriteLine(sql);
            connexion.Close();
        }

        public void Delete(int id)
        {
            string connexionString = InfoSql.CONNEXION_INFO;

            string sql = "USE Hopital;DELETE FROM visites WHERE id=" + id;


            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = new SqlCommand(sql, connexion);

            connexion.Open();

            // execution de la requete
            command.ExecuteNonQuery();
            Console.WriteLine("Suppression visite ok");
            connexion.Close();

        }
    }
}
