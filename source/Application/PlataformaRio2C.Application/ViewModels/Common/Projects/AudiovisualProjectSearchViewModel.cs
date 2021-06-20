// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-20-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-20-2021
// ***********************************************************************
// <copyright file="AudiovisualProjectSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>AudiovisualProjectSearchViewModel</summary>
    public class AudiovisualProjectSearchViewModel
    {
        [Display(Name = "Search", ResourceType = typeof(Labels))]
        public string Search { get; set; }

        [Display(Name = "Genre", ResourceType = typeof(Labels))]
        public Guid? InterestUid { get; set; }

        [Display(Name = "ShowPitchings", ResourceType = typeof(Labels))]
        public bool ShowPitchings { get; set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

        public List<Interest> Interests;

        /// <summary>Initializes a new instance of the <see cref="AudiovisualProjectSearchViewModel"/> class.</summary>
        public AudiovisualProjectSearchViewModel()
        {
        }

        /// <summary>
        /// Updates the models and lists.
        /// </summary>
        /// <param name="interests">The interests.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateModelsAndLists(List<Interest> interests, string userInterfaceLanguage)
        {
            this.Interests = interests.GetSeparatorTranslation(i => i.Name, userInterfaceLanguage, '|')?.OrderBy(i => i.Name)?.ToList();
        }
    }
}