// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="VerticalTrackBaseCommandHandler.cs" company="Softo">
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
    /// <summary>VerticalTrackBaseCommandHandler</summary>
    public class VerticalTrackBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IVerticalTrackRepository VerticalTrackRepo;

        /// <summary>Initializes a new instance of the <see cref="VerticalTrackBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="verticalTrackRepository">The vertical track repository.</param>
        public VerticalTrackBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, IVerticalTrackRepository verticalTrackRepository)
            : base(eventBus, uow)
        {
            this.VerticalTrackRepo = verticalTrackRepository;
        }

        /// <summary>Gets the vertical track by uid.</summary>
        /// <param name="verticalTrackUid">The vertical track uid.</param>
        /// <returns></returns>
        public async Task<VerticalTrack> GetVerticalTrackByUid(Guid verticalTrackUid)
        {
            var verticalTrack = await this.VerticalTrackRepo.GetAsync(verticalTrackUid);
            if (verticalTrack == null || verticalTrack.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Track, Labels.FoundF), new string[] { "Track" }));
            }

            return verticalTrack;
        }
    }
}