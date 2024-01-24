// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-23-2022
// ***********************************************************************
// <copyright file="InnovationOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class InnovationOrganizations.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.AggregateRoot" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.AggregateRoot" />
    public class InnovationOrganization : Entity
    {
        public static readonly int NameMaxLenght = 300;
        public static readonly int DocumentMaxLenght = 50;
        public static readonly int ServiceNameMaxLenght = 300;
        public static readonly int DescriptionMaxLenght = 600;
        public static readonly int WebsiteMaxLenght = 300;

        public string Name { get; private set; }
        public string Document { get; private set; }
        public string ServiceName { get; private set; }
        public string Description { get; private set; }
        public string Website { get; private set; }
        public DateTimeOffset? ImageUploadDate { get; private set; }
        public int? FoundationYear { get; private set; }

        public virtual ICollection<AttendeeInnovationOrganization> AttendeeInnovationOrganizations { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganization" /> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="name">The name.</param>
        /// <param name="document">The document.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="description">The description.</param>
        /// <param name="website">The website.</param>
        /// <param name="accumulatedRevenue">The accumulated revenue.</param>
        /// <param name="businessDefinition">The business definition.</param>
        /// <param name="businessFocus">The business focus.</param>
        /// <param name="marketSize">Size of the market.</param>
        /// <param name="businessEconomicModel">The business economic model.</param>
        /// <param name="businessOperationalModel">The business operational model.</param>
        /// <param name="videoUrl">The video URL.</param>
        /// <param name="businessDifferentials">The business differentials.</param>
        /// <param name="businessStage">The business stage.</param>
        /// <param name="isPresentationUploaded">if set to <c>true</c> [is presentation uploaded].</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="presentationFileExtension">The presentation file extension.</param>
        /// <param name="attendeeInnovationOrganizationFounderApiDtos">The attendee innovation organization founder API dtos.</param>
        /// <param name="attendeeInnovationOrganizationCompetitorApiDtos">The attendee innovation organization competitor API dtos.</param>
        /// <param name="innovationOrganizationExperienceOptionApiDtos">The innovation organization experience option API dtos.</param>
        /// <param name="innovationOrganizationObjectivesOptionApiDtos">The innovation organization objectives option API dtos.</param>
        /// <param name="innovationOrganizationTechnologyOptionApiDtos">The innovation organization technology option API dtos.</param>
        /// <param name="innovationOrganizationTrackOptionApiDtos">The innovation organization track option API dtos.</param>
        /// <param name="innovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos">The innovation organization sustainable development objectives option API dtos.</param>
        /// <param name="wouldYouLikeParticipateBusinessRound">would you like participate business round.</param>
        /// <param name="wouldYouLikeParticipatePitching">if set to <c>true</c> [would you like participate pitching].</param>
        /// <param name="accumulatedRevenueForLastTwelveMonths">accumulated revenue for last twelve months.</param>
        /// <param name="businessFoundationYear">business foundtaion year.</param>
        /// <param name="userId">The user identifier.</param>
        public InnovationOrganization(
            Edition edition,
            AttendeeCollaborator attendeeCollaborator,
            string name,
            string document,
            string serviceName,
            string description,
            string website,
            decimal accumulatedRevenue,
            string businessDefinition,
            string businessFocus,
            string marketSize,
            string businessEconomicModel,
            string businessOperationalModel,
            string videoUrl,
            string businessDifferentials,
            string businessStage,
            bool isPresentationUploaded,
            bool isImageUploaded,
            string presentationFileExtension,
            List<AttendeeInnovationOrganizationFounderApiDto> attendeeInnovationOrganizationFounderApiDtos,
            List<AttendeeInnovationOrganizationCompetitorApiDto> attendeeInnovationOrganizationCompetitorApiDtos,
            List<InnovationOrganizationExperienceOptionApiDto> innovationOrganizationExperienceOptionApiDtos,
            List<InnovationOrganizationObjectivesOptionApiDto> innovationOrganizationObjectivesOptionApiDtos,
            List<InnovationOrganizationTechnologyOptionApiDto> innovationOrganizationTechnologyOptionApiDtos,
            List<InnovationOrganizationTrackOptionApiDto> innovationOrganizationTrackOptionApiDtos,
            List<InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDto> innovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos,
            bool wouldYouLikeParticipateBusinessRound,
            bool wouldYouLikeParticipatePitching,
            decimal? accumulatedRevenueForLastTwelveMonths,
            int? businessFoundationYear,
            int userId)
        {
            this.Name = name;
            this.Document = document.RemoveNonNumeric();
            this.ServiceName = serviceName;
            this.Description = description;
            this.Website = website;
            this.FoundationYear = businessFoundationYear;

            this.UpdateImageUploadDate(isImageUploaded, false);
            base.SetCreateDate(userId);

            this.SynchronizeAttendeeInnovationOrganizations(
                edition,
                accumulatedRevenue,
                marketSize,
                businessDefinition,
                businessFocus,
                businessEconomicModel,
                businessDifferentials,
                businessStage,
                isPresentationUploaded,
                businessOperationalModel,
                videoUrl,
                presentationFileExtension,
                wouldYouLikeParticipateBusinessRound,
                wouldYouLikeParticipatePitching,
                accumulatedRevenueForLastTwelveMonths,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationCollaborators(
                edition,
                attendeeCollaborator,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationFounders(
                edition,
                attendeeInnovationOrganizationFounderApiDtos,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationCompetitors(
                edition,
                attendeeInnovationOrganizationCompetitorApiDtos,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationExperiences(
                edition,
                innovationOrganizationExperienceOptionApiDtos,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationObjectives(
                edition,
                innovationOrganizationObjectivesOptionApiDtos,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationTechnologies(
                edition,
                innovationOrganizationTechnologyOptionApiDtos,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationSustainableDevelopmentObjectives(
               edition,
               innovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos,
               userId); 

            this.SynchronizeAttendeeInnovationOrganizationTracks(
                edition,
                innovationOrganizationTrackOptionApiDtos,
                userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganization"/> class.
        /// </summary>
        public InnovationOrganization()
        {

        }

        /// <summary>
        /// Updates the specified edition.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="name">The name.</param>
        /// <param name="document">The document.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="foundationDate">The foundation date.</param>
        /// <param name="description">The description.</param>
        /// <param name="website">The website.</param>
        /// <param name="accumulatedRevenue">The accumulated revenue.</param>
        /// <param name="businessDefinition">The business definition.</param>
        /// <param name="businessFocus">The business focus.</param>
        /// <param name="marketSize">Size of the market.</param>
        /// <param name="businessEconomicModel">The business economic model.</param>
        /// <param name="businessOperationalModel">The business operational model.</param>
        /// <param name="videoUrl">The video URL.</param>
        /// <param name="businessDifferentials">The business differentials.</param>
        /// <param name="businessStage">The business stage.</param>
        /// <param name="isPresentationUploaded">if set to <c>true</c> [is presentation uploaded].</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        /// <param name="presentationFileExtension">The presentation file extension.</param>
        /// <param name="attendeeInnovationOrganizationFounderApiDtos">The attendee innovation organization founder API dtos.</param>
        /// <param name="attendeeInnovationOrganizationCompetitorApiDtos">The attendee innovation organization competitor API dtos.</param>
        /// <param name="innovationOrganizationExperienceOptionApiDtos">The innovation organization experience option API dtos.</param>
        /// <param name="innovationOrganizationObjectivesOptionApiDtos">The innovation organization objectives option API dtos.</param>
        /// <param name="innovationOrganizationTechnologyOptionApiDtos">The innovation organization technology option API dtos.</param>
        /// <param name="innovationOrganizationTrackOptionApiDtos">The innovation organization track option API dtos.</param>
        /// <param name="innovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos">The innovation organization track option API dtos.</param>
        /// <param name="wouldYouLikeParticipateBusinessRound">would you like participate business round.</param>
        /// <param name="wouldYouLikeParticipatePitching">if set to <c>true</c> [would you like participate pitching].</param>
        /// <param name="accumulatedRevenueForLastTwelveMonths">accumulated revenue for last twelve months.</param>
        /// <param name="businessFoundationYear">business foundtaion year.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            Edition edition,
            AttendeeCollaborator attendeeCollaborator,
            string name,
            string document,
            string serviceName,
            DateTime foundationDate,
            string description,
            string website,
            decimal accumulatedRevenue,
            string businessDefinition,
            string businessFocus,
            string marketSize,
            string businessEconomicModel,
            string businessOperationalModel,
            string videoUrl,
            string businessDifferentials,
            string businessStage,
            bool isPresentationUploaded,
            bool isImageUploaded,
            bool isImageDeleted,
            string presentationFileExtension,
            List<AttendeeInnovationOrganizationFounderApiDto> attendeeInnovationOrganizationFounderApiDtos,
            List<AttendeeInnovationOrganizationCompetitorApiDto> attendeeInnovationOrganizationCompetitorApiDtos,
            List<InnovationOrganizationExperienceOptionApiDto> innovationOrganizationExperienceOptionApiDtos,
            List<InnovationOrganizationObjectivesOptionApiDto> innovationOrganizationObjectivesOptionApiDtos,
            List<InnovationOrganizationTechnologyOptionApiDto> innovationOrganizationTechnologyOptionApiDtos,
            List<InnovationOrganizationTrackOptionApiDto> innovationOrganizationTrackOptionApiDtos,
            List<InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDto> innovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos,
            bool wouldYouLikeParticipateBusinessRound,
            bool wouldYouLikeParticipatePitching,
            decimal? accumulatedRevenueForLastTwelveMonths,
            int? businessFoundationYear,
            int userId)
        {
            this.Name = name;
            this.Document = document.RemoveNonNumeric();
            this.ServiceName = serviceName;
            this.Description = description;
            this.Website = website;
            this.FoundationYear = businessFoundationYear;

            this.UpdateImageUploadDate(isImageUploaded, isImageDeleted);
            base.SetUpdateDate(userId);

            this.SynchronizeAttendeeInnovationOrganizations(
                edition,
                accumulatedRevenue,
                marketSize,
                businessDefinition,
                businessFocus,
                businessEconomicModel,
                businessDifferentials,
                businessStage,
                isPresentationUploaded,
                businessOperationalModel,
                videoUrl,
                presentationFileExtension,
                wouldYouLikeParticipateBusinessRound,
                wouldYouLikeParticipatePitching,
                accumulatedRevenueForLastTwelveMonths,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationCollaborators(
                edition,
                attendeeCollaborator,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationFounders(
                edition,
                attendeeInnovationOrganizationFounderApiDtos,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationCompetitors(
                edition,
                attendeeInnovationOrganizationCompetitorApiDtos,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationExperiences(
                edition,
                innovationOrganizationExperienceOptionApiDtos,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationObjectives(
                edition,
                innovationOrganizationObjectivesOptionApiDtos,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationTechnologies(
                edition,
                innovationOrganizationTechnologyOptionApiDtos,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationTracks(
                edition,
                innovationOrganizationTrackOptionApiDtos,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationSustainableDevelopmentObjectives(
               edition,
               innovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos,
               userId);
        }

        /// <summary>
        /// Deletes the Innovation Organization.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public new void Delete(int userId)
        {
            this.DeleteAttendeeInnovationOrganizations(userId);
            base.Delete(userId);
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
        private void UpdateImageUploadDate(bool isImageUploaded, bool isImageDeleted)
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

        #region Evaluation

        /// <summary>
        /// Evaluates the specified edition.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="evaluatorUser">The evaluator user.</param>
        /// <param name="grade">The grade.</param>
        public void Evaluate(Edition edition, User evaluatorUser, decimal grade)
        {
            var attendeeInnovationOrganization = this.GetAttendeeInnovationOrganizationByEditionId(edition.Id);
            attendeeInnovationOrganization?.Evaluate(evaluatorUser, grade);
        }

        #endregion

        #region Attendee Innovation Organization

        /// <summary>
        /// Synchronizes the attendee innovation organizations.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="accumulatedRevenue">The accumulated revenue.</param>
        /// <param name="marketSize">Size of the market.</param>
        /// <param name="businessDefinition">The business definition.</param>
        /// <param name="businessFocus">The business focus.</param>
        /// <param name="businessEconomicModel">The business economic model.</param>
        /// <param name="businessDifferentials">The business differentials.</param>
        /// <param name="businessStage">The business stage.</param>
        /// <param name="isPresentationUploaded">if set to <c>true</c> [is presentation uploaded].</param>
        /// <param name="businessOperationalModel">The business operational model.</param>
        /// <param name="videoUrl">The video URL.</param>
        /// <param name="presentationFileExtension">The presentation file extension.</param>
        /// <param name="wouldYouLikeParticipateBusinessRound">would you like participate business round.</param>
        /// <param name="wouldYouLikeParticipatePitching">if set to <c>true</c> [would you like participate pitching].</param>
        /// <param name="accumulatedRevenueForLastTwelveMonths">accumulated revenue for last twelve months.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizations(
            Edition edition,
            decimal accumulatedRevenue,
            string marketSize,
            string businessDefinition,
            string businessFocus,
            string businessEconomicModel,
            string businessDifferentials,
            string businessStage,
            bool isPresentationUploaded,
            string businessOperationalModel,
            string videoUrl,
            string presentationFileExtension,
            bool wouldYouLikeParticipateBusinessRound,
            bool wouldYouLikeParticipatePitching,
            decimal? accumulatedRevenueForLastTwelveMonths,
            int userId)
        {
            if (edition == null)
            {
                return;
            }

            if (this.AttendeeInnovationOrganizations == null)
            {
                this.AttendeeInnovationOrganizations = new List<AttendeeInnovationOrganization>();
            }

            var attendeeInnovationOrganization = this.AttendeeInnovationOrganizations.FirstOrDefault(ao => ao.EditionId == edition.Id);
            if (attendeeInnovationOrganization != null)
            {
                attendeeInnovationOrganization.Restore(userId);
            }
            else
            {
                this.AttendeeInnovationOrganizations.Add(
                    new AttendeeInnovationOrganization(
                    edition,
                    this,
                    accumulatedRevenue,
                    marketSize,
                    businessDefinition,
                    businessFocus,
                    businessEconomicModel,
                    businessDifferentials,
                    businessStage,
                    isPresentationUploaded,
                    businessOperationalModel,
                    videoUrl,
                    presentationFileExtension,
                    wouldYouLikeParticipateBusinessRound,
                    wouldYouLikeParticipatePitching,
                    accumulatedRevenueForLastTwelveMonths,
                    userId));
            }
        }

        /// <summary>
        /// Gets the attendee innovation organization by edition identifier.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public AttendeeInnovationOrganization GetAttendeeInnovationOrganizationByEditionId(int editionId)
        {
            return this.AttendeeInnovationOrganizations?.FirstOrDefault(aio => aio.Edition.Id == editionId);
        }

        /// <summary>
        /// Deletes the attendee innovation organizations.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private void DeleteAttendeeInnovationOrganizations(int userId)
        {
            if(this.AttendeeInnovationOrganizations == null)
            {
                return;
            }

            var attendeeInnovationOrganizationsToDelete = this.AttendeeInnovationOrganizations.Where(aio => !aio.IsDeleted).ToList();
            foreach (var attendeeInnovationOrganization in attendeeInnovationOrganizationsToDelete)
            {
                attendeeInnovationOrganization.Delete(userId);
            }
        }

        #endregion

        #region Attendee Innovation Organization Collaborators

        /// <summary>
        /// Synchronizes the attendee innovation organization collaborators.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizationCollaborators(
            Edition edition,
            AttendeeCollaborator attendeeCollaborator,
            int userId)
        {
            var attendeeInnovationOrganization = this.GetAttendeeInnovationOrganizationByEditionId(edition?.Id ?? 0);

            attendeeInnovationOrganization?.SynchronizeAttendeeInnovationOrganizationColaborators(
                     attendeeCollaborator,
                     userId);
        }

        #endregion

        #region Attendee Innovation Organization Founder

        /// <summary>
        /// Synchronizes the attendee innovation organization founders.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="innovationOrganizationFounderOptionApiDtos">The innovation organization founder option API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizationFounders(
            Edition edition,
            List<AttendeeInnovationOrganizationFounderApiDto> innovationOrganizationFounderOptionApiDtos,
            int userId)
        {
            var attendeeInnovationOrganization = this.GetAttendeeInnovationOrganizationByEditionId(edition?.Id ?? 0);

            foreach (var innovationOrganizationFounderOptionApiDto in innovationOrganizationFounderOptionApiDtos)
            {
                attendeeInnovationOrganization?.SynchronizeAttendeeInnovationOrganizationFounders(
                    innovationOrganizationFounderOptionApiDto.WorkDedication,
                    innovationOrganizationFounderOptionApiDto.FullName,
                    innovationOrganizationFounderOptionApiDto.Curriculum,
                    userId);
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #endregion

        #region Attendee Innovation Organization Competitor

        /// <summary>
        /// Synchronizes the attendee innovation organization competitors.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizationCompetitors(
        Edition edition,
        List<AttendeeInnovationOrganizationCompetitorApiDto> attendeeInnovationOrganizationCompetitorApiDtos,
        int userId)
        {
            var attendeeInnovationOrganization = this.GetAttendeeInnovationOrganizationByEditionId(edition?.Id ?? 0);

            foreach (var attendeeInnovationOrganizationCompetitorApiDto in attendeeInnovationOrganizationCompetitorApiDtos)
            {
                attendeeInnovationOrganization?.SynchronizeAttendeeInnovationOrganizationCompetitors(attendeeInnovationOrganizationCompetitorApiDto.Name, userId);
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #endregion

        #region Attendee Innovation Organization Experience

        /// <summary>
        /// Synchronizes the attendee innovation organization experiences.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="innovationOrganizationExperienceOptionApiDtos">The innovation organization experience option API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizationExperiences(
            Edition edition,
            List<InnovationOrganizationExperienceOptionApiDto> innovationOrganizationExperienceOptionApiDtos,
            int userId)
        {
            var attendeeInnovationOrganization = this.GetAttendeeInnovationOrganizationByEditionId(edition?.Id ?? 0);

            foreach (var innovationOrganizationExperienceOptionApiDto in innovationOrganizationExperienceOptionApiDtos)
            {
                attendeeInnovationOrganization?.SynchronizeAttendeeInnovationOrganizationExperiences(
                    innovationOrganizationExperienceOptionApiDto.InnovationOrganizationExperienceOption,
                    innovationOrganizationExperienceOptionApiDto.AdditionalInfo,
                    userId);
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #endregion

        #region Attendee Innovation Organization Objective

        /// <summary>
        /// Synchronizes the attendee innovation organization experiences.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="innovationOrganizationObjectivesOptionApiDtos">The innovation organization objectives option API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizationObjectives(
            Edition edition,
            List<InnovationOrganizationObjectivesOptionApiDto> innovationOrganizationObjectivesOptionApiDtos,
            int userId)
        {
            var attendeeInnovationOrganization = this.GetAttendeeInnovationOrganizationByEditionId(edition?.Id ?? 0);

            foreach (var innovationOrganizationObjectivesOptionApiDto in innovationOrganizationObjectivesOptionApiDtos)
            {
                attendeeInnovationOrganization?.SynchronizeAttendeeInnovationOrganizationObjectives(
                    innovationOrganizationObjectivesOptionApiDto.InnovationOrganizationObjectivesOption,
                    innovationOrganizationObjectivesOptionApiDto.AdditionalInfo,
                    userId);
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #endregion

        #region Attendee Innovation Organization Technology

        /// <summary>
        /// Synchronizes the attendee innovation organization technologies.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="innovationOrganizationTechnologyOptionApiDtos">The innovation organization technology option API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizationTechnologies(
            Edition edition,
            List<InnovationOrganizationTechnologyOptionApiDto> innovationOrganizationTechnologyOptionApiDtos,
            int userId)
        {
            var attendeeInnovationOrganization = this.GetAttendeeInnovationOrganizationByEditionId(edition?.Id ?? 0);

            foreach (var innovationOrganizationTechnologyOptionApiDto in innovationOrganizationTechnologyOptionApiDtos)
            {
                attendeeInnovationOrganization?.SynchronizeAttendeeInnovationOrganizationTechnologies(
                    innovationOrganizationTechnologyOptionApiDto.InnovationOrganizationTechnologyOption,
                    innovationOrganizationTechnologyOptionApiDto.AdditionalInfo,
                    userId);
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #endregion

        #region Attendee Innovation Organization Track

        /// <summary>
        /// Synchronizes the attendee innovation organization tracks.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="innovationOrganizationTrackOptionApiDtos">The innovation organization track option API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizationTracks(
            Edition edition,
            List<InnovationOrganizationTrackOptionApiDto> innovationOrganizationTrackOptionApiDtos,
            int userId)
        {
            var attendeeInnovationOrganization = this.GetAttendeeInnovationOrganizationByEditionId(edition?.Id ?? 0);

            foreach (var innovationOrganizationTrackOptionApiDto in innovationOrganizationTrackOptionApiDtos)
            {
                attendeeInnovationOrganization?.SynchronizeAttendeeInnovationOrganizationTracks(
                    innovationOrganizationTrackOptionApiDto.InnovationOrganizationTrackOption,
                    innovationOrganizationTrackOptionApiDto.AdditionalInfo,
                    userId);
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #endregion

        #region Attendee Innovation Organization Sustainable Developemnt Objectives

        /// <summary>
        /// Synchronizes the attendee innovation Sustainable Development Objectives
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="innovationOrganizationSustainableDevelopmentObjectivesOptionApiDto">The innovation organization Sustainable Development Objectives option API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizationSustainableDevelopmentObjectives(
            Edition edition,
            List<InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDto> innovationOrganizationSustainableDevelopmentObjectivesOptionApiDto,
            int userId)
        {
            var attendeeInnovationOrganization = this.GetAttendeeInnovationOrganizationByEditionId(edition?.Id ?? 0);

            foreach (var objective in innovationOrganizationSustainableDevelopmentObjectivesOptionApiDto)
            {
                attendeeInnovationOrganization?.SynchronizeAttendeeInnovationOrganizationSustainableDevelopmentObjectives(
                    objective.InnovationOrganizationSustainableDevelopmentObjectivesOption,
                    objective.AdditionalInfo,
                    userId);
            }
            base.SetUpdateDate(userId);
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateDescription();
            this.ValidateName();
            this.ValidateDocument();
            this.ValidateServiceName();
            this.ValidateWebsite();
            this.ValidateAttendeeInnovationOrganizations();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the description.
        /// </summary>
        private void ValidateDescription()
        {
            if (this.Description.Length > DescriptionMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Description), DescriptionMaxLenght, 1), new string[] { nameof(Description) }));
            }
        }

        /// <summary>
        /// Validates the name.
        /// </summary>
        private void ValidateName()
        {
            if (this.Name.Length > NameMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Name), NameMaxLenght, 1), new string[] { nameof(Name) }));
            }
        }

        /// <summary>
        /// Validates the document.
        /// </summary>
        private void ValidateDocument()
        {
            if (this.Document.Length > DocumentMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Document), DocumentMaxLenght, 1), new string[] { nameof(Document) }));
            }
        }

        /// <summary>
        /// Validates the name of the service.
        /// </summary>
        private void ValidateServiceName()
        {
            if (this.ServiceName.Length > ServiceNameMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(ServiceName), ServiceNameMaxLenght, 1), new string[] { nameof(ServiceName) }));
            }
        }

        /// <summary>
        /// Validates the website.
        /// </summary>
        private void ValidateWebsite()
        {
            if (this.Website.Length > WebsiteMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Website), WebsiteMaxLenght, 1), new string[] { nameof(Website) }));
            }
        }

        /// <summary>
        /// Validates the attendee innovation organizations.
        /// </summary>
        private void ValidateAttendeeInnovationOrganizations()
        {
            if (this.AttendeeInnovationOrganizations?.Any() != true)
            {
                return;
            }

            foreach (var attendeeInnovationOrganization in this.AttendeeInnovationOrganizations.Where(aiof => !aiof.IsValid()))
            {
                this.ValidationResult.Add(attendeeInnovationOrganization.ValidationResult);
            }
        }

        #endregion
    }
}
