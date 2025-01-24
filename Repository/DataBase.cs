using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Reposytory
{
    public class DataBase
    {
        NpgsqlConnection sqlConnection;// = new NpgsqlConnection(@"Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123;Pooling=true");
        public bool Status = false;
        public void openConnection()
        {
            try
            {
                if (sqlConnection == null || sqlConnection.State == System.Data.ConnectionState.Broken || sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection = new NpgsqlConnection(@"Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123;Pooling=true");
                    sqlConnection.Open();
                    Status = true;
                }
            }
            catch
            {
                Status = false;
            }
        }
        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        public NpgsqlConnection getConnection()
        {
            return sqlConnection;
        }
        public virtual List<Dictionary<string, string>> GetTable(string query)
        {
            openConnection();
            if (!Status)
            {
                return new List<Dictionary<string, string>> { new Dictionary<string, string> { { "NaN", "NaN" } } };
            }

            List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();

            using (var conn = getConnection())
            {
                //List<string> columns = new List<string>();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Dictionary<string, string> row = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader[i].ToString();
                            }
                            rows.Add(row);
                        }

                    }
                }


            }
            closeConnection();
            return (rows);

        }
    }
}
