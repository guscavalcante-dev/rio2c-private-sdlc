// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 07-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-23-2019
// ***********************************************************************
// <copyright file="Error.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models
{
    /// <summary>Error</summary>
    public class Error
    {
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }
}