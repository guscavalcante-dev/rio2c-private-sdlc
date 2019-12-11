// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-11-2019
// ***********************************************************************
// <copyright file="ProjectsApiController.cs" company="Softo">
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
    /// <summary>ProjectsApiController</summary>
    [System.Web.Http.RoutePrefix("api/v1.0/projects")]
    public class ProjectsApiController : BaseApiController
    {
        private readonly IMediator commandBus;

        /// <summary>Initializes a new instance of the <see cref="ProjectsApiController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        public ProjectsApiController(IMediator commandBus)
        {
            this.commandBus = commandBus;
        }

        #region Send Project Buyer Evaluation Emails

        /// <summary>Sends the projects buyers evaluations emails.</summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("SendProjectsBuyersEvaluationsEmails/{key?}")]
        public async Task<IHttpActionResult> SendProjectsBuyersEvaluationsEmails(string key)
        {
            var result = new AppValidationResult();

            try
            {
                if (key?.ToLowerInvariant() != ConfigurationManager.AppSettings["SendProjectsBuyersEvaluationsEmailsApiKey"]?.ToLowerInvariant())
                {
                    throw new Exception("Invalid key to execute send projects buyers evaluations emails.");
                }

                result = await this.commandBus.Send(new SendProjectsBuyersEvaluationsEmailsAsync());
                if (!result.IsValid)
                {
                    throw new DomainException("Send projects buyers evaluations emails processed with some errors.");
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
                return await Json(new { status = "error", message = "Send projects buyers evaluations emails failed." });
            }

            return await Json(new { status = "success", message = "Send projects buyers evaluations emails processed successfully without errors." });
        }

        #endregion
    }
}
