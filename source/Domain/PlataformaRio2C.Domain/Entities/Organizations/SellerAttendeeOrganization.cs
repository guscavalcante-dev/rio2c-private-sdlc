// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 11-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-16-2019
// ***********************************************************************
// <copyright file="SellerAttendeeOrganization.cs" company="Softo">
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
    /// <summary>SellerAttendeeOrganization</summary>
    public class SellerAttendeeOrganization : Entity
    {
        public int AttendeeOrganizationId { get; private set; }
        public int AttendeeCollaboratorTicketId { get; private set; }
        public int ProjectsCount { get; private set; }
        public int ProjectsBuyerEvaluationGroupsCount { get; private set; }

        public virtual AttendeeOrganization AttendeeOrganization { get; private set; }
        public virtual AttendeeCollaboratorTicket AttendeeCollaboratorTicket { get; private set; }
        public virtual ICollection<Project> Projects { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="SellerAttendeeOrganization"/> class.</summary>
        /// <param name="attendeeOrganization">The attendee organization.</param>
        /// <param name="attendeeCollaboratorTicket">The attendee collaborator ticket.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="numberOfEpisodes">The number of episodes.</param>
        /// <param name="eachEpisodePlayingTime">The each episode playing time.</param>
        /// <param name="valuePerEpisode">The value per episode.</param>
        /// <param name="totalValueOfProject">The total value of project.</param>
        /// <param name="valueAlreadyRaised">The value already raised.</param>
        /// <param name="valueStillNeeded">The value still needed.</param>
        /// <param name="isPitching">if set to <c>true</c> [is pitching].</param>
        /// <param name="titles">The titles.</param>
        /// <param name="logLines">The log lines.</param>
        /// <param name="summaries">The summaries.</param>
        /// <param name="productionPlans">The production plans.</param>
        /// <param name="additionalInformations">The additional informations.</param>
        /// <param name="interests">The interests.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="imageLink">The image link.</param>
        /// <param name="teaserLink">The teaser link.</param>
        /// <param name="userId">The user identifier.</param>
        public SellerAttendeeOrganization(
            AttendeeOrganization attendeeOrganization, 
            AttendeeCollaboratorTicket attendeeCollaboratorTicket,
            ProjectType projectType,
            int? numberOfEpisodes,
            string eachEpisodePlayingTime,
            int? valuePerEpisode,
            int? totalValueOfProject,
            int? valueAlreadyRaised,
            int? valueStillNeeded,
            bool isPitching,
            List<ProjectTitle> titles,
            List<ProjectLogLine> logLines,
            List<ProjectSummary> summaries,
            List<ProjectProductionPlan> productionPlans,
            List<ProjectAdditionalInformation> additionalInformations,
            List<Interest> interests,
            List<TargetAudience> targetAudiences,
            string imageLink,
            string teaserLink,
            int userId)
        {
            this.AttendeeOrganizationId = attendeeOrganization?.Id ?? 0;
            this.AttendeeOrganization = attendeeOrganization;
            this.AttendeeCollaboratorTicketId = attendeeCollaboratorTicket?.Id ?? 0;
            this.AttendeeCollaboratorTicket = attendeeCollaboratorTicket;
            this.CreateProject(
                projectType,
                numberOfEpisodes,
                eachEpisodePlayingTime,
                valuePerEpisode,
                totalValueOfProject,
                valueAlreadyRaised,
                valueStillNeeded,
                isPitching,
                titles,
                logLines,
                summaries,
                productionPlans,
                additionalInformations,
                interests,
                targetAudiences,
                imageLink,
                teaserLink,
                userId);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="SellerAttendeeOrganization"/> class.</summary>
        protected SellerAttendeeOrganization()
        {
        }

        #region Projects

        /// <summary>Creates the project.</summary>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="numberOfEpisodes">The number of episodes.</param>
        /// <param name="eachEpisodePlayingTime">The each episode playing time.</param>
        /// <param name="valuePerEpisode">The value per episode.</param>
        /// <param name="totalValueOfProject">The total value of project.</param>
        /// <param name="valueAlreadyRaised">The value already raised.</param>
        /// <param name="valueStillNeeded">The value still needed.</param>
        /// <param name="isPitching">if set to <c>true</c> [is pitching].</param>
        /// <param name="titles">The titles.</param>
        /// <param name="logLines">The log lines.</param>
        /// <param name="summaries">The summaries.</param>
        /// <param name="productionPlans">The production plans.</param>
        /// <param name="additionalInformations">The additional informations.</param>
        /// <param name="interests">The interests.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="imageLink">The image link.</param>
        /// <param name="teaserLink">The teaser link.</param>
        /// <param name="userId">The user identifier.</param>
        public void CreateProject(
            ProjectType projectType,
            int? numberOfEpisodes,
            string eachEpisodePlayingTime,
            int? valuePerEpisode,
            int? totalValueOfProject,
            int? valueAlreadyRaised,
            int? valueStillNeeded,
            bool isPitching,
            List<ProjectTitle> titles,
            List<ProjectLogLine> logLines,
            List<ProjectSummary> summaries,
            List<ProjectProductionPlan> productionPlans,
            List<ProjectAdditionalInformation> additionalInformations,
            List<Interest> interests,
            List<TargetAudience> targetAudiences,
            string imageLink,
            string teaserLink,
            int userId)
        {
            if (this.Projects == null)
            {
                this.Projects = new List<Project>();
            }

            this.Projects.Add(new Project(
                projectType,
                this,
                numberOfEpisodes,
                eachEpisodePlayingTime,
                valuePerEpisode,
                totalValueOfProject,
                valueAlreadyRaised,
                valueStillNeeded,
                isPitching,
                titles,
                logLines,
                summaries,
                productionPlans,
                additionalInformations,
                interests,
                targetAudiences,
                imageLink,
                teaserLink,
                userId));

            this.UpdateProjectsCount();
            this.UpdateProjectsBuyerEvaluationGroupsCount();
        }

        /// <summary>Gets the last created project.</summary>
        /// <returns></returns>
        public Project GetLastCreatedProject()
        {
            return this.Projects?.OrderByDescending(p => p.CreateDate).FirstOrDefault();
        }

        #region Projects Counter

        /// <summary>Updates the projects count.</summary>
        public void UpdateProjectsCount()
        {
            this.ProjectsCount = this.RecountProjects();
        }

        /// <summary>Counts the projects.</summary>
        /// <returns></returns>
        public int RecountProjects()
        {
            return this.Projects?.Count(p => !p.IsDeleted) ?? 0;
        }

        /// <summary>Determines whether [has projects available].</summary>
        /// <returns>
        ///   <c>true</c> if [has projects available]; otherwise, <c>false</c>.</returns>
        public bool HasProjectsAvailable()
        {
            return this.ProjectsCount < this.GetTicketTypeProjectMaxCount();
        }

        #endregion

        #region Projects Buyer Evaluation Groups Counter

        /// <summary>Updates the projects buyer evaluation groups count.</summary>
        public void UpdateProjectsBuyerEvaluationGroupsCount()
        {
            this.ProjectsBuyerEvaluationGroupsCount = this.RecountProjectsBuyerEvaluationGroups();
        }

        /// <summary>Recounts the projects buyer evaluation groups.</summary>
        /// <returns></returns>
        public int RecountProjectsBuyerEvaluationGroups()
        {
            return this.Projects?.Where(p => !p.IsDeleted)?.Sum(p => p.ProjectBuyerEvaluationGroupsCount) ?? 0;
        }

        /// <summary>Determines whether [has projects buyer evaluations available].</summary>
        /// <returns>
        ///   <c>true</c> if [has projects buyer evaluations available]; otherwise, <c>false</c>.</returns>
        public bool HasProjectsBuyerEvaluationsAvailable()
        {
            return this.ProjectsBuyerEvaluationGroupsCount < this.GetTicketTypeBuyerEvaluationGroupMaxCount();
        }

        #endregion

        #region Ticket Type Counters

        /// <summary>Gets the ticket type project maximum count.</summary>
        /// <returns></returns>
        public int GetTicketTypeProjectMaxCount()
        {
            return this.AttendeeCollaboratorTicket?.AttendeeSalesPlatformTicketType?.ProjectMaxCount ?? 0;
        }

        /// <summary>Gets the ticket type buyer evaluation maximum count.</summary>
        /// <returns></returns>
        public int GetTicketTypeBuyerEvaluationMaxCount()
        {
            return this.AttendeeCollaboratorTicket?.AttendeeSalesPlatformTicketType?.ProjectBuyerEvaluationMaxCount ?? 0;
        }

        /// <summary>Gets the ticket type buyer evaluation group maximum count.</summary>
        /// <returns></returns>
        public int GetTicketTypeBuyerEvaluationGroupMaxCount()
        {
            return this.AttendeeCollaboratorTicket?.AttendeeSalesPlatformTicketType?.ProjectBuyerEvaluationGroupMaxCount ?? 0;
        }

        #endregion

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateAttendeeOrganization();
            this.ValidateAttendeeCollaboratorTicket();
            this.ValidateProjects();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the attendee organization.</summary>
        public void ValidateAttendeeOrganization()
        {
            if (this.AttendeeOrganization == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Company), new string[] { "ToastrError" }));
            }
        }

        /// <summary>Validates the attendee collaborator ticket.</summary>
        public void ValidateAttendeeCollaboratorTicket()
        {
            if (this.AttendeeCollaboratorTicket == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Ticket), new string[] { "ToastrError" }));
            }
            else
            {
                if (this.ProjectsCount > this.GetTicketTypeProjectMaxCount())
                {
                    this.ValidationResult.Add(new ValidationError(Messages.IsNotPossibleCreateProjectLimit, new string[] { "ToastrError" }));
                }

                if (this.ProjectsBuyerEvaluationGroupsCount > this.GetTicketTypeBuyerEvaluationGroupMaxCount())
                {
                    this.ValidationResult.Add(new ValidationError(Messages.IsNotPossibleCreateProjectLimit, new string[] { "ToastrError" }));
                }
            }
        }

        /// <summary>Validates the projects.</summary>
        public void ValidateProjects()
        {
            if (this.Projects?.Any() != true)
            {
                return;
            }

            foreach (var project in this.Projects?.Where(d => !d.IsDeleted && !d.IsCreateValid())?.ToList())
            {
                this.ValidationResult.Add(project.ValidationResult);
            }
        }

        #endregion
    }
}