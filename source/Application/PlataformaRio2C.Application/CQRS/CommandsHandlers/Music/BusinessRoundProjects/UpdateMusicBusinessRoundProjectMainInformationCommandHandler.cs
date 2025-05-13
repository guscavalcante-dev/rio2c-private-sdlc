// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Gilson Oliveira
// Created          : 01-30-2025
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 01-30-2025
// ***********************************************************************
// <copyright file="UpdateMusicBusinessRoundProjectMainInformationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Interfaces.Repositories.Music.Projects;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateMusicBusinessRoundProjectMainInformationCommandHandler</summary>
    public class UpdateMusicBusinessRoundProjectMainInformationCommandHandler : BaseCommandHandler, IRequestHandler<UpdateMusicBusinessRoundProjectMainInformation, AppValidationResult>
    {
        private readonly ILanguageRepository languageRepo;
        private readonly IMusicBusinessRoundProjectRepository musicBusinessRoundProjectRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMusicBusinessRoundProjectMainInformationCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="musicBusinessRoundProjectRepo">The music business round project repo.</param>
        public UpdateMusicBusinessRoundProjectMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILanguageRepository languageRepository,
            IMusicBusinessRoundProjectRepository musicBusinessRoundProjectRepo)
            : base(eventBus, uow)
        {
            this.languageRepo = languageRepository;
            this.musicBusinessRoundProjectRepo = musicBusinessRoundProjectRepo;
        }

        /// <summary>Handles the specified update project main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateMusicBusinessRoundProjectMainInformation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var musicProject = await this.GetMusicProjectByUid(cmd.MusicProjectUid ?? Guid.Empty);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            musicProject.UpdateMainInformation(
                cmd.MusicBusinessRoundProjectExpectationsForMeetings?.Select(d => new MusicBusinessRoundProjectExpectationsForMeeting(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.AttachmentUrl,
                cmd.UserId);

            if (!musicProject.IsValid())
            {
                this.AppValidationResult.Add(musicProject.ValidationResult);
                return this.AppValidationResult;
            }

            this.musicBusinessRoundProjectRepo.Update(musicProject);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = musicProject;

            return this.AppValidationResult;
        }

        /// <summary>Gets the music project by uid.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicBusinessRoundProject> GetMusicProjectByUid(Guid musicProjectUid)
        {
            var musicProject = await this.musicBusinessRoundProjectRepo.GetAsync(musicProjectUid);
            if (musicProject == null || musicProject.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM), new string[] { "ToastrError" }));
                return null;
            }
            return musicProject;
        }
    }
}