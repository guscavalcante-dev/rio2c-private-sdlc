// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-14-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-27-2024
// ***********************************************************************
// <copyright file="BaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>BaseDto</summary>
    public class BaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public int UpdateUserId { get; set; }

        /// <summary>Initializes a new instance of the <see cref="BaseDto"/> class.</summary>
        public BaseDto()
        {
        }
    }
}