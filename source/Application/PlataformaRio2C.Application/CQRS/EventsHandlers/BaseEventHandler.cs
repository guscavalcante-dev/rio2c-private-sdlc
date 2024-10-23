// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-22-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-22-2024
// ***********************************************************************
// <copyright file="BaseEventHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;

namespace PlataformaRio2C.Application.CQRS.EventsHandlers
{
    public abstract class BaseEventHandler
    {
        protected readonly IMediator CommandBus;

        public BaseEventHandler(IMediator commandBus)
        {
            this.CommandBus = commandBus;
        }
    }
}
