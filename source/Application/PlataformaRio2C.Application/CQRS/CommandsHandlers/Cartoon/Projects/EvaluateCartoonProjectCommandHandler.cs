// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-28-2021
//
// Last Modified By : Rafael Franco
// Last Modified On : 02-10-2022
// ***********************************************************************
// <copyright file="EvaluateCartoonProjectCommandHandler.cs" company="Softo">
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
    /// <summary>EvaluateCartoonProjectCommandHandler</summary>
    public class EvaluateCartoonProjectCommandHandler : CartoonProjectBaseCommandHandler, IRequestHandler<EvaluateCartoonProject, AppValidationResult>
    {
        private readonly ICartoonProjectRepository CartoonProjectRepository;
        private readonly IEditionRepository editionRepo;
        private readonly IUserRepository userRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="EvaluateCartoonProjectCommandHandler"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="cartoonProjectRepository">The repo.</param>
        /// <param name="editionRepo">The edition repo.</param>
        /// <param name="userRepo">The user repo.</param>
        public EvaluateCartoonProjectCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            ICartoonProjectRepository cartoonProjectRepository,
            IEditionRepository editionRepo,
            IUserRepository userRepo
            )
            : base(commandBus, uow, cartoonProjectRepository)
        {
            this.CartoonProjectRepository = cartoonProjectRepository;
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
        public async Task<AppValidationResult> Handle(EvaluateCartoonProject cmd, CancellationToken cancellationToken)
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
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired), new string[] { "Grade" })));
                return this.AppValidationResult;
            }

            var editionDto = await editionRepo.FindDtoAsync(cmd.EditionId.Value);
            if (editionDto.IsCartoonProjectEvaluationOpen() != true)
            {
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(Texts.ForbiddenErrorMessage, new string[] { "ToastrError" })));
                return this.AppValidationResult;
            }

            var cartoonProject = await CartoonProjectRepository.FindByIdAsync(cmd.CartoonProjectId.Value);
            cartoonProject.Evaluate(
                editionDto.Edition,
                await userRepo.FindByIdAsync(cmd.UserId),
                cmd.Grade.Value);

            if (!cartoonProject.IsValid())
            {
                this.AppValidationResult.Add(cartoonProject.ValidationResult);
                return this.AppValidationResult;
            }

            this.CartoonProjectRepository.Update(cartoonProject);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = cartoonProject;

            return this.AppValidationResult;
        }
    }
}