// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 02-12-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-12-2022
// ***********************************************************************
// <copyright file="DeleteCartoonProjectCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>DeleteCartoonProjectCommandHandler</summary>
    public class DeleteCartoonProjectCommandHandler : CartoonProjectBaseCommandHandler, IRequestHandler<DeleteCartoonProject, AppValidationResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCartoonProjectCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="cartoonProjectRepository">The cartoon project repository.</param>
        public DeleteCartoonProjectCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICartoonProjectRepository cartoonProjectRepository)
            : base(eventBus, uow, cartoonProjectRepository)
        {
        }

        /// <summary>Handles the specified delete music project.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteCartoonProject cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var cartoonProject = await this.GetCartoonProjectByUid(cmd.CartoonProjectUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            cartoonProject.Delete(cmd.UserId);
            if (!cartoonProject.IsValid())
            {
                this.AppValidationResult.Add(cartoonProject.ValidationResult);
                return this.AppValidationResult;
            }

            this.CartoonProjectRepo.Update(cartoonProject);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}