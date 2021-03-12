// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="UpdateEditionMainInformationCommandHandler.cs" company="Softo">
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
    /// <summary>UpdateEditionMainInformationCommandHandler</summary>
    public class UpdateEditionMainInformationCommandHandler : EditionBaseCommandHandler, IRequestHandler<UpdateEditionMainInformation, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateEditionMainInformationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="editionRepository">The edition event repository.</param>
        public UpdateEditionMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IEditionRepository editionRepository)
            : base(eventBus, uow, editionRepository)
        {
            this.editionRepo = editionRepository;
        }

        /// <summary>Handles the specified update edition event main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateEditionMainInformation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var edition = await this.GetEditionByUid(cmd.EditionUid);

            #region Initial validations

            var existentUrlCodeEdition = editionRepo.FindByUrlCode(cmd.UrlCode);
            if (existentUrlCodeEdition != null && existentUrlCodeEdition.Uid != cmd.EditionUid)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.Edition.ToLowerInvariant(), $"{Labels.TheM.ToLowerInvariant()} {Labels.UrlCode.ToLowerInvariant()}", cmd.UrlCode), new string[] { "ToastrError" }));
            }

            var currentEditions = this.editionRepo.FindAllByIsCurrent()?
                                                    .Where(e => e.Id != cmd.EditionId) //Discard the currentEdition for the checks below
                                                    .ToList();

            if (!cmd.IsCurrent && currentEditions?.Count == 0)
            {
                this.ValidationResult.Add(new ValidationError(Messages.CanNotUncheckCurrentEdition, new string[] { "ToastrError" }));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            edition.UpdateMainInformation(cmd.Name,
                                          cmd.UrlCode,
                                          cmd.IsCurrent,
                                          cmd.IsActive,
                                          cmd.AttendeeOrganizationMaxSellProjectsCount,
                                          cmd.ProjectMaxBuyerEvaluationsCount,
                                          cmd.StartDate.Value,
                                          cmd.EndDate.Value,
                                          cmd.SellStartDate.Value,
                                          cmd.SellEndDate.Value,
                                          cmd.ProjectSubmitStartDate.Value,
                                          cmd.ProjectSubmitEndDate.Value,
                                          cmd.ProjectEvaluationStartDate.Value,
                                          cmd.ProjectEvaluationEndDate.Value,
                                          cmd.OneToOneMeetingsScheduleDate.Value,
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

            if (edition.IsCurrent && currentEditions?.Count > 0)
            {
                currentEditions.ForEach(e => e.DisableIsCurrent());
                this.EditionRepo.UpdateAll(currentEditions);
            }

            this.EditionRepo.Update(edition);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}