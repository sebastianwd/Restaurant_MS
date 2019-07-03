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

        public void SetPassword(string password)
        {
            FS_CLA_USUA = Crypto.Hash(password);
        }

        public bool CheckPassword(string password)
        {
            return string.Equals(FS_CLA_USUA, Crypto.Hash(password));
        }

        [MaxLength(20)]
        public string FS_NUM_TEL1 { get; set; }

        [MaxLength(50)]
        public string FS_DES_MAIL { get; set; }

        [MaxLength(4)]
        public string FS_TIP_SITU { get; set; }

        public int FI_NUM_SECU { get; set; }

        [MaxLength(4)]
        public string FS_TIP_USUA { get; set; }

        [MaxLength(100)]
        public string FS_DES_OBSE { get; set; }

        [MaxLength(100)]
        public string FS_DES_ROLE { get; set; }

        public DateTime FD_FEC_USCR { get; set; }

        public string FS_RUT_FOTO { get; set; }
        public int current_instance { get; set; }
    }
}