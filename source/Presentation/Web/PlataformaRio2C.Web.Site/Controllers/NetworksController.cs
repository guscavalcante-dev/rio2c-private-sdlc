// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 11-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
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
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Site.Filters;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>NetworksController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.NetworksString)]
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

        #region Contacts list

        /// <summary>Indexes the specified search keywords.</summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        //[AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.Industry)]
        public async Task<ActionResult> Index(string searchKeywords, int? page = 1, int? pageSize = 15)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.NetworkRio2C, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.NetworkRio2C, Url.Action("Index", "Networks", new { Area = "" }))
            });

            #endregion

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchKeywords = searchKeywords;

            return View();
        }

        /// <summary>Shows the contacts list widget.</summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowContactsListWidget(string searchKeywords, int? page = 1, int? pageSize = 15)
        {
            var attendeeCollaboratos = await this.attendeeCollaboratorRepo.FindAllNetworkDtoByEditionIdPagedAsync(this.EditionDto.Id, searchKeywords, page.Value, pageSize.Value);

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchKeywords = searchKeywords;

            return Json(new
            {
                status = "success",
                pages = new List<dynamic>
                {
                    new { page = this.RenderRazorViewToString("Widgets/ContactsWidget", attendeeCollaboratos), divIdOrClass = "#NetworksContactsListWidget" },
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Messages

        /// <summary>Messageses this instance.</summary>
        /// <returns></returns>
        [HttpGet]
        //[AuthorizeCollaboratorType(Order = 3, Types = Constants.CollaboratorType.ExecutiveAudiovisual + "," + Constants.CollaboratorType.Industry)]
        public async Task<ActionResult> Messages(Guid? id)
        {
            #region Breadcrumb

            ViewBag.Breadcrumb = new BreadcrumbHelper(Labels.NetworkRio2C, new List<BreadcrumbItemHelper> {
                new BreadcrumbItemHelper(Labels.NetworkRio2C, Url.Action("Index", "Networks", new { Area = "" })),
                new BreadcrumbItemHelper(Labels.Messages, Url.Action("Messages", "Networks", new { Area = "" }))
            });

            #endregion

            return View(id);
        }

        #region Main Conversations Widget

        /// <summary>Shows the conversations widget.</summary>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="userUid">The user uid.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowConversationsWidget(string searchKeywords, Guid? userUid)
        {
            var conversations = await this.messageRepo.FindAllConversationsDtosByEditionIdAndByUserIdAndBySearchKeywords(this.EditionDto.Id, this.UserAccessControlDto.User.Id, searchKeywords) ??
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

        #endregion
    }
}