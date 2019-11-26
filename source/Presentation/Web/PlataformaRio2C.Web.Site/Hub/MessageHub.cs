// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-26-2019
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
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.IOC;
using SimpleInjector.Lifestyles;

namespace PlataformaRio2C.Web.Site.Hub
{
    /// <summary>MessageHub</summary>
    //[Microsoft.AspNet.SignalR.Authorize(Roles = "Player,Producer")]
    public class MessageHub: Microsoft.AspNet.SignalR.Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();       
        public IMessageAppService _messageAppService { get; set; }

        /// <summary>Sends the message.</summary>
        /// <param name="emailRecipient">The email recipient.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<string> SendMessage(string emailRecipient, string message)
        {
            await Task.Delay(1);

            try
            {
                var viewModel = new MessageChatAppViewModel() { Text = message, RecipientEmail = emailRecipient };            

                var container = BootStrapperHub.InitializeThreadScoped();
                using (ThreadScopedLifestyle.BeginScope(container))
                {
                    _messageAppService = container.GetInstance<IMessageAppService>();

                    var identity = HttpContext.Current.User.Identity;                  
                    int userId = identity.GetUserId<int>();

                    //var result = _messageAppService.Send(userId, ref viewModel);

                    var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

                    //if (result.IsValid)
                    //{
                        viewModel.SenderEmail = "rafael.ruiz@sof.to";
                        var viewModelSerialize =  JsonConvert.SerializeObject(viewModel, Formatting.None, jsonSerializerSettings);

                        foreach (var connectionId in _connections.GetConnections(emailRecipient))
                        {
                            Clients.Client(connectionId).receiveMessage(viewModel.SenderEmail, viewModelSerialize);
                            Clients.Client(connectionId).addUnreadsMessages(viewModelSerialize);                            
                        }
                       
                        return viewModelSerialize;
                    //}
                    //else
                    //{
                    //    var viewModelSerialize = JsonConvert.SerializeObject(result, Formatting.None, jsonSerializerSettings);

                    //    return viewModelSerialize;
                    //}
                }
            }
            catch (Exception e)
            {

                throw;
            }

            return null;
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