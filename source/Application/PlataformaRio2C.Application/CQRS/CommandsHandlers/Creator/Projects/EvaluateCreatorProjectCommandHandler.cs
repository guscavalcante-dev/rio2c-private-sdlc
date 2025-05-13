// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-26-2024
// ***********************************************************************
// <copyright file="EvaluateCreatorProjectCommandHandler.cs" company="Softo">
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
    /// <summary>EvaluateCreatorProjectCommandHandler</summary>
    public class EvaluateCreatorProjectCommandHandler : BaseCommandHandler, IRequestHandler<EvaluateCreatorProject, AppValidationResult>
    {
        private readonly ICreatorProjectRepository creatorProjectRepository;
        private readonly IEditionRepository editionRepo;
        private readonly IUserRepository userRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="EvaluateCreatorProjectCommandHandler" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="creatorProjectRepository">The repo.</param>
        /// <param name="editionRepo">The edition repo.</param>
        /// <param name="userRepo">The user repo.</param>
        public EvaluateCreatorProjectCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            ICreatorProjectRepository creatorProjectRepository,
            IEditionRepository editionRepo,
            IUserRepository userRepo
            )
            : base(commandBus, uow)
        {
            this.creatorProjectRepository = creatorProjectRepository;
            this.editionRepo = editionRepo;
            this.userRepo = userRepo;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="cmd">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="TargetAudienceApiDto"></exception>
        public async Task<AppValidationResult> Handle(EvaluateCreatorProject cmd, CancellationToken cancellationToken)
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
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(cmd.Grade)), new string[] { "Grade" })));
                return this.AppValidationResult;
            }

            var editionDto = await editionRepo.FindDtoAsync(cmd.EditionId.Value);
            if (editionDto.IsCreatorProjectEvaluationOpen() != true)
            {
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(Texts.ForbiddenErrorMessage, new string[] { "ToastrError" })));
                return this.AppValidationResult;
            }

            var creatorProject = await creatorProjectRepository.GetAsync(cmd.CreatorProjectId.Value);
            if (creatorProject == null)
            {
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM.ToLowerInvariant()), new string[] { "ToastrError" })));
                return this.AppValidationResult;
            }

            creatorProject.Evaluate(
                editionDto.Edition,
                await userRepo.FindByIdAsync(cmd.UserId),
                cmd.Grade.Value);

            if (!creatorProject.IsValid())
            {
                this.AppValidationResult.Add(creatorProject.ValidationResult);
                return this.AppValidationResult;
            }

            this.creatorProjectRepository.Update(creatorProject);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = creatorProject;

            return this.AppValidationResult;
        }
    }
}