// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-24-2021
// ***********************************************************************
// <copyright file="CreateReleasedProjectCommandHandler.cs" company="Softo">
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
    /// <summary>CreateReleasedProjectCommandHandler</summary>
    public class CreateReleasedProjectCommandHandler : ReleasedMusicProjectBaseCommandHandler, IRequestHandler<CreateReleasedMusicProject, AppValidationResult>
    {
        private readonly IMusicBandRepository musicBandRepo;
        private readonly IReleasedMusicProjectRepository releasedMusicProjectRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReleasedProjectCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="musicBandRepo">The music band repo.</param>
        /// <param name="releasedMusicProjectRepo">The released music project repo.</param>
        public CreateReleasedProjectCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IMusicBandRepository musicBandRepo,
            IReleasedMusicProjectRepository releasedMusicProjectRepo)
            : base(eventBus, uow, releasedMusicProjectRepo)
        {
            this.musicBandRepo = musicBandRepo;
            this.releasedMusicProjectRepo = releasedMusicProjectRepo;
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateReleasedMusicProject cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var releasedMusicProject = new ReleasedMusicProject(
                await musicBandRepo.FindByIdAsync(cmd.MusicBandId),
                cmd.Name,
                cmd.Year,
                cmd.UserId);

            if (!releasedMusicProject.IsValid())
            {
                this.AppValidationResult.Add(releasedMusicProject.ValidationResult);
                return this.AppValidationResult;
            }

            this.ReleasedMusicProjectRepo.Create(releasedMusicProject);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = releasedMusicProject;

            return this.AppValidationResult;
        }
    }
}