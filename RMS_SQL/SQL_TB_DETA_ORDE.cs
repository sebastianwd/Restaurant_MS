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
 public   class SQL_TB_DETA_ORDE
    {

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["sql_connection"].ConnectionString);

        public IEnumerable<M_TB_DETA_ORDE> listar_detalle_orden(decimal FN_IDE_ORDE, int FI_COD_EMPR)
        {
            var temp = new List<M_TB_DETA_ORDE>();
            SqlCommand cmd = new SqlCommand("SP_DETA_ORDE_CA01", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@INIDE_ORDE", FN_IDE_ORDE);
            cmd.Parameters.AddWithValue("@IICOD_EMPR", FI_COD_EMPR);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                M_TB_DETA_ORDE e = new M_TB_DETA_ORDE();
                e.FS_NOM_ARTI = dr["FS_NOM_ARTI"].ToString();
                e.FN_PRE_VENT = Convert.ToDecimal(dr["FN_PRE_VENT"]);
                e.FN_IDE_ORDE = Convert.ToDecimal(dr["FN_IDE_ORDE"]);
                e.FI_NUM_SECU = Convert.ToInt32( dr["FI_NUM_SECU"]);
                e.FS_TIP_SITU = dr["FS_TIP_SITU"].ToString();
                e.FI_COD_EMPR = Convert.ToInt32(dr["FI_COD_EMPR"].ToString());
                e.FN_CAN_ARTI = Convert.ToDecimal(dr["FN_CAN_ARTI"].ToString());
                e.FS_TIP_SITU = dr["FS_TIP_SITU"].ToString();
                e.FS_COD_ARTI = dr["FS_COD_ARTI"].ToString();



                temp.Add(e);
            }
            cn.Close(); dr.Close();
            return temp;

        }

        public IEnumerable<M_TB_DETA_ORDE> agregar_detalle_orden(decimal FN_IDE_ORDE, string FS_COD_EMPR, int FI_COD_EMPR)
        {
            var temp = new List<M_TB_DETA_ORDE>();
            SqlCommand cmd = new SqlCommand("SP_DETA_ORDE_CA01", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@INIDE_ORDE", FN_IDE_ORDE);
            cmd.Parameters.AddWithValue("@IICOD_EMPR", FI_COD_EMPR);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                M_TB_DETA_ORDE e = new M_TB_DETA_ORDE();
                e.FS_NOM_ARTI = dr["FS_NOM_ARTI"].ToString();
                e.FN_PRE_VENT = Convert.ToDecimal(dr["FN_PRE_VENT"]);
                e.FN_IDE_ORDE = Convert.ToDecimal(dr["FN_IDE_ORDE"]);
                e.FI_NUM_SECU = Convert.ToInt32(dr["FI_NUM_SECU"]);
                e.FS_TIP_SITU = dr["FS_TIP_SITU"].ToString();
                e.FI_COD_EMPR = Convert.ToInt32(dr["FI_COD_EMPR"].ToString());
                e.FN_CAN_ARTI = Convert.ToDecimal(dr["FN_CAN_ARTI"].ToString());
                e.FS_TIP_SITU = dr["FS_TIP_SITU"].ToString();
                e.FS_COD_ARTI = dr["FS_COD_ARTI"].ToString();



                temp.Add(e);
            }
            cn.Close(); dr.Close();
            return temp;

        }
    }
}
