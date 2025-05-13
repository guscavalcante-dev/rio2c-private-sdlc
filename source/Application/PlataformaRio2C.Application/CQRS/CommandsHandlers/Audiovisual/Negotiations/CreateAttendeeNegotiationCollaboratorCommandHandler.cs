// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-26-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-26-2023
// ***********************************************************************
// <copyright file="CreateAttendeeNegotiationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateAttendeeNegotiationCommandHandler</summary>
    public class CreateAttendeeNegotiationCollaboratorCommandHandler : BaseCommandHandler, IRequestHandler<CreateAttendeeNegotiationCollaborator, AppValidationResult>
    {
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly INegotiationRepository negotiationRepo;
        private readonly IAttendeeNegotiationCollaboratorRepository attendeeNegotiationCollaboratorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAttendeeNegotiationCollaboratorCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="negotiationRepository">The negotiation repository.</param>
        /// <param name="attendeeNegotiationCollaboratorRepo">The attendee negotiation collaborator repo.</param>
        public CreateAttendeeNegotiationCollaboratorCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            INegotiationRepository negotiationRepository,
            IAttendeeNegotiationCollaboratorRepository attendeeNegotiationCollaboratorRepo)
            : base(eventBus, uow)
        {
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.negotiationRepo = negotiationRepository;
            this.attendeeNegotiationCollaboratorRepo = attendeeNegotiationCollaboratorRepo;
        }

        /// <summary>Handles the specified create negotiation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateAttendeeNegotiationCollaborator cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var negotiation = await this.negotiationRepo.FindByUidAsync(cmd.NegotiationUid);
            var attendeeCollaborator = await this.attendeeCollaboratorRepo.GetAsync(ac => !ac.IsDeleted && ac.Collaborator.Id == cmd.UserId && ac.EditionId == cmd.EditionId);

            if (negotiation == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Negotiation, Labels.FoundM), new string[] { "ToastrError" }));
            }

            if (attendeeCollaborator == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Participant, Labels.FoundM), new string[] { "ToastrError" }));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            var attendeeNegotiationCollaboratorDb = await this.attendeeNegotiationCollaboratorRepo.GetAsync(anc => !anc.IsDeleted
                                                                                                                    && anc.AttendeeCollaboratorId == attendeeCollaborator.Id
                                                                                                                    && anc.NegotiationId == negotiation.Id);

            if (attendeeNegotiationCollaboratorDb != null)
            {
                attendeeNegotiationCollaboratorDb.Update(cmd.UserId);

                this.attendeeNegotiationCollaboratorRepo.Update(attendeeNegotiationCollaboratorDb);
            }
            else
            {
                AttendeeNegotiationCollaborator attendeeNegotiationCollaborator = new AttendeeNegotiationCollaborator(
                    negotiation,
                    attendeeCollaborator,
                    cmd.UserId);

                this.attendeeNegotiationCollaboratorRepo.Create(attendeeNegotiationCollaborator);
            }

            if (!attendeeCollaborator.IsValid())
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}