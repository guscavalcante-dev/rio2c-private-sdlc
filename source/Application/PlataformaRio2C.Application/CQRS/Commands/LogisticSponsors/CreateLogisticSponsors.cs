// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-27-2020
// ***********************************************************************
// <copyright file="CreateConference.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateLogisticSponsors</summary>
    public class CreateLogisticSponsors : BaseCommand
    {
        public bool IsAirfareTicketRequired { get; set; }
        public List<LogisticSponsorsNameBaseCommand> Names { get; set; }



        /// <summary>Initializes a new instance of the <see cref="CreateLogisticSponsors"/> class.</summary>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public CreateLogisticSponsors(List<LanguageDto> languagesDtos, string userInterfaceLanguage) => this.UpdateNames(languagesDtos);

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticSponsors"/> class.</summary>
        public CreateLogisticSponsors()
        {
        }
        
        #region Private Methods

        /// <summary>Updates the titles.</summary>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateNames(List<LanguageDto> languagesDtos)
        {
            this.Names = new List<LogisticSponsorsNameBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                this.Names.Add(new LogisticSponsorsNameBaseCommand(languageDto));
            }
        }

        #endregion
    }
}