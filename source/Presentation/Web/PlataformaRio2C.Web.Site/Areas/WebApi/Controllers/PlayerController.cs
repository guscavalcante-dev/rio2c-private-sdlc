using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    [Authorize(Roles = "Producer,Player")]
    [RoutePrefix("api/players")]
    public class PlayerController : BaseApiController
    {
        private readonly IPlayerAppService _playerAppService;

        public PlayerController(IPlayerAppService playerAppService)
        {
            _playerAppService = playerAppService;
        }

        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> Index([FromUri]PlayerSelectOptionFilterAppViewModel filter)
        {
            var result = await Task.FromResult(_playerAppService.All());

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

        [Route("GetAllWithGenres")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllWithGenres([FromUri]PlayerSelectOptionFilterAppViewModel filter)
        {
            int userId = User.Identity.GetUserId<int>();
            var result = await Task.FromResult(_playerAppService.GetAllWithGenres(filter, userId));

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

        [Route("GetAllWithGenresGroupbyHolding")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllWithGenresGroupbyHolding([FromUri]PlayerSelectOptionFilterAppViewModel filter)
        {
            int userId = User.Identity.GetUserId<int>();
            var result = await Task.FromResult(_playerAppService.GetAllWithGenresGroupByHolding(filter, userId));

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

        [Route("GetAllOptionsGroupByHolding")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllOptionsGroupByHolding([FromUri]PlayerSelectOptionFilterAppViewModel filter)
        {
            int userId = User.Identity.GetUserId<int>();
            var result = await Task.FromResult(_playerAppService.GetAllOptionGroupByHolding(filter, userId));

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

        [Route("GetAllOptions")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllOptions([FromUri]PlayerSelectOptionFilterAppViewModel filter)
        {
            int userId = User.Identity.GetUserId<int>();
            var result = await Task.FromResult(_playerAppService.GetAllOption(filter, userId));

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }


        [Route("GetAllOptionByUser")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllOptionByUser([FromUri]PlayerSelectOptionFilterAppViewModel filter)
        {
            int userId = User.Identity.GetUserId<int>();
            var result = await Task.FromResult(_playerAppService.GetAllOptionByUser(filter, userId));

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }


        [Route("thumbImage")]
        [HttpGet]
        public async Task<IHttpActionResult> GetThumbImage([FromUri]Guid uid)
        {
            var result = await Task.FromResult(_playerAppService.GetThumbImage(uid));

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
            var result = await Task.FromResult(_playerAppService.GetThumbImage(uid));

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

       
    }
}
