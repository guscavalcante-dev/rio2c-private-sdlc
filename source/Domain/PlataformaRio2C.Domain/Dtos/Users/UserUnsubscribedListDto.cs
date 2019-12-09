// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-05-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
// ***********************************************************************
// <copyright file="UserUnsubscribedListDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>UserUnsubscribedListDto</summary>
    public class UserUnsubscribedListDto
    {
        public UserUnsubscribedList UserUnsubscribedList { get; set; }
        public User User { get; set; }
        public SubscribeList SubscribeList { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UserUnsubscribedListDto"/> class.</summary>
        public UserUnsubscribedListDto()
        {
        }
    }
}