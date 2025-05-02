// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 15-04-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 15-04-2024
// ***********************************************************************
// <copyright file="UpdateAvailabilityCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateAvailabilityCommandHandler</summary>
    public class UpdateAvailabilityCommandHandler : BaseCommandHandler, IRequestHandler<UpdateAvailability, AppValidationResult>
    {
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IConferenceRepository conferenceRepo;

        public UpdateAvailabilityCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            IConferenceRepository conferenceRepository)
            : base(eventBus, uow)
        {
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.conferenceRepo = conferenceRepository;
        }

        public async Task<AppValidationResult> Handle(UpdateAvailability cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var attendeeCollaborator = await this.attendeeCollaboratorRepo.GetAsync(ac => ac.Uid == cmd.AttendeeCollaboratorUid && !ac.IsDeleted);

            // check for conflicts with attendeecollaborator scheduled ( Conferences )
            var conferencesDtoList = await this.conferenceRepo.FindConferencesDtoByParticipantAsync(attendeeCollaborator.Uid, cmd.EditionId.Value);
            var conferences = conferencesDtoList?
                .Select(dto => dto.Conference)
                .Where(c => c != null);

            if (conferences != null)
            {
                foreach (var conf in conferences)
                {
                    var confStart = conf?.StartDate;
                    var confEnd = conf?.EndDate;

                    if (confStart.HasValue && confEnd.HasValue &&
                        (confEnd.Value < cmd.AvailabilityBeginDate || confStart.Value > cmd.AvailabilityEndDate))
                    {
                        this.AppValidationResult.Add("AvailabilityBeginDate", "O palestrante está alocado em uma palestra fora da nova disponibilidade.");
                        return this.AppValidationResult;
                    }
                }
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            attendeeCollaborator.UpdateAvailability(cmd.AvailabilityBeginDate, cmd.AvailabilityEndDate, cmd.UserId);

            if (!attendeeCollaborator.IsValid())
            {
                this.AppValidationResult.Add(attendeeCollaborator.ValidationResult);
                return this.AppValidationResult;
            }

            this.attendeeCollaboratorRepo.Update(attendeeCollaborator);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = attendeeCollaborator;

            return this.AppValidationResult;
        }
    }

}