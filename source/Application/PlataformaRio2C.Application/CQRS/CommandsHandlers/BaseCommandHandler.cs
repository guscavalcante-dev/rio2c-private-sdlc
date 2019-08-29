// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-29-2019
// ***********************************************************************
// <copyright file="BaseCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>BaseCommandHandler</summary>
    public class BaseCommandHandler
    {
        protected AppValidationResult AppValidationResult;
        protected readonly ValidationResult ValidationResult;
        protected readonly IMediator EventBus;
        protected readonly IUnitOfWork Uow;

        /// <summary>Initializes a new instance of the <see cref="BaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        public BaseCommandHandler(IMediator eventBus, IUnitOfWork uow)
        {
            this.AppValidationResult = new AppValidationResult();
            this.ValidationResult = new ValidationResult();
            this.EventBus = eventBus;
            this.Uow = uow;
        }
    }
}