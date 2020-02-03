// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-20-2020
// ***********************************************************************
// <copyright file="LogisticRequestBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>
    /// Class LogisticRequestBaseDto.
    /// </summary>
    public class LogisticRequestDetailsDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public string AirfareSponsor { get; set; }
        public string AccommodationSponsor { get; set; }
        public string TransferSponsor { get; set; }

        public bool IsSponsoredByEvent { get; set; }
        public bool HasLogistics { get; set; }
        public bool HasSponsors { get; set; }

        /// <summary>
        /// Gets the airfare sponsor by language.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns>System.String.</returns>
        public string GetAirfareSponsorByLanguage(string languageCode)
        {
            return this.AirfareSponsor.GetSeparatorTranslation(languageCode, '|');
        }

        /// <summary>
        /// Gets the accommodation sponsor by language.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns>System.String.</returns>
        public string GetAccommodationSponsorByLanguage(string languageCode)
        {
            return this.AccommodationSponsor.GetSeparatorTranslation(languageCode, '|');
        }

        /// <summary>
        /// Gets the transfer sponsor by language.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns>System.String.</returns>
        public string GetTransferSponsorByLanguage(string languageCode)
        {
            return this.TransferSponsor.GetSeparatorTranslation(languageCode, '|');
        }
    }
}