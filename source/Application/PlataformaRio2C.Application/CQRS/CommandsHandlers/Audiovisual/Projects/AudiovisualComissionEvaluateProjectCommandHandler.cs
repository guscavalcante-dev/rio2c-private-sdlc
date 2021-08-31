// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-28-2021
// ***********************************************************************
// <copyright file="AudiovisualComissionEvaluateProjectCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>AudiovisualComissionEvaluateProjectCommandHandler</summary>
    public class AudiovisualComissionEvaluateProjectCommandHandler : BaseProjectCommandHandler, IRequestHandler<AudiovisualComissionEvaluateProject, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IUserRepository userRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudiovisualComissionEvaluateProjectCommandHandler"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="projectRepository">The music band repo.</param>
        /// <param name="editionRepo">The edition repo.</param>
        /// <param name="userRepo">The user repo.</param>
        public AudiovisualComissionEvaluateProjectCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IProjectRepository projectRepository,
            IEditionRepository editionRepo,
            IUserRepository userRepo
            )
            : base(commandBus, uow, attendeeOrganizationRepository, projectRepository)
        {
            this.editionRepo = editionRepo;
            this.userRepo = userRepo;
        }

        /// <summary>
        /// Handles the specified evaluate audiovisual commission project.
        /// </summary>
        /// <param name="evaluateAudiovisualCommissionProject">The evaluate audiovisual commission project.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(AudiovisualComissionEvaluateProject cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            if (!cmd.Grade.HasValue)
            {
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.Grade, "10", "0"), new string[] { nameof(AudiovisualComissionEvaluateProject.Grade) })));
                return this.AppValidationResult;
            }

            var editionDto = await editionRepo.FindDtoAsync(cmd.EditionId.Value);
            if (editionDto.IsAudiovisualCommissionProjectEvaluationOpen() != true)
            {
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(Texts.ForbiddenErrorMessage, new string[] { "ToastrError" })));
                return this.AppValidationResult;
            }

            var project = await ProjectRepo.FindByIdAsync(cmd.ProjectId.Value);
            project.AudiovisualComissionEvaluateProject(
                await userRepo.FindByIdAsync(cmd.UserId),
                cmd.Grade.Value,
                cmd.IsAdmin);

            if (!project.IsValid())
            {
                this.AppValidationResult.Add(project.ValidationResult);
                return this.AppValidationResult;
            }

            this.ProjectRepo.Update(project);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = project;

            return this.AppValidationResult;
        }
    }
}