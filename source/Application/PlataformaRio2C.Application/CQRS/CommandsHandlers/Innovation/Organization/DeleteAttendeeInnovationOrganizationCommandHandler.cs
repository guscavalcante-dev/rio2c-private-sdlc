// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-27-2021
//
// Last Modified By :  Renan Valentim
// Last Modified On :  07-27-2021
// ***********************************************************************
// <copyright file="DeleteAttendeeInnovationOrganizationCommandHandler.cs" company="Softo">
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
    /// <summary>DeleteAttendeeInnovationOrganizationCommandHandler</summary>
    public class DeleteAttendeeInnovationOrganizationCommandHandler : AttendeeInnovationOrganizationBaseCommandHandler, IRequestHandler<DeleteAttendeeInnovationOrganization, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="DeleteAttendeeInnovationOrganizationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="InnovationOrganizationRepository">The music project repository.</param>
        public DeleteAttendeeInnovationOrganizationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeeInnovationOrganizationRepository attendeeInnovationOrganizationRepository)
            : base(eventBus, uow, attendeeInnovationOrganizationRepository)
        {
        }

        /// <summary>Handles the specified delete music project.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteAttendeeInnovationOrganization cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var attendeeInnovationOrganization = await this.GetAttendeeInnovationOrganizationByUid(cmd.AttendeeInnovationOrganizationUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            attendeeInnovationOrganization.Delete(cmd.UserId);
            if (!attendeeInnovationOrganization.IsValid())
            {
                this.AppValidationResult.Add(attendeeInnovationOrganization.ValidationResult);
                return this.AppValidationResult;
            }

            this.AttendeeInnovationOrganizationRepo.Update(attendeeInnovationOrganization);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}