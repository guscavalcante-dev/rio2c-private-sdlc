// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 07-24-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="EventbriteAction.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models
{
    /// <summary>EventbriteAction</summary>
    public class EventbriteAction
    {
        public const string AttendeeUpdated  = "attendee.updated";
        public const string AttendeeCheckedIn = "barcode.checked_in";
        public const string AttendeeCheckedOut = "barcode.un_checked_in";
        public const string OrderPlaced = "order.placed";
        public const string OrderRefunded = "order.refunded";
        public const string OrderUpdated = "order.updated";
    }
}