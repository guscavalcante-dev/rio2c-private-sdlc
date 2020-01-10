//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Web.Site
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-28-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-07-2019
//// ***********************************************************************
//// <copyright file="ConferenceController.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using System.Web.Mvc;
//using MediatR;
//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.Infra.CrossCutting.Identity.Service;

//namespace PlataformaRio2C.Web.Site.Areas.Producer.Controllers
//{
//    /// <summary>ConferenceController</summary>
//    [Authorize(Order = 1, Roles = "Producer")]
//    public class ConferenceController : PlataformaRio2C.Web.Site.Controllers.ConferenceController
//    {
//        /// <summary>Initializes a new instance of the <see cref="ConferenceController"/> class.</summary>
//        /// <param name="commandBus">The command bus.</param>
//        /// <param name="identityController">The identity controller.</param>
//        /// <param name="appService">The application service.</param>
//        public ConferenceController(IMediator commandBus, IdentityAutenticationService identityController, IConferenceAppService appService) 
//            : base(commandBus, identityController, appService)
//        {
//        }
//    }
//}