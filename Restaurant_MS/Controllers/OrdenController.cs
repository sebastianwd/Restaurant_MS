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

        public ActionResult Registro()
        {
            return View();
        }
    }
}