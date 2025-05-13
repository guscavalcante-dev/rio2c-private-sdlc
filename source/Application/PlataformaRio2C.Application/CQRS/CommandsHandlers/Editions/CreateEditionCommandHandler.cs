// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 12-02-2024
// ***********************************************************************
// <copyright file="CreateEditionCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Events.Editions;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateEditionCommandHandler</summary>
    public class CreateEditionCommandHandler : EditionBaseCommandHandler, IRequestHandler<CreateEdition, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEditionCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="editionRepository">The edition event repository.</param>
        public CreateEditionCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IEditionRepository editionRepository)
            : base(eventBus, uow, editionRepository)
        {
            this.editionRepo = editionRepository;
        }

        /// <summary>
        /// Handles the specified create edition.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateEdition cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial Validations

            // Check if have existent editions with current UrlCode.
            // URLCode musb be unique.
            var existentUrlCodeEdition = await editionRepo.FindByUrlCodeAsync(cmd.EditionMainInformation.UrlCode.Value);
            if (existentUrlCodeEdition != null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.Edition.ToLowerInvariant(), $"{Labels.TheM.ToLowerInvariant()} {Labels.UrlCode.ToLowerInvariant()}", cmd.EditionMainInformation.UrlCode), new string[] { "ToastrError" }));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var editionUid = Guid.NewGuid();

            var edition = new Edition(
                editionUid,
                cmd.EditionMainInformation.Name,
                cmd.EditionMainInformation.UrlCode.Value,
                cmd.EditionMainInformation.IsCurrent,
                cmd.EditionMainInformation.IsActive,
                cmd.EditionMainInformation.StartDate.Value,
                cmd.EditionMainInformation.EndDate.Value,
                cmd.EditionMainInformation.SellStartDate.Value,
                cmd.EditionMainInformation.SellEndDate.Value,
                cmd.EditionMainInformation.OneToOneMeetingsScheduleDate.Value,

                cmd.EditionDate.ProjectSubmitStartDate.Value,
                cmd.EditionDate.ProjectSubmitEndDate.Value,
                cmd.EditionDate.ProjectEvaluationStartDate.Value,
                cmd.EditionDate.ProjectEvaluationEndDate.Value,
                cmd.EditionDate.NegotiationStartDate.Value,
                cmd.EditionDate.NegotiationEndDate.Value,
                cmd.EditionDate.AttendeeOrganizationMaxSellProjectsCount.Value,
                cmd.EditionDate.ProjectMaxBuyerEvaluationsCount.Value,
                cmd.EditionDate.AudiovisualNegotiationsVirtualMeetingsJoinMinutes.Value,

                cmd.EditionDate.MusicPitchingSubmitStartDate.Value,
                cmd.EditionDate.MusicPitchingSubmitEndDate.Value,
                cmd.EditionDate.MusicCommissionEvaluationStartDate.Value,
                cmd.EditionDate.MusicCommissionEvaluationEndDate.Value,
                cmd.EditionDate.MusicCommissionMinimumEvaluationsCount.Value,
                cmd.EditionDate.MusicCommissionMaximumApprovedBandsCount.Value,

                cmd.EditionDate.MusicBusinessRoundSubmitStartDate.Value,
                cmd.EditionDate.MusicBusinessRoundSubmitEndDate.Value,
                cmd.EditionDate.MusicBusinessRoundEvaluationStartDate.Value,
                cmd.EditionDate.MusicBusinessRoundEvaluationEndDate.Value,
                cmd.EditionDate.MusicBusinessRoundNegotiationStartDate.Value,
                cmd.EditionDate.MusicBusinessRoundNegotiationEndDate.Value,
                cmd.EditionDate.MusicBusinessRoundsMaximumProjectSubmissionsByCompany.Value,
                cmd.EditionDate.MusicBusinessRoundMaximumEvaluatorsByProject.Value,

                cmd.EditionDate.InnovationProjectSubmitStartDate.Value,
                cmd.EditionDate.InnovationProjectSubmitEndDate.Value,
                cmd.EditionDate.InnovationCommissionEvaluationStartDate.Value,
                cmd.EditionDate.InnovationCommissionEvaluationEndDate.Value,
                cmd.EditionDate.InnovationCommissionMinimumEvaluationsCount.Value,
                cmd.EditionDate.InnovationCommissionMaximumApprovedCompaniesCount.Value,

                cmd.EditionDate.AudiovisualPitchingSubmitStartDate.Value,
                cmd.EditionDate.AudiovisualPitchingSubmitEndDate.Value,
                cmd.EditionDate.AudiovisualCommissionEvaluationStartDate.Value,
                cmd.EditionDate.AudiovisualCommissionEvaluationEndDate.Value,
                cmd.EditionDate.AudiovisualCommissionMinimumEvaluationsCount.Value,
                cmd.EditionDate.AudiovisualCommissionMaximumApprovedProjectsCount.Value,

                cmd.EditionDate.CartoonProjectSubmitStartDate,
                cmd.EditionDate.CartoonProjectSubmitEndDate,
                cmd.EditionDate.CartoonCommissionEvaluationStartDate,
                cmd.EditionDate.CartoonCommissionEvaluationEndDate,
                cmd.EditionDate.CartoonCommissionMinimumEvaluationsCount,
                cmd.EditionDate.CartoonCommissionMaximumApprovedProjectsCount,

                cmd.EditionDate.CreatorProjectSubmitStartDate.Value,
                cmd.EditionDate.CreatorProjectSubmitEndDate.Value,
                cmd.EditionDate.CreatorCommissionEvaluationStartDate.Value,
                cmd.EditionDate.CreatorCommissionEvaluationEndDate.Value,
                cmd.EditionDate.CreatorCommissionMinimumEvaluationsCount.Value,
                cmd.EditionDate.CreatorCommissionMaximumApprovedProjectsCount.Value,

                cmd.EditionDate.MusicPitchingMaximumProjectSubmissionsByEdition.Value,
                cmd.EditionDate.MusicPitchingMaximumProjectSubmissionsByParticipant.Value,
                cmd.EditionDate.MusicPitchingMaximumProjectSubmissionsByCompany.Value,
                cmd.EditionDate.MusicPitchingMaximumApprovedProjectsByCommissionMember.Value,
                cmd.EditionDate.MusicPitchingCuratorEvaluationStartDate,
                cmd.EditionDate.MusicPitchingCuratorEvaluationEndDate,
                cmd.EditionDate.MusicPitchingMaximumApprovedProjectsByCurator.Value,
                cmd.EditionDate.MusicPitchingPopularEvaluationStartDate,
                cmd.EditionDate.MusicPitchingPopularEvaluationEndDate,
                cmd.EditionDate.MusicPitchingMaximumApprovedProjectsByPopularVote.Value,
                cmd.EditionDate.MusicPitchingRepechageEvaluationStartDate,
                cmd.EditionDate.MusicPitchingRepechageEvaluationEndDate,
                cmd.EditionDate.MusicPitchingMaximumApprovedProjectsByRepechage.Value,

                cmd.UserId);

            if (!edition.IsValid())
            {
                this.AppValidationResult.Add(edition.ValidationResult);
                return this.AppValidationResult;
            }

            // Disable all other current Editions. 
            // There can be only one active current Edition!
            var currentEditions = await this.editionRepo.FindAllByIsCurrentAsync();
            if (edition.IsCurrent && currentEditions?.Count > 0)
            {
                currentEditions.ForEach(e => e.DisableIsCurrent());
                this.EditionRepo.UpdateAll(currentEditions);
            }

            this.EditionRepo.Create(edition);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = edition;

            // Update with this Edition Id to send to event
            cmd.UpdatePreSendProperties(
                cmd.UserId,
                cmd.UserUid,
                edition.Id,
                edition.Uid,
                cmd.UserInterfaceLanguage,
                cmd.IsAdmin);

            await this.CommandBus.Publish(new EditionCreated(cmd), cancellationToken);

            return this.AppValidationResult;
        }
    }
}