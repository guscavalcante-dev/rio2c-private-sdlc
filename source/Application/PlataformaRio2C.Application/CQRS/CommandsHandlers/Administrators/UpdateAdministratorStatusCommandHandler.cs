// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-24-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-09-2021
// ***********************************************************************
// <copyright file="UpdateAdministratorStatusCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateAdministratorStatusCommandHandler</summary>
    public class UpdateAdministratorStatusCommandHandler : BaseAdministratorCommandHandler, IRequestHandler<UpdateAdministratorStatus, AppValidationResult>
    {
        private readonly IUserRepository userRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAdministratorStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public UpdateAdministratorStatusCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IUserRepository userRepository)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.userRepo = userRepository;
        }

        /// <summary>
        /// Handles the specified update administrator status.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateAdministratorStatus cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var user = await this.userRepo.FindByUidAsync(cmd.UserUid);

            #region Initial validations          

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            user.UpdateStatus(cmd.Active);

            if (!user.IsValid())
            {
                this.AppValidationResult.Add(user.ValidationResult);
                return this.AppValidationResult;
            }

            this.userRepo.Update(user);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = user;

            return this.AppValidationResult;
        }
    }
}