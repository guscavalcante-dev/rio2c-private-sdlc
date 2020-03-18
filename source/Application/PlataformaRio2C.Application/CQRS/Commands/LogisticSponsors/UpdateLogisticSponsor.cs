// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="UpdateLogisticSponsor.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateLogisticSponsor</summary>
    public class UpdateLogisticSponsor : CreateLogisticSponsor
    {
        public Guid LogisticSponsorUid { get; set; }        
        public bool IsAddingToCurrentEdition { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticSponsor"/> class.</summary>
        /// <param name="dto">The dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        public UpdateLogisticSponsor(LogisticSponsorJsonDto dto, List<LanguageDto> languagesDtos, bool? isAddingToCurrentEdition)
        {
            this.LogisticSponsorUid = dto.Uid;
            this.IsAirfareTicketRequired = dto.IsAirfareTicketRequired;
            this.IsAddingToCurrentEdition = isAddingToCurrentEdition ?? false;
            this.UpdateNames(dto, languagesDtos);            
        }
        
        /// <summary>Initializes a new instance of the <see cref="CreateLogisticSponsor"/> class.</summary>
        public UpdateLogisticSponsor()
        {
        }
        
        #region Private Methods

        /// <summary>Updates the titles.</summary>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateNames(LogisticSponsorJsonDto dto, List<LanguageDto> languagesDtos)
        {
            this.Names = new List<LogisticSponsorsNameBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                this.Names.Add(new LogisticSponsorsNameBaseCommand(dto, languageDto));
            }
        }

        #endregion
    }
}