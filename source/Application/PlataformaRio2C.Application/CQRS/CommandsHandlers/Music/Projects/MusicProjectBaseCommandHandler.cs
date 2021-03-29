// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="MusicProjectBaseCommandHandler.cs" company="Softo">
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
    /// <summary>MusicProjectBaseCommandHandler</summary>
    public class MusicProjectBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IMusicProjectRepository MusicProjectRepo;

        /// <summary>Initializes a new instance of the <see cref="MusicProjectBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="musicProjectRepository">The music project repository.</param>
        public MusicProjectBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, IMusicProjectRepository musicProjectRepository)
            : base(eventBus, uow)
        {
            this.MusicProjectRepo = musicProjectRepository;
        }

        /// <summary>Gets the music project by uid.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<MusicProject> GetMusicProjectByUid(Guid projectUid)
        {
            var musicProject = await this.MusicProjectRepo.GetAsync(projectUid);
            if (musicProject == null || musicProject.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM), new string[] { "ToastrError" }));
                return null;
            }

            return musicProject;
        }
    }
}