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
    public class SQL_TB_ARTI
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["sql_connection"].ConnectionString);

        public IEnumerable<M_TB_ARTI> listar_por_clase(string FS_COD_CLAS)
        {
            var temp = new List<M_TB_ARTI>();
            SqlCommand cmd = new SqlCommand("select * from TB_ARTI where FS_COD_CLAS = @ISCOD_CLAS", cn);
            cmd.Parameters.AddWithValue("@ISCOD_CLAS", FS_COD_CLAS);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                M_TB_ARTI e = new M_TB_ARTI();
                e.FS_COD_ARTI = dr["FS_COD_ARTI"].ToString();
                e.FN_IMP_VENT = Convert.ToDecimal(dr["FN_IMP_VENT"]);
                e.FS_NOM_ARTI = dr["FS_NOM_ARTI"].ToString();
                e.FS_DES_OBSE = dr["FS_DES_OBSE"].ToString();
                e.FS_TIP_SITU = dr["FS_TIP_SITU"].ToString();
                e.FS_COD_CLAS = dr["FS_COD_CLAS"].ToString();
                e.FS_RUT_FOTO = dr["FS_RUT_FOTO"].ToString();

                temp.Add(e);
            }
            cn.Close(); dr.Close();
            return temp;

        }

        public IEnumerable<M_TB_ARTI> listar_productos()
        {
            var temp = new List<M_TB_ARTI>();
            SqlCommand cmd = new SqlCommand("select * from TB_ARTI T1 inner join TB_ARTI_CLAS T2 On (T1.FS_COD_CLAS = T2.FS_COD_CLAS)", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                M_TB_ARTI e = new M_TB_ARTI();
                e.FS_COD_ARTI = dr["FS_COD_ARTI"].ToString();
                e.FN_IMP_VENT = Convert.ToDecimal(dr["FN_IMP_VENT"]);
                e.FS_NOM_ARTI = dr["FS_NOM_ARTI"].ToString();
                e.FS_DES_OBSE = dr["FS_DES_OBSE"].ToString();
                e.FS_TIP_SITU = dr["FS_TIP_SITU"].ToString();
                e.FS_COD_CLAS = dr["FS_COD_CLAS"].ToString();
                e.FS_DES_CLAS = dr["FS_DES_CLAS"].ToString();
                e.FS_RUT_FOTO = dr["FS_RUT_FOTO"].ToString();
                temp.Add(e);
            }
            cn.Close(); dr.Close();
            return temp;

        }
    }
}
