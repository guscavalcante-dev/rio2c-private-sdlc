//using PlataformaRio2C.Application.Interfaces.Services;
//using System;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http.Filters;
//using System.Web.Mvc;
//using System.Web.Routing;

//namespace PlataformaRio2C.Web.Site
//{
//    public static class CustomException
//    {
//        private static IErrorMessageService _errorMessageService;

//        private static void _loadService()
//        {
//            _errorMessageService = (IErrorMessageService)DependencyResolver.Current.GetService(typeof(IErrorMessageService));
//        }

//        public static void SendEmailException(Exception ex)
//        {            
//#if (!DEBUG)
//                _loadService();

//                var identity = HttpContext.Current.User.Identity;
//                var serverName = HttpContext.Current.Server.MachineName;
//                var userName = identity.Name;
//                var url = HttpContext.Current.Request.Url.ToString();
//                var info = string.Format("<p>Servidor: {0}</p> <p>Usuário: {1}</p> <p>URL: {2}</p>", serverName, userName, url);

//                _errorMessageService.SendEmailException(ex, info);
//#endif
//        }
//    }


//    public class CustomExceptionApiFilterAttribute : ExceptionFilterAttribute
//    {
//        public override void OnException(HttpActionExecutedContext actionExecutedContext)
//        {
//            if (!(actionExecutedContext.Exception is TaskCanceledException))
//            {
//                CustomException.SendEmailException(actionExecutedContext.Exception);
//            }

//            base.OnException(actionExecutedContext);
//        }
//    }

//    //public class CustomExceptionMvcFilterAttribute : HandleErrorAttribute
//    //{
//    //    public override void OnException(System.Web.Mvc.ExceptionContext filterContext)
//    //    {
//    //        CustomException.SendEmailException(filterContext.Exception);
//    //        //if (filterContext.Exception is HttpAntiForgeryException)
//    //        //{
//    //        //    filterContext.HttpContext.Response.Clear();
//    //        //    filterContext.HttpContext.Response.TrySkipIisCustomErrors = false;
//    //        //    filterContext.ExceptionHandled = false;
//    //        //    filterContext.HttpContext.Response.Redirect("/Account/ErrorUnexpected");
//    //        //}
//    //        //else
//    //        //{
//    //        //    CustomException.SendEmailException(filterContext.Exception);
//    //        //}

//    //        base.OnException(filterContext);
//    //    }
//    //}
//}