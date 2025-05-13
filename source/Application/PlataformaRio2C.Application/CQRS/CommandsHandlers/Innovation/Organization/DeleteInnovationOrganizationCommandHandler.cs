// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-27-2021
//
// Last Modified By :  Renan Valentim
// Last Modified On :  02-23-2022
// ***********************************************************************
// <copyright file="DeleteInnovationOrganizationCommandHandler.cs" company="Softo">
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
    /// <summary>DeleteInnovationOrganizationCommandHandler</summary>
    public class DeleteInnovationOrganizationCommandHandler : InnovationOrganizationBaseCommandHandler, IRequestHandler<DeleteInnovationOrganization, AppValidationResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteInnovationOrganizationCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="innovationOrganizationRepository">The innovation organization repository.</param>
        public DeleteInnovationOrganizationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IInnovationOrganizationRepository innovationOrganizationRepository)
            : base(eventBus, uow, innovationOrganizationRepository)
        {
        }

        /// <summary>Handles the specified delete music project.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteInnovationOrganization cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var innovationOrganization = await this.GetInnovationOrganizationByUid(cmd.InnovationOrganizationUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            innovationOrganization.Delete(cmd.UserId);
            if (!innovationOrganization.IsValid())
            {
                this.AppValidationResult.Add(innovationOrganization.ValidationResult);
                return this.AppValidationResult;
            }

            this.InnovationOrganizationRepo.Update(innovationOrganization);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}