// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 12-19-2019
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
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Areas.WebApi.Controllers
{
    /// <summary>
    /// OrganizationsApiController
    /// </summary>
    [System.Web.Http.RoutePrefix("api/v1.0")]
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    public class OrganizationsApiController : BaseApiController
    {
        private readonly IOrganizationRepository organizationRepo;
        private readonly IEditionRepository editionRepo;
        private readonly IFileRepository fileRepo;
        private readonly IOrganizationTypeRepository organizationTypeRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationsApiController" /> class.
        /// </summary>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        /// <param name="organizationTypeRepository">The organization type repository.</param>
        public OrganizationsApiController(
            IOrganizationRepository organizationRepository,
            IEditionRepository editionRepository,
            IFileRepository fileRepository,
            IOrganizationTypeRepository organizationTypeRepository)
        {
            this.organizationRepo = organizationRepository;
            this.editionRepo = editionRepository;
            this.fileRepo = fileRepository;
            this.organizationTypeRepo = organizationTypeRepository;
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

            var organizationType = await this.organizationTypeRepo.FindByNameAsync(request?.OrganizationTypeName ?? string.Empty);

            var organizationsApiDtos = await this.organizationRepo.FindAllOrganizationsApiPaged(
                null, // NOT BEING USED
                request?.CompanyName,
                request?.TradeName,
                request?.GetCompanyNumber(),
                organizationType?.Uid ?? Guid.Empty,
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
    }
}