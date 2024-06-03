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
<<<<<<< HEAD
            string sql = "select * from visites";
=======
            string sql = "select * from visite";
>>>>>>> 6430c68c59b133e743f8e5038e36b3ae13e35a0b

            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = new SqlCommand(sql, connexion);

            connexion.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                liste.Add(new Visite(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3),
                                 reader.GetInt32(4), reader.GetDecimal(5)));
            }

            connexion.Close();
            return liste;
        }
        public Visite SelectById(int id)
        {
            Visite v = new Visite();
            string connexionString = InfoSql.CONNEXION_INFO;
<<<<<<< HEAD
            string sql = "select * from visites where id=" + id;
=======
            string sql = "select * from visite where id=" + id;
>>>>>>> 6430c68c59b133e743f8e5038e36b3ae13e35a0b

            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = new SqlCommand(sql, connexion);

            connexion.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {

                v = new Visite(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3),
                                 reader.GetInt32(4), reader.GetDecimal(5));
            }
            connexion.Close();
            return v;
        }
        public void Insert(Visite v)
        {
            string connexionString = InfoSql.CONNEXION_INFO;
<<<<<<< HEAD
            string sql = "insert into visites values (@idPatient,@nomMedecin,@date,@numSalle,@tarif)";
=======
            string sql = "insert into visite values (@idPatient,@nomMedecin,@date,@age,@numSalle,@tarif)";
>>>>>>> 6430c68c59b133e743f8e5038e36b3ae13e35a0b

            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand command = connexion.CreateCommand();
            command.CommandText = sql;
            command.Parameters.Add("idPatient", SqlDbType.Int).Value = v.IdPatient;
            command.Parameters.Add("nomMedecin", SqlDbType.NVarChar).Value = v.NomMedecin;
            command.Parameters.Add("date", SqlDbType.Date).Value = v.Date;
            command.Parameters.Add("numSalle", SqlDbType.Int).Value = v.NumSalle;
            command.Parameters.Add("tarif", SqlDbType.Decimal).Value = v.Tarif;
<<<<<<< HEAD

=======
          
>>>>>>> 6430c68c59b133e743f8e5038e36b3ae13e35a0b

            connexion.Open();
            // Excecution de la requête
            command.ExecuteNonQuery();
            Console.WriteLine("Insertion Visite ok");
            connexion.Close();
        }

        public void Update(Visite v)
        {
            string connexionString = InfoSql.CONNEXION_INFO;
<<<<<<< HEAD
            string sql = "update visites set idPatient=@idPatient ,nomMedecin=@nomMedecin, date=@date, numSalle=@numSalle, tarif=@tarif where idVisite=@idVisite";
=======
            string sql = "update visite set idPatient=@idPatient ,nomMedecin=@nomMedecin, date=@date, numSalle=@numSalle, tarif=@tarif where idVisite=@idVisite";
>>>>>>> 6430c68c59b133e743f8e5038e36b3ae13e35a0b

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
<<<<<<< HEAD
            string sql = "delete from visites where id=" + id;
=======
            string sql = "delete from visite where id=" + id;
>>>>>>> 6430c68c59b133e743f8e5038e36b3ae13e35a0b

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
