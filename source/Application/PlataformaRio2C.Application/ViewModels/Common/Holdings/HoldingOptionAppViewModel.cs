//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Application
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-19-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-12-2019
//// ***********************************************************************
//// <copyright file="HoldingOptionAppViewModel.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    /// <summary>HoldingOptionAppViewModel</summary>
//    public class HoldingOptionAppViewModel
//    {
//        #region props
                
//        public Guid Uid { get; set; }

//        [Display(Name = "Name", ResourceType = typeof(Labels))]
//        public string Name { get; set; }

//        #endregion

//        #region ctor

//        public HoldingOptionAppViewModel()
//        {

//        }

//        public HoldingOptionAppViewModel(Holding entity)
//        {
//            Uid = entity.Uid;
//            Name = entity.Name;
//        }

//        #endregion

//        #region Public methods

//        public static IEnumerable<HoldingOptionAppViewModel> MapList(IEnumerable<Holding> entities)
//        {
//            foreach (var entity in entities)
//            {
//                yield return new HoldingOptionAppViewModel(entity);
//            }
//        }

//        #endregion
//    }
//}
