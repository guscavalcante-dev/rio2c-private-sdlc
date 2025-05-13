// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 01-31-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-31-2024
// ***********************************************************************
// <copyright file="CreatorCommissionSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>CreatorCommissionSearchViewModel</summary>
    public class CreatorCommissionSearchViewModel
    {
        [Display(Name = "Search", ResourceType = typeof(Labels))]
        public string Search { get; set; }

        [Display(Name = "ShowAllEditions", ResourceType = typeof(Labels))]
        public bool ShowAllEditions { get; set; }

        [Display(Name = "ShowAllParticipants", ResourceType = typeof(Labels))]
        public bool ShowAllParticipants { get; set; }

        //[Display(Name = "Vertical", ResourceType = typeof(Labels))]
        //public Guid? CreatorOrganizationTrackOptionGroupUid { get; set; }

        //[Display(Name = "Verticals", ResourceType = typeof(Labels))]
        //public IEnumerable<CreatorOrganizationTrackOptionGroup> CreatorOrganizationTrackOptionGroups { get; private set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreatorCommissionSearchViewModel"/> class.</summary>
        public CreatorCommissionSearchViewModel()
        {
        }

        /// <summary>
        /// Updates the models and lists.
        /// </summary>
        /// <param name="innovationOrganizationTrackOptionGroups">The innovation organization track options.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateModelsAndLists(string userInterfaceLanguage) //List<CreatorOrganizationTrackOptionGroup> innovationOrganizationTrackOptionGroups, string userInterfaceLanguage
        {
            //this.CreatorOrganizationTrackOptionGroups = innovationOrganizationTrackOptionGroups.GetSeparatorTranslation(i => i.Name, userInterfaceLanguage, '|')?.OrderBy(i => i.DisplayOrder)?.ToList();
        }
    }
}