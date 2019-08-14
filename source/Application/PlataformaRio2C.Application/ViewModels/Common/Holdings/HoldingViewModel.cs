// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-14-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-14-2019
// ***********************************************************************
// <copyright file="HoldingViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>HoldingViewModel</summary>
    public class HoldingViewModel
    {
        public Guid Uid { get; set; }

        public CropperImageFileViewModel CropperImage { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        public IEnumerable<HoldingDescriptionAppViewModel> Descriptions { get; set; }
        public List<LanguageDto> LanguagesDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="HoldingAppViewModel"/> class.</summary>
        public HoldingViewModel()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="HoldingViewModel"/> class.</summary>
        /// <param name="languagesDtos">The languages dtos.</param>
        public HoldingViewModel(List<LanguageDto> languagesDtos)
        {
            this.LanguagesDtos = languagesDtos;
            this.CropperImage = new CropperImageFileViewModel(false, null);
        }

        /// <summary>Initializes a new instance of the <see cref="HoldingViewModel"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public HoldingViewModel(Domain.Entities.Holding entity, List<LanguageDto> languagesDtos)
        {
            this.Uid = entity.Uid;
            this.Name = entity.Name;
            this.CropperImage = new CropperImageFileViewModel(entity.IsImageUploaded, entity.Uid);

            if (entity.Descriptions?.Any() == true)
            {
                this.Descriptions = HoldingDescriptionAppViewModel.MapList(entity.Descriptions).ToList();
            }

            this.LanguagesDtos = languagesDtos;
        }

        //public Holding MapReverse()
        //{
        //    var holding = new Domain.Entities.Holding(this.Name);

        //    if (Descriptions != null && Descriptions.Any())
        //    {
        //        var descriptions = new List<HoldingDescription>();
        //        foreach (var description in Descriptions)
        //        {
        //            description.SetLanguage(new LanguageAppViewModel(description.LanguageCode));
        //            holding.AddDescription(description.MapReverse());
        //        }
        //    }

        //    return holding;
        //}

        ///// <summary>Maps the reverse.</summary>
        ///// <param name="entity">The entity.</param>
        ///// <returns></returns>
        //public Holding MapReverse(Holding entity)
        //{
        //    entity.SetName(Name);

        //    //if (ImageUpload != null && Image == null)
        //    //{
        //    //    Image = new ImageFileAppViewModel(ImageUpload);
        //    //}

        //    //if (Image != null)
        //    //{
        //    //    if (entity.Image != null)
        //    //    {
        //    //        entity.SetImage(Image.MapReverse(entity.Image));
        //    //    }
        //    //    else
        //    //    {   
        //    //        entity.SetImage(Image.MapReverse());
        //    //    }
        //    //}

        //    if (Descriptions != null && Descriptions.Any())
        //    {
        //        var descriptions = new List<HoldingDescription>();
        //        foreach (var description in Descriptions)
        //        {
        //            description.SetLanguage(new LanguageAppViewModel(description.LanguageCode));
        //            descriptions.Add(description.MapReverse());
        //        }

        //        entity.SetDescriptions(descriptions);
        //    }

        //    return entity;
        //}
    }
}
