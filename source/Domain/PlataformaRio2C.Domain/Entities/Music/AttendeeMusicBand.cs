// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-04-2021
// ***********************************************************************
// <copyright file="AttendeeMusicBand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeMusicBand</summary>
    public class AttendeeMusicBand : Entity
    {
        public int EditionId { get; private set; }
        public int MusicBandId { get; private set; }
        public decimal? Grade { get; private set; }
        public int EvaluationsCount { get; private set; }
        public DateTimeOffset? LastEvaluationDate { get; private set; }
        public DateTimeOffset? EvaluationEmailSendDate { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual MusicBand MusicBand { get; private set; }

        public virtual ICollection<AttendeeMusicBandCollaborator> AttendeeMusicBandCollaborators { get; private set; }
        public virtual ICollection<MusicProject> MusicProjects { get; private set; }
        public virtual ICollection<AttendeeMusicBandEvaluation> AttendeeMusicBandEvaluations { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeMusicBand"/> class.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="musicBand">The music band.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeMusicBand(
            Edition edition,
            MusicBand musicBand,
            MusicProjectApiDto musicProjectApiDto,
            int userId)
        {
            this.Edition = edition;
            this.MusicBand = musicBand;
            this.EditionId = edition.Id;
            this.MusicBandId = musicBand.Id;

            this.CreateProject(
                musicProjectApiDto.VideoUrl,
                musicProjectApiDto.Music1Url,
                musicProjectApiDto.Music2Url,
                musicProjectApiDto.Release,
                musicProjectApiDto.Clipping1,
                musicProjectApiDto.Clipping2,
                musicProjectApiDto.Clipping3,
                false,
                userId);

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

        #region Evaluation

        /// <summary>
        /// Evaluates the specified evaluation user.
        /// </summary>
        /// <param name="evaluatorUser">The evaluation user.</param>
        /// <param name="grade">The grade.</param>
        public void Evaluate(User evaluatorUser, decimal grade)
        {
            if (this.AttendeeMusicBandEvaluations == null)
                this.AttendeeMusicBandEvaluations = new List<AttendeeMusicBandEvaluation>();

            var existentAttendeeMusicBandEvaluation = this.GetAttendeeMusicBandEvaluationByEvaluatorId(evaluatorUser.Id);
            if (existentAttendeeMusicBandEvaluation != null)
            {
                existentAttendeeMusicBandEvaluation.Update(grade, evaluatorUser.Id);
            }
            else
            {
                this.AttendeeMusicBandEvaluations.Add(new AttendeeMusicBandEvaluation(
                    this,
                    evaluatorUser,
                    grade,
                    evaluatorUser.Id));
            }

            this.Grade = this.GetAverageEvaluation(this.Edition);
            this.EvaluationsCount = this.AttendeeMusicBandEvaluations?.Count ?? 0;
            this.LastEvaluationDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Recalculates the grade.
        /// </summary>
        public void RecalculateGrade(Edition edition)
        {
            this.Grade = this.GetAverageEvaluation(edition);
        }

        /// <summary>
        /// Gets the average evaluation.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <returns></returns>
        private decimal? GetAverageEvaluation(Edition edition)
        {
            if (this.AttendeeMusicBandEvaluations?.Any() != true)
            {
                return null;
            }

            // Can only generate the 'AverageEvaluation' when the 'AttendeeMusicBandEvaluations' count 
            // is greater or equal than minimum necessary evaluations quantity
            if (this.GetAttendeeMusicBandEvaluationTotalCount() >= edition.MusicProjectMinimumEvaluationsCount)
            {
                return this.AttendeeMusicBandEvaluations.Sum(e => e.Grade) / this.AttendeeMusicBandEvaluations.Count;
            }

            return null;
        }

        /// <summary>
        /// Gets the attendee music band evaluation by evaluator identifier.
        /// </summary>
        /// <param name="evaluatorUserId">The evaluator user identifier.</param>
        /// <returns></returns>
        private AttendeeMusicBandEvaluation GetAttendeeMusicBandEvaluationByEvaluatorId(int evaluatorUserId)
        {
            return this.AttendeeMusicBandEvaluations.FirstOrDefault(ambe =>
                ambe.AttendeeMusicBand.EditionId == this.EditionId &&
                ambe.EvaluatorUserId == evaluatorUserId);
        }

        /// <summary>
        /// Gets the attendee music band evaluation total count.
        /// </summary>
        /// <returns></returns>
        private int GetAttendeeMusicBandEvaluationTotalCount()
        {
            return this.AttendeeMusicBandEvaluations.Count(ambe => ambe.AttendeeMusicBand.EditionId == this.EditionId);
        }

        #endregion

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
        private void CreateProject(
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
            this.ValidationResult = new ValidationResult();

            this.ValidateAttendeeMusicBandEvaluations();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the attendee music band evaluations.
        /// </summary>
        public void ValidateAttendeeMusicBandEvaluations()
        {
            if (this.AttendeeMusicBandEvaluations?.Any() != true)
            {
                return;
            }

            foreach (var attendeeMusicBandEvaluation in this.AttendeeMusicBandEvaluations?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(attendeeMusicBandEvaluation.ValidationResult);
            }
        }

        #endregion
    }
}