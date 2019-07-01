using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_MODELOS
{
    public class M_TB_USUA
    {
        [Required]
        [MaxLength(10)]
        public string FS_COD_USUA { get; set; }

        [Required]
        [MaxLength(50)]
        public string FS_NOM_PRIM { get; set; }

        [MaxLength(50)]
        public string FS_NOM_SECU { get; set; }

        [MaxLength(50)]
        public string FS_APE_PATE { get; set; }

        [MaxLength(50)]
        public string FS_APE_MATE { get; set; }

        [Required]
        [MaxLength(20)]
        public string FS_CLA_USUA { get; set; }

        [MaxLength(20)]
        public string FS_NUM_TEL1 { get; set; }

        [MaxLength(50)]
        public string FS_DES_MAIL { get; set; }

        [Required]
        [MaxLength(4)]
        public string FS_TIP_SITU { get; set; }

        public int FI_NUM_SECU { get; set; }

        [MaxLength(4)]
        public string FS_TIP_USUA { get; set; }
    }
}