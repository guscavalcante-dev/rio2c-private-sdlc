// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-05-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-05-2020
// ***********************************************************************
// <copyright file="RoomNameBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>RoomNameBaseDto</summary>
    public class RoomNameBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string Value { get; set; }

        public LanguageBaseDto LanguageDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="RoomNameBaseDto"/> class.</summary>
        public RoomNameBaseDto()
        {
        }
    }
}