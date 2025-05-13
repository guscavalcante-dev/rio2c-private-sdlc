// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="ConferenceParticipantRoleBaseCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>ConferenceParticipantRoleBaseCommandHandler</summary>
    public class ConferenceParticipantRoleBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IConferenceParticipantRoleRepository ConferenceParticipantRoleRepo;

        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantRoleBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="conferenceParticipantRoleRepository">The conferenceParticipantRole repository.</param>
        public ConferenceParticipantRoleBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, IConferenceParticipantRoleRepository conferenceParticipantRoleRepository)
            : base(eventBus, uow)
        {
            this.ConferenceParticipantRoleRepo = conferenceParticipantRoleRepository;
        }

        /// <summary>Gets the conferenceParticipantRole by uid.</summary>
        /// <param name="conferenceParticipantRoleUid">The conferenceParticipantRole uid.</param>
        /// <returns></returns>
        public async Task<ConferenceParticipantRole> GetConferenceParticipantRoleByUid(Guid conferenceParticipantRoleUid)
        {
            var conferenceParticipantRole = await this.ConferenceParticipantRoleRepo.GetAsync(conferenceParticipantRoleUid);
            if (conferenceParticipantRole == null || conferenceParticipantRole.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.SpeakerRole, Labels.FoundF), new string[] { "ToastrError" }));
            }

            return conferenceParticipantRole;
        }
    }
}