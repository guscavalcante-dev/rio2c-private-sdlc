// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 07-10-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-24-2022
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
using System.Web.Http.Description;
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Statics;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>SalesPlatformsApiController</summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    [System.Web.Http.RoutePrefix("api/v1.0/salesplatforms")]
    public class SalesPlatformsApiController : BaseApiController
    {
        private readonly IMediator commandBus;
        private readonly IAttendeeSalesPlatformRepository attendeeSalesPlatformRepo;
        private readonly ISalesPlatformServiceFactory salesPlatformServiceFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesPlatformsApiController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="attendeeSalesPlatformRepository">The attendee sales platform repository.</param>
        /// <param name="salesPlatformServiceFactory">The sales platform service factory.</param>
        public SalesPlatformsApiController(
            IMediator commandBus,
            IAttendeeSalesPlatformRepository attendeeSalesPlatformRepository,
            ISalesPlatformServiceFactory salesPlatformServiceFactory)
        {
            this.commandBus = commandBus;
            this.attendeeSalesPlatformRepo = attendeeSalesPlatformRepository;
            this.salesPlatformServiceFactory = salesPlatformServiceFactory;
        }

        #region Inti requests

        /// <summary>
        /// Endpoint for INTI webhook requests
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("inti")]
        public async Task<IHttpActionResult> Inti()
        {
            try
            {
                //TODO: Inti doesn't send APIKey as parameter. Fix this!
                string key = "7A8C7EDC-3084-47D5-AD5A-DF6A128B341C";

                var salesPlatformWebhooRequestUid = Guid.NewGuid();
                var result = await this.commandBus.Send(new CreateSalesPlatformWebhookRequest(
                    salesPlatformWebhooRequestUid,
                    SalePlatformName.Inti,
                    key,
                    HttpContext.Current.Request.Url.AbsoluteUri,
                    Request.Headers.ToString(),
                    Request.Content.ReadAsStringAsync().Result,
                    HttpContext.Current.Request.GetIpAddress()));
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

            return await Json(new { status = "success", message = $"{SalePlatformName.Inti} event saved successfully." });
        }

        #endregion

        #region Eventbrite requests

        /// <summary>
        /// Endpoint for EventBrite webhook requests
        /// </summary>
        /// <param name="key">The key.</param>
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
                    SalePlatformName.Eventbrite,
                    key,
                    HttpContext.Current.Request.Url.AbsoluteUri,
                    Request.Headers.ToString(),
                    Request.Content.ReadAsStringAsync().Result,
                    HttpContext.Current.Request.GetIpAddress()));
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

            return await Json(new { status = "success", message = $"{SalePlatformName.Eventbrite} event saved successfully." });
        }

        #endregion

        #region Sympla requests

        /// <summary>
        /// Endpoint for Sympla webhook requests
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("sympla/{key?}")]
        public async Task<IHttpActionResult> Sympla(string key)
        {
            try
            {
                var attendeeSalesPlatformDto = await this.attendeeSalesPlatformRepo.FindDtoByNameAsync(SalePlatformName.Sympla);
                var salesPlatformService = salesPlatformServiceFactory.Get(attendeeSalesPlatformDto);
                var salesPlatformAttendeeDtos = salesPlatformService.GetAttendeesByEventId(attendeeSalesPlatformDto?.AttendeeSalesPlatform?.SalesPlatformEventid);

                //TODO: Remove this foreach and create a specific command wich receive "salesPlatformAttendeeDtos" as parameter
                //and proccess all the payloads with only one command call.
                foreach (var salesPlatformAttendeeDto in salesPlatformAttendeeDtos)
                {
                    var salesPlatformWebhooRequestUid = Guid.NewGuid();
                    var result = await this.commandBus.Send(new CreateSalesPlatformWebhookRequest(
                        salesPlatformWebhooRequestUid,
                        SalePlatformName.Sympla,
                        key,
                        HttpContext.Current.Request.Url.AbsoluteUri,
                        Request.Headers.ToString(),
                        salesPlatformAttendeeDto.Payload,
                        HttpContext.Current.Request.GetIpAddress()));
                }
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

            return await Json(new { status = "success", message = $"{SalePlatformName.Sympla} event saved successfully." });
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
