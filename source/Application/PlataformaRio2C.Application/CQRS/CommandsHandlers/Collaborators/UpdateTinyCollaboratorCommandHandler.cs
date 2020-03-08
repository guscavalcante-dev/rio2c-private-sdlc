// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="UpdateTinyCollaboratorCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateTinyCollaboratorCommandHandler</summary>
    public class UpdateTinyCollaboratorCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<UpdateTinyCollaborator, AppValidationResult>
    {
        private readonly IUserRepository userRepo;
        private readonly IEditionRepository editionRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateTinyCollaboratorCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="collaboratorTypeRepository">The collaborator type repository.</param>
        public UpdateTinyCollaboratorCommandHandler(
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

        /// <summary>Handles the specified update tiny collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateTinyCollaborator cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var collaborator = await this.GetCollaboratorByUid(cmd.CollaboratorUid);

            #region Initial validations

            // Check if exists an user with the same email
            var user = await this.userRepo.GetAsync(u => u.Email == cmd.Email.Trim());
            if (user != null && (collaborator?.User == null || user.Uid != collaborator?.User?.Uid))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.User.ToLowerInvariant(), $"{Labels.TheM.ToLowerInvariant()} {Labels.Email.ToLowerInvariant()}", cmd.Email), new string[] { "Email" }));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            collaborator.UpdateTiny(
                await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty),
                await this.collaboratorTypeRepo.FindByNameAsunc(cmd.CollaboratorTypeName),
                cmd.FirstName,
                cmd.LastNames,
                cmd.Email,
                true, // TODO: Get isAddingToCurrentEdition from command for UpdateCollaborator
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