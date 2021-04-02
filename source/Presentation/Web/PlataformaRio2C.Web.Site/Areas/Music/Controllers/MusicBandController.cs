// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 31-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 31-03-2021
// ***********************************************************************
// <copyright file="MusicBandController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Web.Mvc;
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.AuthorizeAttributes;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;
using PlataformaRio2C.Web.Site.Controllers;
using PlataformaRio2C.Web.Site.Filters;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Web.Site.Areas.Music.Controllers
{
    /// <summary>MusicBandController</summary>
    [AjaxAuthorize(Order = 1)]
    [AuthorizeCollaboratorType(Order = 2, Types = Constants.CollaboratorType.CommissionMusic)]
    public class MusicBandController : BaseController
    {
        private readonly IMusicProjectRepository musicProjectRepo;
        private readonly IMusicBandRepository musicBandRepo;
        private readonly IMusicGenreRepository musicGenreRepo;
        private readonly IProjectEvaluationStatusRepository evaluationStatusRepo;
        private readonly IProjectEvaluationRefuseReasonRepository projectEvaluationRefuseReasonRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBandController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="musicProjectRepo">The music project repo.</param>
        /// <param name="musicBandRepo">The music band repo.</param>
        /// <param name="musicGenreRepo">The music genre repo.</param>
        /// <param name="evaluationStatusRepo">The evaluation status repo.</param>
        /// <param name="projectEvaluationRefuseReasonRepo">The project evaluation refuse reason repo.</param>
        public MusicBandController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IMusicProjectRepository musicProjectRepo,
            IMusicBandRepository musicBandRepo,
            IMusicGenreRepository musicGenreRepo,
            IProjectEvaluationStatusRepository evaluationStatusRepo,
            IProjectEvaluationRefuseReasonRepository projectEvaluationRefuseReasonRepo)
            : base(commandBus, identityController)
        {
            this.musicProjectRepo = musicProjectRepo;
            this.musicBandRepo = musicBandRepo;
            this.musicGenreRepo = musicGenreRepo;
            this.evaluationStatusRepo = evaluationStatusRepo;
            this.projectEvaluationRefuseReasonRepo = projectEvaluationRefuseReasonRepo;
        }
    }
}