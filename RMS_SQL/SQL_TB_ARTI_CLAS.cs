using RMS_MODELOS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_SQL
{
    public class SQL_TB_ARTI_CLAS
    {

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["sql_connection"].ConnectionString);

        public IEnumerable<M_TB_ARTI_CLAS> listar_clases_producto()
        {
            var temp = new List<M_TB_ARTI_CLAS>();
            SqlCommand cmd = new SqlCommand("select * from TB_ARTI_CLAS", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                M_TB_ARTI_CLAS e = new M_TB_ARTI_CLAS();
                e.FS_COD_CLAS = dr["FS_COD_CLAS"].ToString();
                e.FS_DES_CLAS = dr["FS_DES_CLAS"].ToString(); ;
              
                temp.Add(e);
            }
            cn.Close(); dr.Close();
            return temp;

        }
    }
}
