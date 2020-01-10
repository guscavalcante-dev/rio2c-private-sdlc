// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="ConferenceBaseCommandHandler.cs" company="Softo">
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
    /// <summary>ConferenceBaseCommandHandler</summary>
    public class ConferenceBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IConferenceRepository ConferenceRepo;

        /// <summary>Initializes a new instance of the <see cref="ConferenceBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="conferenceRepository">The conference repository.</param>
        public ConferenceBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, IConferenceRepository conferenceRepository)
            : base(eventBus, uow)
        {
            this.ConferenceRepo = conferenceRepository;
        }

        /// <summary>Gets the conference by uid.</summary>
        /// <param name="conferenceUid">The conference uid.</param>
        /// <returns></returns>
        public async Task<Conference> GetConferenceByUid(Guid conferenceUid)
        {
            var conference = await this.ConferenceRepo.GetAsync(conferenceUid);
            if (conference == null || conference.IsDeleted) // Do not check IsDeleted because the Collaborator/User can be restored
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Conference, Labels.FoundF), new string[] { "Conference" }));
            }

            return conference;
        }
    }
}