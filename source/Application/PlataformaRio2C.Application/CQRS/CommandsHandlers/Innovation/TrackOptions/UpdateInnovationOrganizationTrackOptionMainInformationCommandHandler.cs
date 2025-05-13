//************************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-25-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-25-2023
// ***********************************************************************
// <copyright file="UpdateInnovationOrganizationTrackOptionMainInformationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    public class UpdateInnovationOrganizationTrackOptionMainInformationCommandHandler : InnovationOrganizationTrackOptionBaseCommandHandler, IRequestHandler<UpdateInnovationOrganizationTrackOptionMainInformation, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInnovationOrganizationTrackOptionMainInformationCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="innovationOrganizationTrackOptionRepository">The innovation organization track option repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="innovationOrganizationTrackOptionGroupRepository">The innovation organization track option group repository.</param>
        public UpdateInnovationOrganizationTrackOptionMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepository,
            IEditionRepository editionRepository,
            IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepository)
            : base(eventBus, uow, innovationOrganizationTrackOptionRepository)
        {
            this.editionRepo = editionRepository;
            this.innovationOrganizationTrackOptionGroupRepo = innovationOrganizationTrackOptionGroupRepository;
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateInnovationOrganizationTrackOptionMainInformation cmd, CancellationToken cancellationToken)
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

            innovationOrganizationTrackOption.UpdateMainInformation(
                cmd.Name,
                await this.innovationOrganizationTrackOptionGroupRepo.GetAsync(cmd.InnovationOrganizationOptionGroupUid ?? Guid.Empty),
                cmd.UserId);

            if (!innovationOrganizationTrackOption.IsValid())
            {
                this.AppValidationResult.Add(innovationOrganizationTrackOption.ValidationResult);
                return this.AppValidationResult;
            }

            this.InnovationOrganizationTrackOptionRepo.Update(innovationOrganizationTrackOption);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = innovationOrganizationTrackOption;

            return this.AppValidationResult;
        }
    }
}