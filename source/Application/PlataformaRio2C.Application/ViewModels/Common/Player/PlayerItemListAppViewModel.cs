// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="PlayerItemListAppViewModel.cs" company="Softo">
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
    /// <summary>PlayerItemListAppViewModel</summary>
    public class PlayerItemListAppViewModel : EntityViewModel<PlayerItemListAppViewModel, Player>
    {
        /// <summary>Nome do player</summary> 
        [Display(Name = "Name", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        [Display(Name = "TradeName", ResourceType = typeof(Labels))]
        public string TradeName { get; set; }

        [Display(Name = "SocialMedia", ResourceType = typeof(Labels))]
        public string SocialMedia { get; set; }

        [Display(Name = "CNPJ", ResourceType = typeof(Labels))]
        public string CNPJ { get; set; }

        [Display(Name = "Website", ResourceType = typeof(Labels))]
        public string Website { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Labels))]
        public string PhoneNumber { get; set; }

        [Display(Name = "Holding", ResourceType = typeof(Labels))]
        public string HoldingName { get; set; }

        public PlayerItemListAppViewModel()
        {

        }

        public PlayerItemListAppViewModel(Player entity)
            : base(entity)
        {
            CreationDate = entity.CreateDate;
            Uid = entity.Uid;
            Name = entity.Name;
            TradeName = entity.TradeName;
            SocialMedia = entity.SocialMedia;
            CNPJ = entity.CNPJ;
            Website = entity.Website;
            PhoneNumber = entity.PhoneNumber;
            HoldingName = entity.Holding.Name;
        }
    }
}
