// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-20-2020
// ***********************************************************************
// <copyright file="Logistic.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Logistic</summary>
    public class Logistic : Entity
    {
        public static readonly int AdditionalInfoMaxLength = 1000;

        public int AttendeeCollaboratorId { get; private set; }
        public bool IsAirfareSponsored { get; private set; }
        public int? AirfareAttendeeLogisticSponsorId { get; private set; }
        public bool IsAccommodationSponsored { get; private set; }
        public int? AccommodationAttendeeLogisticSponsorId { get; private set; }
        public bool IsAirportTransferSponsored { get; private set; }
        public int? AirportTransferAttendeeLogisticSponsorId { get; private set; }
        public bool IsCityTransferRequired { get; private set; }
        public bool IsVehicleDisposalRequired { get; private set; }
        public string AdditionalInfo { get; private set; }

        public virtual AttendeeCollaborator AttendeeCollaborator { get; private set; }
        public virtual AttendeeLogisticSponsor AirfareAttendeeLogisticSponsor { get; private set; }
        public virtual AttendeeLogisticSponsor AccommodationAttendeeLogisticSponsor { get; private set; }
        public virtual AttendeeLogisticSponsor AirportTransferAttendeeLogisticSponsor { get; private set; }
        public virtual User CreateUser { get; private set; }

        public virtual List<LogisticAirfare> LogisticAirfares { get; private set; }
        public virtual List<LogisticAccommodation> LogisticAccommodations { get; private set; }
        public virtual List<LogisticTransfer> LogisticTransfers { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Logistic"/> class.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="isAirfareSponsored">if set to <c>true</c> [is airfare sponsored].</param>
        /// <param name="airfareAttendeeLogisticSponsor">The airfare attendee logistic sponsor.</param>
        /// <param name="otherAirfareLogisticSponsor">The other airfare logistic sponsor.</param>
        /// <param name="otherAirfareSponsorName">Name of the other airfare sponsor.</param>
        /// <param name="isAccommodationSponsored">if set to <c>true</c> [is accommodation sponsored].</param>
        /// <param name="accommodationAttendeeLogisticSponsor">The accommodation attendee logistic sponsor.</param>
        /// <param name="otherAccommodationLogisticSponsor">The other accommodation logistic sponsor.</param>
        /// <param name="otherAccommodationSponsorName">Name of the other accommodation sponsor.</param>
        /// <param name="isAirportTransferSponsored">if set to <c>true</c> [is airport transfer sponsored].</param>
        /// <param name="airportTransferAttendeeLogisticSponsor">The airport transfer attendee logistic sponsor.</param>
        /// <param name="otherAirportTransferLogisticSponsor">The other airport transfer logistic sponsor.</param>
        /// <param name="otherAirportTransferSponsorName">Name of the other airport transfer sponsor.</param>
        /// <param name="isCityTransferRequired">if set to <c>true</c> [is city transfer required].</param>
        /// <param name="isVehicleDisposalRequired">if set to <c>true</c> [is vehicle disposal required].</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public Logistic(
            Edition edition,
            AttendeeCollaborator attendeeCollaborator,
            bool isAirfareSponsored,
            AttendeeLogisticSponsor airfareAttendeeLogisticSponsor,
            LogisticSponsor otherAirfareLogisticSponsor,
            string otherAirfareSponsorName,
            bool isAccommodationSponsored,
            AttendeeLogisticSponsor accommodationAttendeeLogisticSponsor,
            LogisticSponsor otherAccommodationLogisticSponsor,
            string otherAccommodationSponsorName,
            bool isAirportTransferSponsored,
            AttendeeLogisticSponsor airportTransferAttendeeLogisticSponsor,
            LogisticSponsor otherAirportTransferLogisticSponsor,
            string otherAirportTransferSponsorName,
            bool isCityTransferRequired,
            bool isVehicleDisposalRequired,
            string additionalInfo,
            int userId)
        {
            this.AttendeeCollaborator = attendeeCollaborator;
            this.AttendeeCollaboratorId = attendeeCollaborator.Id;

            this.IsAirfareSponsored = isAirfareSponsored;
            this.UpdateAirfareAttendeeLogisticSponsor(edition, airfareAttendeeLogisticSponsor, otherAirfareLogisticSponsor, otherAirfareSponsorName, userId);
            this.IsAccommodationSponsored = isAccommodationSponsored;
            this.UpdateAccommodationAttendeeLogisticSponsor(edition, accommodationAttendeeLogisticSponsor, otherAccommodationLogisticSponsor, otherAccommodationSponsorName, userId);
            this.IsAirportTransferSponsored = isAirportTransferSponsored;
            this.UpdateAirportTransferAttendeeLogisticSponsor(edition, airportTransferAttendeeLogisticSponsor, otherAirportTransferLogisticSponsor, otherAirportTransferSponsorName, userId);

            this.IsCityTransferRequired = isCityTransferRequired;
            this.IsVehicleDisposalRequired = isVehicleDisposalRequired;
            this.AdditionalInfo = additionalInfo?.Trim();

            this.IsDeleted = false;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logistic"/> class.
        /// </summary>
        protected Logistic()
        {
        }

        /// <summary>Updates the specified edition.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="isAirfareSponsored">if set to <c>true</c> [is airfare sponsored].</param>
        /// <param name="airfareAttendeeLogisticSponsor">The airfare attendee logistic sponsor.</param>
        /// <param name="otherAirfareLogisticSponsor">The other airfare logistic sponsor.</param>
        /// <param name="otherAirfareSponsorName">Name of the other airfare sponsor.</param>
        /// <param name="isAccommodationSponsored">if set to <c>true</c> [is accommodation sponsored].</param>
        /// <param name="accommodationAttendeeLogisticSponsor">The accommodation attendee logistic sponsor.</param>
        /// <param name="otherAccommodationLogisticSponsor">The other accommodation logistic sponsor.</param>
        /// <param name="otherAccommodationSponsorName">Name of the other accommodation sponsor.</param>
        /// <param name="isAirportTransferSponsored">if set to <c>true</c> [is airport transfer sponsored].</param>
        /// <param name="airportTransferAttendeeLogisticSponsor">The airport transfer attendee logistic sponsor.</param>
        /// <param name="otherAirportTransferLogisticSponsor">The other airport transfer logistic sponsor.</param>
        /// <param name="otherAirportTransferSponsorName">Name of the other airport transfer sponsor.</param>
        /// <param name="isCityTransferRequired">if set to <c>true</c> [is city transfer required].</param>
        /// <param name="isVehicleDisposalRequired">if set to <c>true</c> [is vehicle disposal required].</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            Edition edition,
            bool isAirfareSponsored,
            AttendeeLogisticSponsor airfareAttendeeLogisticSponsor,
            LogisticSponsor otherAirfareLogisticSponsor,
            string otherAirfareSponsorName,
            bool isAccommodationSponsored,
            AttendeeLogisticSponsor accommodationAttendeeLogisticSponsor,
            LogisticSponsor otherAccommodationLogisticSponsor,
            string otherAccommodationSponsorName,
            bool isAirportTransferSponsored,
            AttendeeLogisticSponsor airportTransferAttendeeLogisticSponsor,
            LogisticSponsor otherAirportTransferLogisticSponsor,
            string otherAirportTransferSponsorName,
            bool isCityTransferRequired,
            bool isVehicleDisposalRequired,
            string additionalInfo,
            int userId)
        {
            this.IsAirfareSponsored = isAirfareSponsored;
            this.UpdateAirfareAttendeeLogisticSponsor(edition, airfareAttendeeLogisticSponsor, otherAirfareLogisticSponsor, otherAirfareSponsorName, userId);
            this.IsAccommodationSponsored = isAccommodationSponsored;
            this.UpdateAccommodationAttendeeLogisticSponsor(edition, accommodationAttendeeLogisticSponsor, otherAccommodationLogisticSponsor, otherAccommodationSponsorName, userId);
            this.IsAirportTransferSponsored = isAirportTransferSponsored;
            this.UpdateAirportTransferAttendeeLogisticSponsor(edition, airportTransferAttendeeLogisticSponsor, otherAirportTransferLogisticSponsor, otherAirportTransferSponsorName, userId);

            this.IsCityTransferRequired = isCityTransferRequired;
            this.IsVehicleDisposalRequired = isVehicleDisposalRequired;
            this.AdditionalInfo = additionalInfo?.Trim();

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            if (this.IsDeleted)
            {
                return;
            }

            this.DeleteLogisticAirfares(userId);
            this.DeleteLogisticAccommodations(userId);
            this.DeleteLogisticTransfers(userId);

            if (this.FindAllLogisticAirfaresNotDeleted()?.Any() == true || this.FindAllLogisticAccommodationsNotDeleted()?.Any() == true || this.FindAllLogisticTransfersNotDeleted()?.Any() == true)
            {
                return;
            }

            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Logistic Airfares

        /// <summary>Deletes the logistic airfares.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteLogisticAirfares(int userId)
        {
            foreach (var logisticAirfare in this.FindAllLogisticAirfaresNotDeleted())
            {
                logisticAirfare?.Delete(userId);
            }
        }

        /// <summary>Finds all logistic airfares not deleted.</summary>
        /// <returns></returns>
        private List<LogisticAirfare> FindAllLogisticAirfaresNotDeleted()
        {
            return this.LogisticAirfares?.Where(la => !la.IsDeleted)?.ToList();
        }

        #endregion

        #region Logistic Accommodations

        /// <summary>Deletes the logistic accommodations.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteLogisticAccommodations(int userId)
        {
            foreach (var logisticAccommodation in this.FindAllLogisticAccommodationsNotDeleted())
            {
                logisticAccommodation?.Delete(userId);
            }
        }

        /// <summary>Finds all logistic accommodations not deleted.</summary>
        /// <returns></returns>
        private List<LogisticAccommodation> FindAllLogisticAccommodationsNotDeleted()
        {
            return this.LogisticAccommodations?.Where(la => !la.IsDeleted)?.ToList();
        }

        #endregion

        #region Logistic Transfers

        /// <summary>Deletes the logistic transfers.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteLogisticTransfers(int userId)
        {
            foreach (var logisticTransfer in this.FindAllLogisticTransfersNotDeleted())
            {
                logisticTransfer?.Delete(userId);
            }
        }

        /// <summary>Finds all logistic transfers not deleted.</summary>
        /// <returns></returns>
        private List<LogisticTransfer> FindAllLogisticTransfersNotDeleted()
        {
            return this.LogisticTransfers?.Where(la => !la.IsDeleted)?.ToList();
        }

        #endregion

        #region Airfare Attendee Logistic Sponsors

        /// <summary>Dissociates the airfare attendee logistic sponsor.</summary>
        /// <param name="userId">The user identifier.</param>
        public void DissociateAirfareAttendeeLogisticSponsor(int userId)
        {
            this.AirfareAttendeeLogisticSponsorId = null;
            this.AirfareAttendeeLogisticSponsor = null;
            this.IsAirfareSponsored = false;

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Updates the airfare attendee logistic sponsor.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="airfareAttendeeLogisticSponsor">The airfare attendee logistic sponsor.</param>
        /// <param name="otherAirfareLogisticSponsor">The other airfare logistic sponsor.</param>
        /// <param name="otherAirfareSponsorName">Name of the other airfare sponsor.</param>
        /// <param name="userId">The user identifier.</param>
        private void UpdateAirfareAttendeeLogisticSponsor(
            Edition edition,
            AttendeeLogisticSponsor airfareAttendeeLogisticSponsor,
            LogisticSponsor otherAirfareLogisticSponsor,
            string otherAirfareSponsorName,
            int userId)
        {
            // Other logistic sponsor typed exists in database
            if (otherAirfareLogisticSponsor != null)
            {
                // Other attendee logistic sponsor type exists in database
                var otherAirfareAttendeeLogisticSponsor = otherAirfareLogisticSponsor.AttendeeLogisticSponsors.FirstOrDefault(als => als.EditionId == edition?.Id);
                if (otherAirfareAttendeeLogisticSponsor != null)
                {
                    otherAirfareAttendeeLogisticSponsor.Update(true, false, userId);
                    airfareAttendeeLogisticSponsor = otherAirfareAttendeeLogisticSponsor;
                }
                // Must create the attendee logistic sponsor typed
                else
                {
                    airfareAttendeeLogisticSponsor = new AttendeeLogisticSponsor(edition, otherAirfareLogisticSponsor, true, false, userId);
                }
            }
            // Other logistic sponsor type does not exist in database and must be created
            else if (!string.IsNullOrEmpty(otherAirfareSponsorName?.Trim()))
            {
                airfareAttendeeLogisticSponsor = new AttendeeLogisticSponsor(edition, otherAirfareSponsorName, userId);
            }

            if (this.IsAirfareSponsored && airfareAttendeeLogisticSponsor != null)
            {
                this.AirfareAttendeeLogisticSponsorId = airfareAttendeeLogisticSponsor.Id;
                this.AirfareAttendeeLogisticSponsor = airfareAttendeeLogisticSponsor;
            }
            else
            {
                this.AirfareAttendeeLogisticSponsorId = null;
                this.AirfareAttendeeLogisticSponsor = null;
            }
        }

        #endregion

        #region Accommodation Attendee Logistic Sponsors

        /// <summary>Dissociates the accommodation attendee logistic sponsor.</summary>
        /// <param name="userId">The user identifier.</param>
        public void DissociateAccommodationAttendeeLogisticSponsor(int userId)
        {
            this.AccommodationAttendeeLogisticSponsorId = null;
            this.AccommodationAttendeeLogisticSponsor = null;
            this.IsAccommodationSponsored = false;

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Updates the accommodation attendee logistic sponsor.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="accommodationAttendeeLogisticSponsor">The accommodation attendee logistic sponsor.</param>
        /// <param name="otherAccommodationLogisticSponsor">The other accommodation logistic sponsor.</param>
        /// <param name="otherAccommodationSponsorName">Name of the other accommodation sponsor.</param>
        /// <param name="userId">The user identifier.</param>
        private void UpdateAccommodationAttendeeLogisticSponsor(
            Edition edition,
            AttendeeLogisticSponsor accommodationAttendeeLogisticSponsor,
            LogisticSponsor otherAccommodationLogisticSponsor,
            string otherAccommodationSponsorName,
            int userId)
        {
            // Other logistic sponsor typed exists in database
            if (otherAccommodationLogisticSponsor != null)
            {
                // Other attendee logistic sponsor type exists in database
                var otherAccommodationAttendeeLogisticSponsor = otherAccommodationLogisticSponsor.AttendeeLogisticSponsors.FirstOrDefault(als => als.EditionId == edition?.Id);
                if (otherAccommodationAttendeeLogisticSponsor != null)
                {
                    otherAccommodationAttendeeLogisticSponsor.Update(true, false, userId);
                    accommodationAttendeeLogisticSponsor = otherAccommodationAttendeeLogisticSponsor;
                }
                // Must create the attendee logistic sponsor typed
                else
                {
                    accommodationAttendeeLogisticSponsor = new AttendeeLogisticSponsor(edition, otherAccommodationLogisticSponsor, true, false, userId);
                }
            }
            // Other logistic sponsor type does not exist in database and must be created
            else if (!string.IsNullOrEmpty(otherAccommodationSponsorName?.Trim()))
            {
                accommodationAttendeeLogisticSponsor = otherAccommodationSponsorName.IsCaseInsensitiveEqualTo(this.AirfareAttendeeLogisticSponsor.LogisticSponsor.Name) ? this.AirfareAttendeeLogisticSponsor :
                                                       new AttendeeLogisticSponsor(edition, otherAccommodationSponsorName, userId);
            }

            if (this.IsAccommodationSponsored && accommodationAttendeeLogisticSponsor != null)
            {
                this.AccommodationAttendeeLogisticSponsorId = accommodationAttendeeLogisticSponsor.Id;
                this.AccommodationAttendeeLogisticSponsor = accommodationAttendeeLogisticSponsor;
            }
            else
            {
                this.AccommodationAttendeeLogisticSponsorId = null;
                this.AccommodationAttendeeLogisticSponsor = null;
            }
        }

        #endregion

        #region Airport Transfer Attendee Logistic Sponsors

        /// <summary>Dissociates the airport transfer attendee logistic sponsor.</summary>
        /// <param name="userId">The user identifier.</param>
        public void DissociateAirportTransferAttendeeLogisticSponsor(int userId)
        {
            this.AirportTransferAttendeeLogisticSponsorId = null;
            this.AirportTransferAttendeeLogisticSponsor = null;
            this.IsAirportTransferSponsored = false;

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Updates the airport transfer attendee logistic sponsor.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="airportTransferAttendeeLogisticSponsor">The airport transfer attendee logistic sponsor.</param>
        /// <param name="otherAirportTransferLogisticSponsor">The other airport transfer logistic sponsor.</param>
        /// <param name="otherAirportTransferSponsorName">Name of the other airport transfer sponsor.</param>
        /// <param name="userId">The user identifier.</param>
        private void UpdateAirportTransferAttendeeLogisticSponsor(
            Edition edition,
            AttendeeLogisticSponsor airportTransferAttendeeLogisticSponsor,
            LogisticSponsor otherAirportTransferLogisticSponsor,
            string otherAirportTransferSponsorName,
            int userId)
        {
            // Other logistic sponsor typed exists in database
            if (otherAirportTransferLogisticSponsor != null)
            {
                // Other attendee logistic sponsor type exists in database
                var otherAirportTransferAttendeeLogisticSponsor = otherAirportTransferLogisticSponsor.AttendeeLogisticSponsors.FirstOrDefault(als => als.EditionId == edition?.Id);
                if (otherAirportTransferAttendeeLogisticSponsor != null)
                {
                    otherAirportTransferAttendeeLogisticSponsor.Update(true, false, userId);
                    airportTransferAttendeeLogisticSponsor = otherAirportTransferAttendeeLogisticSponsor;
                }
                // Must create the attendee logistic sponsor typed
                else
                {
                    airportTransferAttendeeLogisticSponsor = new AttendeeLogisticSponsor(edition, otherAirportTransferLogisticSponsor, true, false, userId);
                }
            }
            // Other logistic sponsor type does not exist in database and must be created
            else if (!string.IsNullOrEmpty(otherAirportTransferSponsorName?.Trim()))
            {
                airportTransferAttendeeLogisticSponsor = otherAirportTransferSponsorName.IsCaseInsensitiveEqualTo(this.AirfareAttendeeLogisticSponsor.LogisticSponsor.Name) ? this.AirfareAttendeeLogisticSponsor :
                                                         otherAirportTransferSponsorName.IsCaseInsensitiveEqualTo(this.AccommodationAttendeeLogisticSponsor.LogisticSponsor.Name) ? this.AccommodationAttendeeLogisticSponsor :
                                                         new AttendeeLogisticSponsor(edition, otherAirportTransferSponsorName, userId);
            }

            if (this.IsAirportTransferSponsored && airportTransferAttendeeLogisticSponsor != null)
            {
                this.AirportTransferAttendeeLogisticSponsorId = airportTransferAttendeeLogisticSponsor.Id;
                this.AirportTransferAttendeeLogisticSponsor = airportTransferAttendeeLogisticSponsor;
            }
            else
            {
                this.AirportTransferAttendeeLogisticSponsorId = null;
                this.AirportTransferAttendeeLogisticSponsor = null;
            }
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            this.ValidateAttendeeCollaborator();
            this.ValidateAirfareAttendeeLogisticSponsor();
            this.ValidateAccommodationAttendeeLogisticSponsor();
            this.ValidateAirportTransferAttendeeLogisticSponsor();
            this.ValidateAdditionalInfo();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the attendee collaborator.</summary>
        public void ValidateAttendeeCollaborator()
        {
            if (this.AttendeeCollaborator == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Participant), new string[] { "AttendeeCollaboratorUid" }));
            }
        }

        /// <summary>Validates the airfare attendee logistic sponsor.</summary>
        public void ValidateAirfareAttendeeLogisticSponsor()
        {
            if (this.IsAirfareSponsored && this.AirfareAttendeeLogisticSponsor == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Sponsor), new string[] { "AirfareSponsorUid" }));
            }
        }

        /// <summary>Validates the accommodation attendee logistic sponsor.</summary>
        public void ValidateAccommodationAttendeeLogisticSponsor()
        {
            if (this.IsAccommodationSponsored && this.AccommodationAttendeeLogisticSponsor == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Sponsor), new string[] { "AccommodationSponsorUid" }));
            }
        }

        /// <summary>Validates the airport transfer attendee logistic sponsor.</summary>
        public void ValidateAirportTransferAttendeeLogisticSponsor()
        {
            if (this.IsAirportTransferSponsored && this.AirportTransferAttendeeLogisticSponsor == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Sponsor), new string[] { "AirportTransferSponsorUid" }));
            }
        }

        /// <summary>Validates the additional information.</summary>
        public void ValidateAdditionalInfo()
        {
            if (!string.IsNullOrEmpty(this.AdditionalInfo?.Trim()) && this.AdditionalInfo?.Trim().Length > AdditionalInfoMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.AdditionalInfo, AdditionalInfoMaxLength, 1), new string[] { "AdditionalInfo" }));
            }
        }

        #endregion    
    }
}