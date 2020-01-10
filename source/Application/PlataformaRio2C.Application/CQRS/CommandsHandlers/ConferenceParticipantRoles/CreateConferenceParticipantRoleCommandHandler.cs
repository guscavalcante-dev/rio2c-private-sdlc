// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="CreateConferenceParticipantRoleCommandHandler.cs" company="Softo">
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
    /// <summary>CreateConferenceParticipantRoleCommandHandler</summary>
    public class CreateConferenceParticipantRoleCommandHandler : ConferenceParticipantRoleBaseCommandHandler, IRequestHandler<CreateConferenceParticipantRole, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateConferenceParticipantRoleCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="conferenceParticipantRoleRepository">The conferenceParticipantRole repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public CreateConferenceParticipantRoleCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IConferenceParticipantRoleRepository conferenceParticipantRoleRepository,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, conferenceParticipantRoleRepository)
        {
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified create conference participant role.</summary>
        /// <param name="cmd">The comman.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateConferenceParticipantRole cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var conferenceParticipantRoleUid = Guid.NewGuid();

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            var conferenceParticipantRole = new ConferenceParticipantRole(
                conferenceParticipantRoleUid,
                await this.editionRepo.GetAsync(cmd.EditionId ?? 0),
                cmd.Titles?.Select(d => new ConferenceParticipantRoleTitle(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.UserId);
            if (!conferenceParticipantRole.IsValid())
            {
                this.AppValidationResult.Add(conferenceParticipantRole.ValidationResult);
                return this.AppValidationResult;
            }

            this.ConferenceParticipantRoleRepo.Create(conferenceParticipantRole);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = conferenceParticipantRole;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}