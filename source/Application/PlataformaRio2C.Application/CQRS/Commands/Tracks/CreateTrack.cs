// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="CreateTrack.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateTrack</summary>
    public class CreateTrack : BaseCommand
    {
        public List<TrackNameBaseCommand> Names { get; set; }

        [Display(Name = "Color", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string Color { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateTrack"/> class.</summary>
        /// <param name="trackDto">The track dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public CreateTrack(
            TrackDto trackDto,
            List<LanguageDto> languagesDtos)
        {
            this.Color = trackDto?.Track?.Color;
            this.UpdateNames(trackDto, languagesDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateTrack"/> class.</summary>
        public CreateTrack()
        {
        }

        #region Private Methods

        /// <summary>Updates the names.</summary>
        /// <param name="trackDto">The track dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateNames(TrackDto trackDto, List<LanguageDto> languagesDtos)
        {
            this.Names = new List<TrackNameBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {       
                this.Names.Add(new TrackNameBaseCommand(trackDto, languageDto));
            }
        }

        #endregion
    }
}