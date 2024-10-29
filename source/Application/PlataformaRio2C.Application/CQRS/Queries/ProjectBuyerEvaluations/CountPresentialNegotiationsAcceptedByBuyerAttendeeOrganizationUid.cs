// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-18-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-18-2024
// ***********************************************************************
// <copyright file="CountPresentialNegotiationsAcceptedByBuyerAttendeeOrganizationUid.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>CountPresentialNegotiationsAcceptedByBuyerAttendeeOrganizationUid</summary>
    public class CountPresentialNegotiationsAcceptedByBuyerAttendeeOrganizationUid : BaseQuery<int>
    {
        public Guid BuyerAttendeeOrganizationUid { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountPresentialNegotiationsAcceptedByBuyerAttendeeOrganizationUid" /> class.
        /// </summary>
        /// <param name="buyerAttendeeOrganizationUid">The buyer attendee organization uid.</param>
        public CountPresentialNegotiationsAcceptedByBuyerAttendeeOrganizationUid(Guid buyerAttendeeOrganizationUid)
        {
            this.BuyerAttendeeOrganizationUid = buyerAttendeeOrganizationUid;
        }
    }
}