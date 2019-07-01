using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_MS.Controllers
{
    public class ArticulosController : Controller
    {
        // GET: Articulos
        public ActionResult Index()
        {
            return View();
        }
    }
}