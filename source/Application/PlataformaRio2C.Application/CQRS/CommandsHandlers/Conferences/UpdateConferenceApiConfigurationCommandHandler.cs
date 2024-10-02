// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Gilson Oliveira
// Created          : 27-09-2024
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 30-09-2024
// ***********************************************************************
// <copyright file="UpdateConferenceApiConfigurationCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateConferenceApiConfigurationCommandHandler</summary>
    public class UpdateConferenceApiConfigurationCommandHandler : ConferenceBaseCommandHandler, IRequestHandler<UpdateConferenceApiConfiguration, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;
        private readonly ILanguageRepository languageRepo;

        public UpdateConferenceApiConfigurationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IEditionRepository editionRepository,
            ICollaboratorTypeRepository collaboratorTypeRepository,
            ILanguageRepository languageRepository,
            IConferenceRepository conferenceRepository
        ) : base(eventBus, uow, conferenceRepository)
        {
            this.editionRepo = editionRepository;
            this.collaboratorTypeRepo = collaboratorTypeRepository;
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified update collaborator API configuration.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateConferenceApiConfiguration cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var conference = await this.GetConferenceByUid(cmd.ConferenceUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            if (!conference.IsAbleToPublishToApi().IsValid)
            {
                this.ValidationResult.Add(
                    new ValidationError(
                        Messages.PendingFieldsToPublishConference,
                        new string[] { "ToastrError" }
                    )
                );
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            conference.UpdateApiConfiguration(
                cmd.IsApiDisplayEnabled,
                cmd.ApiHighlightPosition,
                cmd.UserId
            );
            if (!conference.IsValid())
            {
                this.AppValidationResult.Add(conference.ValidationResult);
                return this.AppValidationResult;
            }

            this.ConferenceRepo.Update(conference);

            #region Disable same highlight position of other collaborators

            if (cmd.IsApiDisplayEnabled && cmd.ApiHighlightPosition != null)
            {
                var sameHighlightPositionConferences = await this.ConferenceRepo.FindAllByHightlightPosition(
                    cmd.ApiHighlightPosition,
                    conference.EditionEventId
                );
                if (sameHighlightPositionConferences?.Any() == true)
                {
                    foreach (var sameHighlightPositionConference in sameHighlightPositionConferences)
                    {
                        sameHighlightPositionConference.DeleteApiHighlightPosition(cmd.UserId);
                        this.ConferenceRepo.Update(sameHighlightPositionConference);
                    }
                }
            }

            #endregion

            this.Uow.SaveChanges();
            this.AppValidationResult.Data = conference;

            return this.AppValidationResult;
        }

    }
}