// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-09-2019
// ***********************************************************************
// <copyright file="UserBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>UserBaseDto</summary>
    public class UserBaseDto
    {
        public Guid Uid { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UserBaseDto"/> class.</summary>
        public UserBaseDto()
        {
        }
    }
}