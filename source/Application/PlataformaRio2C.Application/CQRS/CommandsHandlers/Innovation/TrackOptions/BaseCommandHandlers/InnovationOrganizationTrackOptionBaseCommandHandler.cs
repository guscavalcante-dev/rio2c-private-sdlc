// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-15-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-15-2023
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOptionBaseCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>
    /// InnovationOrganizationTrackOptionBaseCommandHandler
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Application.CQRS.CommandsHandlers.BaseCommandHandler" />
    public class InnovationOrganizationTrackOptionBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IInnovationOrganizationTrackOptionRepository InnovationOrganizationTrackOptionRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionBaseCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="innovationOrganizationTrackOptionRepository">The innovation organization track option group repository.</param>
        public InnovationOrganizationTrackOptionBaseCommandHandler(
            IMediator eventBus, 
            IUnitOfWork uow, 
            IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepository)
            : base(eventBus, uow)
        {
            this.InnovationOrganizationTrackOptionRepo = innovationOrganizationTrackOptionRepository;
        }

        /// <summary>
        /// Gets the track option by uid.
        /// </summary>
        /// <param name="TrackOptionUid">The track option uid.</param>
        /// <returns></returns>
        public async Task<InnovationOrganizationTrackOption> GetTrackOptionByUid(Guid TrackOptionUid)
        {
            var innovationOrganizationTrackOption = await this.InnovationOrganizationTrackOptionRepo.GetAsync(TrackOptionUid);
            if (innovationOrganizationTrackOption == null || innovationOrganizationTrackOption.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Vertical, Labels.FoundF), new string[] { "ToastrError" }));
            }

            return innovationOrganizationTrackOption;
        }
    }
}