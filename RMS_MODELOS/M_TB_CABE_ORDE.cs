using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_MODELOS
{
    public class M_TB_CABE_ORDE
    {
   
            [Required]
            public int FI_COD_EMPR { get; set; }

            [Required]
            public decimal FN_IDE_ORDE { get; set; }

            [Required]
             [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
             public DateTime FD_FEC_ORDE { get; set; }

            [MaxLength(200)]
            public string FS_NOM_CLIE { get; set; }

            [MaxLength(20)]
            public string FS_NUM_RUCS { get; set; }

            [Required]
            [MaxLength(20)]
            public string FS_COD_CLIE { get; set; }

            public decimal FN_IMP_TOTA { get; set; }

            [MaxLength(10)]
            public string FS_COD_MESA { get; set; }

            [Required]
            [MaxLength(4)]
            public string FS_TIP_SITU { get; set; }
 [MaxLength(20)]
           
            public string FS_COD_EJEC { get; set; }
        [MaxLength(4)]
        public string FS_TIP_CLIE { get; set; }


    }
}
