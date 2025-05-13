// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="NegotiationConfigBaseCommandHandler.cs" company="Softo">
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
    /// <summary>NegotiationConfigBaseCommandHandler</summary>
    public class NegotiationConfigBaseCommandHandler : BaseCommandHandler
    {
        protected readonly INegotiationConfigRepository NegotiationConfigRepo;

        /// <summary>Initializes a new instance of the <see cref="NegotiationConfigBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="negotiationConfigRepository">The negotiation configuration repository.</param>
        public NegotiationConfigBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, INegotiationConfigRepository negotiationConfigRepository)
            : base(eventBus, uow)
        {
            this.NegotiationConfigRepo = negotiationConfigRepository;
        }

        /// <summary>Gets the negotiation configuration by uid.</summary>
        /// <param name="negotiationConfigUid">The negotiation configuration uid.</param>
        /// <returns></returns>
        public async Task<NegotiationConfig> GetNegotiationConfigByUid(Guid negotiationConfigUid)
        {
            var negotiationConfig = await this.NegotiationConfigRepo.GetAsync(negotiationConfigUid);
            if (negotiationConfig == null || negotiationConfig.IsDeleted) // Do not check IsDeleted because the Collaborator/User can be restored
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Parameter, Labels.FoundM), new string[] { "ToastrError" }));
            }

            return negotiationConfig;
        }
    }
}