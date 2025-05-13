// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="UpdateRoomMainInformationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateRoomMainInformationCommandHandler</summary>
    public class UpdateRoomMainInformationCommandHandler : RoomBaseCommandHandler, IRequestHandler<UpdateRoomMainInformation, AppValidationResult>
    {
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateRoomMainInformationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="roomRepository">The room repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public UpdateRoomMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IRoomRepository roomRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, roomRepository)
        {
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified update room main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateRoomMainInformation cmd, CancellationToken cancellationToken)
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

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            room.UpdateMainInformation(
                cmd.Names?.Select(d => new RoomName(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.IsVirtualMeeting,
                cmd.UserId);
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