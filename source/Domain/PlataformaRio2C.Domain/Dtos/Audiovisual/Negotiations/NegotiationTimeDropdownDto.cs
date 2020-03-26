// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-05-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-25-2020
// ***********************************************************************
// <copyright file="NegotiationTimeDropdownDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>NegotiationTimeDropdownDto</summary>
    public class NegotiationTimeDropdownDto
    {
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public int RoundNumber { get; set; }

        /// <summary>Initializes a new instance of the <see cref="NegotiationTimeDropdownDto"/> class.</summary>
        public NegotiationTimeDropdownDto()
        {
        }
    }
}