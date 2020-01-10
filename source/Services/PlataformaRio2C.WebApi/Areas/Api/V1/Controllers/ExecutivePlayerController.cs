//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.Application.ViewModels;
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
//    /// Classe que fornece os endpoints sobre executivos players
//    /// </summary>
//    [Authorize]
//    [Microsoft.Web.Http.ApiVersion("1.0")]
//    [RoutePrefix("api/v{api-version:apiVersion}/executivesplayers")]
//    public class ExecutivePlayerController : BaseApiController
//    {
//        private readonly IPlayerAppService _playerAppService;
//        private readonly ICollaboratorAppService _collaboratorAppService;
//        private readonly ICollaboratorPlayerAppService _collaboratorPlayerAppService;

//        /// <summary>
//        /// Construtor do controller
//        /// </summary>
//        public ExecutivePlayerController(IPlayerAppService playerAppService, ICollaboratorPlayerAppService collaboratorPlayerAppService, ICollaboratorAppService collaboratorAppService)
//        {
//            _playerAppService = playerAppService;
//            _collaboratorPlayerAppService = collaboratorPlayerAppService;
//            _collaboratorAppService = collaboratorAppService;
//        }       

//        /// <summary> Todos os Executivos Players</summary> 
//        /// <remarks>Retorna a lista de executivos players sem imagem</remarks>        
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
//        [ResponseType(typeof(IEnumerable<CollaboratorOptionMessageAppViewModel>))]
//        public async Task<IHttpActionResult> ListAll([FromUri]string orderBy = "Name", [FromUri]bool? orderByDesc = false, [FromUri]string fields = null)
//        {
//            IEnumerable<CollaboratorOptionMessageAppViewModel> result = null;

//            result = await Task.FromResult(_collaboratorAppService.GetOptionsChat(0));
//            result = result.Where(e => e.IsPlayer);

//            //ordena
//            try
//            {
//                var pi = typeof(CollaboratorOptionMessageAppViewModel).GetProperty(orderBy);

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
//        /// Retorna detalhes de um executivo player específico
//        /// </summary> 
//        /// <remarks>Retorna detalhes de um player específico</remarks>
//        /// <param name="fields">Campos que serão retornados</param>
//        /// <param name="uid">Guid do executivo player</param>
//        /// <response code="200"></response>
//        /// <response code="400">Bad Request</response>
//        /// <response code="401">Unauthorized</response>
//        /// <response code="403">Forbidden</response>        
//        /// <response code="500">Internal Server Error</response>
//        [Route("{uid}")]
//        [HttpGet]
//        [ResponseType(typeof(IEnumerable<CollaboratorBasicAppViewModel>))]
//        public async Task<IHttpActionResult> Get([FromUri]Guid uid, [FromUri]string fields = null)
//        {
//            var result = await Task.FromResult(_collaboratorPlayerAppService.Get(uid));            

//            if (result != null)
//            {
//                return await Json(result, fields, "UidByEdit,JobTitles,MiniBios");
//            }

//            return NotFound();
//        }

//        /// <summary>
//        /// Retorna a imagem de um executivo player específico
//        /// </summary> 
//        /// <remarks>Retorna a imagem de um executivo player específico</remarks>
//        /// <param name="fields">Campos que serão retornados</param>
//        /// <param name="uid">Guid do executivo player</param>
//        /// <response code="200">Ok</response>
//        /// <response code="400">Bad Request</response>
//        /// <response code="401">Unauthorized</response>
//        /// <response code="403">Forbidden</response>        
//        /// <response code="500">Internal Server Error</response>
//        [Route("{uid}/fullimage")]
//        [HttpGet]
//        [CacheOutput(ServerTimeSpan = 300, ExcludeQueryStringFromCacheKey = false)]
//        [ResponseType(typeof(ImageFileAppViewModel))]
//        public async Task<IHttpActionResult> GetImage([FromUri]Guid uid, [FromUri]string fields = null)
//        {
//            var result = await Task.FromResult(_collaboratorPlayerAppService.GetImage(uid));

//            if (result != null)
//            {
//                return await Json(result, fields);
//            }

//            return NotFound();
//        }


//        /// <summary>
//        /// Retorna a miniatura da imagem de um executivo player específico
//        /// </summary> 
//        /// <remarks>Retorna a miniatura imagem de um executivo player específico</remarks>
//        /// <param name="fields">Campos que serão retornados</param>
//        /// <param name="uid">Guid do executivo player</param>
//        /// <response code="200"></response>
//        /// <response code="400">Bad Request</response>
//        /// <response code="401">Unauthorized</response>
//        /// <response code="403">Forbidden</response>        
//        /// <response code="500">Internal Server Error</response>
//        [Route("{uid}/thumbImage")]
//        [HttpGet]
//        [CacheOutput(ServerTimeSpan = 300, ExcludeQueryStringFromCacheKey = false)]
//        [ResponseType(typeof(ImageFileAppViewModel))]
//        public async Task<IHttpActionResult> GetThumbImage([FromUri]Guid uid, [FromUri]string fields = null)
//        {
//            var result = await Task.FromResult(_collaboratorPlayerAppService.GetThumbImage(uid));

//            if (result != null)
//            {
//                return await Json(result, fields);
//            }

//            return NotFound();
//        }

        
//    }
//}