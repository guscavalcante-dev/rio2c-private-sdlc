using Microsoft.Web.Http.Routing;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Routing;

namespace PlataformaRio2C.WebApi
{
    /// <summary>
    /// A classe WebApiConfig é responsável registrar as configurações tais como o roteamento.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registra configurações
        /// </summary>
        public static void Register(HttpConfiguration config)
        {
            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                    {
                        ["apiVersion"] = typeof( ApiVersionRouteConstraint )
                    }
            };
            config.AddApiVersioning(o => o.ReportApiVersions = true);

            config.MapHttpAttributeRoutes(constraintResolver);

            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
