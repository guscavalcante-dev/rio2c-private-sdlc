// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 20-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 20-03-2021
// ***********************************************************************
// <copyright file="UpdateEditionDatesInformationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateEditionDatesInformationCommandHandler</summary>
    public class UpdateEditionDatesInformationCommandHandler : EditionBaseCommandHandler, IRequestHandler<UpdateEditionDatesInformation, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateEditionDatesInformationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="editionRepository">The edition event repository.</param>
        public UpdateEditionDatesInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IEditionRepository editionRepository)
            : base(eventBus, uow, editionRepository)
        {
            this.editionRepo = editionRepository;
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateEditionDatesInformation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var edition = await this.GetEditionByUid(cmd.EditionUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            edition.UpdateDatesInformation(cmd.SellStartDate.Value,
                                          cmd.SellEndDate.Value,
                                          cmd.ProjectSubmitStartDate.Value,
                                          cmd.ProjectSubmitEndDate.Value,
                                          cmd.ProjectEvaluationStartDate.Value,
                                          cmd.ProjectEvaluationEndDate.Value,
                                          cmd.NegotiationStartDate.Value,
                                          cmd.NegotiationEndDate.Value,
                                          cmd.MusicProjectSubmitStartDate.Value,
                                          cmd.MusicProjectSubmitEndDate.Value,
                                          cmd.MusicProjectEvaluationStartDate.Value,
                                          cmd.MusicProjectEvaluationEndDate.Value,
                                          cmd.InnovationProjectSubmitStartDate.Value,
                                          cmd.InnovationProjectSubmitEndDate.Value,
                                          cmd.InnovationProjectEvaluationStartDate.Value,
                                          cmd.InnovationProjectEvaluationEndDate.Value,
                                          cmd.AudiovisualNegotiationsCreateStartDate.Value,
                                          cmd.AudiovisualNegotiationsCreateEndDate.Value,
                                          cmd.UserId);
            
            if (!edition.IsValid())
            {
                this.AppValidationResult.Add(edition.ValidationResult);
                return this.AppValidationResult;
            }

            this.EditionRepo.Update(edition);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}