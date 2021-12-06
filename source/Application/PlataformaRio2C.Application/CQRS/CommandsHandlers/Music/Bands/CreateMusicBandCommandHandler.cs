// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 23-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 23-03-2021
// ***********************************************************************
// <copyright file="CreateMusicBandCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository.Helpers;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateMusicBandCommandHandler</summary>
    public class CreateMusicBandCommandHandler : MusicBandBaseCommandHandler, IRequestHandler<CreateMusicBand, AppValidationResult>
    {
        private readonly IMusicBandTypeRepository musicBandTypeRepo;
        private readonly IEditionRepository editionRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IMusicGenreRepository musicGenreRepo;
        private readonly ICollaboratorRepository collaboratorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicBandCommandHandler" /> class.
        /// </summary>
        /// <param name="commandBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="musicBandRepo">The music band repo.</param>
        /// <param name="musicBandTypeRepo">The music band type repo.</param>
        /// <param name="editionRepo">The edition repo.</param>
        /// <param name="targetAudienceRepo">The target audience repo.</param>
        /// <param name="musicGenreRepo">The music genre repo.</param>
        /// <param name="collaboratorRepo">The collaborator repo.</param>
        public CreateMusicBandCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            IMusicBandRepository musicBandRepo,
            IMusicBandTypeRepository musicBandTypeRepo,
            IEditionRepository editionRepo,
            ITargetAudienceRepository targetAudienceRepo,
            IMusicGenreRepository musicGenreRepo,
            ICollaboratorRepository collaboratorRepo
            )
            : base(commandBus, uow, musicBandRepo)
        {
            this.musicBandTypeRepo = musicBandTypeRepo;
            this.editionRepo = editionRepo;
            this.targetAudienceRepo = targetAudienceRepo;
            this.musicGenreRepo = musicGenreRepo;
            this.collaboratorRepo = collaboratorRepo;
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateMusicBand cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial validations

            var editionDto = await editionRepo.FindDtoAsync(cmd.EditionId ?? 0);
            if (editionDto?.Edition == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Edition, Labels.FoundF), new string[] { "ToastrError" }));
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            if (editionDto.IsMusicProjectSubmitEnded())
            {
                this.ValidationResult.Add(new ValidationError(Messages.ProjectSubmitPeriodClosed, new string[] { "ToastrError" }));
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            cmd.TargetAudiencesApiDtos = cmd.TargetAudiencesApiDtos.Select(ta =>
                                                    new TargetAudienceApiDto()
                                                    {
                                                        Uid = ta.Uid,
                                                        TargetAudience = targetAudienceRepo.Get(ta.Uid)
                                                    }).ToList();

            cmd.MusicGenresApiDtos = cmd.MusicGenresApiDtos.Select(mg =>
                                                    new MusicGenreApiDto()
                                                    {
                                                        Uid = mg.Uid,
                                                        AdditionalInfo = mg.AdditionalInfo,
                                                        MusicGenre = musicGenreRepo.Get(mg.Uid)
                                                    }).ToList();

            var musicBandType = musicBandTypeRepo.Get(cmd.MusicBandTypeUid);

            CollaboratorDto collaboratorDto = null;

            if (cmd.MusicBandResponsibleApiDto != null)
            {
                collaboratorDto = await collaboratorRepo.FindByEmailAsync(cmd.MusicBandResponsibleApiDto.Email, editionDto.Id);

                if (collaboratorDto == null)
                {
                    #region Creates new Collaborator and User

                    var createCollaboratorCommand = new CreateTinyCollaborator();
                    
                    createCollaboratorCommand.UpdateBaseProperties(
                        cmd.MusicBandResponsibleApiDto.Name, 
                        null,
                        cmd.MusicBandResponsibleApiDto.Email,
                        cmd.MusicBandResponsibleApiDto.PhoneNumber,
                        cmd.MusicBandResponsibleApiDto.CellPhone,
                        cmd.MusicBandResponsibleApiDto.Document);
                    
                    createCollaboratorCommand.UpdatePreSendProperties(
                        CollaboratorType.Music.Name, //"Music" is fixed because in [dbo].[MigrateMusicProjects] procedure, its is fixed too!
                        cmd.UserId, 
                        cmd.UserUid, 
                        editionDto.Edition.Id,
                        editionDto.Edition.Uid, 
                        "");

                    var commandResult = await base.CommandBus.Send(createCollaboratorCommand);
                    if (!commandResult.IsValid)
                    {
                        var currentValidationResult = new ValidationResult();
                        foreach (var error in commandResult?.Errors)
                        {
                            currentValidationResult.Add(new ValidationError(error.Message));
                        }

                        if (!currentValidationResult.IsValid)
                        {
                            this.AppValidationResult.Add(currentValidationResult);
                            return this.AppValidationResult;
                        }
                    }

                    collaboratorDto = commandResult.Data as CollaboratorDto;

                    #endregion
                }
                else
                {
                    #region Updates Collaborator and User

                    var updateCollaboratorCommand = new UpdateTinyCollaborator(collaboratorDto, true);

                    updateCollaboratorCommand.UpdateBaseProperties(
                        cmd.MusicBandResponsibleApiDto.Name,
                        null,
                        cmd.MusicBandResponsibleApiDto.Email,
                        cmd.MusicBandResponsibleApiDto.PhoneNumber,
                        cmd.MusicBandResponsibleApiDto.CellPhone,
                        cmd.MusicBandResponsibleApiDto.Document);

                    updateCollaboratorCommand.UpdatePreSendProperties(
                        CollaboratorType.Music.Name,
                        cmd.UserId,
                        cmd.UserUid,
                        editionDto.Id,
                        editionDto.Uid,
                        "");

                    var commandResult = await base.CommandBus.Send(updateCollaboratorCommand);
                    if (!commandResult.IsValid)
                    {
                        var currentValidationResult = new ValidationResult();
                        foreach (var error in commandResult?.Errors)
                        {
                            currentValidationResult.Add(new ValidationError(error.Message));
                        }

                        if (!currentValidationResult.IsValid)
                        {
                            this.AppValidationResult.Add(currentValidationResult);
                            return this.AppValidationResult;
                        }
                    }

                    collaboratorDto = await collaboratorRepo.FindByEmailAsync(cmd.MusicBandResponsibleApiDto.Email, editionDto.Id);

                    #endregion
                }
            }

            var musicBand = new MusicBand(
                musicBandType,
                editionDto.Edition,
                cmd.Name,
                cmd.FormationDate,
                cmd.MainMusicInfluences,
                cmd.Facebook,
                cmd.Instagram,
                cmd.Twitter,
                cmd.Youtube,
                !string.IsNullOrEmpty(cmd.ImageFile),
                cmd.MusicProjectApiDto,
                collaboratorDto?.EditionAttendeeCollaborator,
                cmd.MusicGenresApiDtos,
                cmd.TargetAudiencesApiDtos,
                cmd.MusicBandMembersApiDtos,
                cmd.MusicBandTeamMembersApiDtos,
                cmd.ReleasedMusicProjectsApiDtos,
                cmd.UserId);

            if (!musicBand.IsValid())
            {
                this.AppValidationResult.Add(musicBand.ValidationResult);
                return this.AppValidationResult;
            }

            this.MusicBandRepo.Create(musicBand);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = musicBand;

            //Uploads the Image
            if (!string.IsNullOrEmpty(cmd.ImageFile))
            {
                ImageHelper.UploadOriginalAndThumbnailImages(
                   musicBand.Uid,
                   cmd.ImageFile,
                   FileRepositoryPathType.MusicBandImage);
            }

            return this.AppValidationResult;
        }
    }
}