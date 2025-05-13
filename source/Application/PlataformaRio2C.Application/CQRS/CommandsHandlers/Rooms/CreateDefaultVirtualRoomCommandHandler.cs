// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-22-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-22-2024
// ***********************************************************************
// <copyright file="CreateDefaultVirtualRoomCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>
    /// CreateDefaultVirtualRoomCommandHandler
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Application.CQRS.CommandsHandlers.RoomBaseCommandHandler" />
    /// <seealso cref="MediatR.IRequestHandler&lt;PlataformaRio2C.Application.CQRS.Commands.CreateDefaultVirtualRoom, PlataformaRio2C.Application.AppValidationResult&gt;" />
    public class CreateDefaultVirtualRoomCommandHandler : RoomBaseCommandHandler, IRequestHandler<CreateDefaultVirtualRoom, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly IRoomRepository roomRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDefaultVirtualRoomCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="roomRepository">The room repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="roomRepo">The room repo.</param>
        public CreateDefaultVirtualRoomCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IRoomRepository roomRepository,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository,
            IRoomRepository roomRepo)
            : base(eventBus, uow, roomRepository)
        {
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
            this.roomRepo = roomRepo;
        }

        /// <summary>Handles the specified create room.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateDefaultVirtualRoom cmd, CancellationToken cancellationToken)
        {
            int virtualMeetingRoomsCount = await roomRepo.CountAsync(r => r.EditionId == cmd.EditionId && r.IsVirtualMeeting);
            if (virtualMeetingRoomsCount == 0)
            {
                this.Uow.BeginTransaction();

                var languageDtos = await this.languageRepo.FindAllDtosAsync();

                var roomNames = new List<RoomName>()
                {
                    new RoomName(
                        "Rodadas de Negócios - Sala Virtual",
                        languageDtos?.FirstOrDefault(l => l.Code == Language.Portuguese.Code)?.Language,
                        cmd.UserId),
                    new RoomName(
                        "One-to-One Meetings - Virtual Room",
                        languageDtos?.FirstOrDefault(l => l.Code == Language.English.Code)?.Language,
                        cmd.UserId)
                };

                var room = new Room(
                    await this.editionRepo.GetAsync(cmd.EditionId ?? 0),
                    roomNames,
                    true,
                    cmd.UserId);

                if (!room.IsValid())
                {
                    this.AppValidationResult.Add(room.ValidationResult);
                    return this.AppValidationResult;
                }

                this.RoomRepo.Create(room);
                this.Uow.SaveChanges();
                this.AppValidationResult.Data = room;
            }

            return this.AppValidationResult;
        }
    }
}