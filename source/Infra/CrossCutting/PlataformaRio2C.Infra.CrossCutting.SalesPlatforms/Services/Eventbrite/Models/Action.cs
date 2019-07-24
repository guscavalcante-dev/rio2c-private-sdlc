// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 07-24-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-24-2019
// ***********************************************************************
// <copyright file="Action.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models
{
    /// <summary>Action</summary>
    public class Action
    {
        public const string EventbriteAttendeeUpdated  = "attendee.updated";
        public const string EventbriteAttendeeCheckedIn = "barcode.checked_in";
        public const string EventbriteAttendeeCheckedOut = "barcode.un_checked_in";
        public const string EventbriteOrderPlaced = "order.placed";
        public const string EventbriteOrderRefunded = "order.refunded";
        public const string EventbriteOrderUpdated = "order.updated";
    }
}