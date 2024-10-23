// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-22-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-22-2024
// ***********************************************************************
// <copyright file="EditionCreated.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.CQRS.Commands;

namespace PlataformaRio2C.Application.CQRS.Events.Editions
{
    public class EditionCreated : BaseEvent
    {
        public CreateEdition CreateEdition { get; private set; }

        public EditionCreated(CreateEdition createEdition)
        {
            this.CreateEdition = createEdition;
        }
    }
}
