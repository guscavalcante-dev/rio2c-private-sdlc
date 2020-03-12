// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 03-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-12-2020
// ***********************************************************************
// <copyright file="UpdateLogisticMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateLogisticMainInformation</summary>
    public class UpdateLogisticMainInformation : CreateLogisticRequest
    {
        public Guid LogisticRequestUid { get; set; }
        public Guid InitialCollaboratorUid { get; set; }
        public string InitialCollaboratorName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticMainInformation"/> class.</summary>
        /// <param name="uid">The uid.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="othersRequiredSponsorUid">The others required sponsor uid.</param>
        /// <param name="accommodationSponsor">The accommodation sponsor.</param>
        /// <param name="airfareSponsor">The airfare sponsor.</param>
        /// <param name="airportTransferSponsor">The airport transfer sponsor.</param>
        /// <param name="isVehicleDisposalRequired">if set to <c>true</c> [is vehicle disposal required].</param>
        /// <param name="isCityTransferRequired">if set to <c>true</c> [is city transfer required].</param>
        /// <param name="list">The list.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public UpdateLogisticMainInformation(
            Guid uid, 
            AttendeeCollaborator attendeeCollaborator, 
            Guid othersRequiredSponsorUid, 
            AttendeeLogisticSponsor accommodationSponsor, 
            AttendeeLogisticSponsor airfareSponsor, 
            AttendeeLogisticSponsor airportTransferSponsor, 
            bool isVehicleDisposalRequired, 
            bool isCityTransferRequired, 
            List<LogisticSponsorBaseDto> list, 
            string userInterfaceLanguage)
        {
            this.LogisticRequestUid = uid;
            this.IsVehicleDisposalRequired = isVehicleDisposalRequired;
            this.IsCityTransferRequired = isCityTransferRequired;

            if (attendeeCollaborator != null)
            {
                this.AttendeeCollaboratorUid = this.InitialCollaboratorUid = attendeeCollaborator.Uid;
                this.InitialCollaboratorName = attendeeCollaborator.Collaborator.GetDisplayName();
            }

            this.UpdateAccommodationSponsor(othersRequiredSponsorUid, accommodationSponsor);
            this.UpdateAirfareSponsor(othersRequiredSponsorUid, airfareSponsor);
            this.UpdateTransferSponsor(othersRequiredSponsorUid, airportTransferSponsor);
            this.UpdateSponsors(list, userInterfaceLanguage);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticMainInformation"/> class.</summary>
        public UpdateLogisticMainInformation()
        {
        }

        #region Private Methods

        /// <summary>Updates the transfer sponsor.</summary>
        /// <param name="othersRequiredSponsorUid">The others required sponsor uid.</param>
        /// <param name="airportTransferSponsor">The airport transfer sponsor.</param>
        private void UpdateTransferSponsor(Guid othersRequiredSponsorUid, AttendeeLogisticSponsor airportTransferSponsor)
        {
            if (airportTransferSponsor == null)
            {
                return;
            }

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

        /// <summary>Updates the airfare sponsor.</summary>
        /// <param name="othersRequiredSponsorUid">The others required sponsor uid.</param>
        /// <param name="airfareSponsor">The airfare sponsor.</param>
        private void UpdateAirfareSponsor(Guid othersRequiredSponsorUid, AttendeeLogisticSponsor airfareSponsor)
        {
            if (airfareSponsor == null)
            {
                return;
            }

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

        /// <summary>Updates the accommodation sponsor.</summary>
        /// <param name="othersRequiredSponsorUid">The others required sponsor uid.</param>
        /// <param name="accommodationSponsor">The accommodation sponsor.</param>
        private void UpdateAccommodationSponsor(Guid othersRequiredSponsorUid, AttendeeLogisticSponsor accommodationSponsor)
        {
            if (accommodationSponsor == null)
            {
                return;
            }

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

        /// <summary>Updates the sponsors.</summary>
        /// <param name="sponsors">The sponsors.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        private void UpdateSponsors(List<LogisticSponsorBaseDto> sponsors, string userInterfaceLanguage)
        {
            sponsors.ForEach(g => g.Name.GetSeparatorTranslation(userInterfaceLanguage, Language.Separator));
            this.Sponsors = sponsors.OrderBy(e => e.IsOtherRequired).ThenBy(e => e.Name).ToList();
        }

        #endregion
    }
}