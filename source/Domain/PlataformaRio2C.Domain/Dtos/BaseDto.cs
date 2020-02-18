// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-14-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
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