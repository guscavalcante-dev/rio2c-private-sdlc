using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Areas.ProducerArea
{
    public class ProducerAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ProducerArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ProducerArea_default",
                "ProducerArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "PlataformaRio2C.Web.Site.Areas.ProducerArea.Controllers" }                
            );
        }
    }
}