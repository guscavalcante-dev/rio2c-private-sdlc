// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-09-2019
// ***********************************************************************
// <copyright file="HoldingAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>HoldingAppViewModel</summary>
    public class HoldingAppViewModel : EntityViewModel<HoldingAppViewModel, Holding>, IEntityViewModel<Holding>
    {
        [Display(Name = "Name", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Labels))]
        public ImageFileAppViewModel Image { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Labels))]
        public HttpPostedFileBase ImageUpload { get; set; }

        public IEnumerable<HoldingDescriptionAppViewModel> Descriptions { get; set; }

        public IEnumerable<LanguageAppViewModel> Languages { get; set; }

        public HoldingAppViewModel()
        {

        }

        public HoldingAppViewModel(Domain.Entities.Holding entity)
        {
            CreationDate = entity.CreateDate;
            Uid = entity.Uid;
            Name = entity.Name;

            //if (entity.Image != null)
            //{
            //    Image = new ImageFileAppViewModel(entity.Image);
            //}

            if (entity.Descriptions != null && entity.Descriptions.Any())
            {
                Descriptions = HoldingDescriptionAppViewModel.MapList(entity.Descriptions).ToList();
            }

        }

        public Holding MapReverse()
        {
            var holding = new Domain.Entities.Holding(this.Name);

            //if (ImageUpload != null && Image == null)
            //{
            //    Image = new ImageFileAppViewModel(ImageUpload);
            //}

            //if (Image != null)
            //{
            //    holding.SetImage(Image.MapReverse());
            //}

            if (Descriptions != null && Descriptions.Any())
            {
                var descriptions = new List<HoldingDescription>();
                foreach (var description in Descriptions)
                {
                    description.SetLanguage(new LanguageAppViewModel(description.LanguageCode));
                    holding.AddDescription(description.MapReverse());
                }
            }

            return holding;
        }

        public Holding MapReverse(Holding entity)
        {
            entity.SetName(Name);

            if (ImageUpload != null && Image == null)
            {
                Image = new ImageFileAppViewModel(ImageUpload);
            }

            //if (Image != null)
            //{
            //    if (entity.Image != null)
            //    {
            //        entity.SetImage(Image.MapReverse(entity.Image));
            //    }
            //    else
            //    {   
            //        entity.SetImage(Image.MapReverse());
            //    }
            //}

            if (Descriptions != null && Descriptions.Any())
            {
                var descriptions = new List<HoldingDescription>();
                foreach (var description in Descriptions)
                {
                    description.SetLanguage(new LanguageAppViewModel(description.LanguageCode));
                    descriptions.Add(description.MapReverse());
                }

                entity.SetDescriptions(descriptions);
            }

            return entity;
        }
    }
}
