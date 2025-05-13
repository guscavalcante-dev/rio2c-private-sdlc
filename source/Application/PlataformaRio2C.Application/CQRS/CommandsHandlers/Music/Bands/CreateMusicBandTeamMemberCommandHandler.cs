// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 23-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 23-03-2021
// ***********************************************************************
// <copyright file="CreateMusicBandTeamMemberCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateMusicBandTeamMemberCommandHandler</summary>
    public class CreateMusicBandTeamMemberCommandHandler : MusicBandTeamMemberBaseCommandHandler, IRequestHandler<CreateMusicBandTeamMember, AppValidationResult>
    {
        private readonly IMusicBandRepository musicBandRepo;
        private readonly IMusicBandTeamMemberRepository musicBandTeamMemberRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicBandTeamMemberCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="musicBandRepo">The music band repo.</param>
        /// <param name="musicBandTeamMemberRepo">The music band team member repo.</param>
        public CreateMusicBandTeamMemberCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IMusicBandRepository musicBandRepo,
            IMusicBandTeamMemberRepository musicBandTeamMemberRepo)
            : base(eventBus, uow, musicBandTeamMemberRepo)
        {
            this.musicBandRepo = musicBandRepo;
            this.musicBandTeamMemberRepo = musicBandTeamMemberRepo;
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateMusicBandTeamMember cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var musicBand = new MusicBandTeamMember(
                await musicBandRepo.FindByIdAsync(cmd.MusicBandId),
                cmd.Name,
                cmd.Role,
                cmd.UserId);

            if (!musicBand.IsValid())
            {
                this.AppValidationResult.Add(musicBand.ValidationResult);
                return this.AppValidationResult;
            }

            this.MusicBandTeamMemberRepo.Create(musicBand);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = musicBand;

            return this.AppValidationResult;
        }
    }
}