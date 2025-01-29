// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-07-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="BaseProjectCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Interfaces.Repositories.Music.Projects;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>BaseProjectCommandHandler</summary>
    public class BaseProjectCommandHandler : BaseCommandHandler
    {
        protected readonly IAttendeeOrganizationRepository AttendeeOrganizationRepo;
        protected readonly IProjectRepository ProjectRepo;
        protected readonly IMusicBusinessRoundProjectRepository musicBusinessRoundProjectRepo;


        /// <summary>Initializes a new instance of the <see cref="BaseProjectCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="projectRepository">The project repository.</param>
        public BaseProjectCommandHandler(IMediator eventBus, IUnitOfWork uow, IAttendeeOrganizationRepository attendeeOrganizationRepository, IProjectRepository projectRepository, IMusicBusinessRoundProjectRepository musicBusinessRoundProjectRepo)
            : base(eventBus, uow)
        {
            this.AttendeeOrganizationRepo = attendeeOrganizationRepository;
            this.ProjectRepo = projectRepository;
            this.musicBusinessRoundProjectRepo = musicBusinessRoundProjectRepo;
        }

        /// <summary>Gets the attendee organization by uid.</summary>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <returns></returns>
        public async Task<AttendeeOrganization> GetAttendeeOrganizationByUid(Guid attendeeOrganizationUid)
        {
            var attendeeOrganization = await this.AttendeeOrganizationRepo.GetAsync(attendeeOrganizationUid);
            if (attendeeOrganization == null || attendeeOrganization.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.CompanyName, Labels.FoundF), new string[] { "ToastrError" }));
                return null;
            }

            return attendeeOrganization;
        }

        /// <summary>Gets the project by uid.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<Project> GetProjectByUid(Guid projectUid)
        {
            var project = await this.ProjectRepo.GetAsync(projectUid);
            if (project == null || project.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM), new string[] { "ToastrError" }));
                return null;
            }

            return project;
        }

        /// <summary>Gets the project by uid.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<MusicBusinessRoundProject> GetMusicBusinessRoundProjectByUid(Guid projectUid)
        {
            var project = await this.musicBusinessRoundProjectRepo.GetAsync(projectUid);
            if (project == null || project.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM), new string[] { "ToastrError" }));
                return null;
            }

            return project;
        }
    }
}