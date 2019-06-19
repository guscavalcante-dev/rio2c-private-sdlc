using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    [Authorize(Roles = "Player,Producer")]
    [RoutePrefix("api/collaborators")]
    public class CollaboratorController : BaseApiController
    {
        private readonly ICollaboratorAppService _collaboratorAppService;

        public CollaboratorController(ICollaboratorAppService collaboratorAppService)
        {
            _collaboratorAppService = collaboratorAppService;
        }


        [Route("{uid}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri]Guid uid)
        {
            var result = await Task.FromResult(_collaboratorAppService.Get(uid));

            if (result != null)
            {
                return await Json(result);
            }

            return await NotFound(Messages.PlayerNotFound);
        }


        [Route("thumbImage")]
        [HttpGet]
        [CacheOutput(ServerTimeSpan = 300, ExcludeQueryStringFromCacheKey = false)]
        public async Task<IHttpActionResult> GetThumbImage([FromUri]Guid uid)
        {
            var result = await Task.FromResult(_collaboratorAppService.GetThumbImage(uid));

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

        [Route("image")]
        [HttpGet]
        [CacheOutput(ServerTimeSpan = 60, ExcludeQueryStringFromCacheKey = false)]
        public async Task<IHttpActionResult> GetImage([FromUri]Guid uid)
        {
            var result = await Task.FromResult(_collaboratorAppService.GetImage(uid));

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }


        [Route("optionschat")]
        [HttpGet]
      
        public async Task<IHttpActionResult> GetOptionsChat()
        {
            int userId = User.Identity.GetUserId<int>();
            var result = await Task.FromResult(_collaboratorAppService.GetOptionsChat(userId));

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }
    }
}
