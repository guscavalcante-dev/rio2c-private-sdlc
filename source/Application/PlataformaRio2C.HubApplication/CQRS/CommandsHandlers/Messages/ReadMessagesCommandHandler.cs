// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-03-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-03-2019
// ***********************************************************************
// <copyright file="ReadMessagesCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.HubApplication.CQRS.Commands;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.HubApplication.CQRS.CommandsHandlers
{
    /// <summary>ReadMessagesCommandHandler</summary>
    public class ReadMessagesCommandHandler : BaseMessageCommandHandler, IRequestHandler<ReadMessages, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="ReadMessagesCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="messageRepository">The message repository.</param>
        public ReadMessagesCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IMessageRepository messageRepository)
            : base(eventBus, uow, messageRepository)
        {
        }

        /// <summary>Handles the specified read messages.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(ReadMessages cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var messages = await this.MessageRepo.FindAllNotReadBySenderIdAndByRecipientId(cmd.OtherUserId, cmd.UserId);
            if (messages == null)
            {
                return this.AppValidationResult;
            }

            foreach (var message in messages)
            {
                message.Read();
                if (!message.IsValid())
                {
                    this.AppValidationResult.Add(message.ValidationResult);
                }
            }

            if (!this.AppValidationResult.IsValid)
            {
                return this.AppValidationResult;
            }

            this.MessageRepo.UpdateAll(messages);
            this.Uow.SaveChanges();

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}