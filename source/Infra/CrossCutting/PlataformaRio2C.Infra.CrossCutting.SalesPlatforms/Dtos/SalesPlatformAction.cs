// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 09-01-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="SalesPlatformAction.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos
{
    /// <summary>SalesPlatformAction</summary>
    public class SalesPlatformAction
    {
        public const string AttendeeUpdated = "attendee.updated";
        public const string AttendeeCheckedIn = "barcode.checked_in";
        public const string AttendeeCheckedOut = "barcode.un_checked_in";
    }
}