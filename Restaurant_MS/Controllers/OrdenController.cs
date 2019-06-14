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
        SQL_TB_ARTI_CLAS S_TB_ARTI_CLAS = new SQL_TB_ARTI_CLAS();
        SQL_TB_ARTI S_TB_ARTI = new SQL_TB_ARTI();
        public ActionResult Registro()
        {


            return View(new M_TB_CABE_ORDE());
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

    }
}