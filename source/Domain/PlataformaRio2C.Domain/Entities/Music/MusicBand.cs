// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="MusicBand.cs" company="Softo">
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
    /// <summary>MusicBand</summary>
    public class MusicBand : AggregateRoot
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 300;
        public static readonly int ImageUrlMaxLength = 300;
        public static readonly int FormationDateMaxLength = 300;
        public static readonly int MainMusicInfluencesMaxLength = 600;
        public static readonly int FacebookMaxLength = 300;
        public static readonly int InstagramMaxLength = 300;
        public static readonly int TwitterMaxLength = 300;
        public static readonly int YoutubeMaxLength = 300;

        public int MusicBandTypeId { get; private set; }
        public string Name { get; private set; }
        public string ImageUrl { get; private set; }
        public string FormationDate { get; private set; }
        public string MainMusicInfluences { get; private set; }
        public string Facebook { get; private set; }
        public string Instagram { get; private set; }
        public string Twitter { get; private set; }
        public string Youtube { get; private set; }

        public virtual MusicBandType MusicBandType { get; private set; }
        //public virtual Address Address { get; private set; }
        //public virtual User Updater { get; private set; }

        public virtual ICollection<AttendeeMusicBand> AttendeeMusicBands { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBand"/> class.</summary>
        protected MusicBand()
        {
        }

        /// <summary>Updates the main information.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="name">The name.</param>
        /// <param name="formationDate">The formation date.</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="mainMusicInfluences">The main music influences.</param>
        /// <param name="release">The release.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(
            Edition edition,
            string name,
            string formationDate,
            string imageUrl,
            string mainMusicInfluences,
            string release,
            bool isImageUploaded,
            bool isImageDeleted,
            int userId)
        {
            this.Name = name?.Trim();
            this.FormationDate = formationDate?.Trim();
            this.ImageUrl = imageUrl?.Trim();
            this.MainMusicInfluences = mainMusicInfluences?.Trim();

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Updates the social networks.</summary>
        /// <param name="facebook">The facebook.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateSocialNetworks(
            string facebook,
            string twitter,
            string instagram,
            string youtube,
            int userId)
        {
            this.Facebook = facebook?.Trim();
            this.Instagram = instagram?.Trim();
            this.Twitter = twitter?.Trim();
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
            this.DeleteAttendeeMusicBand(edition, userId);

            if (this.FindAllAttendeeMusicBandsNotDeleted(edition)?.Any() == false)
            {
                this.IsDeleted = true;
                this.ImageUrl = null;
            }
        }

        /// <summary>Gets the name abbreviation.</summary>
        /// <returns></returns>
        public string GetNameAbbreviation()
        {
            return this.Name?.GetTwoLetterCode();
        }

        /// <summary>Determines whether this instance has image.</summary>
        /// <returns>
        ///   <c>true</c> if this instance has image; otherwise, <c>false</c>.</returns>
        public bool HasImage()
        {
            return !string.IsNullOrEmpty(this.ImageUrl);
        }

        //#region Address

        ///// <summary>Updates the address.</summary>
        ///// <param name="country">The country.</param>
        ///// <param name="stateUid">The state uid.</param>
        ///// <param name="stateName">Name of the state.</param>
        ///// <param name="cityUid">The city uid.</param>
        ///// <param name="cityName">Name of the city.</param>
        ///// <param name="address1">The address1.</param>
        ///// <param name="addressZipCode">The address zip code.</param>
        ///// <param name="addressIsManual">if set to <c>true</c> [address is manual].</param>
        ///// <param name="userId">The user identifier.</param>
        //public void UpdateAddress(
        //    Country country,
        //    Guid? stateUid,
        //    string stateName,
        //    Guid? cityUid,
        //    string cityName,
        //    string address1,
        //    string addressZipCode,
        //    bool addressIsManual,
        //    int userId)
        //{
        //    if (this.Address == null)
        //    {
        //        this.Address = new Address(
        //            country, 
        //            stateUid, 
        //            stateName, 
        //            cityUid, 
        //            cityName, 
        //            address1,
        //            addressZipCode, 
        //            addressIsManual, 
        //            userId);
        //    }
        //    else
        //    {
        //        this.Address.Update(
        //            country, 
        //            stateUid, 
        //            stateName, 
        //            cityUid, 
        //            cityName,
        //            address1,
        //            addressZipCode,
        //            addressIsManual, 
        //            userId);
        //    }
        //}

        //#endregion

        #region Attendee Music Bands

        /// <summary>Synchronizes the attendee music bands.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeMusicBands(
            Edition edition,
            AttendeeCollaborator attendeeCollaborator,
            bool isAddingToCurrentEdition,
            int userId)
        {
            //// Synchronize only when is adding to current edition
            //if (!isAddingToCurrentEdition)
            //{
            //    return;
            //}

            if (this.AttendeeMusicBands == null)
            {
                this.AttendeeMusicBands = new List<AttendeeMusicBand>();
            }

            if (edition == null)
            {
                return;
            }

            var attendeeMusicBand = this.AttendeeMusicBands.FirstOrDefault(ao => ao.EditionId == edition.Id);
            if (attendeeMusicBand != null)
            {
                attendeeMusicBand.Restore(userId);
                attendeeCollaborator?.SynchronizeAttendeeMusicBandCollaborators(new List<AttendeeMusicBand> { attendeeMusicBand }, false, userId);
            }
            else
            {
                var newAttendeeMusicBand = new AttendeeMusicBand(edition, this, userId);
                this.AttendeeMusicBands.Add(newAttendeeMusicBand);
                attendeeCollaborator?.SynchronizeAttendeeMusicBandCollaborators(new List<AttendeeMusicBand> { newAttendeeMusicBand }, false, userId);
            }
        }

        /// <summary>Deletes the attendee music band.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeMusicBand(Edition edition, int userId)
        {
            foreach (var attendeeMusicBand in this.FindAllAttendeeMusicBandsNotDeleted(edition))
            {
                attendeeMusicBand?.Delete(userId);
            }
        }

        /// <summary>Gets the attendee organization by edition identifier.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        private AttendeeMusicBand GetAttendeeOrganizationByEditionId(int editionId)
        {
            return this.AttendeeMusicBands?.FirstOrDefault(amb => amb.Edition.Id == editionId);
        }

        /// <summary>Finds all attendee music bands not deleted.</summary>
        /// <param name="edition">The edition.</param>
        /// <returns></returns>
        private List<AttendeeMusicBand> FindAllAttendeeMusicBandsNotDeleted(Edition edition)
        {
            return this.AttendeeMusicBands?.Where(amb => (edition == null || amb.EditionId == edition.Id) && !amb.IsDeleted)?.ToList();
        }

        #endregion

        //#region Target Audiences

        ///// <summary>Updates the organization target audiences.</summary>
        ///// <param name="targetAudiences">The target audiences.</param>
        ///// <param name="userId">The user identifier.</param>
        //public void UpdateOrganizationTargetAudiences(List<TargetAudience> targetAudiences, int userId)
        //{
        //    this.UpdateDate = DateTime.UtcNow;
        //    this.UpdateUserId = userId;
        //    this.SynchronizeOrganizationTargetAudiences(targetAudiences, userId);
        //}

        ///// <summary>Synchronizes the organization target audiences.</summary>
        ///// <param name="targetAudiences">The target audiences.</param>
        ///// <param name="userId">The user identifier.</param>
        //private void SynchronizeOrganizationTargetAudiences(List<TargetAudience> targetAudiences, int userId)
        //{
        //    if (this.OrganizationTargetAudiences == null)
        //    {
        //        this.OrganizationTargetAudiences = new List<OrganizationTargetAudience>();
        //    }

        //    this.DeleteOrganizationTargetAudiences(targetAudiences, userId);

        //    if (targetAudiences?.Any() != true)
        //    {
        //        return;
        //    }

        //    // Create or update target audiences
        //    foreach (var targetAudience in targetAudiences)
        //    {
        //        var organizationTargetAudienceDb = this.OrganizationTargetAudiences.FirstOrDefault(a => a.TargetAudience.Uid == targetAudience.Uid);
        //        if (organizationTargetAudienceDb != null)
        //        {
        //            organizationTargetAudienceDb.Update(userId);
        //        }
        //        else
        //        {
        //            this.CreateOrganizationTargetAudience(targetAudience, userId);
        //        }
        //    }
        //}

        ///// <summary>Deletes the organization target audiences.</summary>
        ///// <param name="newTargetAudiences">The new target audiences.</param>
        ///// <param name="userId">The user identifier.</param>
        //private void DeleteOrganizationTargetAudiences(List<TargetAudience> newTargetAudiences, int userId)
        //{
        //    var organizationTargetAudiencesToDelete = this.OrganizationTargetAudiences.Where(db => newTargetAudiences?.Select(a => a.Uid)?.Contains(db.TargetAudience.Uid) == false && !db.IsDeleted).ToList();
        //    foreach (var organizationTargetAudienceToDelete in organizationTargetAudiencesToDelete)
        //    {
        //        organizationTargetAudienceToDelete.Delete(userId);
        //    }
        //}

        ///// <summary>Creates the organization target audience.</summary>
        ///// <param name="targetAudience">The target audience.</param>
        ///// <param name="userId">The user identifier.</param>
        //private void CreateOrganizationTargetAudience(TargetAudience targetAudience, int userId)
        //{
        //    this.OrganizationTargetAudiences.Add(new OrganizationTargetAudience(this, targetAudience, userId));
        //}

        //#endregion

        //#region Interests

        ///// <summary>Updates the organization interests.</summary>
        ///// <param name="organizationInterests">The organization interests.</param>
        ///// <param name="userId">The user identifier.</param>
        //public void UpdateOrganizationInterests(List<OrganizationInterest> organizationInterests, int userId)
        //{
        //    this.SynchronizeOrganizationInterests(organizationInterests, userId);

        //    this.IsDeleted = false;
        //    this.UpdateUserId = userId;
        //    this.UpdateDate = DateTime.UtcNow;
        //}

        ///// <summary>Synchronizes the organization interests.</summary>
        ///// <param name="organizationInterests">The organization interests.</param>
        ///// <param name="userId">The user identifier.</param>
        //private void SynchronizeOrganizationInterests(List<OrganizationInterest> organizationInterests, int userId)
        //{
        //    if (this.OrganizationInterests == null)
        //    {
        //        this.OrganizationInterests = new List<OrganizationInterest>();
        //    }

        //    this.DeleteOrganizationInterests(organizationInterests, userId);

        //    if (organizationInterests?.Any() != true)
        //    {
        //        return;
        //    }

        //    // Create or update interests
        //    foreach (var organizationInterest in organizationInterests)
        //    {
        //        var interestDb = this.OrganizationInterests.FirstOrDefault(a => a.Interest.Uid == organizationInterest.Interest.Uid);
        //        if (interestDb != null)
        //        {
        //            interestDb.Update(organizationInterest, userId);
        //        }
        //        else
        //        {
        //            this.OrganizationInterests.Add(organizationInterest);
        //        }
        //    }
        //}

        //private void DeleteOrganizationInterests(List<OrganizationInterest> newOrganizationInterests, int userId)
        //{
        //    var organizationInterestsToDelete = this.OrganizationInterests.Where(db => newOrganizationInterests?.Select(a => a.Interest.Uid)?.Contains(db.Interest.Uid) == false && !db.IsDeleted).ToList();
        //    foreach (var organizationInterestToDelete in organizationInterestsToDelete)
        //    {
        //        organizationInterestToDelete.Delete(userId);
        //    }
        //}

        //#endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateMusicBandType();
            this.ValidateName();
            this.ValidateImageUrl();
            this.ValidateMainMusicInfluences();
            this.ValidateFormationDate();
            this.ValidateFacebook();
            this.ValidateInstagram();
            this.ValidateTwitter();
            this.ValidateYoutube();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the type of the music band.</summary>
        public void ValidateMusicBandType()
        {
            if (this.MusicBandType == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.BandType), new string[] { "MusicBandType" }));
            }
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

        /// <summary>Validates the image URL.</summary>
        public void ValidateImageUrl()
        {
            if (!string.IsNullOrEmpty(this.ImageUrl) && this.ImageUrl?.Trim().Length > ImageUrlMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Photo, ImageUrlMaxLength, 1), new string[] { "ImageUrl" }));
            }
        }

        /// <summary>Validates the formation date.</summary>
        public void ValidateFormationDate()
        {
            if (!string.IsNullOrEmpty(this.FormationDate) && this.FormationDate?.Trim().Length > FormationDateMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Date, FormationDateMaxLength, 1), new string[] { "FormationDate" })); //TODO: Change Labels.Date to Labels.FormationDate
            }
        }

        /// <summary>Validates the main music influences.</summary>
        public void ValidateMainMusicInfluences()
        {
            if (!string.IsNullOrEmpty(this.MainMusicInfluences) && this.MainMusicInfluences?.Trim().Length > MainMusicInfluencesMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.MainMusicInfluences, MainMusicInfluencesMaxLength, 1), new string[] { "MainMusicInfluences" }));
            }
        }

        /// <summary>Validates the facebook.</summary>
        public void ValidateFacebook()
        {
            if (!string.IsNullOrEmpty(this.Facebook) && this.Facebook?.Trim().Length > FacebookMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Facebook", FacebookMaxLength, 1), new string[] { "Facebook" }));
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

        /// <summary>Validates the twitter.</summary>
        public void ValidateTwitter()
        {
            if (!string.IsNullOrEmpty(this.Twitter) && this.Twitter?.Trim().Length > TwitterMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Twitter", TwitterMaxLength, 1), new string[] { "Twitter" }));
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

        ///// <summary>Validates the descriptions.</summary>
        //public void ValidateDescriptions()
        //{
        //    if (this.OrganizationDescriptions?.Any() != true)
        //    {
        //        return;
        //    }

        //    foreach (var description in this.OrganizationDescriptions?.Where(d => !d.IsValid())?.ToList())
        //    {
        //        this.ValidationResult.Add(description.ValidationResult);
        //    }
        //}

        ///// <summary>Validates the address.</summary>
        //public void ValidateAddress()
        //{
        //    if (this.Address != null && !this.Address.IsValid())
        //    {
        //        this.ValidationResult.Add(this.Address.ValidationResult);
        //    }
        //}

        #endregion
    }
}