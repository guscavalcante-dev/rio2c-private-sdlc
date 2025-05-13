// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-11-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-11-2022
// ***********************************************************************
// <copyright file="CartoonProjectOrganizationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CartoonProjectOrganizationDto</summary>
    public class CartoonProjectOrganizationDto
    {
        public string Name { get; set; }
        public string TradeName { get; set; }
        public string Document { get; set; }
        public string PhoneNumber { get; set; }
        public string ReelUrl { get; set; }
        public string Address { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CartoonProjectOrganizationDto"/> class.</summary>
        public CartoonProjectOrganizationDto()
        {
        }
    }
}