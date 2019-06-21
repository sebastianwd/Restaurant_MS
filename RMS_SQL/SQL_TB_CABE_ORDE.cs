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
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["sql_connection"].ConnectionString);


        public decimal obtener_numero_nueva_orden(int FI_COD_EMPR)
        {
            decimal FN_IDE_ORDE = 0;

            using (AdoHelper db = new AdoHelper())
            {
                FN_IDE_ORDE = Convert.ToDecimal(db.ExecScalar("select IsNull(max(FN_IDE_ORDE),1) from TB_CABE_ORDE"));

            }


            return FN_IDE_ORDE;

        }

        public int agregar_orden(M_TB_CABE_ORDE reg)
        {

                int result = 0;
            using (AdoHelper db = new AdoHelper())
            {
                result = db.ExecNonQueryProc("SP_CABE_ORDE_AD01", reg.FI_COD_EMPR, reg.FN_IDE_ORDE, reg.FD_FEC_ORDE, reg.FS_NOM_CLIE,
                reg.FS_NUM_RUCS, reg.FS_COD_CLIE, reg.FN_IMP_TOTA, reg.FS_COD_MESA, reg.FS_TIP_SITU, reg.FS_COD_EJEC, reg.FS_TIP_CLIE);
            }
            return result;

        }
    }
}
