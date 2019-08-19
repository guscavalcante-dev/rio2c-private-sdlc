// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
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
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateHolding</summary>
    public class UpdateHolding : HoldingBaseCommand
    {
        public Guid HoldingUid { get; set; }
        public UserBaseDto UpdaterBaseDto { get; set; }
        public DateTime UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateHolding"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public UpdateHolding(HoldingDto entity, List<LanguageDto> languagesDtos)
        {
            if (entity == null)
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Holding, Labels.FoundF));
            }

            this.HoldingUid = entity.Uid;
            this.Name = entity.Name;
            this.UpdaterBaseDto = entity.UpdaterDto;
            this.UpdateDate = entity.UpdateDate;
            this.UpdateDescriptions(entity, languagesDtos);
            this.CropperImage = new CropperImageBaseCommand(entity.ImageUploadDate, entity.Uid, FileRepositoryPathType.HoldingImage);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateHolding"/> class.</summary>
        public UpdateHolding()
        {
        }

        /// <summary>Updates the descriptions.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateDescriptions(HoldingDto entity, List<LanguageDto> languagesDtos)
        {
            this.Descriptions = new List<HoldingDescriptionBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var description = entity.DescriptionsDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                if (description != null)
                {
                    this.Descriptions.Add(new HoldingDescriptionBaseCommand(description));
                }
                else
                {
                    this.Descriptions.Add(new HoldingDescriptionBaseCommand(languageDto));
                }
            }
        }
    }
}
