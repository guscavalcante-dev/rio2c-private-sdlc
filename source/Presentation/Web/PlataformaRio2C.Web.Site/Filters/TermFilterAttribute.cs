using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Infra.CrossCutting.Identity.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Web.Site.Controllers;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site
{
    public class TermFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        private IUserUseTermAppService _userUseTermAppService;

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            

            if (!skipAuthorization && !HasTermAccept(filterContext))
            {
                if (IsAjaxRequest(filterContext))
                {
                    filterContext.Result = new HttpStatusCodeResult(403);
                }
                else
                {
                    if (!(filterContext.Controller.GetType().Name == typeof(DashboardController).Name))
                    {
                        var controller = (Controller)filterContext.Controller;
                        controller.StatusMessage("Para prosseguir você deve aceitar o termo!", Infra.CrossCutting.Tools.Enums.StatusMessageType.Danger);
                    }

                    var area = HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"] != null ? HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"].ToString() : null;

                    if (area == "ProducerArea")
                    {
                        filterContext.Result = new RedirectResult("/Term/Producer");
                    }
                    else if (string.IsNullOrWhiteSpace(area))
                    {
                        filterContext.Result = new RedirectResult("/Term/Player");
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("/Account/LogOff");
                    }
                }
            }

            this.OnActionExecuting(filterContext);
        }

        private void _loadService()
        {
            _userUseTermAppService = (IUserUseTermAppService)DependencyResolver.Current.GetService(typeof(IUserUseTermAppService));
        }

        private bool HasTermAccept(ActionExecutingContext filterContex)
        {
            var identity = HttpContext.Current.User.Identity;

            if (identity.IsAuthenticated)
            {
                int userId = identity.GetUserId<int>();
               
                if (userId > 0)
                {
                    var area = HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"] != null ? HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"].ToString() : null;
                    var claimPlayer = ClaimExtension.GetClaimValue(HttpContext.Current.User, "HasTermAcceptPlayer");
                    var claimProducer = ClaimExtension.GetClaimValue(HttpContext.Current.User, "HasTermAcceptProducer");                    

                    DateTime dtAcceptPlayer = claimPlayer != null ? DateTime.ParseExact(claimPlayer, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : default(DateTime);
                    DateTime dtAcceptProducer = claimProducer != null ? DateTime.ParseExact(claimProducer, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : default(DateTime);

                    if (string.IsNullOrWhiteSpace(area) && dtAcceptPlayer != default(DateTime) && dtAcceptPlayer.AddDays(1) > DateTime.Now)
                    {
                        return true;
                    }
                    else if (area == "ProducerArea" && dtAcceptProducer != default(DateTime) && dtAcceptProducer.AddDays(1) > DateTime.Now)
                    {
                        return true;
                    }

                    _loadService();

                    var result = _userUseTermAppService.GetAllByUserId(userId);
                    IPrincipal principal = HttpContext.Current.User;
                    
                    if (area == "ProducerArea" && result != null && result.Any(e => e.Role == "Producer"))
                    {                        
                        ClaimExtension.AddUpdateClaim(principal, "HasTermAcceptProducer", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        return true;
                    }
                    else if (string.IsNullOrWhiteSpace(area) && result != null && (result.Any(e => e.Role == "Player") || result.Any(e => e.Role == "")))
                    {
                        ClaimExtension.AddUpdateClaim(principal, "HasTermAcceptPlayer", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        return true;
                    }                   
                }
            }

            return false;
        }

        private bool IsAjaxRequest(ActionExecutingContext filterContext)
        {
            var requestedWith = filterContext.HttpContext.Request.ServerVariables["HTTP_X_REQUESTED_WITH"];

            return requestedWith != null && requestedWith == "XMLHttpRequest";
        }
    }
}