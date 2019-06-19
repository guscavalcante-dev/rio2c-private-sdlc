using PlataformaRio2C.Application.Interfaces.Services;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Areas.ProducerArea.Controllers
{
    [Authorize(Roles = "Producer")]
    public class ScheduleController : PlataformaRio2C.Web.Site.Controllers.ScheduleController
    {
        public ScheduleController(IScheduleAppService scheduleAppService)
            :base(scheduleAppService)
        {            
        }
    }
}