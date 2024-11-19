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
            IAttendeeMusicBandRepository attendeeMusicBandRepo)
            : base(commandBus, uow, musicBandRepo)
        {
            this.musicBandTypeRepo = musicBandTypeRepo;
            this.editionRepo = editionRepo;
            this.targetAudienceRepo = targetAudienceRepo;
            this.musicGenreRepo = musicGenreRepo;
            this.collaboratorRepo = collaboratorRepo;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepo;
            this.attendeeMusicBandRepo = attendeeMusicBandRepo;
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

            if (editionDto.IsMusicProjectSubmitEnded())
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

            var attendeeCollaboratorTicketsInformationDto = await attendeeCollaboratorRepo.FindUserTicketsInformationDtoByEmail(editionDto.Id, cmd.MusicBandResponsibleApiDto.Email);

            // Pitching validations
            if (cmd.MusicBandDataApiDtos.Any(dto => dto.WouldYouLikeParticipatePitching))
            {
                var attendeeMusicBandsCount = await this.attendeeMusicBandRepo.CountByEditionIdAsync(editionDto.Id);
                attendeeMusicBandsCount += cmd.MusicBandDataApiDtos.Count;
                if (attendeeMusicBandsCount > editionDto.MusicPitchingMaximumProjectsInEdition)
                {
                    string validationMessage = string.Format(
                        Messages.YouCanMusicPitchingMaximumProjectsInEdition,
                        editionDto.MusicPitchingMaximumProjectsInEdition,
                        Labels.MusicProjects
                    );
                    this.ValidationResult.Add(new ValidationError(validationMessage));
                    this.AppValidationResult.Add(this.ValidationResult);
                    return this.AppValidationResult;
                }
                
                attendeeMusicBandsCount = await this.attendeeMusicBandRepo.CountByResponsibleAsync(editionDto.Id, cmd.MusicBandResponsibleApiDto.Document, cmd.MusicBandResponsibleApiDto.Email);
                attendeeMusicBandsCount += cmd.MusicBandDataApiDtos.Count;
                if (attendeeMusicBandsCount > editionDto.MusicPitchingMaximumProjectsPerAttendee)
                {
                    string validationMessage = string.Format(
                        Messages.YouCanMusicPitchingMaximumProjectsPerAttendee,
                        editionDto.MusicPitchingMaximumProjectsPerAttendee,
                        Labels.MusicProjects,
                        Labels.Pitching
                    );
                    this.ValidationResult.Add(new ValidationError(validationMessage));
                    this.AppValidationResult.Add(this.ValidationResult);
                    return this.AppValidationResult;
                }

                if (attendeeCollaboratorTicketsInformationDto == null)
                {
                    // Is a new responsible, validate if have subscriptions available by document type
                    var newAttendeeCollaboratorTicketsInformationDto = new AttendeeCollaboratorTicketsInformationDto(editionDto.Edition);
                    if (!newAttendeeCollaboratorTicketsInformationDto.HasMusicPitchingProjectsSubscriptionsAvailable(
                            cmd.MusicBandResponsibleApiDto.Document, 
                            cmd.MusicBandDataApiDtos.Count(dto => dto.WouldYouLikeParticipatePitching))) 
                    {
                        string validationMessage = string.Format(Messages.YouCanSubmitMaxXProjectsFor,
                                                                 newAttendeeCollaboratorTicketsInformationDto.GetMusicPitchingMaxSellProjectsCount(cmd.MusicBandResponsibleApiDto.Document),
                                                                 Labels.MusicProjects,
                                                                 Labels.Pitching);
                        this.ValidationResult.Add(new ValidationError(validationMessage));
                        this.AppValidationResult.Add(this.ValidationResult);
                        return this.AppValidationResult;
                    }
                }
                else
                {
                    // Has no more subscriptions available
                    if (!attendeeCollaboratorTicketsInformationDto.HasMusicPitchingProjectsSubscriptionsAvailable(
                            cmd.MusicBandResponsibleApiDto.Document, 
                            cmd.MusicBandDataApiDtos.Count(dto => dto.WouldYouLikeParticipatePitching)))
                    {
                        this.ValidationResult.Add(new ValidationError(string.Format(Messages.ProjectRegistrationLimitReachedFor, Labels.MusicProjects, Labels.Pitching)));
                        this.AppValidationResult.Add(this.ValidationResult);
                        return this.AppValidationResult;
                    }
                }
            }

            // Business Rounds validations
            if (cmd.MusicBandDataApiDtos.Any(dto => dto.WouldYouLikeParticipateBusinessRound))
            {
                // Has no existent responsible, so, has no ticket. Cannot subscribe projects for Business Rounds
                if (attendeeCollaboratorTicketsInformationDto == null)
                {
                    this.ValidationResult.Add(new ValidationError(string.Format(Messages.NoTicketsFoundForEmail, cmd.MusicBandResponsibleApiDto.Email)));
                    this.AppValidationResult.Add(this.ValidationResult);
                    return this.AppValidationResult;
                }
                else
                {
                    // Has existent responsible, but, has no ticket. Cannot subscribe projects for Business Rounds
                    if (!attendeeCollaboratorTicketsInformationDto.HasTicket())
                    {
                        this.ValidationResult.Add(new ValidationError(string.Format(Messages.NoTicketsFoundForEmail, cmd.MusicBandResponsibleApiDto.Email)));
                        this.AppValidationResult.Add(this.ValidationResult);
                        return this.AppValidationResult;
                    }

                    // Has no more subscriptions available
                    if (!attendeeCollaboratorTicketsInformationDto.HasMusicBusinessRoundsProjectsSubscriptionsAvailable(
                            cmd.MusicBandDataApiDtos.Count(dto => dto.WouldYouLikeParticipateBusinessRound)))
                    {
                        this.ValidationResult.Add(new ValidationError(string.Format(Messages.ProjectRegistrationLimitReachedFor, Labels.MusicProjects, Labels.BusinessRound)));
                        this.AppValidationResult.Add(this.ValidationResult);
                        return this.AppValidationResult;
                    }
                }
            }

            #endregion

            #region Create or update Music Band Responsible as Collaborator

            var collaboratorDto = await collaboratorRepo.FindByEmailAsync(cmd.MusicBandResponsibleApiDto.Email, editionDto.Id);

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

                var updateCollaboratorCommand = new UpdateTinyCollaborator(collaboratorDto, true);

                updateCollaboratorCommand.UpdateBaseProperties(
                    cmd.MusicBandResponsibleApiDto.Name,
                    null,
                    cmd.MusicBandResponsibleApiDto.Email,
                    cmd.MusicBandResponsibleApiDto.PhoneNumber,
                    cmd.MusicBandResponsibleApiDto.CellPhone,
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

            collaboratorDto = await collaboratorRepo.FindByEmailAsync(cmd.MusicBandResponsibleApiDto.Email, editionDto.Id);

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
                    musicBandDataApiDto.MainMusicInfluences,
                    musicBandDataApiDto.Facebook,
                    musicBandDataApiDto.Instagram,
                    musicBandDataApiDto.Twitter,
                    musicBandDataApiDto.Youtube,
                    musicBandDataApiDto.Tiktok,
                    musicBandDataApiDto.WouldYouLikeParticipateBusinessRound,
                    musicBandDataApiDto.WouldYouLikeParticipatePitching,
                    !string.IsNullOrEmpty(musicBandDataApiDto.ImageFile),
                    musicBandDataApiDto.MusicProjectApiDto.ToMusicProject(cmd.UserId),
                    collaboratorDto?.EditionAttendeeCollaborator,
                    musicBandDataApiDto.MusicGenresApiDtos.Select(dto => dto.ToMusicBandGenre(cmd.UserId))?.ToList(),
                    musicBandDataApiDto.TargetAudiencesApiDtos.Select(dto => dto.ToMusicBandTargetAudience(cmd.UserId))?.ToList(),
                    musicBandDataApiDto.MusicBandMembersApiDtos.Select(dto => dto.ToMusicBandMember(cmd.UserId))?.ToList(),
                    musicBandDataApiDto.MusicBandTeamMembersApiDtos.Select(dto => dto.ToMusicBandTeamMember(cmd.UserId))?.ToList(),
                    musicBandDataApiDto.ReleasedMusicProjectsApiDtos.Select(dto => dto.ToReleasedMusicProject(cmd.UserId))?.ToList(),
                    cmd.UserId);

                if (!musicBand.IsValid())
                {
                    this.AppValidationResult.Add(musicBand.ValidationResult);
                    return this.AppValidationResult;
                }

                this.MusicBandRepo.Create(musicBand);
                this.Uow.SaveChanges();
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
                // TargetAudiencesApiDtos
                // ---------------------------------------------------
                musicBandDataApiDto.TargetAudiencesApiDtos = musicBandDataApiDto.TargetAudiencesApiDtos.Select(ta =>
                                                               new TargetAudienceApiDto()
                                                               {
                                                                   Uid = ta.Uid,
                                                                   TargetAudience = this.targetAudienceRepo.Get(ta.Uid)
                                                               }).ToList();

                if (musicBandDataApiDto.TargetAudiencesApiDtos.Any(dto => dto.TargetAudience == null))
                {
                    var uidsNotFound = musicBandDataApiDto.TargetAudiencesApiDtos.Where(dto => dto.TargetAudience == null).Select(dto => dto.Uid);

                    validationResult.Add(new ValidationError(string.Format(
                        Messages.EntityNotAction,
                        $@"{MusicBandDataApiDto.GetJsonPropertyAttributeName(nameof(MusicBandDataApiDto.TargetAudiencesApiDtos))}: {string.Join(", ", uidsNotFound)}",
                        Labels.FoundM)));
                }

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