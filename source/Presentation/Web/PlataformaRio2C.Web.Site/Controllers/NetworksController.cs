// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 11-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-03-2019
// ***********************************************************************
// <copyright file="NetworksController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Site.Filters;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>NetworksController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.ExecutiveAudiovisual + "," + Constants.CollaboratorType.Industry )]
    public class NetworksController : BaseController
    {
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IUserRepository userRepo;
        private readonly IMessageRepository messageRepo;

        /// <summary>Initializes a new instance of the <see cref="NetworksController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="messageRepository">The message repository.</param>
        public NetworksController(
            IMediator commandBus, 
            IdentityAutenticationService identityController,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            IUserRepository userRepository,
            IMessageRepository messageRepository)
            : base(commandBus, identityController)
        {
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.userRepo = userRepository;
            this.messageRepo = messageRepository;
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.Industry)]
        public async Task<ActionResult> Index(int? page = 1, int? pageSize = 15)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.NetworkRio2C, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.NetworkRio2C, Url.Action("Index", "Networks", new { Area = "" }))
            });

            #endregion

            var attendeeCollaboratos = await this.attendeeCollaboratorRepo.FindAllNetworkDtoByEditionIdPagedAsync(this.EditionDto.Id, page.Value, pageSize.Value);

            ViewBag.PageSize = pageSize;

            return View(attendeeCollaboratos);
        }

        #region Messages

        /// <summary>Messageses this instance.</summary>
        /// <returns></returns>
        [AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.ExecutiveAudiovisual + "," + Constants.CollaboratorType.Industry)]
        public async Task<ActionResult> Messages(Guid? id)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.NetworkRio2C, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.Messages, Url.Action("Messages", "Networks", new { Area = "" }))
            });

            #endregion

            return View(id);
        }

        #region Main Conversations Widget

        /// <summary>Shows the conversations widget.</summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowConversationsWidget(Guid? userUid)
        {
            var conversations = await this.messageRepo.FindAllConversationsDtosByEditionIdAndByUserId(this.EditionDto.Id, this.UserAccessControlDto.User.Id) ??
                                new List<ConversationDto>();
            
            // Create conversation menu for selected user to send message
            if (userUid.HasValue)
            {
                // Check if already has a conversation and set viewbag to select it
                var conversation = conversations?.FirstOrDefault(c => c.OtherUser.Uid == userUid);
                if (conversation != null)
                {
                    ViewBag.OtherUserUid = conversation.OtherUser.Uid;
                }
                // If there is no conversation, create one at the top of the list
                else
                {
                    var newConversation = await this.messageRepo.FindNewConversationsDtoByEditionIdAndByOtherUserUid(this.EditionDto.Id, userUid.Value);
                    if (newConversation != null)
                    {
                        conversations.Insert(0, newConversation);
                    }
                }
            }

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ConversationsWidget", conversations), divIdOrClass = "#MessagesConversationsWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Main Conversations Widget

        /// <summary>Shows the conversation widget.</summary>
        /// <param name="recipientId">The recipient identifier.</param>
        /// <param name="recipientUid">The recipient uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowConversationWidget(int? recipientId, Guid? recipientUid)
        {
            if (!recipientId.HasValue || !recipientUid.HasValue)
            {
                return Json(new { status = "error", message = string.Format(Infra.CrossCutting.Resources.Messages.EntityNotAction, Labels.User, Labels.FoundM.ToLowerInvariant()) }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.OtherUserDto = await this.userRepo.FindUserDtoByUserIdAsync(recipientId.Value);

            var messagesDtos = await this.messageRepo.FindAllMessagesDtosByEditionIdAndByUserIdAndByRecipientIdAndByRecipientUid(this.EditionDto.Id, this.UserAccessControlDto.User.Id, recipientId.Value, recipientUid.Value);

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ConversationWidget", messagesDtos), divIdOrClass = "#MessagesConversationWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Send unread messages email

        /// <summary>Sends the unread conversations emails.</summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SendUnreadConversationsEmails(string key)
        {
            var result = new AppValidationResult();

            try
            {
                if (key?.ToLowerInvariant() != ConfigurationManager.AppSettings["SendUnreadConversationsEmailsApiKey"]?.ToLowerInvariant())
                {
                    throw new Exception("Invalid key to execute send unread conversations emails.");
                }

                result = await this.CommandBus.Send(new SendUnreadConversationsEmailsAsync());
                if (!result.IsValid)
                {
                    throw new DomainException("Send unread conversations emails processed with some errors.");
                }
            }
            catch (DomainException ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "success", message = ex.GetInnerMessage(), errors = result?.Errors?.Select(e => new { e.Code, e.Message }) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json(new { status = "error", message = "Send unread conversations emails failed." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = "Send unread conversations emails processed successfully without errors." }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion
    }
}