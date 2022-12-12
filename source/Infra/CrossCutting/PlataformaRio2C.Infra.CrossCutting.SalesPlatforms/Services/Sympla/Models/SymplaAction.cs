// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Renan Valentim
// Created          : 11-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-24-2022
// ***********************************************************************
// <copyright file="SymplaAction.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Sympla.Models
{
    /// <summary>SymplaAction is 'Order_Status' in Sympla platform</summary>
    public class SymplaAction
    {
        public const string AttendeeUpdated  = "attendee.updated"; //TODO: Sympla doesn't have this status!
        public const string AttendeeCheckedIn = "barcode.checked_in"; //TODO: Sympla doesn't have this status!
        public const string AttendeeCheckedOut = "barcode.un_checked_in"; //TODO: Sympla doesn't have this status!
        public const string OrderPlaced = "order.placed"; //TODO: Sympla doesn't have this status!
        public const string OrderRefunded = "order.refunded"; //TODO: Sympla doesn't have this status!
        public const string OrderUpdated = "order.updated"; //TODO: Sympla doesn't have this status!

        public const string TicketSold = "A";
        public const string TicketCancelled = "C";
        public const string ParticipantUpdated = "participant_updated"; //TODO: Sympla doesn't have this status!
    }
}