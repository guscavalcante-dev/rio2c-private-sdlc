// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="CreateRoomCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateRoomCommandHandler</summary>
    public class CreateRoomCommandHandler : RoomBaseCommandHandler, IRequestHandler<CreateRoom, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateRoomCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="roomRepository">The room repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public CreateRoomCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IRoomRepository roomRepository,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, roomRepository)
        {
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified create room.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateRoom cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var roomUid = Guid.NewGuid();

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            var room = new Room(
                await this.editionRepo.GetAsync(cmd.EditionId ?? 0),
                cmd.Names?.Select(d => new RoomName(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.IsVirtualMeeting,
                cmd.UserId);
            if (!room.IsValid())
            {
                this.AppValidationResult.Add(room.ValidationResult);
                return this.AppValidationResult;
            }

            this.RoomRepo.Create(room);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = room;

            return this.AppValidationResult;
        }
    }
}