using Newtonsoft.Json;
using RMS_MODELOS;
using RMS_SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Restaurant_MS.Controllers
{
    public class HomeController : Controller
    {
        private SQL_TB_MESA S_TB_MESA = new SQL_TB_MESA();
        private M_TB_USUA O_TB_USUA = new M_TB_USUA();
        private SQL_TB_USUA S_TB_USUA = new SQL_TB_USUA();
        private SQL_TB_EMPR S_TB_EMPR = new SQL_TB_EMPR();

        private static bool _secure = false;
        private static string _valuser;

        private ResponseModel resp = new ResponseModel
        {
            response = true,
            redirect = "/Home/Index",
            error = null
        };

        [Authorize]
        [HttpGet, OutputCache(NoStore = true, Duration = 1)]
        public ActionResult Index()
        {
            return View(S_TB_MESA.listar_mesas());
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            _secure = false;
            Session.Clear();
            FormsAuthentication.SignOut();
            Session.Abandon();
            if (Request.Cookies["C_FI_COD_EMPR"] != null)
            {
                var c = new HttpCookie("C_FI_COD_EMPR");
                c.Expires = DateTime.Now.AddDays(-1);
            }
            return View("Login");
        }

        [HttpPost]
        public JsonResult Ingresar(M_TB_USUA usuario, M_TB_EMPR empresa)
        {
            if (_secure == false)
            {
                usuario.SetPassword(usuario.FS_CLA_USUA);
                _valuser = usuario.FS_COD_USUA;

                O_TB_USUA = S_TB_USUA.buscar_por_codigo(usuario.FS_COD_USUA);

                if (!O_TB_USUA.CheckPassword(usuario.FS_CLA_USUA))
                {
                    resp.response = false;
                    resp.error = "Contraseña incorrecta";
                    return Json(resp);
                }
            }

            if (usuario.FS_COD_USUA == "" || usuario.FS_CLA_USUA == "")
            {
                resp.error = "Contraseña incorrecta";
                resp.response = false;
            }

            if (_secure == true)
            {
                Session["VS_FI_COD_EMPR"] = empresa.FI_COD_EMPR;

                var G_TB_USUA = new M_TB_USUA() { FS_COD_USUA = usuario.FS_COD_USUA, current_instance = empresa.FI_COD_EMPR };
                var serializedUser = Newtonsoft.Json.JsonConvert.SerializeObject(G_TB_USUA);

                var ticket = new FormsAuthenticationTicket(1, usuario.FS_COD_USUA, DateTime.Now, DateTime.Now.AddHours(6), false, serializedUser);

                var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                var isSsl = Request.IsSecureConnection;

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                {
                    HttpOnly = true,
                    Secure = isSsl,
                };

                Response.Cookies.Set(cookie);
                Conectar_Empresa(empresa.FI_COD_EMPR);
            }

            return Json(resp);
        }

        [HttpPost]
        public JsonResult Conectar_Empresa(int FI_COD_EMPR)
        {
            HttpCookie C_FI_COD_EMPR = new HttpCookie("C_FI_COD_EMPR", FI_COD_EMPR.ToString())
            {
                HttpOnly = true,
                Secure = Request.IsSecureConnection,
            };
            Response.Cookies.Set(C_FI_COD_EMPR);

            return Json(true);
        }

        [AllowAnonymous]
        public ActionResult ConexionEmpresa()
        {
            ViewBag.getEmpresa = S_TB_EMPR.listar();
            _secure = true;

            return View();
        }

        [Authorize]
        public ActionResult GetNombreUsuario()
        {
            return Content(User.Identity.Name);
        }

        [Authorize]
        public ActionResult GetImageUsuario()
        {
            var reg = S_TB_USUA.buscar_por_codigo(User.Identity.Name);

            return Content(reg.FS_RUT_FOTO);
        }

        [Authorize]
        public ActionResult GetNombreEmpresa()
        {
            var instance = Utils.GetInstance(Request);
            var O_TB_EMPR = S_TB_EMPR.buscar_por_codigo(instance);
            return Content("Empresa: (" + instance + ") " + O_TB_EMPR.FS_NOM_EMPR);
        }
    }
}