﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-25-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-25-2021
// ***********************************************************************
// <copyright file="SendProducerNegotiationsEmailAsyncCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.Interfaces;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>
    /// SendProducerNegotiationsEmailAsyncCommandHandler
    /// </summary>
    public class SendProducerNegotiationsEmailAsyncCommandHandler : MailerBaseCommandHandler, IRequestHandler<SendProducerNegotiationsEmailAsync, AppValidationResult>
    {
        private readonly ICollaboratorRepository collaboratorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendProducerNegotiationsEmailAsyncCommandHandler"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="mailerService">The mailer service.</param>
        /// <param name="sentEmailRepository">The sent email repository.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        public SendProducerNegotiationsEmailAsyncCommandHandler(
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
        /// Handles the specified send producer negotiations email asynchronous.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(SendProducerNegotiationsEmailAsync cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            // Save sent email
            var sentEmailUid = Guid.NewGuid();
            var sentEmail = new SentEmail(sentEmailUid, cmd.RecipientUserId, cmd.Edition.Id, "ProducersNegotiations");
            if (!sentEmail.IsValid())
            {
                this.AppValidationResult.Add(sentEmail.ValidationResult);
                return this.AppValidationResult;
            }

            this.SentEmailRepo.Create(sentEmail);

            // Sends the email
            await this.MailerService.SendProducersNegotiationEmail(cmd, sentEmail.Uid).SendAsync();

            this.Uow.SaveChanges();

            this.AppValidationResult.Data = sentEmail;
            return this.AppValidationResult;
        }
    }
}