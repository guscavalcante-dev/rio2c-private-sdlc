// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 08-11-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-11-2023
// ***********************************************************************
// <copyright file="WeConnectApiController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Statics;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>WeConnectApiController</summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    [System.Web.Http.RoutePrefix("api/v1.0/weconnect")]
    public class WeConnectApiController : BaseApiController
    {
        private readonly IMediator commandBus;
        private readonly IdentityAutenticationService identityController;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeConnectApiController"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        public WeConnectApiController(
            IMediator commandBus,
            IdentityAutenticationService identityController)
        {
            this.commandBus = commandBus;
            this.identityController = identityController;
        }

        #region Instagram Syncronize

        /// <summary>
        /// Synchronizes the instagram publications.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        [HttpGet]
        [Route("syncInstagramPublications/{key?}")]
        public async Task<IHttpActionResult> SyncInstagramPublications(string key)
        {
            var result = new AppValidationResult();
            
            try
            {
                var applicationUser = await identityController.FindByEmailAsync(Domain.Entities.User.BatchProcessUser.Email);
                if (applicationUser == null)
                {
                    throw new DomainException(Messages.UserNotFound);
                }

                var cmd = new SynchronizeWeConnectPublications(
                    SocialMediaPlatformName.Instagram, 
                    key);

                cmd.UpdatePreSendProperties(
                    applicationUser.Id,
                    applicationUser.Uid,
                    null,
                    null, 
                    "");

                result = await this.commandBus.Send(cmd);

                if (!result.IsValid)
                {
                    throw new DomainException($"{SocialMediaPlatformName.Instagram} publications synchronized with some errors: {result.Errors.Select(e => e.Message).Distinct().ToString("; ")}");
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

            return await Json(new { status = "success", message = $"{Labels.Instagram} publications imported successfully." });
        }

        #endregion
    }
}
