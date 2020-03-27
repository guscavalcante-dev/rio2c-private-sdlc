// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-27-2020
// ***********************************************************************
// <copyright file="AgendaSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>AgendaSearchViewModel</summary>
    public class AgendaSearchViewModel
    {
        [Display(Name = "StartDate", ResourceType = typeof(Labels))]
        public long? StartDate { get; set; }

        [Display(Name = "EndDate", ResourceType = typeof(Labels))]
        public long? EndDate { get; set; }

        [Display(Name = "ShowMyConferences", ResourceType = typeof(Labels))]
        public bool ShowMyConferences { get; set; }

        [Display(Name = "ShowAllConferences", ResourceType = typeof(Labels))]
        public bool ShowAllConferences { get; set; }

        [Display(Name = "ShowOneToOneMeetings", ResourceType = typeof(Labels))]
        public bool ShowOneToOneMeetings { get; set; }

        [Display(Name = "ShowFlights", ResourceType = typeof(Labels))]
        public bool ShowFlights { get; set; }

        [Display(Name = "ShowAccommodations", ResourceType = typeof(Labels))]
        public bool ShowAccommodations { get; set; }

        [Display(Name = "ShowTransfers", ResourceType = typeof(Labels))]
        public bool ShowTransfers { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AgendaSearchViewModel"/> class.</summary>
        public AgendaSearchViewModel()
        {
            this.ShowMyConferences = true;
            this.ShowAllConferences = false;
            this.ShowOneToOneMeetings = true;
            this.ShowFlights = true;
            this.ShowAccommodations = true;
            this.ShowTransfers = true;
        }
    }
}