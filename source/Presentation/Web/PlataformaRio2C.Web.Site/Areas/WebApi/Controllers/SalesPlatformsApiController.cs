// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 07-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-12-2019
// ***********************************************************************
// <copyright file="SalesPlatformsApiController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti.Models;
using System.IO;
using System.Web.Script.Serialization;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>SalesPlatformsApiController</summary>
    [System.Web.Http.RoutePrefix("api/v1.0/salesplatforms")]
    public class SalesPlatformsApiController : BaseApiController
    {
        private readonly IMediator commandBus;

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformsApiController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        public SalesPlatformsApiController(IMediator commandBus)
        {
            this.commandBus = commandBus;
        }

        #region Inti requests

        /// <summary>Ticket Sold.</summary>
        /// <returns></returns>
        [HttpPost]
        [Route("inti-ticket-sold")]
        public async Task<IHttpActionResult> IntiTicketSold()
        {            
            var ctx = HttpContext.Current;
            var json = String.Empty;
            ctx.Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(ctx.Request.InputStream)){
                json = inputStream.ReadToEnd();
            }
            var dto = new JavaScriptSerializer().Deserialize<IntiSaleOrCancellation>(json);

            var headers = Request.Headers.ToString();
            var ip = HttpContext.Current.Request.GetIpAddress();
            var salesPlatformWebhooRequestUid = Guid.NewGuid();
            var result = await this.commandBus.Send(new CreateSalesPlatformWebhookRequest(
                salesPlatformWebhooRequestUid,
                "Inti",
                dto.id.ToString(),
                HttpContext.Current.Request.Url.AbsoluteUri,
                headers,
                json, // Request.Content.ReadAsStringAsync().Result,
                ip)); 

            return await Json(new { status = "success", message = "Received ticket sold" });
        }


        [HttpPost]
        [Route("receive-payload")]
        public async Task<IHttpActionResult> receivePayload()
        {
            var ctx = HttpContext.Current;
            var json = String.Empty;
            ctx.Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(ctx.Request.InputStream))
            {
                json = inputStream.ReadToEnd();
            }

            var content = json;
            
            return await Json(new { status = "ok" });
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await BadRequest(ex.GetInnerMessage());
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await BadRequest(ex.GetInnerMessage());
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
            var result = new AppValidationResult();

            try
            {
                if (key?.ToLowerInvariant() != ConfigurationManager.AppSettings["ProcessWebhookRequestsApiKey"]?.ToLowerInvariant())
                {
                    throw new Exception("Invalid key to execute process webhook requests.");
                }

                result = await this.commandBus.Send(new ProcessPendingPlatformWebhookRequestsAsync());
                if (!result.IsValid)
                {
                    throw new DomainException("Sales platform webhook requests processed with some errors.");
                }
            }
            catch (DomainException ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = "success", message = ex.GetInnerMessage(), errors = result?.Errors?.Select(e => new { e.Code, e.Message }) });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = "error", message = "Sales platform webhook requests failed." });
            }

            return await Json(new { status = "success", message = "Sales platform webhook requests processed successfully without errors." });
        }

        #endregion
    }
}
