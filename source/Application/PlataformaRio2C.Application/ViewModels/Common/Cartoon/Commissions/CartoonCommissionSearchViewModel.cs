﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 01-29-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-29-2022
// ***********************************************************************
// <copyright file="CartoonCommissionSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>CartoonCommissionSearchViewModel</summary>
    public class CartoonCommissionSearchViewModel
    {
        [Display(Name = "Search", ResourceType = typeof(Labels))]
        public string Search { get; set; }

        [Display(Name = "ShowAllEditions", ResourceType = typeof(Labels))]
        public bool ShowAllEditions { get; set; }

        [Display(Name = "ShowAllParticipants", ResourceType = typeof(Labels))]
        public bool ShowAllParticipants { get; set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CartoonCommissionSearchViewModel"/> class.</summary>
        public CartoonCommissionSearchViewModel()
        {
        }
    }
}