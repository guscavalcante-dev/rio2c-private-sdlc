//************************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-15-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-15-2023
// ***********************************************************************
// <copyright file="CreateInnovationOrganizationTrackOptionCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateInnovationOrganizationTrackOptionCommandHandler</summary>
    public class CreateInnovationOrganizationTrackOptionCommandHandler : InnovationOrganizationTrackOptionBaseCommandHandler, IRequestHandler<CreateInnovationOrganizationTrackOption, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationOrganizationTrackOptionCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="innovationOrganizationTrackOptionRepository">The innovation organization track option repository.</param>
        /// <param name="innovationOrganizationTrackOptionGroupRepository">The innovation organization track option group repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        public CreateInnovationOrganizationTrackOptionCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepository,
            IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepository,
            IEditionRepository editionRepository)
            : base(eventBus, uow, innovationOrganizationTrackOptionRepository)
        {
            this.editionRepo = editionRepository;
            this.innovationOrganizationTrackOptionGroupRepo = innovationOrganizationTrackOptionGroupRepository;
        }

        /// <summary>Handles the specified create track.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateInnovationOrganizationTrackOption cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var innovationOrganizationTrackOption = new InnovationOrganizationTrackOption(
                cmd.Name,
                await this.innovationOrganizationTrackOptionGroupRepo.GetAsync(cmd.InnovationOrganizationOptionGroupUid ?? Guid.Empty),
                cmd.UserId);

            if (!innovationOrganizationTrackOption.IsValid())
            {
                this.AppValidationResult.Add(innovationOrganizationTrackOption.ValidationResult);
                return this.AppValidationResult;
            }

            this.InnovationOrganizationTrackOptionRepo.Create(innovationOrganizationTrackOption);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = innovationOrganizationTrackOption;

            return this.AppValidationResult;
        }
    }
}