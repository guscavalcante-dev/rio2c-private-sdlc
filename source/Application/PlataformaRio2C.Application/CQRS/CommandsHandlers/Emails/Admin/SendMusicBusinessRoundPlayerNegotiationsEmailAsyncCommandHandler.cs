// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-26-2021
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 04-25-2025
// ***********************************************************************
// <copyright file="SendPlayerNegotiationsEmailAsyncCommandHandler.cs" company="Softo">
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
    /// SendPlayerNegotiationsEmailAsyncCommandHandler
    /// </summary>
    public class SendMusicBusinessRoundPlayerNegotiationsEmailAsyncCommandHandler : MailerBaseCommandHandler, IRequestHandler<SendMusicBusinessRoundPlayerNegotiationsEmailAsync, AppValidationResult>
    {
        private readonly ICollaboratorRepository collaboratorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendPlayerNegotiationsEmailAsyncCommandHandler"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="mailerService">The mailer service.</param>
        /// <param name="sentEmailRepository">The sent email repository.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        public SendMusicBusinessRoundPlayerNegotiationsEmailAsyncCommandHandler(
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
        /// Handles the specified send player negotiations email asynchronous.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(SendMusicBusinessRoundPlayerNegotiationsEmailAsync cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            // Save sent email
            var sentEmailUid = Guid.NewGuid();
            var sentEmail = new SentEmail(sentEmailUid, cmd.RecipientUserId, cmd.Edition.Id, "PlayersNegotiations");
            if (!sentEmail.IsValid())
            {
                this.AppValidationResult.Add(sentEmail.ValidationResult);
                return this.AppValidationResult;
            }

            this.SentEmailRepo.Create(sentEmail);

            // Sends the email
            await this.MailerService.SendMusicBusinessRoundPlayersNegotiationEmail(cmd, sentEmail.Uid).SendAsync();

            this.Uow.SaveChanges();

            this.AppValidationResult.Data = sentEmail;
            return this.AppValidationResult;
        }
    }
}