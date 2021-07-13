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
        public int EditionId { get; set; }
        public int InnovationOrganizationId { get; set; }
        public DateTimeOffset? EvaluationEmailSendDate { get; set; }
        public decimal? Grade { get; set; }
        public int EvaluationsCount { get; set; }
        public DateTimeOffset? LastEvaluationDate { get; set; }
        public decimal AccumulatedRevenue { get; set; }
        public string MarketSize { get; set; }
        public string BusinessDefinition { get; set; }
        public string BusinessFocus { get; set; }
        public string BusinessEconomicModel { get; set; }
        public string BusinessDifferentials { get; set; }
        public string BusinessStage { get; set; }
        public DateTimeOffset? PresentationUploadDate { get; set; }
        public string BusinessOperationalModel { get; set; }

        public virtual Edition Edition { get; private set; }
        public virtual InnovationOrganization InnovationOrganization { get; private set; }

        public virtual ICollection<AttendeeInnovationOrganizationCollaborator> AttendeeInnovationOrganizationCollaborators { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganization"/> class.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="innovationOrganization">The music band.</param>
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
        public void Delete(int userId)
        {
            this.DeleteAttendeeInnovationOrganizationCollaborators(userId);
            if (this.FindAllAttendeeInnovationOrganizationCollaboratorsNotDeleted()?.Any() == true)
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

        #region Attendee Innovation Organization Collaborators

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
