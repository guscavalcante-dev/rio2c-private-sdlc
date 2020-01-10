//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Application
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-19-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-06-2019
//// ***********************************************************************
//// <copyright file="RoomNameAppViewModel.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System.ComponentModel.DataAnnotations;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    /// <summary>RoomNameAppViewModel</summary>
//    public class RoomNameAppViewModel : EntityViewModel<RoomNameAppViewModel, RoomName>, IEntityViewModel<RoomName>
//    {
//        [Display(Name = "Title", ResourceType = typeof(Labels))]
//        public string Value { get; set; }

//        public string LanguageCode { get; set; }
//        public string LanguageName { get; set; }

//        public LanguageAppViewModel Language { get; set; }
        

//        public RoomNameAppViewModel()
//        {
           
//        }

//        public RoomNameAppViewModel(RoomName entity)
//        {
//            CreationDate = entity.CreateDate;
//            Uid = entity.Uid;
//            Value = entity.Value;

//            LanguageCode = entity.Language.Code;
//            LanguageName = entity.Language.Name;
//        }


//        public void SetLanguage(LanguageAppViewModel language)
//        {
//            Language = language;
//        }

//        public RoomName MapReverse()
//        {
//            var entity = new RoomName(Value, LanguageCode);

//            return entity;
//        }

//        public RoomName MapReverse(RoomName entity)
//        {
//            return entity;
//        }
//    }
//}
