// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-27-2024
// ***********************************************************************
// <copyright file="Edition.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Edition</summary>
    public class Edition : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 50;

        #region MainInformation

        public string Name { get; private set; }
        public int UrlCode { get; private set; }
        public bool IsCurrent { get; private set; }
        public bool IsActive { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public DateTimeOffset SellStartDate { get; private set; }
        public DateTimeOffset SellEndDate { get; private set; }      
        public DateTimeOffset OneToOneMeetingsScheduleDate { get; private set; }
        public int SpeakersApiHighlightPositionsCount { get; private set; }
        public int ConferenceApiHighlightPositionsCount { get; private set; }        

        #endregion

        #region Dates Information

        #region Audiovisual - Business Rounds

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
        public short AudiovisualNegotiationsVirtualMeetingsJoinMinutes { get; private set; }

        #endregion

        #region Audiovisual - Commissions

        public DateTimeOffset AudiovisualCommissionEvaluationStartDate { get; private set; }
        public DateTimeOffset AudiovisualCommissionEvaluationEndDate { get; private set; }
        public int AudiovisualCommissionMinimumEvaluationsCount { get; private set; }
        public int AudiovisualCommissionMaximumApprovedProjectsCount { get; private set; }

        #endregion

        #region Music - Business Rounds

        public int MusicPitchingIndividualMaxSellProjectsCount { get; private set; }
        public int MusicPitchingEntityMaxSellProjectsCount { get; private set; }
        public int MusicBusinessRoundsMaxSellProjectsCount { get; private set; }

        #endregion

        #region Music - Commissions

        public DateTimeOffset MusicProjectSubmitStartDate { get; private set; }
        public DateTimeOffset MusicProjectSubmitEndDate { get; private set; }
        public DateTimeOffset MusicCommissionEvaluationStartDate { get; private set; }
        public DateTimeOffset MusicCommissionEvaluationEndDate { get; private set; }
        public int MusicCommissionMinimumEvaluationsCount { get; private set; }
        public int MusicCommissionMaximumApprovedBandsCount { get; private set; }

        #endregion

        #region Innovation - Business Rounds

        public int InnovationPitchingMaxSellProjectsCount { get; private set; }
        public int InnovationBusinessRoundsMaxSellProjectsCount { get; private set; }

        #endregion

        #region Innovation - Commissions

        public DateTimeOffset InnovationProjectSubmitStartDate { get; private set; }
        public DateTimeOffset InnovationProjectSubmitEndDate { get; private set; }
        public DateTimeOffset InnovationCommissionEvaluationStartDate { get; private set; }
        public DateTimeOffset InnovationCommissionEvaluationEndDate { get; private set; }
        public int InnovationCommissionMinimumEvaluationsCount { get; private set; }
        public int InnovationCommissionMaximumApprovedCompaniesCount { get; private set; }

        #endregion

        #region Cartoon - Commissions

        public DateTimeOffset? CartoonProjectSubmitStartDate { get; private set; }
        public DateTimeOffset? CartoonProjectSubmitEndDate { get; private set; }
        public DateTimeOffset? CartoonCommissionEvaluationStartDate { get; private set; }
        public DateTimeOffset? CartoonCommissionEvaluationEndDate { get; private set; }
        public int? CartoonCommissionMinimumEvaluationsCount { get; private set; }
        public int? CartoonCommissionMaximumApprovedProjectsCount { get; private set; }

        #endregion

        #region Creator - Commissions

        public DateTimeOffset CreatorProjectSubmitStartDate { get; private set; }
        public DateTimeOffset CreatorProjectSubmitEndDate { get; private set; }
        public DateTimeOffset CreatorCommissionEvaluationStartDate { get; private set; }
        public DateTimeOffset CreatorCommissionEvaluationEndDate { get; private set; }
        public int CreatorCommissionMinimumEvaluationsCount { get; private set; }
        public int CreatorCommissionMaximumApprovedProjectsCount { get; private set; }

        #endregion

        #endregion

        public virtual ICollection<AttendeeOrganization> AttendeeOrganizations { get; private set; }
        public virtual ICollection<AttendeeCollaborator> AttendeeCollaborators { get; private set; }
        public virtual ICollection<AttendeeSalesPlatform> AttendeeSalesPlatforms { get; private set; }
        public virtual ICollection<EditionEvent> EditionEvents { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Edition" /> class.</summary>
        /// <param name="uid">The uid.</param>
        /// <param name="name">The name.</param>
        /// <param name="urlCode">The URL code.</param>
        /// <param name="isCurrent">if set to <c>true</c> [is current].</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="sellStartDate">The sell start date.</param>
        /// <param name="sellEndDate">The sell end date.</param>
        /// <param name="oneToOneMeetingsScheduleDate">The one to one meetings schedule date.</param>
        /// <param name="projectSubmitStartDate">The project submit start date.</param>
        /// <param name="projectSubmitEndDate">The project submit end date.</param>
        /// <param name="projectEvaluationStartDate">The project evaluation start date.</param>
        /// <param name="projectEvaluationEndDate">The project evaluation end date.</param>
        /// <param name="negotiationStartDate">The negotiation start date.</param>
        /// <param name="negotiationEndDate">The negotiation end date.</param>
        /// <param name="attendeeOrganizationMaxSellProjectsCount">The attendee organization maximum sell projects count.</param>
        /// <param name="projectMaxBuyerEvaluationsCount">The project maximum buyer evaluations count.</param>
        /// <param name="audiovisualNegotiationsVirtualMeetingsJoinMinutes">The audiovisual negotiations virtual meetings join minutes.</param>
        /// <param name="musicProjectSubmitStartDate">The music project submit start date.</param>
        /// <param name="musicProjectSubmitEndDate">The music project submit end date.</param>
        /// <param name="musicCommissionEvaluationStartDate">The music commission evaluation start date.</param>
        /// <param name="musicCommissionEvaluationEndDate">The music commission evaluation end date.</param>
        /// <param name="musicCommissionMinimumEvaluationsCount">The music commission minimum evaluations count.</param>
        /// <param name="musicCommissionMaximumApprovedBandsCount">The music commission maximum approved bands count.</param>
        /// <param name="innovationProjectSubmitStartDate">The innovation project submit start date.</param>
        /// <param name="innovationProjectSubmitEndDate">The innovation project submit end date.</param>
        /// <param name="innovationCommissionEvaluationStartDate">The innovation commission evaluation start date.</param>
        /// <param name="innovationCommissionEvaluationEndDate">The innovation commission evaluation end date.</param>
        /// <param name="innovationCommissionMinimumEvaluationsCount">The innovation commission minimum evaluations count.</param>
        /// <param name="innovationCommissionMaximumApprovedCompaniesCount">The innovation commission maximum approved companies count.</param>
        /// <param name="audiovisualCommissionEvaluationStartDate">The audiovisual commission evaluation start date.</param>
        /// <param name="audiovisualCommissionEvaluationEndDate">The audiovisual commission evaluation end date.</param>
        /// <param name="audiovisualCommissionMinimumEvaluationsCount">The audiovisual commission minimum evaluations count.</param>
        /// <param name="audiovisualCommissionMaximumApprovedProjectsCount">The audiovisual commission maximum approved projects count.</param>
        /// <param name="cartoonProjectSubmitStartDate">The cartoon project submit start date.</param>
        /// <param name="cartoonProjectSubmitEndDate">The cartoon project submit end date.</param>
        /// <param name="cartoonCommissionEvaluationStartDate">The cartoon commission evaluation start date.</param>
        /// <param name="cartoonCommissionEvaluationEndDate">The cartoon commission evaluation end date.</param>
        /// <param name="cartoonCommissionMinimumEvaluationsCount">The cartoon commission minimum evaluations count.</param>
        /// <param name="cartoonCommissionMaximumApprovedProjectsCount">The cartoon commission maximum approved companies count.</param>
        /// <param name="creatorProjectSubmitStartDate"></param>
        /// <param name="creatorProjectSubmitEndDate"></param>
        /// <param name="creatorCommissionEvaluationStartDate"></param>
        /// <param name="creatorCommissionEvaluationEndDate"></param>
        /// <param name="creatorCommissionMinimumEvaluationsCount"></param>
        /// <param name="creatorCommissionMaximumApprovedProjectsCount"></param>
        /// <param name="userId">The user identifier.</param>
        public Edition(
            Guid uid,
            string name,
            int urlCode,
            bool isCurrent,
            bool isActive,
            DateTime startDate,
            DateTime endDate,
            DateTime sellStartDate,
            DateTime sellEndDate,
            DateTime oneToOneMeetingsScheduleDate,

            DateTime projectSubmitStartDate,
            DateTime projectSubmitEndDate,
            DateTime projectEvaluationStartDate,
            DateTime projectEvaluationEndDate,
            DateTime negotiationStartDate,
            DateTime negotiationEndDate,
            int attendeeOrganizationMaxSellProjectsCount,
            int projectMaxBuyerEvaluationsCount,
            short audiovisualNegotiationsVirtualMeetingsJoinMinutes,

            DateTime musicProjectSubmitStartDate,
            DateTime musicProjectSubmitEndDate,
            DateTime musicCommissionEvaluationStartDate,
            DateTime musicCommissionEvaluationEndDate,
            int musicCommissionMinimumEvaluationsCount,
            int musicCommissionMaximumApprovedBandsCount,

            DateTime innovationProjectSubmitStartDate,
            DateTime innovationProjectSubmitEndDate,
            DateTime innovationCommissionEvaluationStartDate,
            DateTime innovationCommissionEvaluationEndDate,
            int innovationCommissionMinimumEvaluationsCount,
            int innovationCommissionMaximumApprovedCompaniesCount,
                        
            DateTime audiovisualCommissionEvaluationStartDate,
            DateTime audiovisualCommissionEvaluationEndDate,
            int audiovisualCommissionMinimumEvaluationsCount,
            int audiovisualCommissionMaximumApprovedProjectsCount,

            DateTime? cartoonProjectSubmitStartDate,
            DateTime? cartoonProjectSubmitEndDate,
            DateTime? cartoonCommissionEvaluationStartDate,
            DateTime? cartoonCommissionEvaluationEndDate,
            int? cartoonCommissionMinimumEvaluationsCount,
            int? cartoonCommissionMaximumApprovedProjectsCount,

            DateTime creatorProjectSubmitStartDate,
            DateTime creatorProjectSubmitEndDate,
            DateTime creatorCommissionEvaluationStartDate,
            DateTime creatorCommissionEvaluationEndDate,
            int creatorCommissionMinimumEvaluationsCount,
            int creatorCommissionMaximumApprovedProjectsCount,

            int userId)
        {
            // Main Information
            this.Name = name;
            this.UrlCode = urlCode;
            this.IsCurrent = isCurrent;
            this.IsActive = isActive;
            this.StartDate = startDate.ToUtcTimeZone();
            this.EndDate = endDate.ToEndDateTimeOffset();
            this.SellStartDate = sellStartDate.ToUtcTimeZone();
            this.SellEndDate = sellEndDate.ToEndDateTimeOffset();

            // Audiovisual - Negotiations
            this.ProjectSubmitStartDate = projectSubmitStartDate.ToUtcTimeZone();
            this.ProjectSubmitEndDate = projectSubmitEndDate.ToEndDateTimeOffset();
            this.ProjectEvaluationStartDate = projectEvaluationStartDate.ToUtcTimeZone();
            this.ProjectEvaluationEndDate = projectEvaluationEndDate.ToEndDateTimeOffset();
            this.OneToOneMeetingsScheduleDate = oneToOneMeetingsScheduleDate.ToUtcTimeZone();
            this.NegotiationStartDate = negotiationStartDate.ToUtcTimeZone();
            this.NegotiationEndDate = negotiationEndDate.ToEndDateTimeOffset();
            this.AttendeeOrganizationMaxSellProjectsCount = attendeeOrganizationMaxSellProjectsCount;
            this.ProjectMaxBuyerEvaluationsCount = projectMaxBuyerEvaluationsCount;
            this.AudiovisualNegotiationsVirtualMeetingsJoinMinutes = audiovisualNegotiationsVirtualMeetingsJoinMinutes;

            // Music - Commissions
            this.MusicProjectSubmitStartDate = musicProjectSubmitStartDate.ToUtcTimeZone();
            this.MusicProjectSubmitEndDate = musicProjectSubmitEndDate.ToEndDateTimeOffset();
            this.MusicCommissionEvaluationStartDate = musicCommissionEvaluationStartDate.ToUtcTimeZone();
            this.MusicCommissionEvaluationEndDate = musicCommissionEvaluationEndDate.ToEndDateTimeOffset();
            this.MusicCommissionMinimumEvaluationsCount = musicCommissionMinimumEvaluationsCount;
            this.MusicCommissionMaximumApprovedBandsCount = musicCommissionMaximumApprovedBandsCount;

            // Innovation - Commissions
            this.InnovationProjectSubmitStartDate = innovationProjectSubmitStartDate.ToUtcTimeZone();
            this.InnovationProjectSubmitEndDate = innovationProjectSubmitEndDate.ToEndDateTimeOffset();
            this.InnovationCommissionEvaluationStartDate = innovationCommissionEvaluationStartDate.ToUtcTimeZone();
            this.InnovationCommissionEvaluationEndDate = innovationCommissionEvaluationEndDate.ToEndDateTimeOffset();
            this.InnovationCommissionMinimumEvaluationsCount = innovationCommissionMinimumEvaluationsCount;
            this.InnovationCommissionMaximumApprovedCompaniesCount = innovationCommissionMaximumApprovedCompaniesCount;

            // Audiovisual - Commissions           
            this.AudiovisualCommissionEvaluationStartDate = audiovisualCommissionEvaluationStartDate.ToUtcTimeZone();
            this.AudiovisualCommissionEvaluationEndDate = audiovisualCommissionEvaluationEndDate.ToEndDateTimeOffset();
            this.AudiovisualCommissionMinimumEvaluationsCount = audiovisualCommissionMinimumEvaluationsCount;
            this.AudiovisualCommissionMaximumApprovedProjectsCount = audiovisualCommissionMaximumApprovedProjectsCount;

            // Cartoon - Commissions
            this.CartoonProjectSubmitStartDate = cartoonProjectSubmitStartDate?.ToUtcTimeZone();
            this.CartoonProjectSubmitEndDate = cartoonProjectSubmitEndDate?.ToEndDateTimeOffset();
            this.CartoonCommissionEvaluationStartDate = cartoonCommissionEvaluationStartDate?.ToUtcTimeZone();
            this.CartoonCommissionEvaluationEndDate = cartoonCommissionEvaluationEndDate?.ToEndDateTimeOffset();
            this.CartoonCommissionMinimumEvaluationsCount = cartoonCommissionMinimumEvaluationsCount;
            this.CartoonCommissionMaximumApprovedProjectsCount = cartoonCommissionMaximumApprovedProjectsCount;

            // Creator - Commissions
            this.CreatorProjectSubmitStartDate = creatorProjectSubmitStartDate.ToUtcTimeZone();
            this.CreatorProjectSubmitEndDate = creatorProjectSubmitEndDate.ToEndDateTimeOffset();
            this.CreatorCommissionEvaluationStartDate = creatorCommissionEvaluationStartDate.ToUtcTimeZone();
            this.CreatorCommissionEvaluationEndDate = creatorCommissionEvaluationEndDate.ToEndDateTimeOffset();
            this.CreatorCommissionMinimumEvaluationsCount = creatorCommissionMinimumEvaluationsCount;
            this.CreatorCommissionMaximumApprovedProjectsCount = creatorCommissionMaximumApprovedProjectsCount;

            base.SetCreateDate(userId);
        }

        /// <summary>Initializes a new instance of the <see cref="Edition"/> class.</summary>
        protected Edition()
        {
        }

        /// <summary>
        /// Updates the main information.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="urlCode">The URL code.</param>
        /// <param name="isCurrent">if set to <c>true</c> [is current].</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="sellStartDate">The sell start date.</param>
        /// <param name="sellEndDate">The sell end date.</param>
        /// <param name="oneToOneMeetingsScheduleDate">The one to one meetings schedule date.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(
            string name,
            int urlCode,
            bool isCurrent,
            bool isActive,
            DateTime startDate,
            DateTime endDate,
            DateTime sellStartDate,
            DateTime sellEndDate,
            DateTime oneToOneMeetingsScheduleDate,
            int speakersApiHighlightPositionsCount,
            int userId)
        {
            this.Name = name;
            this.UrlCode = urlCode;
            this.IsCurrent = isCurrent;
            this.IsActive = isActive;
            this.StartDate = startDate.ToUtcTimeZone();
            this.EndDate = endDate.ToEndDateTimeOffset();
            this.SellStartDate = sellStartDate.ToUtcTimeZone();
            this.SellEndDate = sellEndDate.ToEndDateTimeOffset();
            this.OneToOneMeetingsScheduleDate = oneToOneMeetingsScheduleDate.ToUtcTimeZone();
            this.SpeakersApiHighlightPositionsCount = speakersApiHighlightPositionsCount;

            base.SetUpdateDate(userId);
        }

        /// <summary>
        /// Updates the dates information.
        /// </summary>
        /// <param name="projectSubmitStartDate">The project submit start date.</param>
        /// <param name="projectSubmitEndDate">The project submit end date.</param>
        /// <param name="projectEvaluationStartDate">The project evaluation start date.</param>
        /// <param name="projectEvaluationEndDate">The project evaluation end date.</param>
        /// <param name="negotiationStartDate">The negotiation start date.</param>
        /// <param name="negotiationEndDate">The negotiation end date.</param>
        /// <param name="attendeeOrganizationMaxSellProjectsCount">The attendee organization maximum sell projects count.</param>
        /// <param name="projectMaxBuyerEvaluationsCount">The project maximum buyer evaluations count.</param>
        /// <param name="audiovisualNegotiationsVirtualMeetingsJoinMinutes">The audiovisual negotiations virtual meetings join minutes.</param>
        /// <param name="musicProjectSubmitStartDate">The music project submit start date.</param>
        /// <param name="musicProjectSubmitEndDate">The music project submit end date.</param>
        /// <param name="musicCommissionEvaluationStartDate">The music commission evaluation start date.</param>
        /// <param name="musicCommissionEvaluationEndDate">The music commission evaluation end date.</param>
        /// <param name="musicCommissionMinimumEvaluationsCount">The music commission minimum evaluations count.</param>
        /// <param name="musicCommissionMaximumApprovedBandsCount">The music commission maximum approved bands count.</param>
        /// <param name="innovationProjectSubmitStartDate">The innovation project submit start date.</param>
        /// <param name="innovationProjectSubmitEndDate">The innovation project submit end date.</param>
        /// <param name="innovationCommissionEvaluationStartDate">The innovation commission evaluation start date.</param>
        /// <param name="innovationCommissionEvaluationEndDate">The innovation commission evaluation end date.</param>
        /// <param name="innovationCommissionMinimumEvaluationsCount">The innovation commission minimum evaluations count.</param>
        /// <param name="innovationCommissionMaximumApprovedCompaniesCount">The innovation commission maximum approved companies count.</param>
        /// <param name="audiovisualCommissionEvaluationStartDate">The audiovisual commission evaluation start date.</param>
        /// <param name="audiovisualCommissionEvaluationEndDate">The audiovisual commission evaluation end date.</param>
        /// <param name="audiovisualCommissionMinimumEvaluationsCount">The audiovisual commission minimum evaluations count.</param>
        /// <param name="audiovisualCommissionMaximumApprovedProjectsCount">The audiovisual commission maximum approved projects count.</param>
        /// <param name="cartoonProjectSubmitStartDate">The cartoon project submit start date.</param>
        /// <param name="cartoonProjectSubmitEndDate">The cartoon project submit end date.</param>
        /// <param name="cartoonCommissionEvaluationStartDate">The cartoon commission evaluation start date.</param>
        /// <param name="cartoonCommissionEvaluationEndDate">The cartoon commission evaluation end date.</param>
        /// <param name="cartoonCommissionMinimumEvaluationsCount">The cartoon commission minimum evaluations count.</param>
        /// <param name="cartoonCommissionMaximumApprovedProjectsCount">The cartoon commission maximum approved projects count.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateDatesInformation(     
            DateTime projectSubmitStartDate,
            DateTime projectSubmitEndDate,
            DateTime projectEvaluationStartDate,
            DateTime projectEvaluationEndDate,
            DateTime negotiationStartDate,
            DateTime negotiationEndDate,
            int attendeeOrganizationMaxSellProjectsCount,
            int projectMaxBuyerEvaluationsCount,
            short audiovisualNegotiationsVirtualMeetingsJoinMinutes,

            DateTime musicProjectSubmitStartDate,
            DateTime musicProjectSubmitEndDate,
            DateTime musicCommissionEvaluationStartDate,
            DateTime musicCommissionEvaluationEndDate,
            int musicCommissionMinimumEvaluationsCount,
            int musicCommissionMaximumApprovedBandsCount,

            DateTime innovationProjectSubmitStartDate,
            DateTime innovationProjectSubmitEndDate,
            DateTime innovationCommissionEvaluationStartDate,
            DateTime innovationCommissionEvaluationEndDate,
            int innovationCommissionMinimumEvaluationsCount,
            int innovationCommissionMaximumApprovedCompaniesCount,
                       
            DateTime audiovisualCommissionEvaluationStartDate,
            DateTime audiovisualCommissionEvaluationEndDate,
            int audiovisualCommissionMinimumEvaluationsCount,
            int audiovisualCommissionMaximumApprovedProjectsCount,

            DateTime? cartoonProjectSubmitStartDate,
            DateTime? cartoonProjectSubmitEndDate,
            DateTime? cartoonCommissionEvaluationStartDate,
            DateTime? cartoonCommissionEvaluationEndDate,
            int? cartoonCommissionMinimumEvaluationsCount,
            int? cartoonCommissionMaximumApprovedProjectsCount,

            DateTime creatorProjectSubmitStartDate,
            DateTime creatorProjectSubmitEndDate,
            DateTime creatorCommissionEvaluationStartDate,
            DateTime creatorCommissionEvaluationEndDate,
            int creatorCommissionMinimumEvaluationsCount,
            int creatorCommissionMaximumApprovedProjectsCount,

            int userId)
        {
            this.ProjectSubmitStartDate = projectSubmitStartDate.ToUtcTimeZone();
            this.ProjectSubmitEndDate = projectSubmitEndDate.ToEndDateTimeOffset();
            this.ProjectEvaluationStartDate = projectEvaluationStartDate.ToUtcTimeZone();
            this.ProjectEvaluationEndDate = projectEvaluationEndDate.ToEndDateTimeOffset();
            this.NegotiationStartDate = negotiationStartDate.ToUtcTimeZone();
            this.NegotiationEndDate = negotiationEndDate.ToEndDateTimeOffset();
            this.AttendeeOrganizationMaxSellProjectsCount = attendeeOrganizationMaxSellProjectsCount;
            this.ProjectMaxBuyerEvaluationsCount = projectMaxBuyerEvaluationsCount;
            this.AudiovisualNegotiationsVirtualMeetingsJoinMinutes = audiovisualNegotiationsVirtualMeetingsJoinMinutes;

            this.MusicProjectSubmitStartDate = musicProjectSubmitStartDate.ToUtcTimeZone();
            this.MusicProjectSubmitEndDate = musicProjectSubmitEndDate.ToEndDateTimeOffset();
            this.MusicCommissionEvaluationStartDate = musicCommissionEvaluationStartDate.ToUtcTimeZone();
            this.MusicCommissionEvaluationEndDate = musicCommissionEvaluationEndDate.ToEndDateTimeOffset();
            this.MusicCommissionMinimumEvaluationsCount = musicCommissionMinimumEvaluationsCount;
            this.MusicCommissionMaximumApprovedBandsCount = musicCommissionMaximumApprovedBandsCount;

            this.InnovationProjectSubmitStartDate = innovationProjectSubmitStartDate.ToUtcTimeZone();
            this.InnovationProjectSubmitEndDate = innovationProjectSubmitEndDate.ToEndDateTimeOffset();
            this.InnovationCommissionEvaluationStartDate = innovationCommissionEvaluationStartDate.ToUtcTimeZone();
            this.InnovationCommissionEvaluationEndDate = innovationCommissionEvaluationEndDate.ToEndDateTimeOffset();
            this.InnovationCommissionMinimumEvaluationsCount = innovationCommissionMinimumEvaluationsCount;
            this.InnovationCommissionMaximumApprovedCompaniesCount = innovationCommissionMaximumApprovedCompaniesCount;

            this.AudiovisualCommissionEvaluationStartDate = audiovisualCommissionEvaluationStartDate.ToUtcTimeZone();
            this.AudiovisualCommissionEvaluationEndDate = audiovisualCommissionEvaluationEndDate.ToEndDateTimeOffset();
            this.AudiovisualCommissionMinimumEvaluationsCount = audiovisualCommissionMinimumEvaluationsCount;
            this.AudiovisualCommissionMaximumApprovedProjectsCount = audiovisualCommissionMaximumApprovedProjectsCount;

            this.CartoonProjectSubmitStartDate = cartoonProjectSubmitStartDate?.ToUtcTimeZone();
            this.CartoonProjectSubmitEndDate = cartoonProjectSubmitEndDate?.ToEndDateTimeOffset();
            this.CartoonCommissionEvaluationStartDate = cartoonCommissionEvaluationStartDate?.ToUtcTimeZone();
            this.CartoonCommissionEvaluationEndDate = cartoonCommissionEvaluationEndDate?.ToEndDateTimeOffset();
            this.CartoonCommissionMinimumEvaluationsCount = cartoonCommissionMinimumEvaluationsCount;
            this.CartoonCommissionMaximumApprovedProjectsCount = cartoonCommissionMaximumApprovedProjectsCount;

            this.CreatorProjectSubmitStartDate = creatorProjectSubmitStartDate.ToUtcTimeZone();
            this.CreatorProjectSubmitEndDate = creatorProjectSubmitEndDate.ToEndDateTimeOffset();
            this.CreatorCommissionEvaluationStartDate = creatorCommissionEvaluationStartDate.ToUtcTimeZone();
            this.CreatorCommissionEvaluationEndDate = creatorCommissionEvaluationEndDate.ToEndDateTimeOffset();
            this.CreatorCommissionMinimumEvaluationsCount = creatorCommissionMinimumEvaluationsCount;
            this.CreatorCommissionMaximumApprovedProjectsCount = creatorCommissionMaximumApprovedProjectsCount;

            base.SetUpdateDate(userId);
        }

        #region Actions

        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public new void Delete(int userId)
        {
            this.IsDeleted = true;
            this.DeleteEditionEvents(userId);

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Starts the audiovisual negotiations creation.</summary>
        /// <param name="userId">The user identifier.</param>
        public void StartAudiovisualNegotiationsCreation(int userId)
        {
            this.AudiovisualNegotiationsCreateStartDate = DateTime.UtcNow;
            this.AudiovisualNegotiationsCreateEndDate = null;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Finishes the audiovisual negotiations creation.</summary>
        /// <param name="userId">The user identifier.</param>
        public void FinishAudiovisualNegotiationsCreation(int userId)
        {
            this.AudiovisualNegotiationsCreateEndDate = DateTime.UtcNow;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Cancels the audiovisual negotiations creation.</summary>
        /// <param name="userId">The user identifier.</param>
        public void CancelAudiovisualNegotiationsCreation(int userId)
        {
            this.AudiovisualNegotiationsCreateStartDate = null;
            this.AudiovisualNegotiationsCreateEndDate = null;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Disables the is current.
        /// </summary>
        public void DisableIsCurrent()
        {
            this.IsCurrent = false;
        }

        /// <summary>
        /// Deletes the edition events.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteEditionEvents(int userId)
        {
            if (this.EditionEvents?.Any() != true)
            {
                return;
            }

            foreach (var editionEvent in this.EditionEvents.Where(c => !c.IsDeleted))
            {
                editionEvent.Delete(userId);
            }
        }

        #endregion

        #region Music - Commissions

        #region Project Evaluation

        /// <summary>Determines whether [is music project evaluation open].</summary>
        /// <returns>
        ///   <c>true</c> if [is music project evaluation open]; otherwise, <c>false</c>.</returns>
        public bool IsMusicProjectEvaluationOpen()
        {
            return DateTime.UtcNow >= this.MusicCommissionEvaluationStartDate && DateTime.UtcNow <= this.MusicCommissionEvaluationEndDate;
        }

        #endregion

        #endregion

        #region Innovation - Commissions

        #region Project Evaluation

        /// <summary>Determines whether [is innovation project evaluation open].</summary>
        /// <returns>
        ///   <c>true</c> if [is innovation project evaluation open]; otherwise, <c>false</c>.</returns>
        public bool IsInnovationProjectEvaluationOpen()
        {
            return DateTime.UtcNow >= this.InnovationCommissionEvaluationStartDate && DateTime.UtcNow <= this.InnovationCommissionEvaluationEndDate;
        }

        #endregion

        #endregion

        #region Creator - Commissions

        #region Project Evaluation

        /// <summary>
        /// Determines whether [is creator project evaluation open].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is creator project evaluation open]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCreatorProjectEvaluationOpen()
        {
            return DateTime.UtcNow >= this.CreatorCommissionEvaluationStartDate && DateTime.UtcNow <= this.CreatorCommissionEvaluationEndDate;
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

            this.ValidateName();
            this.ValidateUrlCode();
            this.ValidateIsCurrentAndIsActive();
            this.ValidateIsCurrentAndIsDeleted();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the name.
        /// </summary>
        public void ValidateName()
        {
            if (string.IsNullOrEmpty(this.Name?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "Name" }));
            }

            if (this.Name?.Trim().Length < NameMinLength || this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, NameMinLength), new string[] { "Name" }));
            }
        }

        /// <summary>
        /// Validates the URL code.
        /// </summary>
        public void ValidateUrlCode()
        {
            if (this.UrlCode < 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanValue, Labels.Code, 0), new string[] { "UrlCode" }));
            }
        }

        /// <summary>
        /// Validates the is current and is active.
        /// </summary>
        public void ValidateIsCurrentAndIsActive()
        {
            if(this.IsCurrent && !this.IsActive)
            {
                this.ValidationResult.Add(new ValidationError(Messages.CanNotDisableCurrentEdition, new string[] { "IsActive" }));
            }
        }

        /// <summary>
        /// Validates the is current and is deleted.
        /// </summary>
        public void ValidateIsCurrentAndIsDeleted()
        {
            if (this.IsCurrent && this.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(Messages.CanNotDeleteCurrentEdition, new string[] { "ToastrError" }));
            }
        }

        #endregion
    }
}