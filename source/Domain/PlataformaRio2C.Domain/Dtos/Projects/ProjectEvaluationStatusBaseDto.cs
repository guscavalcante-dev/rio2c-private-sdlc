// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-24-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-24-2021
// ***********************************************************************
// <copyright file="ProjectEvaluationStatusBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectEvaluationStatusBaseDto</summary>
    public class ProjectEvaluationStatusBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsEvaluated { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectEvaluationStatusBaseDto"/> class.</summary>
        public ProjectEvaluationStatusBaseDto()
        {
        }
    }
}