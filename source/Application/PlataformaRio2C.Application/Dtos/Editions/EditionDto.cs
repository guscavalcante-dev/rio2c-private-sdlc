// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-09-2019
// ***********************************************************************
// <copyright file="EditionDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.Dtos
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
    }
}