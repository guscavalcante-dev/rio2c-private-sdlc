// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 03-01-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-31-2021
// ***********************************************************************
// <copyright file="MusicBandsApiController.cs" company="Softo">
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
using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter.Context;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    [System.Web.Http.RoutePrefix("api/v1.0")]
    public class MusicBandsApiController : BaseApiController
    {
        private readonly IMediator commandBus;
        private readonly IdentityAutenticationService identityController;
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly IMusicBandTypeRepository musicBandTypesRepo;
        private readonly IMusicGenreRepository musicGenresRepo;
        private readonly ITargetAudienceRepository targetAudiencesRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBandsApiController"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        public MusicBandsApiController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository,
            IMusicBandTypeRepository musicBandTypesRepository,
            IMusicGenreRepository musicGenresRepository,
            ITargetAudienceRepository targetAudiencesRepository)
        {
            this.commandBus = commandBus;
            this.identityController = identityController;
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
            this.musicBandTypesRepo = musicBandTypesRepository;
            this.musicGenresRepo = musicGenresRepository;
            this.targetAudiencesRepo = targetAudiencesRepository;
        }

        /// <summary>
        /// Creates the music band.
        /// </summary>
        /// <param name="musicBandApiDto">The music band dto.</param>
        /// <returns></returns>
        /// <exception cref="DomainException">
        /// </exception>
        [HttpGet]
        [Route("CreateMusicBand/{key?}")]
        public async Task<IHttpActionResult> CreateMusicBand(string key, HttpRequestMessage request)
        {
            var validationResult = new AppValidationResult();

            try
            {
                #region Initial Validations

                if (key.ToLowerInvariant() != ConfigurationManager.AppSettings["CreateMusicBandApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

                var applicationUser = await identityController.FindByEmailAsync(Domain.Entities.User.BatchProcessUser.Email);
                if (applicationUser == null)
                {
                    throw new DomainException(Messages.UserNotFound);
                }

                var currentEdition = await editionRepo.FindByIsCurrentAsync();
                if (currentEdition == null)
                {
                    throw new DomainException(Messages.CurrentEditionNotFound);
                }

                #endregion

                var musicBandApiDto = JsonConvert.DeserializeObject<MusicBandApiDto>(request.Content.ReadAsStringAsync().Result);
                if (!musicBandApiDto.IsValid())
                {
                    validationResult.Add(musicBandApiDto.ValidationResult);
                    throw new DomainException(Messages.CorrectFormValues);
                }

                var cmd = new CreateMusicBand(musicBandApiDto);
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
        public async Task<IHttpActionResult> GetMusicBandTypes([FromUri] MusicBandTypesApiRequest request)
        {
            try
            {
                var editions = await this.editionRepo.FindAllByIsActiveAsync(false);
                if (editions?.Any() == false)
                {
                    return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "No active editions found." } });
                }

                // Get edition from request otherwise get current
                var edition = request?.Edition.HasValue == true ? editions?.FirstOrDefault(e => e.UrlCode == request.Edition) :
                                                                  editions?.FirstOrDefault(e => e.IsCurrent);
                if (edition == null)
                {
                    return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00002", Message = "No editions found." } });
                }

                // Get language from request otherwise get default
                var languages = await this.languageRepo.FindAllDtosAsync();
                var requestLanguage = languages?.FirstOrDefault(l => l.Code == request?.Culture);
                var defaultLanguage = languages?.FirstOrDefault(l => l.IsDefault);
                if (requestLanguage == null && defaultLanguage == null)
                {
                    return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00003", Message = "No active languages found." } });
                }

                var musicBandTypes = await this.musicBandTypesRepo.FindAllAsync();

                return await Json(new MusicBandTypesApiResponse
                {
                    MusicBandTypes = musicBandTypes.OrderBy(o => o.DisplayOrder).Select(mbt => new ApiListItemBaseResponse()
                    {
                        Uid = mbt.Uid,
                        Name = mbt.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
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
        public async Task<IHttpActionResult> GetMusicGenres([FromUri] MusicGenresApiRequest request)
        {
            try
            {
                var editions = await this.editionRepo.FindAllByIsActiveAsync(false);
                if (editions?.Any() == false)
                {
                    return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "No active editions found." } });
                }

                // Get edition from request otherwise get current
                var edition = request?.Edition.HasValue == true ? editions?.FirstOrDefault(e => e.UrlCode == request.Edition) :
                                                                  editions?.FirstOrDefault(e => e.IsCurrent);
                if (edition == null)
                {
                    return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00002", Message = "No editions found." } });
                }

                // Get language from request otherwise get default
                var languages = await this.languageRepo.FindAllDtosAsync();
                var requestLanguage = languages?.FirstOrDefault(l => l.Code == request?.Culture);
                var defaultLanguage = languages?.FirstOrDefault(l => l.IsDefault);
                if (requestLanguage == null && defaultLanguage == null)
                {
                    return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00003", Message = "No active languages found." } });
                }

                var musicGenres = await this.musicGenresRepo.FindAllAsync();

                return await Json(new MusicGenresApiResponse
                {
                    MusicGenres = musicGenres.OrderBy(o => o.DisplayOrder).Select(mg => new ApiListItemBaseResponse()
                    {
                        Uid = mg.Uid,
                        Name = mg.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
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
        public async Task<IHttpActionResult> GetTargetAudiences([FromUri] TargetAudiencesApiRequest request)
        {
            try
            {
                var editions = await this.editionRepo.FindAllByIsActiveAsync(false);
                if (editions?.Any() == false)
                {
                    return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "No active editions found." } });
                }

                // Get edition from request otherwise get current
                var edition = request?.Edition.HasValue == true ? editions?.FirstOrDefault(e => e.UrlCode == request.Edition) :
                                                                  editions?.FirstOrDefault(e => e.IsCurrent);
                if (edition == null)
                {
                    return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00002", Message = "No editions found." } });
                }

                // Get language from request otherwise get default
                var languages = await this.languageRepo.FindAllDtosAsync();
                var requestLanguage = languages?.FirstOrDefault(l => l.Code == request?.Culture);
                var defaultLanguage = languages?.FirstOrDefault(l => l.IsDefault);
                if (requestLanguage == null && defaultLanguage == null)
                {
                    return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00003", Message = "No active languages found." } });
                }

                //Using "ProjectType.Inovation" because in "[dbo].[MigrateMusicProjects]" procedure, its fixed too.
                var targetAudiences = await this.targetAudiencesRepo.FindAllByProjectTypeIdAsync(ProjectType.Inovation.Id);

                return await Json(new TargetAudiencesApiResponse
                {
                    TargetAudiences = targetAudiences.OrderBy(o => o.DisplayOrder).Select(ta => new ApiListItemBaseResponse()
                    {
                        Uid = ta.Uid,
                        Name = ta.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
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
