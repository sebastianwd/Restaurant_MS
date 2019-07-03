using RMS_MODELOS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_SQL
{
    public class SQL_TB_EMPR
    {
        public M_TB_EMPR buscar_por_codigo(int FI_COD_EMPR)
        {
            var temp = new List<M_TB_EMPR>();
            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReaderProc("SP_EMPR_BU01", "@IICOD_EMPR", FI_COD_EMPR))
            {
                while (dr.Read())
                {
                    M_TB_EMPR e = new M_TB_EMPR();
                    e.FI_COD_EMPR = Convert.ToInt32(dr["FI_COD_EMPR"].ToString());
                    e.FS_NOM_EMPR = dr["FS_NOM_EMPR"].ToString();
                    e.FS_NUM_RUCC = dr["FS_NUM_RUCC"].ToString();
                    e.FS_DIR_PRIN = dr["FS_DIR_PRIN"].ToString();
                    e.FS_NUM_TEL1 = dr["FS_NUM_TEL1"].ToString();
                    e.FS_NUM_TEL2 = dr["FS_NUM_TEL2"].ToString();
                    e.FS_PAG_WEBB = dr["FS_PAG_WEBB"].ToString();
                    e.FS_RUT_LOGO_EMPR = dr["FS_RUT_LOGO_EMPR"].ToString();
                    temp.Add(e);
                }
                dr.Close();
            }

            return temp.FirstOrDefault();
        }

        public IEnumerable<M_TB_EMPR> listar()
        {
            var temp = new List<M_TB_EMPR>();

            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReader("select * from TB_EMPR"))
            {
                while (dr.Read())
                {
                    M_TB_EMPR e = new M_TB_EMPR();
                    e.FI_COD_EMPR = Convert.ToInt32(dr["FI_COD_EMPR"].ToString());
                    e.FS_NOM_EMPR = dr["FS_NOM_EMPR"].ToString();
                    e.FS_NUM_RUCC = dr["FS_NUM_RUCC"].ToString();
                    e.FS_DIR_PRIN = dr["FS_DIR_PRIN"].ToString();
                    e.FS_NUM_TEL1 = dr["FS_NUM_TEL1"].ToString();
                    e.FS_NUM_TEL2 = dr["FS_NUM_TEL2"].ToString();
                    e.FS_PAG_WEBB = dr["FS_PAG_WEBB"].ToString();
                    e.FS_RUT_LOGO_EMPR = dr["FS_RUT_LOGO_EMPR"].ToString();

                    temp.Add(e);
                }
                dr.Close();
            }

            return temp;
        }
    }
}