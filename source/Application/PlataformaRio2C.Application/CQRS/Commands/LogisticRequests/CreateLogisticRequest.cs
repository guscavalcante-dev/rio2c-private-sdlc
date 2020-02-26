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
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateLogisticSponsors</summary>
    public class CreateLogisticRequest : BaseCommand
    {
        [Display(Name = "Participant", ResourceType = typeof(Labels))]
        public int AttendeeCollaboratorId { get; set; }
        public int? AirfareAttendeeLogisticSponsorId { get; set; }
        public int? AccommodationAttendeeLogisticSponsorId { get; set; }
        public Guid? AirportTransferAttendeeLogisticSponsorUid { get; set; }

        [Display(Name = "Others", ResourceType = typeof(Labels))]
        public string AirportTransferAttendeeLogisticSponsorOthers { get; set; }

        public bool IsCityTransferRequired { get; set; }
        public bool IsVehicleDisposalRequired { get; set; }

        public List<LogisticSponsorBaseDto> Sponsors { get; set; }
        public bool IsAirfareTicketRequired { get; set; }

        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        public string AdditionalInfo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticSponsors"/> class.</summary>
        public CreateLogisticRequest(List<LogisticSponsorBaseDto> sponsors, List<LanguageDto> languagesDtos, string userInterfaceLanguage)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticSponsors"/> class.</summary>
        public CreateLogisticRequest()
        {
        }
        
        #region Private Methods


        #endregion
    }
}