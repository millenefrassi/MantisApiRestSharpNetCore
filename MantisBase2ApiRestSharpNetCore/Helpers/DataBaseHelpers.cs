using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantisBase2ApiRestSharpNetCore.Helpers
{
    public class DataBaseHelpers
    {
        private static MySqlConnection GetDBConnection()
        {
            string connectionString = "Server=" + JsonBuilder.ReturnParameterAppSettings("DB_URL") + ";" +
                                      "Port=" + JsonBuilder.ReturnParameterAppSettings("DB_PORT") + ";" +
                                      "Database=" + JsonBuilder.ReturnParameterAppSettings("DB_NAME") + ";" +
                                      "UID=" + JsonBuilder.ReturnParameterAppSettings("DB_USER") + "; " +
                                      "Password=" + JsonBuilder.ReturnParameterAppSettings("DB_PASSWORD") + ";" +
                                      "SslMode=" + JsonBuilder.ReturnParameterAppSettings("SSL_MODE");

            MySqlConnection connection = new MySqlConnection(connectionString);

            return connection;
        }

        public static void ExecuteQuery(string query)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, GetDBConnection()))
            {
                cmd.CommandTimeout = Int32.Parse(JsonBuilder.ReturnParameterAppSettings("DB_CONNECTION_TIMEOUT").ToString());
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public static List<string> RetornaDadosQuery(string query)
        {
            DataSet ds = new DataSet();
            List<string> lista = new List<string>();

            using (MySqlCommand cmd = new MySqlCommand(query, GetDBConnection()))
            {
                cmd.CommandTimeout = Int32.Parse(JsonBuilder.ReturnParameterAppSettings("DB_CONNECTION_TIMEOUT").ToString());
                cmd.Connection.Open();

                DataTable table = new DataTable();
                table.Load(cmd.ExecuteReader());
                ds.Tables.Add(table);

                cmd.Connection.Close();
            }

            if (ds.Tables[0].Columns.Count == 0)
            {
                return null;
            }

            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        lista.Add(ds.Tables[0].Rows[i][j].ToString());
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            return lista;
        }
    }
}
