// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-17-2019
// ***********************************************************************
// <copyright file="HtmlExtensions.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using LinqKit;
using PlataformaRio2C.Infra.CrossCutting.Tools.Enums;
using PlataformaRio2C.Infra.CrossCutting.Tools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    /// <summary>HtmlExtensions</summary>
    public static class HtmlExtensions
    {
        public static MvcHtmlString ToMvcHtmlString(this MvcHtmlString htmlString)
        {
            if (htmlString != null)
            {
                return new MvcHtmlString(HttpUtility.HtmlDecode(htmlString.ToString()));
            }
            return null;
        }


        public static MvcHtmlString ValidationMessagesFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return ValidationMessagesFor(htmlHelper, expression, null);
        }

        public static MvcHtmlString ValidationMessagesFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var propertyName = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).PropertyName;
            var modelState = htmlHelper.ViewData.ModelState;

            if (modelState.ContainsKey(propertyName) && modelState[propertyName].Errors.Count > 1)
            {

                var msgs = new StringBuilder();
                foreach (ModelError error in modelState[propertyName].Errors)
                {

                    msgs.AppendLine(htmlHelper.ValidationMessageFor(expression, error.ErrorMessage, htmlAttributes as IDictionary<string, object> ?? htmlAttributes).ToHtmlString());
                }

                return new MvcHtmlString(msgs.ToString());
            }

            return htmlHelper.ValidationMessageFor(expression, null, htmlAttributes as IDictionary<string, object> ?? htmlAttributes);
        }

        public static MvcHtmlString ValidationMessages(this HtmlHelper htmlHelper, string modelName)
        {
            return ValidationMessages(htmlHelper, modelName, null);
        }

        public static MvcHtmlString ValidationMessages(this HtmlHelper htmlHelper, string modelName, object htmlAttributes)
        {
            var propertyName = modelName;
            var modelState = htmlHelper.ViewData.ModelState;

            if (modelState.ContainsKey(propertyName) && modelState[propertyName].Errors.Count > 1)
            {

                var msgs = new StringBuilder();
                foreach (ModelError error in modelState[propertyName].Errors)
                {

                    msgs.AppendLine(htmlHelper.ValidationMessage(modelName, error.ErrorMessage, htmlAttributes as IDictionary<string, object> ?? htmlAttributes).ToHtmlString());
                }

                return new MvcHtmlString(msgs.ToString());
            }

            return htmlHelper.ValidationMessage(modelName, null, htmlAttributes as IDictionary<string, object> ?? htmlAttributes);
        }

        public static string GetBaseUrl(this HtmlHelper helper)
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }
        public static HtmlString Serializar(this HtmlHelper html, object objeto)
        {
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };

            var result = serializer.Serialize(objeto);

            return new HtmlString(result);
        }

        public static string GetCurrentUserName(this HtmlHelper helper)
        {
            if (helper.ViewContext.HttpContext.Request.IsAuthenticated)
            {
                var claimsIdentity = helper.ViewContext.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;

                var claim = claimsIdentity.FindFirst("Name");

                if (claim != null)
                {
                    return claim.Value;
                }
            }
            return null;
        }

        public static bool HasClaim(this HtmlHelper helper, string nameClaim)
        {
            if (helper.ViewContext.HttpContext.Request.IsAuthenticated)
            {
                var claimsIdentity = helper.ViewContext.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;

                var claim = claimsIdentity.FindFirst(nameClaim);

                if (claim != null)
                {
                    return claim.Value == "true";
                }
            }

            return false;
        }

        public static bool UserInRole(this HtmlHelper helper, string valueCheck)
        {
            if (helper.ViewContext.HttpContext.Request.IsAuthenticated)
            {
                var claimsIdentity = helper.ViewContext.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;

                return claimsIdentity.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == valueCheck);
            }

            return false;
        }

        public static string[] GetCurrentUserRoles(this HtmlHelper helper)
        {
            if (helper.ViewContext.HttpContext.Request.IsAuthenticated)
            {
                var claimsIdentity = helper.ViewContext.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;

                var claim = claimsIdentity.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();

                if (claim != null)
                {
                    return claim.Select(e => e.Value).ToArray();
                }
            }
            return new string[] { };
        }

        public static string GetCurrentLastAccess(this HtmlHelper helper)
        {
            if (helper.ViewContext.HttpContext.Session["UserSession.LastAccess"] != null)
            {

                return helper.ViewContext.HttpContext.Session["UserSession.LastAccess"].ToString();
            }
            return null;
        }

        public static IHtmlString GetValuesPaging(this HtmlHelper html, string templateTexto)
        {
            return GetValuesPaging(html, templateTexto, "", false);
        }

        /// <summary>
        /// Metodo que captura os valores de totais de registro encontrados setados no controle e os aplica no template de texto fornecido, retornando html formatado.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="templateTexto">Template de texto com no máximo 3 variaveis <example>"apresentando {0} de {1} do total de {2} registros."</example> </param>
        /// <param name="persitir">opcional -> para mantar os valores na memória, útil para o uso de subviews</param>
        /// <returns>html renderizado</returns>
        public static IHtmlString GetValuesPaging(this HtmlHelper html, string templateTexto, string named, bool persitir)
        {
            var pagingOf = string.Format("paging{0}-Of", named);
            var pagingTo = string.Format("paging{0}-To", named);
            var pagingTotal = string.Format("paging{0}-Total", named);

            var of = (int)html.ViewContext.TempData[pagingOf];
            var to = (int)html.ViewContext.TempData[pagingTo];
            var total = (int)html.ViewContext.TempData[pagingTotal];

            if (persitir)
            {
                html.ViewContext.TempData.Keep(pagingOf);
                html.ViewContext.TempData.Keep(pagingTo);
                html.ViewContext.TempData.Keep(pagingTotal);
            }

            return html.Raw(string.Format(templateTexto, of, to, total));
        }


        public static MvcHtmlString Image(this HtmlHelper html, byte[] image)
        {
            return Image(html, image, null, null);
        }

        public static MvcHtmlString Image(this HtmlHelper html, byte[] image, string cssClass, string altText)
        {
            if (image != null)
            {
                return Image(html, Convert.ToBase64String(image), cssClass, altText);
            }
            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString Image(this HtmlHelper html, byte[] image, int width)
        {
            return Image(html, image, width, null, null);
        }

        public static MvcHtmlString Image(this HtmlHelper html, byte[] image, int width, string cssClass, string altText)
        {
            if (image != null)
            {
                return Image(html, Convert.ToBase64String(image), width, cssClass, altText);
            }
            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString Image(this HtmlHelper html, byte[] image, int width, string dataName, object dataValue)
        {
            return Image(html, image, width, dataName, dataValue, null, null);
        }

        public static MvcHtmlString Image(this HtmlHelper html, byte[] image, int width, string dataName, object dataValue, string cssClass, string altText)
        {
            if (image != null)
            {
                return Image(html, Convert.ToBase64String(image), width, dataName, dataValue, cssClass, altText);
            }
            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString Image(this HtmlHelper html, byte[] image, int height, int width, string dataName, object dataValue)
        {
            return Image(html, image, height, width, dataName, dataValue, null, null);
        }

        public static MvcHtmlString Image(this HtmlHelper html, byte[] image, int height, int width, string dataName, object dataValue, string cssClass, string altText)
        {
            if (image != null)
            {
                return Image(html, Convert.ToBase64String(image), height, width, dataName, dataValue, cssClass, altText);
            }
            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString Image(this HtmlHelper html, string imageBase64String, int width)
        {
            return Image(html, imageBase64String, width, null, null);
        }

        public static MvcHtmlString Image(this HtmlHelper html, string imageBase64String, int width, string cssClass, string altText)
        {
            if (!string.IsNullOrEmpty(imageBase64String))
            {
                var img = String.Format("data:image/png;base64,{0}", imageBase64String);

                var classCss = string.IsNullOrEmpty(cssClass) ? "" : "class='" + cssClass + "'";
                var alt = string.IsNullOrEmpty(cssClass) ? "" : "alt='" + altText + "'";
                var wdt = width == 0 ? "" : "width='" + width.ToString() + "'";

                var tag = string.Format("<img {0} {1} src='{2}' {3} />", classCss, alt, img, wdt);

                return new MvcHtmlString(tag);
            }
            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString Image(this HtmlHelper html, string imageBase64String, int width, string dataName, object dataValue)
        {
            return Image(html, imageBase64String, width, dataName, dataValue, null, null);
        }

        public static MvcHtmlString Image(this HtmlHelper html, string imageBase64String, int width, string dataName, object dataValue, string cssClass, string altText)
        {
            if (!string.IsNullOrEmpty(imageBase64String))
            {
                var img = String.Format("data:image/jpg;base64,{0}", imageBase64String);

                var classCss = string.IsNullOrEmpty(cssClass) ? "" : "class='" + cssClass + "'";
                var alt = string.IsNullOrEmpty(cssClass) ? "" : "alt='" + altText + "'";
                var wdt = width == 0 ? "" : "width='" + width.ToString() + "'";
                var data = string.Format("{0}=\"{1}\"", dataName, dataValue);
                var tag = string.Format("<img {0} {1} src='{2}' {3} {4} />", classCss, alt, img, wdt, data);

                return new MvcHtmlString(tag);
            }
            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString Image(this HtmlHelper html, string imageBase64String, int height, int width, string dataName, object dataValue)
        {
            return Image(html, imageBase64String, height, width, dataName, dataValue, null, null);
        }

        public static MvcHtmlString Image(this HtmlHelper html, string imageBase64String, int height, int width, string dataName, object dataValue, string cssClass, string altText)
        {
            if (!string.IsNullOrEmpty(imageBase64String))
            {
                var img = String.Format("data:image/jpg;base64,{0}", imageBase64String);

                var classCss = string.IsNullOrEmpty(cssClass) ? "" : "class='" + cssClass + "'";
                var alt = string.IsNullOrEmpty(cssClass) ? "" : "alt='" + altText + "'";
                var wdt = width == 0 ? "" : "width='" + width.ToString() + "'";
                var hght = height == 0 ? "" : "height='" + height.ToString() + "'";
                var data = string.Format("{0}=\"{1}\"", dataName, dataValue);
                var tag = string.Format("<img {0} {1} src='{2}' {5} {3} {4} />", classCss, alt, img, wdt, data, hght);

                return new MvcHtmlString(tag);
            }
            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString Image(this HtmlHelper html, string imageBase64String)
        {
            return Image(html, imageBase64String, null, null);
        }

        public static MvcHtmlString Image(this HtmlHelper html, string imageBase64String, string cssClass, string altText)
        {
            if (!string.IsNullOrEmpty(imageBase64String))
            {
                var img = String.Format("data:image/jpg;base64,{0}", imageBase64String);

                var classCss = string.IsNullOrEmpty(cssClass) ? "" : "class='" + cssClass + "'";
                var alt = string.IsNullOrEmpty(cssClass) ? "" : "alt='" + altText + "'";

                var tag = string.Format("<img {0} {1} src='{2}' />", classCss, alt, img);

                return new MvcHtmlString(tag);
            }
            return null;
        }

        public static MvcHtmlString PrintFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return PrintFor(html, expression, null);
        }

        public static MvcHtmlString PrintFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string emptyText)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var model = metadata.Model ?? emptyText;
            if (string.IsNullOrEmpty(model.ToString()))
                model = emptyText;
            return MvcHtmlString.Create(model.ToString());
        }

        public static MvcHtmlString PrintFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object concat)
        {
            return PrintFor(html, expression, concat, null);
        }

        public static MvcHtmlString PrintFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object concat, string emptyText)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            var model = metadata.Model;

            if (model == null || model.ToString() == string.Empty)
            {
                return MvcHtmlString.Create(emptyText);
            }

            return MvcHtmlString.Create(string.Concat(model.ToString(), concat));
        }

        public static string GetAntiForgeryToken(this HtmlHelper helper)
        {
            string cookieToken, formToken;
            AntiForgery.GetTokens(null, out cookieToken, out formToken);
            return cookieToken + ":" + formToken;
        }

        #region Verifica Permissões

        /// <summary>
        /// Método de extensão que verifica se o usuário logado tem todas as permissões requeridas.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="permissions">Codigo das permissões</param>
        /// <returns></returns>
        public static bool HasPermissions(this HtmlHelper helper, int[] permissions)
        {
            var userPermissions = (int[])helper.ViewContext.HttpContext.Session[BaseUserSession.UserPermissionsKeys];

            var predicate = PredicateBuilder.New<int>(false);

            foreach (var permission in permissions)
            {
                predicate = predicate.Or(i => i == permission);
            }

            return userPermissions.Any(predicate.Compile());
        }

        /// <summary>
        /// Método de extensão que verifica se o usuário logado tem a permissão
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="permission">Código da permissão</param>
        /// <returns></returns>
        //public static bool HasPermission(this HtmlHelper helper, int permission)
        //{
        //    var userPermissions = helper.GetCurrentUserPermissions();

        //    return userPermissions.Any(i => i == permission);
        //}

        #endregion

        #region Extend UrlAction

        //public static string ActionPermission(this UrlHelper url, string actionName, string controllerName, object routeValues, int permission)
        //{
        //    return ActionPermission(url, actionName, controllerName, routeValues, new int[1] { permission });
        //}

        //public static string ActionPermission(this UrlHelper url, string actionName, string controllerName, object routeValues, int[] permissions)
        //{
        //    var userPermissions = url.GetCurrentUserPermissions();

        //    var predicate = PredicateBuilder.True<int>();
        //    foreach (var permission in permissions)
        //    {
        //        predicate = predicate.And(i => i == permission);
        //    }

        //    if (userPermissions.Any(predicate.Compile()))
        //    {
        //        return url.Action(actionName, controllerName, routeValues);
        //    }
        //    return "#";
        //}

        //public static string ActionPermission(this UrlHelper url, string actionName, string controllerName, int permission)
        //{
        //    return ActionPermission(url, actionName, controllerName, new int[1] { permission });
        //}

        //public static string ActionPermission(this UrlHelper url, string actionName, string controllerName, int[] permissions)
        //{
        //    var userPermissions = url.GetCurrentUserPermissions();

        //    var predicate = PredicateBuilder.True<int>();
        //    foreach (var permission in permissions)
        //    {
        //        predicate = predicate.And(i => i == permission);
        //    }

        //    if (userPermissions.Any(predicate.Compile()))
        //    {
        //        return url.Action(actionName, controllerName);
        //    }
        //    return "#";
        //}

        #endregion

        #region Extend ActionLink with Permission

        //public static MvcHtmlString ActionLinkPermission(this HtmlHelper htmlHelper, string linkText, string actionName, int permission)
        //{
        //    return ActionLinkPermission(htmlHelper, linkText, actionName, new int[1] { permission });
        //}

        //public static MvcHtmlString ActionLinkPermission(this HtmlHelper htmlHelper, string linkText, string actionName, int[] permissions)
        //{
        //    if (htmlHelper.HasPermissions(permissions))
        //    {
        //        return htmlHelper.ActionLink(linkText, actionName);
        //    }
        //    return new MvcHtmlString(linkText);
        //}

        //public static MvcHtmlString ActionLinkPermission(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, int permission)
        //{
        //    return ActionLinkPermission(htmlHelper, linkText, actionName, routeValues, new int[1] { permission });
        //}

        //public static MvcHtmlString ActionLinkPermission(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, int[] permissions)
        //{
        //    if (htmlHelper.HasPermissions(permissions))
        //    {
        //        return htmlHelper.ActionLink(linkText, actionName, routeValues);
        //    }
        //    return new MvcHtmlString(linkText);
        //}

        //public static MvcHtmlString ActionLinkPermission(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, int permission)
        //{
        //    return ActionLinkPermission(htmlHelper, linkText, actionName, routeValues, new int[1] { permission });
        //}

        //public static MvcHtmlString ActionLinkPermission(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, int[] permissions)
        //{
        //    if (htmlHelper.HasPermissions(permissions))
        //    {
        //        return htmlHelper.ActionLink(linkText, actionName, routeValues);
        //    }
        //    return new MvcHtmlString(linkText);
        //}

        //public static MvcHtmlString ActionLinkPermission(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, int permission)
        //{
        //    return ActionLinkPermission(htmlHelper, linkText, actionName, controllerName, new int[1] { permission });
        //}

        //public static MvcHtmlString ActionLinkPermission(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, int[] permissions)
        //{
        //    if (htmlHelper.HasPermissions(permissions))
        //    {
        //        return htmlHelper.ActionLink(linkText, actionName, controllerName);
        //    }
        //    return new MvcHtmlString(linkText);
        //}

        //public static MvcHtmlString ActionLinkPermission(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes, int permission)
        //{
        //    return ActionLinkPermission(htmlHelper, linkText, actionName, routeValues, htmlAttributes, new int[1] { permission });
        //}

        //public static MvcHtmlString ActionLinkPermission(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes, int[] permissions)
        //{
        //    if (htmlHelper.HasPermissions(permissions))
        //    {
        //        return htmlHelper.ActionLink(linkText, actionName, routeValues, htmlAttributes);
        //    }
        //    return new MvcHtmlString(linkText);
        //}

        //public static MvcHtmlString ActionLinkPermission(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, int permission)
        //{
        //    return ActionLinkPermission(htmlHelper, linkText, actionName, routeValues, htmlAttributes, new int[1] { permission });
        //}

        //public static MvcHtmlString ActionLinkPermission(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, int[] permissions)
        //{
        //    if (htmlHelper.HasPermissions(permissions))
        //    {
        //        return htmlHelper.ActionLink(linkText, actionName, routeValues, htmlAttributes);
        //    }
        //    return new MvcHtmlString(linkText);
        //}

        //public static MvcHtmlString ActionLinkPermission(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, int permission)
        //{
        //    return ActionLinkPermission(htmlHelper, linkText, actionName, controllerName, routeValues, htmlAttributes, new int[1] { permission });
        //}

        //public static MvcHtmlString ActionLinkPermission(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, int[] permissions)
        //{
        //    if (htmlHelper.HasPermissions(permissions))
        //    {
        //        return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
        //    }
        //    return new MvcHtmlString(linkText);
        //}

        //public static MvcHtmlString ActionLinkPermission(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, int permission)
        //{
        //    return ActionLinkPermission(htmlHelper, linkText, actionName, controllerName, routeValues, htmlAttributes, new int[1] { permission });
        //}

        //public static MvcHtmlString ActionLinkPermission(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, int[] permissions)
        //{
        //    if (htmlHelper.HasPermissions(permissions))
        //    {
        //        return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
        //    }
        //    return new MvcHtmlString(linkText);
        //}

        #endregion

        #region Extend Action with Permission

        //public static MvcHtmlString ActionPermission(this HtmlHelper htmlHelper, string actionName, int permission)
        //{
        //    return ActionPermission(htmlHelper, actionName, new int[1] { permission });
        //}

        //public static MvcHtmlString ActionPermission(this HtmlHelper htmlHelper, string actionName, int[] permissions)
        //{
        //    if (htmlHelper.HasPermissions(permissions))
        //    {
        //        return htmlHelper.Action(actionName);
        //    }
        //    return new MvcHtmlString(string.Empty);
        //}

        //public static MvcHtmlString ActionPermission(this HtmlHelper htmlHelper, string actionName, object routeValues, int permission)
        //{
        //    return ActionPermission(htmlHelper, actionName, routeValues, new int[1] { permission });
        //}

        //public static MvcHtmlString ActionPermission(this HtmlHelper htmlHelper, string actionName, object routeValues, int[] permissions)
        //{
        //    if (htmlHelper.HasPermissions(permissions))
        //    {
        //        return htmlHelper.Action(actionName, routeValues);
        //    }
        //    return new MvcHtmlString(string.Empty);
        //}

        //public static MvcHtmlString ActionPermission(this HtmlHelper htmlHelper, string actionName, RouteValueDictionary routeValues, int permission)
        //{
        //    return ActionPermission(htmlHelper, actionName, routeValues, new int[1] { permission });
        //}

        //public static MvcHtmlString ActionPermission(this HtmlHelper htmlHelper, string actionName, RouteValueDictionary routeValues, int[] permissions)
        //{
        //    if (htmlHelper.HasPermissions(permissions))
        //    {
        //        return htmlHelper.Action(actionName, routeValues);
        //    }
        //    return new MvcHtmlString(string.Empty);
        //}

        //public static MvcHtmlString ActionPermission(this HtmlHelper htmlHelper, string actionName, string controllerName, int permission)
        //{
        //    return ActionPermission(htmlHelper, actionName, controllerName, new int[1] { permission });
        //}

        //public static MvcHtmlString ActionPermission(this HtmlHelper htmlHelper, string actionName, string controllerName, int[] permissions)
        //{
        //    if (htmlHelper.HasPermissions(permissions))
        //    {
        //        return htmlHelper.Action(actionName, controllerName);
        //    }
        //    return new MvcHtmlString(string.Empty);
        //}

        //public static MvcHtmlString ActionPermission(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, int permission)
        //{
        //    return ActionPermission(htmlHelper, actionName, controllerName, routeValues, new int[1] { permission });
        //}

        //public static MvcHtmlString ActionPermission(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, int[] permissions)
        //{
        //    if (htmlHelper.HasPermissions(permissions))
        //    {
        //        return htmlHelper.Action(actionName, controllerName, routeValues);
        //    }
        //    return new MvcHtmlString(string.Empty);
        //}

        //public static MvcHtmlString ActionPermission(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, int permission)
        //{
        //    return ActionPermission(htmlHelper, actionName, controllerName, routeValues, new int[1] { permission });
        //}

        //public static MvcHtmlString ActionPermission(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, int[] permissions)
        //{
        //    if (htmlHelper.HasPermissions(permissions))
        //    {
        //        return htmlHelper.Action(actionName, controllerName, routeValues);
        //    }
        //    return new MvcHtmlString(string.Empty);
        //}

        #endregion

        #region Controle de visibilidade, e verificações baseadas no controller e action correntes.

        /// <summary>
        /// Método que retorna para a view o nome do controller da requisição
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString ControllerName(this HtmlHelper helper)
        {
            return new MvcHtmlString(helper.ViewContext.RouteData.Values["controller"].ToString());
        }

        /// <summary>
        /// Método que retorna para a view o nome da action da requisição
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString ActionName(this HtmlHelper helper)
        {
            return new MvcHtmlString(helper.ViewContext.RouteData.Values["action"].ToString());
        }

        /// <summary>
        /// Método que verifica se a action corrente é umas das actions informadas.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="actionNames">Relação de nomes das actions separadas pelo caracter separador.</param>
        /// <param name="separatorCharacter">Caracter separador. Valor default ','.</param>
        /// <returns></returns>
        public static bool IsAction(this HtmlHelper helper, string actionNames)
        {
            return IsAction(helper, actionNames, ',');
        }
        public static bool IsAction(this HtmlHelper helper, string actionNames, char separatorCharacter)
        {
            var currentAction = helper.ViewContext.RouteData.Values["action"].ToString();
            var requiredActions = actionNames.Split(separatorCharacter);

            return requiredActions.Contains(currentAction);
        }

        /// <summary>
        /// Método que verifica se a action corrente é umas das actions informadas
        /// e se o controller corrente um dos controllers informado.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="actionNames">Relação de nomes das actions separadas pelo caracter separador.</param>
        /// <param name="controllerNames">Relação de nomes de controllers separados pelo caracter separador.</param>
        /// <param name="separatorCharacter">Caracter separador. Valor default ','.</param>
        /// <returns></returns>
        public static bool IsAction(this HtmlHelper helper, string actionNames, string controllerNames)
        {
            return IsAction(helper, actionNames, controllerNames, ',');
        }
        public static bool IsAction(this HtmlHelper helper, string actionNames, string controllerNames, char separatorCharacter)
        {
            var currentAction = helper.ViewContext.RouteData.Values["action"].ToString();
            var currentController = helper.ViewContext.RouteData.Values["controller"].ToString();
            var requiredActions = actionNames.Split(separatorCharacter);
            var requiredControllers = controllerNames.Split(separatorCharacter);

            return (requiredActions.Contains(currentAction) && requiredControllers.Contains(currentController));
        }

        /// <summary>
        /// Método que coloca um htmlAttribute class para ativar o elemento html,
        /// caso a action atual esteja na relação de actions informadas no parametro actionNames
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="cssActive">Classe de css para ativar o elemento.</param>
        /// <param name="actionNames">Relação de nomes das actions separadas pelo caracter separador.</param>
        /// <param name="separatorCharacter">Caracter separador. Valor default ','.</param>
        /// <returns></returns>
        public static MvcHtmlString HtmlAttributeClassActive(this HtmlHelper helper, string cssActive, string actionNames, char separatorCharacter = ',')
        {
            var currentAction = helper.ViewContext.RouteData.Values["action"].ToString();
            var requiredActions = actionNames.Split(separatorCharacter);

            return requiredActions.Contains(currentAction) ? new MvcHtmlString(string.Format("class='{0}'", cssActive)) : new MvcHtmlString(string.Empty);
        }

        /// <summary>
        /// Método que coloca um htmlAttribute class para ativar o elemento html,
        /// caso a action e controller atuais estejam nas relações de actions e de controllers.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="cssActive">Classe de css para ativar o elemento.</param>
        /// <param name="actionNames">Relação de nomes das actions separadas pelo caracter separador.</param>
        /// <param name="controllerNames">Relação de nomes de controllers separados pelo caracter separador.</param>
        /// <param name="separatorCharacter">Caracter separador. Valor default ','.</param>
        /// <returns></returns>
        public static MvcHtmlString HtmlAttributeClassActive(this HtmlHelper helper, string cssActive, string actionNames, string controllerNames, char separatorCharacter = ',')
        {
            var currentAction = helper.ViewContext.RouteData.Values["action"].ToString();
            var currentController = helper.ViewContext.RouteData.Values["controller"].ToString();
            var requiredActions = actionNames.Split(separatorCharacter);
            var requiredControllers = controllerNames.Split(separatorCharacter);

            return (requiredActions.Contains(currentAction) && requiredControllers.Contains(currentController)) ? new MvcHtmlString(string.Format("class='{0}'", cssActive)) : new MvcHtmlString(string.Empty);
        }


        /// <summary>
        /// Método que retorna a Classe de css que ativa elemento de html,
        /// caso a action corrente esteja na relação de actions.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="cssActive">Classe de css para ativar o elemento.</param>
        /// <param name="actionNames"Relação de nomes das actions separadas pelo caracter separador.</param>
        /// <param name="separatorCharacter">Caracter separador. Valor default ','.</param>
        /// <returns></returns>
        public static MvcHtmlString AddClassActive(this HtmlHelper helper, string cssActive, string actionNames, char separatorCharacter = ',')
        {
            var currentActionName = helper.ViewContext.RouteData.Values["action"].ToString();
            var requiredActions = actionNames.Split(separatorCharacter);

            return new MvcHtmlString(requiredActions.Contains(currentActionName) ? cssActive : string.Empty);
        }

        /// <summary>
        /// Método que retorna a Classe de css para ativar o elemento html,
        /// caso a action e controller atuais estejam nas relações de actions e de controllers.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="cssActive">Classe de css para ativar o elemento.</param>
        /// <param name="actionNames">Relação de nomes das actions separadas pelo caracter separador.</param>
        /// <param name="controllerNames">Relação de nomes de controllers separados pelo caracter separador.</param>
        /// <param name="separatorCharacter">Caracter separador. Valor default ','.</param>
        /// <returns></returns>
        public static MvcHtmlString AddClassActive(this HtmlHelper helper, string cssActive, string actionNames, string controllerNames, char separatorCharacter = ',')
        {
            var currentActionName = helper.ViewContext.RouteData.Values["action"].ToString();
            var currentController = helper.ViewContext.RouteData.Values["controller"].ToString();
            var requiredActions = actionNames.Split(separatorCharacter);
            var requiredControllers = controllerNames.Split(separatorCharacter);

            return new MvcHtmlString((requiredActions.Contains(currentActionName) && requiredControllers.Contains(currentController)) ? cssActive : string.Empty);
        }

        /// <summary>
        /// Método que retorna Html Attribute Class com a classe de css que torna o componente oculto,
        /// baseado no valor da propriedade hide.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="cssHide">Classe css para tornar o componente oculto.</param>
        /// <param name="hide">Variavel de controle do uso ou não da classe de css para ocultar.</param>
        /// <returns></returns>
        public static MvcHtmlString HtmlAttributeClassHide(this HtmlHelper helper, string cssHide, bool hide)
        {
            return new MvcHtmlString(hide ? string.Format("class='{0}'", cssHide) : string.Empty);
        }

        /// <summary>
        /// Método que retorna a classe de css que torna o componente oculto,
        /// baseado no valor da propriedade hide.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="cssHide">Classe css para tornar o componente oculto.</param>
        /// <param name="hide">Variavel de controle do uso ou não da classe de css para ocultar.</param>
        /// <returns></returns>
        public static MvcHtmlString AddClassHide(this HtmlHelper helper, string cssHide, bool hide)
        {
            return new MvcHtmlString(hide ? cssHide : string.Empty);
        }

        #endregion

        #region Zebrado

        /// <summary>
        /// Método que retorna o a propriedade html class= com a classe de css para zebrar as posições pares
        /// de uma lista, criando efeito de zebrado.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="classeCss">Classe css para diferenciar item da lista.</param>
        /// <param name="position">Posição no indice da lista.</param>
        /// <returns></returns>
        public static MvcHtmlString HtmlAttributeClassZebrado(this HtmlHelper helper, string classeCss, int position)
        {
            return (position % 2) == 0 ? new MvcHtmlString(string.Format("class='{0}'", classeCss)) : new MvcHtmlString(string.Empty);
        }

        /// <summary>
        /// Método que retorna a propriedade html class= com a classe de css de acordo com a posição na lista
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="classePar">Classe css para indice par.</param>
        /// <param name="classeInpar">Classe css para indice impar.</param>
        /// <param name="position">Posição no indice da lista.</param>
        /// <returns></returns>
        public static MvcHtmlString HtmlAttributeClassZebrado(this HtmlHelper helper, string classePar, string classeInpar, int position)
        {
            return new MvcHtmlString(string.Format("class='{0}'", (position % 2) == 0 ? classePar : classeInpar));
        }

        /// <summary>
        /// Método que retorna somente a classe css caso a posição no indice seja par
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="classeCss">Classe css para Zebrar.</param>
        /// <param name="position">Posição no indice.</param>
        /// <returns></returns>
        public static MvcHtmlString AddClassZebrado(this HtmlHelper helper, string classeCss, int position)
        {
            return (position % 2) == 0 ? new MvcHtmlString(classeCss) : new MvcHtmlString(string.Empty);
        }

        /// <summary>
        /// Metodo que retorna somente a classe css, referente a posição no indice.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="classePar">Classe css para zebrar as posições pares.</param>
        /// <param name="classeInpar">Classe css para zebrar as posições impares.</param>
        /// <param name="position">Posição no indice.</param>
        /// <returns></returns>
        public static MvcHtmlString AddClassZebrado(this HtmlHelper helper, string classePar, string classeInpar, int position)
        {
            return new MvcHtmlString((position % 2) == 0 ? classePar : classeInpar);
        }

        #endregion

        #region Status Message

        /// <summary>Statuses the message toastr.</summary>
        /// <param name="helper">The helper.</param>
        /// <returns></returns>
        public static MvcHtmlString StatusMessageToastr(this HtmlHelper helper)
        {
            var msg = helper.ViewContext.TempData["ToastrStatusMessageText"]?.ToString() ?? "";
            var type = helper.ViewContext.TempData["ToastrStatusMessageType"]?.ToString() ?? "";
            var isFixed = helper.ViewContext.TempData["ToastrStatusMessageIsFixed"]?.ToString() ?? "";

            var html = string.Empty;
            if (!string.IsNullOrEmpty(msg))
            {
                html = $"MyRio2cCommon.showAlert({{ message: '{msg}', messageType: '{type}', isFixed: {isFixed} }});";
            }

            var htmlOutput = MvcHtmlString.Create(html);

            return new MvcHtmlString(htmlOutput.ToHtmlString());
        }

        /// <summary>
        /// Método que captura variaveis de mensagem de status no tempData do controller e apresenta com utilização do jquery.
        /// Tem dependência com o Js da aplicação hospedeira.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString StatusMessage(this HtmlHelper helper)
        {
            var msg = helper.ViewContext.TempData["StatusMessageText"] != null ? helper.ViewContext.TempData["StatusMessageText"].ToString() : "";
            var type = helper.ViewContext.TempData["StatusMessageCssClass"] != null ? helper.ViewContext.TempData["StatusMessageCssClass"] : "";

            var html = !string.IsNullOrEmpty(msg) ? string.Format("<div id=\"Status_message\" class=\"notify alert alert-{0}\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button>{1}</div>", type, msg) : null;

            return new MvcHtmlString(html);
        }


        /// <summary>
        /// Método que captura variaveis de mensagem de status no tempData do controller e apresenta com utilização do jquery.
        /// Tem dependência com o Js da aplicação hospedeira.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString StatusMessageModal(this HtmlHelper helper)
        {
            var msg = helper.ViewContext.TempData["StatusMessageModalText"] != null ? helper.ViewContext.TempData["StatusMessageModalText"].ToString() : "";
            var type = helper.ViewContext.TempData["StatusMessageModalCssClass"] != null ? helper.ViewContext.TempData["StatusMessageModalCssClass"] : "";

            var html = !string.IsNullOrEmpty(msg) ? string.Format("<div class=\"modal fade {1} in\" role=\"dialog\" id=\"modalStatusMessage\" tabindex=\"-1\" aria-labelledby=\"myModalLabel\"> <div class=\"modal-dialog\" role=\"document\"> <div class=\"modal-content\"> <div class=\"modal-header\"> <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button> </div> <div class=\"modal-body text-{1}\">{0}</div> </div> </div> </div>", msg, type) : null;

            var htmlOutput = MvcHtmlString.Create(html);

            return new MvcHtmlString(htmlOutput.ToHtmlString());
        }

        // <summary>
        /// Método que captura variaveis de mensagem de status no tempData do controller e apresenta com utilização do jquery.
        /// Tem dependência com o Js da aplicação hospedeira.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string StatusMessagesJsonString(this HtmlHelper helper)
        {
            var statusMessages = helper.ViewContext.TempData["StatusMessages"] != null ? helper.ViewContext.TempData["StatusMessages"] as List<object> : null;

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };

            var result = serializer.Serialize(statusMessages);

            return result;
        }

        // <summary>
        /// Método que captura variaveis de mensagem de status no tempData do controller e apresenta com utilização do jquery.
        /// Tem dependência com o Js da aplicação hospedeira.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString StatusMessages(this HtmlHelper helper)
        {
            var statusMessages = helper.ViewContext.TempData["StatusMessages"] != null ? helper.ViewContext.TempData["StatusMessages"] as List<object> : null;
            bool hasMessage = false;
            var html = string.Empty;

            if (statusMessages != null)
            {
                try
                {
                    foreach (var statusMessage in statusMessages)
                    {
                        var message = statusMessage.GetType().GetProperty("Message").GetValue(statusMessage, null);

                        if (message != null && !string.IsNullOrWhiteSpace(message.ToString()))
                        {
                            hasMessage = true;
                            break;
                        }
                    }

                    if (hasMessage)
                    {
                        foreach (var statusMessage in statusMessages)
                        {
                            var type = statusMessage.GetType().GetProperty("Type").GetValue(statusMessage, null);
                            var message = statusMessage.GetType().GetProperty("Message").GetValue(statusMessage, null);

                            if (message != null && !string.IsNullOrWhiteSpace(message.ToString()))
                            {
                                html += string.Format("<div id=\"Status_message\" class=\"notify alert alert-{0}\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"> <span aria-hidden=\"true\">&times;</span> </button>{1}</div>", type, message);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            return new MvcHtmlString(html);
        }

        /// <summary>
        /// Método que captura variaveis de mensagem de status no tempData do controller e apresenta com utilização do jquery.
        /// Usado para partial views.
        /// Tem dependência com o Js da aplicação hospedeira.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString StatusMessagePartial(this HtmlHelper helper)
        {
            var msg = helper.ViewContext.TempData["StatusMessageTextPartial"] != null ? helper.ViewContext.TempData["StatusMessageTextPartial"].ToString() : "";
            var type = helper.ViewContext.TempData["StatusMessageCssClassPartial"] != null ? helper.ViewContext.TempData["StatusMessageCssClassPartial"].ToString() : "";

            var html = string.Format(@"<script> var _msg = ""{0}""; var _class = ""{1}""; if(_msg.length > 0 ){{jQuery(""#Status_message.alert"").html(_msg).removeClass(""error warning success"").addClass(_class);  StatusMessage.show();}}</script>", msg, type);

            return new MvcHtmlString(html);
        }

        public static MvcHtmlString Message(this HtmlHelper helper)
        {
            if (helper.ViewContext.TempData.ContainsKey("MessageText") && helper.ViewContext.TempData.ContainsKey("MessagetYPE"))
            {
                string msg = helper.ViewContext.TempData["MessageText"].ToString();
                MessageType type;

                if (!string.IsNullOrEmpty(msg) && Enum.TryParse<MessageType>(helper.ViewContext.TempData["MessagetYPE"].ToString(), out type))
                {
                    var types = new Dictionary<MessageType, string>();
                    types.Add(MessageType.SUCCESS, "success");
                    types.Add(MessageType.INFO, "info");
                    types.Add(MessageType.ERROR, "danger");
                    return new MvcHtmlString(string.Format(@"<div class=""alert alert-{0} message-alert"" role=""alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button><p>{1}</p></div>", types[type], msg));
                }
            }

            return new MvcHtmlString(string.Empty);
        }

        #endregion

        #region LabelRequired - Label com *

        /// <summary>
        /// LabelFor<TModel> que traz junto ao Label o *
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression">Expression Func para selecionar a propriedade do objeto de Model.</param>
        /// <returns></returns>
        public static MvcHtmlString LabelRequiredFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return LabelRequiredFor(html, expression, null);
        }

        /// <summary>
        /// LabelFor<TModel> que traz junto ao Label o *
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression">Expression Func para selecionar a propriedade do objeto de Model.</param>
        /// <param name="htmlAttributes">Attributos de html colocar no componente.</param>
        /// <returns></returns>
        public static MvcHtmlString LabelRequiredFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return LabelRequiredFor(html, expression, new RouteValueDictionary(htmlAttributes));
        }

        /// <summary>
        /// LabelFor<TModel> que traz junto ao Label o *
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression">Expression Func para selecionar a propriedade do objeto de Model.</param>
        /// <param name="htmlAttributes">Attributos de html colocar no componente.</param>
        /// <returns></returns>
        public static MvcHtmlString LabelRequiredFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            TagBuilder tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            tag.SetInnerText(labelText + " *");
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Label que traz junto ao Label o *
        /// </summary>
        /// <param name="html"></param>
        /// <param name="labelText">Texto do Label.</param>
        /// <returns></returns>
        public static MvcHtmlString LabelRequired(this HtmlHelper html, string labelText)
        {
            return LabelRequired(html, labelText, null);
        }

        /// <summary>
        /// Label que traz junto ao Label o *
        /// </summary>
        /// <param name="html"></param>
        /// <param name="labelText">Texto do Label.</param>
        /// <param name="htmlAttributes">Attributos de html colocar no componente.</param>
        /// <returns></returns>
        public static MvcHtmlString LabelRequired(this HtmlHelper html, string labelText, object htmlAttributes)
        {
            return LabelRequired(html, labelText, new RouteValueDictionary(htmlAttributes));
        }

        /// <summary>
        /// Label que traz junto ao Label o *
        /// </summary>
        /// <param name="html"></param>
        /// <param name="labelText">Texto do Label.</param>
        /// <param name="htmlAttributes">Attributos de html colocar no componente.</param>
        /// <returns></returns>
        public static MvcHtmlString LabelRequired(this HtmlHelper html, string labelText, IDictionary<string, object> htmlAttributes)
        {
            if (String.IsNullOrEmpty(labelText.Trim())) { return MvcHtmlString.Empty; }
            TagBuilder tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.SetInnerText(labelText.Trim() + " *");
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        #endregion

        #region Componentes

        /// <summary>
        /// Método que apresenta o valor de uma propriedade string de um model,
        /// com opção de texto padrão caso o valor do model seja nulo.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression">Expression Func para selecionar a propriedade do objeto de Model.</param>
        /// <param name="emptyText">Texto padrão a ser apresentado no caso de vazio ou nulo.</param>
        /// <returns></returns>
        public static MvcHtmlString PrintStringWithDefaultValue<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string emptyText = "")
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            if (metadata.Model == null || string.IsNullOrEmpty(metadata.Model.ToString()))
            {
                return MvcHtmlString.Create(emptyText);
            }

            return MvcHtmlString.Create(metadata.Model.ToString());
        }

        /// <summary>
        /// Método que apresenta o valor de uma propriedade boolean de um model,
        /// utilizando os textos personalizados para o valor true e para o valor false.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression">Expression Func para selecionar a propriedade do objeto de Model.</param>
        /// <param name="trueText">Texto para valor true.</param>
        /// <param name="falseText">Texto para valor false.</param>
        /// <returns></returns>
        public static MvcHtmlString PrintBooleanWithCustomTexts<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string trueText, string falseText)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            var model = (bool)(metadata.Model ?? false);

            return new MvcHtmlString(model ? trueText : falseText);
        }

        /// <summary>
        /// Método que formata e apresenta um texto trazendo os dois pontos ':' concatenado.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="text">Texto a receber os dois pontos.</param>
        /// <returns></returns>
        public static MvcHtmlString PrintTextWithColon(this HtmlHelper html, string text)
        {
            return new MvcHtmlString(text + ": ");
        }

        #region RadioButtonForSelectList

        /// <summary>
        /// Método que apresenta um RadioButton de um SelectList.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">Expression Func para selecionar a propriedade do objeto de Model.</param>
        /// <param name="listOfValues">Lista de SelectList com os valores.</param>
        /// <param name="position">Disposição dos componentes: horizontal/vertical</param>
        /// <returns></returns>
        public static MvcHtmlString RadioButtonForSelectList<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> listOfValues, Position position = Position.Horizontal)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string fullName = ExpressionHelper.GetExpressionText(expression);
            var sb = new StringBuilder();
            var prefix = htmlHelper.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix;
            if (listOfValues != null && listOfValues.Any())
            {
                // Create a radio button for each item in the list
                foreach (SelectListItem item in listOfValues)
                {
                    // Generate an id to be given to the radio button field
                    var id = string.Format("rb_{0}_{1}", fullName.Replace("[", "").Replace("]", "").Replace(".", "_"), item.Value);
                    var idInput = string.IsNullOrEmpty(prefix) ? id : string.Format("{0}_{1}", prefix, id);

                    // Create and populate a radio button using the existing html helpers
                    var label = htmlHelper.Label(id, item.Text);

                    var radio = htmlHelper.RadioButton(fullName, item.Value, item.Selected, new { id = idInput }).ToHtmlString();

                    // Create the html string that will be returned to the client
                    // e.g. <input data-val="true" data-val-required= "You must select an option" id="TestRadio_1" name="TestRadio" type="radio" value="1" /><label for="TestRadio_1">Line1</label>
                    sb.AppendFormat("<div class=\"sss-box-radio {2}\">{0}{1}</div>", radio, label, (position == Position.Horizontal ? "horizontal" : "vertical"));
                }
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        public enum Position
        {
            Horizontal = 0,
            Vertical = 1,
        }

        #endregion

        #region BeginCollectionItem

        /// <summary>
        /// Begins a collection item by inserting either a previously used .Index hidden field value for it or a new one.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="html"></param>
        /// <param name="collectionName">The name of the collection property from the Model that owns this item.</param>
        /// <returns></returns>
        public static IDisposable BeginCollectionItem<TModel>(this HtmlHelper<TModel> html, string collectionName, bool clearIndex = false)
        {
            if (String.IsNullOrEmpty(collectionName))
#pragma warning disable CSE0001 // Use nameof when passing parameter names as arguments
                throw new ArgumentException("collectionName is null or empty.", "collectionName");
#pragma warning restore CSE0001 // Use nameof when passing parameter names as arguments

            string collectionIndexFieldName = String.Format("{0}.Index", collectionName);

            string itemIndex = GetCollectionItemIndex(collectionIndexFieldName);

            string collectionItemName = String.Format("{0}[{1}]", collectionName, itemIndex);

            TagBuilder indexField = new TagBuilder("input");
            indexField.MergeAttributes(new Dictionary<string, string>() {
                { "name", collectionIndexFieldName },
                { "value", itemIndex },
                { "type", "hidden" },
                { "autocomplete", "off" }
            });

            html.ViewContext.Writer.WriteLine(indexField.ToString(TagRenderMode.SelfClosing));
            return new CollectionItemNamePrefixScope(html.ViewData.TemplateInfo, collectionItemName);
        }

        /// <summary>
        /// Begins a collection item by inserting either a previously used .Index hidden field value for it or a new one.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="html"></param>
        /// <param name="collectionName">The name of the collection property from the Model that owns this item.</param>
        /// <returns></returns>
        public static IDisposable BeginCollectionItem<TModel>(this HtmlHelper<TModel> html, string collectionName, Guid itemIndex)
        {
            if (String.IsNullOrEmpty(collectionName))
#pragma warning disable CSE0001 // Use nameof when passing parameter names as arguments
                throw new ArgumentException("collectionName is null or empty.", "collectionName");
#pragma warning restore CSE0001 // Use nameof when passing parameter names as arguments

            string collectionIndexFieldName = String.Format("{0}.Index", collectionName);

            string collectionItemName = String.Format("{0}[{1}]", collectionName, itemIndex.ToString());

            TagBuilder indexField = new TagBuilder("input");
            indexField.MergeAttributes(new Dictionary<string, string>() {
                { "name", collectionIndexFieldName },
                { "value", itemIndex.ToString() },
                { "type", "hidden" },
                { "autocomplete", "off" }
            });

            html.ViewContext.Writer.WriteLine(indexField.ToString(TagRenderMode.SelfClosing));
            return new CollectionItemNamePrefixScope(html.ViewData.TemplateInfo, collectionItemName);
        }

        private class CollectionItemNamePrefixScope : IDisposable
        {
            private readonly TemplateInfo _templateInfo;
            private readonly string _previousPrefix;

            public CollectionItemNamePrefixScope(TemplateInfo templateInfo, string collectionItemName)
            {
                this._templateInfo = templateInfo;

                _previousPrefix = templateInfo.HtmlFieldPrefix;
                templateInfo.HtmlFieldPrefix = collectionItemName;
            }

            public void Dispose()
            {
                _templateInfo.HtmlFieldPrefix = _previousPrefix;
            }
        }

        /// <summary>
        /// Tries to reuse old .Index values from the HttpRequest in order to keep the ModelState consistent
        /// across requests. If none are left returns a new one.
        /// </summary>
        /// <param name="collectionIndexFieldName"></param>
        /// <returns>a GUID string</returns>
        private static string GetCollectionItemIndex(string collectionIndexFieldName, bool clearIndex = false)
        {
            Queue<string> previousIndices = (Queue<string>)HttpContext.Current.Items[collectionIndexFieldName];
            if (previousIndices == null)
            {
                HttpContext.Current.Items[collectionIndexFieldName] = previousIndices = new Queue<string>();

                string previousIndicesValues = HttpContext.Current.Request[collectionIndexFieldName];
                if (!String.IsNullOrWhiteSpace(previousIndicesValues))
                {
                    foreach (string index in previousIndicesValues.Split(','))
                        previousIndices.Enqueue(index);
                }
            }

            if (!clearIndex)
            {
                return Guid.NewGuid().ToString();
            }

            return previousIndices.Count > 0 ? previousIndices.Dequeue() : Guid.NewGuid().ToString();
        }

        #endregion

        /// <summary>
        /// Helper para renderizar o hidden, checkbox e label para os checkboxs de permissão do sistema
        /// </summary>
        /// <typeparam name="TModel">Model da pagina</typeparam>
        /// <typeparam name="TValue">Propriedade da model que representa o checkbox</typeparam>
        /// <param name="html"></param>
        /// <param name="expression">A Expressão em Lambda para selecionar a propriedade</param>
        /// <returns></returns>
        public static MvcHtmlString PermissionCheckBoxFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var prefix = html.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix;

            //checkbox
            string checkboxName = ExpressionHelper.GetExpressionText(expression);
            bool checkValue = bool.Parse(metadata.SimpleDisplayText);

            //hidden
            int hiddenValue = 0;
            string hiddenName = string.Empty;

            //label
            string labelText = string.Empty;
            string lableName = string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (var property in html.ViewData.Model.GetType().GetProperties())
            {
                if (property.PropertyType == typeof(System.Int32))
                {
                    hiddenValue = (int)property.GetValue(html.ViewData.Model, null);
                    hiddenName = property.Name;
                }
                if (property.PropertyType == typeof(System.String))
                {
                    labelText = (string)property.GetValue(html.ViewData.Model, null);
                    lableName = property.Name;
                }
            }

            TagBuilder hidden = new TagBuilder("input");
            hidden.Attributes.Add("type", "hidden");
            hidden.Attributes.Add("name", string.Format("{0}.{1}", prefix, hiddenName));
            hidden.Attributes.Add("id", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(hiddenName));
            hidden.Attributes.Add("value", hiddenValue.ToString());
            sb.AppendLine(hidden.ToString(TagRenderMode.SelfClosing));

            TagBuilder checkbox = new TagBuilder("input");
            checkbox.Attributes.Add("type", "checkbox");
            checkbox.Attributes.Add("id", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(checkboxName));
            checkbox.Attributes.Add("name", string.Format("{0}.{1}", prefix, checkboxName));
            checkbox.Attributes.Add("value", "true");
            if (checkValue)
                checkbox.Attributes.Add("checked", "checked");
            sb.AppendLine(checkbox.ToString(TagRenderMode.SelfClosing));

            TagBuilder hidden2 = new TagBuilder("input");
            hidden2.Attributes.Add("type", "hidden");
            hidden2.Attributes.Add("name", string.Format("{0}.{1}", prefix, checkboxName));
            hidden2.Attributes.Add("value", "false");
            sb.AppendLine(hidden2.ToString(TagRenderMode.SelfClosing));

            TagBuilder label = new TagBuilder("label");
            label.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(checkboxName));
            label.SetInnerText(labelText);
            sb.AppendLine(label.ToString(TagRenderMode.Normal));

            TagBuilder valueLabel = new TagBuilder("input");
            valueLabel.Attributes.Add("type", "hidden");
            valueLabel.Attributes.Add("name", string.Format("{0}.{1}", prefix, lableName));
            valueLabel.Attributes.Add("id", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(lableName));
            valueLabel.Attributes.Add("value", labelText);
            sb.AppendLine(valueLabel.ToString(TagRenderMode.SelfClosing));

            return MvcHtmlString.Create(sb.ToString());
        }

        #region CustomAjaxActionLink

        /// <summary>
        /// Método que permite links de ajax customizado, possibilitando html como texto do link.
        /// </summary>
        /// <param name="ajaxHelper"></param>
        /// <param name="linkText"></param>
        /// <param name="actionName"></param>
        /// <param name="ajaxOptions"></param>
        /// <returns></returns>
        public static IHtmlString CustomAjaxActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, AjaxOptions ajaxOptions)
        {
            var targetUrl = UrlHelper.GenerateUrl(null, actionName, null, null, ajaxHelper.RouteCollection, ajaxHelper.ViewContext.RequestContext, true);
            return MvcHtmlString.Create(ajaxHelper.GenerateLink(linkText, targetUrl, ajaxOptions ?? new AjaxOptions(), null));
        }

        /// <summary>
        /// Método que permite links de ajax customizado, possibilitando html como texto do link.
        /// </summary>
        /// <param name="ajaxHelper"></param>
        /// <param name="linkText"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <param name="ajaxOptions"></param>
        /// <param name="htmlAtributes"></param>
        /// <returns></returns>
        public static IHtmlString CustomAjaxActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAtributes)
        {
            var targetUrl = UrlHelper.GenerateUrl(null, actionName, controllerName, null, null, null, new RouteValueDictionary(routeValues), ajaxHelper.RouteCollection, ajaxHelper.ViewContext.RequestContext, true);

            return MvcHtmlString.Create(ajaxHelper.GenerateLink(linkText, targetUrl, ajaxOptions, new RouteValueDictionary(htmlAtributes)));
        }

        private static string GenerateLink(this AjaxHelper ajaxHelper, string linkText, string targetUrl, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            var a = new TagBuilder("a") { InnerHtml = linkText };

            a.MergeAttributes<string, object>(htmlAttributes);
            a.MergeAttribute("href", targetUrl);
            a.MergeAttributes<string, object>(ajaxOptions.ToUnobtrusiveHtmlAttributes());

            return a.ToString(TagRenderMode.Normal);
        }

        #endregion

        #endregion

        /// <summary>Gets the unobtrusive validation attributes for.</summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <returns></returns>
        public static IHtmlString GetUnobtrusiveValidationAttributesFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> propertySelector)
        {
            string propertyName = html.NameFor(propertySelector).ToString();
            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(propertySelector, html.ViewData);
            IDictionary<string, object> attributeCollection = html.GetUnobtrusiveValidationAttributes(propertyName, metaData);

            return html.Raw(String.Join(" ", attributeCollection.Select(kvp => kvp.Key + "=\"" + kvp.Value.ToString() + "\"")));
        }
    }
}