using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Models
{
    public class IdentitySmsMessageService : IIdentityMessageService
    {
        private readonly IdentitySmsSetup _setup;
        public IdentitySmsMessageService(IdentitySmsSetup setup)
        {
            _setup = setup;
        }

        public Task SendAsync(IdentityMessage message)
        {
            var url = string.Format(_setup.UrlGatewaySms, message.Destination, message.Body);
            var uri = new Uri(url);

            var request = WebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Get;

            var response = request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            response.Close();

            return Task.FromResult(0);
        }
    }
}
