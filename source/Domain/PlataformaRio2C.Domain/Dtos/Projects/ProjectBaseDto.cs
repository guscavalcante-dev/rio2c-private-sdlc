// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Sergio Almado Junior
// Created          : 12-13-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-31-2021
// ***********************************************************************
// <copyright file="ProjectBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectBaseDto</summary>
    public class ProjectBaseDto : ProjectDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string ProjectName { get; set; }
        public string ProducerName { get; set; }
        public DateTimeOffset? ProducerImageUploadDate { get; set; }
        public Guid ProducerUid { get; set; }
        public List<string> Genre { get; set; }
        public IEnumerable<ProjectInterestDto> Genres => this.GetAllInterestsByInterestGroupUid(InterestGroup.AudiovisualGenre.Uid);
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset? FinishDate { get; set; }
        public decimal? CommissionGrade { get; set; }
        public int CommissionEvaluationsCount { get; set; }
        public bool IsApproved { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectBaseDto"/> class.</summary>
        public ProjectBaseDto()
        {
        }
    }
}