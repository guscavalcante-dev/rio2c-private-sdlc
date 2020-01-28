// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-17-2019
// ***********************************************************************
// <copyright file="Edition.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Edition</summary>
    public class Edition : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 50;

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
        public int AttendeeOrganizationMaxSellProjectsCount { get; private set; }
        public int ProjectMaxBuyerEvaluationsCount { get; private set; }

        public virtual Quiz Quiz { get; private set; }

        public virtual ICollection<AttendeeOrganization> AttendeeOrganizations { get; private set; }
        public virtual ICollection<AttendeeCollaborator> AttendeeCollaborators { get; private set; }
        public virtual ICollection<AttendeeSalesPlatform> AttendeeSalesPlatforms { get; private set; }
        public virtual ICollection<AttendeeLogisticSponsor> AttendeeLogisticSponsors { get; private set; }        

        /// <summary>Initializes a new instance of the <see cref="Edition"/> class.</summary>
        /// <param name="name">The name.</param>
        public Edition(string name)
        {
            Name = name;
        }

        /// <summary>Initializes a new instance of the <see cref="Edition"/> class.</summary>
        protected Edition()
        {
        }

        /// <summary>Sets the start date.</summary>
        /// <param name="startDate">The start date.</param>
        public void SetStartDate(DateTime startDate)
        {
            StartDate = startDate;
        }

        /// <summary>Sets the end date.</summary>
        /// <param name="endDate">The end date.</param>
        public void SetEndDate(DateTime endDate)
        {
            EndDate = endDate;
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            return true;
        }
    }
}