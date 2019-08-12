// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-12-2019
// ***********************************************************************
// <copyright file="HoldingItemListAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>HoldingItemListAppViewModel</summary>
    public class HoldingItemListAppViewModel : EntityViewModel<HoldingItemListAppViewModel, Holding>
    {
        [Display(Name = "Name", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        /// <summary>Initializes a new instance of the <see cref="HoldingItemListAppViewModel"/> class.</summary>
        public HoldingItemListAppViewModel()
            :base()
        {

        }

        /// <summary>Initializes a new instance of the <see cref="HoldingItemListAppViewModel"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public HoldingItemListAppViewModel(Holding entity)
            :base(entity)
        {
            Name = entity.Name;
        }
    }
}
