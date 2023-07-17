// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-17-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-17-2023
// ***********************************************************************
// <copyright file="DeleteInnovationOrganizationTrackOptionGroupCommandHandler.cs" company="Softo">
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
    /// <summary>DeleteInnovationOrganizationTrackOptionGroupCommandHandler</summary>
    public class DeleteInnovationOrganizationTrackOptionGroupCommandHandler : InnovationOrganizationTrackOptionGroupBaseCommandHandler, IRequestHandler<DeleteInnovationOrganizationTrackOptionGroup, AppValidationResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteInnovationOrganizationTrackOptionGroupCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="innovationOrganizationTrackOptionGroupRepository">The innovation organization track option group repository.</param>
        public DeleteInnovationOrganizationTrackOptionGroupCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepository)
            : base(eventBus, uow, innovationOrganizationTrackOptionGroupRepository)
        {
        }

        /// <summary>Handles the specified delete track.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteInnovationOrganizationTrackOptionGroup cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var innovationOrganizationTrackOptionGroup = await this.GetTrackOptionGroupByUid(cmd.InnovationOrganizationTrackOptionGroupUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            innovationOrganizationTrackOptionGroup.Delete(cmd.UserId);
            if (!innovationOrganizationTrackOptionGroup.IsValid())
            {
                this.AppValidationResult.Add(innovationOrganizationTrackOptionGroup.ValidationResult);
                return this.AppValidationResult;
            }

            this.InnovationOrganizationTrackOptionGroupRepo.Update(innovationOrganizationTrackOptionGroup);
            this.Uow.SaveChanges();

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}