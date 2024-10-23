// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-22-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-22-2024
// ***********************************************************************
// <copyright file="EditionEventHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Events.Editions;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.EventsHandlers.Editions
{
    public class EditionEventHandler : BaseEventHandler,
        INotificationHandler<EditionCreated>, 
        INotificationHandler<EditionUpdated>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditionEventHandler"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        public EditionEventHandler(IMediator commandBus) : base(commandBus)
        {
        }

        public async Task Handle(EditionCreated notification, CancellationToken cancellationToken)
        {
            var createRoomCmd = new CreateDefaultVirtualRoom();

            createRoomCmd.UpdatePreSendProperties(notification.CreateEdition);

            var result = await this.CommandBus.Send(createRoomCmd);
        }

        public async Task Handle(EditionUpdated notification, CancellationToken cancellationToken)
        {
            var createRoomCmd = new CreateDefaultVirtualRoom();

            createRoomCmd.UpdatePreSendProperties(notification.UpdateEditionMainInformation);

            var result = await this.CommandBus.Send(createRoomCmd);
        }
    }
}
