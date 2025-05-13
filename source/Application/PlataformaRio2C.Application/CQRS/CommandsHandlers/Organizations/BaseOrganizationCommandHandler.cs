// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="BaseOrganizationCommandHandler.cs" company="Softo">
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
    /// <summary>BaseOrganizationCommandHandler</summary>
    public class BaseOrganizationCommandHandler : BaseCommandHandler
    {
        protected readonly IOrganizationRepository OrganizationRepo;

        /// <summary>Initializes a new instance of the <see cref="BaseOrganizationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="organizationRepository">The organization repository.</param>
        public BaseOrganizationCommandHandler(IMediator eventBus, IUnitOfWork uow, IOrganizationRepository organizationRepository)
            : base(eventBus, uow)
        {
            this.OrganizationRepo = organizationRepository;
        }

        /// <summary>Gets the organization by uid.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        public async Task<Organization> GetOrganizationByUid(Guid organizationUid)
        {
            var organization = await this.OrganizationRepo.GetAsync(organizationUid);
            if (organization == null || organization.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Player, Labels.FoundM), new string[] { "ToastrError" }));
                return null;
            }

            return organization;
        }
    }
}