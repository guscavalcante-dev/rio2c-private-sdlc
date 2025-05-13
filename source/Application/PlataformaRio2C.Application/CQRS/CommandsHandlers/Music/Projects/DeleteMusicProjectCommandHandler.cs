// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-01-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-01-2020
// ***********************************************************************
// <copyright file="DeleteMusicProjectCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>DeleteMusicProjectCommandHandler</summary>
    public class DeleteMusicProjectCommandHandler : MusicProjectBaseCommandHandler, IRequestHandler<DeleteMusicProject, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="DeleteMusicProjectCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="musicProjectRepository">The music project repository.</param>
        public DeleteMusicProjectCommandHandler(
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
        public async Task<AppValidationResult> Handle(DeleteMusicProject cmd, CancellationToken cancellationToken)
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

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}