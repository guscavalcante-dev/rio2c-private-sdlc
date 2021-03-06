// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="CreateEditionCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateEditionCommandHandler</summary>
    public class CreateEditionCommandHandler : EditionBaseCommandHandler, IRequestHandler<CreateEdition, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;

        public CreateEditionCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IEditionRepository editionRepository)
            : base(eventBus, uow, editionRepository)
        {
            this.editionRepo = editionRepository;
        }

        public async Task<AppValidationResult> Handle(CreateEdition cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var editionUid = Guid.NewGuid();

            var edition = new Edition(editionUid,
                                      cmd.Name,
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
                                      cmd.AudiovisualNegotiationsCreateEndDate.Value,
                                      cmd.AudiovisualNegotiationsCreateEndDate.Value,
                                      cmd.UserId);
                
            if (!edition.IsValid())
            {
                this.AppValidationResult.Add(edition.ValidationResult);
                return this.AppValidationResult;
            }

            this.EditionRepo.Create(edition);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = edition;

            return this.AppValidationResult;
        }
    }
}