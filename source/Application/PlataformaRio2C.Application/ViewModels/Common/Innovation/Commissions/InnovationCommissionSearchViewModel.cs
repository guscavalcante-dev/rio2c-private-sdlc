// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-08-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-04-2023
// ***********************************************************************
// <copyright file="InnovationCommissionSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>InnovationCommissionSearchViewModel</summary>
    public class InnovationCommissionSearchViewModel
    {
        [Display(Name = "Search", ResourceType = typeof(Labels))]
        public string Search { get; set; }

        [Display(Name = "ShowAllEditions", ResourceType = typeof(Labels))]
        public bool ShowAllEditions { get; set; }

        [Display(Name = "ShowAllParticipants", ResourceType = typeof(Labels))]
        public bool ShowAllParticipants { get; set; }

        [Display(Name = "Vertical", ResourceType = typeof(Labels))]
        public Guid? InnovationOrganizationTrackOptionGroupUid { get; set; }


        [Display(Name = "Verticals", ResourceType = typeof(Labels))]
        public IEnumerable<InnovationOrganizationTrackOptionGroup> InnovationOrganizationTrackOptionGroups { get; private set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

        /// <summary>Initializes a new instance of the <see cref="InnovationCommissionSearchViewModel"/> class.</summary>
        public InnovationCommissionSearchViewModel()
        {
        }

        /// <summary>
        /// Updates the models and lists.
        /// </summary>
        /// <param name="innovationOrganizationTrackOptionGroups">The innovation organization track options.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateModelsAndLists(List<InnovationOrganizationTrackOptionGroup> innovationOrganizationTrackOptionGroups, string userInterfaceLanguage)
        {
            this.InnovationOrganizationTrackOptionGroups = innovationOrganizationTrackOptionGroups.GetSeparatorTranslation(i => i.Name, userInterfaceLanguage, '|')?.OrderBy(i => i.DisplayOrder)?.ToList();
        }
    }
}