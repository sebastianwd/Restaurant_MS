using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_MODELOS
{
   public class M_TB_ARTI_CLAS
    {
        public string FS_COD_CLAS { get; set; }

        public string FS_DES_CLAS { get; set; }

        public IEnumerable<M_TB_ARTI> lista_articulos { get; set; }

    }
}
