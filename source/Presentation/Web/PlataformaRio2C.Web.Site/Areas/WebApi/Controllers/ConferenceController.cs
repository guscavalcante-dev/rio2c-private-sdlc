//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.Application.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Http;

//namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
//{
//    [Authorize(Roles = "Player,Producer")]
//    [RoutePrefix("api/conference")]
//    public class ConferenceController : BaseApiController
//    {
//        private readonly IConferenceAppService _appService;

//        public ConferenceController(IConferenceAppService appService)
//        {
//            _appService = appService;
//        }


//        [Route("")]
//        [HttpGet]
//        public async Task<IHttpActionResult> Index()
//        {
//            return await Index(null, false);
//        }

//        [Route("")]
//        [HttpGet]
//        public async Task<IHttpActionResult> Index([FromUri]string orderBy)
//        {
//            return await Index(orderBy, false);
//        }

//        [Route("")]
//        [HttpGet]
//        public async Task<IHttpActionResult> Index([FromUri]string orderBy, [FromUri]bool orderByDesc)
//        {

//            IEnumerable<Application.ViewModels.Site.ConferenceItemListAppViewModel> result = null;

//            try
//            {
//                var pi = typeof(Application.ViewModels.Site.ConferenceItemListAppViewModel).GetProperty(orderBy);

//                if (orderByDesc)
//                {
//                    result = await Task.FromResult(_appService.GetAllByPortal().OrderByDescending(x => pi.GetValue(x, null)).ToList());
//                }
//                else
//                {
//                    result = await Task.FromResult(_appService.GetAllByPortal().OrderBy(x => pi.GetValue(x, null)).ToList());
//                }

                
//            }
//            catch (System.Exception)
//            {
//                result = await Task.FromResult(_appService.GetAllByPortal().OrderBy(e => e.Date).ThenBy(e => e.StartTime));
//            }

//            if (result != null && result.Any())
//            {
//                return await Json(result);
//            }
//            else
//            {
//                return NotFound();
//            }
//        }


//        [Route("lecturerThumbImage")]
//        [HttpGet]
//        public async Task<IHttpActionResult> GetThumbImage([FromUri]Guid uid)
//        {
//            var result = await Task.FromResult(_appService.GetLecturerThumbImage(uid));

//            if (result != null)
//            {
//                return await Json(result);
//            }

//            return NotFound();
//        }

//        [Route("lecturerImage")]
//        [HttpGet]
//        public async Task<IHttpActionResult> GetImage([FromUri]Guid uid)
//        {
//            var result = await Task.FromResult(_appService.GetLecturerImage(uid));

//            if (result != null)
//            {
//                return await Json(result);
//            }

//            return NotFound();
//        }

//    }
//}
