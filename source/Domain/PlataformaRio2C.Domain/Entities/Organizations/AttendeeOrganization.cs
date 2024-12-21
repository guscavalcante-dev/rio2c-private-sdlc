// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 10-23-2024
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
        public DateTimeOffset? OnboardingStartDate { get; private set; }
        public DateTimeOffset? OnboardingFinishDate { get; private set; }
        public DateTimeOffset? OnboardingOrganizationDate { get; private set; }
        public DateTimeOffset? OnboardingInterestsDate { get; private set; }
        public DateTimeOffset? ProjectSubmissionOrganizationDate { get; private set; }
        public int SellProjectsCount { get; set; }

        public virtual Edition Edition { get; private set; }
        public virtual Organization Organization { get; private set; }

        public virtual ICollection<AttendeeOrganizationType> AttendeeOrganizationTypes { get; private set; }
        public virtual ICollection<AttendeeOrganizationCollaborator> AttendeeOrganizationCollaborators { get; private set; }
        public virtual ICollection<Project> SellProjects { get; private set; }
        public virtual ICollection<ProjectBuyerEvaluation> ProjectBuyerEvaluations { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeOrganization" /> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="organization">The organization.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeOrganization(
            Edition edition,
            Organization organization,
            OrganizationType organizationType,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            int userId)
        {
            this.Edition = edition;
            this.Organization = organization;
            this.SellProjectsCount = 0;
            this.SynchronizeAttendeeOrganizationTypes(organizationType, isApiDisplayEnabled, apiHighlightPosition, userId);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
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
            this.DeleteOrganizationType(organizationType, userId);
            this.DeleteAttendeeOrganizationCollaborators(userId);
            this.DeleteSellProjects(userId);

            if (this.FindAllAttendeeOrganizationTypesNotDeleted(organizationType)?.Any() != true
                && this.FindAllAttendeeOrganizationCollaboratorsNotDeleted()?.Any() != true
                && this.FindAllSellProjectsNotDeleted()?.Any() != true)
            {
                this.IsDeleted = true;
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Restores the specified organization type.
        /// </summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="isVirtualMeeting">The is virtual meeting.</param>
        /// <param name="userId">The user identifier.</param>
        public void Restore(OrganizationType organizationType, bool? isApiDisplayEnabled, int? apiHighlightPosition, int userId)
        {
            this.SynchronizeAttendeeOrganizationTypes(organizationType, isApiDisplayEnabled, apiHighlightPosition, userId);

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Attendee Organization Types

        /// <summary>
        /// Updates the API configuration.
        /// </summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="isApiDisplayEnabled">if set to <c>true</c> [is API display enabled].</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateApiConfiguration(OrganizationType organizationType, bool isApiDisplayEnabled, int? apiHighlightPosition, int userId)
        {
            this.SynchronizeAttendeeOrganizationTypes(organizationType, isApiDisplayEnabled, apiHighlightPosition, userId);

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Synchronizes the attendee organization types.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeOrganizationTypes(OrganizationType organizationType, bool? isApiDisplayEnabled, int? apiHighlightPosition, int userId)
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
                attendeeOrganizationType.Update(isApiDisplayEnabled, apiHighlightPosition, userId);
            }
            else
            {
                this.AttendeeOrganizationTypes.Add(new AttendeeOrganizationType(this, organizationType, isApiDisplayEnabled, apiHighlightPosition, userId));
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

        /// <summary>Finds the type of the attendee organization types by organization.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <returns></returns>
        private AttendeeOrganizationType FindAttendeeOrganizationTypesByOrganizationType(OrganizationType organizationType)
        {
            return this.AttendeeOrganizationTypes?.FirstOrDefault(aot => !aot.IsDeleted
                                                                         && aot.OrganizationTypeId == organizationType?.Id);
        }

        /// <summary>Deletes the API highlight position.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteApiHighlightPosition(OrganizationType organizationType, int userId)
        {
            var attendeeOrganizationType = this.FindAttendeeOrganizationTypesByOrganizationType(organizationType);
            attendeeOrganizationType?.DeleteApiHighlightPosition(userId);
        }

        #endregion

        #region Attendee Organization Collaborators

        /// <summary>
        /// Associates the collaborator.
        /// </summary>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        public void AssociateCollaborator(AttendeeCollaborator attendeeCollaborator, CollaboratorType collaboratorType, int userId)
        {
            if (this.AttendeeOrganizationCollaborators == null)
            {
                this.AttendeeOrganizationCollaborators = new List<AttendeeOrganizationCollaborator>();
            }

            if (collaboratorType?.Name == CollaboratorType.PlayerExecutiveAudiovisual.Name)
            {
                attendeeCollaborator.SynchronizeAttendeeCollaboratorType(collaboratorType, false, null, userId);
            }

            var attendeeOrganizationCollaboratorDb = this.AttendeeOrganizationCollaborators.FirstOrDefault(aoc => aoc.AttendeeCollaboratorId == attendeeCollaborator.Id);
            if (attendeeOrganizationCollaboratorDb != null)
            {
                attendeeOrganizationCollaboratorDb.Update(userId);
            }
            else
            {
                this.CreateAttendeeOrganizationCollaborator(this, attendeeCollaborator, userId);
            }
        }


        /// <summary>
        /// Disassociates the collaborator.
        /// </summary>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        public void DisassociateCollaborator(AttendeeCollaborator attendeeCollaborator, CollaboratorType collaboratorType, int userId)
        {
            if (this.AttendeeOrganizationCollaborators == null)
            {
                return;
            }

            var attendeeOrganizationCollaborators = this.FindAttendeeOrganizationCollaboratorByAttendeeCollaboratorIdNotDeleted(attendeeCollaborator.Id);
            foreach (var attendeeOrganizationCollaborator in attendeeOrganizationCollaborators)
            {
                attendeeOrganizationCollaborator.Delete(userId);
            }

            if (collaboratorType?.Name == CollaboratorType.PlayerExecutiveAudiovisual.Name)
            {
                //Deactivate the AttendeeCollaborator when disassociated from all AttendeeOrganizations
                if (!attendeeCollaborator.AttendeeOrganizationCollaborators.Any(aoc => !aoc.IsDeleted))
                {
                    attendeeCollaborator.DeleteAttendeeCollaboratorTypeAndAttendeeCollaborator(collaboratorType, userId);
                }
            }
        }

        /// <summary>Deletes the attendee organization collaborators.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeOrganizationCollaborators(int userId)
        {
            foreach (var attendeeOrganizationCollaborator in this.FindAllAttendeeOrganizationCollaboratorsNotDeleted())
            {
                attendeeOrganizationCollaborator.Delete(userId);
            }
        }

        /// <summary>
        /// Creates the attendee organization collaborator.
        /// </summary>
        /// <param name="attendeeOrganization">The attendee organization.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateAttendeeOrganizationCollaborator(AttendeeOrganization attendeeOrganization, AttendeeCollaborator attendeeCollaborator, int userId)
        {
            this.AttendeeOrganizationCollaborators.Add(new AttendeeOrganizationCollaborator(attendeeOrganization, attendeeCollaborator, userId));
        }

        /// <summary>Finds all attendee organization collaborators not deleted.</summary>
        /// <returns></returns>
        private List<AttendeeOrganizationCollaborator> FindAllAttendeeOrganizationCollaboratorsNotDeleted()
        {
            return this.AttendeeOrganizationCollaborators?.Where(aoc => !aoc.IsDeleted)?.ToList();
        }


        private List<AttendeeOrganizationCollaborator> FindAttendeeOrganizationCollaboratorByAttendeeCollaboratorIdNotDeleted(int attendeeCollaboratorId)
        {
            return this.AttendeeOrganizationCollaborators?.Where(aoc => !aoc.IsDeleted 
                                                                        && aoc.AttendeeCollaboratorId == attendeeCollaboratorId)?.ToList();
        }

        #endregion

        #region Onboarding

        /// <summary>Called when [player].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardPlayer(int userId)
        {
            this.OnboardingStartDate = this.OnboardingOrganizationDate = DateTime.UtcNow;

            this.SetUpdateDate(userId);
        }

        /// <summary>Called when [interests].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardInterests(int userId)
        {
            this.OnboardingFinishDate = this.OnboardingInterestsDate = DateTime.UtcNow;

            this.SetUpdateDate(userId);
        }

        /// <summary>
        /// Called when [organization].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardTIcketBuyer(int userId)
        {
            this.OnboardingStartDate = this.OnboardingFinishDate = this.OnboardingOrganizationDate = DateTime.UtcNow;

            this.SetUpdateDate(userId);
        }

        /// <summary>Called when [producer].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardProducer(int userId)
        {
            this.OnboardingStartDate = this.OnboardingFinishDate = this.OnboardingOrganizationDate = this.ProjectSubmissionOrganizationDate = DateTime.UtcNow;

            this.SetUpdateDate(userId);
        }

        #endregion

        #region Projects

        /// <summary>Creates the project.</summary>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="totalPlayingTime">The total playing time.</param>
        /// <param name="numberOfEpisodes">The number of episodes.</param>
        /// <param name="eachEpisodePlayingTime">The each episode playing time.</param>
        /// <param name="valuePerEpisode">The value per episode.</param>
        /// <param name="totalValueOfProject">The total value of project.</param>
        /// <param name="valueAlreadyRaised">The value already raised.</param>
        /// <param name="valueStillNeeded">The value still needed.</param>
        /// <param name="projectTitles">The project titles.</param>
        /// <param name="projectLogLines">The project log lines.</param>
        /// <param name="projectSummaries">The project summaries.</param>
        /// <param name="projectProductionPlans">The project production plans.</param>
        /// <param name="projectAdditionalInformations">The project additional informations.</param>
        /// <param name="projectInterests">The project interests.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="imageLink">The image link.</param>
        /// <param name="teaserLink">The teaser link.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="projectModality">Modality of the project.</param>
        public void CreateProject(
            ProjectType projectType,
            string totalPlayingTime,
            int? numberOfEpisodes,
            string eachEpisodePlayingTime,
            string valuePerEpisode,
            string totalValueOfProject,
            string valueAlreadyRaised,
            string valueStillNeeded,
            List<ProjectTitle> projectTitles,
            List<ProjectLogLine> projectLogLines,
            List<ProjectSummary> projectSummaries,
            List<ProjectProductionPlan> projectProductionPlans,
            List<ProjectAdditionalInformation> projectAdditionalInformations,
            List<ProjectInterest> projectInterests,
            List<TargetAudience> targetAudiences,
            string imageLink,
            string teaserLink,
            int userId,
            ProjectModality projectModality)
        {
            if (this.SellProjects == null)
            {
                this.SellProjects = new List<Project>();
            }

            // Validate collaborator tickets
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            this.SellProjects.Add(
                new Project(
                    projectType,
                    this,
                    totalPlayingTime,
                    numberOfEpisodes,
                    eachEpisodePlayingTime,
                    valuePerEpisode,
                    totalValueOfProject,
                    valueAlreadyRaised,
                    valueStillNeeded,
                    projectTitles,
                    projectLogLines,
                    projectSummaries,
                    projectProductionPlans,
                    projectAdditionalInformations,
                    projectInterests,
                    targetAudiences,
                    imageLink,
                    teaserLink,
                    userId,
                    projectModality
                )
            );

            this.UpdateProjectsCount();
            this.SynchronizeAttendeeOrganizationTypes(projectType.OrganizationTypes?.FirstOrDefault(ot => ot.IsSeller), false, null, userId);
        }

        /// <summary>Gets the last created project.</summary>
        /// <returns></returns>
        public Project GetLastCreatedProject()
        {
            return this.SellProjects?.OrderByDescending(p => p.CreateDate).FirstOrDefault();
        }

        /// <summary>
        /// Deletes the sell projects.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteSellProjects(int userId)
        {
            var sellProjects = this.FindAllSellProjectsNotDeleted();
            if (sellProjects?.Any() != true)
            {
                return;
            }

            sellProjects.ForEach(sp => sp.Delete(userId, true));
        }

        /// <summary>
        /// Finds all sell projects not deleted.
        /// </summary>
        /// <returns></returns>
        private List<Project> FindAllSellProjectsNotDeleted()
        {
            return this.SellProjects?.Where(sp => !sp.IsDeleted)?.ToList();
        }

        #region Projects Counter

        /// <summary>Updates the projects count.</summary>
        public void UpdateProjectsCount()
        {
            this.SellProjectsCount = this.RecountProjects();
        }

        /// <summary>Counts the projects.</summary>
        /// <returns></returns>
        public int RecountProjects()
        {
            return this.SellProjects?.Count(p => !p.IsDeleted) ?? 0;
        }

        /// <summary>Determines whether [has projects available].</summary>
        /// <returns>
        ///   <c>true</c> if [has projects available]; otherwise, <c>false</c>.</returns>
        public bool HasProjectsAvailable()
        {
            return this.SellProjectsCount < this.GetMaxSellProjectsCount();
        }

        #endregion

        #region Edition Limits

        /// <summary>Gets the maximum sell projects count.</summary>
        /// <returns></returns>
        public int GetMaxSellProjectsCount()
        {
            return this.Edition?.AttendeeOrganizationMaxSellProjectsCount ?? 0;
        }

        /// <summary>Gets the project maximum buyer evaluations count.</summary>
        /// <returns></returns>
        public int GetProjectMaxBuyerEvaluationsCount()
        {
            return this.Edition?.ProjectMaxBuyerEvaluationsCount ?? 0;
        }

        #endregion

        #endregion

        #region Helpers

        /// <summary>
        /// Determines whether [is audiovisual player].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual player]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAudiovisualPlayer()
        {
            return this.HasOrganizationType(OrganizationType.AudiovisualPlayer.Name);
        }

        /// <summary>
        /// Determines whether [is music player].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is music player]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMusicPlayer()
        {
            return this.HasOrganizationType(OrganizationType.MusicPlayer.Name);
        }

        /// <summary>
        /// Determines whether [is startup player].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is startup player]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsStartupPlayer()
        {
            return this.HasOrganizationType(OrganizationType.StartupPlayer.Name);
        }

        /// <summary>
        /// Determines whether [has organization type] [the specified organization type name].
        /// </summary>
        /// <param name="organizationTypeName">Name of the organization type.</param>
        /// <returns>
        ///   <c>true</c> if [has organization type] [the specified organization type name]; otherwise, <c>false</c>.
        /// </returns>
        private bool HasOrganizationType(string organizationTypeName)
        {
            if (string.IsNullOrEmpty(organizationTypeName))
            {
                return false;
            }

            return this.AttendeeOrganizationTypes?.Any(aot => !aot.IsDeleted && aot.OrganizationType.Name == organizationTypeName) == true;
        }

        /// <summary>
        /// Determines whether [has any organization type] [the specified organization type names].
        /// </summary>
        /// <param name="organizationTypeNames">The organization type names.</param>
        /// <returns>
        ///   <c>true</c> if [has any organization type] [the specified organization type names]; otherwise, <c>false</c>.
        /// </returns>
        private bool HasAnyOrganizationType(string[] organizationTypeNames)
        {
            if (organizationTypeNames?.Any() != true)
            {
                return false;
            }

            return this.AttendeeOrganizationTypes?.Any(aot => !aot.IsDeleted && organizationTypeNames.Contains(aot.OrganizationType.Name)) == true;
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

            this.ValidateProjectsLimits();
            this.ValidateProjects();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Determines whether [is create business round valid].</summary>
        /// <returns>
        ///   <c>true</c> if [is create business round valid]; otherwise, <c>false</c>.</returns>
        public bool IsCreateBusinessRoundValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }
            this.SellProjectsCount = this.SellProjects?.Count(p =>
                !p.IsDeleted
                && new int[] {
                    ProjectModality.Both.Id,
                    ProjectModality.BusinessRound.Id
                }.Contains(p.ProjectModalityId)
             ) ?? 0;
            this.ValidateBusinessRoundLimits();
            this.ValidateProjects();
            return this.ValidationResult.IsValid;
        }

        /// <summary>Determines whether [is create pitching valid].</summary>
        /// <returns>
        ///   <c>true</c> if [is create pitching valid]; otherwise, <c>false</c>.</returns>
        public bool IsCreatePitchingValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }
            this.SellProjectsCount = this.SellProjects?.Count(p =>
                !p.IsDeleted
                && new int[] {
                    ProjectModality.Both.Id,
                    ProjectModality.Pitching.Id
                }.Contains(p.ProjectModalityId)
             ) ?? 0;
            this.ValidatePitchingLimits();
            this.ValidateProjects();
            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the business rounds limits.</summary>
        public void ValidateBusinessRoundLimits()
        {
            if (this.SellProjectsCount > this.GetMaxSellProjectsCount())
            {
                this.ValidationResult.Add(new ValidationError(Messages.IsNotPossibleCreateBusinessRoundProjectLimit, new string[] { "ToastrError" }));
            }
        }

        /// <summary>Validates the pitching projects limits.</summary>
        public void ValidatePitchingLimits()
        {
            if (this.SellProjectsCount > this.GetMaxSellProjectsCount())
            {
                this.ValidationResult.Add(new ValidationError(Messages.IsNotPossibleCreatePitchingProjectLimit, new string[] { "ToastrError" }));
            }
        }

        /// <summary>Validates the projects limits.</summary>
        public void ValidateProjectsLimits()
        {
            if (this.SellProjectsCount > this.GetMaxSellProjectsCount())
            {
                this.ValidationResult.Add(new ValidationError(Messages.IsNotPossibleCreateProjectLimit, new string[] { "ToastrError" }));
            }
        }

        /// <summary>Validates the projects.</summary>
        public void ValidateProjects()
        {
            if (this.SellProjects?.Any() != true)
            {
                return;
            }

            foreach (var project in this.SellProjects?.Where(d => !d.IsDeleted && !d.IsCreateValid())?.ToList())
            {
                this.ValidationResult.Add(project.ValidationResult);
            }
        }

        #endregion
    }
}