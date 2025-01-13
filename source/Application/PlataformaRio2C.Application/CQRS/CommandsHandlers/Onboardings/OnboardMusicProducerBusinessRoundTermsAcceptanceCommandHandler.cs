// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Daniel Giese Rodrigues
// Created          : 01-08-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-08-2025
// ***********************************************************************
// <copyright file="OnboardAudiovisualPlayerTermsAcceptanceCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>OnboardAudiovisualPlayerTermsAcceptanceCommandHandler</summary>
    public class OnboardMusicProducerBusinessRoundTermsAcceptanceCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<OnboardMusicProducerBusinessRoundTermsAcceptance, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;

        /// <summary>Initializes a new instance of the <see cref="OnboardAudiovisualPlayerTermsAcceptanceCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        public OnboardMusicProducerBusinessRoundTermsAcceptanceCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IEditionRepository editionRepository)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.editionRepo = editionRepository;
        }

        /// <summary>Handles the specified onboard player terms acceptance.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(OnboardMusicProducerBusinessRoundTermsAcceptance cmd, CancellationToken cancellationToken)
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

            collaborator.OnboardMusicProducerTermsAcceptanceDate(
                await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty),
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