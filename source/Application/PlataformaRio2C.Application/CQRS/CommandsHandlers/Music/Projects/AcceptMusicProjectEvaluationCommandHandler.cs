// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="AcceptMusicProjectEvaluationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>AcceptProjectEvaluationCommandHandler</summary>
    public class AcceptMusicProjectEvaluationCommandHandler : BaseMusicProjectCommandHandler, IRequestHandler<AcceptMusicProjectEvaluation, AppValidationResult>
    {
        private IProjectEvaluationStatusRepository projectEvaluationStatusRepo;

        /// <summary>Initializes a new instance of the <see cref="AcceptMusicProjectEvaluationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="musicProjectRepository">The music project repository.</param>
        /// <param name="projectEvaluationStatusRepository">The project evaluation status repository.</param>
        public AcceptMusicProjectEvaluationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IMusicProjectRepository musicProjectRepository,
            IProjectEvaluationStatusRepository projectEvaluationStatusRepository)
            : base(eventBus, uow, musicProjectRepository)
        {
            this.projectEvaluationStatusRepo = projectEvaluationStatusRepository;
        }

        /// <summary>Handles the specified accept music project evaluation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(AcceptMusicProjectEvaluation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var musicProject = await this.GetMusicProjectByUid(cmd.ProjectUid ?? Guid.Empty);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            musicProject.Accept(
                await this.projectEvaluationStatusRepo.FindAllAsync(),
                cmd.UserId);
            if (!musicProject.IsEvaluationValid())
            {
                this.AppValidationResult.Add(musicProject.ValidationResult);
                return this.AppValidationResult;
            }

            this.MusicProjectRepo.Update(musicProject);
            this.Uow.SaveChanges();
            //this.AppValidationResult.Data = project;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}