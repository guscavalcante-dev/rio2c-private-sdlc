// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="ProducerDescriptionAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>ProducerDescriptionAppViewModel</summary>
    public class ProducerDescriptionAppViewModel : EntityViewModel<ProducerDescriptionAppViewModel, ProducerDescription>, IEntityViewModel<ProducerDescription>
    {
        [AllowHtml]
        public string Value { get; set; }

        public string LanguageCode { get; set; }

        public string LanguageName{ get; set; }

        public  LanguageAppViewModel Language { get; set; }       

        public ProducerDescriptionAppViewModel()
        {

        }

        public ProducerDescriptionAppViewModel(ProducerDescription entity)
        {
            CreationDate = entity.CreateDate;
            Uid = entity.Uid;
            Value = entity.Value;

            LanguageCode = entity.Language.Code;
            LanguageName = entity.Language.Name;
        }

        public ProducerDescription MapReverse()
        {
            var producerDescription = new ProducerDescription(Value, LanguageCode);

            return producerDescription;
        }

        public void SetLanguage(LanguageAppViewModel language)
        {
            Language = language;
        }
      

        public ProducerDescription MapReverse(ProducerDescription entity)
        {
            return entity;
        }
    }
}
