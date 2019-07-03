using RMS_MODELOS;
using RMS_SQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_MS.Controllers
{
    public class OrdenController : Controller
    {
        private SQL_TB_ARTI_CLAS S_TB_ARTI_CLAS = new SQL_TB_ARTI_CLAS();
        private SQL_TB_ARTI S_TB_ARTI = new SQL_TB_ARTI();
        private SQL_TB_DETA_ORDE S_TB_DETA_ORDE = new SQL_TB_DETA_ORDE();
        private SQL_TB_CABE_ORDE S_TB_CABE_ORDE = new SQL_TB_CABE_ORDE();
        private SQL_TB_TIPO_CLIE S_TB_TIPO_CLIE = new SQL_TB_TIPO_CLIE();
        private SQL_TB_CLIE S_TB_CLIE = new SQL_TB_CLIE();

        private ResponseModel res = new ResponseModel
        {
            result = "",
            response = true,
            error = ""
        };

        public ActionResult Registro()
        {
            Session["detalle_orden"] = new List<M_TB_DETA_ORDE>();
            ViewBag.numero_orden = S_TB_CABE_ORDE.obtener_numero_nueva_orden(1);

            var reg = new M_TB_CABE_ORDE { FS_TIP_SITU = "ACT" };

            ViewBag.status = "AGREGAR";

            return View(reg);
        }

        [HttpGet]
        [AjaxOnly]
        public PartialViewResult registro_form(decimal FN_IDE_ORDE)
        {
            M_TB_CABE_ORDE obj = new M_TB_CABE_ORDE();
            obj = S_TB_CABE_ORDE.buscar_por_codigo(FN_IDE_ORDE, 1);

            ViewBag.numero_orden = obj.FN_IDE_ORDE;

            ViewBag.status = "EDITAR";
            return PartialView("_registro_form", obj);
        }

        [ChildActionOnly]
        public PartialViewResult Lista_productos_por_clase()
        {
            var clases = S_TB_ARTI_CLAS.listar_clases_producto();
            var articulos_por_clase = clases.Select(p => new M_TB_ARTI_CLAS
            {
                FS_COD_CLAS = p.FS_COD_CLAS,
                FS_DES_CLAS = p.FS_DES_CLAS,

                lista_articulos = S_TB_ARTI.listar_productos().Where(x => x.FS_COD_CLAS == p.FS_COD_CLAS)
            });

            return PartialView("_lista_articulos", articulos_por_clase);
        }

        [HttpGet]
        public JsonResult listar_detalle_orden(decimal FN_IDE_ORDE, int FI_COD_EMPR)
        {
            var temp = S_TB_DETA_ORDE.listar_detalle_orden(FN_IDE_ORDE, FI_COD_EMPR);

            Session["detalle_orden"] = temp;

            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listar_tipo_cliente()
        {
            var temp = S_TB_TIPO_CLIE.listar_tipo_cliente();

            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult agregar_detalle_orden(string FS_COD_ARTI)
        {
            decimal total = 0;

            if (Session["detalle_orden"] == null)
            {
                Session["detalle_orden"] = new List<M_TB_DETA_ORDE>();
            }
            List<M_TB_DETA_ORDE> auxiliar = (List<M_TB_DETA_ORDE>)Session["detalle_orden"];

            if (FS_COD_ARTI == "" || FS_COD_ARTI == null)
            {
                res.response = false;
                res.error = "Artículo no existe";
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            if (auxiliar.Count(p => p.FS_COD_ARTI == FS_COD_ARTI) > 0)
            {
                auxiliar.Where(p => p.FS_COD_ARTI == FS_COD_ARTI).FirstOrDefault().FN_CAN_ARTI++;
                res.result = "Cantidad agregada";

                total = auxiliar.Sum(item => item.FN_PRE_VENT * item.FN_CAN_ARTI);
                return Json(new { response = res.response, result = res.result, data = auxiliar, total }, JsonRequestBehavior.AllowGet);
            }
            var O_TB_ARTI = S_TB_ARTI.buscar_por_codigo(FS_COD_ARTI).FirstOrDefault();
            if (O_TB_ARTI.FS_COD_ARTI == "" || O_TB_ARTI.FS_COD_ARTI == null)
            {
                res.response = false;
                res.error = "Artículo no existe";
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            var lastItemNumber = 1;
            if (auxiliar.Count > 0)
            {
                lastItemNumber = auxiliar
   .Aggregate((agg, next) =>
   next.FI_NUM_SECU > agg.FI_NUM_SECU ? next : agg).FI_NUM_SECU + 1;
            }
            M_TB_DETA_ORDE reg = new M_TB_DETA_ORDE
            {
                FI_NUM_SECU = lastItemNumber,
                FS_COD_ARTI = O_TB_ARTI.FS_COD_ARTI,
                FS_NOM_ARTI = O_TB_ARTI.FS_NOM_ARTI,
                FN_PRE_VENT = O_TB_ARTI.FN_IMP_VENT,
                FN_CAN_ARTI = 1
            };
            auxiliar.Add(reg);
            total = auxiliar.Sum(item => item.FN_PRE_VENT * item.FN_CAN_ARTI);

            res.result = "Artículo <strong>" + reg.FS_NOM_ARTI + "</strong> agregado";

            return Json(new { response = res.response, result = res.result, data = auxiliar, total }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult registrar_orden(M_TB_CABE_ORDE reg, string status)
        {
            reg.FS_TIP_SITU = "ACT";

            if (!ModelState.IsValid)
            {
                res.response = false;
                ModelError error_msg = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault();
                res.error = error_msg.ErrorMessage;
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            try
            {
                reg.FI_COD_EMPR = 1;
                var n = 0;
                if (status == "AGREGAR")
                {
                    reg.FN_IDE_ORDE = S_TB_CABE_ORDE.obtener_numero_nueva_orden(1);

                    n = S_TB_CABE_ORDE.agregar_orden(reg, (List<M_TB_DETA_ORDE>)Session["detalle_orden"]);
                }
                else
                {
                    n = S_TB_CABE_ORDE.actualizar_orden(reg, (List<M_TB_DETA_ORDE>)Session["detalle_orden"]);
                }

                if (n < 1)
                {
                    res.response = false;
                    res.error = "No se pudo completar la operación";
                    return Json(res, JsonRequestBehavior.AllowGet);
                }

                res.result = reg.FN_IDE_ORDE.ToString();
            }
            catch (Exception e)
            {
                res.response = false;
                res.error = "Error at OrdenController - method registrar_orden " + e.Message;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult buscar_cliente_por_tipo(string FS_TIP_CLIE)
        {
            M_TB_CLIE temp = S_TB_CLIE.buscar_por_tipo_cliente(FS_TIP_CLIE).FirstOrDefault();

            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult eliminar_detalle(string FS_COD_ARTI)
        {
            decimal total = 0;

            List<M_TB_DETA_ORDE> auxiliar = (List<M_TB_DETA_ORDE>)Session["detalle_orden"];

            if (FS_COD_ARTI == "" || FS_COD_ARTI == null)
            {
                res.response = false;
                res.error = "Artículo no existe";
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            if (auxiliar.Count(p => p.FS_COD_ARTI == FS_COD_ARTI) > 0)
            {
                var count = auxiliar.Where(p => p.FS_COD_ARTI == FS_COD_ARTI).FirstOrDefault().FN_CAN_ARTI--;
                if (count < 2)
                {
                    auxiliar.RemoveAt(auxiliar.FindIndex(p => p.FS_COD_ARTI == FS_COD_ARTI));
                }
                res.result = "Artículo removido";

                total = auxiliar.Sum(item => item.FN_PRE_VENT * item.FN_CAN_ARTI);
                return Json(new { response = res.response, result = res.result, data = auxiliar, total }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { response = res.response, result = res.result, data = auxiliar, total }, JsonRequestBehavior.AllowGet);
        }
    }
}