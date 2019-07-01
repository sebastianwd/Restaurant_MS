using RMS_MODELOS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_SQL
{
    public class SQL_TB_CABE_DOCU_VENT
    {
        public string obtener_numero_nuevo_documento_correlativo(int FI_COD_EMPR, string FS_TIP_DOCU)
        {
            string FS_IDE_DOCU;

            using (AdoHelper db = new AdoHelper())
            {
                FS_IDE_DOCU = db.ExecScalarProc("SP_CABE_DOCU_VENT_BU01",
                    "@IICOD_EMPR", FI_COD_EMPR,
                    "@ISTIP_DOCU", FS_TIP_DOCU).ToString();
            }

            return FS_IDE_DOCU;
        }

        public M_TB_CABE_DOCU_VENT cargar_orden(decimal FN_IDE_ORDE, int FI_COD_EMPR)
        {
            var temp = new List<M_TB_CABE_DOCU_VENT>();

            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReaderProc("SP_CABE_ORDE_BU01",
                        "@IICOD_EMPR", FI_COD_EMPR,
                        "@INIDE_ORDE", FN_IDE_ORDE))
            {
                while (dr.Read())
                {
                    M_TB_CABE_DOCU_VENT e = new M_TB_CABE_DOCU_VENT();
                    e.FI_COD_EMPR = Convert.ToInt32(dr["FI_COD_EMPR"].ToString());
                    e.FN_IDE_ORDE = Convert.ToDecimal(dr["FN_IDE_ORDE"].ToString());
                    e.FD_FEC_DOCU = Convert.ToDateTime(dr["FD_FEC_ORDE"].ToString());
                    e.FS_NOM_CLIE = dr["FS_NOM_CLIE"].ToString();
                    e.FS_NUM_RUCS = dr["FS_NUM_RUCS"].ToString();
                    e.FS_NUM_DOCU_CLIE = dr["FS_NUM_DOCU_IDEN"].ToString();
                    e.FS_COD_CLIE = dr["FS_COD_CLIE"].ToString();
                    e.FN_IMP_TOTA = Convert.ToDecimal(dr["FN_IMP_TOTA"]);
                    e.FS_COD_EJEC = dr["FS_COD_EJEC"].ToString();

                    temp.Add(e);
                }
                dr.Close();
            }
            return temp.FirstOrDefault();
        }

        public decimal obtener_numero_nuevo_documento(int FI_COD_EMPR)
        {
            decimal FS_IDE_DOCU = 0;

            using (AdoHelper db = new AdoHelper())
            {
                FS_IDE_DOCU = Convert.ToDecimal(db.ExecScalar("select IsNull(max(FN_IDE_DOCU),0) +1 from TB_CABE_DOCU_VENT").ToString());
            }

            return FS_IDE_DOCU;
        }

        public IEnumerable<M_TB_DETA_DOCU_VENT> listar_detalle_venta(decimal FN_IDE_DOCU, int FI_COD_EMPR)
        {
            var temp = new List<M_TB_DETA_DOCU_VENT>();

            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReaderProc("SP_DETA_DOCU_VENT_BU01", "@INIDE_DOCU", FN_IDE_DOCU, "@IICOD_EMPR",
                FI_COD_EMPR))
            {
                while (dr.Read())
                {
                    M_TB_DETA_DOCU_VENT e = new M_TB_DETA_DOCU_VENT();
                    e.FN_IDE_DOCU = Convert.ToDecimal(dr["FN_IDE_DOCU"].ToString());
                    e.FI_COD_EMPR = Convert.ToInt32(dr["FI_COD_EMPR"].ToString());
                    e.FI_NUM_SECU = Convert.ToInt32(dr["FI_NUM_SECU"].ToString());
                    e.FD_FEC_DOCU = Convert.ToDateTime(dr["FD_FEC_DOCU"].ToString());
                    e.FS_TIP_DOCU = dr["FS_TIP_DOCU"].ToString();
                    e.FS_COD_ARTI = dr["FS_COD_ARTI"].ToString();
                    e.FN_CAN_ARTI = Convert.ToDecimal(dr["FN_CAN_ARTI"].ToString());
                    e.FN_PRE_VENT = Convert.ToDecimal(dr["FN_PRE_VENT"].ToString());
                    temp.Add(e);
                }
                dr.Close();
            }

            return temp;
        }

        public int agregar_venta(M_TB_CABE_DOCU_VENT reg, List<M_TB_DETA_ORDE> detalle)
        {
            int result = 0;
            using (AdoHelper db = new AdoHelper())
            {
                db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                try
                {
                    result = db.ExecNonQueryProc("SP_CABE_DOCU_VENT_AD01",
                        "@INIDE_DOCU", reg.FN_IDE_DOCU,
                        "@IICOD_EMPR", reg.FI_COD_EMPR,
                       "@ISTIP_DOCU", reg.FS_TIP_DOCU,
                       "@IDFEC_DOCU", reg.FD_FEC_DOCU,
                       "@ISCOD_CLIE", reg.FS_COD_CLIE,
                       "@INIDE_ORDE", reg.FN_IDE_ORDE,
                       "@ISNOM_CLIE", reg.FS_NOM_CLIE,
                       "@ISNUM_RUCS", reg.FS_NUM_RUCS,
                       "@ISDES_DIRE", reg.FS_DES_DIRE,
                       "@INIMP_TOTA", reg.FN_IMP_TOTA,
                       "@ISCOD_MONE", reg.FS_COD_MONE,
                       "@ISCOD_ESTA_DOCU", reg.FS_COD_ESTA_DOCU,
                       "@ISDES_OBSE", reg.FS_DES_OBSE,
                       "@ISCOD_EJEC", reg.FS_COD_EJEC,
                       "@ISNUM_DOCU", reg.FS_NUM_DOCU);

                    foreach (M_TB_DETA_ORDE x in detalle)
                    {
                        result = db.ExecNonQueryProc("SP_DETA_DOCU_VENT_AD01",
                        "@INIDE_DOCU", reg.FN_IDE_DOCU,
                        "@IICOD_EMPR", reg.FI_COD_EMPR,
                       "@IINUM_SECU", x.FI_NUM_SECU,
                       "@IDFEC_DOCU", reg.FD_FEC_DOCU,
                       "@ISTIP_DOCU", reg.FS_TIP_DOCU,
                       "@ISCOD_ARTI", x.FS_COD_ARTI,
                       "@INCAN_ARTI", x.FN_CAN_ARTI,
                        "@INPRE_VENT", x.FN_PRE_VENT

                      );
                    }
                    db.Commit();
                }
                catch (Exception e)
                {
                    db.Rollback();
                }
                return result;
            }
        }
    }
}