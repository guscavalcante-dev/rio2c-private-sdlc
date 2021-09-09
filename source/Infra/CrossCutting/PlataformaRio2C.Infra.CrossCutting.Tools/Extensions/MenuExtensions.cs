// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 07-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-05-2020
// ***********************************************************************
// <copyright file="MenuExtensions.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using System.Web.Mvc;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    /// <summary>
    /// The menu extensions.
    /// </summary>
    public static class MenuExtensions
    {
        /// <summary>Determines whether the specified CSS class is open.</summary>
        /// <param name="page">The page.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <param name="actionNames">The action names.</param>
        /// <param name="controlNames">The control names.</param>
        /// <param name="areaName">Name of the area.</param>
        /// <param name="separatorCharacter">The separator character.</param>
        /// <returns></returns>
        public static string IsOpen(this WebViewPage page, string cssClass, string actionNames, string controlNames, string areaName, char separatorCharacter = ',')
        {
            if (areaName != null)
            {
                var currentAreaName = page.ViewContext.RouteData.DataTokens["area"]?.ToString() ?? "";
                if (currentAreaName != areaName)
                {
                    return string.Empty;
                }
            }

            if (!string.IsNullOrEmpty(controlNames))
            {
                var currentControllerName = page.ViewContext.RouteData.Values["controller"]?.ToString() ?? "";
                var controllers = controlNames.Split(separatorCharacter);
                if (!controllers.Contains(currentControllerName))
                {
                    return string.Empty;
                }
            }

            if (!string.IsNullOrEmpty(actionNames))
            {
                var currentActionName = page.ViewContext.RouteData.Values["action"]?.ToString() ?? "";
                var actions = actionNames.Split(separatorCharacter);
                if (!actions.Contains(currentActionName))
                {
                    return string.Empty;
                }
            }

            return cssClass;
        }
    }
}