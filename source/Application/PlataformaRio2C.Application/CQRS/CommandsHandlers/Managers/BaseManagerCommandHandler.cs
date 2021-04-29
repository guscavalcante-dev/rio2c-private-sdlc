// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-24-2021
// ***********************************************************************
// <copyright file="BaseManagerCommandHandler.cs" company="Softo">
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
    /// <summary>BaseManagerCommandHandler</summary>
    public class BaseManagerCommandHandler : BaseCommandHandler
    {
        protected readonly ICollaboratorRepository CollaboratorRepo;

        /// <summary>Initializes a new instance of the <see cref="BaseManagerCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        public BaseManagerCommandHandler(IMediator eventBus, IUnitOfWork uow, ICollaboratorRepository collaboratorRepository)
            : base(eventBus, uow)
        {
            this.CollaboratorRepo = collaboratorRepository;
        }

        /// <summary>Gets the collaborator by uid.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        public async Task<Collaborator> GetCollaboratorByUid(Guid collaboratorUid)
        {
            var collaborator = await this.CollaboratorRepo.GetAsync(collaboratorUid);
            if (collaborator == null) // Do not check IsDeleted because the Collaborator/User can be restored
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Executive, Labels.FoundM), new string[] { "ToastrError" }));
            }

            return collaborator;
        }

       
    }
}