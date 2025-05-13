// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="CreateRoom.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateRoom</summary>
    public class CreatePresentationFormat : BaseCommand
    {
        public List<PresentationFormatNameBaseCommand> Names { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreatePresentationFormat"/> class.</summary>
        /// <param name="presentationFormatDto">The presentation format dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public CreatePresentationFormat(
            PresentationFormatDto presentationFormatDto,
            List<LanguageDto> languagesDtos)
        {
            this.UpdateNames(presentationFormatDto, languagesDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="CreatePresentationFormat"/> class.</summary>
        public CreatePresentationFormat()
        {
        }

        #region Private Methods

        /// <summary>Updates the names.</summary>
        /// <param name="presentationFormatDto">The presentation format dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateNames(PresentationFormatDto presentationFormatDto, List<LanguageDto> languagesDtos)
        {
            this.Names = new List<PresentationFormatNameBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                this.Names.Add(new PresentationFormatNameBaseCommand(presentationFormatDto, languageDto));
            }
        }

        #endregion
    }
}