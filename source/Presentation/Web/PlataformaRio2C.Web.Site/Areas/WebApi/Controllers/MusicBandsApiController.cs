// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 03-01-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2024
// ***********************************************************************
// <copyright file="MusicBandsApiController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Newtonsoft.Json;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.ApiModels;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using Swashbuckle.Swagger.Annotations;
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
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IdentityAutenticationService identityController;
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly IMusicBandTypeRepository musicBandTypesRepo;
        private readonly IMusicGenreRepository musicGenresRepo;
        private readonly ITargetAudienceRepository targetAudiencesRepo;
        private readonly IMusicBandRepository musicBandRepo;
        private readonly IUnitOfWork uow;
        private readonly IFileRepository fileRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBandsApiController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="musicBandTypesRepository">The music band types repository.</param>
        /// <param name="musicGenresRepository">The music genres repository.</param>
        /// <param name="targetAudiencesRepository">The target audiences repository.</param>
        /// <param name="musicBandRepository">The music band repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="fileRepository">The file repository.</param>
        public MusicBandsApiController(
            IMediator commandBus,
            ICollaboratorRepository collaboratorRepository,
            IdentityAutenticationService identityController,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository,
            IMusicBandTypeRepository musicBandTypesRepository,
            IMusicGenreRepository musicGenresRepository,
            ITargetAudienceRepository targetAudiencesRepository,
            IMusicBandRepository musicBandRepository,
            IUnitOfWork unitOfWork,
            IFileRepository fileRepository)
        {
            this.commandBus = commandBus;
            this.collaboratorRepo = collaboratorRepository;
            this.identityController = identityController;
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
            this.musicBandTypesRepo = musicBandTypesRepository;
            this.musicGenresRepo = musicGenresRepository;
            this.targetAudiencesRepo = targetAudiencesRepository;
            this.musicBandRepo = musicBandRepository;
            this.uow = unitOfWork;
            this.fileRepo = fileRepository;
        }

        /// <summary>
        /// Creates the Music Band
        /// </summary>
        /// <param name="key">The API Key</param>
        /// <param name="musicBandApiDto">The musicBandApiDto</param>
        /// <returns></returns>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        [Route("createmusicband/{key?}"), HttpPost]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> CreateMusicBand(string key, [FromBody] MusicBandApiDto musicBandApiDto)
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

                if (musicBandApiDto == null)
                {
                    throw new DomainException(Messages.IncorrectJsonStructure);
                }

                if (!musicBandApiDto.IsValid())
                {
                    validationResult.Add(musicBandApiDto.ValidationResult);
                    throw new DomainException(Messages.CorrectFormValues);
                }

                var cmd = new CreateMusicBand(
                    musicBandApiDto.MusicBandResponsibleApiDto,
                    musicBandApiDto.MusicBandDataApiDtos);

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
                    errors = validationResult?.Errors?.Select(e => new { e.Message })
                });
            }
            catch (JsonSerializationException ex)
            {
                return await Json(new
                {
                    status = ApiStatus.Error,
                    message = ex.GetInnerMessage(),
                    errors = validationResult?.Errors?.Select(e => new { e.Message })
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
        /// Get the Music Bands API filters
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("filters"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
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
                var targetAudiences = await this.targetAudiencesRepo.FindAllByProjectTypeIdAsync(ProjectType.Startup.Id);

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

        #region Commissions

        /// <summary>
        /// Get the Music Bands Commission Members
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("commissions"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Commissions([FromUri] MusicCommissionsApiRequest request)
        {
            #region Basic API Validations

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

            #endregion

            var collaboratorDtos = await this.collaboratorRepo.FindAllMusicCommissionMembersApiPaged(
                edition.Id,
                request?.Keywords,
                request?.Page ?? 1,
                request?.PageSize ?? 10);

            return await Json(new MusicCommissionsApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                HasPreviousPage = collaboratorDtos.HasPreviousPage,
                HasNextPage = collaboratorDtos.HasNextPage,
                TotalItemCount = collaboratorDtos.TotalItemCount,
                PageCount = collaboratorDtos.PageCount,
                PageNumber = collaboratorDtos.PageNumber,
                PageSize = collaboratorDtos.PageSize,
                Commissions = collaboratorDtos?.Select(c => new MusicCommissionListApiItem
                {
                    Uid = c.Uid,
                    Name = c.FullName?.Trim(),
                    Picture = c.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, c.Uid, c.ImageUploadDate, true, "_500x500") : null,
                    JobTitle = c.GetCollaboratorJobTitleBaseDtoByLanguageCode(requestLanguage?.Code ?? defaultLanguage?.Code)?.Value?.Trim(),
                    OrganizationsNames = c.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.Name ?? "-")?.ToString(", ")
                })?.ToList()
            });
        }

        /// <summary>
        /// Get the Music Bands Comission Member details
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("commissions/details/{uid?}"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> CommissionDetails([FromUri] MusicCommissionApiRequest request)
        {
            #region Basic API Validations

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

            #endregion

            var collaboratorDto = await this.collaboratorRepo.FindMusicCommissionMemberApi(
                request?.Uid ?? Guid.Empty,
                edition.Id);
            if (collaboratorDto == null)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00004", Message = "Commission Member not found." } });
            }

            return await Json(new MusicCommissionApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                Uid = collaboratorDto.Uid,
                Name = collaboratorDto.FullName,
                Picture = collaboratorDto.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, collaboratorDto.Uid, collaboratorDto.ImageUploadDate, true, "_500x500") : null,
                JobTitle = collaboratorDto.GetCollaboratorJobTitleBaseDtoByLanguageCode(requestLanguage?.Code ?? defaultLanguage?.Code)?.Value?.Trim(),
                OrganizationsNames = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.Name ?? "-")?.ToString(", "),
                MiniBio = collaboratorDto.GetMiniBioBaseDtoByLanguageCode(request.Culture ?? defaultLanguage.Code)?.Value?.Trim()
            });
        }

        #endregion

        /// <summary>
        /// Migrates database images to aws.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [NonAction]
        [Route("migrateimagestoaws"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
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
