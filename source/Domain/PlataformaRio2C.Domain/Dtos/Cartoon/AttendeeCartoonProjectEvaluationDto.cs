// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Franco
// Created          : 02-08-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 02-08-2022
// ***********************************************************************
// <copyright file="AttendeeCartoonProjectEvaluationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCartoonProjectEvaluationDto</summary>
    public class AttendeeCartoonProjectEvaluationDto
    {
        public CartoonProject CartoonProject { get; set; }
        public AttendeeCartoonProject AttendeeCartoonProject { get; set; }
        
        public AttendeeCartoonProjectEvaluation AttendeeCartoonProjectEvaluation { get; set; }
        public User EvaluatorUser { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCartoonProjectEvaluationDto"/> class.
        /// </summary>
        public AttendeeCartoonProjectEvaluationDto()
        {
        }
    }
}