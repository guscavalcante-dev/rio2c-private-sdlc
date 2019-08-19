// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="PlayersController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>PlayersController</summary>
    [AjaxAuthorize(Roles = "Admin")]
    public class PlayersController : BaseController
    {
        private readonly IFileRepository fileRepo;

        /// <summary>Initializes a new instance of the <see cref="PlayersController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="fileRepository">The file repository.</param>
        public PlayersController(IMediator commandBus, IdentityAutenticationService identityController, IFileRepository fileRepository)
            : base(commandBus, identityController)
        {
            this.fileRepo = fileRepository;
        }

        #region List

        /// <summary>Indexes the specified search view model.</summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(PlayerCompanySearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Players, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Companies, Url.Action("Index", "Players", new { Area = "" }))
            });

            #endregion

            return View(searchViewModel);
        }

        #endregion

        #region DataTable Widget

        /// <summary>Searches the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request, bool showAllEditions)
        {
            var holdings = await this.CommandBus.Send(new FindAllOrganizationsBaseDtosAsync(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                OrganizationType.Player.Uid,
                showAllEditions,
                this.UserId,
                this.UserUid,
                this.EditionId,
                this.EditionUid,
                this.UserInterfaceLanguage));

            var response = DataTablesResponse.Create(request, holdings.TotalItemCount, holdings.TotalItemCount, holdings);

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Total Count Widget

        /// <summary>Shows the total count widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var holdingsCount = await this.CommandBus.Send(new CountAllOrganizationsAsync(
                OrganizationType.Player.Uid,
                true,
                this.UserId,
                this.UserUid,
                this.EditionId,
                this.EditionUid,
                this.UserInterfaceLanguage));

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", holdingsCount), divIdOrClass = "#HoldingTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>Shows the edition count widget.</summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var holdingsCount = await this.CommandBus.Send(new CountAllOrganizationsAsync(
                OrganizationType.Player.Uid,
                false,
                this.UserId,
                this.UserUid,
                this.EditionId,
                this.EditionUid,
                this.UserInterfaceLanguage));

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", holdingsCount), divIdOrClass = "#HoldingEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create

        /// <summary>Shows the create modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowCreateModal()
        {
            var cmd = new CreateOrganization(
                await this.CommandBus.Send(new FindAllHoldingsBaseDtosAsync(null, this.UserInterfaceLanguage)),
                await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)));

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/CreateModal", cmd), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Creates the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrganization cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException();
                }

                cmd.UpdateBaseProperties(
                    this.UserId,
                    this.UserUid,
                    this.EditionId,
                    this.EditionUid,
                    this.UserInterfaceLanguage);

                result = await this.CommandBus.Send(cmd);
                if (!result.IsValid)
                {
                    throw new DomainException();
                }
            }
            catch (DomainException)
            {
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }

                return Json(new
                {
                    status = "error",
                    message = Messages.CorrectFormValues,
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_Form", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Player, Labels.CreatedM) });
        }

        #endregion#region Create

        #region Update

        /// <summary>Shows the update modal.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUpdateModal(Guid? organizationUid)
        {
            UpdateOrganization cmd;

            try
            {
                cmd = new UpdateOrganization(
                    await this.CommandBus.Send(new FindOrganizationDtoByUidAsync(organizationUid, this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllHoldingsBaseDtosAsync(null, this.UserInterfaceLanguage)),
                    await this.CommandBus.Send(new FindAllLanguagesDtosAsync(this.UserInterfaceLanguage)));
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

        /// <summary>Updates the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Update(UpdateOrganization cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException();
                }

                cmd.UpdateBaseProperties(
                    this.UserId,
                    this.UserUid,
                    this.EditionId,
                    this.EditionUid,
                    this.UserInterfaceLanguage);

                result = await this.CommandBus.Send(cmd);
                if (!result.IsValid)
                {
                    throw new DomainException();
                }
            }
            catch (DomainException)
            {
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }

                return Json(new
                {
                    status = "error",
                    message = Messages.CorrectFormValues,
                    pages = new List<dynamic>
                    {
                        new { page = this.RenderRazorViewToString("Modals/_Form", cmd), divIdOrClass = "#form-container" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Player, Labels.UpdatedM) });
        }

        #endregion#region Create

        #region Delete

        /// <summary>Deletes the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteOrganization cmd)
        {
            var result = new AppValidationResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException();
                }

                cmd.UpdateBaseProperties(
                    this.UserId,
                    this.UserUid,
                    this.EditionId,
                    this.EditionUid,
                    this.UserInterfaceLanguage);

                result = await this.CommandBus.Send(cmd);
                if (!result.IsValid)
                {
                    throw new DomainException();
                }
            }
            catch (DomainException)
            {
                foreach (var error in result.Errors)
                {
                    var target = error.Target ?? "";
                    ModelState.AddModelError(target, error.Message);
                }

                return Json(new
                {
                    status = "error",
                    message = Messages.CorrectFormValues,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Player, Labels.DeletedM) });
        }

        #endregion
    }
}