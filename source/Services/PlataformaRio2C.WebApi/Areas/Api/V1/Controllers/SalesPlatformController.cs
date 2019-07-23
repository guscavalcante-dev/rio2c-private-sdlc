// ***********************************************************************
// Assembly         : PlataformaRio2C.WebApi
// Author           : Rafael Dantas Ruiz
// Created          : 07-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-22-2019
// ***********************************************************************
// <copyright file="SalesPlatformController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.WebApi.Areas.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using WebApi.OutputCache.V2;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.WebApi.Areas.Api.V1.Controllers
{
    /// <summary>
    /// Class for Eventbrite endpoints
    /// </summary>
    //[Authorize]
    [Microsoft.Web.Http.ApiVersion("1.0")]
    [RoutePrefix("api/v{api-version:apiVersion}/salesplatforms")]
    public class SalesPlatformController : BaseApiController
    {
        private readonly IMediator commandBus;
        private readonly ISalesPlatformServiceFactory salesPlatformServiceFactory;

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="salesPlatformServiceFactory">The sales platform service factory.</param>
        public SalesPlatformController(IMediator commandBus, ISalesPlatformServiceFactory salesPlatformServiceFactory)
        {
            this.commandBus = commandBus;
            this.salesPlatformServiceFactory = salesPlatformServiceFactory;
        }

        /// <summary>Pings this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ping")]
        public async Task<IHttpActionResult> Ping()
        {
            return await Json(new { status = "success", message = "Pong" });
        }

        /// <summary>Tests this instance.</summary>
        /// <returns></returns>
        [HttpPost]
        [Route("eventbrite/{key?}")]
        public async Task<IHttpActionResult> Eventbrite(string key)
        {
            try
            {
                var salesPlatformWebhooRequestUid = Guid.NewGuid();
                var result = await this.commandBus.Send(new CreateSalesPlatformWebhookRequest(
                    salesPlatformWebhooRequestUid,
                    "Eventbrite",
                    key,
                    HttpContext.Current.Request.Url.AbsoluteUri,
                    Request.Headers.ToString(),
                    Request.Content.ReadAsStringAsync().Result,
                    HttpContext.Current.Request.GetIpAddress()));
                //if (response.Errors.Any())
                //{
                //    return BadRequest(response.Errors);
                //}

                //return Ok(response.Value);
            }
            catch (DomainException ex)
            {
                return await Json(new { status = "error", message = ex.GetInnerMessage() });
                //this.SetResultMessage(new ResultMessage(this.localizer[ex.GetInnerMessage()], ResultMessageType.Error));
            }
            catch (Exception ex)
            {
                return await Json(new { status = "error", message = "Undefined error." });
                //HttpContext.RiseError(ex);
                //return BadRequest(ex.Message);
            }

            return await Json(new { status = "success", message = "Eventbrite event saved successfully." });
        }

        /// <summary>Processes the requests.</summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("processrequests/{key?}")]
        public async Task<IHttpActionResult> ProcessRequests(string key)
        {
            try
            {
                var pendingRequests = await this.commandBus.Send(new FindAllSalesPlatformWebhooRequestsByPending());
                if (pendingRequests == null)
                {
                    return null;
                }

                // Mark all requests as processing
                var processingRequestsUids = await this.commandBus.Send(new ProcessSalesPlatformWebhookRequests(pendingRequests.Select(m => m.Uid).ToList()));

                foreach (var pendingRequest in pendingRequests.Where(m => processingRequestsUids.Contains(m.Uid)))
                {
                    try
                    {
                        var salesPlatformService = this.salesPlatformServiceFactory.Get(pendingRequest);
                        salesPlatformService.ExecuteRequest();

                        // Set as processed
                        await this.commandBus.Send(new ConcludeSalesPlatformWebhookRequest(pendingRequest.Uid, pendingRequest.SecurityStamp));
                    }
                    catch (Exception ex)
                    {
                        // Set to process agin
                        await this.commandBus.Send(new PostponeSalesPlatformWebhookRequest(pendingRequest.Uid, null, ex.GetInnerMessage(), pendingRequest.SecurityStamp));
                    }
                }
            }
            catch (DomainException ex)
            {
                return await Json(new { status = "error", message = ex.GetInnerMessage() });
            }
            catch (Exception ex)
            {
                return await Json(new { status = "error", message = "Undefined error." });
                //HttpContext.RiseError(ex);
                //return BadRequest(ex.Message);
            }

            return await Json(new { status = "success", message = "Eventbrite event saved successfully." });
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
