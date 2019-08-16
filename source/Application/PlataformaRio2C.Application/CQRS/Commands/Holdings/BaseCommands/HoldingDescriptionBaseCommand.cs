// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-16-2019
// ***********************************************************************
// <copyright file="HoldingDescriptionBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Mvc;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>HoldingDescriptionBaseCommand</summary>
    public class HoldingDescriptionBaseCommand
    {
        [AllowHtml]
        public string Value { get; set; }
        public string LanguageCode { get; set; }

        /// <summary>Initializes a new instance of the <see cref="HoldingDescriptionBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public HoldingDescriptionBaseCommand(HoldingDescription entity)
        {
            this.Value = entity.Value;
            this.LanguageCode = entity.LanguageCode;
        }
        
        /// <summary>Initializes a new instance of the <see cref="HoldingDescriptionBaseCommand"/> class.</summary>
        public HoldingDescriptionBaseCommand()
        {
        }
    }
}