// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="UpdateLogisticAirfareCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateLogisticAirfareCommandHandler</summary>
    public class UpdateLogisticAirfareCommandHandler : LogisticAirfareBaseCommandHandler, IRequestHandler<UpdateLogisticAirfare, AppValidationResult>
    {
        private readonly ILogisticRepository logisticRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticAirfareCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticRepository">The logistic repository.</param>
        /// <param name="logisticsAirfareRepository">The logistics airfare repository.</param>
        public UpdateLogisticAirfareCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticRepository logisticRepository,
            ILogisticAirfareRepository logisticsAirfareRepository) 
            : base(eventBus, uow, logisticsAirfareRepository)
        {
            this.logisticRepo = logisticRepository;
        }

        /// <summary>Handles the specified create track.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateLogisticAirfare cmd, CancellationToken cancellationToken)
        {

            this.Uow.BeginTransaction();

            var airfare = await this.GetByUid(cmd.Uid);

            #region Initial validations

            //// Check if exists an user with the same email
            //var user = await this.repository.GetAsync(u => u.Email == cmd.Email.Trim());
            //if (user != null && (collaborator?.User == null || user.Uid != collaborator?.User?.Uid))
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.Executive.ToLowerInvariant(), $"{Labels.TheM} {Labels.Email}", cmd.Email), new string[] { "Email" }));
            //}

            //if (!this.ValidationResult.IsValid)
            //{
            //    this.AppValidationResult.Add(this.ValidationResult);
            //    return this.AppValidationResult;
            //}

            #endregion

            airfare.Update(
                cmd.IsNational,
                cmd.IsArrival,
                cmd.From,
                cmd.To,
                cmd.TicketNumber,
                cmd.AdditionalInfo,
                cmd.Departure,
                cmd.Arrival,
                cmd.UserId);

            if (!airfare.IsValid())
            {
                this.AppValidationResult.Add(airfare.ValidationResult);
                return this.AppValidationResult;
            }

            this.repository.Update(airfare);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = airfare;

            return this.AppValidationResult;
        }
    }
}