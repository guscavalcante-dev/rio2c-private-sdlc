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
    /// <summary>PillarBaseCommandHandler</summary>
    public class LogisticAccommodationBaseCommandHandler : BaseCommandHandler
    {
        protected readonly ILogisticAccommodationRepository LogisticAccommodationRepo;

        /// <summary>Initializes a new instance of the <see cref="LogisticAccommodationBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticAccommodationRepository">The logistic accommodation repository.</param>
        public LogisticAccommodationBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, ILogisticAccommodationRepository logisticAccommodationRepository)
            : base(eventBus, uow)
        {
            this.LogisticAccommodationRepo = logisticAccommodationRepository;
        }

        /// <summary>Gets the logistic accommodation by uid.</summary>
        /// <param name="logisticAccommodationUid">The logistic accommodation uid.</param>
        /// <returns></returns>
        public async Task<LogisticAccommodation> GetLogisticAccommodationByUid(Guid logisticAccommodationUid)
        {
            var logisticAccommodation = await this.LogisticAccommodationRepo.GetAsync(logisticAccommodationUid);
            if (logisticAccommodation == null || logisticAccommodation.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Accommodation, Labels.FoundF), new string[] { "ToastrError" }));
            }

            return logisticAccommodation;
        }
    }
}