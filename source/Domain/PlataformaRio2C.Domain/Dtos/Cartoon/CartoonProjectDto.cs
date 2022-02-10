// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Franco
// Created          : 02-08-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 02-08-2022
// ***********************************************************************
// <copyright file="CartoonProjectDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CartoonProjectDto</summary>
    public class CartoonProjectDto
    {
        public CartoonProject CartoonProject { get; set; }
        public AttendeeCartoonProject AttendeeCartoonProject { get; set; }
        public IEnumerable<AttendeeCartoonProjectEvaluationDto> AttendeeCartoonProjectEvaluationsDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CartoonProjectDto"/> class.</summary>
        public CartoonProjectDto()
        {
        }
    }
}