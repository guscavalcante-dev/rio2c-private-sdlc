// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-09-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-09-2022
// ***********************************************************************
// <copyright file="AttendeeCartoonProjectJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CartoonProjectJsonDto</summary>
    public class AttendeeCartoonProjectJsonDto
    {
        public int AttendeeCartoonProjectId { get; set; }
        public Guid AttendeeCartoonProjectUid { get; set; }
        public int CartoonProjectId { get; set; }
        public Guid CartoonProjectUid { get; set; }
        public string CartoonProjectTitle { get; set; }
        public string CartoonProjectTitleAbbreviation => this.CartoonProjectTitle?.GetTwoLetterCode() ?? "-";
        public string CartoonProjectFormatName { get; set; }
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