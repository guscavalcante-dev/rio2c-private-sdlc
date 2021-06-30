// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 06-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-28-2021
// ***********************************************************************
// <copyright file="InnovationApiController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using MediatR;
using Newtonsoft.Json;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.ApiModels;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    [System.Web.Http.RoutePrefix("api/v1.0")]
    public class InnovationApiController : BaseApiController
    {
        private readonly IMediator commandBus;
        private readonly IdentityAutenticationService identityController;
        private readonly IEditionRepository editionsRepo;
        private readonly IMusicBandTypeRepository musicBandTypesRepo;
        private readonly IMusicGenreRepository musicGenresRepo;
        private readonly ITargetAudienceRepository targetAudiencesRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationApiController"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        public InnovationApiController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IEditionRepository editionsRepo,
            IMusicBandTypeRepository musicBandTypesRepo,
            IMusicGenreRepository musicGenresRepo,
            ITargetAudienceRepository targetAudiencesRepo)
        {
            this.commandBus = commandBus;
            this.identityController = identityController;
            this.editionsRepo = editionsRepo;
            this.musicBandTypesRepo = musicBandTypesRepo;
            this.musicGenresRepo = musicGenresRepo;
            this.targetAudiencesRepo = targetAudiencesRepo;
        }


        /// <summary>
        /// Creates the startup.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;IHttpActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        [HttpPost]
        [Route("CreateStartup/{key?}")]
        public async Task<IHttpActionResult> CreateStartup(string key, HttpRequestMessage request)
        {
            var validationResult = new AppValidationResult();

            try
            {
                #region Initial Validations

                if (key.ToLowerInvariant() != ConfigurationManager.AppSettings["CreateStartupApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

                var applicationUser = await identityController.FindByEmailAsync(Domain.Entities.User.BatchProcessUser.Email);
                if (applicationUser == null)
                {
                    throw new DomainException(Messages.UserNotFound);
                }

                var currentEdition = await editionsRepo.FindByIsCurrentAsync();
                if (currentEdition == null)
                {
                    throw new DomainException(Messages.CurrentEditionNotFound);
                }

                #endregion

                var innovationOrganizationApiDto = JsonConvert.DeserializeObject<InnovationOrganizationApiDto>(request.Content.ReadAsStringAsync().Result);
                if (!innovationOrganizationApiDto.IsValid())
                {
                    validationResult.Add(innovationOrganizationApiDto.ValidationResult);
                    throw new DomainException(Messages.CorrectFormValues);
                }

                var cmd = new CreateInnovationOrganization(innovationOrganizationApiDto);
                cmd.UpdatePreSendProperties(
                    applicationUser.Id,
                    applicationUser.Uid,
                    currentEdition.Id,
                    currentEdition.Uid,
                    ""); //TODO: Implements User interface language?

                validationResult = await this.commandBus.Send(cmd);
                if (!validationResult.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }
            }
            catch (DomainException ex)
            {
                return await Json(new { status = ApiStatus.Error, message = ex.GetInnerMessage(), errors = validationResult?.Errors?.Select(e => new { e.Code, e.Message }) });
            }
            catch (JsonSerializationException ex)
            {
                return await Json(new { status = ApiStatus.Error, message = ex.GetInnerMessage(), errors = validationResult?.Errors?.Select(e => new { e.Code, e.Message }) });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = ApiStatus.Error, message = Messages.WeFoundAndError });
            }

            return await Json(new { status = ApiStatus.Success, message = string.Format(Messages.EntityActionSuccessfull, Labels.MusicBand, Labels.CreatedF) });
        }

        /// <summary>
        /// Gets the music band types.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        [HttpGet]
        [Route("GetMusicBandTypes")]
        public async Task<IHttpActionResult> GetMusicBandTypes(string key)
        {
            try
            {
                if (key.ToLowerInvariant() != ConfigurationManager.AppSettings["CreateMusicBandApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

                var musicBandTypes = await this.musicBandTypesRepo.FindAllAsync();

                return await Json(new MusicBandTypesApiResponse
                {
                    MusicBandTypes = musicBandTypes.OrderBy(o => o.DisplayOrder).Select(mbt => new MusicBandTypeListApiItem()
                    {
                        Id = mbt.Id,
                        Name = mbt.Name
                    })?.ToList(),
                    Status = ApiStatus.Success
                });
            }
            catch (DomainException ex)
            {
                return await Json(new { status = ApiStatus.Error, message = ex.GetInnerMessage() });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = ApiStatus.Error, message = Messages.WeFoundAndError });
            }
        }

        /// <summary>
        /// Gets the music genres.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        [HttpGet]
        [Route("GetMusicGenres")]
        public async Task<IHttpActionResult> GetMusicGenres(string key)
        {
            try
            {
                if (key.ToLowerInvariant() != ConfigurationManager.AppSettings["CreateMusicBandApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

                var musicGenres = await this.musicGenresRepo.FindAllAsync();

                return await Json(new MusicGenresApiResponse
                {
                    MusicGenres = musicGenres.OrderBy(o => o.DisplayOrder).Select(mg => new MusicGenreListApiItem()
                    {
                        Id = mg.Id,
                        Name = mg.Name
                    })?.ToList(),
                    Status = ApiStatus.Success
                });
            }
            catch (DomainException ex)
            {
                return await Json(new { status = ApiStatus.Error, message = ex.GetInnerMessage() });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = ApiStatus.Error, message = Messages.WeFoundAndError });
            }
        }

        /// <summary>
        /// Gets the target audiences.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        [HttpGet]
        [Route("GetTargetAudiences")]
        public async Task<IHttpActionResult> GetTargetAudiences(string key)
        {
            try
            {
                if (key.ToLowerInvariant() != ConfigurationManager.AppSettings["CreateMusicBandApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

                //Using "ProjectType.Inovation" because in "[dbo].[MigrateMusicProjects]" procedure, its fixed too.
                var targetAudiences = await this.targetAudiencesRepo.FindAllByProjectTypeIdAsync(ProjectType.Inovation.Id);

                return await Json(new TargetAudiencesApiResponse
                {
                    TargetAudiences = targetAudiences.OrderBy(o => o.DisplayOrder).Select(ta => new TargetAudienceListApiItem()
                    {
                        Id = ta.Id,
                        Name = ta.Name
                    })?.ToList(),
                    Status = ApiStatus.Success
                });
            }
            catch (DomainException ex)
            {
                return await Json(new { status = ApiStatus.Error, message = ex.GetInnerMessage() });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = ApiStatus.Error, message = Messages.WeFoundAndError });
            }
        }
    }
}
