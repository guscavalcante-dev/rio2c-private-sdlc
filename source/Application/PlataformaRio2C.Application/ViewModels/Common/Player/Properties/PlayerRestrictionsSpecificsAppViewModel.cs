// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="PlayerRestrictionsSpecificsAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>PlayerRestrictionsSpecificsAppViewModel</summary>
    public class PlayerRestrictionsSpecificsAppViewModel : EntityViewModel<PlayerRestrictionsSpecificsAppViewModel, PlayerRestrictionsSpecifics>, IEntityViewModel<PlayerRestrictionsSpecifics>
    {
        [AllowHtml]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName{ get; set; }

        public  LanguageAppViewModel Language { get; set; }
        public PlayerAppViewModel Player { get; set; }

        public PlayerRestrictionsSpecificsAppViewModel()
        {

        }

        public PlayerRestrictionsSpecificsAppViewModel(Domain.Entities.PlayerRestrictionsSpecifics entity)
        {
            CreationDate = entity.CreateDate;
            Uid = entity.Uid;
            Value = entity.Value;

            LanguageCode = entity.Language.Code;
            LanguageName = entity.Language.Name;
        }

        public PlayerRestrictionsSpecifics MapReverse()
        {
            var holdingDescription = new PlayerRestrictionsSpecifics(Value, LanguageCode);

            return holdingDescription;
        }

        public void SetLanguage(LanguageAppViewModel language)
        {
            Language = language;
        }

        public void SetHolding(PlayerAppViewModel player)
        {
            Player = player;
        }

        public PlayerRestrictionsSpecifics MapReverse(PlayerRestrictionsSpecifics entity)
        {
            return entity;
        }
    }
}
