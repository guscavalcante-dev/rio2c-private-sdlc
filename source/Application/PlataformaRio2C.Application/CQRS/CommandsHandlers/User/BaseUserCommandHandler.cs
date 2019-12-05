// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-05-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
// ***********************************************************************
// <copyright file="BaseUserCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>BaseUserCommandHandler</summary>
    public class BaseUserCommandHandler : BaseCommandHandler
    {
        protected readonly IUserRepository UserRepo;

        /// <summary>Initializes a new instance of the <see cref="BaseUserCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="userRepository">The user repository.</param>
        public BaseUserCommandHandler(IMediator eventBus, IUnitOfWork uow, IUserRepository userRepository)
            : base(eventBus, uow)
        {
            this.UserRepo = userRepository;
        }

        /// <summary>Gets the user by uid.</summary>
        /// <param name="userUid">The user uid.</param>
        /// <returns></returns>
        public async Task<Domain.Entities.User> GetUserByUid(Guid userUid)
        {
            var user = await this.UserRepo.GetAsync(userUid);
            if (user == null || user.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.User, Labels.FoundM)));
                return null;
            }

            return user;
        }

        /// <summary>Gets the user by identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<Domain.Entities.User> GetUserById(int userId)
        {
            var user = await this.UserRepo.GetAsync(userId);
            if (user == null || user.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.User, Labels.FoundM)));
                return null;
            }

            return user;
        }
    }
}