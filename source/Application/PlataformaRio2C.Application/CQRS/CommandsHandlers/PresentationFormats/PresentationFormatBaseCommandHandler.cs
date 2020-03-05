// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="PresentationFormatBaseCommandHandler.cs" company="Softo">
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
    /// <summary>PresentationFormatBaseCommandHandler</summary>
    public class PresentationFormatBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IPresentationFormatRepository PresentationFormatRepo;

        /// <summary>Initializes a new instance of the <see cref="PresentationFormatBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="presentationFormatRepository">The presentation format repository.</param>
        public PresentationFormatBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, IPresentationFormatRepository presentationFormatRepository)
            : base(eventBus, uow)
        {
            this.PresentationFormatRepo = presentationFormatRepository;
        }

        /// <summary>Gets the presentation format by uid.</summary>
        /// <param name="presentationFormatUid">The presentation format uid.</param>
        /// <returns></returns>
        public async Task<PresentationFormat> GetPresentationFormatByUid(Guid presentationFormatUid)
        {
            var presentationFormat = await this.PresentationFormatRepo.GetAsync(presentationFormatUid);
            if (presentationFormat == null || presentationFormat.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Track, Labels.FoundF), new string[] { "ToastrError" }));
            }

            return presentationFormat;
        }
    }
}