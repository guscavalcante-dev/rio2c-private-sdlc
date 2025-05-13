// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-11-2019
// ***********************************************************************
// <copyright file="SendProjectBuyerEvaluationEmailAsyncCommandHandler.cs" company="Softo">
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
    /// <summary>SendProjectBuyerEvaluationEmailAsyncCommandHandler</summary>
    public class SendProjectBuyerEvaluationEmailAsyncCommandHandler : MailerBaseCommandHandler, IRequestHandler<SendProjectBuyerEvaluationEmailAsync, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="SendProjectBuyerEvaluationEmailAsyncCommandHandler"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="mailerService">The mailer service.</param>
        /// <param name="sentEmailRepository">The sent email repository.</param>
        public SendProjectBuyerEvaluationEmailAsyncCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            IMailerService mailerService,
            ISentEmailRepository sentEmailRepository)
            : base(commandBus, uow, mailerService, sentEmailRepository)
        {
        }

        /// <summary>Handles the specified send project buyer evaluation email asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(SendProjectBuyerEvaluationEmailAsync cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            // Save sent email
            var sentEmailUid = Guid.NewGuid();
            var sentEmail = new SentEmail(sentEmailUid, cmd.RecipientUserId, cmd.Edition.Id, "ProjectBuyerEvaluationEmail");
            if (!sentEmail.IsValid())
            {
                this.AppValidationResult.Add(sentEmail.ValidationResult);
                return this.AppValidationResult;
            }

            this.SentEmailRepo.Create(sentEmail);
            //this.Uow.SaveChanges();

            // Sends the email
            await this.MailerService.SendProjectBuyerEvaluationEmail(cmd, sentEmail.Uid).SendAsync();

            this.AppValidationResult.Data = sentEmail;
            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}