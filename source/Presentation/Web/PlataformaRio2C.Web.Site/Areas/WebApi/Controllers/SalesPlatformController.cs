// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 07-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-30-2019
// ***********************************************************************
// <copyright file="SalesPlatformController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>
    /// Class for sales platforms endpoints
    /// </summary>
    [RoutePrefix("api/v1.0/salesplatforms")]
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

        #region Ping

        /// <summary>Pings this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ping")]
        public async Task<IHttpActionResult> Ping()
        {
            return await Json(new { status = "success", message = "Pong" });
        }

        #endregion

        #region Eventbrite requests

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

        #endregion

        #region Requests processing

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
                        //throw new DomainException("teste");
                        var salesPlatformService = this.salesPlatformServiceFactory.Get(pendingRequest);
                        salesPlatformService.ExecuteRequest();

                        // Set as processed
                        await this.commandBus.Send(new ConcludeSalesPlatformWebhookRequest(pendingRequest.Uid, pendingRequest.SecurityStamp));
                    }
                    catch (Exception ex)
                    {
                        // Set to process again
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

            return await Json(new { status = "success", message = "Requests processed successfully." });
        }

        #endregion
    }
}
