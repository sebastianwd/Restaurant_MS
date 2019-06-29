using System;
using System.Collections.Generic;
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

        public decimal obtener_numero_nuevo_documento(int FI_COD_EMPR)
        {
            decimal FS_IDE_DOCU = 0;

            using (AdoHelper db = new AdoHelper())
            {
                FS_IDE_DOCU = Convert.ToDecimal(db.ExecScalar("select IsNull(max(FN_IDE_DOCU),0) +1 from TB_CABE_DOCU_VENT").ToString());
            }

            return FS_IDE_DOCU;
        }
    }
}