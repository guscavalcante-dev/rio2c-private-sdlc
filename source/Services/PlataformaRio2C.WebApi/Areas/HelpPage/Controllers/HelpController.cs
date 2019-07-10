// ***********************************************************************
// Assembly         : PlataformaRio2C.WebApi
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-10-2019
// ***********************************************************************
// <copyright file="HelpController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Web.Http;
using System.Web.Mvc;
using PlataformaRio2C.WebApi.Areas.HelpPage.ModelDescriptions;
using PlataformaRio2C.WebApi.Areas.HelpPage.Models;

namespace PlataformaRio2C.WebApi.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        public HttpConfiguration Configuration { get; private set; }
        private const string ErrorViewName = "Error";

        /// <summary>Initializes a new instance of the <see cref="HelpController"/> class.</summary>
        public HelpController()
        {
            this.Configuration = GlobalConfiguration.Configuration;
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        /// <summary>APIs the specified API identifier.</summary>
        /// <param name="apiId">The API identifier.</param>
        /// <returns></returns>
        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }

        /// <summary>Resources the model.</summary>
        /// <param name="modelName">Name of the model.</param>
        /// <returns></returns>
        public ActionResult ResourceModel(string modelName)
        {
            if (!String.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
    }
}