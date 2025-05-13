// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 10-14-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-19-2019
// ***********************************************************************
// <copyright file="BaseApiController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PlataformaRio2C.Application;
using PlataformaRio2C.Domain.ApiModels;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>BaseApiController</summary>
    public class BaseApiController : ApiController
    {
        /// <summary>Bads the request.</summary>
        /// <param name="validationResult">The validation result.</param>
        /// <returns></returns>
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

        /// <summary>returns Unauthorized (401) with customized message.</summary>
        /// <param name="message">A mensagem de erro.</param>
        /// <returns></returns>
        protected Task<IHttpActionResult> Unauthorized(string message)
        {
            IHttpActionResult response;

            var errorResponse = new ErrorResponse
            {
                Error = new Error
                {
                    Code = "Unauthorized",
                    Message = message,
                    Target = "",
                    InnerError = null
                }
            };

            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent(new JavaScriptSerializer().Serialize(errorResponse), Encoding.UTF8, "application/json")
            };

            response = ResponseMessage(responseMessage);

            return Task.FromResult(response);
        }

        /// <summary>Nots the found.</summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
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

        /// <summary>Bads the request.</summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
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

        /// <summary>Createds the specified view model.</summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
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

        /// <summary>Jsons the specified view model.</summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
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