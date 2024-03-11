// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-27-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-27-2024
// ***********************************************************************
// <copyright file="AttendeeCreatorProjectEvaluationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCreatorProjectEvaluationDto</summary>
    public class AttendeeCreatorProjectEvaluationDto : BaseDto
    {
        public int AttendeeCreatorProjectId { get; set; }
        public int EvaluatorUserId { get; set; }
        public decimal Grade { get; set; }

        public CreatorProjectDto CreatorProjectDto { get; set; }
        public AttendeeCreatorProjectDto AttendeeCreatorProjectDto { get; set; }
        public UserDto EvaluatorUserDto { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCreatorProjectEvaluationDto"/> class.
        /// </summary>
        public AttendeeCreatorProjectEvaluationDto()
        {
        }
    }
}