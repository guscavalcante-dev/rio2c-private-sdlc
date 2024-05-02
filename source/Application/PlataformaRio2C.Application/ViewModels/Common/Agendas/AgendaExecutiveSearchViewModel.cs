// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-29-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-29-2024
// ***********************************************************************
// <copyright file="AgendaExecutiveSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>AgendaExecutiveSearchViewModel</summary>
    public class AgendaExecutiveSearchViewModel
    {
        [Display(Name = "Search", ResourceType = typeof(Labels))]
        public string Search { get; set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AgendaExecutiveSearchViewModel"/> class.</summary>
        public AgendaExecutiveSearchViewModel()
        {
        }
    }
}