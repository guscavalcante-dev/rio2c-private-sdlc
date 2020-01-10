//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.WebApi.Areas.Api.Controllers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Http.Description;
//using WebApi.OutputCache.V2;

//namespace PlataformaRio2C.WebApi.Areas.Api.V1.Controllers
//{
//    /// <summary>
//    /// Classe que fornece os endpoints sobre conferencias
//    /// </summary>
//    [Authorize]
//    [Microsoft.Web.Http.ApiVersion("1.0")]
//    [RoutePrefix("api/v{api-version:apiVersion}/conferences")]
//    public class ConferenceController : BaseApiController
//    {
//        private readonly IConferenceAppService _conferenceAppService;

//        /// <summary>Construtor do controller</summary>
//        public ConferenceController(IConferenceAppService conferenceAppService)
//        {
//            _conferenceAppService = conferenceAppService;
//        }

//        /// <summary>Todas as Palestras</summary> 
//        /// <remarks>Retorna a lista de Palestras</remarks>
//        /// <param name="orderBy">Campo para ordenação</param>
//        /// <param name="orderByDesc">Sentido da ordenação</param>
//        /// <param name="fields">Campos que serão retornados</param>
//        /// <response code="200"></response>
//        /// <response code="400">Bad Request</response>
//        /// <response code="401">Unauthorized</response>
//        /// <response code="403">Forbidden</response>        
//        /// <response code="500">Internal Server Error</response>
//        [Route("")]
//        [HttpGet]
//        [ResponseType(typeof(IEnumerable<Application.ViewModels.Api.ConferenceItemListAppViewModel>))]
//        public async Task<IHttpActionResult> ListAll([FromUri]string orderBy = "Name", [FromUri]bool? orderByDesc = false, [FromUri]string fields = null)
//        {
//            IEnumerable<Application.ViewModels.Api.ConferenceItemListAppViewModel> result = null;

//            result = await Task.FromResult(_conferenceAppService.GetAllByApi());            

//            //ordena
//            try
//            {
//                var pi = typeof(Application.ViewModels.Api.ConferenceItemListAppViewModel).GetProperty(orderBy);

//                if (orderByDesc != null && orderByDesc == true)
//                {
//                    result = await Task.FromResult(result.OrderByDescending(x => pi.GetValue(x, null)).ToList());
//                }
//                else
//                {
//                    result = await Task.FromResult(result.OrderBy(x => pi.GetValue(x, null)).ToList());
//                }
//            }
//            catch (System.Exception)
//            {
//            }

//            if (result != null && result.Any())
//            {
//                return await Json(result, fields);
//            }

//            return NotFound();
//        }

//        /// <summary>
//        /// Retorna detalhes de uma palestra específica
//        /// </summary> 
//        /// <remarks>Retorna detalhes de uma palestra específica</remarks>
//        /// <param name="fields">Campos que serão retornados</param>
//        /// <param name="uid">Guid da palestra</param>
//        /// <response code="200"></response>
//        /// <response code="400">Bad Request</response>
//        /// <response code="401">Unauthorized</response>
//        /// <response code="403">Forbidden</response>        
//        /// <response code="500">Internal Server Error</response>
//        [ResponseType(typeof(Application.ViewModels.Api.ConferenceDetailAppViewModel))]
//        [Route("{uid}")]
//        [HttpGet]
//        public async Task<IHttpActionResult> Get([FromUri]Guid uid, [FromUri]string fields = null)
//        {
//            var result = _conferenceAppService.GetByApi(uid);

//            if (result != null)
//            {
//                return await Json(result, fields);
//            }

//            return NotFound();
//        }

//        /// <summary>
//        /// Retorna a imagem de um participante da palestra específico
//        /// </summary> 
//        /// <remarks>Retorna a imagem de um participante da palestra específico</remarks>
//        /// <param name="fields">Campos que serão retornados</param>
//        /// <param name="uid">Guid do participante da palestra</param>
//        /// <response code="200">Ok</response>
//        /// <response code="400">Bad Request</response>
//        /// <response code="401">Unauthorized</response>
//        /// <response code="403">Forbidden</response>        
//        /// <response code="500">Internal Server Error</response>
//        [Route("{uid}/lecturerfullimage")]
//        [HttpGet]
//        [CacheOutput(ServerTimeSpan = 300, ExcludeQueryStringFromCacheKey = false)]
//        public async Task<IHttpActionResult> GetImage([FromUri]Guid uid, [FromUri]string fields = null)
//        {
//            var result = await Task.FromResult(_conferenceAppService.GetLecturerImage(uid));

//            if (result != null)
//            {
//                return await Json(result, fields);
//            }

//            return NotFound();
//        }

//        /// <summary>
//        /// Retorna a imagem de um participante da palestra específico
//        /// </summary> 
//        /// <remarks>Retorna a miniatura da imagem de um participante da palestra específico</remarks>
//        /// <param name="fields">Campos que serão retornados</param>
//        /// <param name="uid">Guid do participante da palestra</param>
//        /// <response code="200">Ok</response>
//        /// <response code="400">Bad Request</response>
//        /// <response code="401">Unauthorized</response>
//        /// <response code="403">Forbidden</response>        
//        /// <response code="500">Internal Server Error</response>
//        [Route("{uid}/lecturerthumbnailimage")]
//        [HttpGet]
//        [CacheOutput(ServerTimeSpan = 300, ExcludeQueryStringFromCacheKey = false)]
//        public async Task<IHttpActionResult> GetThumbImage([FromUri]Guid uid, [FromUri]string fields = null)
//        {
//            var result = await Task.FromResult(_conferenceAppService.GetLecturerThumbImage(uid));

//            if (result != null)
//            {
//                return await Json(result, fields);
//            }

//            return NotFound();
//        }        
//    }
//}
