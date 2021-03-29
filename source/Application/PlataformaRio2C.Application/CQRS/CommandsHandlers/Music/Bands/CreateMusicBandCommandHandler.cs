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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateMusicBandCommandHandler</summary>
    public class CreateMusicBandCommandHandler : MusicBandBaseCommandHandler, IRequestHandler<CreateMusicBand, AppValidationResult>
    {
        private readonly IMusicBandRepository musicBandRepo;
        private readonly IMusicBandTypeRepository musicBandTypeRepo;
        private readonly IEditionRepository editionRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IMusicGenreRepository musicGenreRepo;
        private readonly ICollaboratorTypeRepository collaboratorTypeRepo;
        private readonly ICollaboratorRepository collaboratorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicBandCommandHandler"/> class.
        /// </summary>
        /// <param name="commandBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="musicBandRepo">The music band repo.</param>
        /// <param name="musicBandTypeRepo">The music band type repo.</param>
        /// <param name="editionRepo">The edition repo.</param>
        public CreateMusicBandCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            IMusicBandRepository musicBandRepo,
            IMusicBandTypeRepository musicBandTypeRepo,
            IEditionRepository editionRepo,
            ITargetAudienceRepository targetAudienceRepo,
            IMusicGenreRepository musicGenreRepo,
            ICollaboratorTypeRepository collaboratorTypeRepo,
            ICollaboratorRepository collaboratorRepo
            )
            : base(commandBus, uow, musicBandRepo)
        {
            this.musicBandRepo = musicBandRepo;
            this.musicBandTypeRepo = musicBandTypeRepo;
            this.editionRepo = editionRepo;
            this.targetAudienceRepo = targetAudienceRepo;
            this.musicGenreRepo = musicGenreRepo;
            this.collaboratorTypeRepo = collaboratorTypeRepo;
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

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var musicBandApiDto = cmd.MusicBandApiDto;

            musicBandApiDto.TargetAudiencesApiDtos = musicBandApiDto.TargetAudiencesApiDtos.Select(ta =>
                                                    new TargetAudienceApiDto()
                                                    {
                                                        Id = ta.Id,
                                                        TargetAudience = targetAudienceRepo.Get(ta.Id)
                                                    }).ToList();

            musicBandApiDto.MusicGenresApiDtos = musicBandApiDto.MusicGenresApiDtos.Select(mg =>
                                                    new MusicGenreApiDto()
                                                    {
                                                        Id = mg.Id,
                                                        MusicGenre = musicGenreRepo.Get(mg.Id)
                                                    }).ToList();

            var musicBandType = await musicBandTypeRepo.FindByIdAsync(musicBandApiDto.MusicBandTypeId);
            var edition = await editionRepo.FindByIdAsync(cmd.EditionId ?? 0);

            Collaborator collaborator = null;
            var projectResponsible = musicBandApiDto.MusicBandMembersApiDtos.FirstOrDefault(mbm => mbm.IsProjectResponsible);
            if (projectResponsible != null)
            {
                #region Opção 1 - Criar colaborador via command

                collaborator = await collaboratorRepo.FindByEmailAsync(projectResponsible.Email);

                if (collaborator == null)
                {
                    var createCollaboratorCommand = new CreateTinyCollaborator();
                    createCollaboratorCommand.UpdateBaseProperties(projectResponsible.Name, null, projectResponsible.Email);
                    //"Music" is fixed because in [dbo].[MigrateMusicProjects] procedure, its is fixed too!
                    createCollaboratorCommand.UpdatePreSendProperties("Music", cmd.UserId, cmd.UserUid, edition.Id, edition.Uid, "");

                    var commandResult = await base.CommandBus.Send(createCollaboratorCommand);
                    if (!commandResult.IsValid)
                    {
                        throw new DomainException(commandResult.Errors.Select(e => e.Message).FirstOrDefault().ToString());
                    }

                    collaborator = commandResult.Data as Collaborator;
                }

                #endregion

                #region Opção 2 - Criar colaborador via new object (Disabled)

                ////TODO: É esse collaboratorType que deve usar pra criar um colaborador para a banda?
                //var collaboratorType = await collaboratorTypeRepo.FindByNameAsync(Domain.Constants.CollaboratorType.Music);

                ////TODO: Deve ser separado Name e LastName em MusicBandMemberApiDto? Está passando nulo aqui e na proc do SQL também.
                //var collaborator = new Collaborator(edition, collaboratorType, projectResponsible.Name, null, projectResponsible.Email, cmd.UserId);
                //if (!collaborator.IsValid())
                //{
                //    this.AppValidationResult.Add(collaborator.ValidationResult);
                //    return this.AppValidationResult;
                //}

                //this.collaboratorRepo.Create(collaborator);
                //this.Uow.SaveChanges();
                ////this.AppValidationResult.Data = collaborator;

                #endregion

                #region Opção 3 - Criar via event (Not Implemented)

                //TODO: Implementar evento que cria colaborador aqui!

                #endregion
            }

            var musicBand = new MusicBand(
                musicBandType,
                edition,
                musicBandApiDto.Name,
                musicBandApiDto.ImageUrl,
                musicBandApiDto.FormationDate,
                musicBandApiDto.MainMusicInfluences,
                musicBandApiDto.Facebook,
                musicBandApiDto.Instagram,
                musicBandApiDto.Twitter,
                musicBandApiDto.Youtube,
                musicBandApiDto.MusicProjectApiDto,
                collaborator?.AttendeeCollaborators?.FirstOrDefault(),
                musicBandApiDto.MusicGenresApiDtos,
                musicBandApiDto.TargetAudiencesApiDtos,
                musicBandApiDto.MusicBandMembersApiDtos,
                musicBandApiDto.MusicBandTeamMembersApiDtos,
                musicBandApiDto.ReleasedMusicProjectsApiDtos,
                cmd.UserId);

            if (!musicBand.IsValid())
            {
                this.AppValidationResult.Add(musicBand.ValidationResult);
                return this.AppValidationResult;
            }

            this.MusicBandRepo.Create(musicBand);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = musicBand;

            return this.AppValidationResult;
        }
    }
}