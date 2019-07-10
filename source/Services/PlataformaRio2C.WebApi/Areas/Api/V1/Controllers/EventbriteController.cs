// ***********************************************************************
// Assembly         : PlataformaRio2C.WebApi
// Author           : Rafael Dantas Ruiz
// Created          : 07-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-10-2019
// ***********************************************************************
// <copyright file="EventbriteController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.WebApi.Areas.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using WebApi.OutputCache.V2;

namespace PlataformaRio2C.WebApi.Areas.Api.V1.Controllers
{
    /// <summary>
    /// Class for Eventbrite endpoints
    /// </summary>
    //[Authorize]
    [Microsoft.Web.Http.ApiVersion("1.0")]
    [RoutePrefix("api/v{api-version:apiVersion}/eventbrite")]
    public class EventbriteController : BaseApiController
    {
        /// <summary>Initializes a new instance of the <see cref="EventbriteController"/> class.</summary>
        public EventbriteController()
        {
        }

        /// <summary>Pings this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ping")]
        public async Task<IHttpActionResult> Ping()
        {
            return await Json(new { status = "success", message = "Pong" });
        }

        /// <summary>Tests the specified body.</summary>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Test")]
        public async Task<IHttpActionResult> Test(dynamic body)
        {
            return await Json(new { status = "success", message = "Test with success." });
        }

        //[Route("")]
        //[HttpGet]
        //[ResponseType(typeof(IEnumerable<Application.ViewModels.Api.ConferenceItemListAppViewModel>))]
        //public async Task<IHttpActionResult> ListAll([FromUri]string orderBy = "Name", [FromUri]bool? orderByDesc = false, [FromUri]string fields = null)
        //{
        //    IEnumerable<Application.ViewModels.Api.ConferenceItemListAppViewModel> result = null;

        //    result = await Task.FromResult(_conferenceAppService.GetAllByApi());            

        //    //ordena
        //    try
        //    {
        //        var pi = typeof(Application.ViewModels.Api.ConferenceItemListAppViewModel).GetProperty(orderBy);

        //        if (orderByDesc != null && orderByDesc == true)
        //        {
        //            result = await Task.FromResult(result.OrderByDescending(x => pi.GetValue(x, null)).ToList());
        //        }
        //        else
        //        {
        //            result = await Task.FromResult(result.OrderBy(x => pi.GetValue(x, null)).ToList());
        //        }
        //    }
        //    catch (System.Exception)
        //    {
        //    }

        //    if (result != null && result.Any())
        //    {
        //        return await Json(result, fields);
        //    }

        //    return NotFound();
        //}

        ///// <summary>
        ///// Retorna a imagem de um participante da palestra específico
        ///// </summary> 
        ///// <remarks>Retorna a imagem de um participante da palestra específico</remarks>
        ///// <param name="fields">Campos que serão retornados</param>
        ///// <param name="uid">Guid do participante da palestra</param>
        ///// <response code="200">Ok</response>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="403">Forbidden</response>        
        ///// <response code="500">Internal Server Error</response>
        //[Route("{uid}/lecturerfullimage")]
        //[HttpGet]
        //[CacheOutput(ServerTimeSpan = 300, ExcludeQueryStringFromCacheKey = false)]
        //public async Task<IHttpActionResult> GetImage([FromUri]Guid uid, [FromUri]string fields = null)
        //{
        //    var result = await Task.FromResult(_conferenceAppService.GetLecturerImage(uid));

        //    if (result != null)
        //    {
        //        return await Json(result, fields);
        //    }

        //    return NotFound();
        //}

        ///// <summary>
        ///// Retorna a imagem de um participante da palestra específico
        ///// </summary> 
        ///// <remarks>Retorna a miniatura da imagem de um participante da palestra específico</remarks>
        ///// <param name="fields">Campos que serão retornados</param>
        ///// <param name="uid">Guid do participante da palestra</param>
        ///// <response code="200">Ok</response>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="403">Forbidden</response>        
        ///// <response code="500">Internal Server Error</response>
        //[Route("{uid}/lecturerthumbnailimage")]
        //[HttpGet]
        //[CacheOutput(ServerTimeSpan = 300, ExcludeQueryStringFromCacheKey = false)]
        //public async Task<IHttpActionResult> GetThumbImage([FromUri]Guid uid, [FromUri]string fields = null)
        //{
        //    var result = await Task.FromResult(_conferenceAppService.GetLecturerThumbImage(uid));

        //    if (result != null)
        //    {
        //        return await Json(result, fields);
        //    }

        //    return NotFound();
        //}        
    }
}
