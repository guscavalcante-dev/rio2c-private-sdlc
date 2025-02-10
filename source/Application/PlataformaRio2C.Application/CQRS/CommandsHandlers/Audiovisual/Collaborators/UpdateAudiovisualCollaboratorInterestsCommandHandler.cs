// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-19-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-17-2024
// ***********************************************************************
// <copyright file="UpdateAudiovisualCollaboratorInterestsCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateAudiovisualCollaboratorInterestsCommandHandler</summary>
    public class UpdateAudiovisualCollaboratorInterestsCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<UpdateAudiovisualCollaboratorInterests, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IInterestRepository interestRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAudiovisualCollaboratorInterestsCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        public UpdateAudiovisualCollaboratorInterestsCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IEditionRepository editionRepository,
            IInterestRepository interestRepository)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.editionRepo = editionRepository;
            this.interestRepo = interestRepository;
        }

        /// <summary>Handles the specified update collaborator social networks.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateAudiovisualCollaboratorInterests cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var collaborator = await this.GetCollaboratorByUid(cmd.CollaboratorUid);
            var edition = await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var interestsDtos = await this.interestRepo.FindAllDtosByInterestGroupUidAsync(InterestGroup.AudiovisualPitchingSubGenre.Uid);

            // Interests
            var attendeeCollaboratorInterests = new List<AttendeeCollaboratorInterest>();
            if (cmd.Interests?.Any() == true)
            {
                foreach (var interestBaseCommands in cmd.Interests)
                {
                    foreach (var interestBaseCommand in interestBaseCommands?.Where(ibc => ibc.IsChecked)?.ToList())
                    {
                        attendeeCollaboratorInterests.Add(new AttendeeCollaboratorInterest(interestsDtos?.FirstOrDefault(id => id.Interest.Uid == interestBaseCommand.InterestUid)?.Interest, interestBaseCommand.AdditionalInfo, cmd.UserId));
                    }
                }
            }

            collaborator.UpdateCollaboratorInterests(
                edition,
                ProjectType.AudiovisualBusinessRound,
                attendeeCollaboratorInterests,
                cmd.UserId);
            if (!collaborator.IsValid())
            {
                this.AppValidationResult.Add(collaborator.ValidationResult);
                return this.AppValidationResult;
            }

            this.CollaboratorRepo.Update(collaborator);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = collaborator;

            return this.AppValidationResult;
        }
    }
}