// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
// ***********************************************************************
// <copyright file="Organization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Organization</summary>
    public class Organization : AggregateRoot
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 81;
        public static readonly int CompanyNameMaxLength = 100;
        public static readonly int TradeNameMaxLength = 100;
        public static readonly int DocumentMaxLength = 50;
        public static readonly int WebsiteMaxLength = 100;
        public static readonly int SocialMediaMaxLength = 256;
        public static readonly int PhoneNumberMaxLength = 50;

        public int? HoldingId { get; private set; }
        public string Name { get; private set; }
        public string CompanyName { get; private set; }
        public string TradeName { get; private set; }
        public string Document { get; private set; }
        public string Website { get; private set; }
        public string SocialMedia { get; private set; }
        public string PhoneNumber { get; private set; }
        public int? AddressId { get; private set; }
        public DateTime? ImageUploadDate { get; private set; }
        
        public virtual Holding Holding { get; private set; }
        public virtual Address Address { get; private set; }
        public virtual User Updater { get; private set; }

        public virtual ICollection<OrganizationDescription> Descriptions { get; private set; }
        public virtual ICollection<AttendeeOrganization> AttendeeOrganizations { get; private set; }
        public virtual ICollection<OrganizationActivity> OrganizationActivities { get; private set; }
        public virtual ICollection<OrganizationTargetAudience> OrganizationTargetAudiences { get; private set; }

        //public virtual ICollection<PlayerInterest> Interests { get; private set; }
        //public virtual ICollection<Collaborator> Collaborators { get; private set; }
        //public virtual ICollection<Collaborator> CollaboratorsOld { get; private set; }
        //public virtual ICollection<PlayerActivity> PlayerActivitys { get; private set; }
        //public virtual ICollection<PlayerTargetAudience> PlayerTargetAudience { get; private set; }
        //public virtual ICollection<PlayerRestrictionsSpecifics> RestrictionsSpecifics { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Organization"/> class.</summary>
        /// <param name="uid">The uid.</param>
        /// <param name="holding">The holding.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="name">The name.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <param name="document">The document.</param>
        /// <param name="website">The website.</param>
        /// <param name="socialMedia">The social media.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="addressZipCode">The address zip code.</param>
        /// <param name="addressIsManual">if set to <c>true</c> [address is manual].</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="descriptions">The descriptions.</param>
        /// <param name="userId">The user identifier.</param>
        public Organization(
            Guid uid, 
            Holding holding, 
            Edition edition,
            OrganizationType organizationType,
            string name, 
            string companyName, 
            string tradeName, 
            string document, 
            string website, 
            string socialMedia, 
            string phoneNumber, 
            Country country,
            Guid? stateUid,
            string stateName,
            Guid? cityUid,
            string cityName,
            string address1,
            string address2,
            string addressZipCode,
            bool addressIsManual,
            bool isImageUploaded, 
            List<OrganizationDescription> descriptions, 
            int userId)
        {
            //this.Uid = uid;
            this.Holding = holding;
            this.HoldingId = holding?.Id;
            this.Name = name?.Trim();
            this.CompanyName = companyName?.Trim();
            this.TradeName = tradeName?.Trim();
            this.Document = document?.Trim();
            this.Website = website?.Trim();
            this.SocialMedia = socialMedia?.Trim();
            this.PhoneNumber = phoneNumber?.Trim();
            this.UpdateImageUploadDate(isImageUploaded, false);
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeDescriptions(descriptions, userId);
            this.SynchronizeAttendeeOrganizations(edition, organizationType, true, userId);
            this.UpdateAddress(country, stateUid, stateName, cityUid, cityName, address1, address2, addressZipCode, addressIsManual, userId);
        }

        /// <summary>Initializes a new instance of the <see cref="Organization"/> class.</summary>
        protected Organization()
        {
        }

        /// <summary>Updates the specified holding.</summary>
        /// <param name="holding">The holding.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="name">The name.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <param name="document">The document.</param>
        /// <param name="website">The website.</param>
        /// <param name="socialMedia">The social media.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="addressZipCode">The address zip code.</param>
        /// <param name="addressIsManual">if set to <c>true</c> [address is manual].</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        /// <param name="descriptions">The descriptions.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        public void Update (
            Holding holding,
            Edition edition,
            OrganizationType organizationType,
            string name,
            string companyName,
            string tradeName,
            string document,
            string website,
            string socialMedia,
            string phoneNumber,
            Country country,
            Guid? stateUid,
            string stateName,
            Guid? cityUid,
            string cityName,
            string address1,
            string address2,
            string addressZipCode,
            bool addressIsManual,
            bool isImageUploaded,
            bool isImageDeleted,
            List<OrganizationDescription> descriptions,
            bool isAddingToCurrentEdition,
            int userId)
        {
            //this.Uid = uid;
            this.Holding = holding;
            this.HoldingId = holding?.Id;
            this.Name = name?.Trim();
            this.CompanyName = companyName?.Trim();
            this.TradeName = tradeName?.Trim();
            this.Document = document?.Trim();
            this.Website = website?.Trim();
            this.SocialMedia = socialMedia?.Trim();
            this.PhoneNumber = phoneNumber?.Trim();
            this.UpdateImageUploadDate(isImageUploaded, isImageDeleted);
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeDescriptions(descriptions, userId);
            this.SynchronizeAttendeeOrganizations(edition, organizationType, isAddingToCurrentEdition, userId);
            this.UpdateAddress(country, stateUid, stateName, cityUid, cityName, address1, address2, addressZipCode, addressIsManual, userId);
        }

        /// <summary>Deletes the specified edition.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        public void Delete(Edition edition, OrganizationType organizationType, int userId)
        {
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.DeleteAttendeeOrganization(edition, organizationType, userId);

            if (this.FindAllAttendeeOrganizationsNotDeleted(edition)?.Any() == false)
            {
                this.IsDeleted = true;
                this.UpdateImageUploadDate(false, true);
            }
        }

        /// <summary>Updates the image upload date.</summary>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        private void UpdateImageUploadDate(bool isImageUploaded, bool isImageDeleted)
        {
            if (isImageUploaded)
            {
                this.ImageUploadDate = DateTime.Now;
            }
            else if (isImageDeleted)
            {
                this.ImageUploadDate = null;
            }
        }

        #region Onboarding

        public void OnboardData(
            Edition edition,
            OrganizationType organizationType,
            string name,
            string companyName,
            string document,
            Country country,
            Guid? stateUid,
            string stateName,
            Guid? cityUid,
            string cityName,
            string address1,
            string address2,
            string addressZipCode,
            bool addressIsManual,
            bool isImageUploaded,
            bool isImageDeleted,
            List<OrganizationDescription> descriptions,
            List<Activity> activities,
            List<TargetAudience> targetAudiences,
            int userId)
        {
            //this.Uid = uid;
            //this.Holding = holding;
            //this.HoldingId = holding?.Id;
            this.Name = name?.Trim();
            this.CompanyName = companyName?.Trim();
            //this.TradeName = tradeName?.Trim();
            this.Document = document?.Trim();
            //this.Website = website?.Trim();
            //this.SocialMedia = socialMedia?.Trim();
            //this.PhoneNumber = phoneNumber?.Trim();
            this.UpdateImageUploadDate(isImageUploaded, isImageDeleted);
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeDescriptions(descriptions, userId);
            this.SynchronizeOrganizationActivities(activities, userId);
            this.SynchronizeOrganizationTargetAudiences(targetAudiences, userId);
            this.SynchronizeAttendeeOrganizations(edition, organizationType, true, userId);
            this.UpdateAddress(country, stateUid, stateName, cityUid, cityName, address1, address2, addressZipCode, addressIsManual, userId);
            this.OnboardAttendeeOrganizationData(edition, userId);
        }

        #endregion

        #region Address

        /// <summary>Updates the address.</summary>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="addressZipCode">The address zip code.</param>
        /// <param name="addressIsManual">if set to <c>true</c> [address is manual].</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateAddress(
            Country country,
            Guid? stateUid,
            string stateName,
            Guid? cityUid,
            string cityName,
            string address1,
            string address2,
            string addressZipCode,
            bool addressIsManual,
            int userId)
        {
            if (country == null)
            {
                this.Address?.Delete(userId);
            }
            else if (this.Address == null)
            {
                this.Address = new Address(
                    country, 
                    stateUid, 
                    stateName, 
                    cityUid, 
                    cityName, 
                    address1,
                    address2, 
                    addressZipCode, 
                    addressIsManual, 
                    userId);
            }
            else
            {
                this.Address.Update(
                    country, 
                    stateUid, 
                    stateName, 
                    cityUid, 
                    cityName,
                    address1,
                    address2,
                    addressZipCode,
                    addressIsManual, 
                    userId);
            }
        }

        #endregion

        #region Descriptions

        /// <summary>Synchronizes the descriptions.</summary>
        /// <param name="descriptions">The descriptions.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeDescriptions(List<OrganizationDescription> descriptions, int userId)
        {
            if (this.Descriptions == null)
            {
                this.Descriptions = new List<OrganizationDescription>();
            }

            this.DeleteDescriptions(descriptions, userId);

            if (descriptions?.Any() != true)
            {
                return;
            }

            // Create or update descriptions
            foreach (var description in descriptions)
            {
                var descriptionDb = this.Descriptions.FirstOrDefault(d => d.Language.Code == description.Language.Code);
                if (descriptionDb != null)
                {
                    descriptionDb.Update(description);
                }
                else
                {
                    this.CreateDescription(description);
                }
            }
        }

        /// <summary>Deletes the descriptions.</summary>
        /// <param name="newDescriptions">The new descriptions.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteDescriptions(List<OrganizationDescription> newDescriptions, int userId)
        {
            var descriptionsToDelete = this.Descriptions.Where(db => newDescriptions?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false).ToList();
            foreach (var descriptionToDelete in descriptionsToDelete)
            {
                descriptionToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the description.</summary>
        /// <param name="description">The description.</param>
        private void CreateDescription(OrganizationDescription description)
        {
            this.Descriptions.Add(description);
        }

        #endregion

        #region Attendee Organizations

        /// <summary>Called when [attendee organization data].</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardAttendeeOrganizationData(Edition edition, int userId)
        {
            var attendeeOrganization = this.GetAttendeeOrganizationByEditionId(edition.Id);
            attendeeOrganization?.OnboardOrganizationData(userId);
            //else
            //{
            //    if (this.AttendeeCollaborators == null)
            //    {
            //        this.AttendeeCollaborators = new List<AttendeeCollaborator>();
            //    }

            //    this.AttendeeCollaborators.Add(new AttendeeCollaborator(edition, null, this, false, userId));
            //}
        }

        /// <summary>Called when [attendee organization interests].</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardAttendeeOrganizationInterests(Edition edition, int userId)
        {
            var attendeeOrganization = this.GetAttendeeOrganizationByEditionId(edition.Id);
            attendeeOrganization?.OnboardInterests(userId);
            //else
            //{
            //    if (this.AttendeeCollaborators == null)
            //    {
            //        this.AttendeeCollaborators = new List<AttendeeCollaborator>();
            //    }

            //    this.AttendeeCollaborators.Add(new AttendeeCollaborator(edition, null, this, false, userId));
            //}
        }


        /// <summary>Synchronizes the attendee organizations.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeOrganizations(Edition edition, OrganizationType organizationType, bool isAddingToCurrentEdition, int userId)
        {
            // Synchronize only when is adding to current edition
            if (!isAddingToCurrentEdition)
            {
                return;
            }

            if (this.AttendeeOrganizations == null)
            {
                this.AttendeeOrganizations = new List<AttendeeOrganization>();
            }

            if (edition == null)
            {
                return;
            }

            var attendeeOrganization = this.AttendeeOrganizations.FirstOrDefault(ao => ao.EditionId == edition.Id);
            if (attendeeOrganization != null)
            {
                attendeeOrganization.Restore(organizationType, userId);
            }
            else
            {
                this.AttendeeOrganizations.Add(new AttendeeOrganization(edition, this, organizationType, userId));
            }
        }

        /// <summary>Deletes the attendee organization.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeOrganization(Edition edition, OrganizationType organizationType, int userId)
        {
            foreach (var attendeeOrganization in this.FindAllAttendeeOrganizationsNotDeleted(edition))
            {
                attendeeOrganization?.Delete(organizationType, userId);
            }
        }

        /// <summary>Gets the attendee organization by edition identifier.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        private AttendeeOrganization GetAttendeeOrganizationByEditionId(int editionId)
        {
            return this.AttendeeOrganizations?.FirstOrDefault(ao => ao.EditionId == editionId);
        }

        /// <summary>Finds all attendee organizations not deleted.</summary>
        /// <param name="edition">The edition.</param>
        /// <returns></returns>
        private List<AttendeeOrganization> FindAllAttendeeOrganizationsNotDeleted(Edition edition)
        {
            return this.AttendeeOrganizations?.Where(ao => (edition == null || ao.EditionId == edition.Id) && !ao.IsDeleted)?.ToList();
        }

        #endregion

        #region Organization Activities

        /// <summary>Synchronizes the organization activities.</summary>
        /// <param name="activities">The activities.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeOrganizationActivities(List<Activity> activities, int userId)
        {
            if (this.OrganizationActivities == null)
            {
                this.OrganizationActivities = new List<OrganizationActivity>();
            }

            this.DeleteOrganizationActivities(activities, userId);

            if (activities?.Any() != true)
            {
                return;
            }

            // Create or update activities
            foreach (var activity in activities)
            {
                var organizationActivityDb = this.OrganizationActivities.FirstOrDefault(a => a.Activity.Uid == activity.Uid);
                if (organizationActivityDb != null)
                {
                    organizationActivityDb.Update(userId);
                }
                else
                {
                    this.CreateOrganizationActivity(activity, userId);
                }
            }
        }

        /// <summary>Deletes the organization activities.</summary>
        /// <param name="newActivities">The new activities.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteOrganizationActivities(List<Activity> newActivities, int userId)
        {
            var activitiesToDelete = this.OrganizationActivities.Where(db => newActivities?.Select(a => a.Uid)?.Contains(db.Activity.Uid) == false).ToList();
            foreach (var activityToDelete in activitiesToDelete)
            {
                activityToDelete.Delete(userId);
            }
        }

        private void CreateOrganizationActivity(Activity activity, int userId)
        {
            this.OrganizationActivities.Add(new OrganizationActivity(this, activity, userId));
        }

        #endregion

        #region Organization Target Audiences

        /// <summary>Synchronizes the organization target audiences.</summary>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeOrganizationTargetAudiences(List<TargetAudience> targetAudiences, int userId)
        {
            if (this.OrganizationTargetAudiences == null)
            {
                this.OrganizationTargetAudiences = new List<OrganizationTargetAudience>();
            }

            this.DeleteOrganizationTargetAudiences(targetAudiences, userId);

            if (targetAudiences?.Any() != true)
            {
                return;
            }

            // Create or update activities
            foreach (var targetAudience in targetAudiences)
            {
                var organizationTargetAudienceDb = this.OrganizationActivities.FirstOrDefault(a => a.Activity.Uid == targetAudience.Uid);
                if (organizationTargetAudienceDb != null)
                {
                    organizationTargetAudienceDb.Update(userId);
                }
                else
                {
                    this.CreateOrganizationTargetAudience(targetAudience, userId);
                }
            }
        }

        /// <summary>Deletes the organization target audiences.</summary>
        /// <param name="newTargetAudiences">The new target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteOrganizationTargetAudiences(List<TargetAudience> newTargetAudiences, int userId)
        {
            var organizationTargetAudiencesToDelete = this.OrganizationTargetAudiences.Where(db => newTargetAudiences?.Select(a => a.Uid)?.Contains(db.TargetAudience.Uid) == false).ToList();
            foreach (var organizatioNTargetAudienceToDelete in organizationTargetAudiencesToDelete)
            {
                organizatioNTargetAudienceToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the organization target audience.</summary>
        /// <param name="targetAudience">The target audience.</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateOrganizationTargetAudience(TargetAudience targetAudience, int userId)
        {
            this.OrganizationTargetAudiences.Add(new OrganizationTargetAudience(this, targetAudience, userId));
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateName();
            this.ValidateCompanyName();
            this.ValidateTradeName();
            this.ValidateDocument();
            this.ValidateWebsite();
            this.ValidateSocialMedia();
            this.ValidatePhoneNumber();
            this.ValidateDescriptions();
            this.ValidateAddress();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the name.</summary>
        public void ValidateName()
        {
            if (string.IsNullOrEmpty(this.Name?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "Name" }));
            }

            if (this.Name?.Trim().Length < NameMinLength || this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, NameMinLength), new string[] { "Name" }));
            }
        }

        /// <summary>Validates the name of the company.</summary>
        public void ValidateCompanyName()
        {
            if (!string.IsNullOrEmpty(this.CompanyName) && this.CompanyName?.Trim().Length > CompanyNameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.CompanyName, CompanyNameMaxLength, 1), new string[] { "CompanyName" }));
            }
        }

        /// <summary>Validates the name of the trade.</summary>
        public void ValidateTradeName()
        {
            if (!string.IsNullOrEmpty(this.TradeName) && this.TradeName?.Trim().Length > TradeNameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.TradeName, TradeNameMaxLength, 1), new string[] { "TradeName" }));
            }
        }

        /// <summary>Validates the document.</summary>
        public void ValidateDocument()
        {
            if (!string.IsNullOrEmpty(this.Document) && this.Document?.Trim().Length > DocumentMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.CompanyDocument, DocumentMaxLength, 1), new string[] { "CompanyDocument" }));
            }
        }

        /// <summary>Validates the website.</summary>
        public void ValidateWebsite()
        {
            if (!string.IsNullOrEmpty(this.Website) && this.Website?.Trim().Length > WebsiteMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Website, WebsiteMaxLength, 1), new string[] { "Website" }));
            }
        }

        /// <summary>Validates the social media.</summary>
        public void ValidateSocialMedia()
        {
            if (!string.IsNullOrEmpty(this.SocialMedia) && this.SocialMedia?.Trim().Length > SocialMediaMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.SocialMedia, SocialMediaMaxLength, 1), new string[] { "SocialMedia" }));
            }
        }

        /// <summary>Validates the phone number.</summary>
        public void ValidatePhoneNumber()
        {
            if (!string.IsNullOrEmpty(this.PhoneNumber) && this.PhoneNumber?.Trim().Length > PhoneNumberMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.PhoneNumber, PhoneNumberMaxLength, 1), new string[] { "PhoneNumber" }));
            }
        }

        /// <summary>Validates the descriptions.</summary>
        public void ValidateDescriptions()
        {
            foreach (var description in this.Descriptions?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(description.ValidationResult);
            }
        }

        /// <summary>Validates the address.</summary>
        public void ValidateAddress()
        {
            if (this.Address != null && !this.Address.IsValid())
            {
                this.ValidationResult.Add(this.Address.ValidationResult);
            }
        }

        #endregion
    }
}