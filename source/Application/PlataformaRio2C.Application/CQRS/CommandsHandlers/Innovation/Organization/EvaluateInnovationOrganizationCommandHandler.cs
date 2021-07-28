// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-28-2021
// ***********************************************************************
// <copyright file="EvaluateInnovationOrganizationCommandHandler.cs" company="Softo">
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
    /// <summary>EvaluateInnovationOrganizationCommandHandler</summary>
    public class EvaluateInnovationOrganizationCommandHandler : InnovationOrganizationBaseCommandHandler, IRequestHandler<EvaluateInnovationOrganization, AppValidationResult>
    {
        private readonly IInnovationOrganizationRepository innovationOrganizationRepo;
        private readonly IEditionRepository editionRepo;
        private readonly IUserRepository userRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="EvaluateInnovationOrganizationCommandHandler"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="innovationOrganizationRepo">The music band repo.</param>
        /// <param name="editionRepo">The edition repo.</param>
        /// <param name="userRepo">The user repo.</param>
        public EvaluateInnovationOrganizationCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            IInnovationOrganizationRepository innovationOrganizationRepo,
            IEditionRepository editionRepo,
            IUserRepository userRepo
            )
            : base(commandBus, uow, innovationOrganizationRepo)
        {
            this.innovationOrganizationRepo = innovationOrganizationRepo;
            this.editionRepo = editionRepo;
            this.userRepo = userRepo;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="TargetAudienceApiDto"></exception>
        public async Task<AppValidationResult> Handle(EvaluateInnovationOrganization cmd, CancellationToken cancellationToken)
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
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.Grade, "10", "0"), new string[] { nameof(EvaluateInnovationOrganization.Grade) })));
                return this.AppValidationResult;
            }

            var editionDto = await editionRepo.FindDtoAsync(cmd.EditionId.Value);
            if (editionDto.IsInnovationProjectEvaluationOpen() != true)
            {
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(Texts.ForbiddenErrorMessage, new string[] { "ToastrError" })));
                return this.AppValidationResult;
            }

            var innovationOrganization = await innovationOrganizationRepo.FindByIdAsync(cmd.InnovationOrganizationId.Value);
            innovationOrganization.Evaluate(
                editionDto.Edition,
                await userRepo.FindByIdAsync(cmd.UserId),
                cmd.Grade.Value);

            if (!innovationOrganization.IsValid())
            {
                this.AppValidationResult.Add(innovationOrganization.ValidationResult);
                return this.AppValidationResult;
            }

            this.InnovationOrganizationRepo.Update(innovationOrganization);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = innovationOrganization;

            return this.AppValidationResult;
        }
    }
}