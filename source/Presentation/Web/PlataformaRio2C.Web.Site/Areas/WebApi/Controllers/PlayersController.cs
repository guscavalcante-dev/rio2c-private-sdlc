// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-30-2019
// ***********************************************************************
// <copyright file="PlayersController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Web.Site.Areas.WebApi.Models;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>
    /// Class for sales platforms endpoints
    /// </summary>
    [System.Web.Http.RoutePrefix("api/v1.0")]
    public class PlayersController : BaseApiController
    {
        private readonly IOrganizationRepository organizationRepo;
        private readonly IEditionRepository editionRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>Initializes a new instance of the <see cref="PlayersController"/> class.</summary>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public PlayersController(
            IOrganizationRepository organizationRepository,
            IEditionRepository editionRepository,
            IFileRepository fileRepository)
        {
            this.organizationRepo = organizationRepository;
            this.editionRepo = editionRepository;
            this.fileRepo = fileRepository;
        }

        /// <summary>Playerses the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("players")]
        public async Task<IHttpActionResult> Players([FromUri]PlayersApiRequest request)
        {
            var editions = this.editionRepo.FindAllByIsActive(false);
            if (editions?.Any() == false)
            {
                return await Json(new ApiBaseResponse
                {
                    Status = ApiStatus.Error,
                    Error = new ApiError
                    {
                        Code = "00001",
                        Message = "No active editions found."
                    }
                });
            }

            // Get edition from request otherwise get current
            var edition = request?.Edition.HasValue == true ? editions?.FirstOrDefault(e => e.UrlCode == request.Edition) : 
                                                              editions?.FirstOrDefault(e => e.IsCurrent);
            if (edition == null)
            {
                return await Json(new ApiBaseResponse
                {
                    Status = ApiStatus.Error,
                    Error = new ApiError
                    {
                        Code = "00002",
                        Message = "No editions found."
                    }
                });
            }

            var organizations = await this.organizationRepo.FindAllPublicApiPaged(
                edition.Id,
                request?.Keywords,
                OrganizationType.Player.Uid, 
                request?.Page ?? 1, 
                request?.PageSize ?? 10);

            return await Json(new PlayersApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                HasPreviousPage = organizations.HasPreviousPage,
                HasNextPage = organizations.HasNextPage,
                TotalItemCount = organizations.TotalItemCount,
                PageCount = organizations.PageCount,
                PageNumber = organizations.PageNumber,
                PageSize = organizations.PageSize,
                Players = organizations?.Select(o => new PlayersApiListItem
                {
                    Id = o.Id,
                    Uid = o.Uid,
                    TradeName = o.TradeName,
                    CompanyName = o.CompanyName,
                    Picture = o.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, o.Uid, o.ImageUploadDate, true) : null
                })?.ToList()
            });
        }

        //[HttpGet]
        //[Route("player")]
        //public async Task<IHttpActionResult> Player([FromUri]PlayerApiRequest request)
        //{
        //    var editions = this.editionRepo.FindAllByIsActive(false);
        //    if (editions?.Any() == false)
        //    {
        //        return await Json(new PlayersApiResponse
        //        {
        //            Status = ApiStatus.Success,
        //            Error = new ApiError
        //            {
        //                Code = "00001",
        //                Message = "No active editions found."
        //            }
        //        });
        //    }
        //}
    }
}
