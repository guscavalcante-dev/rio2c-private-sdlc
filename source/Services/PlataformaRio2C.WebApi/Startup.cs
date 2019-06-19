using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PlataformaRio2C.WebApi.Models;
using PlataformaRio2C.WebApi.Providers;
using System;
using System.Web.Cors;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

//[assembly: OwinStartup(typeof(PlataformaRio2C.WebApi.Startup))]

namespace PlataformaRio2C.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            SimpleInjectorInitializer.Initialize();

            HttpConfiguration config = new HttpConfiguration();
            config.MessageHandlers.Add(new LanguageMessageHandler());            

            ConfigureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);          
            app.UseWebApi(config);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            SwaggerConfig.Register();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        /// <summary>
        /// Configura o OAuth.
        /// </summary>
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/security/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new AuthorizationApiServerProvider()
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
