// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-05-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
// ***********************************************************************
// <copyright file="UpdateUserEmailSettingsCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateUserEmailSettingsCommandHandler</summary>
    public class UpdateUserEmailSettingsCommandHandler : BaseUserCommandHandler, IRequestHandler<UpdateUserEmailSettings, AppValidationResult>
    {
        private readonly ISubscribeListRepository subscribeListRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateUserEmailSettingsCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="subscribeListRepository">The subscribe list repository.</param>
        public UpdateUserEmailSettingsCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IUserRepository userRepository,
            ISubscribeListRepository subscribeListRepository)
            : base(eventBus, uow, userRepository)
        {
            this.subscribeListRepo = subscribeListRepository;
        }

        /// <summary>Handles the specified update user email settings.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateUserEmailSettings cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var user = await this.GetUserByUid(cmd.UserUid);
            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            // Before update values
            user.UpdateUserUnsubscribedLists(
                await this.subscribeListRepo.FindAllByNotUids(cmd.SelectedSubscribeListUids),
                cmd.UserId);
            if (!user.IsValid())
            {
                this.AppValidationResult.Add(user.ValidationResult);
                return this.AppValidationResult;
            }

            this.UserRepo.Update(user);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = user;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);
            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}