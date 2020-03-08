// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="ProducersController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Web.Admin.Controllers;
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Areas.Audiovisual.Controllers
{
    /// <summary>ProducersController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual)]
    public class ProducersController : BaseController
    {
        private readonly IOrganizationRepository organizationRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>Initializes a new instance of the <see cref="ProducersController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public ProducersController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            IOrganizationRepository organizationRepository,
            IFileRepository fileRepository)
            : base(commandBus, identityController)
        {
            this.organizationRepo = organizationRepository;
            this.fileRepo = fileRepository;
        }

        #region Finds

        /// <summary>Finds all by filters.</summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="filterByProjectsInNegotiation">The filter by projects in negotiation.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [HttpGet]
        //[AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.CuratorshipAudiovisual + "," + Constants.CollaboratorType.CommissionAudiovisual)]
        public async Task<ActionResult> FindAllByFilters(string keywords, bool? filterByProjectsInNegotiation = false, int? page = 1)
        {
            var collaboratorsApiDtos = await this.organizationRepo.FindAllDropdownApiListDtoPaged(
                this.EditionDto.Id,
                keywords,
                filterByProjectsInNegotiation.Value,
                OrganizationType.Producer.Uid,
                page.Value,
                10);

            return Json(new
            {
                status = "success",
                HasPreviousPage = collaboratorsApiDtos.HasPreviousPage,
                HasNextPage = collaboratorsApiDtos.HasNextPage,
                TotalItemCount = collaboratorsApiDtos.TotalItemCount,
                PageCount = collaboratorsApiDtos.PageCount,
                PageNumber = collaboratorsApiDtos.PageNumber,
                PageSize = collaboratorsApiDtos.PageSize,
                Organizations = collaboratorsApiDtos?.Select(c => new OrganizationDropdownDto
                {
                    Uid = c.Uid,
                    TradeName = c.TradeName,
                    CompanyName = c.CompanyName,
                    Picture = c.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, c.Uid, c.ImageUploadDate, true) : null
                })?.ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}