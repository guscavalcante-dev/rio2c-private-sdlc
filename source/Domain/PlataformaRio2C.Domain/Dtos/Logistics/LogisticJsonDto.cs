// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-20-2020
// ***********************************************************************
// <copyright file="LogisticJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>LogisticJsonDto</summary>
    public class LogisticJsonDto
    {
        public int? Id { get; set; }
        public Guid? Uid { get; set; }
        public Guid CollaboratorUid { get; set; }
        public Guid AttendeeCollaboratorUid { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? CollaboratorImageUploadDate { get; set; }
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

        /// <summary>Gets the airfare sponsor translation.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetAirfareSponsorTranslation(string languageCode)
        {
            return this.AirfareSponsor?.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        /// <summary>Gets the accommodation sponsor translation.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetAccommodationSponsorTranslation(string languageCode)
        {
            return this.AccommodationSponsor?.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        /// <summary>Gets the airport transfer sponsor translation.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetAirportTransferSponsorTranslation(string languageCode)
        {
            return this.AirportTransferSponsor?.GetSeparatorTranslation(languageCode, Language.Separator);
        }
    }
}