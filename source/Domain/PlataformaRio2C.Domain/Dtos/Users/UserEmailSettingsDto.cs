// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-05-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
// ***********************************************************************
// <copyright file="UserEmailSettingsDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>UserEmailSettingsDto</summary>
    public class UserEmailSettingsDto
    {
        public User User { get; set; }
        public IEnumerable<UserUnsubscribedListDto> UserUnsubscribedListDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UserEmailSettingsDto"/> class.</summary>
        public UserEmailSettingsDto()
        {
        }
    }
}