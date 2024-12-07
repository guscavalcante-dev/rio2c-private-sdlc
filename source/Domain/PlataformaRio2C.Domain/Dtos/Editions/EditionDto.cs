// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-06-2019
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 12-02-2024
// ***********************************************************************
// <copyright file="EditionDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>EditionDto</summary>
    public class EditionDto
    {
        public Edition Edition { get; set; }
        public IEnumerable<EditionEventDto> EditionEventDtos { get; set; }

        #region Main Information

        public int Id { get; private set; }
        public Guid Uid { get; private set; }
        public string Name { get; private set; }
        public int UrlCode { get; private set; }
        public bool IsCurrent { get; private set; }
        public bool IsActive { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public DateTimeOffset SellStartDate { get; private set; }
        public DateTimeOffset SellEndDate { get; private set; }
        public DateTimeOffset OneToOneMeetingsScheduleDate { get; private set; }
        public int SpeakersApiHighlightPositionsCount { get; set; }
        public int ConferenceApiHighlightPositionsCount { get; set; }

        #endregion

        #region Dates Information

        // Audiovisual - Negotiations
        public DateTimeOffset ProjectSubmitStartDate { get; private set; }
        public DateTimeOffset ProjectSubmitEndDate { get; private set; }
        public DateTimeOffset ProjectEvaluationStartDate { get; private set; }
        public DateTimeOffset ProjectEvaluationEndDate { get; private set; }
        public DateTimeOffset NegotiationStartDate { get; private set; }
        public DateTimeOffset NegotiationEndDate { get; private set; }
        public DateTimeOffset? AudiovisualNegotiationsCreateStartDate { get; private set; }
        public DateTimeOffset? AudiovisualNegotiationsCreateEndDate { get; private set; }
        public int AttendeeOrganizationMaxSellProjectsCount { get; private set; }
        public int ProjectMaxBuyerEvaluationsCount { get; private set; }

        // Music - Commissions
        public DateTimeOffset MusicProjectSubmitStartDate { get; private set; }
        public DateTimeOffset MusicProjectSubmitEndDate { get; private set; }
        public DateTimeOffset MusicCommissionEvaluationStartDate { get; private set; }
        public DateTimeOffset MusicCommissionEvaluationEndDate { get; private set; }
        public int MusicCommissionMinimumEvaluationsCount { get; private set; }
        public int MusicCommissionMaximumApprovedBandsCount { get; private set; }
        public int MusicPitchingMaximumProjectSubmissionsByEdition { get; private set; }
        public int MusicPitchingMaximumProjectSubmissionsByParticipant { get; private set; }
        public int MusicPitchingMaximumApprovedProjectsByCommissionMember { get; private set; }
        public DateTimeOffset? MusicPitchingCuratorEvaluationStartDate { get; private set; }
        public DateTimeOffset? MusicPitchingCuratorEvaluationEndDate { get; private set; }
        public int MusicPitchingMaximumApprovedProjectsByCurator { get; private set; }
        public DateTimeOffset? MusicPitchingPopularEvaluationStartDate { get; private set; }
        public DateTimeOffset? MusicPitchingPopularEvaluationEndDate { get; private set; }
        public int MusicPitchingMaximumApprovedProjectsByPopularVote { get; private set; }
        public DateTimeOffset? MusicPitchingRepechageEvaluationStartDate { get; private set; }
        public DateTimeOffset? MusicPitchingRepechageEvaluationEndDate { get; private set; }
        public int MusicPitchingMaximumApprovedProjectsByRepechage { get; private set; }

        // Innovation - Commissions
        public DateTimeOffset InnovationProjectSubmitStartDate { get; private set; }
        public DateTimeOffset InnovationProjectSubmitEndDate { get; private set; }
        public DateTimeOffset InnovationCommissionEvaluationStartDate { get; private set; }
        public DateTimeOffset InnovationCommissionEvaluationEndDate { get; private set; }
        public int InnovationCommissionMinimumEvaluationsCount { get; private set; }
        public int InnovationCommissionMaximumApprovedCompaniesCount { get; private set; }

        // Audiovisual - Commissions
        public DateTimeOffset AudiovisualCommissionEvaluationStartDate { get; private set; }
        public DateTimeOffset AudiovisualCommissionEvaluationEndDate { get; private set; }
        public int AudiovisualCommissionMinimumEvaluationsCount { get; private set; }
        public int AudiovisualCommissionMaximumApprovedProjectsCount { get; private set; }

        // Cartoon - Commissions
        public DateTimeOffset? CartoonProjectSubmitStartDate { get; private set; }
        public DateTimeOffset? CartoonProjectSubmitEndDate { get; private set; }
        public DateTimeOffset? CartoonCommissionEvaluationStartDate { get; private set; }
        public DateTimeOffset? CartoonCommissionEvaluationEndDate { get; private set; }
        public int? CartoonCommissionMinimumEvaluationsCount { get; private set; }
        public int? CartoonCommissionMaximumApprovedProjectsCount { get; private set; }

        // Creator - Commissions
        public DateTimeOffset CreatorProjectSubmitStartDate { get; private set; }
        public DateTimeOffset CreatorProjectSubmitEndDate { get; private set; }
        public DateTimeOffset CreatorCommissionEvaluationStartDate { get; private set; }
        public DateTimeOffset CreatorCommissionEvaluationEndDate { get; private set; }
        public int CreatorCommissionMinimumEvaluationsCount { get; private set; }
        public int CreatorCommissionMaximumApprovedCompaniesCount { get; private set; }

        #endregion

        public DateTimeOffset CreateDate { get; private set; }
        public int CreateUserId { get; private set; }
        public DateTimeOffset UpdateDate { get; private set; }
        public int UpdateUserId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditionDto"/> class.
        /// </summary>
        public EditionDto()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="EditionDto"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public EditionDto(Edition entity)
        {
            if (entity == null)
            {
                return;
            }

            this.Edition = entity;

            // Main Information
            this.Id = entity.Id;
            this.Uid = entity.Uid;
            this.Name = entity.Name;
            this.UrlCode = entity.UrlCode;
            this.IsCurrent = entity.IsCurrent;
            this.IsActive = entity.IsActive;
            this.StartDate = entity.StartDate;
            this.EndDate = entity.EndDate;
            this.SellStartDate = entity.SellStartDate;
            this.SellEndDate = entity.SellEndDate;
            this.OneToOneMeetingsScheduleDate = entity.OneToOneMeetingsScheduleDate;
            this.SpeakersApiHighlightPositionsCount = entity.SpeakersApiHighlightPositionsCount;
            this.ConferenceApiHighlightPositionsCount = entity.ConferenceApiHighlightPositionsCount;

            // Audiovisual - Negotiations
            this.ProjectSubmitStartDate = entity.ProjectSubmitStartDate;
            this.ProjectSubmitEndDate = entity.ProjectSubmitEndDate;
            this.ProjectEvaluationStartDate = entity.ProjectEvaluationStartDate;
            this.ProjectEvaluationEndDate = entity.ProjectEvaluationEndDate;
            this.NegotiationStartDate = entity.NegotiationStartDate;
            this.NegotiationEndDate = entity.NegotiationEndDate;
            this.AttendeeOrganizationMaxSellProjectsCount = entity.AttendeeOrganizationMaxSellProjectsCount;
            this.ProjectMaxBuyerEvaluationsCount = entity.ProjectMaxBuyerEvaluationsCount;

            // Music - Commissions
            this.MusicProjectSubmitStartDate = entity.MusicProjectSubmitStartDate;
            this.MusicProjectSubmitEndDate = entity.MusicProjectSubmitEndDate;
            this.MusicCommissionEvaluationStartDate = entity.MusicCommissionEvaluationStartDate;
            this.MusicCommissionEvaluationEndDate = entity.MusicCommissionEvaluationEndDate;
            this.MusicCommissionMinimumEvaluationsCount = entity.MusicCommissionMinimumEvaluationsCount;
            this.MusicCommissionMaximumApprovedBandsCount = entity.MusicCommissionMaximumApprovedBandsCount;
            this.MusicPitchingMaximumProjectSubmissionsByEdition = entity.MusicPitchingMaximumProjectSubmissionsByEdition;
            this.MusicPitchingMaximumProjectSubmissionsByParticipant = entity.MusicPitchingMaximumProjectSubmissionsByParticipant;
            this.MusicPitchingMaximumApprovedProjectsByCommissionMember = entity.MusicPitchingMaximumApprovedProjectsByCommissionMember;
            this.MusicPitchingCuratorEvaluationStartDate = entity.MusicPitchingCuratorEvaluationStartDate;
            this.MusicPitchingCuratorEvaluationEndDate = entity.MusicPitchingCuratorEvaluationEndDate;
            this.MusicPitchingMaximumApprovedProjectsByCurator = entity.MusicPitchingMaximumApprovedProjectsByCurator;
            this.MusicPitchingPopularEvaluationStartDate = entity.MusicPitchingPopularEvaluationStartDate;
            this.MusicPitchingPopularEvaluationEndDate = entity.MusicPitchingPopularEvaluationEndDate;
            this.MusicPitchingMaximumApprovedProjectsByPopularVote = entity.MusicPitchingMaximumApprovedProjectsByPopularVote;
            this.MusicPitchingRepechageEvaluationStartDate = entity.MusicPitchingRepechageEvaluationStartDate;
            this.MusicPitchingRepechageEvaluationEndDate = entity.MusicPitchingRepechageEvaluationEndDate;
            this.MusicPitchingMaximumApprovedProjectsByRepechage = entity.MusicPitchingMaximumApprovedProjectsByRepechage;

            // Innovation - Commissions
            this.InnovationProjectSubmitStartDate = entity.InnovationProjectSubmitStartDate;
            this.InnovationProjectSubmitEndDate = entity.InnovationProjectSubmitEndDate;
            this.InnovationCommissionEvaluationStartDate = entity.InnovationCommissionEvaluationStartDate;
            this.InnovationCommissionEvaluationEndDate = entity.InnovationCommissionEvaluationEndDate;
            this.InnovationCommissionMinimumEvaluationsCount = entity.InnovationCommissionMinimumEvaluationsCount;
            this.InnovationCommissionMaximumApprovedCompaniesCount = entity.InnovationCommissionMaximumApprovedCompaniesCount;

            // Audiovisual - Commissions
            this.AudiovisualNegotiationsCreateStartDate = entity.AudiovisualNegotiationsCreateStartDate;
            this.AudiovisualNegotiationsCreateEndDate = entity.AudiovisualNegotiationsCreateEndDate;
            this.AudiovisualCommissionEvaluationStartDate = entity.AudiovisualCommissionEvaluationStartDate;
            this.AudiovisualCommissionEvaluationEndDate = entity.AudiovisualCommissionEvaluationEndDate;
            this.AudiovisualCommissionMinimumEvaluationsCount = entity.AudiovisualCommissionMinimumEvaluationsCount;
            this.AudiovisualCommissionMaximumApprovedProjectsCount = entity.AudiovisualCommissionMaximumApprovedProjectsCount;

            // Cartoon - Commissions
            this.CartoonProjectSubmitStartDate = entity.CartoonProjectSubmitStartDate;
            this.CartoonProjectSubmitEndDate = entity.CartoonProjectSubmitEndDate;
            this.CartoonCommissionEvaluationStartDate = entity.CartoonCommissionEvaluationStartDate;
            this.CartoonCommissionEvaluationEndDate = entity.CartoonCommissionEvaluationEndDate;
            this.CartoonCommissionMinimumEvaluationsCount = entity.CartoonCommissionMinimumEvaluationsCount;
            this.CartoonCommissionMaximumApprovedProjectsCount = entity.CartoonCommissionMaximumApprovedProjectsCount;

            // Creator - Commissions
            this.CreatorProjectSubmitStartDate = entity.CreatorProjectSubmitStartDate;
            this.CreatorProjectSubmitEndDate = entity.CreatorProjectSubmitEndDate;
            this.CreatorCommissionEvaluationStartDate = entity.CreatorCommissionEvaluationStartDate;
            this.CreatorCommissionEvaluationEndDate = entity.CreatorCommissionEvaluationEndDate;
            this.CreatorCommissionMinimumEvaluationsCount = entity.CreatorCommissionMinimumEvaluationsCount;
            this.CreatorCommissionMaximumApprovedCompaniesCount = entity.CreatorCommissionMaximumApprovedProjectsCount;

            this.CreateDate = entity.CreateDate;
            this.CreateUserId = entity.CreateUserId;
            this.UpdateDate = entity.UpdateDate;
            this.UpdateUserId = entity.UpdateUserId;
        }

        #region Music

        #region Project Submit

        /// <summary>Determines whether [is music project submit open].</summary>
        /// <returns>
        ///   <c>true</c> if [is music project submit open]; otherwise, <c>false</c>.</returns>
        public bool IsMusicProjectSubmitOpen()
        {
            return DateTime.UtcNow >= this.MusicProjectSubmitStartDate && DateTime.UtcNow <= this.MusicProjectSubmitEndDate;
        }

        /// <summary>Determines whether [is music project submit started].</summary>
        /// <returns>
        ///   <c>true</c> if [is music project submit started]; otherwise, <c>false</c>.</returns>
        public bool IsMusicProjectSubmitStarted()
        {
            return DateTime.UtcNow >= this.MusicProjectSubmitStartDate;
        }

        /// <summary>Determines whether [is music project submit ended].</summary>
        /// <returns>
        ///   <c>true</c> if [is music project submit ended]; otherwise, <c>false</c>.</returns>
        public bool IsMusicProjectSubmitEnded()
        {
            return DateTime.UtcNow > this.MusicProjectSubmitEndDate;
        }

        #endregion

        #region Project Evaluation

        /// <summary>Determines whether [is music project evaluation open].</summary>
        /// <returns>
        ///   <c>true</c> if [is music project evaluation open]; otherwise, <c>false</c>.</returns>
        public bool IsMusicPitchingComissionEvaluationOpen()
        {
            return DateTime.UtcNow >= this.MusicCommissionEvaluationStartDate && DateTime.UtcNow <= this.MusicCommissionEvaluationEndDate;
        }

        /// <summary>Determines whether [is music project evaluation started].</summary>
        /// <returns>
        ///   <c>true</c> if [is music project evaluation started]; otherwise, <c>false</c>.</returns>
        public bool IsMusicPitchingCommissionEvaluationStarted()
        {
            return DateTime.UtcNow >= this.MusicCommissionEvaluationStartDate;
        }

        /// <summary>Determines whether [is music project evaluation ended].</summary>
        /// <returns>
        ///   <c>true</c> if [is music project evaluation ended]; otherwise, <c>false</c>.</returns>
        public bool IsMusicPitchingCommissionEvaluationEnded()
        {
            return DateTime.UtcNow > this.MusicCommissionEvaluationEndDate;
        }

        /// <summary>Determines whether [is music project evaluation open].</summary>
        /// <returns>
        ///   <c>true</c> if [is music project evaluation open]; otherwise, <c>false</c>.</returns>
        public bool IsMusicPitchingCuratorEvaluationOpen()
        {
            return !this.IsMusicPitchingComissionEvaluationOpen()
                && DateTime.UtcNow >= this.MusicPitchingCuratorEvaluationStartDate
                && DateTime.UtcNow <= this.MusicPitchingCuratorEvaluationEndDate;
        }

        /// <summary>Determines whether [is music pitching popular evaluation open].</summary>
        /// <returns>
        ///   <c>true</c> if [is music pitching popular evaluation open]; otherwise, <c>false</c>.</returns>
        public bool IsMusicPitchingPopularEvaluationOpen()
        {
            return !this.IsMusicPitchingComissionEvaluationOpen()
                && !this.IsMusicPitchingCuratorEvaluationOpen()
                && DateTime.UtcNow >= this.MusicPitchingPopularEvaluationStartDate
                && DateTime.UtcNow <= this.MusicPitchingPopularEvaluationEndDate;
        }

        /// <summary>Determines whether [is music pitching repechage evaluation open].</summary>
        /// <returns>
        ///   <c>true</c> if [is music pitching repechage evaluation open]; otherwise, <c>false</c>.</returns>
        public bool IsMusicPitchingRepechageEvaluationOpen()
        {
            return !this.IsMusicPitchingComissionEvaluationOpen()
                && !this.IsMusicPitchingCuratorEvaluationOpen()
                && !this.IsMusicPitchingPopularEvaluationOpen()
                && DateTime.UtcNow >= this.MusicPitchingRepechageEvaluationStartDate
                && DateTime.UtcNow <= this.MusicPitchingRepechageEvaluationEndDate;
        }

        #endregion

        #endregion

        #region Innovation

        #region Project Submit

        /// <summary>Determines whether [is innovation project submit open].</summary>
        /// <returns>
        ///   <c>true</c> if [is innovation project submit open]; otherwise, <c>false</c>.</returns>
        public bool IsInnovationProjectSubmitOpen()
        {
            return DateTime.UtcNow >= this.InnovationProjectSubmitStartDate && DateTime.UtcNow <= this.InnovationProjectSubmitEndDate;
        }

        /// <summary>Determines whether [is innovation project submit started].</summary>
        /// <returns>
        ///   <c>true</c> if [is innovation project submit started]; otherwise, <c>false</c>.</returns>
        public bool IsInnovationProjectSubmitStarted()
        {
            return DateTime.UtcNow >= this.InnovationProjectSubmitStartDate;
        }

        /// <summary>Determines whether [is innovation project submit ended].</summary>
        /// <returns>
        ///   <c>true</c> if [is innovation project submit ended]; otherwise, <c>false</c>.</returns>
        public bool IsInnovationProjectSubmitEnded()
        {
            return DateTime.UtcNow > this.InnovationProjectSubmitEndDate;
        }

        #endregion

        #region Project Evaluation

        /// <summary>Determines whether [is innovation project evaluation open].</summary>
        /// <returns>
        ///   <c>true</c> if [is innovation project evaluation open]; otherwise, <c>false</c>.</returns>
        public bool IsInnovationProjectEvaluationOpen()
        {
            return DateTime.UtcNow >= this.InnovationCommissionEvaluationStartDate && DateTime.UtcNow <= this.InnovationCommissionEvaluationEndDate;
        }

        /// <summary>Determines whether [is innovation project evaluation started].</summary>
        /// <returns>
        ///   <c>true</c> if [is innovation project evaluation started]; otherwise, <c>false</c>.</returns>
        public bool IsInnovationProjectEvaluationStarted()
        {
            return DateTime.UtcNow >= this.InnovationCommissionEvaluationStartDate;
        }

        /// <summary>Determines whether [is innovation project evaluation ended].</summary>
        /// <returns>
        ///   <c>true</c> if [is innovation project evaluation ended]; otherwise, <c>false</c>.</returns>
        public bool IsInnovationProjectEvaluationEnded()
        {
            return DateTime.UtcNow > this.InnovationCommissionEvaluationEndDate;
        }

        #endregion

        #endregion

        #region Audiovisual

        #region Project Submit

        /// <summary>
        /// Determines whether [is audiovisual project submit open].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual project submit open]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAudiovisualProjectSubmitOpen()
        {
            return DateTime.UtcNow >= this.ProjectSubmitStartDate && DateTime.UtcNow <= this.ProjectSubmitEndDate;
        }

        /// <summary>
        /// Determines whether [is audiovisual project submit started].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual project submit started]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAudiovisualProjectSubmitStarted()
        {
            return DateTime.UtcNow >= this.ProjectSubmitStartDate;
        }

        /// <summary>
        /// Determines whether [is audiovisual project submit ended].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual project submit ended]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAudiovisualProjectSubmitEnded()
        {
            return DateTime.UtcNow > this.ProjectSubmitEndDate;
        }

        #endregion

        #region Project Evaluation - Commission

        /// <summary>
        /// Determines whether [is audiovisual commission project evaluation open].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual commission project evaluation open]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAudiovisualCommissionProjectEvaluationOpen()
        {
            return DateTime.UtcNow >= this.AudiovisualCommissionEvaluationStartDate && DateTime.UtcNow <= this.AudiovisualCommissionEvaluationEndDate;
        }

        /// <summary>
        /// Determines whether [is audiovisual commission project evaluation started].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual commission project evaluation started]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAudiovisualCommissionProjectEvaluationStarted()
        {
            return DateTime.UtcNow >= this.AudiovisualCommissionEvaluationStartDate;
        }

        /// <summary>
        /// Determines whether [is audiovisual commission project evaluation ended].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual commission project evaluation ended]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAudiovisualCommissionProjectEvaluationEnded()
        {
            return DateTime.UtcNow > this.AudiovisualCommissionEvaluationEndDate;
        }

        #endregion

        #region Project Evaluation - Buyer

        /// <summary>
        /// Determines whether [is project buyer evaluation open].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is project buyer evaluation open]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsProjectBuyerEvaluationOpen()
        {
            return DateTime.UtcNow >= this.ProjectEvaluationStartDate && DateTime.UtcNow <= this.ProjectEvaluationEndDate;
        }

        /// <summary>
        /// Determines whether [is project buyer evaluation started].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is project buyer evaluation started]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsProjectBuyerEvaluationStarted()
        {
            return DateTime.UtcNow >= this.ProjectEvaluationStartDate;
        }

        /// <summary>
        /// Determines whether [is project buyer evaluation released for disclosure].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is project buyer evaluation released for disclosure]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsProjectBuyerEvaluationReleasedForDisclosure()
        {
            return DateTime.UtcNow > this.OneToOneMeetingsScheduleDate;
        }

        #endregion

        #region Project Negotiations

        /// <summary>
        /// Determines whether [is audiovisual project negotiations started].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual project negotiations started]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAudiovisualProjectNegotiationsStarted()
        {
            return DateTime.UtcNow >= this.NegotiationStartDate;
        }

        #endregion

        #endregion

        #region Cartoon

        #region Project Submit

        /// <summary>Determines whether [is Cartoon project submit open].</summary>
        /// <returns>
        ///   <c>true</c> if [is Cartoon project submit open]; otherwise, <c>false</c>.</returns>
        public bool IsCartoonProjectSubmitOpen()
        {
            return DateTime.UtcNow >= this.CartoonProjectSubmitStartDate && DateTime.UtcNow <= this.CartoonProjectSubmitEndDate;
        }

        /// <summary>Determines whether [is Cartoon project submit started].</summary>
        /// <returns>
        ///   <c>true</c> if [is Cartoon project submit started]; otherwise, <c>false</c>.</returns>
        public bool IsCartoonProjectSubmitStarted()
        {
            return DateTime.UtcNow >= this.CartoonProjectSubmitStartDate;
        }

        /// <summary>Determines whether [is Cartoon project submit ended].</summary>
        /// <returns>
        ///   <c>true</c> if [is Cartoon project submit ended]; otherwise, <c>false</c>.</returns>
        public bool IsCartoonProjectSubmitEnded()
        {
            return DateTime.UtcNow > this.CartoonProjectSubmitEndDate;
        }

        #endregion

        #region Project Evaluation

        /// <summary>Determines whether [is Cartoon project evaluation open].</summary>
        /// <returns>
        ///   <c>true</c> if [is Cartoon project evaluation open]; otherwise, <c>false</c>.</returns>
        public bool IsCartoonProjectEvaluationOpen()
        {
            return DateTime.UtcNow >= this.CartoonCommissionEvaluationStartDate && DateTime.UtcNow <= this.CartoonCommissionEvaluationEndDate;
        }

        /// <summary>Determines whether [is Cartoon project evaluation started].</summary>
        /// <returns>
        ///   <c>true</c> if [is Cartoon project evaluation started]; otherwise, <c>false</c>.</returns>
        public bool IsCartoonProjectEvaluationStarted()
        {
            return DateTime.UtcNow >= this.CartoonCommissionEvaluationStartDate;
        }

        /// <summary>Determines whether [is Cartoon project evaluation ended].</summary>
        /// <returns>
        ///   <c>true</c> if [is Cartoon project evaluation ended]; otherwise, <c>false</c>.</returns>
        public bool IsCartoonProjectEvaluationEnded()
        {
            return DateTime.UtcNow > this.CartoonCommissionEvaluationEndDate;
        }

        #endregion

        #endregion

        #region Creator

        #region Project Submit

        public bool IsCreatorProjectSubmitOpen()
        {
            return DateTime.UtcNow >= this.CreatorProjectSubmitStartDate && DateTime.UtcNow <= this.CreatorProjectSubmitEndDate;
        }

        public bool IsCreatorProjectSubmitStarted()
        {
            return DateTime.UtcNow >= this.CreatorProjectSubmitStartDate;
        }

        public bool IsCreatorProjectSubmitEnded()
        {
            return DateTime.UtcNow > this.CreatorProjectSubmitEndDate;
        }

        #endregion

        #region Project Evaluation

        public bool IsCreatorProjectEvaluationOpen()
        {
            return DateTime.UtcNow >= this.CreatorCommissionEvaluationStartDate && DateTime.UtcNow <= this.CreatorCommissionEvaluationEndDate;
        }

        public bool IsCreatorProjectEvaluationStarted()
        {
            return DateTime.UtcNow >= this.CreatorCommissionEvaluationStartDate;
        }

        public bool IsCreatorProjectEvaluationEnded()
        {
            return DateTime.UtcNow > this.CreatorCommissionEvaluationEndDate;
        }

        #endregion

        #endregion
    }
}