using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_MODELOS
{
    public class M_TB_TIPO_PERS_SUNA
    {
        [MaxLength(2)]
        public string FS_COD_TIPE_SUNA { get; set; }

        [MaxLength(50)]
        public string FS_DES_TIPE_SUNA { get; set; }

        [MaxLength(1)]
        public string FS_IND_PERS { get; set; }
    }
}