// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-29-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-29-2019
// ***********************************************************************
// <copyright file="CollaboratorSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>CollaboratorSearchViewModel</summary>
    public class CollaboratorSearchViewModel
    {
        [Display(Name = "Search", ResourceType = typeof(Labels))]
        public string Search { get; set; }

        [Display(Name = "ShowAllEditions", ResourceType = typeof(Labels))]
        public bool ShowAllEditions { get; set; }

        [Display(Name = "ShowAllExecutives", ResourceType = typeof(Labels))]
        public bool ShowAllCollaborators { get; set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorSearchViewModel"/> class.</summary>
        public CollaboratorSearchViewModel()
        {
        }
    }
}