// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-28-2019
// ***********************************************************************
// <copyright file="CreateMessageCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.HubApplication.CQRS.Commands;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.HubApplication.CQRS.CommandsHandlers
{
    /// <summary>CreateMessageCommandHandler</summary>
    public class CreateMessageCommandHandler : BaseMessageCommandHandler, IRequestHandler<CreateMessage, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IUserRepository userRepo;

        public CreateMessageCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IMessageRepository messageRepository,
            IEditionRepository editionRepository,
            IUserRepository userRepository)
            : base(eventBus, uow, messageRepository)
        {
            this.editionRepo = editionRepository;
            this.userRepo = userRepository;
        }

        /// <summary>Handles the specified create message.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateMessage cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var messageUid = Guid.NewGuid();

            var edition = await this.editionRepo.FindByUidAsync(cmd.EditionUid ?? Guid.Empty, false);
            var senderUser = await this.userRepo.FindByIdAsync(cmd.UserId);
            var recipientUser = await this.userRepo.FindByIdAsync(cmd.RecipientId);

            var message = new Message(
                messageUid,
                edition,
                senderUser,
                recipientUser,
                cmd.Text);
            if (!message.IsValid())
            {
                this.AppValidationResult.Add(message.ValidationResult);
                return this.AppValidationResult;
            }

            this.MessageRepo.Create(message);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = new MessageDto
            {
                SenderUser = senderUser,
                SenderCollaborator = senderUser.Collaborator,
                RecipientUser = recipientUser,
                RecipientCollaborator = recipientUser.Collaborator,
                Message = message
            };

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}