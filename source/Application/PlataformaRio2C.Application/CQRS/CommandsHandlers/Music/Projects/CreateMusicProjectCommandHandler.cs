// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 23-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 23-03-2021
// ***********************************************************************
// <copyright file="CreateMusicProjectCommandHandler.cs" company="Softo">
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
    /// <summary>CreateMusicProjectCommandHandler</summary>
    public class CreateMusicProjectCommandHandler : MusicProjectBaseCommandHandler, IRequestHandler<CreateMusicProject, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="CreateMusicProjectCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="musicProjectRepository">The music project repository.</param>
        public CreateMusicProjectCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IMusicProjectRepository musicProjectRepository)
            : base(eventBus, uow, musicProjectRepository)
        {
        }

        /// <summary>Handles the specified delete music project.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateMusicProject cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var musicProject = await this.GetMusicProjectByUid(cmd.MusicProjectUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            musicProject.Delete(cmd.UserId);
            if (!musicProject.IsValid())
            {
                this.AppValidationResult.Add(musicProject.ValidationResult);
                return this.AppValidationResult;
            }

            this.MusicProjectRepo.Update(musicProject);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}