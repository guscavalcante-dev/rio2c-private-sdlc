// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 06-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-01-2025
// ***********************************************************************
// <copyright file="InnovationApiController.cs" company="Softo">
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
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>
    /// InnovationApiController
    /// </summary>
    [System.Web.Http.RoutePrefix("api/v1.0/innovation")]
    public class InnovationApiController : BaseApiController
    {
        private readonly IMediator commandBus;
        private readonly IdentityAutenticationService identityController;
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly IWorkDedicationRepository workDedicationRepo;
        private readonly IInnovationOrganizationExperienceOptionRepository innovationOrganizationExperienceOptionRepo;
        private readonly IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepo;
        private readonly IInnovationOrganizationTechnologyOptionRepository innovationOrganizationTechnologyOptionRepo;
        private readonly IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepo;
        private readonly IInnovationOrganizationSustainableDevelopmentObjectivesOptionRepository innovationOrganizationSustainableDevelopmentObjectivesOptionRepo;
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IFileRepository fileRepo;
        private readonly IActivityRepository activityRepo;
        private readonly IInterestRepository interestRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IOrganizationRepository organizationRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationApiController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="workDedicationRepository">The work dedication repository.</param>
        /// <param name="innovationOrganizationExperienceOptionRepository">The innovation organization experience option repository.</param>
        /// <param name="innovationOrganizationTrackOptionRepository">The innovation organization track option repository.</param>
        /// <param name="innovationOrganizationTechnologyOptionRepository">The innovation organization technology option repository.</param>
        /// <param name="innovationOrganizationObjectivesOptionRepository">The innovation organization objectives option repository.</param>
        /// <param name="innovationOrganizationSustainableDevelopmentObjectivesOptionRepository">The innovation organization sustainable development objectives option repository.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        /// <param name="targetAudienceRepo">The target audience repo.</param>
        /// <param name="organizationRepository">The organization repo.</param>
        public InnovationApiController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository,
            IWorkDedicationRepository workDedicationRepository,
            IInnovationOrganizationExperienceOptionRepository innovationOrganizationExperienceOptionRepository,
            IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepository,
            IInnovationOrganizationTechnologyOptionRepository innovationOrganizationTechnologyOptionRepository,
            IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepository,
            IInnovationOrganizationSustainableDevelopmentObjectivesOptionRepository innovationOrganizationSustainableDevelopmentObjectivesOptionRepository,
            ICollaboratorRepository collaboratorRepository,
            IFileRepository fileRepository,
            IActivityRepository activityRepository,
            IInterestRepository interestRepository,
            ITargetAudienceRepository targetAudienceRepo,
            IOrganizationRepository organizationRepository)
        {
            this.commandBus = commandBus;
            this.identityController = identityController;
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
            this.workDedicationRepo = workDedicationRepository;
            this.innovationOrganizationExperienceOptionRepo = innovationOrganizationExperienceOptionRepository;
            this.innovationOrganizationTrackOptionRepo = innovationOrganizationTrackOptionRepository;
            this.innovationOrganizationTechnologyOptionRepo = innovationOrganizationTechnologyOptionRepository;
            this.innovationOrganizationObjectivesOptionRepo = innovationOrganizationObjectivesOptionRepository;
            this.innovationOrganizationSustainableDevelopmentObjectivesOptionRepo = innovationOrganizationSustainableDevelopmentObjectivesOptionRepository;
            this.collaboratorRepo = collaboratorRepository;
            this.fileRepo = fileRepository;
            this.activityRepo = activityRepository;
            this.interestRepo = interestRepository;
            this.targetAudienceRepo = targetAudienceRepo;
            this.organizationRepo = organizationRepository;
        }

        /// <summary>
        /// Creates the Startup
        /// </summary>
        /// <param name="key">The API Key</param>
        /// <param name="innovationOrganizationApiDto">The innovationOrganizationApiDto</param>
        /// <returns></returns>
        [Route("create-startup/{key?}"), HttpPost]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> CreateStartup(string key, [FromBody] InnovationOrganizationApiDto innovationOrganizationApiDto)
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

                var currentEdition = await editionRepo.FindByIsCurrentAsync();
                if (currentEdition == null)
                {
                    throw new DomainException(Messages.CurrentEditionNotFound);
                }

                #endregion

                if (innovationOrganizationApiDto == null)
                {
                    throw new DomainException(Messages.IncorrectJsonStructure);
                }

                if (!innovationOrganizationApiDto.IsValid())
                {
                    validationResult.Add(innovationOrganizationApiDto.ValidationResult);
                    throw new DomainException(Messages.CorrectFormValues);
                }

                var cmd = new CreateInnovationOrganization(
                    innovationOrganizationApiDto.Name,
                    innovationOrganizationApiDto.Document,
                    innovationOrganizationApiDto.ServiceName,
                    DateTime.Now,
                    innovationOrganizationApiDto.AccumulatedRevenue,
                    innovationOrganizationApiDto.Description,
                    "",
                    innovationOrganizationApiDto.Website,
                    innovationOrganizationApiDto.BusinessFocus,
                    innovationOrganizationApiDto.MarketSize,
                    innovationOrganizationApiDto.BusinessEconomicModel,
                    innovationOrganizationApiDto.BusinessOperationalModel,
                    innovationOrganizationApiDto.ImageFile,
                    "",
                    innovationOrganizationApiDto.BusinessDifferentials,
                    innovationOrganizationApiDto.BusinessStage,
                    innovationOrganizationApiDto.ResponsibleName,
                    innovationOrganizationApiDto.Email,
                    innovationOrganizationApiDto.CellPhone,
                    innovationOrganizationApiDto.PresentationFile,
                    innovationOrganizationApiDto.PresentationFileName,
                    innovationOrganizationApiDto.AttendeeInnovationOrganizationFounderApiDtos,
                    innovationOrganizationApiDto.AttendeeInnovationOrganizationCompetitorApiDtos,
                    innovationOrganizationApiDto.InnovationOrganizationExperienceOptionApiDtos,
                    innovationOrganizationApiDto.InnovationOrganizationTrackOptionApiDtos,
                    innovationOrganizationApiDto.InnovationOrganizationObjectivesOptionApiDtos,
                    innovationOrganizationApiDto.InnovationOrganizationTechnologyOptionApiDtos,
                    innovationOrganizationApiDto.InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos,
                    innovationOrganizationApiDto.WouldYouLikeParticipateBusinessRound,
                    innovationOrganizationApiDto.WouldYouLikeParticipatePitching,
                    innovationOrganizationApiDto.AccumulatedRevenueForLastTwelveMonths,
                    innovationOrganizationApiDto.BusinessFoundationYear
                    );

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
                    message = $"{ex.Message}{(ex.InnerException != null ? " - " + ex.GetInnerMessage() : "")}",
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

            return await Json(new { status = ApiStatus.Success, message = string.Format(Messages.EntityActionSuccessfull, Labels.Startup, Labels.CreatedF) });
        }

        /// <summary>
        /// Get the create-startup API filters
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("filters"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Filters([FromUri] InnovationFiltersApiRequest request)
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
                string currentLanguageCode = requestLanguage?.Code ?? defaultLanguage?.Code;

                #endregion

                var innovationOrganizationExperienceOptions = await this.innovationOrganizationExperienceOptionRepo.FindAllAsync();
                var innovationOrganizationTechnologyOptions = await this.innovationOrganizationTechnologyOptionRepo.FindAllAsync();
                var innovationOrganizationObjectivesOptions = await this.innovationOrganizationObjectivesOptionRepo.FindAllAsync();
                var innovationOrganizationSustainableDevelopmentObjectivesOptions = new List<InnovationOrganizationSustainableDevelopmentObjectivesOption>(); //await this.innovationOrganizationSustainableDevelopmentObjectivesOptionRepo.FindAllAsync();
                var innovationOrganizationTrackOptions = await this.innovationOrganizationTrackOptionRepo.FindAllAsync();
                var workDedications = await this.workDedicationRepo.FindAllAsync();

                return await Json(new InnovationFiltersApiResponse
                {
                    InnovationOrganizationExperienceOptions = innovationOrganizationExperienceOptions.Select(ioeo => new ApiListItemBaseResponse()
                    {
                        Uid = ioeo.Uid,
                        Name = ioeo.GetNameTranslation(currentLanguageCode)
                    })?.ToList(),

                    InnovationOrganizationTechnologyOptions = innovationOrganizationTechnologyOptions.Select(ioto => new ApiListItemBaseResponse()
                    {
                        Uid = ioto.Uid,
                        Name = ioto.GetNameTranslation(currentLanguageCode)
                    })?.ToList(),

                    InnovationOrganizationObjectivesOptions = innovationOrganizationObjectivesOptions.Select(iooo => new ApiListItemBaseResponse()
                    {
                        Uid = iooo.Uid,
                        Name = iooo.GetNameTranslation(currentLanguageCode)
                    })?.ToList(),

                    InnovationOrganizationSustainableDevelopmentObjectivesOptions = innovationOrganizationSustainableDevelopmentObjectivesOptions.Select(iooo => new InnovationOrganizationSustainableDevelopmentObjectivesOptionListItemApiResponse()
                    {
                        Uid = iooo.Uid,
                        Name = iooo.GetNameTranslation(currentLanguageCode),
                        Description = iooo.GetDesctiptionTranslation(currentLanguageCode),
                        DisplayOrder = iooo.DisplayOrder
                    })?.ToList(),

                    InnovationOrganizationTrackOptions = innovationOrganizationTrackOptions.Select(ioto => new InnovationOrganizationTrackOptionListItemApiResponse()
                    {
                        Uid = ioto.Uid,
                        Name = ioto.GetNameTranslation(currentLanguageCode),
                        Description = ioto.GetDesctiptionTranslation(currentLanguageCode),
                        GroupUid = ioto.InnovationOrganizationTrackOptionGroup?.Uid.ToString(),
                        GroupName = ioto.GetInnovationOrganizationTrackOptionGroupNameTranslation(currentLanguageCode)
                    })?.ToList(),

                    WorkDedications = workDedications.Select(wd => new ApiListItemBaseResponse()
                    {
                        Uid = wd.Uid,
                        Name = wd.GetNameTranslation(currentLanguageCode)
                    })?.ToList(),

                    Status = ApiStatus.Success
                });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00004", Message = "Innovation filters api failed." } });
            }
        }

        #region Players

        /// <summary>
        /// Get the Innovation Players
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("players"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Players([FromUri] InnovationPlayersApiRequest request)
        {
            #region Initial Validations

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

            #endregion

            var playerOrganizationApiDtos = await this.organizationRepo.FindAllInnovationPlayersPublicApiPaged(
                edition.Id,
                request?.Keywords,
                request?.ActivitiesUids?.ToListGuid(','),
                request?.TargetAudiencesUids?.ToListGuid(','),
                request?.InterestsUids?.ToListGuid(','),
                request?.ModifiedAfterDate.ToUtcDateKind(),
                request?.ShowDetails ?? false,
                request?.ShowDeleted ?? false,
                request?.Page ?? 1,
                request?.PageSize ?? 10);

            return await Json(new InnovationPlayersApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                HasPreviousPage = playerOrganizationApiDtos.HasPreviousPage,
                HasNextPage = playerOrganizationApiDtos.HasNextPage,
                TotalItemCount = playerOrganizationApiDtos.TotalItemCount,
                PageCount = playerOrganizationApiDtos.PageCount,
                PageNumber = playerOrganizationApiDtos.PageNumber,
                PageSize = playerOrganizationApiDtos.PageSize,
                Players = playerOrganizationApiDtos?.Select(dto => new InnovationPlayerApiResponse
                {
                    Uid = dto.Uid,
                    TradeName = !string.IsNullOrEmpty(dto.TradeName) ? dto.TradeName : dto.Name,
                    CompanyName = dto.CompanyName,
                    HighlightPosition = dto.ApiHighlightPosition,
                    Picture = dto.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, dto.Uid, dto.ImageUploadDate, true) : null,
                    DescriptionsApiResponses = dto.GetDescriptionsApiResponses(),
                    InterestGroupApiResponses = dto.GetInterestGroupApiResponses(),
                    IsDeleted = dto.IsDeleted,
                    PlayerCollaboratorApiResponses = dto.CollaboratorsDtos?.Select(cd => new PlayerCollaboratorApiResponse
                    {
                        Uid = cd.Uid,
                        BadgeName = cd.Badge,
                        Name = cd.FullName,
                        Picture = cd.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, cd.Uid, cd.ImageUploadDate, true) : null,
                        JobTitlesApiResponses = cd.JobTitleBaseDtos?.Select(jtd => new LanguageValueApiResponse
                        {
                            Culture = jtd.LanguageDto.Code,
                            Value = HttpUtility.HtmlDecode(jtd.Value)
                        })?.ToList(),
                        MiniBiosApiResponses = cd.MiniBioBaseDtos?.Select(jtd => new LanguageValueApiResponse
                        {
                            Culture = jtd.LanguageDto.Code,
                            Value = HttpUtility.HtmlDecode(jtd.Value)
                        })?.ToList()
                    })
                })?.ToList()
            });
        }

        /// <summary>
        /// Get the Innovation Players API filters
        /// </summary>
        /// <returns></returns>
        [Route("players/filters"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> PlayersFilters([FromUri] InnovationPlayersFiltersApiRequest request)
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

                var activities = await this.activityRepo.FindAllByProjectTypeIdAsync(ProjectType.Startup.Id);
                var targetAudiences = await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Startup.Id);
                var intrests = await this.interestRepo.FindAllByProjectTypeIdAndGroupedByInterestGroupAsync(ProjectType.Startup.Id);

                return await Json(new InnovationPlayersFiltersApiResponse
                {
                    Status = ApiStatus.Success,
                    Error = null,
                    ActivityApiResponses = activities?.OrderBy(ta => ta.DisplayOrder)?.Select(ta => new ActivityApiResponse
                    {
                        Uid = ta.Uid,
                        Name = ta.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
                    })?.ToList(),
                    TargetAudienceApiResponses = targetAudiences?.OrderBy(ta => ta.DisplayOrder)?.Select(ta => new TargetAudienceApiResponse
                    {
                        Uid = ta.Uid,
                        Name = ta.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
                    })?.ToList(),
                    InterestGroupApiResponses = intrests?.OrderBy(i => i.Key.DisplayOrder)?.Select(intrest => new InterestGroupApiResponse
                    {
                        Uid = intrest.Key.Uid,
                        Name = intrest.Key.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code),
                        InterestsApiResponses = intrest?.OrderBy(i => i.DisplayOrder)?.Select(i => new InterestApiResponse
                        {
                            Uid = i.Uid,
                            Name = i.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
                        })?.ToList()
                    })?.ToList()
                });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "Players filters api failed." } });
            }
        }

        /// <summary>
        /// Get the Innovation Player details
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("players/details/{uid?}"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> PlayerDetails([FromUri] InnovationPlayerApiRequest request)
        {
            #region Initial Validations

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

            #endregion

            var playerOrganizationApiDto = await this.organizationRepo.FindInnovationPlayerPublicApiDtoByUid(
                request?.Uid ?? Guid.Empty,
                edition.Id);
            if (playerOrganizationApiDto == null)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00003", Message = "Player not found." } });
            }

            return await Json(new InnovationPlayerApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                Uid = playerOrganizationApiDto.Uid,
                TradeName = !string.IsNullOrEmpty(playerOrganizationApiDto.TradeName) ? playerOrganizationApiDto.TradeName : playerOrganizationApiDto.Name,
                CompanyName = playerOrganizationApiDto.CompanyName,
                Picture = playerOrganizationApiDto.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, playerOrganizationApiDto.Uid, playerOrganizationApiDto.ImageUploadDate, true) : null,
                DescriptionsApiResponses = playerOrganizationApiDto.OrganizationDescriptionBaseDtos?.Select(dd => new LanguageValueApiResponse
                {
                    Culture = dd.LanguageDto.Code,
                    Value = HttpUtility.HtmlDecode(dd.Value)
                })?.ToList(),
                InterestGroupApiResponses = playerOrganizationApiDto.GetInterestGroupApiResponses(),
                PlayerCollaboratorApiResponses = playerOrganizationApiDto.CollaboratorsDtos?.Select(cd => new PlayerCollaboratorApiResponse
                {
                    Uid = cd.Uid,
                    BadgeName = cd.Badge,
                    Name = cd.FullName,
                    Picture = cd.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, cd.Uid, cd.ImageUploadDate, true) : null,
                    JobTitlesApiResponses = cd.JobTitleBaseDtos?.Select(jtd => new LanguageValueApiResponse
                    {
                        Culture = jtd.LanguageDto.Code,
                        Value = HttpUtility.HtmlDecode(jtd.Value)
                    })?.ToList(),
                    MiniBiosApiResponses = cd.MiniBioBaseDtos?.Select(jtd => new LanguageValueApiResponse
                    {
                        Culture = jtd.LanguageDto.Code,
                        Value = HttpUtility.HtmlDecode(jtd.Value)
                    })?.ToList()
                })?.ToList()
            });
        }

        #endregion

        #region Players Executives (Disabled by Rio2C customer's request) 

        /// <summary>
        /// Get the Startup Players Executives
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("players/executives"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IHttpActionResult> PlayersExecutives([FromUri] InnovationPlayersExecutivesApiRequest request)
        {
            #region Initial Validations

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

            #endregion

            var playerOrganizationApiDtos = await this.collaboratorRepo.FindAllInnovationPlayersExecutivesPublicApiPaged(
                edition.Id,
                request?.Keywords,
                request?.ActivitiesUids?.ToListGuid(','),
                request?.TargetAudiencesUids?.ToListGuid(','),
                request?.InterestsUids?.ToListGuid(','),
                request?.ModifiedAfterDate.ToUtcDateKind(),
                request?.ShowDetails ?? false,
                request?.ShowDeleted ?? false,
                request?.Page ?? 1,
                request?.PageSize ?? 10);

            return await Json(new InnovationPlayersExecutivesApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                HasPreviousPage = playerOrganizationApiDtos.HasPreviousPage,
                HasNextPage = playerOrganizationApiDtos.HasNextPage,
                TotalItemCount = playerOrganizationApiDtos.TotalItemCount,
                PageCount = playerOrganizationApiDtos.PageCount,
                PageNumber = playerOrganizationApiDtos.PageNumber,
                PageSize = playerOrganizationApiDtos.PageSize,
                PlayersExecutives = playerOrganizationApiDtos?.Select(dto => new InnovationPlayerExecutiveApiResponse
                {
                    Uid = dto.Uid,
                    BadgeName = dto.Badge,
                    Name = dto.FullName,
                    IsDeleted = dto.IsDeleted,
                    HighlightPosition = dto.ApiHighlightPosition,
                    Picture = dto.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, dto.Uid, dto.ImageUploadDate, true) : null,
                    JobTitlesApiResponses = dto.JobTitleBaseDtos?.Select(jtd => new LanguageValueApiResponse
                    {
                        Culture = jtd.LanguageDto.Code,
                        Value = HttpUtility.HtmlDecode(jtd.Value)
                    })?.ToList(),
                    MiniBiosApiResponses = dto.MiniBioBaseDtos?.Select(jtd => new LanguageValueApiResponse
                    {
                        Culture = jtd.LanguageDto.Code,
                        Value = HttpUtility.HtmlDecode(jtd.Value)
                    })?.ToList()
                })?.ToList()
            });
        }

        /// <summary>
        /// Get the Startup Players Executives API filters
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("players/executives/filters"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IHttpActionResult> PlayersExecutivesFilters([FromUri] InnovationPlayersExecutivesFiltersApiRequest request)
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

                var activities = await this.activityRepo.FindAllByProjectTypeIdAsync(ProjectType.Startup.Id);
                var targetAudiences = await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Startup.Id);
                var interests = await this.interestRepo.FindAllByProjectTypeIdAndGroupedByInterestGroupAsync(ProjectType.Startup.Id);

                return await Json(new InnovationPlayersExecutivesFiltersApiResponse
                {
                    Status = ApiStatus.Success,
                    Error = null,
                    ActivityApiResponses = activities?.OrderBy(ta => ta.DisplayOrder)?.Select(ta => new ActivityApiResponse
                    {
                        Uid = ta.Uid,
                        Name = ta.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
                    })?.ToList(),
                    TargetAudienceApiResponses = targetAudiences?.OrderBy(ta => ta.DisplayOrder)?.Select(ta => new TargetAudienceApiResponse
                    {
                        Uid = ta.Uid,
                        Name = ta.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
                    })?.ToList(),
                    InterestGroupApiResponses = interests?.OrderBy(i => i.Key.DisplayOrder)?.Select(intrest => new InterestGroupApiResponse
                    {
                        Uid = intrest.Key.Uid,
                        Name = intrest.Key.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code),
                        InterestsApiResponses = intrest?.OrderBy(i => i.DisplayOrder)?.Select(i => new InterestApiResponse
                        {
                            Uid = i.Uid,
                            Name = i.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
                        })?.ToList()
                    })?.ToList()
                });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "Players filters api failed." } });
            }
        }

        /// <summary>
        /// Get the Startup Player Executive details
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("players/executives/details/{uid?}"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IHttpActionResult> PlayerExecutiveDetails([FromUri] InnovationPlayerExecutiveApiRequest request)
        {
            #region Initial Validations

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

            #endregion

            var innovationPlayerCollaboratorApiDto = await this.collaboratorRepo.FindInnovationPlayerExecutivePublicApiDtoByUid(
                request?.Uid ?? Guid.Empty,
                edition.Id);
            if (innovationPlayerCollaboratorApiDto == null)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00003", Message = "Player Executive not found." } });
            }

            return await Json(new InnovationPlayerExecutiveApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                Uid = innovationPlayerCollaboratorApiDto.Uid,
                BadgeName = innovationPlayerCollaboratorApiDto.Badge,
                Name = innovationPlayerCollaboratorApiDto.FullName,
                IsDeleted = innovationPlayerCollaboratorApiDto.IsDeleted,
                HighlightPosition = innovationPlayerCollaboratorApiDto.ApiHighlightPosition,
                Picture = innovationPlayerCollaboratorApiDto.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, innovationPlayerCollaboratorApiDto.Uid, innovationPlayerCollaboratorApiDto.ImageUploadDate, true) : null,
                JobTitlesApiResponses = innovationPlayerCollaboratorApiDto.JobTitleBaseDtos?.Select(jtd => new LanguageValueApiResponse
                {
                    Culture = jtd.LanguageDto.Code,
                    Value = HttpUtility.HtmlDecode(jtd.Value)
                })?.ToList(),
                MiniBiosApiResponses = innovationPlayerCollaboratorApiDto.MiniBioBaseDtos?.Select(jtd => new LanguageValueApiResponse
                {
                    Culture = jtd.LanguageDto.Code,
                    Value = HttpUtility.HtmlDecode(jtd.Value)
                })?.ToList()
            });
        }

        #endregion
    }
}
