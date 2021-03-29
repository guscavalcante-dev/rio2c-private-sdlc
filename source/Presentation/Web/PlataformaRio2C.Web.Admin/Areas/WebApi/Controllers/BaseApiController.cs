using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PlataformaRio2C.Application;
using PlataformaRio2C.Web.Admin.Areas.WebApi.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace PlataformaRio2C.Web.Admin.Areas.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        protected IMediator CommandBus;

        public BaseApiController(IMediator commandBus)
        {
            this.CommandBus = commandBus;
        }

        protected BaseApiController()
        {
        }

        protected Task<IHttpActionResult> BadRequest(AppValidationResult validationResult)
        {
            IHttpActionResult response;

            var errorResponse = new ErrorResponse();

            errorResponse.Error = new Error()
            {
                Code = validationResult.Errors.Select(s => s.Code).FirstOrDefault(),
                Message = validationResult.Errors.Select(s => s.Message).FirstOrDefault(),
                Target = validationResult.Errors.Select(s => s.Target).FirstOrDefault(),
                InnerError = null
            };

            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(new JavaScriptSerializer().Serialize(errorResponse), Encoding.UTF8, "application/json")
            };

            response = ResponseMessage(responseMessage);

            return Task.FromResult(response);
        }

        protected Task<IHttpActionResult> NotFound(string message)
        {
            IHttpActionResult response;

            var errorResponse = new ErrorResponse();

            errorResponse.Error = new Error()
            {
                Code = "",
                Message = message,
                Target = "",
                InnerError = null
            };

            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(new JavaScriptSerializer().Serialize(errorResponse), Encoding.UTF8, "application/json")
            };

            response = ResponseMessage(responseMessage);

            return Task.FromResult(response);
        }

        protected Task<IHttpActionResult> BadRequest(string message)
        {
            IHttpActionResult response;

            var errorResponse = new ErrorResponse();

            errorResponse.Error = new Error()
            {
                Code = "",
                Message = message,
                Target = "",
                InnerError = null
            };

            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(new JavaScriptSerializer().Serialize(errorResponse), Encoding.UTF8, "application/json")
            };

            response = ResponseMessage(responseMessage);

            return Task.FromResult(response);
        }

        protected Task<IHttpActionResult> Created(object viewModel)
        {
            IHttpActionResult response;

            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(new JavaScriptSerializer().Serialize(viewModel), Encoding.UTF8, "application/json")
            };

            response = ResponseMessage(responseMessage);


            return Task.FromResult(response);
        }

        protected Task<IHttpActionResult> Json(object viewModel)
        {
            IHttpActionResult response;

            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(viewModel, Formatting.None, jsonSerializerSettings), Encoding.UTF8, "application/json")
            };

            response = ResponseMessage(responseMessage);

            return Task.FromResult(response);
        }
    }
}