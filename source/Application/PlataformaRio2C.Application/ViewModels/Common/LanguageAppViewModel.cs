// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="LanguageAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>LanguageAppViewModel</summary>
    public class LanguageAppViewModel : EntityViewModel<LanguageAppViewModel, Language>, IEntityViewModel<Language>
    {        
        public string Name { get; set; }
        public string Code { get; set; }

        public LanguageAppViewModel()
        {

        }

        public LanguageAppViewModel(Language language)
        {
            Uid = language.Uid;
            CreationDate = language.CreateDate;
            Name = language.Name;
            Code = language.Code;
        }

        public LanguageAppViewModel(string languageCode)
        {
            Code = languageCode;
        }

        public Language MapReverse()
        {
            return new Language(this.Name, this.Code);
        }

        public Language MapReverse(Language entity)
        {
            entity.SetName(Name);
            return entity;
        }
    }
}
