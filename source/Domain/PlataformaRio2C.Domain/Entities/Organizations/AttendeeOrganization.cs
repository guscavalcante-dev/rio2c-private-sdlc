// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-14-2019
// ***********************************************************************
// <copyright file="AttendeeOrganization.cs" company="Softo">
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
    /// <summary>AttendeeOrganization</summary>
    public class AttendeeOrganization : Entity
    {
        public int EditionId { get; private set; }
        public int OrganizationId { get; private set; }
        public DateTime? OnboardingStartDate { get; private set; }
        public DateTime? OnboardingFinishDate { get; private set; }
        public DateTime? OnboardingOrganizationDate { get; private set; }
        public DateTime? OnboardingInterestsDate { get; private set; }
        public DateTime? ProjectSubmissionOrganizationDate { get; private set; }
        public bool IsApiDisplayEnabled { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual Organization Organization { get; private set; }

        public virtual ICollection<AttendeeOrganizationType> AttendeeOrganizationTypes { get; private set; }
        public virtual ICollection<AttendeeOrganizationCollaborator> AttendeeOrganizationCollaborators { get; private set; }
        public virtual ICollection<SellerAttendeeOrganization> SellerAttendeeOrganizations { get; private set; }
        public virtual ICollection<ProjectBuyerEvaluation> ProjectBuyerEvaluations { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganization"/> class.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="organization">The organization.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeOrganization(Edition edition, Organization organization, OrganizationType organizationType, bool? isApiDisplayEnabled, int userId)
        {
            this.Edition = edition;
            this.Organization = organization;
            this.UpdateApiDisplay(isApiDisplayEnabled);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeAttendeeOrganizationTypes(organizationType, userId);
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganization"/> class.</summary>
        protected AttendeeOrganization()
        {
        }

        /// <summary>Deletes the specified organization type.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        public void Delete(OrganizationType organizationType, int userId)
        {
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.UpdateApiDisplay(false);
            this.DeleteOrganizationType(organizationType, userId);
            this.DeleteAttendeeOrganizationCollaborators(userId);

            if (this.FindAllAttendeeOrganizationTypesNotDeleted(organizationType)?.Any() != true
                && this.FindAllAttendeeOrganizationCollaboratorsNotDeleted()?.Any() != true)
            {
                this.IsDeleted = true;
            }
        }

        /// <summary>Restores the specified organization type.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="userId">The user identifier.</param>
        public void Restore(OrganizationType organizationType, bool? isApiDisplayEnabled, int userId)
        {
            this.UpdateApiDisplay(isApiDisplayEnabled);

            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.SynchronizeAttendeeOrganizationTypes(organizationType, userId);
        }

        /// <summary>Updates the API display.</summary>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        private void UpdateApiDisplay(bool? isApiDisplayEnabled)
        {
            if (!isApiDisplayEnabled.HasValue)
            {
                return;
            }

            this.IsApiDisplayEnabled = isApiDisplayEnabled.Value;
        }

        #region Attendee Organization Types

        /// <summary>Synchronizes the attendee organization types.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeOrganizationTypes(OrganizationType organizationType, int userId)
        {
            if (this.AttendeeOrganizationTypes == null)
            {
                this.AttendeeOrganizationTypes = new List<AttendeeOrganizationType>();
            }

            if (organizationType == null)
            {
                return;
            }

            var attendeeOrganizationType = this.AttendeeOrganizationTypes.FirstOrDefault(aot => aot.OrganizationTypeId == organizationType.Id);
            if (attendeeOrganizationType != null)
            {
                attendeeOrganizationType.Restore(userId);
            }
            else
            {
                this.AttendeeOrganizationTypes.Add(new AttendeeOrganizationType(this, organizationType, userId));
            }
        }

        /// <summary>Deletes the type of the organization.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteOrganizationType(OrganizationType organizationType, int userId)
        {
            foreach (var attendeeOrganizationType in this.FindAllAttendeeOrganizationTypesNotDeleted(organizationType))
            {
                attendeeOrganizationType?.Delete(userId);
            }
        }

        /// <summary>Finds all attendee organization types not deleted.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <returns></returns>
        private List<AttendeeOrganizationType> FindAllAttendeeOrganizationTypesNotDeleted(OrganizationType organizationType)
        {
            return this.AttendeeOrganizationTypes?.Where(aot => (organizationType == null || aot.OrganizationType.Uid == organizationType.Uid) && !aot.IsDeleted)?.ToList();
        }

        #endregion

        #region Attendee Organization Collaborators

        /// <summary>Deletes the attendee organization collaborators.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeOrganizationCollaborators(int userId)
        {
            foreach (var attendeeOrganizationCollaborator in this.FindAllAttendeeOrganizationCollaboratorsNotDeleted())
            {
                attendeeOrganizationCollaborator.Delete(userId);
            }
        }

        /// <summary>Finds all attendee organization collaborators not deleted.</summary>
        /// <returns></returns>
        private List<AttendeeOrganizationCollaborator> FindAllAttendeeOrganizationCollaboratorsNotDeleted()
        {
            return this.AttendeeOrganizationCollaborators?.Where(aoc => !aoc.IsDeleted)?.ToList();
        }

        #endregion

        #region Onboarding

        /// <summary>Called when [player].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardPlayer(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.OnboardingStartDate = this.OnboardingOrganizationDate = DateTime.Now;
        }

        /// <summary>Called when [interests].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardInterests(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.OnboardingFinishDate = this.OnboardingInterestsDate = DateTime.Now;
        }

        /// <summary>Called when [t icket buyer].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardTIcketBuyer(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.OnboardingStartDate = this.OnboardingFinishDate = this.OnboardingOrganizationDate = DateTime.Now;
        }

        /// <summary>Called when [producer].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardProducer(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.OnboardingStartDate = this.OnboardingFinishDate = this.OnboardingOrganizationDate = this.ProjectSubmissionOrganizationDate = DateTime.Now;
        }

        #endregion

        #region Projects

        /// <summary>Creates the project.</summary>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="attendeeCollaboratorTickets">The attendee collaborator tickets.</param>
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
            List<AttendeeCollaboratorTicket> attendeeCollaboratorTickets,
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
            if (this.SellerAttendeeOrganizations == null)
            {
                this.SellerAttendeeOrganizations = new List<SellerAttendeeOrganization>();
            }

            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            var sellerAttendeeOrganizations = this.SellerAttendeeOrganizations.Where(sao => attendeeCollaboratorTickets?.Contains(sao.AttendeeCollaboratorTicket) == true).ToList();

            // If there is no seller attendee organization
            if (this.SellerAttendeeOrganizations?.Any() != true)
            {
                this.SellerAttendeeOrganizations.Add(new SellerAttendeeOrganization(
                    this,
                    attendeeCollaboratorTickets?.OrderBy(act => act.CreateDate)?.FirstOrDefault(),
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
                    userId));
            }
            // If exists seller attendee organization with projects available
            else if (this.SellerAttendeeOrganizations.Any(sao => sao.HasProjectsAvailable()))
            {
                var sellerAttendeeOrganization = this.SellerAttendeeOrganizations
                                                            .Where(sao => sao.HasProjectsAvailable())
                                                            .OrderByDescending(sao => sao.ProjectsCount)
                                                            .First();
                sellerAttendeeOrganization.CreateProject(
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
            }
            //// If the seller attendee organization has projects available
            //else if (sellerAttendeeOrganizations.Any(sao => sao.CountProjects() < sao.AttendeeCollaboratorTicket.AttendeeSalesPlatformTicketType.ProjectMaxCount))
            //{
            //    foreach (var sellerAttendeeOrganization in sellerAttendeeOrganizations.OrderByDescending(sag => sag.CountProjects()))
            //    {
            //        if (sellerAttendeeOrganization.CountProjects() >= sellerAttendeeOrganization.AttendeeCollaboratorTicket.AttendeeSalesPlatformTicketType.ProjectMaxCount)
            //        {
            //            continue;
            //        }

            //        sellerAttendeeOrganization.CreateProject(
            //            projectType,
            //            numberOfEpisodes,
            //            eachEpisodePlayingTime,
            //            valuePerEpisode,
            //            totalValueOfProject,
            //            valueAlreadyRaised,
            //            valueStillNeeded,
            //            isPitching,
            //            titles,
            //            logLines,
            //            summaries,
            //            productionPlans,
            //            additionalInformations,
            //            interests,
            //            targetAudiences,
            //            imageLink,
            //            teaserLink,
            //            userId);

            //        break;
            //    }
            //}
            // If there is an unused attendee collaborator tickets
            else if (attendeeCollaboratorTickets?.Any(act => sellerAttendeeOrganizations.All(sao => sao.AttendeeCollaboratorTicketId != act.Id)) == true)
            {
                var attendeeCollaboratorTicket = attendeeCollaboratorTickets?.FirstOrDefault(act => sellerAttendeeOrganizations.All(sao => sao.AttendeeCollaboratorTicketId != act.Id));
                this.SellerAttendeeOrganizations.Add(new SellerAttendeeOrganization(
                    this,
                    attendeeCollaboratorTicket,
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
                    userId));
            }
            // If there is no avaiable projects and tickets
            else
            {
                this.ValidationResult.Add(new ValidationError(Messages.YouReachedProjectsLimit, new string[] { "ToastrError" }));
                return;
            }

            this.SynchronizeAttendeeOrganizationTypes(projectType.OrganizationTypes?.FirstOrDefault(ot => ot.IsSeller), userId);
        }

        /// <summary>Gets the last created project.</summary>
        /// <returns></returns>
        public Project GetLastCreatedProject()
        {
            return this.SellerAttendeeOrganizations?.Select(sao => sao.GetLastCreatedProject())?.OrderByDescending(p => p.CreateDate)?.FirstOrDefault();
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            return true;
        }

        /// <summary>Determines whether [is create project valid].</summary>
        /// <returns>
        ///   <c>true</c> if [is create project valid]; otherwise, <c>false</c>.</returns>
        public bool IsCreateProjectValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            this.ValidateSellerAttendeeOrganizations();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the projects.</summary>
        public void ValidateSellerAttendeeOrganizations()
        {
            if (this.SellerAttendeeOrganizations?.Any() != true)
            {
                return;
            }

            if (this.SellerAttendeeOrganizations.GroupBy(d => d.AttendeeCollaboratorTicketId).Count() > 1)
            {
                this.ValidationResult.Add(new ValidationError(Messages.IsNotPossibleCreateProjectSameTicket, new string[] { "ToastrError" }));
            }

            foreach (var sellerAttendeeOrganization in this.SellerAttendeeOrganizations?.Where(d => !d.IsDeleted && !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(sellerAttendeeOrganization.ValidationResult);
            }
        }

        #endregion
    }
}