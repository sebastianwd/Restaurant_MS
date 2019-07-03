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
    public class UsuariosController : Controller
    {
        private SQL_TB_USUA S_TB_USUA = new SQL_TB_USUA();

        private ResponseModel res = new ResponseModel
        {
            result = "",
            response = true,
            error = ""
        };

        public ActionResult Index()
        {
            var reg = S_TB_USUA.buscar_por_codigo(User.Identity.Name);

            return View(reg);
        }

        [HttpPost]
        public JsonResult actualizar_usuario(M_TB_USUA reg, string pwd_check, HttpPostedFileBase imagen_usuario)
        {
            reg.FS_TIP_SITU = "ACT";
            reg.FS_TIP_USUA = "ADMI";
            int update_pwd = 0;
            if (!ModelState.IsValid)
            {
                res.response = false;
                ModelError error_msg = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault();
                res.error = error_msg.ErrorMessage;
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            if (pwd_check != null && pwd_check != "")
            {
                if (reg.FS_CLA_USUA.Length > 10)
                {
                    res.response = false;
                    res.error = "La contraseña debe tener máximo 10 caracteres";
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
                if (reg.FS_CLA_USUA != pwd_check)
                {
                    res.response = false;
                    res.error = "Las contraseñas no coinciden";
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
                update_pwd = 1;
            }

            string[] valid_extensions = { ".png", ".jpg", ".svg", ".gif", ".jpeg" };
            string virtual_route = null;
            string physical_route = null;
            if (imagen_usuario != null)
            {
                string extension = Path.GetExtension(imagen_usuario.FileName);

                if (!Array.Exists(valid_extensions, element => element == extension))
                {
                    res.error = "Escoja una imagen con extensión válida";
                    res.response = false;
                    return Json(res, JsonRequestBehavior.AllowGet);
                }

                virtual_route = "/Fotos/usuario/" + Path.GetFileName(imagen_usuario.FileName);
                physical_route = Path.Combine(Server.MapPath("~/Fotos/usuario/"), Path.GetFileName(imagen_usuario.FileName));

                if (!Utils.HasWritePermissionOnDir(Server.MapPath("~/Fotos/usuario/")))
                {
                    res.error = "No tiene acceso a la ruta configurada para guardar archivos";
                    res.response = false;
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
                reg.FS_RUT_FOTO = virtual_route;
            }

            try
            {
                var n = 0;
                n = S_TB_USUA.actualizar_usuario(reg, update_pwd);
                if (n < 1)
                {
                    res.response = false;
                    res.error = "No se pudo completar la operación";
                    return Json(res, JsonRequestBehavior.AllowGet);
                }

                if (physical_route != null)
                {
                    imagen_usuario.SaveAs(physical_route);
                }
                res.result = "OK";
            }
            catch (Exception e)
            {
                res.error = "Error at OrdenController - method registrar_reservacion" + e.Message;
                res.response = false;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}