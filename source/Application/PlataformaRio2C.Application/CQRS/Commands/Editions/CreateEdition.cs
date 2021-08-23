// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-03-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-26-2021
// ***********************************************************************
// <copyright file="CreateEdition.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateEdition</summary>
    public class CreateEdition : EditionBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEdition"/> class.
        /// </summary>
        /// <param name="editionDto">The edition dto.</param>
        public CreateEdition(EditionDto editionDto) : base(editionDto, true, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEdition" /> class.
        /// </summary>
        public CreateEdition()
        {
        }
    }
}