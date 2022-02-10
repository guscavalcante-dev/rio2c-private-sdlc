// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-31-2021
// ***********************************************************************
// <copyright file="AttendeeCartoonProjectJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCartoonProjectJsonDto</summary>
    public class AttendeeCartoonProjectJsonDto
    {
        public int AttendeeCartoonProjectId { get; set; }
        public Guid AttendeeCartoonProjectUid { get; set; }
        public int CartoonProjectId { get; set; }
        public Guid CartoonProjectUid { get; set; }
        public string CartoonProjectName { get; set; }
        public string CartoonProjectServiceName { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public decimal? Grade { get; set; }
        public int EvaluationsCount { get; set; }
        public bool IsApproved { get; set; }

        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCartoonProjectJsonDto"/> class.</summary>
        public AttendeeCartoonProjectJsonDto()
        {
        }
    }
}