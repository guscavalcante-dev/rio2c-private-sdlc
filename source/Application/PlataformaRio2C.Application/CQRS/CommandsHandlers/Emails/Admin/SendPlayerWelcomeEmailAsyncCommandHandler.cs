// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-11-2019
// ***********************************************************************
// <copyright file="SendPlayerWelcomeEmailAsyncCommandHandler.cs" company="Softo">
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
    /// <summary>SendPlayerWelcomeEmailAsyncCommandHandler</summary>
    public class SendPlayerWelcomeEmailAsyncCommandHandler : MailerBaseCommandHandler, IRequestHandler<SendPlayerWelcomeEmailAsync, AppValidationResult>
    {
        private readonly ICollaboratorRepository collaboratorRepo;

        /// <summary>Initializes a new instance of the <see cref="SendPlayerWelcomeEmailAsyncCommandHandler"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="mailerService">The mailer service.</param>
        /// <param name="sentEmailRepository">The sent email repository.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        public SendPlayerWelcomeEmailAsyncCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            IMailerService mailerService,
            ISentEmailRepository sentEmailRepository,
            ICollaboratorRepository collaboratorRepository)
            : base(commandBus, uow, mailerService, sentEmailRepository)
        {
            this.collaboratorRepo = collaboratorRepository;
        }

        /// <summary>Handles the specified send player welcome email asynchronous.</summary>
        /// <param name="cmd">The command</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(SendPlayerWelcomeEmailAsync cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            // Save sent email
            var sentEmailUid = Guid.NewGuid();
            var sentEmail = new SentEmail(sentEmailUid, cmd.RecipientUserId, cmd.Edition.Id, "PlayerWelcome");
            if (!sentEmail.IsValid())
            {
                this.AppValidationResult.Add(sentEmail.ValidationResult);
                return this.AppValidationResult;
            }

            this.SentEmailRepo.Create(sentEmail);

            // Update collaborator welcome email
            var collaborator = await this.collaboratorRepo.GetAsync(cmd.Collaboratoruid);
            if (collaborator == null || collaborator.IsDeleted 
                || !collaborator.AttendeeCollaborators.Any(ac => !ac.IsDeleted
                                                                 && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                            && !act.CollaboratorType.IsDeleted
                                                                                                            && act.CollaboratorType.Name == Domain.Constants.CollaboratorType.ExecutiveAudiovisual)))
            {
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Executive, Labels.FoundM))));
            }

            if (collaborator?.AttendeeCollaborators.Any(ac => !ac.IsDeleted 
                                                              && ac.EditionId == cmd.Edition.Id
                                                              && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                         && !act.CollaboratorType.IsDeleted
                                                                                                         && act.CollaboratorType.Name == Domain.Constants.CollaboratorType.ExecutiveAudiovisual)) != true)
            {
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Executive, Labels.Player))));
            }

            if (!this.AppValidationResult.IsValid)
            {
                return this.AppValidationResult;
            }

            collaborator.SendWelcomeEmailSendDate(cmd.Edition.Id, cmd.UserId);

            // Sends the email
            await this.MailerService.SendPlayerWelcomeEmail(cmd, sentEmail.Uid).SendAsync();

            this.Uow.SaveChanges();

            this.AppValidationResult.Data = sentEmail;
            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}