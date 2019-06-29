using RMS_MODELOS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_SQL
{
    public class SQL_TB_TIPO_DOCU_SIST
    {
        public IEnumerable<M_TB_TIPO_DOCU_SIST> listar_tipos_documentos()
        {
            var temp = new List<M_TB_TIPO_DOCU_SIST>();

            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReader("select FS_COD_TIDO_SIST ,FS_DES_TIDO_SIST  from TB_TIPO_DOCU_SIST "))
            {
                while (dr.Read())
                {
                    M_TB_TIPO_DOCU_SIST e = new M_TB_TIPO_DOCU_SIST();
                    e.FS_COD_TIDO_SIST = dr["FS_COD_TIDO_SIST"].ToString();
                    e.FS_DES_TIDO_SIST = dr["FS_DES_TIDO_SIST"].ToString();
                    temp.Add(e);
                }
                dr.Close();
            }

            return temp;
        }
    }
}