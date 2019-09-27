// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-01-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-26-2019
// ***********************************************************************
// <copyright file="AttendeeSalesPlatformTicketTypeDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeSalesPlatformTicketTypeDto</summary>
    public class AttendeeSalesPlatformTicketTypeDto
    {
        public AttendeeSalesPlatformTicketType AttendeeSalesPlatformTicketType { get; set; }
        public CollaboratorType CollaboratorType { get; set; }
        public Role Role { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeSalesPlatformTicketTypeDto"/> class.</summary>
        public AttendeeSalesPlatformTicketTypeDto()
        {
        }
    }
}