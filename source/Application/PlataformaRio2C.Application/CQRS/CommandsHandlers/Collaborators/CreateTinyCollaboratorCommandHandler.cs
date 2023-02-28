// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-12-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="CreateTinyCollaboratorCommandHandler.cs" company="Softo">
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
    /// <summary>CreateTinyCollaboratorCommandHandler</summary>
    public class CreateTinyCollaboratorCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<CreateTinyCollaborator, AppValidationResult>
    {
        private readonly IUserRepository userRepo;
        private readonly IEditionRepository editionRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;
        private readonly ICountryRepository countryRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTinyCollaboratorCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="collaboratorTypeRepository">The collaborator type repository.</param>
        /// <param name="countryRepository">The country repository.</param>
        public CreateTinyCollaboratorCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IUserRepository userRepository,
            IEditionRepository editionRepository,
            ICollaboratorTypeRepository collaboratorTypeRepository,
            ICountryRepository countryRepository)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.userRepo = userRepository;
            this.editionRepo = editionRepository;
            this.collaboratorTypeRepo = collaboratorTypeRepository;
            this.countryRepo = countryRepository;
        }

        /// <summary>Handles the specified create tiny collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateTinyCollaborator cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial validations

            var user = await this.userRepo.GetAsync(u => u.Email == cmd.Email.Trim() && !u.IsDeleted);
            if (user != null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.User.ToLowerInvariant(), $"{Labels.TheM.ToLowerInvariant()} {Labels.Email.ToLowerInvariant()}", cmd.Email), new string[] { "Email" }));
            }

            Country country = null;
            if (cmd.IsUpdatingAddress)
            {
                country = await this.countryRepo.FindByNameAsync(cmd.Country);
                if (country == null)
                {
                    this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Country, Labels.FoundM), new string[] { "Country" }));
                }
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
                #region Create

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

                if (cmd.IsUpdatingAddress)
                {
                    collaborator.UpdateAddress(
                        country,
                        null,
                        cmd.State,
                        null,
                        cmd.City,
                        cmd.Address,
                        cmd.ZipCode,
                        true,
                        cmd.UserId);
                }

                if (!collaborator.IsValid())
                {
                    this.AppValidationResult.Add(collaborator.ValidationResult);
                    return this.AppValidationResult;
                }

                this.CollaboratorRepo.Create(collaborator);
                this.Uow.SaveChanges();
                this.AppValidationResult.Data = collaborator;

                #endregion
            }
            else
            {
                #region Update

                var updateCmd = new UpdateTinyCollaborator(
                    user.Collaborator.Uid,
                    true,
                    cmd.FirstName,
                    cmd.LastNames,
                    cmd.Email,
                    cmd.PhoneNumber,
                    cmd.CellPhone,
                    cmd.Document,
                    cmd.Address,
                    cmd.Country,
                    cmd.State,
                    cmd.City,
                    cmd.ZipCode);

                updateCmd.UpdatePreSendProperties(
                    cmd.CollaboratorTypeName,
                    cmd.UserId,
                    cmd.UserUid,
                    cmd.EditionId,
                    cmd.EditionUid,
                    cmd.UserInterfaceLanguage);

                this.AppValidationResult = await this.CommandBus.Send(updateCmd, cancellationToken);

                #endregion
            }

            return this.AppValidationResult;
        }
    }
}