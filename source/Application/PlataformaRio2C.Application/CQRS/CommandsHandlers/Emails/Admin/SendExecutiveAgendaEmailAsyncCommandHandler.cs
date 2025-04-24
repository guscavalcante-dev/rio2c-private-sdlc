// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 05-03-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-03-2024
// ***********************************************************************
// <copyright file="SendExecutiveAgendaEmailAsyncCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Application.Interfaces;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>SendExecutiveAgendaEmailAsyncCommandHandler</summary>
    public class SendExecutiveAgendaEmailAsyncCommandHandler : MailerBaseCommandHandler, IRequestHandler<SendExecutiveAgendaEmailAsync, AppValidationResult>
    {
        private readonly ICollaboratorRepository collaboratorRepo;

        /// <summary>Initializes a new instance of the <see cref="SendExecutiveAgendaEmailAsyncCommandHandler"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="mailerService">The mailer service.</param>
        /// <param name="sentEmailRepository">The sent email repository.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        public SendExecutiveAgendaEmailAsyncCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            IMailerService mailerService,
            ISentEmailRepository sentEmailRepository,
            ICollaboratorRepository collaboratorRepository)
            : base(commandBus, uow, mailerService, sentEmailRepository)
        {
            this.collaboratorRepo = collaboratorRepository;
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(SendExecutiveAgendaEmailAsync cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            // Save sent email
            var sentEmailUid = Guid.NewGuid();
            var sentEmail = new SentEmail(sentEmailUid, cmd.RecipientUserId, cmd.Edition.Id, "ExecutiveAgenda");
            if (!sentEmail.IsValid())
            {
                this.AppValidationResult.Add(sentEmail.ValidationResult);
                return this.AppValidationResult;
            }

            this.SentEmailRepo.Create(sentEmail);

            var collaborator = await this.collaboratorRepo.GetAsync(cmd.Collaboratoruid);
            if (collaborator == null || collaborator.IsDeleted)
            {
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Executive, Labels.FoundM))));
            }

            if (!this.AppValidationResult.IsValid)
            {
                return this.AppValidationResult;
            }

            collaborator?.UpdateAgendaEmailSendDate(cmd.Edition.Id, cmd.UserId);

            // Sends the email
            await this.MailerService.SendExecutiveAgendaEmail(cmd, sentEmail.Uid).SendAsync();

            this.Uow.SaveChanges();

            this.AppValidationResult.Data = sentEmail;
            return this.AppValidationResult;
        }
    }
}