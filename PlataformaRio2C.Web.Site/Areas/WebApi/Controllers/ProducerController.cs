using PlataformaRio2C.Application.Interfaces.Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    [Authorize(Roles = "Player,Producer")]
    [RoutePrefix("api/producers")]
    public class ProducerController : BaseApiController
    {
        private readonly IProducerAppService _producerAppService;

        public ProducerController(IProducerAppService producerAppService)
        {
            _producerAppService = producerAppService;
        }       


        [Route("thumbImage")]
        [HttpGet]
        public async Task<IHttpActionResult> GetThumbImage([FromUri]Guid uid)
        {
            var result = await Task.FromResult(_producerAppService.GetThumbImage(uid));

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

        [Route("image")]
        [HttpGet]
        public async Task<IHttpActionResult> GetImage([FromUri]Guid uid)
        {
            var result = await Task.FromResult(_producerAppService.GetImage(uid));

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }
    }
}
