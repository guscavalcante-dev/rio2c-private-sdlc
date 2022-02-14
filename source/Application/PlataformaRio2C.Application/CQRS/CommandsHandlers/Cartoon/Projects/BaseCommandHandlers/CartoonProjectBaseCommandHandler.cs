// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 01-25-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-25-2022
// ***********************************************************************
// <copyright file="CartoonProjectBaseCommandHandler.cs" company="Softo">
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
    /// <summary>CartoonProjectBaseCommandHandler</summary>
    public class CartoonProjectBaseCommandHandler : BaseCommandHandler
    {
        protected readonly ICartoonProjectRepository CartoonProjectRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartoonProjectBaseCommandHandler"/> class.
        /// </summary>
        /// <param name="commandBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="cartoonProjectRepository">The music band repository.</param>
        public CartoonProjectBaseCommandHandler(
            IMediator commandBus, 
            IUnitOfWork uow, 
            ICartoonProjectRepository cartoonProjectRepository)
            : base(commandBus, uow)
        {
            this.CartoonProjectRepo = cartoonProjectRepository;
        }

        /// <summary>
        /// Gets the cartoon project by uid.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<CartoonProject> GetCartoonProjectByUid(Guid projectUid)
        {
            var cartoonProject = await this.CartoonProjectRepo.FindByUidAsync(projectUid);
            if (cartoonProject == null || cartoonProject.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM), new string[] { "ToastrError" }));
                return null;
            }

            return cartoonProject;
        }
    }
}