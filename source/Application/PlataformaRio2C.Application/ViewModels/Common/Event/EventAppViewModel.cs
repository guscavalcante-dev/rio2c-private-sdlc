// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="EventAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>EventAppViewModel</summary>
    public class EventAppViewModel : IEntityViewModel<Domain.Entities.Edition>
    {
        public string Name { get; set; }

        public EventAppViewModel()
        {

        }

        public EventAppViewModel(Domain.Entities.Edition entity)
        {
            Name = entity.Name;
        }

        public Edition MapReverse()
        {
            return new Domain.Entities.Edition(this.Name);
        }

        public Edition MapReverse(Edition entity)
        {
            throw new NotImplementedException();
        }
    }
}
