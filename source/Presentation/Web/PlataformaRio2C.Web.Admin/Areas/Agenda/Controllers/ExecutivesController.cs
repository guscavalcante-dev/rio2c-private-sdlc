// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 04-29-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-29-2024
// ***********************************************************************
// <copyright file="AgendasController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MediatR;
using Newtonsoft.Json;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Sympla.Models;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Admin.Controllers;
using PlataformaRio2C.Web.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Constants = PlataformaRio2C.Domain.Constants;
using static PlataformaRio2C.Web.Admin.Areas.Agenda.Controllers.ExecutivesController;
using PlataformaRio2C.Domain.Dtos.Agendas;

namespace PlataformaRio2C.Web.Admin.Areas.Agenda.Controllers
{
    /// <summary>
    /// AgendasController
    /// </summary>
    [AjaxAuthorize(Order = 1, Roles = Constants.Role.AnyAdmin)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.AdminConferences)]
    public class ExecutivesController : BaseController
    {
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IRoleRepository roleRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutivesController"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="collaboratorTypeRepository">The collaborator type repository.</param>
        public ExecutivesController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            ICollaboratorRepository collaboratorRepository,
            IRoleRepository roleRepository,
            ICollaboratorTypeRepository collaboratorTypeRepository)
            : base(commandBus, identityController)
        {
            this.collaboratorRepo = collaboratorRepository;
            this.roleRepo = roleRepository;
            this.collaboratorTypeRepo = collaboratorTypeRepository;
        }

        #region List

        /// <summary>
        /// Indexes the specified search view model.
        /// </summary>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(AgendaExecutiveSearchViewModel searchViewModel)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.Agenda, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Agenda, null),
                new BreadcrumbItemHelper(Labels.Executives, Url.Action("Index", "Executives", new { Area = "Agenda" }))
            });

            #endregion

            return View(searchViewModel);
        }

        #region DataTable Widget

        /// <summary>
        /// Searches the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="searchViewModel">The search view model.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(IDataTablesRequest request)
        {
            var collaboratorDtos = await this.collaboratorRepo.FindAllWithAgendaByDataTable(
                request.Start / request.Length,
                request.Length,
                request.Search?.Value,
                request.GetSortColumns(),
                Constants.CollaboratorType.ReceivesAgendaEmail,
                this.UserInterfaceLanguage,
                this.EditionDto?.Id);

            var response = DataTablesResponse.Create(request, collaboratorDtos.TotalItemCount, collaboratorDtos.TotalItemCount, collaboratorDtos);

            return Json(new
            {
                status = "success",
                dataTable = response
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Send Agenda Emails

        /// <summary>
        /// Sends the agenda emails.
        /// </summary>
        /// <param name="selectedCollaboratorsUids">The selected collaborators uids.</param>
        /// <returns></returns>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        [HttpPost]
        public async Task<ActionResult> SendAgendaEmails(string selectedCollaboratorsUids)
        {
            AppValidationResult result = null;

            try
            {
                var collaboratorsUids = selectedCollaboratorsUids?.ToListGuid(',');
                var collaboratorsDtos = await this.collaboratorRepo.FindAllCollaboratorDtosWithAgendaByUids(this.EditionDto.Id, collaboratorsUids);
                if (collaboratorsDtos?.Any() != true)
                {
                    throw new DomainException(Messages.SelectAtLeastOneOption);
                }

                // Get "Eventos Paralelos" from RIO2C tech team API
                var request = new SearchCollaboratorEventsRequest() { CollaboratorsID = collaboratorsUids.ToArray() };
                SearchCollaboratorEventsResponse apiResult = this.ExecuteRequest<SearchCollaboratorEventsResponse>("searchCollaboratorEvents", HttpMethod.Post, request.ToJson());

                // Converts the API response to an internal DTO containing all Collaborators with all Events
                List<CollaboratorEventsDto> collaboratorsEventsDtos = apiResult.CollaboratorsEvents.Select(ce => new CollaboratorEventsDto
                {
                    CollaboratorUid = Guid.Parse(ce.Key),
                    CollaboratorEventDtos = apiResult.EventsInfo
                                                        .Where(ei => ce.Value.Contains(ei.Key))
                                                        .Select(ei => new CollaboratorEventDto 
                                                        { 
                                                           Data = (!string.IsNullOrEmpty(ei.Value?.Data) ? DateTime.ParseExact(ei.Value.Data, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture) : new DateTime()),
                                                           Descritivo = ei.Value?.Descritivo,
                                                           Horario = ei.Value?.Horario,
                                                           Local = ei.Value?.Local,
                                                           Nome = ei.Value?.Nome
                                                        }).ToList()
                }).ToList();

                List<string> errors = new List<string>();
                foreach (var collaboratorDto in collaboratorsDtos)
                {
                    var collaboratorLanguageCode = collaboratorDto.UserInterfaceLanguage ?? this.UserInterfaceLanguage;

                    //Get Events from current iterating Collaborator
                    var collaboratorEventDtos = collaboratorsEventsDtos?.FirstOrDefault(ce => ce.CollaboratorUid == collaboratorDto.Uid)?.CollaboratorEventDtos;

                    try
                    {
                        result = await this.CommandBus.Send(new SendExecutiveAgendaEmailAsync(
                            collaboratorDto.Uid,
                            collaboratorDto.UserBaseDto.SecurityStamp,
                            collaboratorDto.UserBaseDto.Id,
                            collaboratorDto.UserBaseDto.Uid,
                            collaboratorDto.FirstName,
                            collaboratorDto.FullName,
                            collaboratorDto.UserBaseDto.Email,
                            this.EditionDto.Edition,
                            this.AdminAccessControlDto.User.Id,
                            collaboratorLanguageCode,
                            collaboratorDto.AttendeeCollaboratorTypeDtos,
                            collaboratorDto.ConferencesDtos,
                            collaboratorDto.NegotiationBaseDtos.Concat(collaboratorDto.ProducerNegotiationBaseDtos),
                            collaboratorEventDtos));
                        if (!result.IsValid)
                        {
                            throw new DomainException(Messages.CorrectFormValues);
                        }
                    }
                    catch (DomainException)
                    {
                        //Cannot stop sending email when exception occurs.
                        errors.AddRange(result.Errors.Select(e => e.Message));
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                }

                if (errors.Any())
                {
                    throw new DomainException(string.Format(Messages.OneOrMoreEmailsNotSend, Labels.WelcomeEmail));
                }
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

            return Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.Email.ToLowerInvariant(), Labels.Sent.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
        }

        #region RIO2C "Eventos Paralelos" API integration (TODO: Refactor this!)

        //1. Create a specific project to external generic API Integrations
        //2. Move all this classes to this project
        //2. Create a Service whith this "Private methods" region
        //3. Configure service at Statup.cs
        //4. Use here via dependency injection

        #region Classes

        public class SearchCollaboratorEventsRequest
        {
            public Guid[] CollaboratorsID { get; set; }
        }

        public class EventsInfo
        {
            public string Local { get; set; }
            public string Horario { get; set; }
            public string Data { get; set; }
            public string Nome { get; set; }
            public string Descritivo { get; set; }
        }

        public class SearchCollaboratorEventsResponse
        {
            public bool Success { get; set; }

            public Dictionary<string, List<string>> CollaboratorsEvents { get; set; }

            public Dictionary<string, EventsInfo> EventsInfo { get; set; }
        }

        #endregion

        #region Private methods

        private readonly string apiUrl = "https://eventos.rio2c.com.br/api/";
        private readonly string apiKey = "21A502BC-8F12-9C91-2A21CQRHMH542JK2";

        /// <summary>
        /// Deserializes the payload.
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        private SymplaParticipant DeserializePayload(string payload)
        {
            var symplaPayload = JsonConvert.DeserializeObject<SymplaParticipant>(payload);

            symplaPayload.PayloadString = payload.ToJsonMinified();

            return symplaPayload;
        }

        /// <summary>
        /// Executes the request.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="jsonString">The json string.</param>
        /// <returns></returns>
        private string ExecuteRequest(string path, HttpMethod httpMethod, string jsonString)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("API-KEY", this.apiKey);

                ServicePointManager.Expect100Continue = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                       SecurityProtocolType.Tls11 |
                                                       SecurityProtocolType.Tls12;
                string url = this.apiUrl + path;
                var response = httpMethod == HttpMethod.Get ? client.DownloadString(url) :
                                                              client.UploadString(url, httpMethod.ToString(), jsonString);

                return response;
            }
        }

        /// <summary>
        /// Executes the request and desserialize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="jsonString">The json string.</param>
        /// <returns></returns>
        private T ExecuteRequest<T>(string path, HttpMethod httpMethod, string jsonString)
        {
            var response = this.ExecuteRequest(path, httpMethod, jsonString);
            return JsonConvert.DeserializeObject<T>(response);
        }

        #endregion

        #endregion

        #endregion

        #region Total Count Widget

        /// <summary>
        /// Shows the total count widget.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowTotalCountWidget()
        {
            var executivesCount = await this.collaboratorRepo.CountAllWithAgendaByDataTable(true, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/TotalCountWidget", executivesCount), divIdOrClass = "#AgendasExecutivesTotalCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edition Count Widget

        /// <summary>
        /// Shows the edition count widget.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ShowEditionCountWidget()
        {
            var executivesCount = await this.collaboratorRepo.CountAllWithAgendaByDataTable(false, this.EditionDto.Id);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/EditionCountWidget", executivesCount), divIdOrClass = "#AgendasExecutivesEditionCountWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}