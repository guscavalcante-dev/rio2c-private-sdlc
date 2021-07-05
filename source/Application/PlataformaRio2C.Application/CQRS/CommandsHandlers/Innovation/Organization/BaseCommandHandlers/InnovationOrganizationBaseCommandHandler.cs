// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 06-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-28-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationBaseCommandHandler.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>
    /// Class InnovationOrganizationBaseCommandHandler.
    /// Implements the <see cref="PlataformaRio2C.Application.CQRS.CommandsHandlers.BaseCommandHandler" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Application.CQRS.CommandsHandlers.BaseCommandHandler" />
    public class InnovationOrganizationBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IInnovationOrganizationRepository InnovationOrganizationRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationBaseCommandHandler"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="innovationOrganizationRepo">The innovation organization repo.</param>
        public InnovationOrganizationBaseCommandHandler(
            IMediator commandBus, 
            IUnitOfWork uow,
            IInnovationOrganizationRepository innovationOrganizationRepository)
            : base(commandBus, uow)
        {
            this.InnovationOrganizationRepo = innovationOrganizationRepository;
        }
    }
}