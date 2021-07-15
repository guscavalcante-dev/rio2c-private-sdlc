// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="CreateInnovationCollaboratorCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
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
    /// <summary>CreateInnovationCollaboratorCommandHandler</summary>
    public class CreateInnovationCollaboratorCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<CreateInnovationCollaborator, AppValidationResult>
    {
        private readonly IUserRepository userRepo;
        private readonly IEditionRepository editionRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateInnovationCollaboratorCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="collaboratorTypeRepository">The collaborator type repository.</param>
        public CreateInnovationCollaboratorCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IUserRepository userRepository,
            IEditionRepository editionRepository,
            ICollaboratorTypeRepository collaboratorTypeRepository)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.userRepo = userRepository;
            this.editionRepo = editionRepository;
            this.collaboratorTypeRepo = collaboratorTypeRepository;
        }

        /// <summary>Handles the specified create tiny collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateInnovationCollaborator cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial validations

            var user = await this.userRepo.GetAsync(u => u.Email == cmd.Email.Trim() && !u.IsDeleted);

            // Return error only if the user is not deleted
            if (user != null)
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
                //TODO: Ainda não temos uma tabela para salvar esses InnovationOptions selecionados.
                //var innovationOptions = await innovationOptionRepo.FindAllByUidsAsync(cmd.InnovationOptionsUids);

                var collaborator = new Collaborator(
                    await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty),
                    await this.collaboratorTypeRepo.FindByNameAsync(cmd.CollaboratorTypeName),
                    cmd.FirstName,
                    cmd.LastNames,
                    cmd.Email,
                    cmd.PhoneNumber,
                    cmd.CellPhone,
                    cmd.Document,
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
                var updateCmd = new UpdateInnovationCollaborator
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

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}