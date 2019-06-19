using System.Web.Mvc;
using PlataformaRio2C.Application.Interfaces.Services;

namespace PlataformaRio2C.Web.Site.Areas.ProducerArea.Controllers
{
    [Authorize(Order = 1, Roles = "Producer")]
    public class ConferenceController : PlataformaRio2C.Web.Site.Controllers.ConferenceController
    {
        public ConferenceController(IConferenceAppService appService) : base(appService)
        {
        }
    }
}