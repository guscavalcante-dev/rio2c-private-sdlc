using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using PlataformaRio2C.Web.Admin.App_Start;
using PlataformaRio2C.Web.Admin.Areas.WebApi;
using PlataformaRio2C.Web.Admin.Models;
using System.Globalization;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartupAttribute(typeof(PlataformaRio2C.Web.Admin.Startup))]
namespace PlataformaRio2C.Web.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

            SimpleInjectorInitializer.Initialize();

            HttpConfiguration config = new HttpConfiguration();

            ConfigureAuth(app);
            config.MessageHandlers.Add(new LanguageMessageHandler());

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.UseDataContractJsonSerializer = true;
            json.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            app.UseWebApi(config);

            
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            //BundleTable.EnableOptimizations = true;
        }
    }
}
