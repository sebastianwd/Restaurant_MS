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
    public class SQL_config_old
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


       /* public int GetProcedure(  
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
                 
                        cmd.Parameters.AddRange(parameters);
                    
                    rc = cmd.ExecuteNonQuery();
                }
            }
           
           return rc;
        }*/

      

        public int GetProcedure(
      string ProcedureName,
      params object[] parameters)
        {
            int rc;
            var da = new SqlDataAdapter();
            using (SqlConnection cn = GetConnection())
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = ProcedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand = cmd;
                    SqlCommandBuilder.DeriveParameters(da.SelectCommand);

                    // assign parameters passed in to the command
                    for (var i = 1; (i <= parameters.Length); i++)
                    {
                        if (((da.SelectCommand.Parameters[i].Direction == ParameterDirection.Input)
                          || (da.SelectCommand.Parameters[i].Direction == ParameterDirection.InputOutput)))
                        {
                            if (parameters[i - 1] == null)
                            {
                                da.SelectCommand.Parameters[i].Value = "";
                            }
                            else
                            {
                                da.SelectCommand.Parameters[i].Value = parameters[i - 1];
                            }

                        }
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

        public object GetScalar(
         string query,
         params object[] parameters)
        {
            object rc;
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
                    rc = cmd.ExecuteScalar();
                }
            }

            return rc;
        }

    }
}
