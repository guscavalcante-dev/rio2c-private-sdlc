// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-08-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-08-2021
// ***********************************************************************
// <copyright file="AudiovisualCommissionSearchViewModel.cs" company="Softo">
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
    /// <summary>AudiovisualCommissionSearchViewModel</summary>
    public class AudiovisualCommissionSearchViewModel
    {
        [Display(Name = "Search", ResourceType = typeof(Labels))]
        public string Search { get; set; }

        [Display(Name = "ShowAllEditions", ResourceType = typeof(Labels))]
        public bool ShowAllEditions { get; set; }

        [Display(Name = "ShowAllParticipants", ResourceType = typeof(Labels))]
        public bool ShowAllParticipants { get; set; }

        [Display(Name = "Interest", ResourceType = typeof(Labels))]
        public Guid? InterestUid { get; set; }


        [Display(Name = "Interests", ResourceType = typeof(Labels))]
        public IEnumerable<Interest> Interests { get; private set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AudiovisualCommissionSearchViewModel"/> class.</summary>
        public AudiovisualCommissionSearchViewModel()
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