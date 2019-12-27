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
//    /// Classe que fornece os endpoints sobre player
//    /// </summary>
//    /// <remarks>Obtém uma nova solicitação e garanta sua automação.</remarks>
//    [Authorize]
//    [Microsoft.Web.Http.ApiVersion("1.0")]    
//    [RoutePrefix("api/v{api-version:apiVersion}/players")]
//    public class PlayerController : BaseApiController
//    {
//        private readonly IPlayerAppService _playerAppService;

//        /// <summary>Construtor do controller</summary>
//        public PlayerController(IPlayerAppService playerAppService)
//        {
//            _playerAppService = playerAppService;
//        }

//        /// <summary>Todos os Players</summary> 
//        /// <remarks>Retorna a lista de players sem imagem</remarks>
//        /// <param name="filter">Filtro</param>
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
//        [ResponseType(typeof(IEnumerable<PlayerProducerAreaAppViewModel>))]
//        public async Task<IHttpActionResult> ListAll([FromUri]PlayerSelectOptionFilterAppViewModel filter, [FromUri]string orderBy = "Name", [FromUri]bool? orderByDesc = false, [FromUri]string fields = null)
//        {
//            IEnumerable<PlayerProducerAreaAppViewModel> result = null;
            
//            result = _playerAppService.GetAllWithGenres(filter, 0);

//            try
//            {
//                var pi = typeof(PlayerProducerAreaAppViewModel).GetProperty(orderBy);

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
//            else
//            {
//                return NotFound();
//            }
//        }

//        /// <summary>
//        /// Retorna detalhes de um player específico
//        /// </summary> 
//        /// <remarks>Retorna detalhes de um player específico</remarks>
//        /// <param name="fields">Campos que serão retornados</param>
//        /// <param name="uid">Guid do player</param>
//        /// <response code="200"></response>
//        /// <response code="400">Bad Request</response>
//        /// <response code="401">Unauthorized</response>
//        /// <response code="403">Forbidden</response>        
//        /// <response code="500">Internal Server Error</response>
//        [ResponseType(typeof(PlayerDetailWithInterestAppViewModel))]
//        [Route("{uid}")]
//        [HttpGet]
//        public async Task<IHttpActionResult> Get([FromUri]Guid uid, [FromUri]string fields = null)
//        {
//            var result = _playerAppService.GetByDetailsWithInterests(uid);

//            if (result != null)
//            {
//                return await Json(result, fields, "LanguagesOptions,Descriptions,RestrictionsSpecifics,Image,NewImage,ImageUpload,HoldingUid,Collaborators");
//            }

//            return NotFound();
//        }

//        /// <summary>
//        /// Todos os players com gênero de interesse agrupados por holding
//        /// </summary> 
//        /// <remarks>Retorna a lista de todos players com gênero de interesse agrupados por holding</remarks>
//        /// <param name="fields">Campos que serão retornados</param>
//        /// <param name="filter">Filtro</param>
//        /// <response code="200"></response>
//        /// <response code="400">Bad Request</response>
//        /// <response code="401">Unauthorized</response>
//        /// <response code="403">Forbidden</response>        
//        /// <response code="500">Internal Server Error</response>
//        [ResponseType(typeof(IEnumerable<GroupPlayerAppViewModel>))]
//        [Route("allgroupbyholding")]
//        [HttpGet]
//        public async Task<IHttpActionResult> ListAllGroupByHolding([FromUri]PlayerSelectOptionFilterAppViewModel filter, [FromUri]string fields = null)
//        {
//            var result = await Task.FromResult(_playerAppService.GetAllWithGenresGroupByHolding(filter, 0));

//            if (result != null)
//            {
//                return await Json(result, fields);
//            }

//            return NotFound();
//        }
        
//        /// <summary>
//        /// Retorna a imagem de um player específico
//        /// </summary> 
//        /// <remarks>Retorna a imagem de um player específico</remarks>
//        /// <param name="fields">Campos que serão retornados</param>
//        /// <param name="uid">Guid do player</param>
//        /// <response code="200">Ok</response>
//        /// <response code="400">Bad Request</response>
//        /// <response code="401">Unauthorized</response>
//        /// <response code="403">Forbidden</response>        
//        /// <response code="500">Internal Server Error</response>
//        [Route("{uid}/fullimage")]
//        [HttpGet]
//        [ResponseType(typeof(ImageFileAppViewModel))]
//        [CacheOutput(ServerTimeSpan = 300, ExcludeQueryStringFromCacheKey = false)]
//        public async Task<IHttpActionResult> GetImage([FromUri]Guid uid, [FromUri]string fields = null)
//        {
//            var result = await Task.FromResult(_playerAppService.GetImage(uid));

//            if (result != null)
//            {
//                return await Json(result, fields);
//            }

//            return NotFound();
//        }

//        /// <summary>
//        /// Retorna a miniatura da imagem de um player específico
//        /// </summary> 
//        /// <remarks>Retorna a miniatura imagem de um player específico</remarks>
//        /// <param name="fields">Campos que serão retornados</param>
//        /// <param name="uid">Guid do player</param>
//        /// <response code="200"></response>
//        /// <response code="400">Bad Request</response>
//        /// <response code="401">Unauthorized</response>
//        /// <response code="403">Forbidden</response>        
//        /// <response code="500">Internal Server Error</response>
//        [ResponseType(typeof(ImageFileAppViewModel))]
//        [Route("{uid}/thumbnailimage")]
//        [HttpGet]
//        [CacheOutput(ServerTimeSpan = 300, ExcludeQueryStringFromCacheKey = false)]
//        public async Task<IHttpActionResult> GetThumbImage([FromUri]Guid uid, [FromUri]string fields = null)
//        {
//            var result = await Task.FromResult(_playerAppService.GetThumbImage(uid));

//            if (result != null)
//            {
//                return await Json(result, fields);
//            }

//            return NotFound();
//        }
//    }
//}