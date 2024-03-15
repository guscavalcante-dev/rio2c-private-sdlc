// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-01-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-15-2024
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
        public string TicketClassName { get; set; }
        public int CollaboratorTypeId { get; set; }
        public string CollaboratorTypeName { get; set; }

        public AttendeeSalesPlatformTicketType AttendeeSalesPlatformTicketType { get; set; }
        public CollaboratorType CollaboratorType { get; set; }
        public Role Role { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeSalesPlatformTicketTypeDto"/> class.</summary>
        public AttendeeSalesPlatformTicketTypeDto()
        {
        }
    }
}