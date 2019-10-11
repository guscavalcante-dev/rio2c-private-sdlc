// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Almado
// Created          : 10-09-2019
//
// Last Modified By : William Almado
// Last Modified On : 10-09-2019
// ***********************************************************************
// <copyright file="UpdateUserInterfaceLanguageCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using MediatR;
using PlataformaRio2C.Application.CQRS.Commands.User;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers.User
{
    public class UpdateUserInterfaceLanguageCommandHandler : BaseCommandHandler, IRequestHandler<UpdateUserInterfaceLanguage, AppValidationResult>
    {
        private readonly IUserRepository userRepo;
        private readonly IEditionRepository editionRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateCollaboratorCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="collaboratorTypeRepository">The collaborator type repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public UpdateUserInterfaceLanguageCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IUserRepository userRepository,
            IEditionRepository editionRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow)
        {
            this.userRepo = userRepository;
            this.editionRepo = editionRepository;
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
            this.languageRepo = languageRepository;
        }

        public async Task<AppValidationResult> Handle(UpdateUserInterfaceLanguage cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();
            
            var user = await this.userRepo.GetAsync(u => u.Uid == cmd.Userid);
            user.UpdateInterfaceLanguage(
                await this.languageRepo.GetAsync(l => l.Code == cmd.LanguageCode));

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
