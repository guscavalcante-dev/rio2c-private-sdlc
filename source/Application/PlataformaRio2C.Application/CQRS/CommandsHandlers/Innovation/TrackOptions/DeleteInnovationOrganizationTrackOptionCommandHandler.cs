// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-26-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="DeleteInnovationOrganizationTrackOptionCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>DeleteInnovationOrganizationTrackOptionCommandHandler</summary>
    public class DeleteInnovationOrganizationTrackOptionCommandHandler : InnovationOrganizationTrackOptionBaseCommandHandler, IRequestHandler<DeleteInnovationOrganizationTrackOption, AppValidationResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteInnovationOrganizationTrackOptionCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="innovationOrganizationTrackOptionRepository">The innovation organization track option repository.</param>
        public DeleteInnovationOrganizationTrackOptionCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepository)
            : base(eventBus, uow, innovationOrganizationTrackOptionRepository)
        {
        }

        /// <summary>Handles the specified delete track.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteInnovationOrganizationTrackOption cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var innovationOrganizationTrackOption = await this.GetTrackOptionByUid(cmd.InnovationOrganizationTrackOptionUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            innovationOrganizationTrackOption.Delete(cmd.UserId);
            if (!innovationOrganizationTrackOption.IsValid())
            {
                this.AppValidationResult.Add(innovationOrganizationTrackOption.ValidationResult);
                return this.AppValidationResult;
            }

            this.InnovationOrganizationTrackOptionRepo.Update(innovationOrganizationTrackOption);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}