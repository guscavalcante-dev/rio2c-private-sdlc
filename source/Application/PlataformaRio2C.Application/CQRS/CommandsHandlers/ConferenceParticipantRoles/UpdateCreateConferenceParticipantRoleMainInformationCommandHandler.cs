// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="UpdateConferenceParticipantRoleMainInformationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
    /// <summary>UpdateConferenceParticipantRoleMainInformationCommandHandler</summary>
    public class UpdateConferenceParticipantRoleMainInformationCommandHandler : ConferenceParticipantRoleBaseCommandHandler, IRequestHandler<UpdateConferenceParticipantRoleMainInformation, AppValidationResult>
    {
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceParticipantRoleMainInformationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="conferenceParticipantRoleRepository">The conferenceParticipantRole repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public UpdateConferenceParticipantRoleMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IConferenceParticipantRoleRepository conferenceParticipantRoleRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, conferenceParticipantRoleRepository)
        {
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified update conference participant role main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateConferenceParticipantRoleMainInformation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var conferenceParticipantRole = await this.GetConferenceParticipantRoleByUid(cmd.ConferenceParticipantRoleUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            conferenceParticipantRole.UpdateMainInformation(
                cmd.Titles?.Select(d => new ConferenceParticipantRoleTitle(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.UserId);
            if (!conferenceParticipantRole.IsValid())
            {
                this.AppValidationResult.Add(conferenceParticipantRole.ValidationResult);
                return this.AppValidationResult;
            }

            this.ConferenceParticipantRoleRepo.Update(conferenceParticipantRole);
            this.Uow.SaveChanges();

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}