using Newtonsoft.Json;
using RMS_MODELOS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Restaurant_MS
{
    public class Utils
    {
        public static bool HasWritePermissionOnDir(string path)
        {
            var writeAllow = false;
            var writeDeny = false;
            var accessControlList = Directory.GetAccessControl(path);
            if (accessControlList == null)
                return false;
            var accessRules = accessControlList.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            if (accessRules == null)
                return false;

            foreach (FileSystemAccessRule rule in accessRules)
            {
                if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write) continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                    writeAllow = true;
                else if (rule.AccessControlType == AccessControlType.Deny)
                    writeDeny = true;
            }

            return writeAllow && !writeDeny;
        }

        public static int GetInstance(HttpRequestBase request)
        {
            HttpCookie cookie = request.Cookies.Get(FormsAuthentication.FormsCookieName);
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            var userData = JsonConvert.DeserializeObject<M_TB_USUA>(ticket.UserData);

            return userData.current_instance;
        }
    }
}