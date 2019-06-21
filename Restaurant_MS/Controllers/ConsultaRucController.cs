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


        public FileResult imgCaptcha()
        {



            CookieContainer cookies = new CookieContainer();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
        HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create("http://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsmulruc/captcha?accion=image&magic=2");
        myWebRequest.CookieContainer = cookies;
            myWebRequest.Proxy = null;
            myWebRequest.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            FileStreamResult myImgStream = new FileStreamResult(myWebResponse.GetResponseStream(), "image/jpeg");

            Session["cookie_sunat"] = myWebRequest.CookieContainer;

            return myImgStream;
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