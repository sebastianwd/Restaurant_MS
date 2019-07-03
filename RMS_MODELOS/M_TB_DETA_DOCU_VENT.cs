using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_MODELOS
{
    public class M_TB_DETA_DOCU_VENT
    {
        [Required]
        public decimal FN_IDE_DOCU { get; set; }

        [Required]
        public int FI_COD_EMPR { get; set; }

        [Required]
        public int FI_NUM_SECU { get; set; }

        [Required]
        public DateTime FD_FEC_DOCU { get; set; }

        [Required]
        [MaxLength(4)]
        public string FS_TIP_DOCU { get; set; }

        [Required]
        [MaxLength(20)]
        public string FS_COD_ARTI { get; set; }

        public string FS_NOM_ARTI { get; set; }

        [Required]
        public decimal FN_CAN_ARTI { get; set; }

        [Required]
        public decimal FN_PRE_VENT { get; set; }
    }
}