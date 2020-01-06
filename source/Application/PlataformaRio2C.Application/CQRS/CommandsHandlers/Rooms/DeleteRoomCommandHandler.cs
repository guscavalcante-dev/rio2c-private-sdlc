// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="DeleteRoomCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>DeleteRoomCommandHandler</summary>
    public class DeleteRoomCommandHandler : RoomBaseCommandHandler, IRequestHandler<DeleteRoom, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="DeleteRoomCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="roomRepository">The room repository.</param>
        public DeleteRoomCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IRoomRepository roomRepository)
            : base(eventBus, uow, roomRepository)
        {
        }

        /// <summary>Handles the specified delete room.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteRoom cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var room = await this.GetRoomByUid(cmd.RoomUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            room.Delete(cmd.UserId);
            if (!room.IsValid())
            {
                this.AppValidationResult.Add(room.ValidationResult);
                return this.AppValidationResult;
            }

            this.RoomRepo.Update(room);
            this.Uow.SaveChanges();

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}