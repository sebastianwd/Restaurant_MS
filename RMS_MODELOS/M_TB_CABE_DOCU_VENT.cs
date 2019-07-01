using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_MODELOS
{
    public class M_TB_CABE_DOCU_VENT
    {
        [Required]
        public decimal FN_IDE_DOCU { get; set; }

        [Required]
        public int FI_COD_EMPR { get; set; }

        [Required]
        [MaxLength(4)]
        public string FS_TIP_DOCU { get; set; }

        [Required]
        public DateTime FD_FEC_DOCU { get; set; }

        [Required]
        [MaxLength(20)]
        public string FS_COD_CLIE { get; set; }

        [Required]
        public decimal FN_IDE_ORDE { get; set; }

        [MaxLength(200)]
        public string FS_NOM_CLIE { get; set; }

        [MaxLength(20)]
        public string FS_NUM_RUCS { get; set; }

        [MaxLength(200)]
        public string FS_DES_DIRE { get; set; }

        [Required]
        public decimal FN_IMP_TOTA { get; set; }

        [MaxLength(4)]
        public string FS_COD_MONE { get; set; }

        [MaxLength(4)]
        public string FS_COD_ESTA_DOCU { get; set; }

        [MaxLength(100)]
        public string FS_DES_OBSE { get; set; }

        [MaxLength(20)]
        public string FS_COD_EJEC { get; set; }

        [Required]
        [MaxLength(20)]
        public string FS_NUM_DOCU { get; set; }

        public string FS_NUM_DOCU_CLIE { get; set; }
    }
}