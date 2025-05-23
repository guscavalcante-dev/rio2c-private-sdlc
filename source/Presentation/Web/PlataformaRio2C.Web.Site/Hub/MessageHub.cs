﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 05-14-2020
// ***********************************************************************
// <copyright file="MessageHub.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PlataformaRio2c.Infra.Data.FileRepository.Helpers;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.HubApplication.CQRS.Commands;
using PlataformaRio2C.Infra.CrossCutting.IOC;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context;
using PlataformaRio2C.Infra.Data.Repository.Repositories;
using SimpleInjector.Lifestyles;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Web.Site.Hub
{
    /// <summary>MessageHub</summary>
    [Microsoft.AspNet.SignalR.Authorize]
    public class MessageHub : Microsoft.AspNet.SignalR.Hub
    {
        private IConnectionRepository connectionRepo;
        //private static readonly ConnectionMapping<string> _connections = new ConnectionMapping<string>(); //TODO: Remove this

        /// <summary>Initializes a new instance of the <see cref="MessageHub" /> class.</summary>
        public MessageHub()
        {
            this.connectionRepo = new ConnectionRepository(new PlataformaRio2CContext());
        }

        /// <summary>Sends the message.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="senderId">The sender identifier.</param>
        /// <param name="senderUid">The sender uid.</param>
        /// <param name="senderEmail">The sender email.</param>
        /// <param name="recipientId">The recipient identifier.</param>
        /// <param name="recipientUid">The recipient uid.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public async Task<string> SendMessage(int editionId, Guid editionUid, int senderId, Guid senderUid, string senderEmail, int recipientId, Guid recipientUid, string text)
        {
            MessageDto messageDto = null;
            HubBaseDto<MessageHubDto> hubBaseDto = null;

            try
            {
                var container = HubBootStrapper.InitializeThreadScoped();
                using (ThreadScopedLifestyle.BeginScope(container))
                {
                    var commandBus = container.GetInstance<IMediator>();

                    var result = await commandBus.Send(new CreateMessage(
                        recipientId,
                        recipientUid,
                        text,
                        senderId,
                        senderUid,
                        editionId,
                        editionUid,
                        null));
                    if (!result.IsValid)
                    {
                        var firstError = result.Errors?.FirstOrDefault();
                        throw new DomainException(firstError != null ? firstError.Message : string.Format(Messages.EntityCouldNotBeAction, Labels.Message, Labels.CreatedF.ToLowerInvariant()));
                    }

                    messageDto = result.Data as MessageDto;
                    if (messageDto == null)
                    {
                        throw new DomainException(string.Format(Messages.EntityCouldNotBeAction, Labels.Message, Labels.CreatedF.ToLowerInvariant()));
                    }
                }
            }
            catch (DomainException ex)
            {
                hubBaseDto = new HubBaseDto<MessageHubDto>
                {
                    Status = "error",
                    Message = ex.GetInnerMessage(),
                    Data = null
                };
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                hubBaseDto = new HubBaseDto<MessageHubDto>
                {
                    Status = "error",
                    Message = "Undefined error. Please contact Rio2C.",
                    Data = null
                };
            }

            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            if (hubBaseDto == null)
            {
                hubBaseDto = new HubBaseDto<MessageHubDto>
                {
                    Status = "success",
                    Message = null,
                    Data = new MessageHubDto
                    {
                        SenderUserUid = messageDto.SenderUser.Uid,
                        SenderEmail = messageDto.SenderUser.Email,
                        SenderName = messageDto.SenderCollaborator?.Badge ??
                                     messageDto.SenderCollaborator?.GetFullName() ??
                                     messageDto.SenderUser?.Name,
                        SenderNameInitials = messageDto.SenderCollaborator?.Badge?.GetTwoLetterCode() ??
                                             messageDto.SenderCollaborator?.GetNameAbbreviation() ??
                                             messageDto.SenderUser?.Name?.GetTwoLetterCode(),
                        SenderImageUrl = messageDto.SenderCollaborator?.ImageUploadDate != null ? ImageHelper.GetImageUrl(FileRepositoryPathType.UserImage, messageDto.SenderCollaborator.Uid, messageDto.SenderCollaborator.ImageUploadDate?.DateTime, true) :
                                                                                                  null,
                        RecipientUserUid = messageDto.RecipientUser.Uid,
                        RecipientEmail = messageDto.RecipientUser.Email,
                        SendDate = messageDto.Message.SendDate,
                        ReadDate = messageDto.Message.ReadDate,
                        Text = messageDto.Message.Text
                    }
                };
            }

            var viewModelSerialize = JsonConvert.SerializeObject(hubBaseDto, Formatting.None, jsonSerializerSettings);

            // Send message to sender
            var senderConnections = await this.connectionRepo.FindAllConnectedByUserNameAsync(senderEmail);
            if (senderConnections?.Any() == true)
            {
                foreach (var senderConnection in senderConnections)
                {
                    Clients.Client(senderConnection.ConnectionId.ToString()).receiveSenderMessage(viewModelSerialize);
                    //Clients.Client(connectionId).addUnreadsMessages(viewModelSerialize);
                }
            }

            //foreach (var connectionId in _connections.GetConnections(senderEmail))
            //{
            //    Clients.Client(connectionId).receiveSenderMessage(viewModelSerialize);
            //    //Clients.Client(connectionId).addUnreadsMessages(viewModelSerialize);
            //}

            // Send message to receiver
            if (!string.IsNullOrEmpty(hubBaseDto?.Data?.RecipientEmail))
            {
                var receiverConnections = await this.connectionRepo.FindAllConnectedByUserNameAsync(hubBaseDto.Data.RecipientEmail);
                if (receiverConnections?.Any() == true)
                {
                    foreach (var receiverConnection in receiverConnections)
                    {
                        Clients.Client(receiverConnection.ConnectionId.ToString()).receiveRecipientMessage(viewModelSerialize);
                        Clients.Client(receiverConnection.ConnectionId.ToString()).receiveNotification(viewModelSerialize);
                        //Clients.Client(connectionId).addUnreadsMessages(viewModelSerialize);
                    }
                }

                //foreach (var connectionId in _connections.GetConnections(hubBaseDto.Data.RecipientEmail))
                //{
                //    Clients.Client(connectionId).receiveRecipientMessage(viewModelSerialize);
                //    Clients.Client(connectionId).receiveNotification(viewModelSerialize);
                //    //Clients.Client(connectionId).addUnreadsMessages(viewModelSerialize);
                //}
            }

            return viewModelSerialize;
        }

        /// <summary>Reads the messages.</summary>
        /// <param name="otherUserId">The other user identifier.</param>
        /// <param name="otherUserUid">The other user uid.</param>
        /// <param name="currentUserId">The current user identifier.</param>
        /// <param name="currentUserUid">The current user uid.</param>
        /// <returns></returns>
        public async Task<string> ReadMessages(int otherUserId, Guid otherUserUid, int currentUserId, Guid currentUserUid)
        {
            MessageDto messageDto = null;
            HubBaseDto<MessageHubDto> hubBaseDto = null;

            try
            {
                var container = HubBootStrapper.InitializeThreadScoped();
                using (ThreadScopedLifestyle.BeginScope(container))
                {
                    var commandBus = container.GetInstance<IMediator>();

                    var result = await commandBus.Send(new ReadMessages(
                        otherUserId,
                        otherUserUid,
                        currentUserId,
                        currentUserUid,
                        null));
                    if (!result.IsValid)
                    {
                        throw new DomainException(string.Format(Messages.EntityCouldNotBeAction, Labels.Message, Labels.PastReadF.ToLowerInvariant()));
                    }

                    messageDto = result.Data as MessageDto;
                    if (messageDto == null)
                    {
                        throw new DomainException(string.Format(Messages.EntityCouldNotBeAction, Labels.Message, Labels.PastReadF.ToLowerInvariant()));
                    }
                }
            }
            catch (DomainException)
            {
                // Ignore
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return null;
        }

        /// <summary>Called when the connection connects to this hub instance.</summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task"/></returns>
        public override async Task OnConnected()
        {
            try
            {
                var container = HubBootStrapper.InitializeThreadScoped();
                using (ThreadScopedLifestyle.BeginScope(container))
                {
                    var commandBus = container.GetInstance<IMediator>();

                    var result = await commandBus.Send(new CreateConnection(
                        new Guid(Context.ConnectionId),
                        Context.User.Identity.Name,
                        Context.Request.Headers["User-Agent"]));
                }

                //var userName = Context.User.Identity.Name; //TODO: Remove this
                //_connections.Add(userName, Context.ConnectionId); //TODO: Remove this
            }
            catch (Exception)
            {
            }

            await base.OnConnected();
        }

        /// <summary>Called when a connection disconnects from this hub gracefully or due to a timeout.</summary>
        /// <param name="stopCalled">
        /// true, if stop was called on the client closing the connection gracefully;
        /// false, if the connection has been lost for longer than the
        /// <see cref="P:Microsoft.AspNet.SignalR.Configuration.IConfigurationManager.DisconnectTimeout"/>.
        /// Timeouts can be caused by clients reconnecting to another SignalR server in scaleout.
        /// </param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task"/></returns>
        public override async Task OnDisconnected(bool stopCalled)
        {
            var container = HubBootStrapper.InitializeThreadScoped();
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                var commandBus = container.GetInstance<IMediator>();

                var result = await commandBus.Send(new DeleteConnection(
                    new Guid(Context.ConnectionId)));
            }

            //// TODO: remove below
            //var name = Context.User.Identity.Name;
            //_connections.Remove(name, Context.ConnectionId);

            await base.OnDisconnected(stopCalled);
        }

        ///// <summary>Called when the connection reconnects to this hub instance.</summary>
        ///// <returns>A <see cref="T:System.Threading.Tasks.Task"/></returns>
        //public override Task OnReconnected()
        //{
        //    string name = Context.User.Identity.Name;

        //    //if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
        //    //{
        //    //    _connections.Add(name, Context.ConnectionId);
        //    //}

        //    return base.OnReconnected();
        //}
    }

    /// <summary>CallerCulturePipelineModule</summary>
    public class CallerCulturePipelineModule : HubPipelineModule
    {
        /// <summary>
        /// This method is called before the incoming components of any modules added later to the <see cref="T:Microsoft.AspNet.SignalR.Hubs.IHubPipeline"/> are
        /// executed. If this returns false, then those later-added modules and the server-side hub method invocation will not
        /// be executed. Even if a client has not been authorized to connect to a hub, it will still be authorized to invoke
        /// server-side methods on that hub unless it is prevented in <see cref="M:Microsoft.AspNet.SignalR.Hubs.IHubPipelineModule.BuildIncoming(System.Func{Microsoft.AspNet.SignalR.Hubs.IHubIncomingInvokerContext,System.Threading.Tasks.Task{System.Object}})"/> by not
        /// executing the invoke parameter or prevented in <see cref="M:Microsoft.AspNet.SignalR.Hubs.HubPipelineModule.OnBeforeIncoming(Microsoft.AspNet.SignalR.Hubs.IHubIncomingInvokerContext)"/> by returning false.
        /// </summary>
        /// <param name="context">A description of the server-side hub method invocation.</param>
        /// <returns>true, if the incoming components of later added modules and the server-side hub method invocation should be executed;
        /// false, otherwise.</returns>
        protected override bool OnBeforeIncoming(IHubIncomingInvokerContext context)
        {
            try
            {
                var cookieCulture = context?.Hub?.Context?.Request?.Cookies["MyRio2CCulture"]?.Value;
                if (!string.IsNullOrEmpty(cookieCulture))
                {
                    var callerCultureInfo = new CultureInfo(cookieCulture);
                    Thread.CurrentThread.CurrentUICulture = callerCultureInfo;
                    Thread.CurrentThread.CurrentCulture = callerCultureInfo;
                }
            }
            catch (Exception)
            {
            }

            return base.OnBeforeIncoming(context);
        }
    }
}