// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="HorizontalTrackBaseCommandHandler.cs" company="Softo">
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
    /// <summary>HorizontalTrackBaseCommandHandler</summary>
    public class HorizontalTrackBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IHorizontalTrackRepository HorizontalTrackRepo;

        /// <summary>Initializes a new instance of the <see cref="HorizontalTrackBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="horizontalTrackRepository">The horizontal track repository.</param>
        public HorizontalTrackBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, IHorizontalTrackRepository horizontalTrackRepository)
            : base(eventBus, uow)
        {
            this.HorizontalTrackRepo = horizontalTrackRepository;
        }

        /// <summary>Gets the horizontal track by uid.</summary>
        /// <param name="horizontalTrackUid">The horizontal track uid.</param>
        /// <returns></returns>
        public async Task<HorizontalTrack> GetHorizontalTrackByUid(Guid horizontalTrackUid)
        {
            var horizontalTrack = await this.HorizontalTrackRepo.GetAsync(horizontalTrackUid);
            if (horizontalTrack == null || horizontalTrack.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Track, Labels.FoundF), new string[] { "Track" }));
            }

            return horizontalTrack;
        }
    }
}