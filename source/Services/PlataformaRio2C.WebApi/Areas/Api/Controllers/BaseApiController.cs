﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.WebApi
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-10-2019
// ***********************************************************************
// <copyright file="BaseApiController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PlataformaRio2C.Application;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.WebApi.Areas.Api.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace PlataformaRio2C.WebApi.Areas.Api.Controllers
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

        /// <summary>Bads the request.</summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        protected Task<IHttpActionResult> BadRequest(Exception ex)
        {
            IHttpActionResult response;

            var errorResponse = new ErrorResponse();

            errorResponse.Error = new Error()
            {
                Code = ex.GetType().Name,
                Message = ex.Message,
                Target = ex.StackTrace.ToString(),
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

        /// <summary>Jsons the specified view model.</summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        protected Task<IHttpActionResult> Json(object viewModel, string fields)
        {
            if (!string.IsNullOrWhiteSpace(fields))
            {
                var fieldsArray = fields.Split(',').Select(e => e.Trim().UppercaseFirst());

                var nameSpaceType = viewModel.GetType().Namespace;
                if (nameSpaceType == "System.Collections.Generic")
                {
                    try
                    {
                        var results = (IList)viewModel;
                        IList<object> output = new List<object>();

                        foreach (var itemResultService in results)
                        {
                            dynamic itemOutput = new ExpandoObject();

                            DynamicExtensions.AddProperty(itemOutput, "Uid", itemResultService.GetType().GetProperty("Uid").GetValue(itemResultService, null));

                            foreach (var itemField in fieldsArray)
                            {
                                DynamicExtensions.AddProperty(itemOutput, itemField, itemResultService.GetType().GetProperty(itemField).GetValue(itemResultService, null));
                            }

                            output.Add(itemOutput as object);
                        }

                        viewModel = output;

                    }
                    catch (System.InvalidCastException e)
                    {
                    }
                    catch (System.Exception e)
                    {
                        return this.BadRequest(e);
                    }
                }
                else if (nameSpaceType == "PlataformaRio2C.Application.ViewModels")
                {
                    try
                    {
                        //var results = (IList)viewModel;

                        dynamic itemOutput = new ExpandoObject();

                        DynamicExtensions.AddProperty(itemOutput, "Uid", viewModel.GetType().GetProperty("Uid").GetValue(viewModel, null));

                        foreach (var itemField in fieldsArray)
                        {
                            DynamicExtensions.AddProperty(itemOutput, itemField, viewModel.GetType().GetProperty(itemField).GetValue(viewModel, null));
                        }

                        viewModel = itemOutput;
                    }
                    catch (System.InvalidCastException e)
                    {
                    }
                    catch (System.Exception e)
                    {
                        return this.BadRequest(e);
                    }
                }
            }

            return Json(viewModel);
        }

        /// <summary>Jsons the specified view model.</summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="fieldsToRemove">The fields to remove.</param>
        /// <returns></returns>
        protected Task<IHttpActionResult> Json(object viewModel, string fields, string fieldsToRemove)
        {
            if (!string.IsNullOrWhiteSpace(fields))
            {
                var fieldsArray = fields.Split(',').Select(e => e.Trim().UppercaseFirst());

                var nameSpaceType = viewModel.GetType().Namespace;
                if (nameSpaceType == "System.Collections.Generic")
                {
                    try
                    {
                        var results = (IList)viewModel;
                        IList<object> output = new List<object>();

                        foreach (var itemResultService in results)
                        {
                            dynamic itemOutput = new ExpandoObject();

                            DynamicExtensions.AddProperty(itemOutput, "Uid", itemResultService.GetType().GetProperty("Uid").GetValue(itemResultService, null));

                            foreach (var itemField in fieldsArray)
                            {
                                DynamicExtensions.AddProperty(itemOutput, itemField, itemResultService.GetType().GetProperty(itemField).GetValue(itemResultService, null));
                            }

                            output.Add(itemOutput as object);
                        }

                        viewModel = output;

                    }
                    catch (System.InvalidCastException e)
                    {
                    }
                    catch (System.Exception e)
                    {
                        return this.BadRequest(e);
                    }
                }
                else if (nameSpaceType == "PlataformaRio2C.Application.ViewModels")
                {
                    try
                    {
                        //var results = (IList)viewModel;

                        dynamic itemOutput = new ExpandoObject();

                        DynamicExtensions.AddProperty(itemOutput, "Uid", viewModel.GetType().GetProperty("Uid").GetValue(viewModel, null));

                        foreach (var itemField in fieldsArray)
                        {
                            DynamicExtensions.AddProperty(itemOutput, itemField, viewModel.GetType().GetProperty(itemField).GetValue(viewModel, null));
                        }

                        viewModel = itemOutput;
                    }
                    catch (System.InvalidCastException e)
                    {
                    }
                    catch (System.Exception e)
                    {
                        return this.BadRequest(e);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(fieldsToRemove))
            {
                var fieldsArray = fieldsToRemove.Split(',').Select(e => e.Trim().UppercaseFirst());
                var nameSpaceType = viewModel.GetType().Namespace;

                if (nameSpaceType == "PlataformaRio2C.Application.ViewModels")
                {
                    try
                    {
                        dynamic itemOutput = new ExpandoObject();

                        DynamicExtensions.AddProperty(itemOutput, "Uid", viewModel.GetType().GetProperty("Uid").GetValue(viewModel, null));

                        foreach (var itemProperty in viewModel.GetType().GetProperties())
                        {
                            if (!fieldsArray.Contains(itemProperty.Name))
                            {
                                DynamicExtensions.AddProperty(itemOutput, itemProperty.Name, viewModel.GetType().GetProperty(itemProperty.Name).GetValue(viewModel, null));
                            }
                        }

                        viewModel = itemOutput;
                    }
                    catch (System.InvalidCastException e)
                    {
                    }
                    catch (System.Exception e)
                    {
                        return this.BadRequest(e);
                    }
                }
            }

            return Json(viewModel);
        }
    }
}