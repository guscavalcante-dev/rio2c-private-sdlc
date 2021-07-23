// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-23-2021
// ***********************************************************************
// <copyright file="SalesPlatformsController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataTables.AspNet.Core;
using MediatR;
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>
    /// SalesPlatformsController
    /// </summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.Admin)]
    public class SalesPlatformsController : BaseController
    {
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IAttendeeSalesPlatformTicketTypeRepository attendeeSalesPlatformTicketTypeRepo;

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformsController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="attendeeSalesPlatformTicketTypeRepository">The attendee sales platform ticket type repository.</param>
        public SalesPlatformsController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            ICollaboratorRepository collaboratorRepository,
            IAttendeeSalesPlatformTicketTypeRepository attendeeSalesPlatformTicketTypeRepository)
            : base(commandBus, identityController)
        {
            this.collaboratorRepo = collaboratorRepository;
            this.attendeeSalesPlatformTicketTypeRepo = attendeeSalesPlatformTicketTypeRepository;
        }

        #region Export Eventbrite CSV

        /// <summary>Shows the export eventbrite CSV modal.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowExportEventbriteCsvModal()
        {
            var ticketTypes = await this.attendeeSalesPlatformTicketTypeRepo.FindAllByEditionIdAsync(this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Modals/EventbriteCsvModal", ticketTypes), divIdOrClass = "#GlobalModalContainer" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Exports the eventbrite CSV.</summary>
        /// <param name="request">The request.</param>
        /// <param name="selectedCollaboratorsUids">The selected collaborators uids.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="ticketClassName">Name of the ticket class.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showHighlights">The show highlights.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ExportEventbriteCsv(
            IDataTablesRequest request, 
            string selectedCollaboratorsUids, 
            string collaboratorTypeName, 
            string ticketClassName, 
            bool showAllEditions, 
            bool showAllParticipants, 
            bool? showHighlights)
        {
            List<EventbriteCsv> eventbriteCsv = null;

            try
            {
                var speakers = await this.collaboratorRepo.FindAllByDataTable(
                    1,
                    10000,
                    request?.Search?.Value,
                    request?.GetSortColumns(),
                    selectedCollaboratorsUids?.ToListGuid(','),
                    new string[] { collaboratorTypeName },
                    showAllEditions,
                    showAllParticipants,
                    showHighlights,
                    this.EditionDto?.Id
                );

                eventbriteCsv = speakers?.Select(s => new EventbriteCsv(
                    s.FirstName, 
                    s.LastNames, 
                    s.Email, 
                    ticketClassName, 1)).ToList();
            }
            catch (DomainException ex)
            {
                return Json(new { status = "error", message = ex.GetInnerMessage(), }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", data = JsonConvert.SerializeObject(eventbriteCsv) }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}