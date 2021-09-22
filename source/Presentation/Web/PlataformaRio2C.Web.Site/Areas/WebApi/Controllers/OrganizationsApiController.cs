// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 10-14-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-16-2021
// ***********************************************************************
// <copyright file="OrganizationsApiController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Domain.ApiModels;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>
    /// OrganizationsApiController
    /// </summary>
    [System.Web.Http.RoutePrefix("api/v1.0")]
    [AjaxAuthorize(Order = 1)]
    public class OrganizationsApiController : BaseApiController
    {
        private readonly IOrganizationRepository organizationRepo;
        private readonly IEditionRepository editionRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationsApiController" /> class.
        /// </summary>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public OrganizationsApiController(
            IOrganizationRepository organizationRepository,
            IEditionRepository editionRepository,
            IFileRepository fileRepository)
        {
            this.organizationRepo = organizationRepository;
            this.editionRepo = editionRepository;
            this.fileRepo = fileRepository;
        }

        /// <summary>Organizationses the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("organizations")]
        public async Task<IHttpActionResult> Organizations([FromUri]OrganizationsApiRequest request)
        {
            //var editions = this.editionRepo.FindAllByIsActive(false);
            //if (editions?.Any() == false)
            //{
            //    return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "No active editions found." }});
            //}

            //// Get edition from request otherwise get current
            //var edition = request?.Edition.HasValue == true ? editions?.FirstOrDefault(e => e.UrlCode == request.Edition) : 
            //                                                  editions?.FirstOrDefault(e => e.IsCurrent);
            //if (edition == null)
            //{
            //    return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00002", Message = "No editions found." }});
            //}

            var organizationsApiDtos = await this.organizationRepo.FindAllOrganizationsApiPaged(
                null, // NOT BEING USED
                request?.CompanyName,
                request?.TradeName,
                request?.GetCompanyNumber(),
                Guid.Empty, //Required only on Web.Admin.OrganizationsApiController
                request?.Page ?? 1, 
                request?.PageSize ?? 10);

            return await Json(new OrganizationsApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                HasPreviousPage = organizationsApiDtos.HasPreviousPage,
                HasNextPage = organizationsApiDtos.HasNextPage,
                TotalItemCount = organizationsApiDtos.TotalItemCount,
                PageCount = organizationsApiDtos.PageCount,
                PageNumber = organizationsApiDtos.PageNumber,
                PageSize = organizationsApiDtos.PageSize,
                Organizations = organizationsApiDtos?.Select(o => new OrganizationsApiListItem
                {
                    Uid = o.Uid,
                    TradeName = o.TradeName,
                    CompanyName = o.CompanyName,
                    CompanyNumber = o.Document,
                    Picture = o.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, o.Uid, o.ImageUploadDate, true) : null
                })?.ToList()
            });
        }

        ///// <summary>Organizations the specified request.</summary>
        ///// <param name="request">The request.</param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("organization/{uid?}")]
        //public async Task<IHttpActionResult> Organization([FromUri]PlayerApiRequest request)
        //{
        //    var editions = this.editionRepo.FindAllByIsActive(false);
        //    if (editions?.Any() == false)
        //    {
        //        return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "No active editions found." }});
        //    }

        //    // Get edition from request otherwise get current
        //    var edition = request?.Edition.HasValue == true ? editions?.FirstOrDefault(e => e.UrlCode == request.Edition) :
        //        editions?.FirstOrDefault(e => e.IsCurrent);
        //    if (edition == null)
        //    {
        //        return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00002", Message = "No editions found." }});
        //    }

        //    var organizationApiDto = await this.organizationRepo.FindApiDtoByUidAsync(
        //        request?.Uid ?? Guid.Empty,
        //        edition.Id,
        //        OrganizationType.Player.Uid);
        //    if (organizationApiDto == null)
        //    {
        //        return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00003", Message = "Player not found." } });
        //    }

        //    var interestsGroups = organizationApiDto?.OrganizationInterestsDtos?.GroupBy(oid => new { oid.InterestGroupId, oid.InterestGroupUid, oid.InterestGroupName });

        //    return await Json(new PlayerApiResponse
        //    {
        //        Status = ApiStatus.Success,
        //        Error = null,
        //        Uid = organizationApiDto.Uid,
        //        TradeName = organizationApiDto.TradeName,
        //        CompanyName = organizationApiDto.CompanyName,
        //        Picture = organizationApiDto.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, organizationApiDto.Uid, organizationApiDto.ImageUploadDate, true) : null,
        //        DescriptionsApiResponses = organizationApiDto.DescriptionsDtos?.Select(dd => new LanguageValueApiResponse
        //        {
        //            Culture = dd.LanguageDto.Code,
        //            Value = dd.Value
        //        })?.ToList(),
        //        InterestGroupApiResponses = interestsGroups?.Select(ig => new InterestGroupApiResponse
        //        {
        //            Uid = ig.Key.InterestGroupUid,
        //            Name = ig.Key.InterestGroupName,
        //            InterestsApiResponses = ig.Select(i => new InterestApiResponse
        //            {
        //                Name = i.InterestName
        //            })?.ToList()
        //        })?.ToList(),
        //        CollaboratorsApiResponses = organizationApiDto.CollaboratorsDtos?.Select(cd => new CollaboratorApiResponse
        //        {
        //            Uid = cd.Uid,
        //            Name = cd.FullName,
        //            Picture = cd.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, cd.Uid, cd.ImageUploadDate, true) : null,
        //            JobTitlesApiResponses = cd.JobTitlesDtos?.Select(jtd => new LanguageValueApiResponse
        //            {
        //                Culture = jtd.LanguageDto.Code,
        //                Value = jtd.Value
        //            })?.ToList(),
        //            MiniBiosApiResponses = cd.MiniBiosDtos?.Select(jtd => new LanguageValueApiResponse
        //            {
        //                Culture = jtd.LanguageDto.Code,
        //                Value = jtd.Value
        //            })?.ToList()
        //        })?.ToList()
        //    });
        //}
    }
}