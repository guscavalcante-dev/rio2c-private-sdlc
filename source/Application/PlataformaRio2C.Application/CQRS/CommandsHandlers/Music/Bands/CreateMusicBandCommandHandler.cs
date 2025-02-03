// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 23-03-2021
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 11-19-2024
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
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IAttendeeMusicBandRepository attendeeMusicBandRepo;
        private readonly ICountryRepository countryRepo;

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
        /// <param name="attendeeCollaboratorRepo">The attendee collaborator repo.</param>
        /// <param name="attendeeMusicBandRepo">The attendee music band repo.</param>
        /// <param name="countryRepo">The country repo.</param>
        public CreateMusicBandCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            IMusicBandRepository musicBandRepo,
            IMusicBandTypeRepository musicBandTypeRepo,
            IEditionRepository editionRepo,
            ITargetAudienceRepository targetAudienceRepo,
            IMusicGenreRepository musicGenreRepo,
            ICollaboratorRepository collaboratorRepo,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepo,
            IAttendeeMusicBandRepository attendeeMusicBandRepo,
            ICountryRepository countryRepo)
            : base(commandBus, uow, musicBandRepo)
        {
            this.musicBandTypeRepo = musicBandTypeRepo;
            this.editionRepo = editionRepo;
            this.targetAudienceRepo = targetAudienceRepo;
            this.musicGenreRepo = musicGenreRepo;
            this.collaboratorRepo = collaboratorRepo;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepo;
            this.attendeeMusicBandRepo = attendeeMusicBandRepo;
            this.countryRepo = countryRepo;
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
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Edition, Labels.FoundF)));
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            if (editionDto.IsMusicPitchingProjectSubmitEnded())
            {
                this.ValidationResult.Add(new ValidationError(Messages.ProjectSubmitPeriodClosed));
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            var listsValidationResult = this.UpdateAndValidateLists(cmd);
            if (!listsValidationResult.IsValid)
            {
                this.AppValidationResult.Add(listsValidationResult);
                return this.AppValidationResult;
            }

            if (cmd.MusicBandResponsibleApiDto == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(cmd.MusicBandResponsibleApiDto))));
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            #region Validates User Ticket and Project registrations available

            // Validates maximum project submissions by Edition
            var attendeeMusicBandsCount = await this.attendeeMusicBandRepo.CountByEditionIdAsync(editionDto.Id);
            attendeeMusicBandsCount += cmd.MusicBandDataApiDtos.Count;
            if (attendeeMusicBandsCount > editionDto.MusicPitchingMaximumProjectSubmissionsByEdition)
            {
                string validationMessage = string.Format(
                    Messages.YouCanMusicPitchingMaximumProjectSubmissionsByEdition,
                    editionDto.MusicPitchingMaximumProjectSubmissionsByEdition,
                    Labels.MusicProjects
                );
                this.ValidationResult.Add(new ValidationError(validationMessage));
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            // Validates maximum project submissions by Responsible Document
            var country = await this.countryRepo.FindByNameAsync(cmd.MusicBandResponsibleApiDto.Country);

            var attendeeCollaboratorTicketsInformationDto = await attendeeCollaboratorRepo.FindUserTicketsInformationDtoByDocument(
                editionDto.Id,
                cmd.MusicBandResponsibleApiDto.Document);

            if (attendeeCollaboratorTicketsInformationDto == null)
            {
                attendeeCollaboratorTicketsInformationDto = new AttendeeCollaboratorTicketsInformationDto(editionDto.Edition);
            }

            if (!attendeeCollaboratorTicketsInformationDto.HasMusicPitchingProjectsSubscriptionsAvailable(
                        cmd.MusicBandResponsibleApiDto.Document,
                        cmd.MusicBandResponsibleApiDto.IsCompany,
                        country,
                        cmd.MusicBandDataApiDtos.Count()))
            {
                var musicPitchingMaxSellProjectsCount = attendeeCollaboratorTicketsInformationDto.GetMusicPitchingMaxSellProjectsCount(
                    cmd.MusicBandResponsibleApiDto.Document,
                    cmd.MusicBandResponsibleApiDto.IsCompany,
                    country);

                string validationMessage = string.Format(Messages.YouCanSubmitMaxXProjectsFor,
                                                         musicPitchingMaxSellProjectsCount,
                                                         Labels.MusicProjects,
                                                         Labels.Pitching);

                this.ValidationResult.Add(new ValidationError(validationMessage));
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            #region Create or update Music Band Responsible as Collaborator

            var collaboratorDto = await collaboratorRepo.FindByDocumentAsync(cmd.MusicBandResponsibleApiDto.Document, editionDto.Id);

            string musicBandResponsibleName = string.IsNullOrEmpty(cmd.MusicBandResponsibleApiDto.Name) ? 
                cmd.MusicBandResponsibleApiDto.StageName : 
                cmd.MusicBandResponsibleApiDto.Name;

            if (collaboratorDto == null)
            {
                #region Creates new Collaborator and User

                var createCollaboratorCommand = new CreateTinyCollaborator();

                createCollaboratorCommand.UpdateBaseProperties(
                    musicBandResponsibleName,
                    null,
                    cmd.MusicBandResponsibleApiDto.StageName,
                    cmd.MusicBandResponsibleApiDto.Email,
                    cmd.MusicBandResponsibleApiDto.PhoneNumber,
                    null,
                    cmd.MusicBandResponsibleApiDto.Document,
                    cmd.MusicBandResponsibleApiDto.Address,
                    cmd.MusicBandResponsibleApiDto.Country,
                    cmd.MusicBandResponsibleApiDto.State,
                    cmd.MusicBandResponsibleApiDto.City,
                    cmd.MusicBandResponsibleApiDto.ZipCode,
                    true);

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

                var updateCollaboratorCommand = new UpdateTinyCollaborator(collaboratorDto, true, true);

                updateCollaboratorCommand.UpdateBaseProperties(
                    musicBandResponsibleName,
                    null,
                    cmd.MusicBandResponsibleApiDto.StageName,
                    cmd.MusicBandResponsibleApiDto.Email,
                    cmd.MusicBandResponsibleApiDto.PhoneNumber,
                    null,
                    cmd.MusicBandResponsibleApiDto.Document,
                    cmd.MusicBandResponsibleApiDto.Address,
                    cmd.MusicBandResponsibleApiDto.Country,
                    cmd.MusicBandResponsibleApiDto.State,
                    cmd.MusicBandResponsibleApiDto.City,
                    cmd.MusicBandResponsibleApiDto.ZipCode,
                    true);

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

                #endregion
            }

            collaboratorDto = await collaboratorRepo.FindByDocumentAsync(cmd.MusicBandResponsibleApiDto.Document, editionDto.Id);

            #endregion

            foreach (var musicBandDataApiDto in cmd.MusicBandDataApiDtos)
            {
                var musicBandType = await musicBandTypeRepo.FindByUidAsync(musicBandDataApiDto.MusicBandTypeUid);

                // TODO: Music Band has no Update method because the MyRio2C customer hasn't defined the fields that will validate if a band already exists!
                // Defining this is important to prevent a band from registering several times!
                var musicBand = new MusicBand(
                    musicBandType,
                    editionDto.Edition,
                    musicBandDataApiDto.Name,
                    musicBandDataApiDto.FormationDate,
                    musicBandDataApiDto.Deezer,
                    musicBandDataApiDto.Instagram,
                    musicBandDataApiDto.Spotify,
                    musicBandDataApiDto.Youtube,
                    !string.IsNullOrEmpty(musicBandDataApiDto.ImageFile),
                    musicBandDataApiDto.MusicProjectApiDto.ToMusicProject(cmd.UserId),
                    collaboratorDto?.EditionAttendeeCollaborator,
                    musicBandDataApiDto.MusicGenresApiDtos.Select(dto => dto.ToMusicBandGenre(cmd.UserId))?.ToList(),
                    musicBandDataApiDto.MusicBandTeamMembersApiDtos.Select(dto => dto.ToMusicBandTeamMember(cmd.UserId))?.ToList(),
                    musicBandDataApiDto.ReleasedMusicProjectsApiDtos.Select(dto => dto.ToReleasedMusicProject(cmd.UserId))?.ToList(),
                    cmd.UserId);

                if (!musicBand.IsValid())
                {
                    this.AppValidationResult.Add(musicBand.ValidationResult);
                    return this.AppValidationResult;
                }

                this.MusicBandRepo.Create(musicBand);
                var saveChangesResult = this.Uow.SaveChanges();
                if (!saveChangesResult.Success)
                    throw new System.Exception("An error occurred while saving the band information to the database.");
                    //this.AppValidationResult.Data = musicBand;

                //Uploads the Image
                if (!string.IsNullOrEmpty(musicBandDataApiDto.ImageFile))
                {
                    ImageHelper.UploadOriginalAndThumbnailImages(
                       musicBand.Uid,
                       musicBandDataApiDto.ImageFile,
                       FileRepositoryPathType.MusicBandImage);
                }
            }

            return this.AppValidationResult;
        }

        /// <summary>
        /// Updates and validate the lists.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        private ValidationResult UpdateAndValidateLists(CreateMusicBand cmd)
        {
            ValidationResult validationResult = new ValidationResult();

            foreach (var musicBandDataApiDto in cmd.MusicBandDataApiDtos)
            {
                // ---------------------------------------------------
                // MusicGenresApiDtos
                // ---------------------------------------------------
                musicBandDataApiDto.MusicGenresApiDtos = musicBandDataApiDto.MusicGenresApiDtos.Select(mg =>
                                                            new MusicGenreApiDto()
                                                            {
                                                                Uid = mg.Uid,
                                                                AdditionalInfo = mg.AdditionalInfo,
                                                                MusicGenre = this.musicGenreRepo.Get(mg.Uid)
                                                            }).ToList();

                if (musicBandDataApiDto.MusicGenresApiDtos.Any(dto => dto.MusicGenre == null))
                {
                    var uidsNotFound = musicBandDataApiDto.MusicGenresApiDtos.Where(dto => dto.MusicGenre == null).Select(dto => dto.Uid);

                    validationResult.Add(new ValidationError(string.Format(
                        Messages.EntityNotAction,
                        $@"{MusicBandDataApiDto.GetJsonPropertyAttributeName(nameof(MusicBandDataApiDto.MusicGenresApiDtos))}: {string.Join(", ", uidsNotFound)}",
                        Labels.FoundM)));
                }
            }

            return validationResult;
        }
    }
}