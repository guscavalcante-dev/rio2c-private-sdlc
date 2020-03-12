// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-12-2020
// ***********************************************************************
// <copyright file="CreateConference.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateLogisticSponsors</summary>
    public class UpdateLogisticSponsors : CreateLogisticSponsors
    {
        public Guid LogisticSponsorUid { get; set; }        
        public bool IsAddingToCurrentEdition { get; set; }
        
        public UpdateLogisticSponsors(AttendeeLogisticSponsorBaseDto dto, List<LanguageDto> languagesDtos, string userInterfaceLanguage, bool? isAddingToCurrentEdition)
        {
            this.LogisticSponsorUid = dto.Uid;
            this.IsAirfareTicketRequired = dto.IsAirfareTicketRequired;
            this.IsAddingToCurrentEdition = isAddingToCurrentEdition ?? false;
            this.UpdateNames(dto, languagesDtos);            
        }
        
        /// <summary>Initializes a new instance of the <see cref="CreateLogisticSponsors"/> class.</summary>
        public UpdateLogisticSponsors()
        {
        }
        
        #region Private Methods

        /// <summary>Updates the titles.</summary>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateNames(AttendeeLogisticSponsorBaseDto dto, List<LanguageDto> languagesDtos)
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