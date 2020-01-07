// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="CreateRoom.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateRoom</summary>
    public class CreateVerticalTrack : BaseCommand
    {
        public List<VerticalTrackNameBaseCommand> Names { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateVerticalTrack"/> class.</summary>
        /// <param name="verticalTrackDto">The vertical track dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public CreateVerticalTrack(
            VerticalTrackDto verticalTrackDto,
            List<LanguageDto> languagesDtos)
        {
            this.UpdateNames(verticalTrackDto, languagesDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateVerticalTrack"/> class.</summary>
        public CreateVerticalTrack()
        {
        }

        #region Private Methods

        /// <summary>Updates the names.</summary>
        /// <param name="verticalTrackDto">The vertical track dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateNames(VerticalTrackDto verticalTrackDto, List<LanguageDto> languagesDtos)
        {
            this.Names = new List<VerticalTrackNameBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {       
                this.Names.Add(new VerticalTrackNameBaseCommand(verticalTrackDto, languageDto));
            }
        }

        #endregion
    }
}