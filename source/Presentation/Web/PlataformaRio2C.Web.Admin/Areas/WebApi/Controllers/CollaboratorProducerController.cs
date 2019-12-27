//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.Application.ViewModels;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Http;

//namespace PlataformaRio2C.Web.Admin.Areas.WebApi.Controllers
//{
//    [Authorize(Roles = "Administrator")]
//    [RoutePrefix("api/collaboratorsproducers")]
//    public class CollaboratorProducerController : BaseApiController
//    {
//        private readonly ICollaboratorProducerAppService _appService;

//        public CollaboratorProducerController(ICollaboratorProducerAppService appService)
//        {
//            _appService = appService;
//        }


//        [Route("")]
//        [HttpGet]
//        public async Task<IHttpActionResult> Index()
//        {
//            return await Index("Name", false);
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

//            IEnumerable<CollaboratorProducerItemListAppViewModel> result = null;

//            try
//            {
//                var pi = typeof(CollaboratorProducerItemListAppViewModel).GetProperty(orderBy);

//                if (orderByDesc)
//                {
//                    result = await Task.FromResult(_appService.All().OrderByDescending(x => pi.GetValue(x, null)).ToList());
//                }
//                else
//                {
//                    result = await Task.FromResult(_appService.All().OrderBy(x => pi.GetValue(x, null)).ToList());
//                }                
//            }
//            catch (System.Exception)
//            {

//                result = await Task.FromResult(_appService.All());
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
//    }
//}
