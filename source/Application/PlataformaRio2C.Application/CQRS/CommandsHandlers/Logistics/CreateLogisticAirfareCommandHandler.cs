// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-16-2020
// ***********************************************************************
// <copyright file="CreateLogisticAirfareCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateLogisticAirfareCommandHandler</summary>
    public class CreateLogisticAirfareCommandHandler : LogisticAirfareBaseCommandHandler, IRequestHandler<CreateLogisticAirfare, AppValidationResult>
    {
        private readonly ILogisticRepository logisticRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticAirfareCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticRepository">The logistic repository.</param>
        /// <param name="logisticsAirfareRepository">The logistics airfare repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public CreateLogisticAirfareCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticRepository logisticRepository,
            ILogisticAirfareRepository logisticsAirfareRepository,
            IFileRepository fileRepository) 
            : base(eventBus, uow, logisticsAirfareRepository)
        {
            this.logisticRepo = logisticRepository;
            this.fileRepo = fileRepository;
        }

        /// <summary>Handles the specified create track.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateLogisticAirfare cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var logisticAirfare = new LogisticAirfare(
                await logisticRepo.GetAsync(cmd.LogisticsUid),
                cmd.IsNational,
                cmd.IsArrival,
                cmd.From,
                cmd.To,
                cmd.TicketNumber,
                cmd.AdditionalInfo,
                cmd.Departure,
                cmd.Arrival,
                cmd.Ticket != null,
                cmd.UserId);
            
            if (!logisticAirfare.IsValid())
            {
                this.AppValidationResult.Add(logisticAirfare.ValidationResult);
                return this.AppValidationResult;
            }

            this.LogisticAirfareRepo.Create(logisticAirfare);

            this.Uow.SaveChanges();
            this.AppValidationResult.Data = logisticAirfare;

            if (cmd.Ticket != null)
            {
                fileRepo.Upload(
                    cmd.Ticket.InputStream,
                    cmd.Ticket.ContentType,
                    logisticAirfare.Uid + ".pdf",
                    FileRepositoryPathType.LogisticAirfareFile);
            }

            return this.AppValidationResult;
        }
    }
}