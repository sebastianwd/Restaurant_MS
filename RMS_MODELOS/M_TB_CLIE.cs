using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_MODELOS
{
   public  class M_TB_CLIE
    {

        [Required]
        [MaxLength(20)]
        public string FS_COD_CLIE { get; set; }

        [MaxLength(50)]
        public string FS_APE_PATE_CLIE { get; set; }

        [MaxLength(50)]
        public string FS_APE_MATE_CLIE { get; set; }

        [Required]
        [MaxLength(50)]
        public string FS_NOM_CLIE_NA01 { get; set; }

        [MaxLength(50)]
        public string FS_NOM_CLIE_NA02 { get; set; }

        [MaxLength(150)]
        public string FS_NOM_RAZO_SOCI { get; set; }

        [Required]
        public DateTime FD_FEC_REGI { get; set; }

        [MaxLength(20)]
        public string FS_NUM_DOCU_IDEN { get; set; }

        [MaxLength(20)]
        public string FS_NUM_RUCS { get; set; }

        [MaxLength(150)]
        public string FS_DES_MAIL { get; set; }

        [MaxLength(100)]
        public string FS_DES_OBSE { get; set; }

        [MaxLength(4)]
        public string FS_TIP_SITU { get; set; }

        [MaxLength(50)]
        public string FS_STA_SUNA { get; set; }

        [MaxLength(4)]
        public string FS_TIP_CLIE { get; set; }

    }
}
