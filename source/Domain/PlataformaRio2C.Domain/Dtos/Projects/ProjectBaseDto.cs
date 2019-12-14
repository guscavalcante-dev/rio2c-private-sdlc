// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Sergio Almado Junior
// Created          : 12-13-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 12-13-2019
// ***********************************************************************
// <copyright file="ProjectBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectBaseDto</summary>
    public class ProjectBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string ProjectName { get; set; } 
        public string ProducerName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? FinishDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectBaseDto"/> class.</summary>
        public ProjectBaseDto()
        {
        }
    }
}