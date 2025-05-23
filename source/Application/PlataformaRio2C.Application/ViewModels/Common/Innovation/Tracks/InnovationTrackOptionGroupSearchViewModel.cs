﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-03-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-03-2023
// ***********************************************************************
// <copyright file="InnovationTrackOptionGroupSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    public class InnovationTrackOptionGroupSearchViewModel
    {
        [Display(Name = nameof(Labels.Search), ResourceType = typeof(Labels))]
        public string Search { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationTrackOptionGroupSearchViewModel"/> class.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        public InnovationTrackOptionGroupSearchViewModel(
            string search,
            int? page = 1,
            int? pageSize = 10)
        {
            this.Search = search;
            this.Page = page;
            this.PageSize = pageSize;
        }

        /// <summary>Initializes a new instance of the <see cref="InnovationTrackOptionGroupSearchViewModel"/> class.</summary>
        public InnovationTrackOptionGroupSearchViewModel()
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