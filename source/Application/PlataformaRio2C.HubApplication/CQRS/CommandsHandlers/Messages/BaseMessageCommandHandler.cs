// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="BaseMessageCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.CommandsHandlers;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading.Tasks;

namespace PlataformaRio2C.HubApplication.CQRS.CommandsHandlers
{
    /// <summary>BaseMessageCommandHandler</summary>
    public class BaseMessageCommandHandler : BaseCommandHandler
    {
        protected readonly IMessageRepository MessageRepo;

        /// <summary>Initializes a new instance of the <see cref="BaseMessageCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="messageRepository">The message repository.</param>
        public BaseMessageCommandHandler(IMediator eventBus, IUnitOfWork uow, IMessageRepository messageRepository)
            : base(eventBus, uow)
        {
            this.MessageRepo = messageRepository;
        }

        /// <summary>Gets the message by uid.</summary>
        /// <param name="messageUid">The message uid.</param>
        /// <returns></returns>
        public async Task<Message> GetMessageByUid(Guid messageUid)
        {
            var message = await this.MessageRepo.GetAsync(messageUid);
            if (message == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Messages, Labels.FoundF), new string[] { "ToastrError" }));
                return null;
            }

            return message;
        }
    }
}