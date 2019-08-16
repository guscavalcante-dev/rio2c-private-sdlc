// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-16-2019
// ***********************************************************************
// <copyright file="UpdateHolding.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateHolding</summary>
    public class UpdateHolding : HoldingBaseCommand
    {
        public Guid Uid;

        /// <summary>Initializes a new instance of the <see cref="UpdateHolding"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public UpdateHolding(Domain.Entities.Holding entity, List<LanguageDto> languagesDtos)
            : base(languagesDtos)
        {
            this.Uid = entity.Uid;
            this.Name = entity.Name;
            this.Descriptions = entity.Descriptions?.Select(d => new HoldingDescriptionBaseCommand(d));
            this.CropperImage = new CropperImageBaseCommand(entity.IsImageUploaded, entity.Uid);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateHolding"/> class.</summary>
        public UpdateHolding()
        {
        }
    }
}
