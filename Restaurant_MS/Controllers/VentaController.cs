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
        private SQL_TB_CABE_ORDE S_TB_CABE_ORDE = new SQL_TB_CABE_ORDE();

        private SQL_TB_DETA_ORDE S_TB_DETA_ORDE = new SQL_TB_DETA_ORDE();

        private ResponseModel res = new ResponseModel
        {
            result = "",
            response = true,
            error = ""
        };

        // GET: Venta
        public ActionResult Registro()
        {
            ViewBag.lista_tipo_documentos = new SelectList(S_TB_TIPO_DOCU_SIST.listar_tipos_documentos(), "FS_COD_TIDO_SIST", "FS_DES_TIDO_SIST");

            ViewBag.numero_venta = S_TB_CABE_DOCU_VENT.obtener_numero_nuevo_documento(1);
            ViewBag.numero_correlativo = S_TB_CABE_DOCU_VENT.obtener_numero_nuevo_documento_correlativo(1, "BOL");
            ViewBag.status = "AGREGAR";

            return View(new M_TB_CABE_DOCU_VENT());
        }

        public PartialViewResult Cargar_Orden(decimal FN_IDE_ORDE)
        {
            M_TB_CABE_DOCU_VENT reg = new M_TB_CABE_DOCU_VENT();
            reg = S_TB_CABE_DOCU_VENT.cargar_orden(FN_IDE_ORDE, 1);
            ViewBag.numero_venta = S_TB_CABE_DOCU_VENT.obtener_numero_nuevo_documento(1);
            ViewBag.lista_tipo_documentos = new SelectList(S_TB_TIPO_DOCU_SIST.listar_tipos_documentos(), "FS_COD_TIDO_SIST", "FS_DES_TIDO_SIST");
            ViewBag.numero_correlativo = S_TB_CABE_DOCU_VENT.obtener_numero_nuevo_documento_correlativo(1, "BOL");

            ViewBag.status = "AGREGAR";
            return PartialView("_venta_form", reg);
        }

        public PartialViewResult Cargar_Venta(decimal FN_IDE_DOCU)
        {
            M_TB_CABE_DOCU_VENT reg = new M_TB_CABE_DOCU_VENT();
            reg = S_TB_CABE_DOCU_VENT.cargar_venta(FN_IDE_DOCU, 1);
            ViewBag.numero_venta = FN_IDE_DOCU;
            ViewBag.lista_tipo_documentos = new SelectList(S_TB_TIPO_DOCU_SIST.listar_tipos_documentos(), "FS_COD_TIDO_SIST", "FS_DES_TIDO_SIST", reg.FS_TIP_DOCU);
            ViewBag.numero_correlativo = reg.FS_NUM_DOCU;

            ViewBag.status = "EDITAR";
            return PartialView("_venta_form", reg);
        }

        [HttpGet]
        public JsonResult listar_detalle_venta(decimal FN_IDE_DOCU)
        {
            var temp = S_TB_CABE_DOCU_VENT.listar_detalle_venta(FN_IDE_DOCU, 1);

            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult registrar_venta(M_TB_CABE_DOCU_VENT reg)
        {
            reg.FS_COD_ESTA_DOCU = "ACT";
            reg.FS_COD_MONE = "SOL";
            reg.FS_DES_DIRE = "";
            reg.FS_COD_EJEC = "";
            if (!ModelState.IsValid)
            {
                res.response = false;
                ModelError error_msg = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault();
                res.error = error_msg.ErrorMessage;
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            try
            {
                reg.FN_IDE_DOCU = S_TB_CABE_DOCU_VENT.obtener_numero_nuevo_documento(1);
                reg.FI_COD_EMPR = 1;

                var n = S_TB_CABE_DOCU_VENT.agregar_venta(reg, (List<M_TB_DETA_ORDE>)Session["detalle_venta"]);

                if (n < 1)
                {
                    res.response = false;
                    res.error = "No se pudo completar la operación";
                    return Json(res, JsonRequestBehavior.AllowGet);
                }

                res.result = reg.FN_IDE_DOCU.ToString();
            }
            catch (Exception e)
            {
                res.response = false;
                res.error = "Error at OrdenController - method registrar_venta " + e.Message;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listar_detalle_orden(decimal FN_IDE_ORDE, int FI_COD_EMPR)
        {
            Session["detalle_venta"] = new List<M_TB_DETA_ORDE>();
            var temp = S_TB_DETA_ORDE.listar_detalle_orden(FN_IDE_ORDE, FI_COD_EMPR);
            Session["detalle_venta"] = temp;
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult obtener_nuevo_documento_correlativo(string FS_TIP_DOCU)
        {
            var temp = S_TB_CABE_DOCU_VENT.obtener_numero_nuevo_documento_correlativo(1, FS_TIP_DOCU);
            return Json(temp, JsonRequestBehavior.AllowGet);
        }
    }
}