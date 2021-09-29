// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 03-01-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-13-2021
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
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>
    /// MusicBandsApiController
    /// </summary>
    [System.Web.Http.RoutePrefix("api/v1.0/music")]
    public class MusicBandsApiController : BaseApiController
    {
        private readonly IMediator commandBus;
        private readonly IdentityAutenticationService identityController;
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly IMusicBandTypeRepository musicBandTypesRepo;
        private readonly IMusicGenreRepository musicGenresRepo;
        private readonly ITargetAudienceRepository targetAudiencesRepo;
        private readonly IMusicBandRepository musicBandRepo;
        private readonly IUnitOfWork uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBandsApiController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="musicBandTypesRepository">The music band types repository.</param>
        /// <param name="musicGenresRepository">The music genres repository.</param>
        /// <param name="targetAudiencesRepository">The target audiences repository.</param>
        public MusicBandsApiController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository,
            IMusicBandTypeRepository musicBandTypesRepository,
            IMusicGenreRepository musicGenresRepository,
            ITargetAudienceRepository targetAudiencesRepository,
            IMusicBandRepository musicBandRepository,
            IUnitOfWork unitOfWork)
        {
            this.commandBus = commandBus;
            this.identityController = identityController;
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
            this.musicBandTypesRepo = musicBandTypesRepository;
            this.musicGenresRepo = musicGenresRepository;
            this.targetAudiencesRepo = targetAudiencesRepository;
            this.musicBandRepo = musicBandRepository;
            this.uow = unitOfWork;
        }

        /// <summary>
        /// Creates the music band.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("createmusicband/{key?}")]
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

                var cmd = new CreateMusicBand(
                        musicBandApiDto.MusicBandTypeUid ?? Guid.Empty,
                        musicBandApiDto.Name,
                        musicBandApiDto.ImageFile,
                        musicBandApiDto.FormationDate,
                        musicBandApiDto.MainMusicInfluences,
                        musicBandApiDto.Facebook,
                        musicBandApiDto.Instagram,
                        musicBandApiDto.Twitter,
                        musicBandApiDto.Youtube,
                        musicBandApiDto.MusicProjectApiDto,
                        musicBandApiDto.MusicBandResponsibleApiDto,
                        musicBandApiDto.MusicBandMembersApiDtos,
                        musicBandApiDto.MusicBandTeamMembersApiDtos,
                        musicBandApiDto.ReleasedMusicProjectsApiDtos,
                        musicBandApiDto.MusicGenresApiDtos,
                        musicBandApiDto.TargetAudiencesApiDtos);

                cmd.UpdatePreSendProperties(
                    applicationUser.Id,
                    applicationUser.Uid,
                    currentEdition.Id,
                    currentEdition.Uid,
                    ""); //TODO: Implements User interface language?

                validationResult = await this.commandBus.Send(cmd);
                if (!validationResult.IsValid)
                {
                    throw new DomainException();
                }
            }
            catch (DomainException ex)
            {
                return await Json(new
                {
                    status = ApiStatus.Error,
                    message = ex.GetInnerMessage(),
                    errors = validationResult?.Errors?.Select(e => new { e.Code, e.Message })
                });
            }
            catch (JsonSerializationException ex)
            {
                return await Json(new
                {
                    status = ApiStatus.Error,
                    message = ex.GetInnerMessage(),
                    errors = validationResult?.Errors?.Select(e => new { e.Code, e.Message })
                });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = ApiStatus.Error, message = Messages.WeFoundAndError });
            }

            return await Json(new { status = ApiStatus.Success, message = string.Format(Messages.EntityActionSuccessfull, Labels.MusicBand, Labels.CreatedF) });
        }

        /// <summary>
        /// Get music API filters
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("filters")]
        public async Task<IHttpActionResult> Filters([FromUri] MusicFiltersApiRequest request)
        {
            try
            {
                #region Initial Validations

                var activeEditions = await this.editionRepo.FindAllByIsActiveAsync(false);
                if (activeEditions?.Any() == false)
                {
                    return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "No active editions found." } });
                }

                // Get edition from request otherwise get current
                var edition = request?.Edition.HasValue == true ? activeEditions?.FirstOrDefault(e => e.UrlCode == request.Edition) :
                                                                  activeEditions?.FirstOrDefault(e => e.IsCurrent);
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

                #endregion

                var musicBandTypes = await this.musicBandTypesRepo.FindAllAsync();
                var musicGenres = await this.musicGenresRepo.FindAllAsync();
                var targetAudiences = await this.targetAudiencesRepo.FindAllByProjectTypeIdAsync(ProjectType.Inovation.Id);

                return await Json(new MusicFiltersApiResponse
                {
                    MusicBandTypes = musicBandTypes.OrderBy(o => o.DisplayOrder).Select(mbt => new ApiListItemBaseResponse()
                    {
                        Uid = mbt.Uid,
                        Name = mbt.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
                    })?.ToList(),

                    MusicGenres = musicGenres.OrderBy(o => o.DisplayOrder).Select(mg => new ApiListItemBaseResponse()
                    {
                        Uid = mg.Uid,
                        Name = mg.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
                    })?.ToList(),

                    TargetAudiences = targetAudiences.OrderBy(o => o.DisplayOrder).Select(ta => new ApiListItemBaseResponse()
                    {
                        Uid = ta.Uid,
                        Name = ta.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
                    })?.ToList(),

                    Status = ApiStatus.Success
                });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00004", Message = "Music filters api failed." } });
            }
        }

        [HttpGet]
        [Route("migrateimagestoaws")]
        public async Task<IHttpActionResult> MigrateImagesToAWS(string key, HttpRequestMessage request)
        {
            if (key.ToLowerInvariant() != "4f6d4f34-c9ef-4721-bf50-363e370d7d4e")
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "Key does not match!" } });

            try
            {
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
                HttpClientHandler handler = new HttpClientHandler { Credentials = credentials };
                HttpClient client = new HttpClient(handler);
                this.uow.BeginTransaction();

                var musicBands = this.musicBandRepo.GetAll(w => w.ImageUrl != null).ToList();
                foreach (var musicBand in musicBands)
                {
                    if (!string.IsNullOrEmpty(musicBand.ImageUrl))
                    {
                        try
                        {
                            var imageBytes = await client.GetByteArrayAsync(musicBand.ImageUrl);

                            PlataformaRio2c.Infra.Data.FileRepository.Helpers.ImageHelper.UploadOriginalAndThumbnailImages(
                                musicBand.Uid,
                                Convert.ToBase64String(imageBytes),
                                Domain.Statics.FileRepositoryPathType.MusicBandImage);

                            musicBand.UpdateImageUploadDate(true, false);
                            musicBand.ImageUrl = null;
                            this.musicBandRepo.Update(musicBand);
                        }
                        catch
                        {
                        }
                    }
                }

                this.uow.SaveChanges();

                return await Json(new { status = ApiStatus.Success, message = string.Format(Messages.EntityActionSuccessfull, Labels.MusicBands, Labels.UpdatedM) });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00004", Message = "Music filters api failed." } });
            }
        }
    }
}
