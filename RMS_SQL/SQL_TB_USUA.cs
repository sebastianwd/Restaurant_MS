using RMS_MODELOS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_SQL
{
    public class SQL_TB_USUA
    {
        public M_TB_USUA buscar_por_codigo(string FS_COD_USUA)
        {
            var temp = new List<M_TB_USUA>();

            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReaderProc("SP_USUA_BU01", "@IICOD_USUA", FS_COD_USUA))
            {
                while (dr.Read())
                {
                    M_TB_USUA e = new M_TB_USUA();
                    e.FS_COD_USUA = dr["FS_COD_USUA"].ToString();
                    e.FS_NOM_PRIM = dr["FS_NOM_PRIM"].ToString();
                    e.FS_NOM_SECU = dr["FS_NOM_SECU"].ToString();
                    e.FS_APE_PATE = dr["FS_APE_PATE"].ToString();
                    e.FS_APE_MATE = dr["FS_APE_MATE"].ToString();
                    e.SetPassword(dr["FS_CLA_USUA"].ToString());
                    e.FS_NUM_TEL1 = dr["FS_NUM_TEL1"].ToString();
                    e.FS_DES_MAIL = dr["FS_DES_MAIL"].ToString();
                    e.FS_TIP_SITU = dr["FS_TIP_SITU"].ToString();
                    e.FI_NUM_SECU = Convert.ToInt32(dr["FI_NUM_SECU"].ToString());
                    e.FS_TIP_USUA = dr["FS_TIP_USUA"].ToString();
                    e.FS_DES_OBSE = dr["FS_DES_OBSE"].ToString();
                    e.FS_DES_ROLE = dr["FS_DES_ROLE"].ToString();
                    e.FD_FEC_USCR = Convert.ToDateTime(dr["FD_FEC_USCR"].ToString());
                    e.FS_RUT_FOTO = dr["FS_RUT_FOTO"].ToString();
                    temp.Add(e);
                }
                dr.Close();
            }

            return temp.FirstOrDefault();
        }

        public int actualizar_usuario(M_TB_USUA reg, int update_pwd)
        {
            int result = 0;
            using (AdoHelper db = new AdoHelper())
            {
                result = db.ExecNonQueryProc("SP_USUA_AC01",
                    "@ISCOD_USUA", reg.FS_COD_USUA,
                    "@ISNOM_PRIM", reg.FS_NOM_PRIM,
                    "@ISNOM_SECU", reg.FS_NOM_SECU,
                    "@ISAPE_PATE", reg.FS_APE_PATE,
                    "@ISAPE_MATE", reg.FS_APE_MATE,
                    "@ISCLA_USUA", reg.FS_CLA_USUA,
                    "@ISNUM_TEL1", reg.FS_NUM_TEL1,
                    "@ISDES_MAIL", reg.FS_DES_MAIL,
                    "@ISTIP_SITU", reg.FS_TIP_SITU,
                    "@IINUM_SECU", reg.FI_NUM_SECU,
                    "@ISTIP_USUA", reg.FS_TIP_USUA,
                    "@ISDES_OBSE", reg.FS_DES_OBSE,
                    "@ISDES_ROLE", reg.FS_DES_ROLE,
                    "@ISRUT_FOTO", reg.FS_RUT_FOTO,
                    "@update_pwd", update_pwd);
            }
            return result;
        }
    }
}