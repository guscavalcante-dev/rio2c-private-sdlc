// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 11-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-12-2020
// ***********************************************************************
// <copyright file="UserDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>UserDto</summary>
    public class UserDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }

        public User User { get; set; }
        public Collaborator Collaborator { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UserDto"/> class.</summary>
        public UserDto()
        {
        }

        /// <summary>Gets the display name.</summary>
        /// <returns></returns>
        public string GetDisplayName()
        {
            return this.Collaborator?.GetDisplayName() ?? this.User.Name;
        }
    }
}