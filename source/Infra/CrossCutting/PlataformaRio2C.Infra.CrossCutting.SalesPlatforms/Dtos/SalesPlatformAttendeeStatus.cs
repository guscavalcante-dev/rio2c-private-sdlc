// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 09-01-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="SalesPlatformAttendeeStatus.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos
{
    /// <summary>SalesPlatformAttendeeStatus</summary>
    public class SalesPlatformAttendeeStatus
    {
        public const string Attending  = "attending";
        public const string NotAttending = "not_attending";
        public const string Unpaid = "upaind";
    }
}