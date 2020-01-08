// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-08-2020
// ***********************************************************************
// <copyright file="SendProjectsBuyersEvaluationsEmailsAsyncCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>SendProjectsBuyersEvaluationsEmailsAsyncCommandHandler</summary>
    public class SendProjectsBuyersEvaluationsEmailsAsyncCommandHandler : BaseCommandHandler, IRequestHandler<SendProjectsBuyersEvaluationsEmailsAsync, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IProjectBuyerEvaluationRepository projectBuyerEvaluationRepo;

        /// <summary>Initializes a new instance of the <see cref="SendProjectsBuyersEvaluationsEmailsAsyncCommandHandler"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="projectBuyerEvaluationRepository">The project buyer evaluation repository.</param>
        public SendProjectsBuyersEvaluationsEmailsAsyncCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            IEditionRepository editionRepository,
            IProjectBuyerEvaluationRepository projectBuyerEvaluationRepository)
            : base(commandBus, uow)
        {
            this.editionRepo = editionRepository;
            this.projectBuyerEvaluationRepo = projectBuyerEvaluationRepository;
        }

        /// <summary>Handles the specified send projects buyers evaluations emails asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(SendProjectsBuyersEvaluationsEmailsAsync cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var currentEdition = await this.editionRepo.FindByIsCurrentAsync();
            if (currentEdition == null)
            {
                this.ValidationResult.Add(new ValidationError("Send projects buyers evaluations emails will not be executed becaused there is no edition configured as current."));
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            // Check if is project evaluation period
            if (DateTime.Now < currentEdition.ProjectEvaluationStartDate || DateTime.Now > currentEdition.ProjectEvaluationEndDate)
            {
                return this.AppValidationResult;
            }

            var unsentProjectBuyerEvaluations = await this.projectBuyerEvaluationRepo.FindAllBuyerEmailDtosAsync(currentEdition.Id, currentEdition.ProjectEvaluationStartDate, currentEdition.ProjectEvaluationEndDate);
            if (unsentProjectBuyerEvaluations?.Any() != true)
            {
                return this.AppValidationResult;
            }

            foreach (var unsentProjectBuyerEvaluation in unsentProjectBuyerEvaluations)
            {
                var currentValidationResult = new ValidationResult();

                try
                {
                    unsentProjectBuyerEvaluation.ProjectBuyerEvaluation.SendBuyerEmail();

                    foreach (var emailRecipientsDto in unsentProjectBuyerEvaluation.EmailRecipientsDtos)
                    {
                        try
                        {
                            var response = await this.CommandBus.Send(new SendProjectBuyerEvaluationEmailAsync(
                                    unsentProjectBuyerEvaluation,
                                    emailRecipientsDto,
                                    currentEdition),
                                cancellationToken);
                            foreach (var error in response?.Errors)
                            {
                                currentValidationResult.Add(new ValidationError(error.Message));
                            }
                        }
                        catch
                        {
                            currentValidationResult.Add(new ValidationError($"Error sending the email for user {emailRecipientsDto.RecipientUser.Name} (email: {emailRecipientsDto.RecipientUser.Email}; uid: {emailRecipientsDto.RecipientUser.Uid}, buyer evaluation uid: {unsentProjectBuyerEvaluation.ProjectBuyerEvaluation.Uid })."));
                        }
                    }

                    if (!currentValidationResult.IsValid)
                    {
                        this.AppValidationResult.Add(currentValidationResult);
                        continue;
                    }

                    this.projectBuyerEvaluationRepo.Update(unsentProjectBuyerEvaluation.ProjectBuyerEvaluation);
                    this.Uow.SaveChanges();
                }
                catch
                {
                    currentValidationResult.Add(new ValidationError($"Error sending the email for organization {unsentProjectBuyerEvaluation.SellerOrganization.TradeName} (buyer evaluation uid: {unsentProjectBuyerEvaluation.ProjectBuyerEvaluation.Uid })."));
                }

                if (!currentValidationResult.IsValid)
                {
                    this.AppValidationResult.Add(currentValidationResult);
                }
            }

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}