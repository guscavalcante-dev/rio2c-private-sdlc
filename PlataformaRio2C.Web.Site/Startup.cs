

using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using PlataformaRio2C.Web.Site.Areas.WebApi;
using PlataformaRio2C.Web.Site.Models;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartupAttribute(typeof(PlataformaRio2C.Web.Site.Startup))]
namespace PlataformaRio2C.Web.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableDetailedErrors = true;
            GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = null;
            GlobalHost.Configuration.DefaultMessageBufferSize = 3000;
            GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(12000);
            GlobalHost.Configuration.KeepAlive = TimeSpan.FromSeconds(10);
            GlobalHost.DependencyResolver = hubConfiguration.Resolver;

            SimpleInjectorInitializer.Initialize();

            GlobalHost.HubPipeline.AddModule(new CallerCulturePipelineModule());

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

            app.MapSignalR("/signalr", hubConfiguration);
        }
    }
}
