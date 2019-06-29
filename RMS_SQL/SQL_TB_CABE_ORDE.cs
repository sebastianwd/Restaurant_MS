using RMS_MODELOS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_SQL
{
    public class SQL_TB_CABE_ORDE
    {
        public decimal obtener_numero_nueva_orden(int FI_COD_EMPR)
        {
            decimal FN_IDE_ORDE = 0;

            using (AdoHelper db = new AdoHelper())
            {
                FN_IDE_ORDE = Convert.ToDecimal(db.ExecScalar("select IsNull(max(FN_IDE_ORDE),1) +1 from TB_CABE_ORDE"));
            }

            return FN_IDE_ORDE;
        }

        public M_TB_CABE_ORDE buscar_por_codigo(decimal FN_IDE_ORDE, int FI_COD_EMPR)
        {
            var temp = new List<M_TB_CABE_ORDE>();

            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReaderProc("SP_CABE_ORDE_BU01",
                        "@IICOD_EMPR", FI_COD_EMPR,
                        "@INIDE_ORDE", FN_IDE_ORDE))
            {
                while (dr.Read())
                {
                    M_TB_CABE_ORDE e = new M_TB_CABE_ORDE();
                    e.FI_COD_EMPR = Convert.ToInt32(dr["FI_COD_EMPR"].ToString());
                    e.FN_IDE_ORDE = Convert.ToDecimal(dr["FN_IDE_ORDE"].ToString());
                    e.FD_FEC_ORDE = Convert.ToDateTime(dr["FD_FEC_ORDE"].ToString());
                    e.FS_NOM_CLIE = dr["FS_NOM_CLIE"].ToString();
                    e.FS_NUM_RUCS = dr["FS_NUM_RUCS"].ToString();
                    e.FS_COD_CLIE = dr["FS_COD_CLIE"].ToString();
                    e.FN_IMP_TOTA = Convert.ToDecimal(dr["FN_IMP_TOTA"]);
                    e.FS_COD_MESA = dr["FS_COD_MESA"].ToString();
                    e.FS_TIP_SITU = dr["FS_TIP_SITU"].ToString();
                    e.FS_COD_EJEC = dr["FS_COD_EJEC"].ToString();
                    e.FS_TIP_CLIE = dr["FS_TIP_CLIE"].ToString();

                    e.FS_NUM_DOCU_IDEN = dr["FS_NUM_DOCU_IDEN"].ToString();
                    temp.Add(e);
                }
                dr.Close();
            }
            return temp.FirstOrDefault();
        }

        public int agregar_orden(M_TB_CABE_ORDE reg, List<M_TB_DETA_ORDE> detalle)
        {
            int result = 0;
            using (AdoHelper db = new AdoHelper())
            {
                db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                try
                {
                    result = db.ExecNonQueryProc("SP_CABE_ORDE_AD01",
                        "@IICOD_EMPR", reg.FI_COD_EMPR,
                        "@INIDE_ORDE", reg.FN_IDE_ORDE,
                       "@IDFEC_ORDE", reg.FD_FEC_ORDE,
                       "@ISNOM_CLIE", reg.FS_NOM_CLIE,
                       "@ISNUM_RUCS", reg.FS_NUM_RUCS,
                       "@ISCOD_CLIE", reg.FS_COD_CLIE,
                       "@INIMP_TOTA", reg.FN_IMP_TOTA,
                       "@ISCOD_MESA", reg.FS_COD_MESA,
                       "@ISTIP_SITU", reg.FS_TIP_SITU,
                       "@ISCOD_EJEC", reg.FS_COD_EJEC,
                       "@ISTIP_CLIE", reg.FS_TIP_CLIE,
                       "@ISNUM_DOCU_CLIE", reg.FS_NUM_DOCU_IDEN);

                    foreach (M_TB_DETA_ORDE x in detalle)
                    {
                        result = db.ExecNonQueryProc("SP_DETA_ORDE_AD01",
                        "@INIDE_ORDE", reg.FN_IDE_ORDE,
                        "@IINUM_SECU", x.FI_NUM_SECU,
                       "@IICOD_EMPR", reg.FI_COD_EMPR,
                       "@INPRE_VENT", x.FN_PRE_VENT,
                       "@INCAN_ARTI", x.FN_CAN_ARTI,
                       "@ISTIP_SITU", x.FS_TIP_SITU,
                       "@ISCOD_ARTI", x.FS_COD_ARTI
                      );
                    }
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                }
                return result;
            }
        }

        public IEnumerable<M_TB_CABE_ORDE> listar_ordenes()
        {
            var temp = new List<M_TB_CABE_ORDE>();

            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReaderProc("SP_CABE_ORDE_BU02"))
            {
                while (dr.Read())
                {
                    M_TB_CABE_ORDE e = new M_TB_CABE_ORDE();
                    e.FN_IDE_ORDE = Convert.ToDecimal(dr["FN_IDE_ORDE"].ToString());
                    e.FD_FEC_ORDE = Convert.ToDateTime(dr["FS_DES_TIDO_SIST"].ToString());
                    e.FS_COD_CLIE = dr["FS_COD_CLIE"].ToString();
                    e.FS_COD_CLIE = dr["FS_COD_CLIE"].ToString();
                    temp.Add(e);
                }
                dr.Close();
            }

            return temp;
        }
    }
}