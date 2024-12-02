// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 11-10-2024
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
using PlataformaRio2C.Infra.CrossCutting.Resources;

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
        public bool WouldYouLikeParticipateBusinessRound { get; private set; }
        public bool WouldYouLikeParticipatePitching { get; private set; }
        public int? EvaluatorUserId { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual MusicBand MusicBand { get; private set; }

        public virtual ICollection<AttendeeMusicBandCollaborator> AttendeeMusicBandCollaborators { get; private set; }
        public virtual ICollection<MusicProject> MusicProjects { get; private set; }
        public virtual ICollection<AttendeeMusicBandEvaluation> AttendeeMusicBandEvaluations { get; private set; }
        public virtual User EvaluatorUser { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeMusicBand" /> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="musicBand">The music band.</param>
        /// <param name="musicProject">The music project.</param>
        /// <param name="wouldYouLikeParticipateBusinessRound">if set to <c>true</c> [would you like participate business round].</param>
        /// <param name="wouldYouLikeParticipatePitching">if set to <c>true</c> [would you like participate pitching].</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeMusicBand(
            Edition edition,
            MusicBand musicBand,
            MusicProject musicProject,
            bool wouldYouLikeParticipateBusinessRound,
            bool wouldYouLikeParticipatePitching,
            int userId)
        {
            this.Edition = edition;
            this.MusicBand = musicBand;
            this.EditionId = edition.Id;
            this.MusicBandId = musicBand.Id;
            this.WouldYouLikeParticipateBusinessRound = wouldYouLikeParticipateBusinessRound;
            this.WouldYouLikeParticipatePitching = wouldYouLikeParticipatePitching;

            this.CreateProject(
                musicProject,
                false,
                userId);

            base.SetCreateDate(userId);
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeMusicBand"/> class.</summary>
        protected AttendeeMusicBand()
        {
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public new void Delete(int userId)
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

            base.Delete(userId);
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
                var evaluation = new AttendeeMusicBandEvaluation(
                    this,
                    evaluatorUser,
                    grade,
                    evaluatorUser.Id
                );
                this.AttendeeMusicBandEvaluations.Add(evaluation);
            }

            this.Grade = this.GetAverageEvaluation();
            this.EvaluationsCount = this.GetAttendeeMusicBandEvaluationTotalCount();
            this.LastEvaluationDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Evaluates the specified evaluation user.
        /// </summary>
        /// <param name="evaluatorUser">The evaluation user.</param>
        /// <param name="commissionEvaluationStatus">The project evaluation status.</param>
        public void ComissionEvaluation(User evaluatorUser, ProjectEvaluationStatus commissionEvaluationStatus)
        {
            if (this.AttendeeMusicBandEvaluations == null)
                this.AttendeeMusicBandEvaluations = new List<AttendeeMusicBandEvaluation>();

            var existentAttendeeMusicBandEvaluation = this.GetAttendeeMusicBandEvaluationByEvaluatorId(evaluatorUser.Id);
            if (existentAttendeeMusicBandEvaluation != null)
            {
                existentAttendeeMusicBandEvaluation.UpdateCommissionEvaluation(commissionEvaluationStatus, evaluatorUser.Id);
            }
            else
            {
                var evaluation = new AttendeeMusicBandEvaluation(
                    this,
                    evaluatorUser,
                    evaluatorUser.Id
                );
                evaluation.UpdateCommissionEvaluation(commissionEvaluationStatus, evaluatorUser.Id);
                this.AttendeeMusicBandEvaluations.Add(evaluation);
            }

            this.EvaluationsCount = this.GetAttendeeMusicBandEvaluationTotalCount();
            this.LastEvaluationDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Evaluates the specified evaluation user.
        /// </summary>
        /// <param name="evaluatorUser">The evaluation user.</param>
        /// <param name="curatorEvaluationStatusId">The project evaluation status.</param>
        public void ComissionMusicCuratorEvaluation(User evaluatorUser, ProjectEvaluationStatus curatorEvaluationStatusId)
        {
            if (this.AttendeeMusicBandEvaluations == null)
                this.AttendeeMusicBandEvaluations = new List<AttendeeMusicBandEvaluation>();

            var existentAttendeeMusicBandEvaluation = this.GetAttendeeMusicBandEvaluationByEvaluatorId(evaluatorUser.Id);
            if (existentAttendeeMusicBandEvaluation != null)
            {
                existentAttendeeMusicBandEvaluation.UpdateCuratorEvaluation(curatorEvaluationStatusId, evaluatorUser.Id);
            }
            else
            {
                var evaluation = new AttendeeMusicBandEvaluation(
                    this,
                    evaluatorUser,
                    evaluatorUser.Id
                );
                evaluation.UpdateCuratorEvaluation(curatorEvaluationStatusId, evaluatorUser.Id);
                this.AttendeeMusicBandEvaluations.Add(evaluation);
            }

            this.EvaluationsCount = this.GetAttendeeMusicBandEvaluationTotalCount();
            this.LastEvaluationDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Update evaluator user id.
        /// </summary>
        public void UpdateEvaluatorUserId(int evaluatorUserId)
        {
            this.EvaluatorUserId = evaluatorUserId;
        }

        /// <summary>
        /// Recalculates the grade.
        /// </summary>
        public void RecalculateGrade()
        {
            this.Grade = this.GetAverageEvaluation();
        }

        /// <summary>
        /// Recalculates the votes count.
        /// </summary>
        public void RecalculateVotesCount()
        {
            this.EvaluationsCount = this.GetAttendeeMusicBandEvaluationTotalCount();
        }

        /// <summary>
        /// Gets the average evaluation.
        /// </summary>
        /// <returns></returns>
        private decimal? GetAverageEvaluation()
        {
            if (this.FindAllAttendeeMusicBandEvaluationsNotDeleted()?.Any() != true)
            {
                return null;
            }

            // Can only generate the 'AverageEvaluation' when the 'AttendeeMusicBandEvaluations' count 
            // is greater or equal than minimum necessary evaluations quantity
            if (this.GetAttendeeMusicBandEvaluationTotalCount() >= this.Edition?.MusicCommissionMinimumEvaluationsCount)
            {
                return this.FindAllAttendeeMusicBandEvaluationsNotDeleted().Sum(e => e.Grade) / this.FindAllAttendeeMusicBandEvaluationsNotDeleted().Count;
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
            return this.FindAllAttendeeMusicBandEvaluationsNotDeleted().FirstOrDefault(ambe =>
                ambe.AttendeeMusicBand.EditionId == this.EditionId &&
                ambe.EvaluatorUserId == evaluatorUserId);
        }

        /// <summary>
        /// Gets the attendee music band evaluation total count.
        /// </summary>
        /// <returns></returns>
        private int GetAttendeeMusicBandEvaluationTotalCount()
        {
            return this.FindAllAttendeeMusicBandEvaluationsNotDeleted()
                .Count(ambe => ambe.AttendeeMusicBand.EditionId == this.EditionId);
        }

        /// <summary>
        /// Gets the not deleted attendee music band evaluations.
        /// OBS.: Allways use this method to calc Average and others! Evaluations calcs cannot consider deleted Evaluations!
        /// </summary>
        /// <returns></returns>
        private List<AttendeeMusicBandEvaluation> FindAllAttendeeMusicBandEvaluationsNotDeleted()
        {
            return this.AttendeeMusicBandEvaluations?.Where(aoc => !aoc.IsDeleted)?.ToList();
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

        /// <summary>
        /// Creates the project.
        /// </summary>
        /// <param name="musicProject">The music project.</param>
        /// <param name="isClippingUploaded">if set to <c>true</c> [is clipping uploaded].</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateProject(
            MusicProject musicProject,
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
                musicProject.VideoUrl,
                musicProject.VideoUrlPassword,
                musicProject.Music1Url,
                musicProject.Music2Url,
                musicProject.Clipping1,
                musicProject.Clipping2,
                musicProject.Clipping3,
                musicProject.Release,
                userId));
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

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateAttendeeMusicBandEvaluations();
            //this.ValidateAttendeeMusicBandCollaborators();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the attendee music band evaluations.
        /// </summary>
        public void ValidateAttendeeMusicBandEvaluations()
        {
            if (this.FindAllAttendeeMusicBandEvaluationsNotDeleted()?.Any() != true)
            {
                return;
            }

            foreach (var attendeeMusicBandEvaluation in this.FindAllAttendeeMusicBandEvaluationsNotDeleted()?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(attendeeMusicBandEvaluation.ValidationResult);
            }
        }

        /// <summary>
        /// Validates the attendee music band collaborators.
        /// </summary>
        public void ValidateAttendeeMusicBandCollaborators()
        {
            if (this.FindAllAttendeeMusicBandCollaboratorsNotDeleted()?.Any() != true)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Responsible), new string[] { "AttendeeMusicBandCollaborators" }));
            }
        }

        #endregion
    }
}