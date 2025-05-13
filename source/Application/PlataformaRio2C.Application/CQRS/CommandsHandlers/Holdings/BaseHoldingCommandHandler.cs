// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="BaseHoldingCommandHandler.cs" company="Softo">
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
    /// <summary>BaseHoldingCommandHandler</summary>
    public class BaseHoldingCommandHandler : BaseCommandHandler
    {
        protected readonly IHoldingRepository HoldingRepo;

        /// <summary>Initializes a new instance of the <see cref="BaseHoldingCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="holdingRepository">The holding repository.</param>
        public BaseHoldingCommandHandler(IMediator eventBus, IUnitOfWork uow, IHoldingRepository holdingRepository)
            : base(eventBus, uow)
        {
            this.HoldingRepo = holdingRepository;
        }

        /// <summary>Gets the holding by uid.</summary>
        /// <param name="holdingUid">The holding uid.</param>
        /// <returns></returns>
        public async Task<Holding> GetHoldingByUid(Guid holdingUid)
        {
            var holding = await this.HoldingRepo.GetAsync(holdingUid);
            if (holding == null || holding.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Holding, Labels.FoundF), new string[] { "ToastrError" }));
                return null;
            }

            return holding;
        }
    }
}