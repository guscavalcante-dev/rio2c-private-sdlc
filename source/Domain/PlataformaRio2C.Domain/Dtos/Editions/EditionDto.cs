// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="EditionDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>EditionDto</summary>
    public class EditionDto
    {
        public Edition Edition { get; private set; }
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
        public DateTimeOffset ProjectSubmitStartDate { get; private set; }
        public DateTimeOffset ProjectSubmitEndDate { get; private set; }
        public DateTimeOffset ProjectEvaluationStartDate { get; private set; }
        public DateTimeOffset ProjectEvaluationEndDate { get; private set; }
        public DateTimeOffset OneToOneMeetingsScheduleDate { get; private set; }
        public DateTimeOffset NegotiationStartDate { get; private set; }
        public DateTimeOffset NegotiationEndDate { get; private set; }
        public int AttendeeOrganizationMaxSellProjectsCount { get; private set; }
        public int ProjectMaxBuyerEvaluationsCount { get; private set; }
        public DateTimeOffset MusicProjectSubmitStartDate { get; private set; }
        public DateTimeOffset MusicProjectSubmitEndDate { get; private set; }
        public DateTimeOffset MusicProjectEvaluationStartDate { get; private set; }
        public DateTimeOffset MusicProjectEvaluationEndDate { get; private set; }
        public DateTimeOffset InnovationProjectSubmitStartDate { get; private set; }
        public DateTimeOffset InnovationProjectSubmitEndDate { get; private set; }
        public DateTimeOffset InnovationProjectEvaluationStartDate { get; private set; }
        public DateTimeOffset InnovationProjectEvaluationEndDate { get; private set; }
        public DateTimeOffset CreateDate { get; private set; }
        public int CreateUserId { get; private set; }
        public DateTimeOffset UpdateDate { get; private set; }
        public int UpdateUserId { get; private set; }

        //public UserAppViewModel Creator { get; set; }
        //public UserAppViewModel Updated { get; set; }

        /// <summary>Initializes a new instance of the <see cref="EditionDto"/> class.</summary>
        public EditionDto()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="EditionDto"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public EditionDto(Domain.Entities.Edition entity)
        {
            if (entity == null)
            {
                return;
            }

            this.Edition = entity;

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
            this.ProjectSubmitStartDate = entity.ProjectSubmitStartDate;
            this.ProjectSubmitEndDate = entity.ProjectSubmitEndDate;
            this.ProjectEvaluationStartDate = entity.ProjectEvaluationStartDate;
            this.ProjectEvaluationEndDate = entity.ProjectEvaluationEndDate;
            this.OneToOneMeetingsScheduleDate = entity.OneToOneMeetingsScheduleDate;
            this.NegotiationStartDate = entity.NegotiationStartDate;
            this.NegotiationEndDate = entity.NegotiationEndDate;
            this.AttendeeOrganizationMaxSellProjectsCount = entity.AttendeeOrganizationMaxSellProjectsCount;
            this.ProjectMaxBuyerEvaluationsCount = entity.ProjectMaxBuyerEvaluationsCount;
            this.MusicProjectSubmitStartDate = entity.MusicProjectSubmitStartDate;
            this.MusicProjectSubmitEndDate = entity.MusicProjectSubmitEndDate;
            this.MusicProjectEvaluationStartDate = entity.MusicProjectEvaluationStartDate;
            this.MusicProjectEvaluationEndDate = entity.MusicProjectEvaluationEndDate;
            this.InnovationProjectSubmitStartDate = entity.InnovationProjectSubmitStartDate;
            this.InnovationProjectSubmitEndDate = entity.InnovationProjectSubmitEndDate;
            this.InnovationProjectEvaluationStartDate = entity.InnovationProjectEvaluationStartDate;
            this.InnovationProjectEvaluationEndDate = entity.InnovationProjectEvaluationEndDate;
            this.CreateDate = entity.CreateDate;
            this.CreateUserId = entity.CreateUserId;
            this.UpdateDate = entity.UpdateDate;
            this.UpdateUserId = entity.UpdateUserId;

            //if (entity.Creator != null)
            //{
            //    this.Creator = new UserAppViewModel(entity.Creator);
            //}

            //if (entity.Updater != null)
            //{
            //    this.Updater = new UserAppViewModel(entity.Updater);
            //}
        }

        #region Audiovisual

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

        #endregion

        #endregion

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
        public bool IsMusicProjectEvaluationOpen()
        {
            return DateTime.UtcNow >= this.MusicProjectEvaluationStartDate && DateTime.UtcNow <= this.MusicProjectEvaluationEndDate;
        }

        /// <summary>Determines whether [is music project evaluation started].</summary>
        /// <returns>
        ///   <c>true</c> if [is music project evaluation started]; otherwise, <c>false</c>.</returns>
        public bool IsMusicProjectEvaluationStarted()
        {
            return DateTime.UtcNow >= this.MusicProjectEvaluationStartDate;
        }

        /// <summary>Determines whether [is music project evaluation ended].</summary>
        /// <returns>
        ///   <c>true</c> if [is music project evaluation ended]; otherwise, <c>false</c>.</returns>
        public bool IsMusicProjectEvaluationEnded()
        {
            return DateTime.UtcNow > this.MusicProjectEvaluationEndDate;
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
            return DateTime.UtcNow >= this.InnovationProjectEvaluationStartDate && DateTime.UtcNow <= this.InnovationProjectEvaluationEndDate;
        }

        /// <summary>Determines whether [is innovation project evaluation started].</summary>
        /// <returns>
        ///   <c>true</c> if [is innovation project evaluation started]; otherwise, <c>false</c>.</returns>
        public bool IsInnovationProjectEvaluationStarted()
        {
            return DateTime.UtcNow >= this.InnovationProjectEvaluationStartDate;
        }

        /// <summary>Determines whether [is innovation project evaluation ended].</summary>
        /// <returns>
        ///   <c>true</c> if [is innovation project evaluation ended]; otherwise, <c>false</c>.</returns>
        public bool IsInnovationProjectEvaluationEnded()
        {
            return DateTime.UtcNow > this.InnovationProjectEvaluationEndDate;
        }

        #endregion

        #endregion
    }
}