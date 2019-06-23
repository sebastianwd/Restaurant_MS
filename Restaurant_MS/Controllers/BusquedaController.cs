using RMS_MODELOS;
using RMS_SQL;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Restaurant_MS.Controllers
{
    public class BusquedaController : Controller
    {
        private SQL_TB_CLIE S_TB_CLIE = new SQL_TB_CLIE();
        private SQL_TB_TIPO_PERS_SUNA S_TB_TIPO_PERS_SUNA = new SQL_TB_TIPO_PERS_SUNA();

        private ResponseModel res = new ResponseModel
        {
            result = "",
            response = true,
            error = ""
        };

        [AjaxOnly]
        public PartialViewResult Busqueda_clientes()
        {
            ViewBag.lista_TB_TIPO_PERS_SUNA = S_TB_TIPO_PERS_SUNA.listar();

            return PartialView("_busqueda_clientes", new M_TB_CLIE());
        }

        [HttpPost]
        public JsonResult ejecutar_busqueda(M_TB_CLIE O_TB_CLIE, string nro_documento, int top)
        {
            if (O_TB_CLIE.FS_COD_CLIE.Length > 20)
            {
                res.response = false;
                res.error = "Código debe ser máximo 20 caracteres";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            var auxiliar = new List<M_TB_CLIE>();

            auxiliar = S_TB_CLIE.buscar_por_filtros(O_TB_CLIE, nro_documento, top);
            res.result = "OK";

            return Json(new { res.response, res.result, data = auxiliar }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Busqueda_cliente_defecto(int FI_STA_DEFE)
        {
            M_TB_CLIE data = S_TB_CLIE.buscar_cliente_defecto(FI_STA_DEFE);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Busqueda_cliente_por_codigo(string FS_COD_CLIE)
        {
            M_TB_CLIE data = S_TB_CLIE.buscar_cliente_por_codigo(FS_COD_CLIE);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}