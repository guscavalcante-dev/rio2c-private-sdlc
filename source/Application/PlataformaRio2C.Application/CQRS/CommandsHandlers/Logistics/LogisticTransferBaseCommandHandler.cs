// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-13-2020
// ***********************************************************************
// <copyright file="LogisticTransferBaseCommandHandler.cs" company="Softo">
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
    /// <summary>LogisticTransferBaseCommandHandler</summary>
    public class LogisticTransferBaseCommandHandler : BaseCommandHandler
    {
        protected readonly ILogisticTransferRepository LogisticTransferRepo;

        /// <summary>Initializes a new instance of the <see cref="LogisticTransferBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticTransferRepository">The logistic transfer repository.</param>
        public LogisticTransferBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, ILogisticTransferRepository logisticTransferRepository)
            : base(eventBus, uow)
        {
            this.LogisticTransferRepo = logisticTransferRepository;
        }

        /// <summary>Gets the logistic transfer by uid.</summary>
        /// <param name="logisticTransferUid">The logistic transfer uid.</param>
        /// <returns></returns>
        public async Task<LogisticTransfer> GetLogisticTransferByUid(Guid logisticTransferUid)
        {
            var logisticTransfer = await this.LogisticTransferRepo.GetAsync(logisticTransferUid);
            if (logisticTransfer == null || logisticTransfer.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Transfer, Labels.FoundM), new string[] { "ToastrError" }));
            }

            return logisticTransfer;
        }
    }
}