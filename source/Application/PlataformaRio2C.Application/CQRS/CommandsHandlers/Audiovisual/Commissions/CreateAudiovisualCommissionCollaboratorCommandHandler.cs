// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-19-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-17-2024
// ***********************************************************************
// <copyright file="CreateAudiovisualCommissionCollaboratorCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateAudiovisualCollaboratorCommandHandler</summary>
    public class CreateAudiovisualCommissionCollaboratorCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<CreateAudiovisualCommissionCollaborator, AppValidationResult>
    {
        private readonly IUserRepository userRepo;
        private readonly IEditionRepository editionRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;
        private readonly IInterestRepository interestRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateAudiovisualCommissionCollaboratorCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="collaboratorTypeRepository">The collaborator type repository.</param>
        public CreateAudiovisualCommissionCollaboratorCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IUserRepository userRepository,
            IEditionRepository editionRepository,
            ICollaboratorTypeRepository collaboratorTypeRepository,
            IInterestRepository interestRepository)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.userRepo = userRepository;
            this.editionRepo = editionRepository;
            this.collaboratorTypeRepo = collaboratorTypeRepository;
            this.interestRepo = interestRepository;
        }

        /// <summary>Handles the specified create tiny collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateAudiovisualCommissionCollaborator cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial validations

            var user = await this.userRepo.GetAsync(u => u.Email == cmd.Email.Trim());

            // Return error only if the user is not deleted
            if (user != null && !user.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.User.ToLowerInvariant(), $"{Labels.TheM.ToLowerInvariant()} {Labels.Email.ToLowerInvariant()}", cmd.Email), new string[] { "Email" }));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            // Create if the user was not found in database
            if (user == null)
            {
                var interestsDtos = await this.interestRepo.FindAllDtosByInterestGroupUidAsync(InterestGroup.Genre.Uid);

                // Interests
                var attendeeCollaboratorInterests = new List<AttendeeCollaboratorInterest>();
                if (cmd.Interests?.Any() == true)
                {
                    foreach (var interestBaseCommands in cmd.Interests)
                    {
                        foreach (var interestBaseCommand in interestBaseCommands?.Where(ibc => ibc.IsChecked)?.ToList())
                        {
                            attendeeCollaboratorInterests.Add(new AttendeeCollaboratorInterest(interestsDtos?.FirstOrDefault(id => id.Interest.Uid == interestBaseCommand.InterestUid)?.Interest, interestBaseCommand.AdditionalInfo, cmd.UserId));
                        }
                    }
                }

                var collaborator = Collaborator.CreateAudiovisualCommissionCollaborator(
                    await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty),
                    await this.collaboratorTypeRepo.FindByNameAsync(cmd.CollaboratorTypeName),
                    ProjectType.Audiovisual,
                    cmd.FirstName,
                    cmd.LastNames,
                    cmd.Email,
                    cmd.PhoneNumber,
                    cmd.CellPhone,
                    cmd.Document,
                    attendeeCollaboratorInterests,
                    cmd.UserId);

                if (!collaborator.IsValid())
                {
                    this.AppValidationResult.Add(collaborator.ValidationResult);
                    return this.AppValidationResult;
                }

                this.CollaboratorRepo.Create(collaborator);
                this.Uow.SaveChanges();
                this.AppValidationResult.Data = collaborator;
            }
            else
            {
                //TODO: This is not the correct way to instantiate a command. Create a new constructor accepting this parameters and use it.
                //TODO: Use "UpdateAudiovisualCommissionCollaborator" instead of "UpdateAudiovisualPlayerExecutiveCollaborator" and test.
                var updateCmd = new UpdateAudiovisualPlayerExecutiveCollaborator()
                {
                    CollaboratorUid = user.Collaborator.Uid,
                    IsAddingToCurrentEdition = true,
                    FirstName = cmd.FirstName,
                    LastNames = cmd.LastNames,
                    Email = cmd.Email,
                };
                updateCmd.UpdatePreSendProperties(cmd.CollaboratorTypeName, cmd.UserId, cmd.UserUid, cmd.EditionId, cmd.EditionUid, cmd.UserInterfaceLanguage);

                this.AppValidationResult = await this.CommandBus.Send(updateCmd, cancellationToken);
            }

            return this.AppValidationResult;
        }
    }
}