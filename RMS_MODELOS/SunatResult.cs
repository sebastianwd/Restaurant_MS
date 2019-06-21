using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_MODELOS
{
   public class SunatResult
    {
     
            public string ruc { get; set; }
            public string razon_social { get; set; }
            public string condicion { get; set; }
            public string nombre_comercial { get; set; }
            public string tipo { get; set; }
            public string fecha_inscripcion { get; set; }
            public string estado { get; set; }
            public string direccion { get; set; }
            public string sistema_emision { get; set; }
            public string actividad_exterior { get; set; }
            public string sistema_contabilidad { get; set; }
            public string emision_electronica { get; set; }
            public List<string> comprobante_electronico { get; set; }
            public string ple { get; set; }
            public string inicio_actividades { get; set; }
        }
}
