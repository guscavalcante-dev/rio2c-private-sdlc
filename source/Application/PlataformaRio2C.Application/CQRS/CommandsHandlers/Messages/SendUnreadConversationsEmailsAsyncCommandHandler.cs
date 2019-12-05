// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-03-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
// ***********************************************************************
// <copyright file="SendUnreadConversationsEmailsAsyncCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>SendUnreadConversationsEmailsAsyncCommandHandler</summary>
    public class SendUnreadConversationsEmailsAsyncCommandHandler : BaseCommandHandler, IRequestHandler<SendUnreadConversationsEmailsAsync, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IMessageRepository messageRepo;

        /// <summary>Initializes a new instance of the <see cref="SendUnreadConversationsEmailsAsyncCommandHandler"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="messageRepository">The message repository.</param>
        public SendUnreadConversationsEmailsAsyncCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            IEditionRepository editionRepository,
            IMessageRepository messageRepository)
            : base(commandBus, uow)
        {
            this.editionRepo = editionRepository;
            this.messageRepo = messageRepository;
        }

        /// <summary>Handles the specified send unread messages emails asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(SendUnreadConversationsEmailsAsync cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var currentEdition = await this.editionRepo.FindByIsCurrentAsync();
            if (currentEdition == null)
            {
                this.ValidationResult.Add(new ValidationError("Send unread conversations will not be executed becaused there is no edition configured as current."));
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            var unreadConversations = await this.messageRepo.FindAllNotificationEmailConversationsDtosByEditionId(currentEdition.Id);
            if (unreadConversations?.Any() != true)
            {
                return this.AppValidationResult;
            }

            var currentValidationResult = new ValidationResult();

            foreach (var unreadConversation in unreadConversations)
            {
                try
                {
                    foreach (var message in unreadConversation.Messages)
                    {
                        message.SendNotificationEmail();
                    }

                    var response = await this.CommandBus.Send(new SendUnreadConversationEmailAsync(
                            unreadConversation,
                            currentEdition.Id,
                            currentEdition.Name,
                            currentEdition.UrlCode),
                        cancellationToken);
                    foreach (var error in response?.Errors)
                    {
                        currentValidationResult.Add(new ValidationError(error.Message));
                        continue;
                    }

                    this.messageRepo.UpdateAll(unreadConversation.Messages);
                    this.Uow.SaveChanges();
                }
                catch
                {
                    currentValidationResult.Add(new ValidationError($"Error sending the email for user {unreadConversation.RecipientUser.Name} (email: {unreadConversation.RecipientUser.Email}; uid: {unreadConversation.RecipientUser.Uid})."));
                }
            }

            if (!currentValidationResult.IsValid)
            {
                this.AppValidationResult.Add(currentValidationResult);
            }

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}