using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_MODELOS
{
    public class M_TB_ARTI
    {
        [Required]
        [MaxLength(20)]
        public string FS_COD_ARTI { get; set; }

        [Required]
        public int FI_COD_EMPR { get; set; }

        [Required]
        [MaxLength(100)]
        public string FS_NOM_ARTI { get; set; }

        [Required]
        [MaxLength(6)]
        public string FS_COD_CLAS { get; set; }

        [MaxLength(6)]
        public string FS_TIP_PRES { get; set; }

        [Required]
        public decimal FN_IMP_VENT { get; set; }

        [MaxLength(4)]
        public string FS_TIP_SITU { get; set; }

        [MaxLength(100)]
        public string FS_DES_OBSE { get; set; }

        public string FS_DES_CLAS { get; set; }

        public string FS_RUT_FOTO { get; set; }
    }
}