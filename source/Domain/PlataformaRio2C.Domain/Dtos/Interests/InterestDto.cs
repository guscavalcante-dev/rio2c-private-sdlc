// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-21-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="InterestDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>InterestDto</summary>
    public class InterestDto
    {
        public string InterestName { get; set; }
        public Guid InterestGroupUid { get; set; }
        public string InterestGroupName { get; set; }

        public Interest Interest { get; set; }
        public InterestGroup InterestGroup { get; set; }

        /// <summary>Initializes a new instance of the <see cref="InterestDto"/> class.</summary>
        public InterestDto()
        {
        }
    }
}