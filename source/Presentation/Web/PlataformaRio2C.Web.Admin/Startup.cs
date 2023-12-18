// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-09-2019
// ***********************************************************************
// <copyright file="Startup.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using PlataformaRio2C.Infra.CrossCutting.Tools.Mvc;
using PlataformaRio2C.Web.Admin.App_Start;
using PlataformaRio2C.Web.Admin.Areas.WebApi;
using PlataformaRio2C.Web.Admin.Models;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartupAttribute(typeof(PlataformaRio2C.Web.Admin.Startup))]
namespace PlataformaRio2C.Web.Admin
{
    /// <summary>Startup</summary>
    public partial class Startup
    {
        /// <summary>Configurations the specified application.</summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            SimpleInjectorInitializer.Initialize();

            HttpConfiguration config = new HttpConfiguration();

            ConfigureAuth(app);
            config.MessageHandlers.Add(new LanguageMessageHandler());

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.UseDataContractJsonSerializer = true;
            json.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // DataTables.AspNet registration with default options.
            DataTables.AspNet.Mvc5.Configuration.RegisterDataTables();

            app.UseWebApi(config);
            
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            ModelBinders.Binders.DefaultBinder = new IdentityModelBinder();
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}