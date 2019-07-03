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
        public IEnumerable<M_TB_ARTI> listar_por_clase(string FS_COD_CLAS)
        {
            var temp = new List<M_TB_ARTI>();
            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReader("select * from TB_ARTI where FS_COD_CLAS = @ISCOD_CLAS",
             "@ISCOD_CLAS", FS_COD_CLAS))
            {
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
                dr.Close();
            }

            return temp;
        }

        public IEnumerable<M_TB_ARTI> listar_productos()
        {
            var temp = new List<M_TB_ARTI>();

            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReader("select * from TB_ARTI T1 inner join TB_ARTI_CLAS" +
                " T2 On (T1.FS_COD_CLAS = T2.FS_COD_CLAS)"))
            {
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
                dr.Close();
            }
            return temp;
        }

        public IEnumerable<M_TB_ARTI> buscar_por_codigo(string FS_COD_ARTI)
        {
            var temp = new List<M_TB_ARTI>();

            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReader("select * from TB_ARTI where FS_COD_ARTI = @ISCOD_ARTI", "@ISCOD_ARTI", FS_COD_ARTI))
            {
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
                dr.Close();
            }
            return temp;
        }

        public bool is_product_real(string FS_COD_ARTI)
        {
            bool temp = false;

            using (AdoHelper db = new AdoHelper())
            {
                var result = db.ExecScalar("select 1 from TB_ARTI where FS_COD_ARTI = @ISCOD_ARTI", "@ISCOD_ARTI", FS_COD_ARTI);
                if (result != null)
                {
                    temp = true;
                }
            }
            return temp;
        }

        public int actualizar_articulo(M_TB_ARTI reg)
        {
            int result = 0;
            using (AdoHelper db = new AdoHelper())
            {
                result = db.ExecNonQueryProc("SP_ARTI_AC01",
                   "@ISCOD_ARTI", reg.FS_COD_ARTI,
                   "@IICOD_EMPR", reg.FI_COD_EMPR,
                   "@ISNOM_ARTI", reg.FS_NOM_ARTI,
                   "@ISCOD_CLAS", reg.FS_COD_CLAS,
                   "@ISTIP_PRES", reg.FS_TIP_PRES,
                   "@INIMP_VENT", reg.FN_IMP_VENT,
                   "@ISTIP_SITU", reg.FS_TIP_SITU,
                   "@ISDES_OBSE", reg.FS_DES_OBSE,
                   "@ISRUT_FOTO", reg.FS_RUT_FOTO

             );
            }
            return result;
        }

        public int agregar_articulo(M_TB_ARTI reg)
        {
            int result = 0;
            using (AdoHelper db = new AdoHelper())
            {
                result = db.ExecNonQueryProc("SP_ARTI_AD01",
                   "@ISCOD_ARTI", reg.FS_COD_ARTI,
                   "@IICOD_EMPR", reg.FI_COD_EMPR,
                   "@ISNOM_ARTI", reg.FS_NOM_ARTI,
                   "@ISCOD_CLAS", reg.FS_COD_CLAS,
                   "@ISTIP_PRES", reg.FS_TIP_PRES,
                   "@INIMP_VENT", reg.FN_IMP_VENT,
                   "@ISTIP_SITU", reg.FS_TIP_SITU,
                   "@ISDES_OBSE", reg.FS_DES_OBSE,
                   "@ISRUT_FOTO", reg.FS_RUT_FOTO

             );
            }
            return result;
        }
    }
}