// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-16-2020
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

        /// <summary>Initializes a new instance of the <see cref="PlayersApiController"/> class.</summary>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="interestRepositoryy">The interest repositoryy.</param>
        /// <param name="fileRepository">The file repository.</param>
        public PlayersApiController(
            IOrganizationRepository organizationRepository,
            IEditionRepository editionRepository,
            IActivityRepository activityRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IInterestRepository interestRepositoryy,
            IFileRepository fileRepository)
        {
            this.organizationRepo = organizationRepository;
            this.editionRepo = editionRepository;
            this.activityRepo = activityRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.interestRepo = interestRepositoryy;
            this.fileRepo = fileRepository;
        }

        #region List

        /// <summary>Playerses the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("players")]
        public async Task<IHttpActionResult> Players([FromUri]PlayersApiRequest request)
        {
            var editions = await this.editionRepo.FindAllByIsActiveAsync(false);
            if (editions?.Any() == false)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "No active editions found." }});
            }

            // Get edition from request otherwise get current
            var edition = request?.Edition.HasValue == true ? editions?.FirstOrDefault(e => e.UrlCode == request.Edition) : 
                                                              editions?.FirstOrDefault(e => e.IsCurrent);
            if (edition == null)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00002", Message = "No editions found." }});
            }

            var organizationsApiDtos = await this.organizationRepo.FindAllPublicApiPaged(
                edition.Id,
                request?.Keywords,
                OrganizationType.Player.Uid,
                request?.ActivitiesUids?.ToListGuid(','),
                request?.TargetAudiencesUids?.ToListGuid(','),
                request?.InterestsUids?.ToListGuid(','),
                request?.Page ?? 1, 
                request?.PageSize ?? 10);

            return await Json(new PlayersApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                HasPreviousPage = organizationsApiDtos.HasPreviousPage,
                HasNextPage = organizationsApiDtos.HasNextPage,
                TotalItemCount = organizationsApiDtos.TotalItemCount,
                PageCount = organizationsApiDtos.PageCount,
                PageNumber = organizationsApiDtos.PageNumber,
                PageSize = organizationsApiDtos.PageSize,
                Players = organizationsApiDtos?.Select(o => new PlayersApiListItem
                {
                    Uid = o.Uid,
                    Name = o.TradeName,
                    TradeName = o.TradeName,
                    CompanyName = o.CompanyName,
                    HighlightPosition = o.ApiHighlightPosition,
                    Picture = o.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, o.Uid, o.ImageUploadDate, true) : null
                })?.ToList()
            });
        }

        /// <summary>Filterses this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        [Route("players/filters")]
        public async Task<IHttpActionResult> Filters()
        {
            try
            {
                var activities = await this.activityRepo.FindAllAsync();
                var targetAudiences = await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Audiovisual.Id);
                var intrests = await this.interestRepo.FindAllGroupedByInterestGroupsAsync();

                return await Json(new PlayersFiltersApiResponse
                {
                    Status = ApiStatus.Success,
                    Error = null,
                    ActivityApiResponses = activities?.OrderBy(ta => ta.DisplayOrder)?.Select(ta => new ActivityApiResponse
                    {
                        Uid = ta.Uid,
                        Name = ta.Name
                    })?.ToList(),
                    TargetAudienceApiResponses = targetAudiences?.OrderBy(ta => ta.DisplayOrder)?.Select(ta => new TargetAudienceApiResponse
                    {
                        Uid = ta.Uid,
                        Name = ta.Name
                    })?.ToList(),
                    InterestGroupApiResponses = intrests?.OrderBy(i => i.Key.DisplayOrder)?.Select(intrest => new InterestGroupApiResponse
                    {
                        Uid = intrest.Key.Uid,
                        Name = intrest.Key.Name,
                        InterestsApiResponses = intrest?.OrderBy(i => i.DisplayOrder)?.Select(i => new InterestApiResponse
                        {
                            Uid = i.Uid,
                            Name = i.Name
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

        /// <summary>Players the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("player/{uid?}")]
        public async Task<IHttpActionResult> Player([FromUri]PlayerApiRequest request)
        {
            var editions = await this.editionRepo.FindAllByIsActiveAsync(false);
            if (editions?.Any() == false)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "No active editions found." }});
            }

            // Get edition from request otherwise get current
            var edition = request?.Edition.HasValue == true ? editions?.FirstOrDefault(e => e.UrlCode == request.Edition) :
                editions?.FirstOrDefault(e => e.IsCurrent);
            if (edition == null)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00002", Message = "No editions found." }});
            }

            var organizationApiDto = await this.organizationRepo.FindApiDtoByUidAsync(
                request?.Uid ?? Guid.Empty,
                edition.Id,
                OrganizationType.Player.Uid);
            if (organizationApiDto == null)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00003", Message = "Player not found." } });
            }

            var interestsGroups = organizationApiDto?.OrganizationInterestDtos?.GroupBy(oid => new
            {
                InterestGroupId = oid.InterestGroup.Id,
                InterestGroupUid = oid.InterestGroup.Uid,
                InterestGroupName = oid.InterestGroup.Name
            });

            return await Json(new PlayerApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                Uid = organizationApiDto.Uid,
                TradeName = organizationApiDto.TradeName,
                CompanyName = organizationApiDto.CompanyName,
                Picture = organizationApiDto.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, organizationApiDto.Uid, organizationApiDto.ImageUploadDate, true) : null,
                DescriptionsApiResponses = organizationApiDto.DescriptionsDtos?.Select(dd => new LanguageValueApiResponse
                {
                    Culture = dd.LanguageDto.Code,
                    Value = HttpUtility.HtmlDecode(dd.Value)
                })?.ToList(),
                InterestGroupApiResponses = interestsGroups?.Select(ig => new InterestGroupApiResponse
                {
                    Uid = ig.Key.InterestGroupUid,
                    Name = ig.Key.InterestGroupName,
                    InterestsApiResponses = ig.Select(i => new InterestApiResponse
                    {
                        Uid = i.Interest.Uid,
                        Name = i.Interest.Name
                    })?.ToList()
                })?.ToList(),
                CollaboratorsApiResponses = organizationApiDto.CollaboratorsDtos?.Select(cd => new PlayerCollaboratorApiResponse
                {
                    Uid = cd.Uid,
                    BadgeName = cd.Badge,
                    Name = cd.FullName,
                    Picture = cd.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, cd.Uid, cd.ImageUploadDate, true) : null,
                    JobTitlesApiResponses = cd.JobTitlesDtos?.Select(jtd => new LanguageValueApiResponse
                    {
                        Culture = jtd.LanguageDto.Code,
                        Value = HttpUtility.HtmlDecode(jtd.Value)
                    })?.ToList(),
                    MiniBiosApiResponses = cd.MiniBiosDtos?.Select(jtd => new LanguageValueApiResponse
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