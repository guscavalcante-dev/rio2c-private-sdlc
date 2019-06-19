using System;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateJsonAntiForgeryTokenAttribute : AuthorizeAttribute
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            log.Debug("A requisição do usuário passou em 'ValidateJsonAntiForgeryToken' no método 'OnAuthorization'");
            var request = filterContext.HttpContext.Request;

            if (request.HttpMethod == WebRequestMethods.Http.Post)
            {
                log.Debug("A requisição é do tipo post.");

                if (request.IsAjaxRequest())
                {
                    log.Debug("A requisição é do tipo ajax.");
                    log.Debug("será chamado o método 'AntiForgery.Validate'");
                    AntiForgery.Validate(CookieValue(request), request.Headers["__RequestVerificationToken"]);
                }

                else
                {
                    log.Debug("A requisição não é do tipo ajax.");
                    log.Debug("será chamado o método 'ValidateAntiForgeryTokenAttribute().OnAuthorization'");
                    new ValidateAntiForgeryTokenAttribute().OnAuthorization(filterContext);
                }
            }
        }

        private string CookieValue(HttpRequestBase request)
        {
            log.Debug("A requisição do usuário passou em 'ValidateJsonAntiForgeryToken' no método 'CookieValue'");

            var cookie = request.Cookies[AntiForgeryConfig.CookieName];
            var result = cookie != null ? cookie.Value : null;

            log.Debug(string.Format("O método 'CookieValue' irá retornar '{0}'", result));
            return result;
        }
    }
}
