// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-29-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class AttendeeInnovationOrganization.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class AttendeeInnovationOrganization : Entity
    {
        public int EditionId { get; private set; }
        public int InnovationOrganizationId { get; private set; }
        public DateTimeOffset? EvaluationEmailSendDate { get; private set; }
        public decimal? Grade { get; private set; }
        public int EvaluationsCount { get; private set; }
        public DateTimeOffset? LastEvaluationDate { get; private set; }
        public decimal AccumulatedRevenue { get; private set; }
        public string MarketSize { get; private set; }
        public string BusinessDefinition { get; private set; }
        public string BusinessFocus { get; private set; }
        public string BusinessEconomicModel { get; private set; }
        public string BusinessDifferentials { get; private set; }
        public string BusinessStage { get; private set; }
        public DateTimeOffset? PresentationUploadDate { get; private set; }
        public string BusinessOperationalModel { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual InnovationOrganization InnovationOrganization { get; private set; }

        public virtual ICollection<AttendeeInnovationOrganizationCollaborator> AttendeeInnovationOrganizationCollaborators { get; private set; }
        public virtual ICollection<AttendeeInnovationOrganizationCompetitor> AttendeeInnovationOrganizationCompetitors { get; private set; }
        public virtual ICollection<AttendeeInnovationOrganizationFounder> AttendeeInnovationOrganizationFounders { get; private set; }
        public virtual ICollection<AttendeeInnovationOrganizationExperience> AttendeeInnovationOrganizationExperiences { get; private set; }
        public virtual ICollection<AttendeeInnovationOrganizationObjective> AttendeeInnovationOrganizationObjectives { get; private set; }
        public virtual ICollection<AttendeeInnovationOrganizationTechnology> AttendeeInnovationOrganizationTechnologies { get; private set; }
        public virtual ICollection<AttendeeInnovationOrganizationTrack> AttendeeInnovationOrganizationTracks { get; private set; }
        public virtual ICollection<AttendeeInnovationOrganizationEvaluation> AttendeeInnovationOrganizationEvaluations { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganization"/> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="innovationOrganization">The innovation organization.</param>
        /// <param name="evaluationEmailSendDate">The evaluation email send date.</param>
        /// <param name="grade">The grade.</param>
        /// <param name="evaluationsCount">The evaluations count.</param>
        /// <param name="lastEvaluationDate">The last evaluation date.</param>
        /// <param name="accumulatedRevenue">The accumulated revenue.</param>
        /// <param name="marketSize">Size of the market.</param>
        /// <param name="businessDefinition">The business definition.</param>
        /// <param name="businessFocus">The business focus.</param>
        /// <param name="businessEconomicModel">The business economic model.</param>
        /// <param name="businessDifferentials">The business differentials.</param>
        /// <param name="businessStage">The business stage.</param>
        /// <param name="presentationUploadDate">The presentation upload date.</param>
        /// <param name="businessOperationalModel">The business operational model.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeInnovationOrganization(
            Edition edition,
            InnovationOrganization innovationOrganization,
            decimal accumulatedRevenue,
            string marketSize,
            string businessDefinition,
            string businessFocus,
            string businessEconomicModel,
            string businessDifferentials,
            string businessStage,
            bool isPresentationUploaded,
            string businessOperationalModel,
            int userId)
        {
            this.Edition = edition;
            this.InnovationOrganization = innovationOrganization;
            this.AccumulatedRevenue = accumulatedRevenue;
            this.MarketSize = marketSize;
            this.BusinessDefinition = businessDefinition;
            this.BusinessFocus = businessFocus;
            this.BusinessEconomicModel = businessEconomicModel;
            this.BusinessDifferentials = businessDifferentials;
            this.BusinessStage = businessStage;
            this.BusinessOperationalModel = businessOperationalModel;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;

            this.UpdatePresentationUploadDate(isPresentationUploaded, false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganization"/> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="innovationOrganization">The innovation organization.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeInnovationOrganization(
            Edition edition,
            InnovationOrganization innovationOrganization,
            int userId)
        {
            this.Edition = edition;
            this.InnovationOrganization = innovationOrganization;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganization"/> class.
        /// </summary>
        public AttendeeInnovationOrganization()
        {

        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public new void Delete(int userId)
        {
            this.DeleteAttendeeInnovationOrganizationCollaborators(userId);
            this.DeleteAttendeeInnovationOrganizationFounders(userId);
            this.DeleteAttendeeInnovationOrganizationCompetitors(userId);
            this.DeleteAttendeeInnovationOrganizationExperiences(userId);
            this.DeleteAttendeeInnovationOrganizationObjectives(userId);
            this.DeleteAttendeeInnovationOrganizationTechnologies(userId);
            this.DeleteAttendeeInnovationOrganizationTracks(userId);

            base.Delete(userId);
        }

        /// <summary>
        /// Updates the presentation upload date.
        /// </summary>
        /// <param name="isPresentationUploaded">if set to <c>true</c> [is presentation uploaded].</param>
        /// <param name="isPresentationDeleted">if set to <c>true</c> [is presentation deleted].</param>
        private void UpdatePresentationUploadDate(bool isPresentationUploaded, bool isPresentationDeleted)
        {
            if (isPresentationUploaded)
            {
                this.PresentationUploadDate = DateTime.UtcNow;
            }
            else if (isPresentationDeleted)
            {
                this.PresentationUploadDate = null;
            }
        }

        #region Evaluation

        /// <summary>
        /// Evaluates the specified evaluation user.
        /// </summary>
        /// <param name="evaluatorUser">The evaluation user.</param>
        /// <param name="grade">The grade.</param>
        public void Evaluate(User evaluatorUser, decimal grade)
        {
            if (this.AttendeeInnovationOrganizationEvaluations == null)
                this.AttendeeInnovationOrganizationEvaluations = new List<AttendeeInnovationOrganizationEvaluation>();

            var existentAttendeeInnovationOrganizationEvaluation = this.GetAttendeeInnovationOrganizationEvaluationByEvaluatorId(evaluatorUser.Id);
            if (existentAttendeeInnovationOrganizationEvaluation != null)
            {
                existentAttendeeInnovationOrganizationEvaluation.Update(grade, evaluatorUser.Id);
            }
            else
            {
                this.AttendeeInnovationOrganizationEvaluations.Add(new AttendeeInnovationOrganizationEvaluation(
                    this,
                    evaluatorUser,
                    grade,
                    evaluatorUser.Id));
            }

            this.Grade = this.GetAverageEvaluation(this.Edition);
            this.EvaluationsCount = this.AttendeeInnovationOrganizationEvaluations?.Count ?? 0;
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
            if (this.AttendeeInnovationOrganizationEvaluations?.Any() != true)
            {
                return null;
            }

            // Can only generate the 'AverageEvaluation' when the 'AttendeeInnovationOrganizationEvaluations' count 
            // is greater or equal than minimum necessary evaluations quantity
            if (this.GetAttendeeInnovationOrganizationEvaluationTotalCount() >= edition.InnovationProjectMinimumEvaluationsCount)
            {
                return this.AttendeeInnovationOrganizationEvaluations.Sum(e => e.Grade) / this.AttendeeInnovationOrganizationEvaluations.Count;
            }

            return null;
        }

        /// <summary>
        /// Gets the attendee innovation organization evaluation by evaluator identifier.
        /// </summary>
        /// <param name="evaluatorUserId">The evaluator user identifier.</param>
        /// <returns></returns>
        private AttendeeInnovationOrganizationEvaluation GetAttendeeInnovationOrganizationEvaluationByEvaluatorId(int evaluatorUserId)
        {
            return this.AttendeeInnovationOrganizationEvaluations.FirstOrDefault(ambe =>
                ambe.AttendeeInnovationOrganization.EditionId == this.EditionId &&
                ambe.EvaluatorUserId == evaluatorUserId);
        }

        /// <summary>
        /// Gets the attendee innovation organization evaluation total count.
        /// </summary>
        /// <returns></returns>
        private int GetAttendeeInnovationOrganizationEvaluationTotalCount()
        {
            return this.AttendeeInnovationOrganizationEvaluations.Count(ambe => ambe.AttendeeInnovationOrganization.EditionId == this.EditionId);
        }

        #endregion

        #region Attendee Innovation Organization Collaborators

        /// <summary>
        /// Synchronizes the attendee innovation organization colaborators.
        /// </summary>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeInnovationOrganizationColaborators(AttendeeCollaborator attendeeCollaborator, int userId)
        {
            if (this.AttendeeInnovationOrganizationCollaborators == null)
            {
                this.AttendeeInnovationOrganizationCollaborators = new List<AttendeeInnovationOrganizationCollaborator>();
            }

            this.AttendeeInnovationOrganizationCollaborators.Add(new AttendeeInnovationOrganizationCollaborator(this, attendeeCollaborator, userId));

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Deletes the attendee innovation organization collaborators.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeInnovationOrganizationCollaborators(int userId)
        {
            foreach (var attendeeInnovationOrganizationCollaborator in this.FindAllAttendeeInnovationOrganizationCollaboratorsNotDeleted())
            {
                attendeeInnovationOrganizationCollaborator.Delete(userId);
            }
        }

        /// <summary>
        /// Finds all attendee innovation organization collaborators not deleted.
        /// </summary>
        /// <returns>List&lt;AttendeeInnovationOrganizationCollaborator&gt;.</returns>
        private List<AttendeeInnovationOrganizationCollaborator> FindAllAttendeeInnovationOrganizationCollaboratorsNotDeleted()
        {
            return this.AttendeeInnovationOrganizationCollaborators?.Where(aoc => !aoc.IsDeleted)?.ToList();
        }

        #endregion

        #region Attendee Innovation Organization Founder

        /// <summary>
        /// Synchronizes the attendee innovation organization founders.
        /// </summary>
        /// <param name="workDedication">The work dedication.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="curriculum">The curriculum.</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeInnovationOrganizationFounders(WorkDedication workDedication, string fullName, string curriculum, int userId)
        {
            if (this.AttendeeInnovationOrganizationFounders == null)
            {
                this.AttendeeInnovationOrganizationFounders = new List<AttendeeInnovationOrganizationFounder>();
            }

            this.AttendeeInnovationOrganizationFounders.Add(new AttendeeInnovationOrganizationFounder(this, workDedication, fullName, curriculum, userId));

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Deletes the attendee innovation organization founders.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeInnovationOrganizationFounders(int userId)
        {
            foreach (var attendeeInnovationOrganizationCollaborator in this.FindAllAttendeeInnovationOrganizationFoundersNotDeleted())
            {
                attendeeInnovationOrganizationCollaborator.Delete(userId);
            }
        }

        /// <summary>
        /// Finds all attendee innovation organization founders not deleted.
        /// </summary>
        /// <returns></returns>
        private List<AttendeeInnovationOrganizationFounder> FindAllAttendeeInnovationOrganizationFoundersNotDeleted()
        {
            return this.AttendeeInnovationOrganizationFounders?.Where(aoc => !aoc.IsDeleted)?.ToList();
        }


        #endregion

        #region Attendee Innovation Organization Competitor

        /// <summary>
        /// Synchronizes the attendee innovation organization competitors.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeInnovationOrganizationCompetitors(string name, int userId)
        {
            if (this.AttendeeInnovationOrganizationCompetitors == null)
            {
                this.AttendeeInnovationOrganizationCompetitors = new List<AttendeeInnovationOrganizationCompetitor>();
            }

            this.AttendeeInnovationOrganizationCompetitors.Add(new AttendeeInnovationOrganizationCompetitor(this, name, userId));

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Deletes the attendee innovation organization competitors.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeInnovationOrganizationCompetitors(int userId)
        {
            foreach (var attendeeInnovationOrganizationCompetitor in this.FindAllAttendeeInnovationOrganizationCompetitorsNotDeleted())
            {
                attendeeInnovationOrganizationCompetitor.Delete(userId);
            }
        }

        /// <summary>
        /// Finds all attendee innovation organization competitors not deleted.
        /// </summary>
        /// <returns></returns>
        private List<AttendeeInnovationOrganizationCompetitor> FindAllAttendeeInnovationOrganizationCompetitorsNotDeleted()
        {
            return this.AttendeeInnovationOrganizationCompetitors?.Where(aoc => !aoc.IsDeleted)?.ToList();
        }

        #endregion

        #region Attendee Innovation Organization Experience

        /// <summary>
        /// Synchronizes the attendee innovation organization experiences.
        /// </summary>
        /// <param name="innovationOrganizationExperienceOption">The innovation organization experience option.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeInnovationOrganizationExperiences(
            InnovationOrganizationExperienceOption innovationOrganizationExperienceOption, 
            string additionalInfo,
            int userId)
        {
            if (this.AttendeeInnovationOrganizationExperiences == null)
            {
                this.AttendeeInnovationOrganizationExperiences = new List<AttendeeInnovationOrganizationExperience>();
            }

            this.AttendeeInnovationOrganizationExperiences.Add(new AttendeeInnovationOrganizationExperience(
                this, 
                innovationOrganizationExperienceOption, 
                additionalInfo, 
                userId));

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Deletes the attendee innovation organization experiences.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeInnovationOrganizationExperiences(int userId)
        {
            foreach (var attendeeInnovationOrganizationExperience in this.FindAllAttendeeInnovationOrganizationExperiencesNotDeleted())
            {
                attendeeInnovationOrganizationExperience.Delete(userId);
            }
        }

        /// <summary>
        /// Finds all attendee innovation organization experiences not deleted.
        /// </summary>
        /// <returns></returns>
        private List<AttendeeInnovationOrganizationExperience> FindAllAttendeeInnovationOrganizationExperiencesNotDeleted()
        {
            return this.AttendeeInnovationOrganizationExperiences?.Where(aoc => !aoc.IsDeleted)?.ToList();
        }

        #endregion

        #region Attendee Innovation Organization Objective

        /// <summary>
        /// Synchronizes the attendee innovation organization objectives.
        /// </summary>
        /// <param name="innovationOrganizationObjectivesOption">The innovation organization objectives option.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeInnovationOrganizationObjectives(
            InnovationOrganizationObjectivesOption innovationOrganizationObjectivesOption,
            string additionalInfo,
            int userId)
        {
            if (this.AttendeeInnovationOrganizationObjectives == null)
            {
                this.AttendeeInnovationOrganizationObjectives = new List<AttendeeInnovationOrganizationObjective>();
            }

            this.AttendeeInnovationOrganizationObjectives.Add(new AttendeeInnovationOrganizationObjective(
                this,
                innovationOrganizationObjectivesOption,
                additionalInfo,
                userId));

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Deletes the attendee innovation organization objectives.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeInnovationOrganizationObjectives(int userId)
        {
            foreach (var attendeeInnovationOrganizationObjective in this.FindAllAttendeeInnovationOrganizationObjectivesNotDeleted())
            {
                attendeeInnovationOrganizationObjective.Delete(userId);
            }
        }

        /// <summary>
        /// Finds all attendee innovation organization objectives not deleted.
        /// </summary>
        /// <returns></returns>
        private List<AttendeeInnovationOrganizationObjective> FindAllAttendeeInnovationOrganizationObjectivesNotDeleted()
        {
            return this.AttendeeInnovationOrganizationObjectives?.Where(aoc => !aoc.IsDeleted)?.ToList();
        }

        #endregion

        #region Attendee Innovation Organization Technology

        /// <summary>
        /// Synchronizes the attendee innovation organization technologies.
        /// </summary>
        /// <param name="innovationOrganizationTechnologyOption">The innovation organization technology option.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeInnovationOrganizationTechnologies(
            InnovationOrganizationTechnologyOption innovationOrganizationTechnologyOption,
            string additionalInfo,
            int userId)
        {
            if (this.AttendeeInnovationOrganizationTechnologies == null)
            {
                this.AttendeeInnovationOrganizationTechnologies = new List<AttendeeInnovationOrganizationTechnology>();
            }

            this.AttendeeInnovationOrganizationTechnologies.Add(new AttendeeInnovationOrganizationTechnology(
                this,
                innovationOrganizationTechnologyOption,
                additionalInfo,
                userId));

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Deletes the attendee innovation organization technologies.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeInnovationOrganizationTechnologies(int userId)
        {
            foreach (var attendeeInnovationOrganizationTechnology in this.FindAllAttendeeInnovationOrganizationTechnologiesNotDeleted())
            {
                attendeeInnovationOrganizationTechnology.Delete(userId);
            }
        }

        /// <summary>
        /// Finds all attendee innovation organization technologies not deleted.
        /// </summary>
        /// <returns></returns>
        private List<AttendeeInnovationOrganizationTechnology> FindAllAttendeeInnovationOrganizationTechnologiesNotDeleted()
        {
            return this.AttendeeInnovationOrganizationTechnologies?.Where(aoc => !aoc.IsDeleted)?.ToList();
        }

        #endregion

        #region Attendee Innovation Organization Track

        /// <summary>
        /// Synchronizes the attendee innovation organization tracks.
        /// </summary>
        /// <param name="innovationOrganizationTrackOption">The innovation organization track option.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeInnovationOrganizationTracks(
            InnovationOrganizationTrackOption innovationOrganizationTrackOption,
            string additionalInfo,
            int userId)
        {
            if (this.AttendeeInnovationOrganizationTracks == null)
            {
                this.AttendeeInnovationOrganizationTracks = new List<AttendeeInnovationOrganizationTrack>();
            }

            this.AttendeeInnovationOrganizationTracks.Add(new AttendeeInnovationOrganizationTrack(
                this,
                innovationOrganizationTrackOption,
                additionalInfo,
                userId));

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Deletes the attendee innovation organization tracks.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeInnovationOrganizationTracks(int userId)
        {
            foreach (var attendeeInnovationOrganizationTrack in this.FindAllAttendeeInnovationOrganizationTracksNotDeleted())
            {
                attendeeInnovationOrganizationTrack.Delete(userId);
            }
        }

        /// <summary>
        /// Finds all attendee innovation organization tracks not deleted.
        /// </summary>
        /// <returns></returns>
        private List<AttendeeInnovationOrganizationTrack> FindAllAttendeeInnovationOrganizationTracksNotDeleted()
        {
            return this.AttendeeInnovationOrganizationTracks?.Where(aoc => !aoc.IsDeleted)?.ToList();
        }

        #endregion

        #region Valitations

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}
