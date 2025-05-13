// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-17-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="PlaceBaseCommandHandler.cs" company="Softo">
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
    /// <summary>PlaceBaseCommandHandler</summary>
    public class PlaceBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IPlaceRepository PlaceRepo;

        /// <summary>Initializes a new instance of the <see cref="PlaceBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="placeRepository">The place repository.</param>
        public PlaceBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, IPlaceRepository placeRepository)
            : base(eventBus, uow)
        {
            this.PlaceRepo = placeRepository;
        }

        /// <summary>Gets the place by uid.</summary>
        /// <param name="placeUid">The place uid.</param>
        /// <returns></returns>
        public async Task<Place> GetPlaceByUid(Guid placeUid)
        {
            var place = await this.PlaceRepo.GetAsync(placeUid);
            if (place == null || place.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Place, Labels.FoundM), new string[] { "ToastrError" }));
            }

            return place;
        }
    }
}