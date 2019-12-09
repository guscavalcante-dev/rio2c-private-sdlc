// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 12-03-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-03-2019
// ***********************************************************************
// <copyright file="NetworksApiController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>
    /// Class for sales platforms endpoints
    /// </summary>
    [System.Web.Http.RoutePrefix("api/v1.0/networks")]
    public class NetworksApiController : BaseApiController
    {
        private readonly IMediator commandBus;

        /// <summary>Initializes a new instance of the <see cref="NetworksApiController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        public NetworksApiController(IMediator commandBus)
        {
            this.commandBus = commandBus;
        }

        #region Send unread conversations emails

        /// <summary>Sends the unread conversations emails.</summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("SendUnreadConversationsEmails/{key?}")]
        public async Task<IHttpActionResult> SendUnreadConversationsEmails(string key)
        {
            var result = new AppValidationResult();

            try
            {
                if (key?.ToLowerInvariant() != ConfigurationManager.AppSettings["SendUnreadConversationsEmailsApiKey"]?.ToLowerInvariant())
                {
                    throw new Exception("Invalid key to execute send unread conversations emails.");
                }

                result = await this.commandBus.Send(new SendUnreadConversationsEmailsAsync());
                if (!result.IsValid)
                {
                    throw new DomainException("Send unread conversations emails processed with some errors.");
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
                return await Json(new { status = "error", message = "Send unread conversations emails failed." });
            }

            return await Json(new { status = "success", message = "Send unread conversations emails processed successfully without errors." });
        }

        #endregion
    }
}
