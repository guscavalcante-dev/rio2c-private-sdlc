using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.WebApi.Areas.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;

namespace PlataformaRio2C.WebApi.Areas.Api.V1.Controllers
{
    /// <summary>
    /// Classe que fornece os endpoints sobre produtoras
    /// </summary>
    [Authorize]
    [Microsoft.Web.Http.ApiVersion("1.0")]
    [RoutePrefix("api/v{api-version:apiVersion}/producers")]
    public class ProducerController : BaseApiController
    {
        private readonly IProducerAppService _producerAppService;

        /// <summary>Construtor do controller</summary>
        public ProducerController(IProducerAppService producerAppService)
        {
            _producerAppService = producerAppService;
        }

        /// <summary>Todos as Produtoras</summary> 
        /// <remarks>Retorna a lista de produtoras sem imagem</remarks>
        /// <param name="filter">Filtro</param>
        /// <param name="orderBy">Campo para ordenação</param>
        /// <param name="orderByDesc">Sentido da ordenação</param>
        /// <param name="fields">Campos que serão retornados</param>
        /// <response code="200"></response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>        
        /// <response code="500">Internal Server Error</response>
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ProducerItemListAppViewModel>))]
        public async Task<IHttpActionResult> ListAll([FromUri]ProducerItemListAppViewModel filter, [FromUri]string orderBy = "Name", [FromUri]bool? orderByDesc = false, [FromUri]string fields = null)
        {
            IEnumerable<ProducerItemListAppViewModel> result = null;
            
            result = _producerAppService.GetAllSimple(filter);

            try
            {
                var pi = typeof(ProducerItemListAppViewModel).GetProperty(orderBy);

                if (orderByDesc != null && orderByDesc == true)
                {
                    result = await Task.FromResult(result.OrderByDescending(x => pi.GetValue(x, null)).ToList());
                }
                else
                {
                    result = await Task.FromResult(result.OrderBy(x => pi.GetValue(x, null)).ToList());
                }
            }
            catch (System.Exception)
            {
            }

            if (result != null && result.Any())
            {
                return await Json(result, fields);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Retorna detalhes de uma Produtora específica
        /// </summary> 
        /// <remarks>Retorna detalhes de uma Produtora específica</remarks>
        /// <param name="fields">Campos que serão retornados</param>
        /// <param name="uid">Guid da Produtora</param>
        /// <response code="200"></response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>        
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(ProducerDetailAppViewModel))]
        [Route("{uid}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri]Guid uid, [FromUri]string fields = null)
        {
            var result = _producerAppService.GetByDetails(uid);

            if (result != null)
            {
                return await Json(result, fields, "Collaborators,Descriptions,Image,NewImage,ImageUpload");
            }

            return NotFound();
        }

        /// <summary>
        /// Retorna a imagem de uma Produtora específico
        /// </summary> 
        /// <remarks>Retorna a imagem de uma Produtora específico</remarks>
        /// <param name="fields">Campos que serão retornados</param>
        /// <param name="uid">Guid da Produtora</param>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>        
        /// <response code="500">Internal Server Error</response>
        [Route("{uid}/fullimage")]
        [HttpGet]
        [ResponseType(typeof(ImageFileAppViewModel))]
        [CacheOutput(ServerTimeSpan = 300, ExcludeQueryStringFromCacheKey = false)]
        public async Task<IHttpActionResult> GetImage([FromUri]Guid uid, [FromUri]string fields = null)
        {
            var result = await Task.FromResult(_producerAppService.GetImage(uid));

            if (result != null)
            {
                return await Json(result, fields);
            }

            return NotFound();
        }

        /// <summary>
        /// Retorna a miniatura da imagem de uma Produtora específico
        /// </summary> 
        /// <remarks>Retorna a miniatura imagem de uma Produtora específico</remarks>
        /// <param name="fields">Campos que serão retornados</param>
        /// <param name="uid">Guid da Produtora</param>
        /// <response code="200"></response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>        
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(ImageFileAppViewModel))]
        [Route("{uid}/thumbnailimage")]
        [HttpGet]
        [CacheOutput(ServerTimeSpan = 300, ExcludeQueryStringFromCacheKey = false)]
        public async Task<IHttpActionResult> GetThumbImage([FromUri]Guid uid, [FromUri]string fields = null)
        {
            var result = await Task.FromResult(_producerAppService.GetThumbImage(uid));

            if (result != null)
            {
                return await Json(result, fields);
            }

            return NotFound();
        }
    }
}
