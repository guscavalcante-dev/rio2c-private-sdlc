// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 07-10-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-02-2023
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
using PlataformaRio2C.Domain.ApiModels;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Resources;
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
        private readonly IUserRepository userRepo;
        private readonly IEditionRepository editionRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesPlatformsApiController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="userRepository">The identity controller.</param>
        /// <param name="editionRepository">The edition repository.</param>
        public SalesPlatformsApiController(
            IMediator commandBus,
            IUserRepository userRepository,
            IEditionRepository editionRepository)
        {
            this.commandBus = commandBus;
            this.userRepo = userRepository;
            this.editionRepo = editionRepository;
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

                var result = await this.commandBus.Send(new CreateSalesPlatformWebhookRequest(
                    Guid.NewGuid(),
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
                var result = await this.commandBus.Send(new CreateSalesPlatformWebhookRequest(
                    Guid.NewGuid(),
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
        /// <param name="reimportAllAttendees">if set to <c>true</c> [reimport all attendees].</param>
        /// <returns></returns>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        [HttpGet]
        [Route("sympla/{key?}")]
        public async Task<IHttpActionResult> Sympla(string key, bool reimportAllAttendees = false)
        {
            var result = new AppValidationResult();

            try
            {
                result = await this.commandBus.Send(new CreateSalesPlatformWebhookRequest(
                    Guid.Empty, // Sympla generates Uids inside command handler
                    SalePlatformName.Sympla,
                    key,
                    HttpContext.Current.Request.Url.AbsoluteUri,
                    Request.Headers.ToString(),
                    null,       // Sympla gets the payload inside command handler
                    HttpContext.Current.Request.GetIpAddress(),
                    reimportAllAttendees));

                if (!result.IsValid)
                {
                    throw new DomainException($"{SalePlatformName.Sympla} webhooks imported with some errors: {result.Errors.Select(e => e.Message).Distinct().ToString("; ")}");
                }
            }
            catch (DomainException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = "error", message = ex.GetInnerMessage(), errors = result?.Errors?.Select(e => new { e.Code, e.Message }) });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await BadRequest(ex.GetInnerMessage());
            }

            return await Json(new { status = "success", message = $"{SalePlatformName.Sympla} webhooks imported successfully." });
        }

        #endregion

        #region Requests processing

        /// <summary>
        /// Processes the requests.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Invalid key to execute process webhook requests.</exception>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException">Sales platform webhook requests processed with some errors.</exception>
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

        /// <summary>
        /// Sends the producer welcome email.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Invalid key to send welcome email.</exception>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        [HttpGet]
        [Route("sendproducerwelcomeemail")]
        public async Task<IHttpActionResult> SendProducerWelcomeEmail(string key, string email)
        {
            var result = new AppValidationResult();

            try
            {
                if (key?.ToLowerInvariant() != ConfigurationManager.AppSettings["SendProducerWelcomeEmailApiKey"]?.ToLowerInvariant())
                {
                    throw new Exception("Invalid key to send welcome email.");
                }

                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new DomainException("The email is required");
                }

                var user = await userRepo.FindUserByEmailAsync(email);
                if (user == null)
                {
                    throw new DomainException($@"{Messages.UserNotFound}: {email}");
                }

                var currentEdition = await editionRepo.FindByIsCurrentAsync();
                if (currentEdition == null)
                {
                    throw new DomainException(Messages.CurrentEditionNotFound);
                }

                result = await this.commandBus.Send(new SendProducerWelcomeEmailAsync(
                        user.SecurityStamp,
                        user.Id,
                        user.Uid,
                        user.Collaborator.FirstName,
                        user.Collaborator.GetFullName(),
                        user.Email,
                        currentEdition,
                        "pt-BR"
                    ));

                if (!result.IsValid)
                {
                    throw new DomainException(string.Format("Error when trying to send a producer welcome email to {0}", email));
                }
            }
            catch (DomainException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = ApiStatus.Error, message = ex.GetInnerMessage(), errors = result?.Errors?.Select(e => new { e.Code, e.Message }) });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = ApiStatus.Error, message = string.Format("Error when trying to send a producer welcome email to {0}", email) });
            }

            return await Json(new { status = ApiStatus.Success, message = string.Format("An Producer welcome email has been sent sucessfully to {0}", email) });
        }

        #endregion
    }
}
