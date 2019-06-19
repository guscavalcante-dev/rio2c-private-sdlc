using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    [Authorize(Roles = "Producer,Player")]
    [RoutePrefix("api/interests")]
    public class InterestsController : BaseApiController
    {
        private readonly IInterestAppService _interestAppService;

        public InterestsController(IInterestAppService interestAppService)
        {
            _interestAppService = interestAppService;
        }

        [Route("genres")]
        [HttpGet]
        public async Task<IHttpActionResult> Genres([FromUri]PlayerSelectOptionFilterAppViewModel filter)
        {
            var result = await Task.FromResult(_interestAppService.All().Where(e => e.Group.Name.Contains("Gênero")));

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }       
    }
}
