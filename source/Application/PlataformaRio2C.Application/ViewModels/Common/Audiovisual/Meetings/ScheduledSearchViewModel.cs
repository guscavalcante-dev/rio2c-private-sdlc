// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan valentim
// Created          : 09-27-2021
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 05-06-2025
// ***********************************************************************
// <copyright file="ScheduledSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>ScheduledSearchViewModel</summary>
    public class ScheduledSearchViewModel
    {
        [Display(Name = nameof(Labels.Player), ResourceType = typeof(Labels))]
        public Guid? BuyerOrganizationUid { get; set; }

        [Display(Name = nameof(Labels.Producer), ResourceType = typeof(Labels))]
        public Guid? SellerOrganizationUid { get; set; }

        [Display(Name = nameof(Labels.Date), ResourceType = typeof(Labels))]
        public DateTime? Date { get; set; }

        [Display(Name = nameof(Labels.Project), ResourceType = typeof(Labels))]
        public string ProjectKeywords { get; set; }

        [Display(Name = nameof(Labels.CollaboratorType), ResourceType = typeof(Labels))]
        public Guid? CollaboratorTypeUid { get; set; }

        [Display(Name = nameof(Labels.Room), ResourceType = typeof(Labels))]
        public Guid? RoomUid { get; set; }

        [Display(Name = nameof(Labels.Type), ResourceType = typeof(Labels))]
        public string Type { get; set; }

        [Display(Name = nameof(Labels.ShowParticipants), ResourceType = typeof(Labels))]
        public bool ShowParticipants { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ScheduledSearchViewModel"/> class.</summary>
        public ScheduledSearchViewModel()
        {
        }
    }
}