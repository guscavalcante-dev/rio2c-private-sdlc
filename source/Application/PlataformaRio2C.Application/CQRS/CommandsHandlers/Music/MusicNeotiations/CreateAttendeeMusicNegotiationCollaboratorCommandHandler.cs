// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Ribeiro 
// Created          : 05-03-2025
//
// Last Modified By : Rafael Ribeiro 
// Last Modified On : 05-03-2025
// ***********************************************************************
// <copyright file="CreateAttendeeNegotiationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Interfaces.Repositories;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateAttendeeNegotiationCommandHandler</summary>
    public class MusicBusinessRoundAttendeeNegotiationCollaboratorCommandHandler : BaseCommandHandler, IRequestHandler<CreateAttendeeMusicBusinessRoundNegotiationCollaborator, AppValidationResult>
    {
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IMusicBusinessRoundNegotiationRepository musicBusinessRoundNegotiationRepository;
        private readonly IAttendeeNegotiationCollaboratorRepository attendeeNegotiationCollaboratorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundAttendeeNegotiationCollaboratorCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="negotiationRepository">The negotiation repository.</param>
        /// <param name="attendeeNegotiationCollaboratorRepo">The attendee negotiation collaborator repo.</param>
        public MusicBusinessRoundAttendeeNegotiationCollaboratorCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            IMusicBusinessRoundNegotiationRepository musicbusinessroundnegotiationRepository,
            IAttendeeNegotiationCollaboratorRepository attendeeNegotiationCollaboratorRepo)
            : base(eventBus, uow)
        {
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.musicBusinessRoundNegotiationRepository = musicbusinessroundnegotiationRepository;
            this.attendeeNegotiationCollaboratorRepo = attendeeNegotiationCollaboratorRepo;
        }

        /// <summary>Handles the specified create negotiation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateAttendeeMusicBusinessRoundNegotiationCollaborator cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var negotiation = await this.musicBusinessRoundNegotiationRepository.FindByUidAsync(cmd.MusicBusinesRoundNegotiationUid);
            var attendeeCollaborator = await this.attendeeCollaboratorRepo.GetAsync(ac => !ac.IsDeleted && ac.CollaboratorId == cmd.UserId && ac.EditionId == cmd.EditionId);

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
                this.attendeeNegotiationCollaboratorRepo.Update(attendeeNegotiationCollaboratorDb);
            }
            else
            {
                if (negotiation == null || attendeeCollaborator == null)
                {
                    this.ValidationResult.Add(new ValidationError("Dados inválidos para criar negociação.", new string[] { "ToastrError" }));
                    this.AppValidationResult.Add(this.ValidationResult);
                    return this.AppValidationResult;
                }

                var attendeeNegotiationCollaborator = new AttendeeMusicBusinessRoundNegotiationCollaborator(
                    negotiation,
                    attendeeCollaborator,
                    cmd.UserId);

                if (!attendeeNegotiationCollaborator.IsValid())
                {
                    this.AppValidationResult.Add(this.ValidationResult);
                    return this.AppValidationResult;
                }

                this.attendeeNegotiationCollaboratorRepo.Create(attendeeNegotiationCollaboratorDb);
            }


            if (!attendeeNegotiationCollaboratorDb.IsValid())
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}