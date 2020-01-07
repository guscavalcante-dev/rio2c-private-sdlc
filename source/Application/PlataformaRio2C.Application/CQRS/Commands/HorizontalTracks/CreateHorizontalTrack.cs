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
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateRoom</summary>
    public class CreateHorizontalTrack : BaseCommand
    {
        public List<HorizontalTrackNameBaseCommand> Names { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateHorizontalTrack"/> class.</summary>
        /// <param name="horizontalTrackDto">The horizontal track dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public CreateHorizontalTrack(
            HorizontalTrackDto horizontalTrackDto,
            List<LanguageDto> languagesDtos)
        {
            this.UpdateNames(horizontalTrackDto, languagesDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateHorizontalTrack"/> class.</summary>
        public CreateHorizontalTrack()
        {
        }

        #region Private Methods

        /// <summary>Updates the names.</summary>
        /// <param name="horizontalTrackDto">The horizontal track dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateNames(HorizontalTrackDto horizontalTrackDto, List<LanguageDto> languagesDtos)
        {
            this.Names = new List<HorizontalTrackNameBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {       
                this.Names.Add(new HorizontalTrackNameBaseCommand(horizontalTrackDto, languageDto));
            }
        }

        #endregion
    }
}