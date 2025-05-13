// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Almado
// Created          : 10-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="UpdateUserInterfaceLanguageCommandHandler.cs" company="Softo">
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
    public class UpdateUserInterfaceLanguageCommandHandler : BaseUserCommandHandler, IRequestHandler<UpdateUserInterfaceLanguage, AppValidationResult>
    {
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateUserInterfaceLanguageCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public UpdateUserInterfaceLanguageCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IUserRepository userRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, userRepository)
        {
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified update user interface language.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateUserInterfaceLanguage cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var user = await this.GetUserByUid(cmd.Useruid);
            user.UpdateInterfaceLanguage(
                await this.languageRepo.GetAsync(l => l.Code == cmd.LanguageCode));

            if (!user.IsValid())
            {
                this.AppValidationResult.Add(user.ValidationResult);
                return this.AppValidationResult;
            }

            this.UserRepo.Update(user);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = user;

            return this.AppValidationResult;
        }
    }
}
