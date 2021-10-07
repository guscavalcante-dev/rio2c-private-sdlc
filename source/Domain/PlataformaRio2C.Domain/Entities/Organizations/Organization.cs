// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-19-2021
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
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

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
        public static readonly int PhoneNumberMaxLength = 50;
        public static readonly int WebsiteMaxLength = 300;
        public static readonly int LinkedinMaxLength = 100;
        public static readonly int TwitterMaxLength = 100;
        public static readonly int InstagramMaxLength = 100;
        public static readonly int YoutubeMaxLength = 300;

        public int? HoldingId { get; private set; }
        public string Name { get; private set; }
        public string CompanyName { get; private set; }
        public string TradeName { get; private set; }
        public string Document { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Website { get; private set; }
        public string Linkedin { get; private set; }
        public string Twitter { get; private set; }
        public string Instagram { get; private set; }
        public string Youtube { get; private set; }
        public int? AddressId { get; private set; }
        public DateTimeOffset? ImageUploadDate { get; private set; }
        
        public virtual Holding Holding { get; private set; }
        public virtual Address Address { get; private set; }
        public virtual User Updater { get; private set; }

        public virtual ICollection<AttendeeOrganization> AttendeeOrganizations { get; private set; }
        public virtual ICollection<OrganizationDescription> OrganizationDescriptions { get; private set; }
        public virtual ICollection<OrganizationRestrictionSpecific> OrganizationRestrictionSpecifics { get; private set; }
        public virtual ICollection<OrganizationActivity> OrganizationActivities { get; private set; }
        public virtual ICollection<OrganizationTargetAudience> OrganizationTargetAudiences { get; private set; }
        public virtual ICollection<OrganizationInterest> OrganizationInterests { get; private set; }

        //public virtual ICollection<PlayerInterest> Interests { get; private set; }
        //public virtual ICollection<Collaborator> Collaborators { get; private set; }
        //public virtual ICollection<Collaborator> CollaboratorsOld { get; private set; }
        //public virtual ICollection<PlayerActivity> PlayerActivitys { get; private set; }
        //public virtual ICollection<PlayerTargetAudience> PlayerTargetAudience { get; private set; }
        //public virtual ICollection<PlayerRestrictionsSpecifics> RestrictionsSpecifics { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Organization" /> class for admin.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <param name="holding">The holding.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="name">The name.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <param name="document">The document.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="addressZipCode">The address zip code.</param>
        /// <param name="addressIsManual">if set to <c>true</c> [address is manual].</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isVirtualMeeting">if set to <c>true</c> [is virtual meeting].</param>
        /// <param name="organizationDescriptions">The organization descriptions.</param>
        /// <param name="organizationRestrictionSpecifics">The organization restriction specifics.</param>
        /// <param name="organizationActivities">The organization activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="organizationInterests">The organization interests.</param>
        /// <param name="userId">The user identifier.</param>
        public Organization(
            Guid uid, 
            Holding holding, 
            Edition edition,
            OrganizationType organizationType,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            string name, 
            string companyName, 
            string tradeName, 
            string document,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            string phoneNumber, 
            Country country,
            Guid? stateUid,
            string stateName,
            Guid? cityUid,
            string cityName,
            string address1,
            string addressZipCode,
            bool addressIsManual,
            bool isImageUploaded,
            bool? isVirtualMeeting,
            List<OrganizationDescription> organizationDescriptions,
            List<OrganizationRestrictionSpecific> organizationRestrictionSpecifics,
            List<OrganizationActivity> organizationActivities,
            List<TargetAudience> targetAudiences,
            List<OrganizationInterest> organizationInterests,
            int userId)
        {
            //this.Uid = uid;
            this.Holding = holding;
            this.HoldingId = holding?.Id;
            this.Name = name?.Trim();
            this.CompanyName = companyName?.Trim();
            this.TradeName = tradeName?.Trim();
            this.Document = document?.Trim();
            this.UpdateSocialNetworks(website, linkedin, twitter, instagram, youtube, userId);
            this.PhoneNumber = phoneNumber?.Trim();
            this.UpdateImageUploadDate(isImageUploaded, false);
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeOrganizationDescriptions(organizationDescriptions, userId);
            this.SynchronizeOrganizationRestrictionSpecifics(organizationRestrictionSpecifics, userId);
            this.SynchronizeAttendeeOrganizations(edition, organizationType, isApiDisplayEnabled, apiHighlightPosition, null, true, isVirtualMeeting, userId);
            this.UpdateAddress(country, stateUid, stateName, cityUid, cityName, address1, addressZipCode, addressIsManual, userId);
            this.SynchronizeOrganizationActivities(organizationActivities, userId);
            this.SynchronizeOrganizationTargetAudiences(targetAudiences, userId);
            this.SynchronizeOrganizationInterests(organizationInterests, userId);
        }

        /// <summary>Initializes a new instance of the <see cref="Organization"/> class for ticket buyer onboarding.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <param name="document">The document.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="addressZipCode">The address zip code.</param>
        /// <param name="addressIsManual">if set to <c>true</c> [address is manual].</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="organizationDscriptions">The organization dscriptions.</param>
        /// <param name="userId">The user identifier.</param>
        public Organization(
            Edition edition,
            AttendeeCollaborator attendeeCollaborator,
            string companyName,
            string tradeName,
            string document,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            Country country,
            Guid? stateUid,
            string stateName,
            Guid? cityUid,
            string cityName,
            string address1,
            string addressZipCode,
            bool addressIsManual,
            bool isImageUploaded,
            bool? isVirtualMeeting,
            List<OrganizationDescription> organizationDscriptions,
            int userId)
        {
            this.Name = tradeName?.Trim();
            this.TradeName = tradeName?.Trim();
            this.CompanyName = companyName?.Trim();
            this.Document = document?.Trim();
            this.UpdateSocialNetworks(website, linkedin, twitter, instagram, youtube, userId);
            this.UpdateImageUploadDate(isImageUploaded, false);
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeOrganizationDescriptions(organizationDscriptions, userId);
            this.SynchronizeAttendeeOrganizations(edition, null, false, null, attendeeCollaborator, true, isVirtualMeeting, userId);
            this.UpdateAddress(country, stateUid, stateName, cityUid, cityName, address1, addressZipCode, addressIsManual, userId);
            this.OnboardTicketBuyerAttendeeOrganizationData(edition, userId);
        }

        /// <summary>Initializes a new instance of the <see cref="Organization"/> class for producer onboarding.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <param name="document">The document.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="addressZipCode">The address zip code.</param>
        /// <param name="addressIsManual">if set to <c>true</c> [address is manual].</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="organizationDescriptions">The organization descriptions.</param>
        /// <param name="organizationActivities">The organization activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        public Organization(
            Edition edition,
            AttendeeCollaborator attendeeCollaborator,
            string companyName,
            string tradeName,
            string document,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            Country country,
            Guid? stateUid,
            string stateName,
            Guid? cityUid,
            string cityName,
            string address1,
            string addressZipCode,
            bool addressIsManual,
            bool isImageUploaded,
            bool? isVirtualMeeting,
            List<OrganizationDescription> organizationDescriptions,
            List<OrganizationActivity> organizationActivities,
            List<TargetAudience> targetAudiences,
            int userId)
        {
            this.Name = tradeName?.Trim();
            this.TradeName = tradeName?.Trim();
            this.CompanyName = companyName?.Trim();
            this.Document = document?.Trim();
            this.UpdateSocialNetworks(website, linkedin, twitter, instagram, youtube, userId);
            this.UpdateImageUploadDate(isImageUploaded, false);
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeOrganizationDescriptions(organizationDescriptions, userId);
            this.SynchronizeOrganizationActivities(organizationActivities, userId);
            this.SynchronizeOrganizationTargetAudiences(targetAudiences, userId);
            this.SynchronizeAttendeeOrganizations(edition, null, false, null, attendeeCollaborator, true, isVirtualMeeting, userId);
            this.UpdateAddress(country, stateUid, stateName, cityUid, cityName, address1, addressZipCode, addressIsManual, userId);
            this.OnboardProducerAttendeeOrganizationData(edition, userId);
        }

        /// <summary>Initializes a new instance of the <see cref="Organization"/> class.</summary>
        protected Organization()
        {
        }

        /// <summary>Updates the organization for admin.</summary>
        /// <param name="holding">The holding.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="name">The name.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <param name="document">The document.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="addressZipCode">The address zip code.</param>
        /// <param name="addressIsManual">if set to <c>true</c> [address is manual].</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        /// <param name="organizationDscriptions">The organization dscriptions.</param>
        /// <param name="organizationRestrictionSpecifics">The organization restriction specifics.</param>
        /// <param name="organizationActivities">The organization activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="organizationIterests">The organization iterests.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        public void Update (
            Holding holding,
            Edition edition,
            OrganizationType organizationType,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            string name,
            string companyName,
            string tradeName,
            string document,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            string phoneNumber,
            Country country,
            Guid? stateUid,
            string stateName,
            Guid? cityUid,
            string cityName,
            string address1,
            string addressZipCode,
            bool addressIsManual,
            bool isImageUploaded,
            bool isImageDeleted,
            bool? isVirtualMeeting,
            List<OrganizationDescription> organizationDscriptions,
            List<OrganizationRestrictionSpecific> organizationRestrictionSpecifics,
            List<OrganizationActivity> organizationActivities,
            List<TargetAudience> targetAudiences,
            List<OrganizationInterest> organizationIterests,
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
            this.UpdateSocialNetworks(website, linkedin, twitter, instagram, youtube, userId);
            this.PhoneNumber = phoneNumber?.Trim();
            this.UpdateImageUploadDate(isImageUploaded, isImageDeleted);
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
            this.SynchronizeOrganizationDescriptions(organizationDscriptions, userId);
            this.SynchronizeOrganizationRestrictionSpecifics(organizationRestrictionSpecifics, userId);
            this.SynchronizeAttendeeOrganizations(edition, organizationType, isApiDisplayEnabled, apiHighlightPosition, null, isAddingToCurrentEdition, isVirtualMeeting, userId);
            this.UpdateAddress(country, stateUid, stateName, cityUid, cityName, address1, addressZipCode, addressIsManual, userId);
            this.SynchronizeOrganizationActivities(organizationActivities, userId);
            this.SynchronizeOrganizationTargetAudiences(targetAudiences, userId);
            this.SynchronizeOrganizationInterests(organizationIterests, userId);
        }


        /// <summary>
        /// Updates the admin main information.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="holding">The holding.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="name">The name.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <param name="document">The document.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        /// <param name="isVirtualMeeting">The is virtual meeting.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="organizationDescriptions">The organization descriptions.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateAdminMainInformation(
            Edition edition,
            Holding holding,
            OrganizationType organizationType,
            string name,
            string companyName,
            string tradeName,
            string document,
            bool isImageUploaded,
            bool isImageDeleted,
            bool? isVirtualMeeting,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            List<OrganizationDescription> organizationDescriptions,
            int userId)
        {
            this.Holding = holding;
            this.HoldingId = holding?.Id;

            this.Name = name?.Trim();
            this.CompanyName = companyName?.Trim();
            this.TradeName = tradeName?.Trim();
            this.Document = document?.Trim();
            this.UpdateImageUploadDate(isImageUploaded, isImageDeleted);
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
            this.SynchronizeOrganizationDescriptions(organizationDescriptions, userId);

            this.SynchronizeAttendeeOrganizations(edition, organizationType, isApiDisplayEnabled, apiHighlightPosition, null, false, isVirtualMeeting, userId);  
        }

        /// <summary>
        /// Updates the site main information.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <param name="document">The document.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        /// <param name="organizationDescriptions">The organization descriptions.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateSiteMainInformation(
            Edition edition,
            string companyName,
            string tradeName,
            string document,
            bool isImageUploaded,
            bool isImageDeleted,
            List<OrganizationDescription> organizationDescriptions,
            int userId)
        {
            this.CompanyName = companyName?.Trim();
            this.TradeName = tradeName?.Trim();
            this.Document = document?.Trim();
            this.UpdateImageUploadDate(isImageUploaded, isImageDeleted);
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
            this.SynchronizeOrganizationDescriptions(organizationDescriptions, userId);

            //this.SynchronizeAttendeeOrganizations(edition, null, null, null, null, false, null, userId);
        }

        /// <summary>Updates the social networks.</summary>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateSocialNetworks(
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            int userId)
        {
            this.Website = website?.Trim();
            this.Linkedin = linkedin?.Trim();
            this.Twitter = twitter?.Trim();
            this.Instagram = instagram?.Trim();
            this.Youtube = youtube?.Trim();

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified edition.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        public void Delete(Edition edition, OrganizationType organizationType, int userId)
        {
            this.UpdateDate = DateTime.UtcNow;
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
                this.ImageUploadDate = DateTime.UtcNow;
            }
            else if (isImageDeleted)
            {
                this.ImageUploadDate = null;
            }
        }

        /// <summary>Gets the name abbreviation.</summary>
        /// <returns></returns>
        public string GetTradeNameAbbreviation()
        {
            return this.TradeName?.GetTwoLetterCode();
        }

        /// <summary>Determines whether this instance has image.</summary>
        /// <returns>
        ///   <c>true</c> if this instance has image; otherwise, <c>false</c>.</returns>
        public bool HasImage()
        {
            return this.ImageUploadDate.HasValue;
        }

        #region Onboarding

        /// <summary>
        /// Onboard player data.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <param name="document">The document.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="addressZipCode">The address zip code.</param>
        /// <param name="addressIsManual">if set to <c>true</c> [address is manual].</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        /// <param name="isVirtualMeeting">if set to <c>true</c> [is virtual meeting].</param>
        /// <param name="organizationDescriptions">The organization descriptions.</param>
        /// <param name="organizationActivities">The organization activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardPlayerData(
            Edition edition,
            OrganizationType organizationType,
            string companyName,
            string tradeName,
            string document,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            Country country,
            Guid? stateUid,
            string stateName,
            Guid? cityUid,
            string cityName,
            string address1,
            string addressZipCode,
            bool addressIsManual,
            bool isImageUploaded,
            bool isImageDeleted,
            bool? isVirtualMeeting,
            List<OrganizationDescription> organizationDescriptions,
            List<OrganizationActivity> organizationActivities,
            List<TargetAudience> targetAudiences,
            int userId)
        {
            this.CompanyName = companyName?.Trim();
            this.TradeName = tradeName?.Trim();
            this.Document = document?.Trim();
            this.UpdateSocialNetworks(website, linkedin, twitter, instagram, youtube, userId);
            this.UpdateImageUploadDate(isImageUploaded, isImageDeleted);
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
            this.SynchronizeOrganizationDescriptions(organizationDescriptions, userId);
            this.SynchronizeOrganizationActivities(organizationActivities, userId);
            this.SynchronizeOrganizationTargetAudiences(targetAudiences, userId);
            this.SynchronizeAttendeeOrganizations(edition, organizationType, null, null, null, true, isVirtualMeeting, userId);
            this.UpdateAddress(country, stateUid, stateName, cityUid, cityName, address1, addressZipCode, addressIsManual, userId);
            this.OnboardPlayerAttendeeOrganizationData(edition, userId);
        }

        /// <summary>Called when [interests].</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="organizationRestrictionSpecifics">The organization restriction specifics.</param>
        /// <param name="organizationInterests">The organization interests.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardInterests(
            Edition edition,
            OrganizationType organizationType,
            List<OrganizationRestrictionSpecific> organizationRestrictionSpecifics,
            List<OrganizationInterest> organizationInterests,
            int userId)
        {
            this.SynchronizeOrganizationRestrictionSpecifics(organizationRestrictionSpecifics, userId);
            this.SynchronizeOrganizationInterests(organizationInterests, userId);
            this.OnboardAttendeeOrganizationInterests(edition, userId);
        }

        /// <summary>
        /// Called when [ticket buyer company data] for ticket buyer onboarding.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <param name="document">The document.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="addressZipCode">The address zip code.</param>
        /// <param name="addressIsManual">if set to <c>true</c> [address is manual].</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        /// <param name="isVirtualMeeting">if set to <c>true</c> [is virtual meeting].</param>
        /// <param name="organizationDescriptions">The organization descriptions.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardTicketBuyerCompanyData(
            Edition edition,
            AttendeeCollaborator attendeeCollaborator,
            string companyName,
            string tradeName,
            string document,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            Country country,
            Guid? stateUid,
            string stateName,
            Guid? cityUid,
            string cityName,
            string address1,
            string addressZipCode,
            bool addressIsManual,
            bool isImageUploaded,
            bool isImageDeleted,
            bool? isVirtualMeeting,
            List<OrganizationDescription> organizationDescriptions,
            int userId)
        {
            this.CompanyName = companyName?.Trim();
            this.TradeName = tradeName?.Trim();
            this.Document = document?.Trim();
            this.UpdateSocialNetworks(website, linkedin, twitter, instagram, youtube, userId);
            this.UpdateImageUploadDate(isImageUploaded, isImageDeleted);
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
            this.SynchronizeOrganizationDescriptions(organizationDescriptions, userId);
            this.SynchronizeAttendeeOrganizations(edition, null, null, null, attendeeCollaborator, true, isVirtualMeeting, userId);
            this.UpdateAddress(country, stateUid, stateName, cityUid, cityName, address1, addressZipCode, addressIsManual, userId);
            this.OnboardTicketBuyerAttendeeOrganizationData(edition, userId);
        }

        /// <summary>
        /// Onboard producer data.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <param name="document">The document.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="addressZipCode">The address zip code.</param>
        /// <param name="addressIsManual">if set to <c>true</c> [address is manual].</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        /// <param name="isVirtualMeeting">if set to <c>true</c> [is virtual meeting].</param>
        /// <param name="organizationDescriptions">The organization descriptions.</param>
        /// <param name="organizationActivities">The organization activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardProducerData(
            Edition edition,
            AttendeeCollaborator attendeeCollaborator,
            string companyName,
            string tradeName,
            string document,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            Country country,
            Guid? stateUid,
            string stateName,
            Guid? cityUid,
            string cityName,
            string address1,
            string addressZipCode,
            bool addressIsManual,
            bool isImageUploaded,
            bool isImageDeleted,
            bool? isVirtualMeeting,
            List<OrganizationDescription> organizationDescriptions,
            List<OrganizationActivity> organizationActivities,
            List<TargetAudience> targetAudiences,
            int userId)
        {
            this.CompanyName = companyName?.Trim();
            this.TradeName = tradeName?.Trim();
            this.Document = document?.Trim();
            this.UpdateSocialNetworks(website, linkedin, twitter, instagram, youtube, userId);
            this.UpdateImageUploadDate(isImageUploaded, isImageDeleted);
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
            this.SynchronizeOrganizationDescriptions(organizationDescriptions, userId);
            this.SynchronizeOrganizationActivities(organizationActivities, userId);
            this.SynchronizeOrganizationTargetAudiences(targetAudiences, userId);
            this.SynchronizeAttendeeOrganizations(edition, null, null, null, attendeeCollaborator, true, isVirtualMeeting, userId);
            this.UpdateAddress(country, stateUid, stateName, cityUid, cityName, address1, addressZipCode, addressIsManual, userId);
            this.OnboardProducerAttendeeOrganizationData(edition, userId);
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
            string addressZipCode,
            bool addressIsManual,
            int userId)
        {
            if (this.Address == null)
            {
                this.Address = new Address(
                    country, 
                    stateUid, 
                    stateName, 
                    cityUid, 
                    cityName, 
                    address1,
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
                    addressZipCode,
                    addressIsManual, 
                    userId);
            }
        }

        #endregion

        #region Descriptions

        /// <summary>Synchronizes the organization descriptions.</summary>
        /// <param name="organizationDescriptions">The organization descriptions.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeOrganizationDescriptions(List<OrganizationDescription> organizationDescriptions, int userId)
        {
            if (this.OrganizationDescriptions == null)
            {
                this.OrganizationDescriptions = new List<OrganizationDescription>();
            }

            this.DeleteOrganizationDescriptions(organizationDescriptions, userId);

            if (organizationDescriptions?.Any() != true)
            {
                return;
            }

            // Create or update descriptions
            foreach (var description in organizationDescriptions)
            {
                var descriptionDb = this.OrganizationDescriptions.FirstOrDefault(d => d.Language.Code == description.Language.Code);
                if (descriptionDb != null)
                {
                    descriptionDb.Update(description);
                }
                else
                {
                    this.CreateOrganizationDescription(description);
                }
            }
        }

        /// <summary>Deletes the organization descriptions.</summary>
        /// <param name="newOrganizationDescriptions">The new organization descriptions.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteOrganizationDescriptions(List<OrganizationDescription> newOrganizationDescriptions, int userId)
        {
            var descriptionsToDelete = this.OrganizationDescriptions.Where(db => newOrganizationDescriptions?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var descriptionToDelete in descriptionsToDelete)
            {
                descriptionToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the organization description.</summary>
        /// <param name="organiationDescription">The organiation description.</param>
        private void CreateOrganizationDescription(OrganizationDescription organiationDescription)
        {
            this.OrganizationDescriptions.Add(organiationDescription);
        }

        #endregion

        #region Restriction Specifics

        /// <summary>Updates the organization restriction specifics.</summary>
        /// <param name="organizationRstrictionSpecifics">The organization rstriction specifics.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateOrganizationRestrictionSpecifics(List<OrganizationRestrictionSpecific> organizationRstrictionSpecifics, int userId)
        {
            this.SynchronizeOrganizationRestrictionSpecifics(organizationRstrictionSpecifics, userId);

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Synchronizes the organization restriction specifics.</summary>
        /// <param name="organizationRstrictionSpecifics">The organization rstriction specifics.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeOrganizationRestrictionSpecifics(List<OrganizationRestrictionSpecific> organizationRstrictionSpecifics, int userId)
        {
            if (this.OrganizationRestrictionSpecifics == null)
            {
                this.OrganizationRestrictionSpecifics = new List<OrganizationRestrictionSpecific>();
            }

            this.DeleteOrganizationRestrictionSpecifics(organizationRstrictionSpecifics, userId);

            if (organizationRstrictionSpecifics?.Any() != true)
            {
                return;
            }

            // Create or update restriction specifics
            foreach (var restrictionSpecific in organizationRstrictionSpecifics)
            {
                var restrictionSpecificDb = this.OrganizationRestrictionSpecifics.FirstOrDefault(d => d.Language.Code == restrictionSpecific.Language.Code);
                if (restrictionSpecificDb != null)
                {
                    restrictionSpecificDb.Update(restrictionSpecific);
                }
                else
                {
                    this.CreateOrganizationRestrictionSpecific(restrictionSpecific);
                }
            }
        }

        /// <summary>Deletes the organization restriction specifics.</summary>
        /// <param name="newOrganizationRestrictionSpecifics">The new organization restriction specifics.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteOrganizationRestrictionSpecifics(List<OrganizationRestrictionSpecific> newOrganizationRestrictionSpecifics, int userId)
        {
            var restrictionSpecificsToDelete = this.OrganizationRestrictionSpecifics.Where(db => newOrganizationRestrictionSpecifics?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var restrictionSpecificToDelete in restrictionSpecificsToDelete)
            {
                restrictionSpecificToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the organization restriction specific.</summary>
        /// <param name="organizationRestrictionSpecific">The organization restriction specific.</param>
        private void CreateOrganizationRestrictionSpecific(OrganizationRestrictionSpecific organizationRestrictionSpecific)
        {
            this.OrganizationRestrictionSpecifics.Add(organizationRestrictionSpecific);
        }

        #endregion

        #region Attendee Organizations

        /// <summary>
        /// Updates the API configuration.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="isApiDisplayEnabled">if set to <c>true</c> [is API display enabled].</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateApiConfiguration(
            Edition edition,
            OrganizationType organizationType,
            bool isApiDisplayEnabled,
            int? apiHighlightPosition,
            int userId)
        {
            var attendeeOrganization = this.GetAttendeeOrganizationByEditionId(edition?.Id ?? 0);
            attendeeOrganization?.UpdateApiConfiguration(organizationType, isApiDisplayEnabled, apiHighlightPosition, userId);

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the attendee organization type highlight position.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteApiHighlightPosition(Edition edition, OrganizationType organizationType, int userId)
        {
            var attendeeOrganization = this.GetAttendeeOrganizationByEditionId(edition?.Id ?? 0);
            attendeeOrganization?.DeleteApiHighlightPosition(organizationType, userId);
        }

        /// <summary>Called when [player attendee organization data].</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardPlayerAttendeeOrganizationData(Edition edition, int userId)
        {
            var attendeeOrganization = this.GetAttendeeOrganizationByEditionId(edition.Id);
            attendeeOrganization?.OnboardPlayer(userId);
        }

        /// <summary>Called when [attendee organization interests].</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardAttendeeOrganizationInterests(Edition edition, int userId)
        {
            var attendeeOrganization = this.GetAttendeeOrganizationByEditionId(edition.Id);
            attendeeOrganization?.OnboardInterests(userId);
        }

        /// <summary>Called when [ticket buyer attendee organization data].</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardTicketBuyerAttendeeOrganizationData(Edition edition, int userId)
        {
            var attendeeOrganization = this.GetAttendeeOrganizationByEditionId(edition.Id);
            attendeeOrganization?.OnboardTIcketBuyer(userId);
        }

        /// <summary>Called when [producer attendee organization data].</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardProducerAttendeeOrganizationData(Edition edition, int userId)
        {
            var attendeeOrganization = this.GetAttendeeOrganizationByEditionId(edition.Id);
            attendeeOrganization?.OnboardProducer(userId);
        }

        /// <summary>
        /// Synchronizes the attendee organizations.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="isVirtualMeeting">The is virtual meeting.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeOrganizations(
            Edition edition, 
            OrganizationType organizationType,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            AttendeeCollaborator attendeeCollaborator, 
            bool isAddingToCurrentEdition, 
            bool? isVirtualMeeting,
            int userId)
        {
            //// Synchronize only when is adding to current edition
            //if (!isAddingToCurrentEdition)
            //{
            //    return;
            //}

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
                attendeeOrganization.Restore(organizationType, isApiDisplayEnabled, apiHighlightPosition, isVirtualMeeting, userId);
                attendeeCollaborator?.SynchronizeAttendeeOrganizationCollaborators(new List<AttendeeOrganization> { attendeeOrganization }, false, userId);
            }
            else
            {
                var newAttendeeOrganization = new AttendeeOrganization(edition, this, organizationType, isApiDisplayEnabled, apiHighlightPosition, isVirtualMeeting, userId);
                this.AttendeeOrganizations.Add(newAttendeeOrganization);
                attendeeCollaborator?.SynchronizeAttendeeOrganizationCollaborators(new List<AttendeeOrganization> { newAttendeeOrganization }, false, userId);
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
            return this.AttendeeOrganizations?.FirstOrDefault(ao => ao.Edition.Id == editionId);
        }

        /// <summary>Finds all attendee organizations not deleted.</summary>
        /// <param name="edition">The edition.</param>
        /// <returns></returns>
        private List<AttendeeOrganization> FindAllAttendeeOrganizationsNotDeleted(Edition edition)
        {
            return this.AttendeeOrganizations?.Where(ao => (edition == null || ao.EditionId == edition.Id) && !ao.IsDeleted)?.ToList();
        }

        #endregion

        #region Activities

        /// <summary>Updates the organization activities.</summary>
        /// <param name="organizationActivities">The organization activities.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateOrganizationActivities(List<OrganizationActivity> organizationActivities, int userId)
        {
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
            this.SynchronizeOrganizationActivities(organizationActivities, userId);
        }

        /// <summary>Synchronizes the organization activities.</summary>
        /// <param name="organizationActivities">The organization activities.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeOrganizationActivities(List<OrganizationActivity> organizationActivities, int userId)
        {
            if (this.OrganizationActivities == null)
            {
                this.OrganizationActivities = new List<OrganizationActivity>();
            }

            this.DeleteOrganizationActivities(organizationActivities, userId);

            if (organizationActivities?.Any() != true)
            {
                return;
            }

            // Create or update activities
            foreach (var organizationActivity in organizationActivities)
            {
                var organizationActivityDb = this.OrganizationActivities.FirstOrDefault(a => a.Activity.Uid == organizationActivity.Activity.Uid);
                if (organizationActivityDb != null)
                {
                    organizationActivityDb.Update(organizationActivity.AdditionalInfo, userId);
                }
                else
                {
                    this.CreateOrganizationActivity(organizationActivity.Activity, organizationActivity.AdditionalInfo, userId);
                }
            }
        }

        /// <summary>Deletes the organization activities.</summary>
        /// <param name="newOrganizationActivities">The new organization activities.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteOrganizationActivities(List<OrganizationActivity> newOrganizationActivities, int userId)
        {
            var activitiesToDelete = this.OrganizationActivities.Where(db => newOrganizationActivities?.Select(oa => oa.Activity.Uid)?.Contains(db.Activity.Uid) == false && !db.IsDeleted).ToList();
            foreach (var activityToDelete in activitiesToDelete)
            {
                activityToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the organization activity.</summary>
        /// <param name="activity">The activity.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateOrganizationActivity(Activity activity, string additionalInfo, int userId)
        {
            this.OrganizationActivities.Add(new OrganizationActivity(this, activity, additionalInfo, userId));
        }

        #endregion

        #region Target Audiences

        /// <summary>Updates the organization target audiences.</summary>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateOrganizationTargetAudiences(List<TargetAudience> targetAudiences, int userId)
        {
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
            this.SynchronizeOrganizationTargetAudiences(targetAudiences, userId);
        }

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

            // Create or update target audiences
            foreach (var targetAudience in targetAudiences)
            {
                var organizationTargetAudienceDb = this.OrganizationTargetAudiences.FirstOrDefault(a => a.TargetAudience.Uid == targetAudience.Uid);
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
            var organizationTargetAudiencesToDelete = this.OrganizationTargetAudiences.Where(db => newTargetAudiences?.Select(a => a.Uid)?.Contains(db.TargetAudience.Uid) == false && !db.IsDeleted).ToList();
            foreach (var organizationTargetAudienceToDelete in organizationTargetAudiencesToDelete)
            {
                organizationTargetAudienceToDelete.Delete(userId);
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

        #region Interests

        /// <summary>Updates the organization interests.</summary>
        /// <param name="organizationInterests">The organization interests.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateOrganizationInterests(List<OrganizationInterest> organizationInterests, int userId)
        {
            this.SynchronizeOrganizationInterests(organizationInterests, userId);

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Synchronizes the organization interests.</summary>
        /// <param name="organizationInterests">The organization interests.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeOrganizationInterests(List<OrganizationInterest> organizationInterests, int userId)
        {
            if (this.OrganizationInterests == null)
            {
                this.OrganizationInterests = new List<OrganizationInterest>();
            }

            this.DeleteOrganizationInterests(organizationInterests, userId);

            if (organizationInterests?.Any() != true)
            {
                return;
            }

            // Create or update interests
            foreach (var organizationInterest in organizationInterests)
            {
                var interestDb = this.OrganizationInterests.FirstOrDefault(a => a.Interest.Uid == organizationInterest.Interest.Uid);
                if (interestDb != null)
                {
                    interestDb.Update(organizationInterest, userId);
                }
                else
                {
                    this.OrganizationInterests.Add(organizationInterest);
                }
            }
        }

        private void DeleteOrganizationInterests(List<OrganizationInterest> newOrganizationInterests, int userId)
        {
            var organizationInterestsToDelete = this.OrganizationInterests.Where(db => newOrganizationInterests?.Select(a => a.Interest.Uid)?.Contains(db.Interest.Uid) == false && !db.IsDeleted).ToList();
            foreach (var organizationInterestToDelete in organizationInterestsToDelete)
            {
                organizationInterestToDelete.Delete(userId);
            }
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
            this.ValidateLinkedin();
            this.ValidateTwitter();
            this.ValidateInstagram();
            this.ValidateYoutube();
            this.ValidatePhoneNumber();
            this.ValidateDescriptions();
            this.ValidateRestrictionSpecifics();
            this.ValidateInterests();
            this.ValidateActivities();
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

        /// <summary>Validates the linkedin.</summary>
        public void ValidateLinkedin()
        {
            if (!string.IsNullOrEmpty(this.Linkedin) && this.Linkedin?.Trim().Length > LinkedinMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Linkedin", LinkedinMaxLength, 1), new string[] { "Linkedin" }));
            }
        }

        /// <summary>Validates the twitter.</summary>
        public void ValidateTwitter()
        {
            if (!string.IsNullOrEmpty(this.Twitter) && this.Twitter?.Trim().Length > TwitterMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Twitter", TwitterMaxLength, 1), new string[] { "Twitter" }));
            }
        }

        /// <summary>Validates the instagram.</summary>
        public void ValidateInstagram()
        {
            if (!string.IsNullOrEmpty(this.Instagram) && this.Instagram?.Trim().Length > InstagramMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Instagram", InstagramMaxLength, 1), new string[] { "Instagram" }));
            }
        }

        /// <summary>Validates the youtube.</summary>
        public void ValidateYoutube()
        {
            if (!string.IsNullOrEmpty(this.Youtube) && this.Youtube?.Trim().Length > YoutubeMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Youtube", YoutubeMaxLength, 1), new string[] { "Youtube" }));
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
            if (this.OrganizationDescriptions?.Any() != true)
            {
                return;
            }

            foreach (var description in this.OrganizationDescriptions?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(description.ValidationResult);
            }
        }

        /// <summary>Validates the restriction specifics.</summary>
        public void ValidateRestrictionSpecifics()
        {
            if (this.OrganizationRestrictionSpecifics?.Any() != true)
            {
                return;
            }

            foreach (var restrictionSpecific in this.OrganizationRestrictionSpecifics?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(restrictionSpecific.ValidationResult);
            }
        }

        /// <summary>Validates the interests.</summary>
        public void ValidateInterests()
        {
            if (this.OrganizationRestrictionSpecifics?.Any() != true)
            {
                return;
            }

            foreach (var interest in this.OrganizationInterests?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(interest.ValidationResult);
            }
        }

        /// <summary>Validates the activities.</summary>
        public void ValidateActivities()
        {
            if (this.OrganizationRestrictionSpecifics?.Any() != true)
            {
                return;
            }

            foreach (var activity in this.OrganizationActivities?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(activity.ValidationResult);
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