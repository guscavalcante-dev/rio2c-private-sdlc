// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="LogisticsBaseCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>LogisticsBaseCommandHandler</summary>
    public class LogisticsBaseCommandHandler : BaseCommandHandler
    {
        protected readonly ILogisticRepository repository;

        /// <summary>Initializes a new instance of the <see cref="LogisticsBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticsRepository">The logistics repository.</param>
        public LogisticsBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, ILogisticRepository logisticsRepository)
            : base(eventBus, uow)
        {
            this.repository = logisticsRepository;
        }

        /// <summary>Gets the logistic by uid.</summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns></returns>
        public async Task<Logistic> GetLogisticByUid(Guid logisticsUid)
        {
            var logistic = await this.repository.GetAsync(logisticsUid);
            if (logistic == null) // Do not check IsDeleted because the Collaborator/User can be restored
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Executive, Labels.FoundM), new string[] { "FirstName" }));
            }

            return logistic;
        }
    }
}