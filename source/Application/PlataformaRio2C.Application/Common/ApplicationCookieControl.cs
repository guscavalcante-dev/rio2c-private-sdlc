using PlataformaRio2C.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.Common
{
    public class ApplicationCookieControl
    {
        public ApplicationCookieControl(){}

        public HttpCookie SetCookie(string culture, HttpCookie cookie, string cookieName)
        {
            if (cookie != null)
            {
                cookie.Value = culture;   // update cookie value
            }
            else
            {
                cookie = new HttpCookie(cookieName);
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }

            return cookie;
        }
    }
}
