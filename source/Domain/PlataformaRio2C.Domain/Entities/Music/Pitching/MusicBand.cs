// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 12-02-2024
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
        public static readonly int FormationDateMaxLength = 300;
        public static readonly int MainMusicInfluencesMaxLength = 600;
        public static readonly int FacebookMaxLength = 300;
        public static readonly int InstagramMaxLength = 300;
        public static readonly int TwitterMaxLength = 300;
        public static readonly int YoutubeMaxLength = 300;
        public static readonly int TiktokMaxLength = 300;

        public int MusicBandTypeId { get; private set; }
        public string Name { get; private set; }
        public string FormationDate { get; private set; }
        public string MainMusicInfluences { get; private set; }
        public string Facebook { get; private set; }
        public string Instagram { get; private set; }
        public string Twitter { get; private set; }
        public string Youtube { get; private set; }
        public string Tiktok { get; private set; }
        public string Deezer { get; private set; }
        public string Spotify { get; private set; }

        public DateTimeOffset? ImageUploadDate { get; private set; }

        //TODO: Delete this field!
        [Obsolete("Initially in the Music Band Registration this field was used, but we refactored it to record the image in the AWS bucket. Actually the image is stored at 'ImageUploadDate' property.")]
        public string ImageUrl { get; set; }

        public virtual MusicBandType MusicBandType { get; private set; }

        public virtual ICollection<AttendeeMusicBand> AttendeeMusicBands { get; private set; }
        public virtual ICollection<MusicBandGenre> MusicBandGenres { get; private set; }
        public virtual ICollection<MusicBandTargetAudience> MusicBandTargetAudiences { get; private set; }
        public virtual ICollection<MusicBandMember> MusicBandMembers { get; private set; }
        public virtual ICollection<MusicBandTeamMember> MusicBandTeamMembers { get; private set; }
        public virtual ICollection<ReleasedMusicProject> ReleasedMusicProjects { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBand" /> class.
        /// </summary>
        /// <param name="musicBandType">Type of the music band.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="name">The name.</param>
        /// <param name="formationDate">The formation date.</param>
        /// <param name="deezer">The deezer.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="spotify">The spotify.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="musicProject">The music project.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="musicBandGenres">The music genres.</param>
        /// <param name="musicBandTeamMembers">The music band team members.</param>
        /// <param name="releasedMusicProjects">The released music projects.</param>
        /// <param name="userId">The user identifier.</param>
        public MusicBand(
            MusicBandType musicBandType,
            Edition edition,
            string name,
            string formationDate,
            string deezer,
            string instagram,
            string spotify,
            string youtube,
            bool isImageUploaded,
            MusicProject musicProject,
            AttendeeCollaborator attendeeCollaborator,
            List<MusicBandGenre> musicBandGenres,
            List<MusicBandTeamMember> musicBandTeamMembers,
            List<ReleasedMusicProject> releasedMusicProjects,
            int userId)
        {
            this.MusicBandType = musicBandType;
            this.Name = name;
            this.FormationDate = formationDate;
            this.Deezer = deezer;
            this.Instagram = instagram;
            this.Spotify = spotify;
            this.Youtube = youtube;

            this.UpdateImageUploadDate(isImageUploaded, false);
            base.SetCreateDate(userId);

            this.SynchronizeAttendeeMusicBands(
                edition, 
                attendeeCollaborator,
                musicProject,
                userId);  
            this.SynchronizeMusicBandGenres(musicBandGenres, userId);
            this.SynchronizeMusicBandTeamMembers(musicBandTeamMembers, userId);
            this.SynchronizeReleasedMusicProjects(releasedMusicProjects, userId);
        }

        /// <summary>Initializes a new instance of the <see cref="MusicBand"/> class.</summary>
        protected MusicBand()
        {
        }

        /// <summary>Determines whether this instance has image.</summary>
        /// <returns>
        ///   <c>true</c> if this instance has image; otherwise, <c>false</c>.</returns>
        public bool HasImage()
        {
            return this.ImageUploadDate.HasValue;
        }

        /// <summary>Gets the name abbreviation.</summary>
        /// <returns></returns>
        public string GetNameAbbreviation()
        {
            return this.Name?.GetTwoLetterCode();
        }

        /// <summary>Updates the image upload date.</summary>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        public void UpdateImageUploadDate(bool isImageUploaded, bool isImageDeleted)
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

        #region Attendee Music Bands

        /// <summary>
        /// Synchronizes the attendee music bands.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="musicProject">The music project.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeMusicBands(
            Edition edition,
            AttendeeCollaborator attendeeCollaborator,
            MusicProject musicProject,
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
                    musicProject,
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

        /// <summary>
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="evaluatorUser">The evaluator user.</param>
        /// <param name="commissionEvaluationStatus">The comission evaluation status.</param>
        public void ComissionEvaluation(Edition edition, User evaluatorUser, ProjectEvaluationStatus commissionEvaluationStatus)
        {
            var attendeeMusicBand = this.FindAttendeeMusicBandByEditionId(edition.Id);
            attendeeMusicBand?.ComissionEvaluation(evaluatorUser, commissionEvaluationStatus);
        }

        /// <summary>
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="evaluatorUser">The evaluator user.</param>
        /// <param name="curatorEvaluationStatusId">The curator evaluation status.</param>
        public void CuratorEvaluation(Edition edition, User evaluatorUser, ProjectEvaluationStatus curatorEvaluationStatusId)
        {
            var attendeeMusicBand = this.FindAttendeeMusicBandByEditionId(edition.Id);
            attendeeMusicBand?.CuratorEvaluation(evaluatorUser, curatorEvaluationStatusId);
        }

        /// <summary>
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="evaluatorUser">The evaluator user.</param>
        /// <param name="curatorEvaluationStatusId">The curator evaluation status.</param>
        public void RepechageEvaluation(Edition edition, User evaluatorUser, ProjectEvaluationStatus curatorEvaluationStatusId)
        {
            var attendeeMusicBand = this.FindAttendeeMusicBandByEditionId(edition.Id);
            attendeeMusicBand?.RepechageEvaluation(evaluatorUser, curatorEvaluationStatusId);
        }

        #endregion

        /// <summary>
        /// Synchronizes the music band members.
        /// </summary>
        /// <param name="musicBandMembers">The music band members.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeMusicBandMembers(List<MusicBandMember> musicBandMembers, int userId)
        {
            if (this.MusicBandMembers == null)
            {
                this.MusicBandMembers = new List<MusicBandMember>();
            }

            foreach(var musicBandMember in musicBandMembers)
            {
                this.MusicBandMembers.Add(new MusicBandMember(this, musicBandMember.Name, musicBandMember.MusicInstrumentName, userId));
            }
        }

        /// <summary>
        /// Synchronizes the music band team members.
        /// </summary>
        /// <param name="musicBandTeamMembers">The music band team members.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeMusicBandTeamMembers(List<MusicBandTeamMember> musicBandTeamMembers, int userId)
        {
            if (this.MusicBandTeamMembers == null)
            {
                this.MusicBandTeamMembers = new List<MusicBandTeamMember>();
            }

            foreach (var musicBandTeamMember in musicBandTeamMembers)
            {
                this.MusicBandTeamMembers.Add(new MusicBandTeamMember(this, musicBandTeamMember.Name, musicBandTeamMember.Role, userId));
            }
        }

        /// <summary>
        /// Synchronizes the released music projects.
        /// </summary>
        /// <param name="releasedMusicProjects">The released music projects.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeReleasedMusicProjects(List<ReleasedMusicProject> releasedMusicProjects, int userId)
        {
            if (this.ReleasedMusicProjects == null)
            {
                this.ReleasedMusicProjects = new List<ReleasedMusicProject>();
            }

            foreach (var releasedMusicProject in releasedMusicProjects)
            {
                this.ReleasedMusicProjects.Add(new ReleasedMusicProject(this, releasedMusicProject.Name, releasedMusicProject.Year, userId));
            }
        }

        /// <summary>
        /// Synchronizes the music band genres.
        /// </summary>
        /// <param name="musicBandGenres">The music genres.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeMusicBandGenres(List<MusicBandGenre> musicBandGenres, int userId)
        {
            if (this.MusicBandGenres == null)
            {
                this.MusicBandGenres = new List<MusicBandGenre>();
            }

            foreach (var musicBandGenre in musicBandGenres)
            {
                this.MusicBandGenres.Add(new MusicBandGenre(this, musicBandGenre.MusicGenre, musicBandGenre.AdditionalInfo, userId));
            }
        }

        /// <summary>
        /// Synchronizes the music band target audience.
        /// </summary>
        /// <param name="musicBandTargetAudiences">The music band target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeMusicBandTargetAudience(List<MusicBandTargetAudience> musicBandTargetAudiences, int userId)
        {
            if (this.MusicBandTargetAudiences == null)
            {
                this.MusicBandTargetAudiences = new List<MusicBandTargetAudience>();
            }

            foreach (var musicBandTargetAudience in musicBandTargetAudiences)
            {
                this.MusicBandTargetAudiences.Add(new MusicBandTargetAudience(this, musicBandTargetAudience.TargetAudience, userId));
            }
        }

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
            this.ValidateAttendeeMusicBands();

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
        /// Validates the attendee music bands.
        /// </summary>
        public void ValidateAttendeeMusicBands()
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