// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 08-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-12-2019
// ***********************************************************************
// <copyright file="FormsAuthenticationConfig.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using PlataformaRio2C.Infra.CrossCutting.Identity.Modules;
using PlataformaRio2C.Web.Site.App_Start;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(FormsAuthenticationConfig), "Register")]
namespace PlataformaRio2C.Web.Site.App_Start
{
    /// <summary>FormsAuthenticationConfig</summary>
    public static class FormsAuthenticationConfig
    {
        /// <summary>Registers this instance.</summary>
        public static void Register()
        {
            DynamicModuleUtility.RegisterModule(typeof(SuppressFormsAuthenticationRedirectModule));
        }
    }
}