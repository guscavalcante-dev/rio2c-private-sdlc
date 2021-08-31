// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-20-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-28-2021
// ***********************************************************************
// <copyright file="UpdateEditionDatesInformationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateEditionDatesInformationCommandHandler</summary>
    public class UpdateEditionDatesInformationCommandHandler : EditionBaseCommandHandler, IRequestHandler<UpdateEditionDatesInformation, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IAttendeeMusicBandRepository attendeeMusicBandRepo;
        private readonly IAttendeeInnovationOrganizationRepository attendeeInnovationOrganizationRepo;
        private readonly IProjectRepository projectRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEditionDatesInformationCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="attendeeMusicBandRepository">The attendee music band repository.</param>
        public UpdateEditionDatesInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IEditionRepository editionRepository,
            IAttendeeMusicBandRepository attendeeMusicBandRepository,
            IAttendeeInnovationOrganizationRepository attendeeInnovationOrganizationRepository,
            IProjectRepository projectRepository
            )
            : base(eventBus, uow, editionRepository)
        {
            this.editionRepo = editionRepository;
            this.attendeeMusicBandRepo = attendeeMusicBandRepository;
            this.attendeeInnovationOrganizationRepo = attendeeInnovationOrganizationRepository;
            this.projectRepo = projectRepository;
        }

        /// <summary>
        /// Handles the specified update edition dates information.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateEditionDatesInformation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var edition = await this.GetEditionByUid(cmd.EditionUid);

            bool changedMusicCommissionMinimumEvaluationsCount = edition.MusicCommissionMinimumEvaluationsCount != cmd.MusicCommissionMinimumEvaluationsCount;
            bool changedInnovationCommissionMinimumEvaluationsCount = edition.InnovationCommissionMinimumEvaluationsCount != cmd.InnovationCommissionMinimumEvaluationsCount;
            bool changedAudiovisualCommissionMinimumEvaluationsCount = edition.AudiovisualCommissionMinimumEvaluationsCount != cmd.AudiovisualCommissionMinimumEvaluationsCount;

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            edition.UpdateDatesInformation(
                cmd.ProjectSubmitStartDate.Value,
                cmd.ProjectSubmitEndDate.Value,
                cmd.ProjectEvaluationStartDate.Value,
                cmd.ProjectEvaluationEndDate.Value,
                cmd.NegotiationStartDate.Value,
                cmd.NegotiationEndDate.Value,
                cmd.AttendeeOrganizationMaxSellProjectsCount.Value,
                cmd.ProjectMaxBuyerEvaluationsCount.Value,

                cmd.MusicProjectSubmitStartDate.Value,
                cmd.MusicProjectSubmitEndDate.Value,
                cmd.MusicCommissionEvaluationStartDate.Value,
                cmd.MusicCommissionEvaluationEndDate.Value,
                cmd.MusicCommissionMinimumEvaluationsCount.Value,
                cmd.MusicCommissionMaximumApprovedBandsCount.Value,

                cmd.InnovationProjectSubmitStartDate.Value,
                cmd.InnovationProjectSubmitEndDate.Value,
                cmd.InnovationCommissionEvaluationStartDate.Value,
                cmd.InnovationCommissionEvaluationEndDate.Value,
                cmd.InnovationCommissionMinimumEvaluationsCount.Value,
                cmd.InnovationCommissionMaximumApprovedCompaniesCount.Value,

                cmd.AudiovisualNegotiationsCreateStartDate.Value,
                cmd.AudiovisualNegotiationsCreateEndDate.Value,
                cmd.AudiovisualCommissionEvaluationStartDate.Value,
                cmd.AudiovisualCommissionEvaluationEndDate.Value,
                cmd.AudiovisualCommissionMinimumEvaluationsCount.Value,
                cmd.AudiovisualCommissionMaximumApprovedProjectsCount.Value,

                cmd.UserId);

            if (!edition.IsValid())
            {
                this.AppValidationResult.Add(edition.ValidationResult);
                return this.AppValidationResult;
            }

            #region Before Save

            // Music Projects Grades must be recalculated when changed "MusicCommissionMinimumEvaluationsCount"
            if (changedMusicCommissionMinimumEvaluationsCount)
            {
                var attendeeMusicBands = await this.attendeeMusicBandRepo.FindAllByEditionIdAsync(edition.Id);
                attendeeMusicBands.ForEach(amb => amb.RecalculateGrade(edition));
                this.attendeeMusicBandRepo.UpdateAll(attendeeMusicBands);
            }

            // Innovation Projects Grades must be recalculated when changed "InnovationCommissionMinimumEvaluationsCount"
            if (changedInnovationCommissionMinimumEvaluationsCount)
            {
                var attendeeInnovationOrganizations = await this.attendeeInnovationOrganizationRepo.FindAllByEditionIdAsync(edition.Id);
                attendeeInnovationOrganizations.ForEach(amb => amb.RecalculateGrade(edition));
                this.attendeeInnovationOrganizationRepo.UpdateAll(attendeeInnovationOrganizations);
            }

            // Audiovisual Projects Grades must be recalculated when changed "AudiovisualCommissionMinimumEvaluationsCount"
            if (changedAudiovisualCommissionMinimumEvaluationsCount)
            {
                var projects = await this.projectRepo.FindAllByEditionIdAsync(edition.Id);
                projects.ForEach(amb => amb.RecalculateGrade(edition));
                this.projectRepo.UpdateAll(projects);
            }

            #endregion

            this.EditionRepo.Update(edition);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}