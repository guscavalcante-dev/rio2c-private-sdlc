//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.Application.ViewModels;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Http;

//namespace PlataformaRio2C.Web.Admin.Areas.WebApi.Controllers
//{
//    [Authorize(Roles = "Administrator")]
//    [RoutePrefix("api/dashboard")]
//    public class DashboardController : BaseApiController
//    {
//        private readonly IDashboardAppService _appService;

//        public DashboardController(IDashboardAppService appService)
//        {
//            _appService = appService;
//        }


//        [Route("totalholding")]
//        [HttpGet]
//        public async Task<IHttpActionResult> GetTotalHolding()
//        {
//            var result = await Task.FromResult(_appService.GetTotalHolding());

//            return await Json(result);
//        }

//        [Route("totalplayer")]
//        [HttpGet]
//        public async Task<IHttpActionResult> GetTotalPlayer()
//        {
//            var result = await Task.FromResult(_appService.GetTotalPlayer());

//            return await Json(result);
//        }

//        [Route("totalproducer")]
//        [HttpGet]
//        public async Task<IHttpActionResult> GetTotalProducer()
//        {
//            var result = await Task.FromResult(_appService.GetTotalProducer());

//            return await Json(result);
//        }

//        [Route("totalproject")]
//        [HttpGet]
//        public async Task<IHttpActionResult> GetTotalProjects()
//        {
//            var result = await Task.FromResult(_appService.GetTotalProjects());

//            return await Json(result);
//        }

//        [Route("projetcchart")]
//        [HttpGet]
//        public async Task<IHttpActionResult> GetProjetcChart()
//        {
//            var result = await Task.FromResult(_appService.GetProjetcChart());

//            return await Json(result);
//        }


//        [Route("projetcssubmissions")]
//        [HttpGet]
//        public async Task<IHttpActionResult> GetProjetcsSubmissions()
//        {
//            var result = await Task.FromResult(_appService.GetProjetcsSubmissions());

//            return await Json(result);
//        }

//        [Route("countryplayer")]
//        [HttpGet]
//        public async Task<IHttpActionResult> GetCountryPlayer()
//        {
//            var result = await Task.FromResult(_appService.GetCountryPlayer());

//            return await Json(result);
//        }

//        [Route("countryproducer")]
//        [HttpGet]
//        public async Task<IHttpActionResult> GetCountryProducer()
//        {
//            var result = await Task.FromResult(_appService.GetCountryProducer());

//            return await Json(result);
//        }
//    }
//}
