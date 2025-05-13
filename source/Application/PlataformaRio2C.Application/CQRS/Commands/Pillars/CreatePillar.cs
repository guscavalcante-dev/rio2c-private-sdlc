// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="CreatePillar.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreatePillar</summary>
    public class CreatePillar : BaseCommand
    {
        public List<PillarNameBaseCommand> Names { get; set; }

        [Display(Name = "Color", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string Color { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreatePillar"/> class.</summary>
        /// <param name="trackDto">The track dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public CreatePillar(
            PillarDto trackDto,
            List<LanguageDto> languagesDtos)
        {
            this.Color = trackDto?.Pillar?.Color;
            this.UpdateNames(trackDto, languagesDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="CreatePillar"/> class.</summary>
        public CreatePillar()
        {
        }

        #region Private Methods

        /// <summary>Updates the names.</summary>
        /// <param name="trackDto">The track dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateNames(PillarDto trackDto, List<LanguageDto> languagesDtos)
        {
            this.Names = new List<PillarNameBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                this.Names.Add(new PillarNameBaseCommand(trackDto, languageDto));
            }
        }

        #endregion
    }
}