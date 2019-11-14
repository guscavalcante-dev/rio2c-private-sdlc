// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 11-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-14-2019
// ***********************************************************************
// <copyright file="AttendeeOrganizationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>SellerAttendeeOrganizationDto</summary>
    public class SellerAttendeeOrganizationDto
    {
        public SellerAttendeeOrganization SellerAttendeeOrganization { get; set; }
        public AttendeeOrganizationDto AttendeeOrganizationDto { get; set; }
        //public AttendeeCollaboratorTicket AttendeeCollaboratorTicket { get; set; }
        public AttendeeSalesPlatformTicketType AttendeeSalesPlatformTicketType { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SellerAttendeeOrganizationDto"/> class.</summary>
        public SellerAttendeeOrganizationDto()
        {
        }

        #region Project Buyer Evaluation Groups

        /// <summary>Gets the projects buyer evaluation groups used.</summary>
        /// <returns></returns>
        public int GetProjectsBuyerEvaluationGroupsUsed()
        {
            return this.SellerAttendeeOrganization?.ProjectsBuyerEvaluationGroupsCount ?? 0;
        }

        /// <summary>Gets the projects buyer evaluation groups maximum.</summary>
        /// <returns></returns>
        public int GetProjectsBuyerEvaluationGroupsMax()
        {
            return this.AttendeeSalesPlatformTicketType?.ProjectBuyerEvaluationGroupMaxCount ?? 0;
        }

        /// <summary>Gets the projects buyer evaluation groups available.</summary>
        /// <returns></returns>
        public int GetProjectsBuyerEvaluationGroupsAvailable()
        {
            return this.GetProjectsBuyerEvaluationGroupsMax() - this.GetProjectsBuyerEvaluationGroupsUsed();
        }

        /// <summary>Gets the projects buyer evaluations maximum.</summary>
        /// <returns></returns>
        public int GetProjectsBuyerEvaluationsMax()
        {
            return this.AttendeeSalesPlatformTicketType?.ProjectBuyerEvaluationMaxCount ?? 0;
        }

        /// <summary>Gets the projects buyer evaluations total.</summary>
        /// <returns></returns>
        public int GetProjectsBuyerEvaluationsTotal()
        {
            return this.GetProjectsBuyerEvaluationGroupsMax() * GetProjectsBuyerEvaluationsMax();
        }

        /// <summary>Gets the projects buyer evaluations available.</summary>
        /// <returns></returns>
        public int GetProjectsBuyerEvaluationsAvailable()
        {
            return this.GetProjectsBuyerEvaluationGroupsAvailable() * this.GetProjectsBuyerEvaluationsMax();
        }

        #endregion
    }
}