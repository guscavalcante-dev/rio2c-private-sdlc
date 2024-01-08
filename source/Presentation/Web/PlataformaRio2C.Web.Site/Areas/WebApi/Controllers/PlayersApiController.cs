// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Elton Assunção
// Last Modified On : 01-08-2024
// ***********************************************************************
// <copyright file="PlayersApiController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Domain.ApiModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using Swashbuckle.Swagger.Annotations;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>
    /// Class for players endpoints
    /// </summary>
    [System.Web.Http.RoutePrefix("api/v1.0")]
    public class PlayersApiController : BaseApiController
    {
        private readonly IOrganizationRepository organizationRepo;
        private readonly IEditionRepository editionRepo;
        private readonly IActivityRepository activityRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IInterestRepository interestRepo;
        private readonly IFileRepository fileRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersApiController" /> class.
        /// </summary>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="interestRepositoryy">The interest repositoryy.</param>
        /// <param name="fileRepository">The file repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public PlayersApiController(
            IOrganizationRepository organizationRepository,
            IEditionRepository editionRepository,
            IActivityRepository activityRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IInterestRepository interestRepositoryy,
            IFileRepository fileRepository,
            ILanguageRepository languageRepository)
        {
            this.organizationRepo = organizationRepository;
            this.editionRepo = editionRepository;
            this.activityRepo = activityRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.interestRepo = interestRepositoryy;
            this.fileRepo = fileRepository;
            this.languageRepo = languageRepository;
        }

        #region List

        /// <summary>
        /// Get the Players
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("players"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Players([FromUri] PlayersApiRequest request)
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

            var playerOrganizationApiDtos = await this.organizationRepo.FindAllPlayersPublicApiPaged(
                edition.Id,
                request?.Keywords,
                request?.ActivitiesUids?.ToListGuid(','),
                request?.TargetAudiencesUids?.ToListGuid(','),
                request?.InterestsUids?.ToListGuid(','),
                request?.ModifiedAfterDate.ToUtcDateKind(),
                request?.ShowDetails ?? false,
                request?.Page ?? 1, 
                request?.PageSize ?? 10);

            return await Json(new PlayersApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                HasPreviousPage = playerOrganizationApiDtos.HasPreviousPage,
                HasNextPage = playerOrganizationApiDtos.HasNextPage,
                TotalItemCount = playerOrganizationApiDtos.TotalItemCount,
                PageCount = playerOrganizationApiDtos.PageCount,
                PageNumber = playerOrganizationApiDtos.PageNumber,
                PageSize = playerOrganizationApiDtos.PageSize,
                Players = playerOrganizationApiDtos?.Select(dto => new PlayerApiResponse
                {
                    Uid = dto.Uid,
                    Name = dto.Name,
                    TradeName = dto.TradeName,
                    CompanyName = dto.CompanyName,
                    HighlightPosition = dto.ApiHighlightPosition,
                    Picture = dto.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, dto.Uid, dto.ImageUploadDate, true) : null,
                    DescriptionsApiResponses = dto.GetDescriptionsApiResponses(),
                    InterestGroupApiResponses = dto.GetInterestGroupApiResponses(),
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
        /// Get the Players API filters
        /// </summary>
        /// <returns></returns>
        [Route("players/filters"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Filters([FromUri] PlayersFiltersApiRequest request)
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

                var activities = await this.activityRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id);
                var targetAudiences = await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id);
                var intrests = await this.interestRepo.FindAllByProjectTypeIdAndGroupedByInterestGroupAsync(ProjectType.Audiovisual.Id);

                return await Json(new PlayersFiltersApiResponse
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

        #endregion

        #region Details

        /// <summary>
        /// Get the Player details
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("player/{uid?}"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Player([FromUri] PlayerApiRequest request)
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

            var playerOrganizationApiDto = await this.organizationRepo.FindPlayerPublicApiDtoByUid(
                request?.Uid ?? Guid.Empty,
                edition.Id);
            if (playerOrganizationApiDto == null)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00003", Message = "Player not found." } });
            }

            return await Json(new PlayerApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                Uid = playerOrganizationApiDto.Uid,
                TradeName = playerOrganizationApiDto.TradeName,
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
    }
}