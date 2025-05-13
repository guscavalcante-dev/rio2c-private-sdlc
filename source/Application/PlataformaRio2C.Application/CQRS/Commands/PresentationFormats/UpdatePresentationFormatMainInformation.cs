// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="UpdatePresentationFormatMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdatePresentationFormatMainInformation</summary>
    public class UpdatePresentationFormatMainInformation : CreatePresentationFormat
    {
        public Guid PresentationFormatUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdatePresentationFormatMainInformation"/> class.</summary>
        /// <param name="presentationFormatDto">The presentation format dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public UpdatePresentationFormatMainInformation(
            PresentationFormatDto presentationFormatDto,
            List<LanguageDto> languagesDtos)
            : base(presentationFormatDto, languagesDtos)
        {
            this.PresentationFormatUid = presentationFormatDto?.PresentationFormat?.Uid ?? Guid.Empty;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdatePresentationFormatMainInformation"/> class.</summary>
        public UpdatePresentationFormatMainInformation()
        {
        }
    }
}