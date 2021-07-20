// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-04-2021
// ***********************************************************************
// <copyright file="MusicBand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
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

        public virtual ICollection<AttendeeMusicBand> AttendeeMusicBands { get; private set; }
        public virtual ICollection<MusicBandGenre> MusicBandGenres { get; private set; }
        public virtual ICollection<MusicBandTargetAudience> MusicBandTargetAudiences { get; private set; }
        public virtual ICollection<MusicBandMember> MusicBandMembers { get; private set; }
        public virtual ICollection<MusicBandTeamMember> MusicBandTeamMembers { get; private set; }
        public virtual ICollection<ReleasedMusicProject> ReleasedMusicProjects { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBand"/> class.
        /// </summary>
        /// <param name="musicBandType">Type of the music band.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="name">The name.</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="formationDate">The formation date.</param>
        /// <param name="mainMusicInfluences">The main music influences.</param>
        /// <param name="facebook">The facebook.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="musicProjectApiDto">The music project API dto.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="musicGenreApiDtos">The music genre API dtos.</param>
        /// <param name="targetAudienceApiDtos">The target audience API dtos.</param>
        /// <param name="musicBandMemberApiDtos">The music band member API dtos.</param>
        /// <param name="musicBandTeamMemberApiDtos">The music band team member API dtos.</param>
        /// <param name="releasedMusicProjectApiDtos">The released music project API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        public MusicBand(
            MusicBandType musicBandType,
            Edition edition,
            string name,
            string imageUrl,
            string formationDate,
            string mainMusicInfluences,
            string facebook,
            string instagram,
            string twitter,
            string youtube,
            MusicProjectApiDto musicProjectApiDto,
            AttendeeCollaborator attendeeCollaborator,
            List<MusicGenreApiDto> musicGenreApiDtos,
            List<TargetAudienceApiDto> targetAudienceApiDtos,
            List<MusicBandMemberApiDto> musicBandMemberApiDtos,
            List<MusicBandTeamMemberApiDto> musicBandTeamMemberApiDtos,
            List<ReleasedMusicProjectApiDto> releasedMusicProjectApiDtos,
            int userId)
        {
            this.MusicBandType = musicBandType;
            this.Name = name;
            this.ImageUrl = imageUrl;
            this.FormationDate = formationDate;
            this.MainMusicInfluences = mainMusicInfluences;
            this.Facebook = facebook;
            this.Instagram = instagram;
            this.Twitter = twitter;
            this.Youtube = youtube;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;

            this.SynchronizeAttendeeMusicBandsCollaborators(edition, attendeeCollaborator, musicProjectApiDto, userId);  
            this.AddMusicBandGenresDtos(musicGenreApiDtos, userId);
            this.AddMusicBandTargetAudienceDtos(targetAudienceApiDtos, userId);
            this.AddMusicBandMembersDtos(musicBandMemberApiDtos, userId);
            this.AddMusicBandTeamMembersDtos(musicBandTeamMemberApiDtos, userId);
            this.AddReleasedMusicProjectsDtos(releasedMusicProjectApiDtos, userId);
        }

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

        /// <summary>
        /// Updates the social networks.
        /// </summary>
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

        #region Attendee Music Bands

        /// <summary>
        /// Synchronizes the attendee music bands collaborators.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="musicProjectApiDto">The music project API dto.</param>
        private void SynchronizeAttendeeMusicBandsCollaborators(
            Edition edition, 
            AttendeeCollaborator attendeeCollaborator, 
            MusicProjectApiDto musicProjectApiDto, 
            int userId)
        {
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
                var newAttendeeMusicBand = new AttendeeMusicBand(
                    edition, 
                    this, 
                    musicProjectApiDto.VideoUrl,
                    musicProjectApiDto.Music1Url,
                    musicProjectApiDto.Music2Url,
                    musicProjectApiDto.Release,
                    musicProjectApiDto.Clipping1,
                    musicProjectApiDto.Clipping2,
                    musicProjectApiDto.Clipping3,
                    userId);
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
        private AttendeeMusicBand FindAttendeeMusicBandByEditionId(int editionId)
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

        #region Evaluation

        /// <summary>
        /// Evaluates the specified edition.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="evaluatorUser">The evaluator user.</param>
        /// <param name="grade">The grade.</param>
        public void Evaluate(Edition edition, User evaluatorUser, decimal grade)
        {
            var attendeeMusicBand = this.FindAttendeeMusicBandByEditionId(edition.Id);
            attendeeMusicBand?.Evaluate(evaluatorUser, grade);
        }

        #endregion

        #region Music Band Members

        /// <summary>
        /// Synchronizes the attendee music bands.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        private void AddMusicBandMembersDtos(List<MusicBandMemberApiDto> musicBandMemberApiDtos, int userId)
        {
            if (this.MusicBandMembers == null)
            {
                this.MusicBandMembers = new List<MusicBandMember>();
            }

            foreach(var bandMember in musicBandMemberApiDtos)
            {
                this.MusicBandMembers.Add(new MusicBandMember(this, bandMember.Name, bandMember.MusicInstrumentName, userId));
            }
        }

        #endregion

        #region Music Band Team Members

        /// <summary>
        /// Adds the music band team members.
        /// </summary>
        /// <param name="musicBandTeamMemberApiDtos">The music band team member API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        private void AddMusicBandTeamMembersDtos(List<MusicBandTeamMemberApiDto> musicBandTeamMemberApiDtos, int userId)
        {
            if (this.MusicBandTeamMembers == null)
            {
                this.MusicBandTeamMembers = new List<MusicBandTeamMember>();
            }

            foreach (var bandTeamMember in musicBandTeamMemberApiDtos)
            {
                this.MusicBandTeamMembers.Add(new MusicBandTeamMember(this, bandTeamMember.Name, bandTeamMember.Role, userId));
            }
        }

        #endregion

        #region Released Music Projects

        /// <summary>
        /// Adds the released music projects.
        /// </summary>
        /// <param name="releasedMusicProjectApiDtos">The released music project API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        private void AddReleasedMusicProjectsDtos(List<ReleasedMusicProjectApiDto> releasedMusicProjectApiDtos, int userId)
        {
            if (this.ReleasedMusicProjects == null)
            {
                this.ReleasedMusicProjects = new List<ReleasedMusicProject>();
            }

            foreach (var releasedMusicProject in releasedMusicProjectApiDtos)
            {
                this.ReleasedMusicProjects.Add(new ReleasedMusicProject(this, releasedMusicProject.Name, releasedMusicProject.Year, userId));
            }
        }

        #endregion

        #region Music Band Genres

        /// <summary>
        /// Adds the music band genres.
        /// </summary>
        /// <param name="musicGenreApiDtos">The music genre API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        private void AddMusicBandGenresDtos(List<MusicGenreApiDto> musicGenreApiDtos, int userId)
        {
            if (this.MusicBandGenres == null)
            {
                this.MusicBandGenres = new List<MusicBandGenre>();
            }

            foreach (var musicGenre in musicGenreApiDtos)
            {
                this.MusicBandGenres.Add(new MusicBandGenre(this, musicGenre.MusicGenre, null, userId));
            }
        }

        #endregion

        #region Music Band Target Audiences

        /// <summary>
        /// Adds the music band target audience.
        /// </summary>
        /// <param name="targetAudienceApiDtos">The target audience API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        private void AddMusicBandTargetAudienceDtos(List<TargetAudienceApiDto> targetAudienceApiDtos, int userId)
        {
            if (this.MusicBandTargetAudiences == null)
            {
                this.MusicBandTargetAudiences = new List<MusicBandTargetAudience>();
            }

            foreach (var targetAudience in targetAudienceApiDtos)
            {
                this.MusicBandTargetAudiences.Add(new MusicBandTargetAudience(this, targetAudience.TargetAudience, userId));
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

            this.ValidateMusicBandType();
            this.ValidateName();
            this.ValidateImageUrl();
            this.ValidateMainMusicInfluences();
            this.ValidateFormationDate();
            this.ValidateFacebook();
            this.ValidateInstagram();
            this.ValidateTwitter();
            this.ValidateYoutube();
            this.ValidateMusicBandGenres();
            this.ValidateMusicBandTargetAudiences();
            this.ValidateMusicBandMembers();
            this.ValidateMusicBandTeamMembers();
            this.ValidateReleasedMusicProjects();
            this.ValidateAttendeeMusicBand();

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

        /// <summary>Validates the music band genres.</summary>
        public void ValidateMusicBandGenres()
        {
            if (this.MusicBandGenres?.Any() != true)
            {
                return;
            }

            foreach (var musicBandGenre in this.MusicBandGenres?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(musicBandGenre.ValidationResult);
            }
        }

        /// <summary>Validates the music band target audiences.</summary>
        public void ValidateMusicBandTargetAudiences()
        {
            if (this.MusicBandTargetAudiences?.Any() != true)
            {
                return;
            }

            foreach (var musicBandTargetAudience in this.MusicBandTargetAudiences?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(musicBandTargetAudience.ValidationResult);
            }
        }

        /// <summary>Validates the music band members.</summary>
        public void ValidateMusicBandMembers()
        {
            if (this.MusicBandMembers?.Any() != true)
            {
                return;
            }

            foreach (var musicBandMember in this.MusicBandMembers?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(musicBandMember.ValidationResult);
            }
        }

        /// <summary>Validates the music band team members.</summary>
        public void ValidateMusicBandTeamMembers()
        {
            if (this.MusicBandTeamMembers?.Any() != true)
            {
                return;
            }

            foreach (var musicBandTeamMember in this.MusicBandTeamMembers?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(musicBandTeamMember.ValidationResult);
            }
        }

        /// <summary>Validates the released music projects.</summary>
        public void ValidateReleasedMusicProjects()
        {
            if (this.ReleasedMusicProjects?.Any() != true)
            {
                return;
            }

            foreach (var releasedMusicProject in this.ReleasedMusicProjects?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(releasedMusicProject.ValidationResult);
            }
        }

        /// <summary>
        /// Validates the attendee music band.
        /// </summary>
        public void ValidateAttendeeMusicBand()
        {
            if (this.AttendeeMusicBands?.Any() != true)
            {
                return;
            }

            foreach (var attendeeMusicBand in this.AttendeeMusicBands?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(attendeeMusicBand.ValidationResult);
            }
        }

        #endregion
    }
}