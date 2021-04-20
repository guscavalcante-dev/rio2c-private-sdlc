// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-20-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-20-2021
// ***********************************************************************
// <copyright file="ManagerSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Constants;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>ManagerSearchViewModel</summary>
    public class ManagerSearchViewModel
    {
        [Display(Name = "Search", ResourceType = typeof(Labels))]
        public string Search { get; set; }

        [Display(Name = "ShowAllEditions", ResourceType = typeof(Labels))]
        public bool ShowAllEditions { get; set; }

        [Display(Name = "CollaboratorTypes", ResourceType = typeof(Labels))]
        public string[] CollaboratorTypes { get; set; }

        [Display(Name = "SelectedCollaboratorTypes", ResourceType = typeof(Labels))]
        public string[] SelectedCollaboratorTypes { get; set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ManagerSearchViewModel"/> class.</summary>
        public ManagerSearchViewModel()
        {
            //this.ShowAllEditions = true;
            this.CollaboratorTypes = CollaboratorType.Admins;
        }
    }
}