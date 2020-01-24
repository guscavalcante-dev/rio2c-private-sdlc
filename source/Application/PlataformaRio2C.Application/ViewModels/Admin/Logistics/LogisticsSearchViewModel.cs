// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-20-2020
// ***********************************************************************
// <copyright file="LogisticsSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>LogisticsSearchViewModel</summary>
    public class LogisticsSearchViewModel
    {
        [Display(Name = "Search", ResourceType = typeof(Labels))]
        public string Search { get; set; }

        //[Display(Name = "ShowAllEditions", ResourceType = typeof(Labels))]
        //public bool ShowAllEditions { get; set; }

        //[Display(Name = "ShowHighlights", ResourceType = typeof(Labels))]
        //public bool ShowHighlights { get; set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

        /// <summary>Initializes a new instance of the <see cref="LogisticsSearchViewModel"/> class.</summary>
        public LogisticsSearchViewModel()
        {
        }
    }
}