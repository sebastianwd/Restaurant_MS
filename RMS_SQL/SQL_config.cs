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

        public int GetProcedure(string ProcedureName, params object[] param)
        {
            int result = 0;
            int iCount;
            SqlCommand cmd = new SqlCommand();
            var da = new SqlDataAdapter();
            SqlConnection cn = GetConnection();
            cmd.Connection = cn;
            cmd.CommandText = ProcedureName;
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            try
            {
                SqlCommandBuilder.DeriveParameters(da.SelectCommand);
                for (iCount = 1; (iCount <= param.Length); iCount++)
                {
                    if (((da.SelectCommand.Parameters[iCount].Direction == ParameterDirection.Input)
                                || (da.SelectCommand.Parameters[iCount].Direction == ParameterDirection.InputOutput)))
                    {
                        if (param[iCount - 1] == null)
                        {
                            if (param[iCount - 1] is null)
                            {
                                da.SelectCommand.Parameters[iCount].Value = "";
                            }
                            else if(param[iCount - 1].GetType() == typeof(string))
                            {
                                da.SelectCommand.Parameters[iCount].Value = "";
                            }
                            else if (param[iCount - 1].GetType() == typeof(int))
                            {
                                da.SelectCommand.Parameters[iCount].Value = 0;
                            }
                            else if (param[iCount - 1].GetType() == typeof(decimal))
                            {
                                da.SelectCommand.Parameters[iCount].Value = 0;
                            }

                        }
                        else
                        {
                            da.SelectCommand.Parameters[iCount].Value = param[iCount - 1];
                        }

                    }

                }

            }
            catch (Exception ex)
            {
            }
            result = cmd.ExecuteNonQuery();

            return result;
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
