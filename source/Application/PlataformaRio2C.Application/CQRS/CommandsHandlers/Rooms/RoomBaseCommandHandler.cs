// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="RoomBaseCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>RoomBaseCommandHandler</summary>
    public class RoomBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IRoomRepository RoomRepo;

        /// <summary>Initializes a new instance of the <see cref="RoomBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="roomRepository">The room repository.</param>
        public RoomBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, IRoomRepository roomRepository)
            : base(eventBus, uow)
        {
            this.RoomRepo = roomRepository;
        }

        /// <summary>Gets the room by uid.</summary>
        /// <param name="roomUid">The room uid.</param>
        /// <returns></returns>
        public async Task<Room> GetRoomByUid(Guid roomUid)
        {
            var room = await this.RoomRepo.GetAsync(roomUid);
            if (room == null || room.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Room, Labels.FoundF), new string[] { "ToastrError" }));
            }

            return room;
        }
    }
}