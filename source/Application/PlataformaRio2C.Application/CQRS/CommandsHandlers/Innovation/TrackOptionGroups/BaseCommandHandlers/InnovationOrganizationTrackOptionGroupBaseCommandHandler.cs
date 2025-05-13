// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-13-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-13-2023
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOptionGroupBaseCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>
    /// InnovationOrganizationTrackOptionGroupBaseCommandHandler
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Application.CQRS.CommandsHandlers.BaseCommandHandler" />
    public class InnovationOrganizationTrackOptionGroupBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IInnovationOrganizationTrackOptionGroupRepository InnovationOrganizationTrackOptionGroupRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionGroupBaseCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="innovationOrganizationTrackOptionGroupRepository">The innovation organization track option group repository.</param>
        public InnovationOrganizationTrackOptionGroupBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, IInnovationOrganizationTrackOptionGroupRepository innovationOrganizationTrackOptionGroupRepository)
            : base(eventBus, uow)
        {
            this.InnovationOrganizationTrackOptionGroupRepo = innovationOrganizationTrackOptionGroupRepository;
        }

        /// <summary>
        /// Gets the track option group by uid.
        /// </summary>
        /// <param name="trackOptionGroupUid">The track option group uid.</param>
        /// <returns></returns>
        public async Task<InnovationOrganizationTrackOptionGroup> GetTrackOptionGroupByUid(Guid trackOptionGroupUid)
        {
            var innovationOrganizationTrackOptionGroup = await this.InnovationOrganizationTrackOptionGroupRepo.GetAsync(trackOptionGroupUid);
            if (innovationOrganizationTrackOptionGroup == null || innovationOrganizationTrackOptionGroup.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Vertical, Labels.FoundF), new string[] { "ToastrError" }));
            }

            return innovationOrganizationTrackOptionGroup;
        }
    }
}