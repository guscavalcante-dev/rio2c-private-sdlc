// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-05-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-05-2019
// ***********************************************************************
// <copyright file="OnboardCollaboratorAccessDataCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>OnboardCollaboratorAccessDataCommandHandler</summary>
    public class OnboardCollaboratorAccessDataCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<OnboardCollaboratorAccessData, AppValidationResult>
    {
        private readonly IUserRepository userRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly ICountryRepository countryRepo;

        public OnboardCollaboratorAccessDataCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IUserRepository userRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository,
            ICountryRepository countryRepository)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.userRepo = userRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
            this.countryRepo = countryRepository;
        }

        /// <summary>Handles the specified onboard collaborator access data.</summary>
        /// <param name="cmd">The comand.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(OnboardCollaboratorAccessData cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var collaborator = await this.GetCollaboratorByUid(cmd.CollaboratorUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            collaborator.OnboardAccessData(
                await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty),
                cmd.FirstName,
                cmd.LastNames,
                cmd.Badge,
                cmd.CellPhone,
                cmd.PhoneNumber,
                cmd.PasswordHash,
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

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}