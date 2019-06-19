using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application.Interfaces.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    [Authorize(Roles = "Producer,Player")]
    [RoutePrefix("api/schedule")]
    public class ScheduleController : BaseApiController
    {
        private readonly IScheduleAppService _scheduleAppService;

        public ScheduleController(IScheduleAppService scheduleAppService)
        {
            _scheduleAppService = scheduleAppService;
        }

        [Route("days")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDays()
        {            
            var result = await Task.FromResult(_scheduleAppService.GetDays());
            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }


        [Route("player")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSchedulePlayer()
        {
            int userId = User.Identity.GetUserId<int>();
            var result = await Task.FromResult(_scheduleAppService.GetSchedulePlayer(userId));
            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

        [Route("producer")]
        [HttpGet]
        public async Task<IHttpActionResult> GetScheduleProducer()
        {
            int userId = User.Identity.GetUserId<int>();
            var result = await Task.FromResult(_scheduleAppService.GetScheduleProducer(userId));
            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }
    }
}
