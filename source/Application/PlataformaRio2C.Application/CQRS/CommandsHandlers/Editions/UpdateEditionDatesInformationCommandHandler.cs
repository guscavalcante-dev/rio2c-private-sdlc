// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-20-2021
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 12-02-2024
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
        private readonly IAttendeeMusicBandRepository attendeeMusicBandRepo;
        private readonly IAttendeeInnovationOrganizationRepository attendeeInnovationOrganizationRepo;
        private readonly IProjectRepository audiovisualProjectRepo;
        private readonly IAttendeeCartoonProjectRepository attendeeCartoonProjectRepo;
        private readonly IAttendeeCreatorProjectRepository attendeeCreatorProjectRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEditionDatesInformationCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="attendeeMusicBandRepository">The attendee music band repository.</param>
        /// <param name="attendeeInnovationOrganizationRepository">The attendee innovation organization repository.</param>
        /// <param name="audiovisualProjectRepository">The audiovisual project repository.</param>
        /// <param name="attendeeCartoonProjectRepository">The attendee cartoon project repository.</param>
        /// <param name="attendeeCreatorProjectRepository">The creator project repository.</param>
        public UpdateEditionDatesInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IEditionRepository editionRepository,
            IAttendeeMusicBandRepository attendeeMusicBandRepository,
            IAttendeeInnovationOrganizationRepository attendeeInnovationOrganizationRepository,
            IProjectRepository audiovisualProjectRepository,
            IAttendeeCartoonProjectRepository attendeeCartoonProjectRepository,
            IAttendeeCreatorProjectRepository attendeeCreatorProjectRepository
            )
            : base(eventBus, uow, editionRepository)
        {
            this.attendeeMusicBandRepo = attendeeMusicBandRepository;
            this.attendeeInnovationOrganizationRepo = attendeeInnovationOrganizationRepository;
            this.audiovisualProjectRepo = audiovisualProjectRepository;
            this.attendeeCartoonProjectRepo = attendeeCartoonProjectRepository;
            this.attendeeCreatorProjectRepo = attendeeCreatorProjectRepository;
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

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            bool changedMusicCommissionMinimumEvaluationsCount = edition.MusicCommissionMinimumEvaluationsCount != cmd.MusicCommissionMinimumEvaluationsCount;
            bool changedInnovationCommissionMinimumEvaluationsCount = edition.InnovationCommissionMinimumEvaluationsCount != cmd.InnovationCommissionMinimumEvaluationsCount;
            bool changedAudiovisualCommissionMinimumEvaluationsCount = edition.AudiovisualCommissionMinimumEvaluationsCount != cmd.AudiovisualCommissionMinimumEvaluationsCount;
            bool changedCartoonCommissionMinimumEvaluationsCount = edition.CartoonCommissionMinimumEvaluationsCount != cmd.CartoonCommissionMinimumEvaluationsCount;
            bool changedCreatorCommissionMinimumEvaluationsCount = edition.CreatorCommissionMinimumEvaluationsCount != cmd.CreatorCommissionMinimumEvaluationsCount;

            edition.UpdateDatesInformation(
                cmd.ProjectSubmitStartDate.Value,
                cmd.ProjectSubmitEndDate.Value,
                cmd.ProjectEvaluationStartDate.Value,
                cmd.ProjectEvaluationEndDate.Value,
                cmd.NegotiationStartDate.Value,
                cmd.NegotiationEndDate.Value,
                cmd.AttendeeOrganizationMaxSellProjectsCount.Value,
                cmd.ProjectMaxBuyerEvaluationsCount.Value,
                cmd.AudiovisualNegotiationsVirtualMeetingsJoinMinutes.Value,

                cmd.MusicBusinessRoundSubmitStartDate.Value,
                cmd.MusicBusinessRoundSubmitEndDate.Value,
                cmd.MusicBusinessRoundEvaluationStartDate.Value,
                cmd.MusicBusinessRoundEvaluationEndDate.Value,
                cmd.MusicBusinessRoundNegotiationStartDate.Value,
                cmd.MusicBusinessRoundNegotiationEndDate.Value,
                cmd.MusicBusinessRoundsMaximumProjectSubmissionsByCompany.Value,
                cmd.MusicBusinessRoundMaximumEvaluatorsByProject.Value,

                cmd.MusicPitchingSubmitStartDate.Value,
                cmd.MusicPitchingSubmitEndDate.Value,
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

                cmd.AudiovisualCommissionEvaluationStartDate.Value,
                cmd.AudiovisualCommissionEvaluationEndDate.Value,
                cmd.AudiovisualCommissionMinimumEvaluationsCount.Value,
                cmd.AudiovisualCommissionMaximumApprovedProjectsCount.Value,

                cmd.CartoonProjectSubmitStartDate,
                cmd.CartoonProjectSubmitEndDate,
                cmd.CartoonCommissionEvaluationStartDate,
                cmd.CartoonCommissionEvaluationEndDate,
                cmd.CartoonCommissionMinimumEvaluationsCount,
                cmd.CartoonCommissionMaximumApprovedProjectsCount,

                cmd.CreatorProjectSubmitStartDate.Value,
                cmd.CreatorProjectSubmitEndDate.Value,
                cmd.CreatorCommissionEvaluationStartDate.Value,
                cmd.CreatorCommissionEvaluationEndDate.Value,
                cmd.CreatorCommissionMinimumEvaluationsCount.Value,
                cmd.CreatorCommissionMaximumApprovedProjectsCount.Value,

                cmd.MusicPitchingMaximumProjectSubmissionsByEdition.Value,
                cmd.MusicPitchingMaximumProjectSubmissionsByParticipant.Value,
                cmd.MusicPitchingMaximumProjectSubmissionsByCompany.Value,
                cmd.MusicPitchingMaximumApprovedProjectsByCommissionMember.Value,
                cmd.MusicPitchingCuratorEvaluationStartDate,
                cmd.MusicPitchingCuratorEvaluationEndDate,
                cmd.MusicPitchingMaximumApprovedProjectsByCurator.Value,
                cmd.MusicPitchingPopularEvaluationStartDate,
                cmd.MusicPitchingPopularEvaluationEndDate,
                cmd.MusicPitchingMaximumApprovedProjectsByPopularVote.Value,
                cmd.MusicPitchingRepechageEvaluationStartDate,
                cmd.MusicPitchingRepechageEvaluationEndDate,
                cmd.MusicPitchingMaximumApprovedProjectsByRepechage.Value,

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
                attendeeMusicBands.ForEach(amb => amb.RecalculateGrade());
                this.attendeeMusicBandRepo.UpdateAll(attendeeMusicBands);
            }

            // Innovation Projects Grades must be recalculated when changed "InnovationCommissionMinimumEvaluationsCount"
            if (changedInnovationCommissionMinimumEvaluationsCount)
            {
                var attendeeInnovationOrganizations = await this.attendeeInnovationOrganizationRepo.FindAllByEditionIdAsync(edition.Id);
                attendeeInnovationOrganizations.ForEach(aio => aio.RecalculateGrade());
                this.attendeeInnovationOrganizationRepo.UpdateAll(attendeeInnovationOrganizations);
            }

            // Audiovisual Projects Grades must be recalculated when changed "AudiovisualCommissionMinimumEvaluationsCount"
            if (changedAudiovisualCommissionMinimumEvaluationsCount)
            {
                var projects = await this.audiovisualProjectRepo.FindAllByEditionIdAsync(edition.Id);
                projects.ForEach(p => p.RecalculateGrade(edition));
                this.audiovisualProjectRepo.UpdateAll(projects);
            }

            // Cartoon Projects Grades must be recalculated when changed "CartoonCommissionMinimumEvaluationsCount"
            if (changedCartoonCommissionMinimumEvaluationsCount)
            {
                var projects = await this.attendeeCartoonProjectRepo.FindAllByEditionIdAsync(edition.Id);
                projects.ForEach(acp => acp.RecalculateGrade());
                this.attendeeCartoonProjectRepo.UpdateAll(projects);
            }

            // Creator Projects Grades must be recalculated when changed "CreatorCommissionMinimumEvaluationsCount"
            if (changedCreatorCommissionMinimumEvaluationsCount)
            {
                var projects = await this.attendeeCreatorProjectRepo.FindAllByEditionIdAsync(edition.Id);
                projects.ForEach(acp => acp.RecalculateGrade());
                this.attendeeCreatorProjectRepo.UpdateAll(projects);
            }

            #endregion

            this.EditionRepo.Update(edition);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}