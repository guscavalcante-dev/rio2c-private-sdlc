// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Renan Valentim
// Created          : 06-21-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-21-2021
// ***********************************************************************
// <copyright file="IntiAction.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti.Models
{
    public static class IntiAction
    {
        public const string TicketSold = "ticket_sold";
        public const string TicketCancelled = "ticket_canceled";
        public const string ParticipantUpdated = "participant_updated";
    }
}