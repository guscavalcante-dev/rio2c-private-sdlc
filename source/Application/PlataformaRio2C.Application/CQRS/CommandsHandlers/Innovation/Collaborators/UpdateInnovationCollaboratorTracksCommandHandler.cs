// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-19-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-06-2023
// ***********************************************************************
// <copyright file="UpdateInnovationCollaboratorTracksCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateInnovationCollaboratorTracksCommandHandler</summary>
    public class UpdateInnovationCollaboratorTracksCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<UpdateInnovationCollaboratorTracks, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInnovationCollaboratorTracksCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="innovationOrganizationTrackOptionRepository">The innovation organization track option repository.</param>
        public UpdateInnovationCollaboratorTracksCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IEditionRepository editionRepository,
            IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepository)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.editionRepo = editionRepository;
            this.innovationOrganizationTrackOptionRepo = innovationOrganizationTrackOptionRepository;
        }

        /// <summary>Handles the specified update collaborator social networks.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateInnovationCollaboratorTracks cmd, CancellationToken cancellationToken)
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

            var innovationOrganizationTrackOptions = await this.innovationOrganizationTrackOptionRepo.FindAllByGroupsUidsAsync(cmd.AttendeeInnovationOrganizationTrackGroups
                                                                                                                                    ?.Where(ioto => ioto.IsChecked)
                                                                                                                                    ?.Select(ioto => ioto.InnovationOrganizationTrackOptionGroupUid));

            collaborator.UpdateAttendeeCollaboratorInnovationOrganizationTracks(edition,
                                                                                innovationOrganizationTrackOptions.Select(ioto => new AttendeeInnovationOrganizationTrack(ioto, string.Empty, cmd.UserId)).ToList(),
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