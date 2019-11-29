// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-28-2019
// ***********************************************************************
// <copyright file="HubBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MessageHubDto</summary>
    public class HubBaseDto<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        /// <summary>Initializes a new instance of the <see cref="HubBaseDto{T}"/> class.</summary>
        public HubBaseDto()
        {
        }
    }
}