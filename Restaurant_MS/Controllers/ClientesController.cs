﻿using RMS_MODELOS;
using RMS_SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_MS.Controllers
{
    public class ClientesController : Controller
    {
        private SQL_TB_CLIE S_TB_CLIE = new SQL_TB_CLIE();

        private ResponseModel res = new ResponseModel
        {
            result = "",
            response = true,
            error = ""
        };

        // GET: Clientes
        public ActionResult Index()
        {
            ViewBag.status = "AGREGAR";
            return View(new M_TB_CLIE());
        }

        [HttpGet]
        [AjaxOnly]
        public JsonResult listar_clientes()
        {
            var temp = S_TB_CLIE.listar_todos();

            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public PartialViewResult cargar_cliente(string FS_COD_CLIE)
        {
            ViewBag.status = "EDITAR";
            var reg = S_TB_CLIE.buscar_cliente_por_codigo(FS_COD_CLIE);
            return PartialView("_form", reg);
        }

        [HttpPost]
        public JsonResult registrar_cliente(M_TB_CLIE reg, string status)
        {
            reg.FS_TIP_SITU = "ACTIVO";
            reg.FS_TIP_CLIE = "0200";
            reg.FD_FEC_REGI = DateTime.Today;
            if (!ModelState.IsValid)
            {
                res.response = false;
                ModelError error_msg = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault();
                res.error = error_msg.ErrorMessage;
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var n = 0;
                if  (status == "AGREGAR")
                {
                     n = S_TB_CLIE.agregar_cliente(reg);
                }else if ( status == "EDITAR")
                {
                    // actualizar
                     n = S_TB_CLIE.actualizar_cliente(reg);
                }

                if (n < 1)
                {
                    res.response = false;
                    res.error = "No se pudo completar la operación";
                    return Json(res, JsonRequestBehavior.AllowGet);
                }

                res.result = reg.FS_COD_CLIE;
            }
            catch (Exception e)
            {
                res.error = "Error at OrdenController - method registrar_cliente" + e.Message;
                if (e.Message.Contains("PRIMARY KEY"))
                {
                    res.error = "Cliente con código " + reg.FS_COD_CLIE + " ya existe";
                }
                res.response = false;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult eliminar_cliente(string FS_COD_CLIE)
        {
            try
            {
                var result = S_TB_CLIE.eliminar_cliente(FS_COD_CLIE);

                res.result = "Cliente eliminado";
            }
            catch (Exception e)
            {
                res.response = false;
                res.error = e.Message;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}