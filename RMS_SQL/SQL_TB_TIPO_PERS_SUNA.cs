using RMS_MODELOS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_SQL
{
    public class SQL_TB_TIPO_PERS_SUNA
    {
        public IEnumerable<M_TB_TIPO_PERS_SUNA> listar()
        {
            var temp = new List<M_TB_TIPO_PERS_SUNA>();
            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReader("select * from TB_TIPO_PERS_SUNA"))
            {
                while (dr.Read())
                {
                    M_TB_TIPO_PERS_SUNA e = new M_TB_TIPO_PERS_SUNA();
                    e.FS_COD_TIPE_SUNA = dr["FS_COD_TIPE_SUNA"].ToString();
                    e.FS_DES_TIPE_SUNA = dr["FS_DES_TIPE_SUNA"].ToString();
                    e.FS_IND_PERS = dr["FS_IND_PERS"].ToString();

                    temp.Add(e);
                }
                dr.Close();
            }

            return temp;
        }
    }
}