// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 23-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 23-03-2021
// ***********************************************************************
// <copyright file="CreateMusicBandMemberCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateMusicBandMemberCommandHandler</summary>
    public class CreateMusicBandMemberCommandHandler : MusicBandMemberBaseCommandHandler, IRequestHandler<CreateMusicBandMember, AppValidationResult>
    {
        private readonly IMusicBandRepository musicBandRepo;
        private readonly IMusicBandMemberRepository musicBandMemberRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicBandMemberCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="musicBandRepo">The music band repo.</param>
        /// <param name="musicBandMemberRepo">The music band member repository.</param>
        public CreateMusicBandMemberCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IMusicBandRepository musicBandRepo,
            IMusicBandMemberRepository musicBandMemberRepo)
            : base(eventBus, uow, musicBandMemberRepo)
        {
            this.musicBandRepo = musicBandRepo;
            this.musicBandMemberRepo = musicBandMemberRepo;
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateMusicBandMember cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var musicBand = new MusicBandMember(
                await musicBandRepo.FindByIdAsync(cmd.MusicBandId),
                cmd.Name,
                cmd.MusicInstrumentName,
                cmd.UserId);

            if (!musicBand.IsValid())
            {
                this.AppValidationResult.Add(musicBand.ValidationResult);
                return this.AppValidationResult;
            }

            this.MusicBandMemberRepo.Create(musicBand);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = musicBand;

            return this.AppValidationResult;
        }
    }
}