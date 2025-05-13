// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Franco
// Created          : 07-02-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 07-02-2022
// ***********************************************************************
// <copyright file="CartoonController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Web.Site.Controllers;
using PlataformaRio2C.Web.Site.Filters;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Site.Areas.Cartoon.Controllers
{
    /// <summary>CartoonController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.CommissionCartoon)]
    public class CartoonController : BaseController
    {
        private readonly ICartoonProjectRepository cartoonProjectRepo;
        private readonly IProjectEvaluationStatusRepository evaluationStatusRepo;
        private readonly IProjectEvaluationRefuseReasonRepository projectEvaluationRefuseReasonRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartoonController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="cartoonProjectRepo">The cartoon project repo.</param>
        /// <param name="evaluationStatusRepo">The evaluation status repo.</param>
        /// <param name="projectEvaluationRefuseReasonRepo">The project evaluation refuse reason repo.</param>
        public CartoonController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            ICartoonProjectRepository cartoonProjectRepo,
            IProjectEvaluationStatusRepository evaluationStatusRepo,
            IProjectEvaluationRefuseReasonRepository projectEvaluationRefuseReasonRepo)
            : base(commandBus, identityController)
        {
            this.cartoonProjectRepo = cartoonProjectRepo;
            this.evaluationStatusRepo = evaluationStatusRepo;
            this.projectEvaluationRefuseReasonRepo = projectEvaluationRefuseReasonRepo;
        }
    }
}