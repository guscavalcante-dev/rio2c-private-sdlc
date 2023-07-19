//************************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-19-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-19-2023
// ***********************************************************************
// <copyright file="UpdateInnovationOrganizationTrackOptionGroupMainInformationCommandHandler.cs" company="Softo">
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
    public class UpdateInnovationOrganizationTrackOptionGroupMainInformationCommandHandler : InnovationOrganizationTrackOptionGroupBaseCommandHandler, IRequestHandler<UpdateInnovationOrganizationTrackOptionGroupMainInformation, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInnovationOrganizationTrackOptionGroupMainInformationCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="innovationOrganizationTrackOptionGroupRepository">The innovation organization track option group repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        public UpdateInnovationOrganizationTrackOptionGroupMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepository,
            IEditionRepository editionRepository)
            : base(eventBus, uow, innovationOrganizationTrackOptionGroupRepository)
        {
            this.editionRepo = editionRepository;
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateInnovationOrganizationTrackOptionGroupMainInformation cmd, CancellationToken cancellationToken)
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

            innovationOrganizationTrackOptionGroup.UpdateMainInformation(cmd.Name, cmd.UserId);

            if (!innovationOrganizationTrackOptionGroup.IsValid())
            {
                this.AppValidationResult.Add(innovationOrganizationTrackOptionGroup.ValidationResult);
                return this.AppValidationResult;
            }

            this.InnovationOrganizationTrackOptionGroupRepo.Update(innovationOrganizationTrackOptionGroup);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = innovationOrganizationTrackOptionGroup;

            return this.AppValidationResult;
        }
    }
}