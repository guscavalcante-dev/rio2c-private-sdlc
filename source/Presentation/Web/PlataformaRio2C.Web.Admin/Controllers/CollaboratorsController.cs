// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 12-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-11-2020
// ***********************************************************************
// <copyright file="SpeakersController.cs" company="Softo">
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
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>SpeakersController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    public class CollaboratorsController : BaseController
    {
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>Initializes a new instance of the <see cref="SpeakersController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public CollaboratorsController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            IFileRepository fileRepository)
            : base(commandBus, identityController)
        {
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.fileRepo = fileRepository;
        }

        #region Finds

        /// <summary>Finds all by filters.</summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> FindAllByFilters(string keywords, int? page = 1)
        {
            var collaboratorsApiDtos = await this.attendeeCollaboratorRepo.FindAllDropdownApiListDtoPaged(
                this.EditionDto.Id,
                keywords,
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
                Collaborators = collaboratorsApiDtos?.Select(c => new CollaboratorsDropdownDto
                {
                    Uid = c.Uid,
                    BadgeName = c.BadgeName?.Trim(),
                    Name = c.Name?.Trim(),
                    Picture = c.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, c.CollaboratorUid, c.ImageUploadDate, true) : null,
                    JobTitle = c.GetCollaboratorJobTitleBaseDtoByLanguageCode(this.UserInterfaceLanguage)?.Value?.Trim(),
                    Companies = c.OrganizationsDtos?.Select(od => new CollaboratorsDropdownOrganizationDto()
                    {
                        Uid = od.Uid,
                        TradeName = od.TradeName,
                        CompanyName = od.CompanyName,
                        Picture = od.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, od.Uid, od.ImageUploadDate, true) : null
                    })?.ToList()
                })?.ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}