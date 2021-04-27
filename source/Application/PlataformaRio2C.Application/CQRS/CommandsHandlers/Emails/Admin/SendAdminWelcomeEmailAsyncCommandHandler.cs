// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-27-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-27-2021
// ***********************************************************************
// <copyright file="SendAdminWelcomeEmailAsyncCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.Services;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>SendAdminWelcomeEmailAsyncCommandHandler</summary>
    public class SendAdminWelcomeEmailAsyncCommandHandler : MailerBaseCommandHandler, IRequestHandler<SendAdminWelcomeEmailAsync, AppValidationResult>
    {
        private readonly ICollaboratorRepository collaboratorRepo;

        /// <summary>Initializes a new instance of the <see cref="SendAdminWelcomeEmailAsyncCommandHandler"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="mailerService">The mailer service.</param>
        /// <param name="sentEmailRepository">The sent email repository.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        public SendAdminWelcomeEmailAsyncCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            IMailerService mailerService,
            ISentEmailRepository sentEmailRepository,
            ICollaboratorRepository collaboratorRepository)
            : base(commandBus, uow, mailerService, sentEmailRepository)
        {
            this.collaboratorRepo = collaboratorRepository;
        }

        /// <summary>Handles the specified send music commission welcome email asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(SendAdminWelcomeEmailAsync cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            // Save sent email
            var sentEmailUid = Guid.NewGuid();
            var sentEmail = new SentEmail(sentEmailUid, cmd.RecipientUserId, cmd.Edition.Id, "AdminWelcome");
            if (!sentEmail.IsValid())
            {
                this.AppValidationResult.Add(sentEmail.ValidationResult);
                return this.AppValidationResult;
            }

            this.SentEmailRepo.Create(sentEmail);

            Guid[] enabledCollaboratorTypesUids = new Guid[] { CollaboratorType.AdminAudiovisual.Uid, CollaboratorType.AdminLogistic.Uid };

            // Update collaborator welcome email
            var collaborator = await this.collaboratorRepo.GetAsync(cmd.Collaboratoruid);
            if (collaborator == null || collaborator.IsDeleted 
                || !collaborator.AttendeeCollaborators.Any(ac => !ac.IsDeleted
                                                                 && ac.EditionId == cmd.Edition.Id
                                                                 && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                            && !act.CollaboratorType.IsDeleted
                                                                                                            && enabledCollaboratorTypesUids.Contains(act.CollaboratorType.Uid))))
            {
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM))));
            }

            if (!this.AppValidationResult.IsValid)
            {
                return this.AppValidationResult;
            }

            collaborator?.SendWelcomeEmailSendDate(cmd.Edition.Id, cmd.UserId);

            // Sends the email
            await this.MailerService.SendAdminWelcomeEmail(cmd, sentEmail.Uid).SendAsync();

            this.Uow.SaveChanges();

            this.AppValidationResult.Data = sentEmail;
            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}