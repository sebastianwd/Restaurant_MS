using RMS_MODELOS;
using RMS_SQL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_MS.Controllers
{
    public class ArticulosController : Controller
    {
        private SQL_TB_ARTI S_TB_ARTI = new SQL_TB_ARTI();
        private SQL_TB_ARTI_CLAS S_TB_ARTI_CLAS = new SQL_TB_ARTI_CLAS();

        private ResponseModel res = new ResponseModel
        {
            result = "",
            response = true,
            error = ""
        };

        public ActionResult Index()
        {
            ViewBag.listado_clase = new SelectList(S_TB_ARTI_CLAS.listar_clases_producto(), "FS_COD_CLAS", "FS_DES_CLAS");
            ViewBag.status = "AGREGAR";

            return View(new M_TB_ARTI());
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult cargar_articulo(string FS_COD_ARTI)
        {
            M_TB_ARTI reg = S_TB_ARTI.buscar_por_codigo(FS_COD_ARTI).FirstOrDefault();
            ViewBag.listado_clase = new SelectList(S_TB_ARTI_CLAS.listar_clases_producto(), "FS_COD_CLAS", "FS_DES_CLAS", reg.FS_COD_CLAS);
            ViewBag.status = "EDITAR";

            return View("_form", reg);
        }

        [AjaxOnly]
        public JsonResult listar_articulos()
        {
            var temp = S_TB_ARTI.listar_productos();

            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult eliminar_articulo(string FS_COD_ARTI)
        {
            try
            {
                var n = 0;

                n = S_TB_ARTI.eliminar_articulo(FS_COD_ARTI);

                if (n < 1)
                {
                    res.response = false;
                    res.error = "No se pudo completar la operación";
                    return Json(res, JsonRequestBehavior.AllowGet);
                }

                res.result = FS_COD_ARTI;
            }
            catch (Exception e)
            {
                res.error = "Error at OrdenController - method registrar_articulo" + e.Message;
                if (e.Message.Contains("REFERENCE"))
                {
                    res.error = "Artículo " + FS_COD_ARTI + " está siendo usado en Órdenes, no se puede eliminar";
                }
                res.response = false;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult registrar_artículo(HttpPostedFileBase imagen_articulo, M_TB_ARTI reg, string status)
        {
            reg.FS_TIP_PRES = "";
            reg.FI_COD_EMPR = 1;
            reg.FS_TIP_SITU = "ACT";
            if (!ModelState.IsValid)
            {
                res.response = false;
                ModelError error_msg = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault();
                res.error = error_msg.ErrorMessage;
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            string[] valid_extensions = { ".png", ".jpg", ".svg", ".gif", ".jpeg" };
            string virtual_route = null;
            string physical_route = null;
            if (imagen_articulo != null)
            {
                string extension = Path.GetExtension(imagen_articulo.FileName);

                if (!Array.Exists(valid_extensions, element => element == extension))
                {
                    res.error = "Escoja una imagen con extensión válida";
                    res.response = false;
                    return Json(res, JsonRequestBehavior.AllowGet);
                }

                virtual_route = "/Fotos/comida/" + Path.GetFileName(imagen_articulo.FileName);
                physical_route = Path.Combine(Server.MapPath("~/Fotos/comida/"), Path.GetFileName(imagen_articulo.FileName));

                if (!Utils.HasWritePermissionOnDir(Server.MapPath("~/Fotos/comida/")))
                {
                    res.error = "No tiene acceso a la ruta configurada para guardar archivos";
                    res.response = false;
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
            }

            try
            {
                var n = 0;
                reg.FS_RUT_FOTO = virtual_route;
                if (status == "AGREGAR")
                {
                    n = S_TB_ARTI.agregar_articulo(reg);
                }
                else if (status == "EDITAR")
                {
                    // actualizar
                    n = S_TB_ARTI.actualizar_articulo(reg);
                }

                if (n < 1)
                {
                    res.response = false;
                    res.error = "No se pudo completar la operación";
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
                if (physical_route != null)
                {
                    imagen_articulo.SaveAs(physical_route);
                }
                res.result = reg.FS_COD_ARTI;
            }
            catch (Exception e)
            {
                res.error = "Error at OrdenController - method registrar_articulo" + e.Message;
                if (e.Message.Contains("PRIMARY KEY"))
                {
                    res.error = "Artículo con código " + reg.FS_COD_ARTI + " ya existe";
                }
                res.response = false;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}