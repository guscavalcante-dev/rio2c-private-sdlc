using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlataformaRio2C.Web.Admin.Areas.WebApi.Controllers
{
    [Authorize(Roles = "Administrator")]
    [RoutePrefix("api/project")]
    public class ProjetcController : BaseApiController
    {
        private readonly IProjectAppService _appService;

        public ProjetcController(IProjectAppService appService)
        {
            _appService = appService;
        }

        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> Index()
        {
            return await Index("CreationDate", true);
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
            IEnumerable<ProjectAdminItemListAppViewModel> result = null;

            try
            {
                var pi = typeof(ProjectAdminItemListAppViewModel).GetProperty(orderBy);

                if (orderByDesc)
                {
                    result = await Task.FromResult(_appService.AllByAdmin().OrderByDescending(x => pi.GetValue(x, null)).ToList());
                    
                }
                else
                {
                    result = await Task.FromResult(_appService.AllByAdmin().OrderBy(x => pi.GetValue(x, null)).ToList());
                }                
            }
            catch (System.Exception e)
            {

                result = await Task.FromResult(_appService.AllByAdmin());
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

        [Route("GetAllOptions")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllOptions([FromUri]ProjectOptionFilterAppViewModel filter)
        {            
            var result = await Task.FromResult(_appService.GetAllOption(filter));

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

        [Route("ProjectPitching")]
        [HttpGet]
        public async Task<IHttpActionResult> ProjectPitching()
        {
            return await ProjectPitching("CreationDate", true);
        }


        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> ProjectPitching([FromUri]string orderBy, [FromUri]bool orderByDesc)
        {
            IEnumerable<ProjectAdminItemListAppViewModel> result = null;

            try
            {
                var pi = typeof(ProjectAdminItemListAppViewModel).GetProperty(orderBy);

                if (orderByDesc)
                {
                    result = await Task.FromResult(_appService.AllByAdmin().OrderByDescending(x => pi.GetValue(x, null)).Where(x=>x.Pitching == true).ToList());

                }
                else
                {
                    result = await Task.FromResult(_appService.AllByAdmin().OrderBy(x => pi.GetValue(x, null)).Where(x => x.Pitching == true).ToList());
                }
            }
            catch (System.Exception e)
            {

                result = await Task.FromResult(_appService.AllByAdmin());
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
