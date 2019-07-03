using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_MODELOS
{
    public class M_TB_MESA
    {
        [MaxLength(10, ErrorMessage = "Máximo 10 caracteres")]
        public string FS_COD_MESA { get; set; }

        public string FS_STA_OCUP { get; set; }
        public string FS_HOR_INIC_RESE { get; set; }
        public string FS_HOR_FINA_RESE { get; set; }
    }
}