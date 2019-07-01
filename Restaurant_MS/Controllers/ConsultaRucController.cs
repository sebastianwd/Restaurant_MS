using Newtonsoft.Json;
using RMS_MODELOS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_MS.Controllers
{
    public class ConsultaRucController : Controller
    {
        // GET: ConsultaRuc
        public ActionResult Index()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<JsonResult> ConsultaRucAsync(string codigo)
        {
            using (var client = new HttpClient())
            {
                var values = new List<KeyValuePair<string, string>>();
                values.Add(new KeyValuePair<string, string>("nruc", codigo));

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync("https://demos.peruanosenlinea.com/sunat/example/consulta.php", content);

                var res = await response.Content.ReadAsStringAsync();

                return Json(JsonConvert.DeserializeObject<RootObject>(res), JsonRequestBehavior.AllowGet);
            }
        }
    }
}