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
        public M_TB_CLIE buscar_cliente_por_codigo(string FS_COD_CLIE)
        {
            var temp = new List<M_TB_CLIE>();

            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReaderProc("SP_CLIE_BU04", "@ISCOD_CLIE", FS_COD_CLIE))
            {
                while (dr.Read())
                {
                    M_TB_CLIE e = new M_TB_CLIE();
                    e.FS_COD_CLIE = dr["FS_COD_CLIE"].ToString();
                    e.FS_APE_PATE_CLIE = dr["FS_APE_PATE_CLIE"].ToString();
                    e.FS_APE_MATE_CLIE = dr["FS_APE_MATE_CLIE"].ToString();
                    e.FS_NOM_CLIE_NA01 = dr["FS_NOM_CLIE_NA01"].ToString();
                    e.FS_NOM_CLIE_NA02 = dr["FS_NOM_CLIE_NA02"].ToString();
                    e.FS_NOM_RAZO_SOCI = dr["FS_NOM_RAZO_SOCI"].ToString();
                    e.FD_FEC_REGI = Convert.ToDateTime(dr["FD_FEC_REGI"]);
                    e.FS_NUM_DOCU_IDEN = dr["FS_NUM_DOCU_IDEN"].ToString();
                    e.FS_NUM_RUCS = dr["FS_NUM_RUCS"].ToString();
                    e.FS_DES_MAIL = dr["FS_DES_MAIL"].ToString();
                    e.FS_DES_OBSE = dr["FS_DES_OBSE"].ToString();
                    e.FS_TIP_SITU = dr["FS_TIP_SITU"].ToString();
                    e.FI_STA_DEFE = Convert.ToInt32(dr["FI_STA_DEFE"]);
                    e.FS_NOM_CLIE = dr["FS_NOM_CLIE"].ToString();
                    e.FS_TIP_CLIE = dr["FS_TIP_CLIE"].ToString();
                    e.FS_DES_TIPO_CLIE = dr["FS_DES_TIPO_CLIE"].ToString();
                    temp.Add(e);
                }
                dr.Close();
            }

            return temp.FirstOrDefault();
        }

        public M_TB_CLIE buscar_cliente_defecto(int FI_STA_DEFE)
        {
            var temp = new List<M_TB_CLIE>();

            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReaderProc("SP_CLIE_BU03", "@IISTA_DEFE", FI_STA_DEFE))
            {
                while (dr.Read())
                {
                    M_TB_CLIE e = new M_TB_CLIE();
                    e.FS_COD_CLIE = dr["FS_COD_CLIE"].ToString();
                    e.FS_APE_PATE_CLIE = dr["FS_APE_PATE_CLIE"].ToString();
                    e.FS_APE_MATE_CLIE = dr["FS_APE_MATE_CLIE"].ToString();
                    e.FS_NOM_CLIE_NA01 = dr["FS_NOM_CLIE_NA01"].ToString();
                    e.FS_NOM_CLIE_NA02 = dr["FS_NOM_CLIE_NA02"].ToString();
                    e.FS_NOM_RAZO_SOCI = dr["FS_NOM_RAZO_SOCI"].ToString();
                    e.FD_FEC_REGI = Convert.ToDateTime(dr["FD_FEC_REGI"]);
                    e.FS_NUM_DOCU_IDEN = dr["FS_NUM_DOCU_IDEN"].ToString();
                    e.FS_NUM_RUCS = dr["FS_NUM_RUCS"].ToString();
                    e.FS_DES_MAIL = dr["FS_DES_MAIL"].ToString();
                    e.FS_DES_OBSE = dr["FS_DES_OBSE"].ToString();
                    e.FS_TIP_SITU = dr["FS_TIP_SITU"].ToString();
                    e.FS_COD_TIPE_SUNA = dr["FS_COD_TIPE_SUNA"].ToString();
                    e.FI_STA_DEFE = Convert.ToInt32(dr["FI_STA_DEFE"]);
                    e.FS_NOM_CLIE = dr["FS_NOM_CLIE"].ToString();
                    e.FS_TIP_CLIE = dr["FS_TIP_CLIE"].ToString();
                    e.FS_DES_TIPO_CLIE = dr["FS_DES_TIPO_CLIE"].ToString();
                    temp.Add(e);
                }
                dr.Close();
            }

            return temp.FirstOrDefault();
        }

        public List<M_TB_CLIE> buscar_por_tipo_cliente(string FS_TIP_CLIE)
        {
            var temp = new List<M_TB_CLIE>();

            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReaderProc("SP_CLIE_BU01", "@ISTIP_CLIE", FS_TIP_CLIE))
            {
                while (dr.Read())
                {
                    M_TB_CLIE e = new M_TB_CLIE();
                    e.FS_COD_CLIE = dr["FS_COD_CLIE"].ToString();
                    e.FS_APE_PATE_CLIE = dr["FS_APE_PATE_CLIE"].ToString();
                    e.FS_APE_MATE_CLIE = dr["FS_APE_MATE_CLIE"].ToString();
                    e.FS_NOM_CLIE_NA01 = dr["FS_NOM_CLIE_NA01"].ToString();
                    e.FS_NOM_CLIE_NA02 = dr["FS_NOM_CLIE_NA02"].ToString();
                    e.FS_NOM_RAZO_SOCI = dr["FS_NOM_RAZO_SOCI"].ToString();
                    e.FD_FEC_REGI = Convert.ToDateTime(dr["FD_FEC_REGI"]);
                    e.FS_NUM_DOCU_IDEN = dr["FS_NUM_DOCU_IDEN"].ToString();
                    e.FS_NUM_RUCS = dr["FS_NUM_RUCS"].ToString();
                    e.FS_DES_MAIL = dr["FS_DES_MAIL"].ToString();
                    e.FS_DES_OBSE = dr["FS_DES_OBSE"].ToString();
                    e.FS_TIP_SITU = dr["FS_TIP_SITU"].ToString();
                    e.FS_COD_TIPE_SUNA = dr["FS_COD_TIPE_SUNA"].ToString();
                    e.FI_STA_DEFE = Convert.ToInt32(dr["FI_STA_DEFE"]);
                    temp.Add(e);
                }
                dr.Close();
            }

            return temp;
        }

        public List<M_TB_CLIE> buscar_por_filtros(M_TB_CLIE O_TB_CLIE, string nro_documento, int top)
        {
            var temp = new List<M_TB_CLIE>();

            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReaderProc("SP_CLIE_BU02",
                "@ISCOD_CLIE", O_TB_CLIE.FS_COD_CLIE,
                "@ISNOM_CLIE", O_TB_CLIE.FS_NOM_CLIE,
                "@ISTIP_CLIE", O_TB_CLIE.FS_TIP_CLIE,
                "@ISTIP_SITU", O_TB_CLIE.FS_TIP_SITU,
                "@ISNUM_RUCS", nro_documento,
                "@ISCOD_TIPE_SUNA", O_TB_CLIE.FS_COD_TIPE_SUNA,
                "@ISTOP_REGI", top))
            {
                while (dr.Read())
                {
                    M_TB_CLIE e = new M_TB_CLIE();
                    e.FS_COD_CLIE = dr["FS_COD_CLIE"].ToString();
                    e.FS_NOM_CLIE = dr["FS_NOM_CLIE"].ToString();
                    e.FS_NUM_DOCU_IDEN = dr["FS_NUM_DOCU_IDEN"].ToString();
                    e.FS_NUM_RUCS = dr["FS_NUM_RUCS"].ToString();
                    temp.Add(e);
                }
                dr.Close();
            }
            return temp;
        }
    }
}