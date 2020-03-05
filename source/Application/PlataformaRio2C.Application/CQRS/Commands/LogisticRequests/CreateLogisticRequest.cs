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
using Foolproof;
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

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid AttendeeCollaboratorUid { get; set; }

        #region Airfare sponsor

        public bool IsAirfareSponsored { get; set; }

        public bool? AirfareRequired { get; set; }
        
        [RequiredIf("IsAirfareSponsored", true, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? AirfareSponsorUid { get; set; }
        
        public Guid? AirfareSponsorOtherUid { get; set; }
        
        [RequiredIfOneWithValueAndOtherEmptyAttribute("AirfareRequired", "True", "AirfareSponsorOtherUid")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AirfareSponsorOtherName { get; set; }

        #endregion

        #region Accommodation Sponsor
        public bool IsAccommodationSponsored { get; set; }
        
        [RequiredIf("IsAccommodationSponsored", true, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? AccommodationSponsorUid { get; set; }
        public bool? AccommodationRequired { get; set; }
        
        [RequiredIfOneWithValueAndOtherEmptyAttribute("AccommodationRequired", "True", "AccommodationSponsorOtherName")]
        public Guid? AccommodationSponsorOtherUid { get; set; }

        [RequiredIfOneWithValueAndOtherEmptyAttribute("AccommodationRequired", "True", "AccommodationSponsorOtherUid")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AccommodationSponsorOtherName { get; set; }

        #endregion

        #region AirportTransfer Sponsor
        public bool IsAirportTransferSponsored { get; set; }

        [RequiredIf("IsAirportTransferSponsored", true, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? AirportTransferSponsorUid { get; set; }
        public bool? AirportTransferRequired { get; set; }

        [RequiredIfOneWithValueAndOtherEmptyAttribute("AirportTransferRequired", "True", "AirportTransferSponsorOtherName")]
        public Guid? AirportTransferSponsorOtherUid { get; set; }
        
        [RequiredIfOneWithValueAndOtherEmptyAttribute("AirportTransferRequired", "True", "AirportTransferSponsorOtherUid")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AirportTransferSponsorOtherName { get; set; }

        #endregion
        
        public bool IsCityTransferRequired { get; set; }
        public bool IsVehicleDisposalRequired { get; set; }

        public List<LogisticSponsorBaseDto> Sponsors { get; set; }

        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        public string AdditionalInfo { get; set; }
        
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