using RMS_MODELOS;
using RMS_SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_MS.Controllers
{
    public class MesaController : Controller
    {
        private SQL_TB_MESA S_TB_MESA = new SQL_TB_MESA();

        private ResponseModel res = new ResponseModel
        {
            result = "",
            response = true,
            error = ""
        };

        [AjaxOnly]
        public PartialViewResult gestion_mesa(string FS_COD_MESA)
        {
            var reg = new M_TB_MESA
            {
                FS_COD_MESA = FS_COD_MESA
            };

            return PartialView("_gestion_mesa", reg);
        }

        [AjaxOnly]
        public PartialViewResult registrar_mesa()
        {
            return PartialView("_agregar_mesa", new M_TB_MESA());
        }

        [AjaxOnly]
        public PartialViewResult lista_mesas()
        {
            return PartialView("_lista_mesas", S_TB_MESA.listar_mesas());
        }

        [HttpPost]
        public JsonResult liberar_mesa(string FS_COD_MESA)
        {
            try
            {
                var n = 0;
                var reg = new M_TB_MESA { FS_STA_OCUP = "N", FS_COD_MESA = FS_COD_MESA };
                n = S_TB_MESA.registrar_reservacion(reg);
                if (n < 1)
                {
                    res.response = false;
                    res.error = "No se pudo completar la operación";
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
                res.result = reg.FS_COD_MESA;
            }
            catch (Exception e)
            {
                res.error = "Error at OrdenController - method registrar_reservacion" + e.Message;
                res.response = false;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult registrar_reservacion(M_TB_MESA reg)
        {
            try
            {
                var n = 0;
                reg.FS_STA_OCUP = "S";
                n = S_TB_MESA.registrar_reservacion(reg);
                if (n < 1)
                {
                    res.response = false;
                    res.error = "No se pudo completar la operación";
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
                res.result = reg.FS_COD_MESA;
            }
            catch (Exception e)
            {
                res.error = "Error at OrdenController - method registrar_reservacion" + e.Message;
                res.response = false;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult agregar_mesa(M_TB_MESA reg)
        {
            try
            {
                var n = 0;
                reg.FS_STA_OCUP = "N";
                n = S_TB_MESA.registrar_mesa(reg);
                if (n < 1)
                {
                    res.response = false;
                    res.error = "No se pudo completar la operación";
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
                res.result = reg.FS_COD_MESA;
            }
            catch (Exception e)
            {
                res.error = "Error at OrdenController - method registrar_reservacion" + e.Message;
                res.response = false;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult eliminar_mesa(string FS_COD_MESA)
        {
            try
            {
                var n = 0;
                n = S_TB_MESA.eliminar_mesa(FS_COD_MESA);
                if (n < 1)
                {
                    res.response = false;
                    res.error = "No se pudo completar la operación";
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
                res.result = FS_COD_MESA;
            }
            catch (Exception e)
            {
                res.error = "Error at OrdenController - method eliminar_mesa" + e.Message;
                res.response = false;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}