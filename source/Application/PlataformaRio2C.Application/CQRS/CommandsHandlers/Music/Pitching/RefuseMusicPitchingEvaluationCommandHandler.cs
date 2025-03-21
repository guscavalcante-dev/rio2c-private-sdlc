// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 12-02-2024
// ***********************************************************************
// <copyright file="RefuseMusicPitchingEvaluationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>RefuseMusicPitchingEvaluationCommandHandler</summary>
    public class RefuseMusicPitchingEvaluationCommandHandler : MusicProjectBaseCommandHandler, IRequestHandler<RefuseMusicPitchingEvaluation, AppValidationResult>
    {
        private readonly IProjectEvaluationStatusRepository projectEvaluationStatusRepo;
        private readonly IMusicBandRepository musicBandRepo;
        private readonly IEditionRepository editionRepo;
        private readonly IUserRepository userRepo;
        private readonly IAttendeeMusicBandEvaluationRepository attendeeMusicBandEvaluationRepo;
        private readonly IAttendeeMusicBandRepository attendeeMusicBandRepo;

        /// <summary>Initializes a new instance of the <see cref="RefuseMusicPitchingEvaluationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="musicProjectRepository">The music project repository.</param>
        /// <param name="projectEvaluationStatusRepository">The project evaluation status repository.</param>
        /// <param name="musicBandRepo">The project evaluation status repository.</param>
        /// <param name="editionRepo">The project evaluation status repository.</param>
        /// <param name="userRepo">The user repo.</param>
        /// <param name="attendeeMusicBandEvaluationRepo">The attendee music band evaluation repo.</param>
        /// <param name="attendeeMusicBandRepo">The attendee music music band repo.</param>
        public RefuseMusicPitchingEvaluationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IMusicProjectRepository musicProjectRepository,
            IProjectEvaluationStatusRepository projectEvaluationStatusRepository,
            IMusicBandRepository musicBandRepo,
            IEditionRepository editionRepo,
            IUserRepository userRepo,
            IAttendeeMusicBandEvaluationRepository attendeeMusicBandEvaluationRepo,
            IAttendeeMusicBandRepository attendeeMusicBandRepo
        )
            : base(eventBus, uow, musicProjectRepository)
        {
            this.projectEvaluationStatusRepo = projectEvaluationStatusRepository;
            this.musicBandRepo = musicBandRepo;
            this.editionRepo = editionRepo;
            this.userRepo = userRepo;
            this.attendeeMusicBandEvaluationRepo = attendeeMusicBandEvaluationRepo;
            this.attendeeMusicBandRepo = attendeeMusicBandRepo;
        }

        /// <summary>Handles the specified refuse music project evaluation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(RefuseMusicPitchingEvaluation cmd, CancellationToken cancellationToken)
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
            var evaluator = await userRepo.FindByIdAsync(cmd.UserId);
            var musicBand = await this.musicBandRepo.FindByUidAsync(cmd.MusicBandUid.Value);

            var isCommissionMusicCurator = cmd.UserAccessControlDto.IsCommissionMusicCurator();
            if (isCommissionMusicCurator)
            {
                if (editionDto.IsMusicPitchingCuratorEvaluationOpen())
                {
                    #region Curator Evaluation

                    musicBand.CuratorEvaluation(editionDto.Edition, evaluator, ProjectEvaluationStatus.Refused.Id);

                    #endregion
                }
                else if (editionDto.IsMusicPitchingRepechageEvaluationOpen())
                {
                    #region Repechage Evaluation

                    var disapprovalByPopulation = await this.attendeeMusicBandEvaluationRepo.CountByPopularEvaluationAsync(
                        editionDto.Id,
                        musicBand.Id,
                        ProjectEvaluationStatus.Refused.Id
                    );

                    if (disapprovalByPopulation == 0)
                    {
                        this.ValidationResult.Add(new ValidationError(Messages.ProjectMustBeDisapprovedByPopulation));
                        this.AppValidationResult.Add(this.ValidationResult);
                        return this.AppValidationResult;
                    }

                    musicBand.RepechageEvaluation(editionDto.Edition, evaluator, ProjectEvaluationStatus.Refused.Id);

                    #endregion
                }
                else
                {
                    this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(Messages.EvaluationPeriodClosed, new string[] { "ToastrError" })));
                    return this.AppValidationResult;
                }
            }
            else
            {
                #region Commission Evaluation

                if (editionDto.IsMusicPitchingComissionEvaluationOpen() == false)
                {
                    this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(Messages.EvaluationPeriodClosed, new string[] { "ToastrError" })));
                    return this.AppValidationResult;
                }

                // Check if the music band has already been evaluated by another user
                var attendeeMusicBand = await this.attendeeMusicBandRepo.FindByMusicBandIdAsync(editionDto.Id, musicBand.Id);
                var lastAttendeeMusicBandEvaluation = attendeeMusicBand?.GetLastAttendeeMusicBandEvaluation(cmd.UserAccessControlDto);
                if (lastAttendeeMusicBandEvaluation != null && lastAttendeeMusicBandEvaluation.EvaluatorUserId != cmd.UserId)
                {
                    this.ValidationResult.Add(new ValidationError(Messages.ThisBandHasBeenEvaluatedByAnotherUser));
                    this.AppValidationResult.Add(this.ValidationResult);
                    return this.AppValidationResult;
                }

                musicBand.ComissionEvaluation(editionDto.Edition, evaluator, ProjectEvaluationStatus.Refused.Id);

                #endregion
            }

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