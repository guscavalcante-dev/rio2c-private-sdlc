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
    public class LogisticRequestBaseDto
    {
        public int? Id { get; set; }
        public Guid? Uid { get; set; }
        public Guid CollaboratorUid { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? CreateDate { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }

        public string AccommodationSponsor { get; set; }
        public string AirportTransferSponsor { get; set; }
        public string AirfareSponsor { get; set; }

        public bool IsSponsoredByEvent { get; set; }
        public bool HasLogistics { get; set; }
        public bool HasRequest { get; set; }
        public string CreateUser { get; set; }
        public string AdditionalInfo { get; set; }
        public bool TransferCity { get; set; }
        public bool IsVehicleDisposalRequired { get; set; }
        public List<string> CollaboratorRoles { get; set; }
        public List<Pillar> CollaboratorPillars { get; set; }

        /// <summary>
        /// Gets the airfare sponsor by language.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns>System.String.</returns>
        public string GetAirfareSponsorTranslation(string languageCode)
        {
            return this.AirfareSponsor?.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        /// <summary>
        /// Gets the accommodation sponsor by language.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns>System.String.</returns>
        public string GetAccommodationSponsorTranslation(string languageCode)
        {
            return this.AccommodationSponsor?.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        /// <summary>
        /// Gets the transfer sponsor by language.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns>System.String.</returns>
        public string GetAirportTransferSponsorTranslation(string languageCode)
        {
            return this.AirportTransferSponsor?.GetSeparatorTranslation(languageCode, Language.Separator);
        }
    }
}