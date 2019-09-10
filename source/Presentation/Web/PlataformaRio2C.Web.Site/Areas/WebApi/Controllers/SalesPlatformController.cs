// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 07-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-10-2019
// ***********************************************************************
// <copyright file="SalesPlatformController.cs" company="Softo">
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
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>
    /// Class for sales platforms endpoints
    /// </summary>
    [System.Web.Http.RoutePrefix("api/v1.0/salesplatforms")]
    public class SalesPlatformController : BaseApiController
    {
        private readonly IMediator commandBus;

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        public SalesPlatformController(IMediator commandBus)
        {
            this.commandBus = commandBus;
        }

        #region Test Welcome Email

        /// <summary>Pings this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        [Route("testwelcomeemail")]
        public async Task<IHttpActionResult> TestWelcomeEmail()
        {
            await this.commandBus.Send(new SendWelcomeEmailAsync(
                "71e9d659-1645-4c1b-a41a-c67a3ae4f42c", 
                1, 
                new Guid("4003E6DD-3DA8-45B5-9A68-0B67759DBD0E"), 
                "Rafael", 
                "Rafael Ruiz", 
                "rafael.ruiz@sof.to", 
                2, 
                "Rio2C 2020", 
                2020, 
                "pt-BR"));
            return await Json(new { status = "success", message = "Welcome email sent." });
        }

        #endregion

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
                return await BadRequest(ex.GetInnerMessage());
            }
            catch (Exception ex)
            {
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(ex));
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
                    throw new DomainException("Invalid key to execute process webhook requests.");
                }

                result = await this.commandBus.Send(new ProcessPendingPlatformWebhookRequestsAsync());
                if (!result.IsValid)
                {
                    throw new DomainException("Sales platform webhook requests processed with some errors.");
                }
            }
            catch (DomainException ex)
            {
                return await Json(new { status = "success", message = ex.GetInnerMessage(), errors = result?.Errors?.Select(e => new { e.Code, e.Message }) });
            }
            catch (Exception ex)
            {
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(ex));
                return await Json(new { status = "error", message = "Sales platform webhook requests failed." });
            }

            return await Json(new { status = "success", message = "Sales platform webhook requests processed successfully without errors." });
        }

        #endregion
    }
}
