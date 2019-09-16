// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="HoldingBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>HoldingBaseCommand</summary>
    public class HoldingBaseCommand : BaseCommand
    {
        [Display(Name = "Name", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(81, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Name { get; set; }

        public List<HoldingDescriptionBaseCommand> Descriptions { get; set; }

        public CropperImageBaseCommand CropperImage { get; set; }

        /// <summary>Initializes a new instance of the <see cref="HoldingBaseCommand"/> class.</summary>
        public HoldingBaseCommand()
        {
        }

        /// <summary>Updates the base properties.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        protected void UpdateBaseProperties(HoldingDto entity, List<LanguageDto> languagesDtos, bool isImageRequired)
        {
            this.Name = entity?.Name;
            this.UpdateDescriptions(entity, languagesDtos);
            this.UpdateCropperImage(entity, isImageRequired);
        }

        /// <summary>Updates the descriptions.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateDescriptions(HoldingDto entity, List<LanguageDto> languagesDtos)
        {
            this.Descriptions = new List<HoldingDescriptionBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var description = entity?.DescriptionsDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.Descriptions.Add(description != null ? new HoldingDescriptionBaseCommand(description) : 
                                                            new HoldingDescriptionBaseCommand(languageDto));
            }
        }

        /// <summary>Updates the cropper image.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        private void UpdateCropperImage(HoldingDto entity, bool isImageRequired)
        {
            this.CropperImage = new CropperImageBaseCommand(entity?.ImageUploadDate, entity?.Uid, FileRepositoryPathType.HoldingImage, isImageRequired);
        }
    }
}