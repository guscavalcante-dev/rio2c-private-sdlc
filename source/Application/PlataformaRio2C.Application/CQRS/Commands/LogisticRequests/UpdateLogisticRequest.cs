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
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateLogisticSponsors</summary>
    public class UpdateLogisticRequest : CreateLogisticRequest
    {
        public Guid LogisticRequestUid { get; set; }
        public Guid InitialCollaboratorUid { get; set; }
        public string InitialCollaboratorName { get; set; }

        public UpdateLogisticRequest()
        {
        }

        public UpdateLogisticRequest(Guid uid, AttendeeCollaborator attendeeCollaborator, Guid othersRequiredSponsorUid, AttendeeLogisticSponsor accommodationSponsor, AttendeeLogisticSponsor airfareSponsor, AttendeeLogisticSponsor airportTransferSponsor, bool isVehicleDisposalRequired, bool isCityTransferRequired, List<LogisticSponsorBaseDto> list, string userInterfaceLanguage)
        {
            this.LogisticRequestUid = uid;
            this.IsVehicleDisposalRequired = isVehicleDisposalRequired;
            this.IsCityTransferRequired = isCityTransferRequired;

            if (attendeeCollaborator != null)
            {
                this.AttendeeCollaboratorUid = this.InitialCollaboratorUid = attendeeCollaborator.Uid;
                this.InitialCollaboratorName = attendeeCollaborator.Collaborator.GetDisplayName();
            }

            UpdateAccommodationSponsor(othersRequiredSponsorUid, accommodationSponsor);
            UpdateAirfareSponsor(othersRequiredSponsorUid, airfareSponsor);
            UpdateTransferSponsor(othersRequiredSponsorUid, airportTransferSponsor);

            this.UpdateSponsors(list, userInterfaceLanguage);
        }

        private void UpdateTransferSponsor(Guid othersRequiredSponsorUid, AttendeeLogisticSponsor airportTransferSponsor)
        {
            if (airportTransferSponsor == null)
                return;

            if (airportTransferSponsor.IsOther)
            {
                this.AirportTransferSponsorOtherName = airportTransferSponsor.LogisticSponsor?.Name;
                this.AirportTransferSponsorOtherUid = airportTransferSponsor.Uid;
                this.AirportTransferSponsorUid = othersRequiredSponsorUid;
            }
            else
            {
                this.AccommodationSponsorUid = airportTransferSponsor.Uid;
            }
        }

        private void UpdateAirfareSponsor(Guid othersRequiredSponsorUid, AttendeeLogisticSponsor airfareSponsor)
        {
            if (airfareSponsor == null)
                return;

            if (airfareSponsor.IsOther)
            {
                this.AirfareSponsorOtherName = airfareSponsor.LogisticSponsor?.Name;
                this.AirfareSponsorOtherUid = airfareSponsor.Uid;
                this.AirfareSponsorUid = othersRequiredSponsorUid;
            }
            else
            {
                this.AirfareSponsorUid = airfareSponsor.Uid;
            }
        }

        private void UpdateAccommodationSponsor(Guid othersRequiredSponsorUid, AttendeeLogisticSponsor accommodationSponsor)
        {
            if (accommodationSponsor == null)
                return;

            if (accommodationSponsor.IsOther)
            {
                this.AccommodationSponsorOtherName = accommodationSponsor.LogisticSponsor?.Name;
                this.AccommodationSponsorOtherUid = accommodationSponsor.Uid;
                this.AccommodationSponsorUid = othersRequiredSponsorUid;
            }
            else
            {
                this.AccommodationSponsorUid = accommodationSponsor.Uid;
            }
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

        #region Private Methods

        /// <summary>Updates the titles.</summary>
        /// <param name="languagesDtos">The languages dtos.</param>
        //private void UpdateNames(LogisticSponsorBaseDto dto, List<LanguageDto> languagesDtos)
        //{
        //    this.Names = new List<LogisticSponsorsNameBaseCommand>();
        //    foreach (var languageDto in languagesDtos)
        //    {
        //        this.Names.Add(new LogisticSponsorsNameBaseCommand(dto, languageDto));
        //    }
        //}

        #endregion
    }
}