// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-01-2020
// ***********************************************************************
// <copyright file="MusicProjectEvaluationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicProjectEvaluationDto</summary>
    public class MusicProjectEvaluationDto
    {
        public User EvaluationCollaboratorUser { get; set; }
        public Collaborator EvaluationCollaborator { get; set; }
        public ProjectEvaluationStatus ProjectEvaluationStatus { get; set; }
        public ProjectEvaluationRefuseReason ProjectEvaluationRefuseReason { get; set; }
        public string Reason { get; set; }
        public DateTimeOffset? EvaluationDate;

        /// <summary>Initializes a new instance of the <see cref="MusicProjectEvaluationDto"/> class.</summary>
        public MusicProjectEvaluationDto()
        {
        }
    }
}