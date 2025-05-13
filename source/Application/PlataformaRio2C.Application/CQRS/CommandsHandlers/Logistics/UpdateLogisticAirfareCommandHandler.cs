// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-16-2020
// ***********************************************************************
// <copyright file="UpdateLogisticAirfareCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateLogisticAirfareCommandHandler</summary>
    public class UpdateLogisticAirfareCommandHandler : LogisticAirfareBaseCommandHandler, IRequestHandler<UpdateLogisticAirfare, AppValidationResult>
    {
        private readonly IFileRepository fileRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticAirfareCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticsAirfareRepository">The logistics airfare repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public UpdateLogisticAirfareCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticAirfareRepository logisticsAirfareRepository,
            IFileRepository fileRepository)
            : base(eventBus, uow, logisticsAirfareRepository)
        {
            this.fileRepo = fileRepository;
        }

        /// <summary>Handles the specified create track.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateLogisticAirfare cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var logisticAirfare = await this.GetLogisticAirfareByUid(cmd.LogisticAirfareUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var beforeTicketUploadDate = logisticAirfare.TicketUploadDate;

            logisticAirfare.Update(
                cmd.IsNational,
                cmd.IsArrival,
                cmd.From,
                cmd.To,
                cmd.TicketNumber,
                cmd.AdditionalInfo,
                cmd.Departure,
                cmd.Arrival,
                cmd.Ticket != null,
                cmd.IsTicketFileDeleted,
                cmd.UserId);

            if (!logisticAirfare.IsValid())
            {
                this.AppValidationResult.Add(logisticAirfare.ValidationResult);
                return this.AppValidationResult;
            }

            this.LogisticAirfareRepo.Update(logisticAirfare);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = logisticAirfare;

            // Update ticket file
            if (cmd.Ticket != null)
            {
                this.fileRepo.Upload(
                    cmd.Ticket.InputStream,
                    cmd.Ticket.ContentType,
                    logisticAirfare.Uid + ".pdf",
                    FileRepositoryPathType.LogisticAirfareFile);
            }
            // Delete ticket file
            else if (cmd.IsTicketFileDeleted == true && beforeTicketUploadDate.HasValue)
            {
                this.fileRepo.DeleteFiles(logisticAirfare.Uid + ".pdf", FileRepositoryPathType.LogisticAirfareFile);
            }

            return this.AppValidationResult;
        }
    }
}