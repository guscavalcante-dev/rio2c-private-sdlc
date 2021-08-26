// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-16-2020
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

            this.CreateDate = entity.CreateDate;
            this.CreateUserId = entity.CreateUserId;
            this.UpdateDate = entity.UpdateDate;
            this.UpdateUserId = entity.UpdateUserId;
        }

        #region Audiovisual - Negotiations

        #region Project Submit

        /// <summary>Determines whether [is project submit open].</summary>
        /// <returns>
        ///   <c>true</c> if [is project submit open]; otherwise, <c>false</c>.</returns>
        public bool IsProjectSubmitOpen()
        {
            return DateTime.UtcNow >= this.ProjectSubmitStartDate && DateTime.UtcNow <= this.ProjectSubmitEndDate;
        }

        /// <summary>Determines whether [is project submit started].</summary>
        /// <returns>
        ///   <c>true</c> if [is project submit started]; otherwise, <c>false</c>.</returns>
        public bool IsProjectSubmitStarted()
        {
            return DateTime.UtcNow >= this.ProjectSubmitStartDate;
        }

        /// <summary>Determines whether [is project submit ended].</summary>
        /// <returns>
        ///   <c>true</c> if [is project submit ended]; otherwise, <c>false</c>.</returns>
        public bool IsProjectSubmitEnded()
        {
            return DateTime.UtcNow > this.ProjectSubmitEndDate;
        }

        #endregion

        #region Project Evaluation

        /// <summary>Determines whether [is project evaluation open].</summary>
        /// <returns>
        ///   <c>true</c> if [is project evaluation open]; otherwise, <c>false</c>.</returns>
        public bool IsProjectEvaluationOpen()
        {
            return DateTime.UtcNow >= this.ProjectEvaluationStartDate && DateTime.UtcNow <= this.ProjectEvaluationEndDate;
        }

        /// <summary>Determines whether [is project evaluation started].</summary>
        /// <returns>
        ///   <c>true</c> if [is project evaluation started]; otherwise, <c>false</c>.</returns>
        public bool IsProjectEvaluationStarted()
        {
            return DateTime.UtcNow >= this.ProjectEvaluationStartDate;
        }

        /// <summary>Determines whether [is project evaluation ended].</summary>
        /// <returns>
        ///   <c>true</c> if [is project evaluation ended]; otherwise, <c>false</c>.</returns>
        public bool IsProjectEvaluationEnded()
        {
            return DateTime.UtcNow > this.ProjectEvaluationEndDate;
        }

        /// <summary>Determines whether [is evaluation released for disclosure].</summary>
        /// <returns>
        ///   <c>true</c> if [is evaluation released for disclosure]; otherwise, <c>false</c>.</returns>
        public bool IsEvaluationReleasedForDisclosure()
        {
            return DateTime.UtcNow > this.OneToOneMeetingsScheduleDate;
        }

        #endregion

        #endregion

        #region Music - Commissions

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
        public bool IsMusicProjectEvaluationOpen()
        {
            return DateTime.UtcNow >= this.MusicCommissionEvaluationStartDate && DateTime.UtcNow <= this.MusicCommissionEvaluationEndDate;
        }

        /// <summary>Determines whether [is music project evaluation started].</summary>
        /// <returns>
        ///   <c>true</c> if [is music project evaluation started]; otherwise, <c>false</c>.</returns>
        public bool IsMusicProjectEvaluationStarted()
        {
            return DateTime.UtcNow >= this.MusicCommissionEvaluationStartDate;
        }

        /// <summary>Determines whether [is music project evaluation ended].</summary>
        /// <returns>
        ///   <c>true</c> if [is music project evaluation ended]; otherwise, <c>false</c>.</returns>
        public bool IsMusicProjectEvaluationEnded()
        {
            return DateTime.UtcNow > this.MusicCommissionEvaluationEndDate;
        }

        #endregion

        #endregion

        #region Innovation - Commissions

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

        #region Audiovisual - Commissions

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

        #region Project Evaluation

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

        #endregion
    }
}