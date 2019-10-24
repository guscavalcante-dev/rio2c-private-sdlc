// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-24-2019
// ***********************************************************************
// <copyright file="EditionDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>EditionDto</summary>
    public class EditionDto
    {
        public int Id { get; private set; }
        public Guid Uid { get; private set; }
        public string Name { get; private set; }
        public int UrlCode { get; private set; }
        public bool IsCurrent { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime SellStartDate { get; private set; }
        public DateTime SellEndDate { get; private set; }
        public DateTime ProjectSubmitStartDate { get; private set; }
        public DateTime ProjectSubmitEndDate { get; private set; }
        public DateTime ProjectEvaluationStartDate { get; private set; }
        public DateTime ProjectEvaluationEndDate { get; private set; }
        public DateTime OneToOneMeetingsScheduleDate { get; private set; }
        public DateTime NegotiationStartDate { get; private set; }
        public DateTime NegotiationEndDate { get; private set; }
        public DateTime CreateDate { get; private set; }
        public int CreateUserId { get; private set; }
        public int UpdateUserId { get; private set; }
        public DateTime UpdateDate { get; private set; }

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

        #region Project Submit

        /// <summary>Determines whether [is project submit opened].</summary>
        /// <returns>
        ///   <c>true</c> if [is project submit opened]; otherwise, <c>false</c>.</returns>
        public bool IsProjectSubmitOpened()
        {
            return DateTime.Now >= this.ProjectSubmitStartDate && DateTime.Now <= this.ProjectSubmitEndDate;
        }

        /// <summary>Determines whether [is project submit started].</summary>
        /// <returns>
        ///   <c>true</c> if [is project submit started]; otherwise, <c>false</c>.</returns>
        public bool IsProjectSubmitStarted()
        {
            return DateTime.Now >= this.ProjectSubmitStartDate;
        }

        /// <summary>Determines whether [is project submit ended].</summary>
        /// <returns>
        ///   <c>true</c> if [is project submit ended]; otherwise, <c>false</c>.</returns>
        public bool IsProjectSubmitEnded()
        {
            return DateTime.Now > this.ProjectSubmitEndDate;
        }

        #endregion

        #region Project Evaluation

        /// <summary>Determines whether [is project evaluation opened].</summary>
        /// <returns>
        ///   <c>true</c> if [is project evaluation opened]; otherwise, <c>false</c>.</returns>
        public bool IsProjectEvaluationOpened()
        {
            return DateTime.Now >= this.ProjectSubmitStartDate && DateTime.Now <= this.ProjectEvaluationEndDate;
        }

        /// <summary>Determines whether [is project evaluation started].</summary>
        /// <returns>
        ///   <c>true</c> if [is project evaluation started]; otherwise, <c>false</c>.</returns>
        public bool IsProjectEvaluationStarted()
        {
            return DateTime.Now >= this.ProjectSubmitStartDate;
        }

        /// <summary>Determines whether [is project evaluation ended].</summary>
        /// <returns>
        ///   <c>true</c> if [is project evaluation ended]; otherwise, <c>false</c>.</returns>
        public bool IsProjectEvaluationEnded()
        {
            return DateTime.Now > this.ProjectEvaluationEndDate;
        }

        #endregion
    }
}