using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS_MODELOS;

namespace RMS_SQL
{
    public class SQL_TB_MESA
    {
        private AdoHelper db = new AdoHelper();

        public IEnumerable<M_TB_MESA> listar_mesas()
        {
            var temp = new List<M_TB_MESA>();

            using (SqlDataReader dr = db.ExecDataReader("select * from TB_MESA"))
            {
                while (dr.Read())
                {
                    M_TB_MESA e = new M_TB_MESA();
                    e.FS_COD_MESA = dr["FS_COD_MESA"].ToString();
                    e.FS_STA_OCUP = dr["FS_STA_OCUP"].ToString(); ;
                    e.FS_HOR_INIC_RESE = dr["FS_HOR_INIC_RESE"].ToString(); ;
                    e.FS_HOR_FINA_RESE = dr["FS_HOR_FINA_RESE"].ToString(); ;
                    temp.Add(e);
                }
                dr.Close();
            }

            return temp;
        }

        public int registrar_reservacion(M_TB_MESA reg)
        {
            int result = 0;
            using (AdoHelper db = new AdoHelper())
            {
                result = db.ExecNonQueryProc("SP_MESA_AC01"
                    , "@ISCOD_MESA", reg.FS_COD_MESA
                    , "@ISSTA_OCUP", reg.FS_STA_OCUP
                    , "@ISHOR_INIC_RESE", reg.FS_HOR_INIC_RESE
                    , "@ISHOR_FINA_RESE", reg.FS_HOR_FINA_RESE);
            }
            return result;
        }

        public int registrar_mesa(M_TB_MESA reg)
        {
            int result = 0;
            using (AdoHelper db = new AdoHelper())
            {
                result = db.ExecNonQueryProc("SP_MESA_AD01"
                    , "@ISCOD_MESA", reg.FS_COD_MESA
                    , "@ISSTA_OCUP", reg.FS_STA_OCUP
                    , "@ISHOR_INIC_RESE", reg.FS_HOR_INIC_RESE
                    , "@ISHOR_FINA_RESE", reg.FS_HOR_FINA_RESE);
            }
            return result;
        }

        public int eliminar_mesa(string FS_COD_MESA)
        {
            int result = 0;
            using (AdoHelper db = new AdoHelper())
            {
                result = db.ExecNonQueryProc("SP_MESA_EL01"
                    , "@IICOD_MESA", FS_COD_MESA
                  );
            }
            return result;
        }
    }
}