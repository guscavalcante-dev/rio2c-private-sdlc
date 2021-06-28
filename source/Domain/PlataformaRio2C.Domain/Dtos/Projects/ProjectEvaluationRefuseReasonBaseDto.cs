// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-24-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-24-2021
// ***********************************************************************
// <copyright file="ProjectEvaluationRefuseReasonBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectEvaluationRefuseReasonBaseDto</summary>
    public class ProjectEvaluationRefuseReasonBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public int ProjectTypeId { get; set; }
        public string Name { get; set; }
        public bool HasAdditionalInfo { get; set; }
        public int DisplayOrder { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectEvaluationRefuseReasonBaseDto"/> class.</summary>
        public ProjectEvaluationRefuseReasonBaseDto()
        {
        }
    }
}