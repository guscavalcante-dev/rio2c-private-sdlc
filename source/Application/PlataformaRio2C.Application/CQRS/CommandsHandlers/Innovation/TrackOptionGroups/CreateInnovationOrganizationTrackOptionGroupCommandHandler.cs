//************************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-13-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-13-2023
// ***********************************************************************
// <copyright file="CreateInnovationOrganizationTrackOptionGroupCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateInnovationOrganizationTrackOptionGroupCommandHandler</summary>
    public class CreateInnovationOrganizationTrackOptionGroupCommandHandler : InnovationOrganizationTrackOptionGroupBaseCommandHandler, IRequestHandler<CreateInnovationOrganizationTrackOptionGroup, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationOrganizationTrackOptionGroupCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="innovationOrganizationTrackOptionGroupRepository">The innovation organization track option group repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        public CreateInnovationOrganizationTrackOptionGroupCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepository,
            IEditionRepository editionRepository)
            : base(eventBus, uow, innovationOrganizationTrackOptionGroupRepository)
        {
            this.editionRepo = editionRepository;
        }

        /// <summary>Handles the specified create track.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateInnovationOrganizationTrackOptionGroup cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var innovationOrganizationTrackOptionGroup = new InnovationOrganizationTrackOptionGroup(
                cmd.Name,
                cmd.UserId);
            if (!innovationOrganizationTrackOptionGroup.IsValid())
            {
                this.AppValidationResult.Add(innovationOrganizationTrackOptionGroup.ValidationResult);
                return this.AppValidationResult;
            }

            this.InnovationOrganizationTrackOptionGroupRepo.Create(innovationOrganizationTrackOptionGroup);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = innovationOrganizationTrackOptionGroup;

            return this.AppValidationResult;
        }
    }
}