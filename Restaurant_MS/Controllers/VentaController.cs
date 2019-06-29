using RMS_MODELOS;
using RMS_SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_MS.Controllers
{
    public class VentaController : Controller
    {
        private SQL_TB_TIPO_DOCU_SIST S_TB_TIPO_DOCU_SIST = new SQL_TB_TIPO_DOCU_SIST();
        private SQL_TB_CABE_DOCU_VENT S_TB_CABE_DOCU_VENT = new SQL_TB_CABE_DOCU_VENT();

        // GET: Venta
        public ActionResult Registro()
        {
            ViewBag.lista_tipo_documentos = new SelectList(S_TB_TIPO_DOCU_SIST.listar_tipos_documentos(), "FS_COD_TIDO_SIST", "FS_DES_TIDO_SIST");

            ViewBag.numero_venta = S_TB_CABE_DOCU_VENT.obtener_numero_nuevo_documento(1);
            ViewBag.numero_correlativo = S_TB_CABE_DOCU_VENT.obtener_numero_nuevo_documento_correlativo(1, "BOL");
            return View(new M_TB_CABE_DOCU_VENT());
        }
    }
}