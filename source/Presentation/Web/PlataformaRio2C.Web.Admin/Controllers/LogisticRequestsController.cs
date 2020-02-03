// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-20-2020
// ***********************************************************************
// <copyright file="LogisticSponsorsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MediatR;
using Newtonsoft.Json;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Filters;
using Constants = PlataformaRio2C.Domain.Constants;
using PagedListExtensions = X.PagedList.PagedListExtensions;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>LogisticSponsorsController</summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.CuratorshipAudiovisual)]
    public class LogisticRequestsController : BaseController
    {
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IAttendeeSalesPlatformTicketTypeRepository attendeeSalesPlatformTicketTypeRepo;
        private readonly IFileRepository fileRepo;

        private readonly ILogisticSponsorRepository logisticSponsorRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="SpeakersController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="attendeeSalesPlatformTicketTypeRepository">The attendee sales platform ticket type repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public LogisticRequestsController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            ILogisticSponsorRepository logisticSponsorRepo,
            ILanguageRepository languageRepo)
            : base(commandBus, identityController)
        {
            this.logisticSponsorRepo = logisticSponsorRepo;
            this.languageRepo = languageRepo;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(LogisticRequestSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Logistics, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Requests, Url.Action("Index", "LogisticRequests", new { Area = "" }))
            });

            #endregion

            return View(searchViewModel);
        }

        #region DataTable Widget

        /// <summary>Searches the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showHighlights">if set to <c>true</c> [show highlights].</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllEditions)
        {
            //var sponsors = await this.logisticSponsorRepo.FindAllByDataTable(
            //    request.Start / request.Length,
            //    request.Length,
            //    request.Search?.Value,
            //    request.GetSortColumns(),
            //    showAllEditions,
            //    this.EditionDto?.Id);

            //foreach (var item in sponsors){
            //    item.Name = item.Name.GetSeparatorTranslation(this.UserInterfaceLanguage, '|');
            //}

            var list = new List<LogisticRequestBaseDto>()
            {
                new LogisticRequestBaseDto()
                {
                    Name = "Participante",
                    AccommodationSponsor = "Próprio",
                    AirfareSponsor = "Rio2C",
                    TransferSponsor = "APEX",
                    IsSponsoredByEvent = true,
                    HasSponsors = true
                },
                new LogisticRequestBaseDto()
                {
                    Name = "Participante",
                    AccommodationSponsor = "Patrocinado por outras instituições",
                    AirfareSponsor = null,
                    TransferSponsor = null,
                    IsSponsoredByEvent = false,
                    HasSponsors = true
                },
                new LogisticRequestBaseDto()
                {
                    Name = "Participante",
                    AccommodationSponsor = null,
                    AirfareSponsor = null,
                    TransferSponsor = null,
                    IsSponsoredByEvent = false,
                    HasSponsors = false
                },
                new LogisticRequestBaseDto()
                {
                    Name = "Participante",
                    AccommodationSponsor = "Patrocinado por outras instituições",
                    AirfareSponsor = "Rio2C",
                    TransferSponsor = "APEX",
                    IsSponsoredByEvent = true,
                    HasSponsors = true
                },
                new LogisticRequestBaseDto()
                {
                    Name = "Participante",
                    AccommodationSponsor = "Patrocinado por outras instituições",
                    AirfareSponsor = "Rio2C",
                    TransferSponsor = "APEX",
                    IsSponsoredByEvent = true,
                    HasSponsors = true
                },
                new LogisticRequestBaseDto()
                {
                    Name = "Participante",
                    AccommodationSponsor = "Próprio | Own",
                    AirfareSponsor = "Rio2C",
                    TransferSponsor = "APEX",
                    IsSponsoredByEvent = true,
                    HasSponsors = true
                },
            };

            var response = DataTablesResponse.Create(request, 100, 100, list);
            
            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion
                       
        #region Create

        /// <summary>Shows the create modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateModal()
        {
            var cmd = new CreateLogisticRequest();

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the specified collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(CreateLogisticSponsors cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    this.AdminAccessControlDto.User.Id,
                    this.AdminAccessControlDto.User.Uid,
                    this.EditionDto.Id,
                    this.EditionDto.Uid,
                    this.UserInterfaceLanguage);

                result = await this.CommandBus.Send(cmd);
                if (!result.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }
            }
            catch (DomainException ex)
            {
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }

                return Json(new
                {
                    status = "error",
                    message = ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_TinyForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.CreatedM) });
        }

        #endregion

        #region Update

        /// <summary>Shows the update modal.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateModal(Guid? sponsorUid, bool? isAddingToCurrentEdition)
        {
            UpdateLogisticSponsors cmd;

            try
            {
                cmd = new UpdateLogisticSponsors(
                    await this.CommandBus.Send(new FindLogisticSponsorDtoByUid(sponsorUid, this.UserInterfaceLanguage)),
                    await languageRepo.FindAllDtosAsync(),
                    UserInterfaceLanguage,
                    isAddingToCurrentEdition);
            }
            catch (DomainException ex)
            {
                return Json(new { status = "error", message = ex.GetInnerMessage() }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                { 
                    new { page = this.RenderRazorViewToString("Modals/UpdateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Updates the specified tiny collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Update(UpdateLogisticSponsors cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    this.AdminAccessControlDto.User.Id,
                    this.AdminAccessControlDto.User.Uid,
                    this.EditionDto.Id,
                    this.EditionDto.Uid,
                    this.UserInterfaceLanguage);

                result = await this.CommandBus.Send(cmd);
                if (!result.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }
            }
            catch (DomainException ex)
            {
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }

                return Json(new
                {
                    status = "error",
                    message = ex.GetInnerMessage(),
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_CreateForm", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.UpdatedM) });
        }

        #endregion

         #region Details
        
        /// <summary>Detailses the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            //var attendeeCollaboratorDto = await this.attendeeCollaboratorRepo.FindSiteDetailstDtoByCollaboratorUidAndByEditionIdAsync(id ?? Guid.Empty, this.EditionDto.Id);
            //if (attendeeCollaboratorDto == null)
            //{
            //    this.StatusMessageToastr(string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM.ToLowerInvariant()), Infra.CrossCutting.Tools.Enums.StatusMessageTypeToastr.Error);
            //    return RedirectToAction("Index", "Home", new { Area = "" });
            //}

            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Speakers, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Speakers, Url.Action("Index", "Speakers", new { id })),
                new BreadcrumbItemHelper("", Url.Action("Details", "Speakers", new { id }))
            });

            #endregion

            return View();
        }

        #endregion

        #region Delete

        /// <summary>Deletes the specified collaborator.</summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteLogisticSponsors cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }

                cmd.UpdatePreSendProperties(
                    this.AdminAccessControlDto.User.Id,
                    this.AdminAccessControlDto.User.Uid,
                    this.EditionDto.Id,
                    this.EditionDto.Uid,
                    this.UserInterfaceLanguage);

                result = await this.CommandBus.Send(cmd);
                if (!result.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }
            }
            catch (DomainException ex)
            {
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }

                return Json(new
                {
                    status = "error",
                    message = ex.GetInnerMessage(),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Speaker, Labels.DeletedM) });
        }

        #endregion

        #region Finds

        /// <summary>Finds all by filters.</summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminAudiovisual + "," + Constants.CollaboratorType.CuratorshipAudiovisual + "," + Constants.CollaboratorType.CommissionAudiovisual)]
        public async Task<ActionResult> FindAllByFilters(string keywords, int? page = 1)
        {
            var collaboratorsApiDtos = await this.collaboratorRepo.FindAllDropdownApiListDtoPaged(
                this.EditionDto.Id,
                keywords,
                Constants.CollaboratorType.Speaker,
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
                Speakers = collaboratorsApiDtos?.Select(c => new SpeakersDropdownDto
                {
                    Uid = c.Uid,
                    BadgeName = c.BadgeName?.Trim(),
                    Name = c.Name?.Trim(),
                    Picture = c.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, c.Uid, c.ImageUploadDate, true) : null,
                    JobTitle = c.GetCollaboratorJobTitleBaseDtoByLanguageCode(this.UserInterfaceLanguage)?.Value?.Trim(),
                    Companies = c.OrganizationsDtos?.Select(od => new SpeakersDropdownOrganizationDto
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