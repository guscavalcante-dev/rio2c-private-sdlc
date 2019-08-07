// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 07-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="ErrorController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Web.Admin.Controllers
{
    /// <summary>ErrorController</summary>
    public class ErrorController : BaseController
    {
        /// <summary>Initializes a new instance of the <see cref="ErrorController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        public ErrorController(IMediator commandBus, IdentityAutenticationService identityController)
            : base(commandBus, identityController)
        {
        }

        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.ErrorCode = 500;
            ViewBag.Message = Texts.ErrorMessage;
            Response.TrySkipIisCustomErrors = true;

            // If to avoid showing "Moved objecto to here" on browser
            if (!string.IsNullOrEmpty(RouteData.Values["culture"] as string))
            {
                Response.StatusCode = ViewBag.ErrorCode;
            }

            if (!Request.IsAjaxRequest())
            {
                return View();
            }

            return Json(new { status = "error", message = ViewBag.Message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Nots the found.</summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            ViewBag.ErrorCode = 404;

            // If to avoid showing "Moved objecto to here" on browser
            if (!string.IsNullOrEmpty(RouteData.Values["culture"] as string))
            {
                Response.StatusCode = ViewBag.ErrorCode;
            }

            ViewBag.Message = Texts.NotFoundErrorMessage;
            Response.TrySkipIisCustomErrors = true;

            if (!Request.IsAjaxRequest())
            {
                return View("Index");
            }

            return Json(new { status = "error", message = ViewBag.Message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Forbiddens this instance.</summary>
        /// <returns></returns>
        public ActionResult Forbidden()
        {
            ViewBag.ErrorCode = 403;
            ViewBag.Message = Texts.ForbiddenErrorMessage;
            Response.TrySkipIisCustomErrors = true;

            // If to avoid showing "Moved objecto to here" on browser
            if (!string.IsNullOrEmpty(RouteData.Values["culture"] as string))
            {
                Response.StatusCode = ViewBag.ErrorCode;
            }

            if (!Request.IsAjaxRequest())
            {
                return View("Index");
            }

            return Json(new { status = "error", message = ViewBag.Message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Called before the action method is invoked.</summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.TrySkipIisCustomErrors = true;
            base.OnActionExecuting(filterContext);
        }
    }
}