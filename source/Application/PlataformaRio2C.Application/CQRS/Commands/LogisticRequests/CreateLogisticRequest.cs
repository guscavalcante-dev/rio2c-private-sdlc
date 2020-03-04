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
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateLogisticSponsors</summary>
    public class CreateLogisticRequest : BaseCommand
    {
        [Display(Name = "Participant", ResourceType = typeof(Labels))]
        public Guid AttendeeCollaboratorUid { get; set; }

        #region Airfare sponsor

        public bool IsAirfareSponsored { get; set; }

        public Guid? AirfareSponsorUid { get; set; }

        // City
        //[RequiredIfOneWithValueAndOtherEmptyAttribute("IsRequired", "True", "CityName")]
        public Guid? AirfareSponsorOtherUid { get; set; }
        
        //[RequiredIfOneWithValueAndOtherEmptyAttribute("IsRequired", "True", "CityUid")]
        //[StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AirfareSponsorOtherName { get; set; }
        
        #endregion

        public Guid? AccommodationAttendeeLogisticSponsorUid { get; set; }
        public string AccommodationAttendeeLogisticSponsorOthers { get; set; }
        
        public Guid? AirportTransferAttendeeLogisticSponsorUid { get; set; }
        public string AirportTransferAttendeeLogisticSponsorOthers { get; set; }
        
        public bool IsCityTransferRequired { get; set; }
        public bool IsVehicleDisposalRequired { get; set; }

        public List<LogisticSponsorBaseDto> Sponsors { get; set; }
        public bool IsAccommodationSponsored { get; set; }
        public bool IsAirportTransferSponsored { get; set; }

        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        public string AdditionalInfo { get; set; }

        public Guid? AirportTransferAttendeeLogisticSponsorOtherUid { get; set; }
        public Guid? AccommodationAttendeeLogisticSponsorOtherUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticSponsors"/> class.</summary>
        public CreateLogisticRequest(List<LogisticSponsorBaseDto> sponsors, string userInterfaceLanguage)
        {
            this.UpdateSponsors(sponsors, userInterfaceLanguage);
        }

        /// <summary>
        /// Updates the sponsors.
        /// </summary>
        /// <param name="sponsors">The sponsors.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        private void UpdateSponsors(List<LogisticSponsorBaseDto> sponsors, string userInterfaceLanguage)
        {
            sponsors.ForEach(g => g.Name.GetSeparatorTranslation(userInterfaceLanguage, Language.Separator));
            this.Sponsors = sponsors.OrderBy(e => e.IsOtherRequired).ThenBy(e => e.Name).ToList();
        }

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticSponsors"/> class.</summary>
        public CreateLogisticRequest()
        {
        }
        
        #region Private Methods


        #endregion
    }
}