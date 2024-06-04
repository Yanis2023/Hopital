using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjetHopital
{
    class DaoAuthentification
    {
        public static bool Login(string login, string mdp, out string nom, out int job)
        {
            string sql = $"USE Hopital; SELECT nom, metier FROM authentification WHERE login = '{login}' AND password = '{mdp}';";
            SqlConnection connexion = new SqlConnection(InfoSql.CONNEXION_INFO);
            SqlCommand command = new SqlCommand(sql, connexion);

            connexion.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read() && reader.HasRows)
            {
                nom = reader.GetString(0);
                job = reader.GetInt32(1);
                connexion.Close();
                return true;
            }
            nom = "";
            job = -2;
            connexion.Close();
            return false;
        }
    }
}
