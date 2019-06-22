using RMS_MODELOS;
using RMS_SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_MS.Controllers
{
    public class HomeController : Controller
    {
        SQL_TB_MESA S_TB_MESA = new SQL_TB_MESA();
        public ActionResult Index()
        {


            return View(S_TB_MESA.listar_mesas());
        }



  

    }
}