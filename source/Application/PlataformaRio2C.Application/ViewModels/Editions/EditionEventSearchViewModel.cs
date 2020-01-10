// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-05-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-05-2020
// ***********************************************************************
// <copyright file="EditionEventSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>EditionEventSearchViewModel</summary>
    public class EditionEventSearchViewModel
    {
        [Display(Name = "Search", ResourceType = typeof(Labels))]
        public string Search { get; set; }

        //[Display(Name = "ShowAllEditions", ResourceType = typeof(Labels))]
        //public bool ShowAllEditions { get; set; }

        //[Display(Name = "ShowHighlights", ResourceType = typeof(Labels))]
        //public bool ShowHighlights { get; set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

        /// <summary>Initializes a new instance of the <see cref="EditionEventSearchViewModel"/> class.</summary>
        public EditionEventSearchViewModel()
        {
        }
    }
}