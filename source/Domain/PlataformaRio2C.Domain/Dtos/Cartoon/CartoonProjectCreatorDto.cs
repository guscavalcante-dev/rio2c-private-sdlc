// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-11-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-11-2022
// ***********************************************************************
// <copyright file="CartoonProjectCreatorDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CartoonProjectCreatorDto</summary>
    public class CartoonProjectCreatorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string PhoneNumber { get; set; }
        public string MiniBio { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CartoonProjectCreatorDto"/> class.</summary>
        public CartoonProjectCreatorDto()
        {
        }
    }
}