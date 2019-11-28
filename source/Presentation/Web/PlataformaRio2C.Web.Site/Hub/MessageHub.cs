// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-28-2019
// ***********************************************************************
// <copyright file="MessageHub.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Web.Site.Hub
{
    /// <summary>MessageHub</summary>
    //[Microsoft.AspNet.SignalR.Authorize(Roles = "Player,Producer")]
    public class MessageHub: Microsoft.AspNet.SignalR.Hub
    {
        private static readonly ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        //public IMessageAppService _messageAppService { get; set; }
        private IMediator commandBus { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MessageHub"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        public MessageHub(IMediator commandBus)
        {
            this.commandBus = commandBus;
        }

        /// <summary>Sends the message.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="senderId">The sender identifier.</param>
        /// <param name="senderUid">The sender uid.</param>
        /// <param name="recipientId">The recipient identifier.</param>
        /// <param name="recipientUid">The recipient uid.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public async Task<string> SendMessage(int editionId, Guid editionUid, int senderId, Guid senderUid, int recipientId, Guid recipientUid, string recipientEmail, string text)
        {
            //await Task.Delay(1);

            var result = new AppValidationResult();

            try
            {
                //var identity = HttpContext.Current.User.Identity;
                //var userId = identity.GetUserId<int>();

                result = await this.commandBus.Send(new CreateMessage(
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
                    throw new DomainException("Errorrrr");
                }

                //var container = BootStrapperHub.InitializeThreadScoped();
                //using (ThreadScopedLifestyle.BeginScope(container))
                //{
                //    this.commandBus = container.GetInstance<IMediator>();

                //}
            }
            catch (DomainException ex)
            {
                //foreach (var error in result.Errors)
                //{
                //    var target = error.Target ?? "";
                //    ModelState.AddModelError(target, error.Message);
                //}

                //cmd.UpdateModelsAndLists(
                //    await this.interestRepo.FindAllGroupedByInterestGroupsAsync());

                //return Json(new
                //{
                //    status = "error",
                //    message = ex.GetInnerMessage(),
                //    pages = new List<dynamic>
                //    {
                //        new { page = this.RenderRazorViewToString("Modals/UpdateMainInformationForm", cmd), divIdOrClass = "#form-container" },
                //    }
                //}, JsonRequestBehavior.AllowGet);

                return null;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                //return Json(new { status = "error", message = Messages.WeFoundAndError, }, JsonRequestBehavior.AllowGet);
                return null;
            }

            //try
            //{
            //    var viewModel = new MessageChatAppViewModel() { Text = message, RecipientEmail = emailRecipient };            

            //    var container = BootStrapperHub.InitializeThreadScoped();
            //    using (ThreadScopedLifestyle.BeginScope(container))
            //    {
            //        //_messageAppService = container.GetInstance<IMessageAppService>();

            //        var identity = HttpContext.Current.User.Identity;                  
            //        int userId = identity.GetUserId<int>();

            //        //var result = _messageAppService.Send(userId, ref viewModel);

            //        var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            //        //if (result.IsValid)
            //        //{
            //            viewModel.SenderEmail = "rafael.ruiz@sof.to";
            //            var viewModelSerialize =  JsonConvert.SerializeObject(viewModel, Formatting.None, jsonSerializerSettings);

            //            foreach (var connectionId in _connections.GetConnections(emailRecipient))
            //            {
            //                Clients.Client(connectionId).receiveMessage(viewModel.SenderEmail, viewModelSerialize);
            //                Clients.Client(connectionId).addUnreadsMessages(viewModelSerialize);                            
            //            }

            //            return viewModelSerialize;
            //        //}
            //        //else
            //        //{
            //        //    var viewModelSerialize = JsonConvert.SerializeObject(result, Formatting.None, jsonSerializerSettings);

            //        //    return viewModelSerialize;
            //        //}
            //    }
            //}
            //catch (Exception e)
            //{

            //    throw;
            //}

            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var viewModel = new MessageChatAppViewModel() { Text = text, RecipientEmail = recipientEmail };

            var viewModelSerialize = JsonConvert.SerializeObject(viewModel, Formatting.None, jsonSerializerSettings);

            foreach (var connectionId in _connections.GetConnections(recipientEmail))
            {
                Clients.Client(connectionId).receiveMessage(viewModel.SenderEmail, viewModelSerialize);
                Clients.Client(connectionId).addUnreadsMessages(viewModelSerialize);
            }

            return viewModelSerialize;
        }

        /// <summary>Called when the connection connects to this hub instance.</summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task"/></returns>
        public override Task OnConnected()
        {
            try
            {
                CultureInfo callerCultureInfo = new CultureInfo(Context.Request.Cookies["_culture"].Value);

                Thread.CurrentThread.CurrentUICulture = callerCultureInfo;
                Thread.CurrentThread.CurrentCulture = callerCultureInfo;
            }
            catch (Exception)
            {                
            }
                     

            string name = Context.User.Identity.Name;

            _connections.Add(name, Context.ConnectionId);

            return base.OnConnected();
        }

        /// <summary>Called when a connection disconnects from this hub gracefully or due to a timeout.</summary>
        /// <param name="stopCalled">
        /// true, if stop was called on the client closing the connection gracefully;
        /// false, if the connection has been lost for longer than the
        /// <see cref="P:Microsoft.AspNet.SignalR.Configuration.IConfigurationManager.DisconnectTimeout"/>.
        /// Timeouts can be caused by clients reconnecting to another SignalR server in scaleout.
        /// </param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task"/></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        /// <summary>Called when the connection reconnects to this hub instance.</summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task"/></returns>
        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }
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
                CultureInfo callerCultureInfo = new CultureInfo(context.Hub.Context.Request.Cookies["_culture"].Value);
                Thread.CurrentThread.CurrentUICulture = callerCultureInfo;
                Thread.CurrentThread.CurrentCulture = callerCultureInfo;
            }
            catch (Exception)
            {
            }

            return base.OnBeforeIncoming(context);
        }
    }
}