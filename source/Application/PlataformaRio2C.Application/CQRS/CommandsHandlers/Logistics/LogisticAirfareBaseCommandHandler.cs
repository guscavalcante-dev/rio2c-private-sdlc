// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-13-2020
// ***********************************************************************
// <copyright file="PillarBaseCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>PillarBaseCommandHandler</summary>
    public class LogisticAirfareBaseCommandHandler : BaseCommandHandler
    {
        protected readonly ILogisticAirfareRepository LogisticAirfareRepo;

        /// <summary>Initializes a new instance of the <see cref="LogisticAirfareBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticAirfareRepo">The logistic airfare repo.</param>
        public LogisticAirfareBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, ILogisticAirfareRepository logisticAirfareRepo)
            : base(eventBus, uow)
        {
            this.LogisticAirfareRepo = logisticAirfareRepo;
        }

        /// <summary>Gets the logistic airfare by uid.</summary>
        /// <param name="logisticAirfareUid">The logistic airfare uid.</param>
        /// <returns></returns>
        public async Task<LogisticAirfare> GetLogisticAirfareByUid(Guid logisticAirfareUid)
        {
            var logisticAirfare = await this.LogisticAirfareRepo.GetAsync(logisticAirfareUid);
            if (logisticAirfare == null || logisticAirfare.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Airfare, Labels.FoundM), new string[] { "ToastrError" }));
            }

            return logisticAirfare;
        }
    }
}