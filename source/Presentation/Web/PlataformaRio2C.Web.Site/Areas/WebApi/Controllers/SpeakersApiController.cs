// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-19-2019
// ***********************************************************************
// <copyright file="PlayersApiController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Web.Site.Areas.WebApi.Models;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>
    /// Class for speakers endpoints
    /// </summary>
    [System.Web.Http.RoutePrefix("api/v1.0")]
    public class SpeakersApiController : BaseApiController
    {
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IEditionRepository editionRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>Initializes a new instance of the <see cref="SpeakersApiController"/> class.</summary>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public SpeakersApiController(
            ICollaboratorRepository collaboratorRepository,
            IEditionRepository editionRepository,
            IFileRepository fileRepository)
        {
            this.collaboratorRepo = collaboratorRepository;
            this.editionRepo = editionRepository;
            this.fileRepo = fileRepository;
        }

        #region List

        /// <summary>Speakerses the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("speakers")]
        public async Task<IHttpActionResult> Speakers([FromUri]SpeakersApiRequest request)
        {
            var editions = this.editionRepo.FindAllByIsActive(false);
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

            var collaboratorsApiDtos = await this.collaboratorRepo.FindAllPublicApiPaged(
                edition.Id,
                request?.Keywords,
                Domain.Constants.CollaboratorType.Speaker,
                request?.Page ?? 1,
                request?.PageSize ?? 10);

            return await Json(new SpeakersApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                HasPreviousPage = collaboratorsApiDtos.HasPreviousPage,
                HasNextPage = collaboratorsApiDtos.HasNextPage,
                TotalItemCount = collaboratorsApiDtos.TotalItemCount,
                PageCount = collaboratorsApiDtos.PageCount,
                PageNumber = collaboratorsApiDtos.PageNumber,
                PageSize = collaboratorsApiDtos.PageSize,
                Speakers = collaboratorsApiDtos?.Select(c => new SpeakersApiListItem
                {
                    Uid = c.Uid,
                    BadgeName = c.BadgeName?.Trim(),
                    Name = c.Name?.Trim(),
                    MiniBio = c.GetCollaboratorMiniBioBaseDtoByLanguageCode(request?.Culture)?.Value?.Trim(),
                    JobTitle = c.GetCollaboratorJobTitleBaseDtoByLanguageCode(request?.Culture)?.Value?.Trim(),
                    HighlightPosition = c.ApiHighlightPosition,
                    Picture = c.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, c.Uid, c.ImageUploadDate, true) : null
                })?.ToList()
            });
        }

        ///// <summary>Filterses this instance.</summary>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("players/filters")]
        //public async Task<IHttpActionResult> Filters()
        //{
        //    try
        //    {
        //        var activities = await this.activityRepo.FindAllAsync();
        //        var targetAudiences = await this.targetAudienceRepo.FindAllAsync();
        //        var intrests = await this.interestRepo.FindAllGroupedByInterestGroupsAsync();

        //        return await Json(new PlayersFiltersApiResponse
        //        {
        //            Status = ApiStatus.Success,
        //            Error = null,
        //            ActivityApiResponses = activities?.OrderBy(ta => ta.DisplayOrder)?.Select(ta => new ActivityApiResponse
        //            {
        //                Uid = ta.Uid,
        //                Name = ta.Name
        //            })?.ToList(),
        //            TargetAudienceApiResponses = targetAudiences?.OrderBy(ta => ta.DisplayOrder)?.Select(ta => new TargetAudienceApiResponse
        //            {
        //                Uid = ta.Uid,
        //                Name = ta.Name
        //            })?.ToList(),
        //            InterestGroupApiResponses = intrests?.OrderBy(i => i.Key.DisplayOrder)?.Select(intrest => new InterestGroupApiResponse
        //            {
        //                Uid = intrest.Key.Uid,
        //                Name = intrest.Key.Name,
        //                InterestsApiResponses = intrest?.OrderBy(i => i.DisplayOrder)?.Select(i => new InterestApiResponse
        //                {
        //                    Uid = i.Uid,
        //                    Name = i.Name
        //                })?.ToList()
        //            })?.ToList()
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "Players filters api failed." } });
        //    }
        //}

        #endregion
    }
}