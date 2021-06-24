// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-24-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-24-2021
// ***********************************************************************
// <copyright file="SendEmailToProducersSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>SendEmailToProducersSearchViewModel</summary>
    public class SendEmailToProducersSearchViewModel
    {
        [Display(Name = "Search", ResourceType = typeof(Labels))]
        public string Search { get; set; }

        //[Display(Name = "ShowAllEditions", ResourceType = typeof(Labels))]
        //public bool ShowAllEditions { get; set; }

        //[Display(Name = "ShowAllCompanies", ResourceType = typeof(Labels))]
        //public bool ShowAllOrganizations { get; set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SendEmailToProducersSearchViewModel"/> class.</summary>
        public SendEmailToProducersSearchViewModel()
        {
        }
    }
}