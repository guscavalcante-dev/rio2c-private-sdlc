// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 02-03-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="CreateLogistic.cs" company="Softo">
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
    /// <summary>CreateLogistic</summary>
    public class CreateLogistic : BaseCommand
    {
        [Display(Name = "Participant", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid AttendeeCollaboratorUid { get; set; }

        #region Airfare sponsor

        public bool IsAirfareSponsored { get; set; }

        public bool? AirfareRequired { get; set; }

        [Display(Name = "Sponsor", ResourceType = typeof(Labels))]
        [RadioButtonRequiredIf("IsAirfareSponsored", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? AirfareSponsorUid { get; set; }

        [Display(Name = "Sponsor", ResourceType = typeof(Labels))]
        [RequiredIfOneWithValueAndOtherEmpty("AirfareRequired", "True", "AirfareSponsorOtherName")]
        public Guid? AirfareSponsorOtherUid { get; set; }

        [Display(Name = "Sponsor", ResourceType = typeof(Labels))]
        [RequiredIfOneWithValueAndOtherEmpty("AirfareRequired", "True", "AirfareSponsorOtherUid")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AirfareSponsorOtherName { get; set; }

        #endregion

        #region Accommodation Sponsor

        public bool IsAccommodationSponsored { get; set; }

        [Display(Name = "Sponsor", ResourceType = typeof(Labels))]
        [RadioButtonRequiredIf("IsAccommodationSponsored", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? AccommodationSponsorUid { get; set; }

        public bool? AccommodationRequired { get; set; }

        [Display(Name = "Sponsor", ResourceType = typeof(Labels))]
        [RequiredIfOneWithValueAndOtherEmpty("AccommodationRequired", "True", "AccommodationSponsorOtherName")]
        public Guid? AccommodationSponsorOtherUid { get; set; }

        [Display(Name = "Sponsor", ResourceType = typeof(Labels))]
        [RequiredIfOneWithValueAndOtherEmpty("AccommodationRequired", "True", "AccommodationSponsorOtherUid")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AccommodationSponsorOtherName { get; set; }

        #endregion

        #region AirportTransfer Sponsor

        public bool IsAirportTransferSponsored { get; set; }

        [Display(Name = "Sponsor", ResourceType = typeof(Labels))]
        [RadioButtonRequiredIf("IsAirportTransferSponsored", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? AirportTransferSponsorUid { get; set; }

        public bool? AirportTransferRequired { get; set; }

        [Display(Name = "Sponsor", ResourceType = typeof(Labels))]
        [RequiredIfOneWithValueAndOtherEmpty("AirportTransferRequired", "True", "AirportTransferSponsorOtherName")]
        public Guid? AirportTransferSponsorOtherUid { get; set; }

        [Display(Name = "Sponsor", ResourceType = typeof(Labels))]
        [RequiredIfOneWithValueAndOtherEmpty("AirportTransferRequired", "True", "AirportTransferSponsorOtherUid")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AirportTransferSponsorOtherName { get; set; }

        #endregion

        public bool IsCityTransferRequired { get; set; }
        public bool IsVehicleDisposalRequired { get; set; }

        [Display(Name = "AdditionalInfo", ResourceType = typeof(Labels))]
        [StringLength(1000, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AdditionalInfo { get; set; }

        public List<AttendeeLogisticSponsorJsonDto> MainSponsors { get; set; }
        public List<AttendeeLogisticSponsorJsonDto> OtherSponsors { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateLogistic"/> class.</summary>
        /// <param name="mainLogisticSponsorBaseDtos">The main logistic sponsor base dtos.</param>
        /// <param name="otherLogisticSponsorBaseDtos">The other logistic sponsor base dtos.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public CreateLogistic(
            List<AttendeeLogisticSponsorJsonDto> mainLogisticSponsorBaseDtos,
            List<AttendeeLogisticSponsorJsonDto> otherLogisticSponsorBaseDtos,
            string userInterfaceLanguage)
        {
            this.UpdateModelsAndLists(mainLogisticSponsorBaseDtos, otherLogisticSponsorBaseDtos, userInterfaceLanguage);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateLogistic"/> class.</summary>
        public CreateLogistic()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="mainLogisticSponsorBaseDtos">The main logistic sponsor base dtos.</param>
        /// <param name="otherLogisticSponsorBaseDtos">The other logistic sponsor base dtos.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateModelsAndLists(
            List<AttendeeLogisticSponsorJsonDto> mainLogisticSponsorBaseDtos,
            List<AttendeeLogisticSponsorJsonDto> otherLogisticSponsorBaseDtos,
            string userInterfaceLanguage)
        {
            // Mains logistic sponsors
            mainLogisticSponsorBaseDtos.ForEach(g => g.Name.GetSeparatorTranslation(userInterfaceLanguage, Language.Separator));
            this.MainSponsors = mainLogisticSponsorBaseDtos.OrderBy(e => e.IsOtherRequired).ThenBy(e => e.Name).ToList();

            // Mains logistic sponsors
            otherLogisticSponsorBaseDtos.ForEach(g => g.Name.GetSeparatorTranslation(userInterfaceLanguage, Language.Separator));
            this.OtherSponsors = otherLogisticSponsorBaseDtos.OrderBy(e => e.Name).ToList();
        }
    }
}