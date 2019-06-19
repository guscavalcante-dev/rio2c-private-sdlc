using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlataformaRio2C.Web.Admin.Areas.WebApi.Controllers
{
    [Authorize(Roles = "Administrator")]
    [RoutePrefix("api/conference")]
    public class ConferenceController : BaseApiController
    {
        private readonly IConferenceAppService _appService;

        public ConferenceController(IConferenceAppService appService)
        {
            _appService = appService;
        }


        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> Index()
        {
            return await Index(null, false);
        }

        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> Index([FromUri]string orderBy)
        {
            return await Index(orderBy, false);
        }

        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> Index([FromUri]string orderBy, [FromUri]bool orderByDesc)
        {

            IEnumerable<ConferenceItemListAppViewModel> result = null;

            try
            {
                var pi = typeof(ConferenceItemListAppViewModel).GetProperty(orderBy);

                if (orderByDesc)
                {
                    result = await Task.FromResult(_appService.All().OrderByDescending(x => pi.GetValue(x, null)).ToList());
                }
                else
                {
                    result = await Task.FromResult(_appService.All().OrderBy(x => pi.GetValue(x, null)).ToList());
                }

                
            }
            catch (System.Exception)
            {
                result = await Task.FromResult(_appService.All());
            }

            if (result != null && result.Any())
            {
                return await Json(result);
            }
            else
            {
                return NotFound();
            }
        }
        
    }
}
