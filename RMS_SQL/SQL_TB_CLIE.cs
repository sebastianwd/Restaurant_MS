using RMS_MODELOS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_SQL
{
    public class SQL_TB_CLIE
    {
        public List<M_TB_CLIE> buscar_por_tipo_cliente(string FS_TIP_CLIE)
        {
            var temp = new List<M_TB_CLIE>();

            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReaderProc("SP_CLIE_BU01", "@ISTIP_CLIE", FS_TIP_CLIE))
            {
                while (dr.Read())
                {
                    M_TB_CLIE e = new M_TB_CLIE();
                    e.FS_COD_CLIE       =  dr["FS_COD_CLIE"     ].ToString();       
                    e.FS_APE_PATE_CLIE  =  dr["FS_APE_PATE_CLIE"].ToString();
                    e.FS_APE_MATE_CLIE  =  dr["FS_APE_MATE_CLIE"].ToString();
                    e.FS_NOM_CLIE_NA01  =  dr["FS_NOM_CLIE_NA01"].ToString();
                    e.FS_NOM_CLIE_NA02  =  dr["FS_NOM_CLIE_NA02"].ToString();
                    e.FS_NOM_RAZO_SOCI  =  dr["FS_NOM_RAZO_SOCI"].ToString();
                    e.FD_FEC_REGI       = Convert.ToDateTime(dr["FD_FEC_REGI"]);
                    e.FS_NUM_DOCU_IDEN  =  dr["FS_NUM_DOCU_IDEN"].ToString();
                    e.FS_NUM_RUCS       =  dr["FS_NUM_RUCS"     ].ToString();
                    e.FS_DES_MAIL       =  dr["FS_DES_MAIL"     ].ToString();
                    e.FS_DES_OBSE       =  dr["FS_DES_OBSE"     ].ToString();
                    e.FS_TIP_SITU       =  dr["FS_TIP_SITU"     ].ToString();
                    e.FS_STA_SUNA = dr["FS_STA_SUNA"].ToString(); ;
                    temp.Add(e);
                }
                dr.Close();
            }

            return temp;

        }

    }
}
