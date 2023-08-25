// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-18-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-18-2023
// ***********************************************************************
// <copyright file="WeConnectSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>WeConnectSearchViewModel</summary>
    public class WeConnectSearchViewModel
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }

        /// <summary>Initializes a new instance of the <see cref="WeConnectSearchViewModel"/> class.</summary>
        public WeConnectSearchViewModel()
        {
            if (!this.Page.HasValue)
            {
                this.Page = 1;
            }

            if (!this.PageSize.HasValue)
            {
                this.PageSize = 12;
            }
        }
    }
}