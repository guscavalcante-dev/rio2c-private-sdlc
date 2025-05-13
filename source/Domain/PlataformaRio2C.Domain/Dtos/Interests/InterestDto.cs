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

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>InterestDto</summary>
    public class InterestDto
    {
        public Guid InterestUid { get; set; }
        public string InterestName { get; set; }
        public Guid InterestGroupUid { get; set; }
        public string InterestGroupName { get; set; }

        [Obsolete("Use the 'InterestName' property instead of this. This property will be deleted!")]
        public Interest Interest { get; set; }

        [Obsolete("Use the 'InterestGroupUid' and 'InterestGroupName' properties instead of this. This property will be deleted!")]
        public InterestGroup InterestGroup { get; set; }

        /// <summary>Initializes a new instance of the <see cref="InterestDto"/> class.</summary>
        public InterestDto()
        {
        }
    }
}