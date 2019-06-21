using RMS_MODELOS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_SQL
{
    public class SQL_TB_TIPO_CLIE
    {


        public IEnumerable<M_TB_TIPO_CLIE> listar_tipo_cliente()
        {
            var temp = new List<M_TB_TIPO_CLIE>();

            using (AdoHelper db = new AdoHelper())
            using (SqlDataReader dr = db.ExecDataReader("select FS_TIP_CLIE as CODIGO,FS_DES_TIPO_CLIE as NOMBRE from TB_TIPO_CLIE "))
            {
                while (dr.Read())
                {
                    M_TB_TIPO_CLIE e = new M_TB_TIPO_CLIE();
                    e.FS_TIP_CLIE = dr["CODIGO"].ToString();
                    e.FS_DES_TIPO_CLIE = dr["NOMBRE"].ToString(); 
                    temp.Add(e);
                }
                dr.Close();
            }

            return temp;

        }
    }
}
