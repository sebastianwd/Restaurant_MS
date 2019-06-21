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

        AdoHelper db = new AdoHelper();

        public IEnumerable <M_TB_MESA> listar_mesas()
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

    }
}
