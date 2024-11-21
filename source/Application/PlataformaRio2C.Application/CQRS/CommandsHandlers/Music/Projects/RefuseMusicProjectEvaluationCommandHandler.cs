// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 11-10-2024
// ***********************************************************************
// <copyright file="RefuseMusicProjectEvaluationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>RefuseMusicProjectEvaluationCommandHandler</summary>
    public class RefuseMusicProjectEvaluationCommandHandler : MusicProjectBaseCommandHandler, IRequestHandler<RefuseMusicProjectEvaluation, AppValidationResult>
    {
        private readonly IProjectEvaluationStatusRepository projectEvaluationStatusRepo;
        private readonly IMusicBandRepository musicBandRepo;
        private readonly IEditionRepository editionRepo;
        private readonly IUserRepository userRepo;
        private readonly IAttendeeMusicBandEvaluationRepository attendeeMusicBandEvaluationRepo;

        /// <summary>Initializes a new instance of the <see cref="RefuseMusicProjectEvaluationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="musicProjectRepository">The music project repository.</param>
        /// <param name="projectEvaluationStatusRepository">The project evaluation status repository.</param>
        /// <param name="musicBandRepo">The project evaluation status repository.</param>
        /// <param name="editionRepo">The project evaluation status repository.</param>
        /// <param name="userRepo">The user repo.</param>
        /// <param name="attendeeMusicBandEvaluationRepo">The attendee music band evaluation repo.</param>
        public RefuseMusicProjectEvaluationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IMusicProjectRepository musicProjectRepository,
            IProjectEvaluationStatusRepository projectEvaluationStatusRepository,
            IMusicBandRepository musicBandRepo,
            IEditionRepository editionRepo,
            IUserRepository userRepo,
            IAttendeeMusicBandEvaluationRepository attendeeMusicBandEvaluationRepo
        )
            : base(eventBus, uow, musicProjectRepository)
        {
            this.projectEvaluationStatusRepo = projectEvaluationStatusRepository;
            this.musicBandRepo = musicBandRepo;
            this.editionRepo = editionRepo;
            this.userRepo = userRepo;
            this.attendeeMusicBandEvaluationRepo = attendeeMusicBandEvaluationRepo;
        }

        /// <summary>Handles the specified refuse music project evaluation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(RefuseMusicProjectEvaluation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var editionDto = await editionRepo.FindDtoAsync(cmd.EditionId.Value);
            if (editionDto.IsMusicProjectEvaluationOpen() != true)
            {
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(Texts.ForbiddenErrorMessage, new string[] { "ToastrError" })));
                return this.AppValidationResult;
            }

            var evaluationsCount = await this.attendeeMusicBandEvaluationRepo.CountByCollaboratorIdAsync(
                editionDto.Id,
                cmd.UserId
            );
            if (evaluationsCount + 1 > editionDto.MusicPitchingMaximumApprovedProjectsByCommissionMember)
            {
                string validationMessage = string.Format(
                    Messages.YouCanMusicPitchingMaximumApprovedProjectsByCommissionMember,
                    editionDto.MusicPitchingMaximumApprovedProjectsByCommissionMember,
                    Labels.MusicProjects
                );
                this.ValidationResult.Add(new ValidationError(validationMessage));
            }

            var projectEvaluationStatuses = await this.projectEvaluationStatusRepo.FindAllAsync();
            var musicBand = await this.musicBandRepo.FindByUidAsync(cmd.MusicBandUid.Value);

            musicBand.ComissionEvaluation(
                editionDto.Edition,
                await userRepo.FindByIdAsync(cmd.UserId),
                projectEvaluationStatuses?.FirstOrDefault(pes => pes.Code == ProjectEvaluationStatus.Refused.Code)
            );

            if (!musicBand.IsValid())
            {
                this.AppValidationResult.Add(musicBand.ValidationResult);
                return this.AppValidationResult;
            }

            this.musicBandRepo.Update(musicBand);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = musicBand;

            return this.AppValidationResult;
        }
    }
}