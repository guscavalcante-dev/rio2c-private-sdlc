// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-01-2020
// ***********************************************************************
// <copyright file="AttendeeMusicBand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeMusicBand</summary>
    public class AttendeeMusicBand : Entity
    {
        public int EditionId { get; private set; }
        public int MusicBandId { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual MusicBand MusicBand { get; private set; }

        public virtual ICollection<AttendeeMusicBandCollaborator> AttendeeMusicBandCollaborators { get; private set; }
        public virtual ICollection<MusicProject> MusicProjects { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeMusicBand"/> class.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="musicBand">The music band.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeMusicBand(
            Edition edition, 
            MusicBand musicBand, 
            int userId)
        {
            this.Edition = edition;
            this.MusicBand = musicBand;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeMusicBand"/> class.</summary>
        protected AttendeeMusicBand()
        {
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            if (this.FindAllMusicProjectsNotDeleted()?.Any() == true)
            {
                return;
            }

            this.DeleteAttendeeMusicBandCollaborators(userId);
            if (this.FindAllAttendeeMusicBandCollaboratorsNotDeleted()?.Any() == true)
            {
                return;
            }

            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Restores the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Restore(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Attendee Music Band Collaborators

        /// <summary>Deletes the attendee music band collaborators.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeMusicBandCollaborators(int userId)
        {
            foreach (var attendeeMusicBandCollaborator in this.FindAllAttendeeMusicBandCollaboratorsNotDeleted())
            {
                attendeeMusicBandCollaborator.Delete(userId);
            }
        }

        /// <summary>Finds all attendee music band collaborators not deleted.</summary>
        /// <returns></returns>
        private List<AttendeeMusicBandCollaborator> FindAllAttendeeMusicBandCollaboratorsNotDeleted()
        {
            return this.AttendeeMusicBandCollaborators?.Where(aoc => !aoc.IsDeleted)?.ToList();
        }

        #endregion

        #region Music Projects

        /// <summary>Creates the project.</summary>
        /// <param name="videoUrl">The video URL.</param>
        /// <param name="music1Url">The music1 URL.</param>
        /// <param name="music2Url">The music2 URL.</param>
        /// <param name="release">The release.</param>
        /// <param name="clipping1">The clipping1.</param>
        /// <param name="clipping2">The clipping2.</param>
        /// <param name="clipping3">The clipping3.</param>
        /// <param name="isClippingUploaded">if set to <c>true</c> [is clipping uploaded].</param>
        /// <param name="userId">The user identifier.</param>
        public void CreateProject(
            string videoUrl,
            string music1Url,
            string music2Url,
            string release,
            string clipping1,
            string clipping2,
            string clipping3,
            bool isClippingUploaded,
            int userId)
        {
            if (this.MusicProjects == null)
            {
                this.MusicProjects = new List<MusicProject>();
            }

            // Validate collaborator tickets
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            this.MusicProjects.Add(new MusicProject(
                this,
                videoUrl,
                music1Url,
                music2Url,
                release,
                clipping1,
                clipping2,
                clipping3,
                userId));

            //this.UpdateProjectsCount();
        }

        /// <summary>Gets the last created music project.</summary>
        /// <returns></returns>
        public MusicProject GetLastCreatedMusicProject()
        {
            return this.MusicProjects?.OrderByDescending(p => p.CreateDate).FirstOrDefault();
        }

        /// <summary>Finds all music projects not deleted.</summary>
        /// <returns></returns>
        private List<MusicProject> FindAllMusicProjectsNotDeleted()
        {
            return this.MusicProjects?.Where(mp => !mp.IsDeleted)?.ToList();
        }

        #region Projects Counter

        ///// <summary>Updates the projects count.</summary>
        //public void UpdateProjectsCount()
        //{
        //    this.SellProjectsCount = this.RecountProjects();
        //}

        ///// <summary>Counts the projects.</summary>
        ///// <returns></returns>
        //public int RecountProjects()
        //{
        //    return this.SellProjects?.Count(p => !p.IsDeleted) ?? 0;
        //}

        ///// <summary>Determines whether [has projects available].</summary>
        ///// <returns>
        /////   <c>true</c> if [has projects available]; otherwise, <c>false</c>.</returns>
        //public bool HasProjectsAvailable()
        //{
        //    return this.SellProjectsCount < this.GetMaxSellProjectsCount();
        //}

        #endregion

        #region Edition Limits

        ///// <summary>Gets the maximum sell projects count.</summary>
        ///// <returns></returns>
        //public int GetMaxSellProjectsCount()
        //{
        //    return this.Edition?.AttendeeOrganizationMaxSellProjectsCount ?? 0;
        //}

        ///// <summary>Gets the project maximum buyer evaluations count.</summary>
        ///// <returns></returns>
        //public int GetProjectMaxBuyerEvaluationsCount()
        //{
        //    return this.Edition?.ProjectMaxBuyerEvaluationsCount ?? 0;
        //}

        #endregion

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            return true;
        }

        ///// <summary>Determines whether [is create project valid].</summary>
        ///// <returns>
        /////   <c>true</c> if [is create project valid]; otherwise, <c>false</c>.</returns>
        //public bool IsCreateProjectValid()
        //{
        //    if (this.ValidationResult == null)
        //    {
        //        this.ValidationResult = new ValidationResult();
        //    }

        //    this.ValidateProjectsLimits();
        //    this.ValidateProjects();

        //    return this.ValidationResult.IsValid;
        //}

        ///// <summary>Validates the projects limits.</summary>
        //public void ValidateProjectsLimits()
        //{
        //    if (this.SellProjectsCount > this.GetMaxSellProjectsCount())
        //    {
        //        this.ValidationResult.Add(new ValidationError(Messages.IsNotPossibleCreateProjectLimit, new string[] { "ToastrError" }));
        //    }
        //}

        ///// <summary>Validates the projects.</summary>
        //public void ValidateProjects()
        //{
        //    if (this.SellProjects?.Any() != true)
        //    {
        //        return;
        //    }

        //    foreach (var project in this.SellProjects?.Where(d => !d.IsDeleted && !d.IsCreateValid())?.ToList())
        //    {
        //        this.ValidationResult.Add(project.ValidationResult);
        //    }
        //}

        #endregion
    }
}