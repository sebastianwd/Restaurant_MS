using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_MODELOS
{
   public class ResponseModel
    {
        public bool response { get; set; }
        public string redirect { get; set; }
        public string result { get; set; }
        public string error { get; set; }
    }
}
