using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_MODELOS
{
    public class M_TB_EJEC_VENT
    {
        [Required]
        [MaxLength(20)]
        public string FS_COD_EJEC { get; set; }

        [Required]
        public int FI_COD_EMPR { get; set; }

        [Required]
        [MaxLength(20)]
        public string FS_NOM_EJEC { get; set; }

        [MaxLength(20)]
        public string FS_APE_PATE_EJEC { get; set; }

        [MaxLength(20)]
        public string FS_APE_MATE_EJEC { get; set; }

        [MaxLength(4)]
        public string FS_TIP_SITU { get; set; }

        [MaxLength(10)]
        public string FS_COD_USUA { get; set; }

        [MaxLength(100)]
        public string FS_DES_MAIL { get; set; }
    }
}