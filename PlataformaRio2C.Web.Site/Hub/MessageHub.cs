using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.IOC;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using SimpleInjector.Lifestyles;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlataformaRio2C.Web.Site
{
    [Microsoft.AspNet.SignalR.Authorize(Roles = "Player,Producer")]
    public class MessageHub: Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();       
        public IMessageAppService _messageAppService { get; set; }                

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
                    var result = _messageAppService.Send(userId, ref viewModel);

                    var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

                    if (result.IsValid)
                    {
                        var viewModelSerialize =  JsonConvert.SerializeObject(viewModel, Formatting.None, jsonSerializerSettings);

                        foreach (var connectionId in _connections.GetConnections(emailRecipient))
                        {
                            Clients.Client(connectionId).receiveMessage(viewModel.SenderEmail, viewModelSerialize);
                            Clients.Client(connectionId).addUnreadsMessages(viewModelSerialize);                            
                        }
                       
                        return viewModelSerialize;
                    }
                    else
                    {
                        var viewModelSerialize = JsonConvert.SerializeObject(result, Formatting.None, jsonSerializerSettings);

                        return viewModelSerialize;
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }

            return null;
        }
                     
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

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

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

    public class CallerCulturePipelineModule : HubPipelineModule
    {
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