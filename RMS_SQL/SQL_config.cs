using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_SQL
{
    public class SQL_config
    {

        public SqlConnection GetConnection(string  connectionName)
        {
            string cnstr = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            SqlConnection cn = new SqlConnection(cnstr);
            cn.Open();
            return cn;
        }
        public SqlConnection GetConnection()
        {
            string cnstr = ConfigurationManager.ConnectionStrings["sql_connection"].ConnectionString;
            SqlConnection cn = new SqlConnection(cnstr);
            cn.Open();

            return cn;
        }


        public int GetProcedure(  
            string procedureName,
            params object[] parameters)
        {
            int rc;
                using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;
                    // assign parameters passed in to the command
                    foreach (var procParameter in parameters)
                    {
                        cmd.Parameters.Add(procParameter);
                    }
                    rc = cmd.ExecuteNonQuery();
                }
            }
           
            return rc;
        }

        public int GetQuery(
           string query,
           params object[] parameters)
        {
            int rc;
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = query;
                    // assign parameters passed in to the command
                    foreach (var procParameter in parameters)
                    {
                        cmd.Parameters.Add(procParameter);
                    }
                    rc = cmd.ExecuteNonQuery();
                }
            }

            return rc;
        }

    }
}
