// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="InterestGroupAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Enums;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>InterestGroupAppViewModel</summary>
    public class InterestGroupAppViewModel : EntityViewModel<InterestGroupAppViewModel, InterestGroup>, IEntityViewModel<InterestGroup>
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public InterestGroupAppViewModel()
        {

        }

        public InterestGroupAppViewModel(InterestGroup interestGroup)
        {
            Uid = interestGroup.Uid;
            CreationDate = interestGroup.CreateDate;
            Name = interestGroup.Name;
            Type = interestGroup.Type;
        }

        public InterestGroup MapReverse()
        {
            if (Type == null)
            {
                Type = InterestGroupTypeCodes.Multiple.ToString();
            }
            
            var entity = new InterestGroup(Name, (InterestGroupTypeCodes)Enum.Parse(typeof(InterestGroupTypeCodes), Type));
            return entity;
        }

        public InterestGroup MapReverse(InterestGroup entity)
        {
            if (Type == null)
            {
                Type = InterestGroupTypeCodes.Multiple.ToString();
            }

            entity.SetName(Name);
            entity.SetType((InterestGroupTypeCodes)Enum.Parse(typeof(InterestGroupTypeCodes), Type));
            return entity;
        }
    }
}
