// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="UpdateHolding.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateHolding</summary>
    public class UpdateHolding : HoldingBaseCommand
    {
        public Guid HoldingUid { get; set; }
        public UserBaseDto UpdaterBaseDto { get; set; }
        public DateTimeOffset UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateHolding"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        /// <exception cref="DomainException"></exception>
        public UpdateHolding(HoldingDto entity, List<LanguageDto> languagesDtos, bool isImageRequired)
        {
            if (entity == null)
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Holding, Labels.FoundF));
            }

            this.HoldingUid = entity.Uid;
            this.UpdaterBaseDto = entity.UpdaterDto;
            this.UpdateDate = entity.UpdateDate;
            this.UpdateBaseProperties(entity, languagesDtos, isImageRequired);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateHolding"/> class.</summary>
        public UpdateHolding()
        {
        }
    }
}
