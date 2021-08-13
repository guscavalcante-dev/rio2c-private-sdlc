// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-24-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationJsonDto</summary>
    public class AttendeeInnovationOrganizationJsonDto
    {
        public int AttendeeInnovationOrganizationId { get; set; }
        public Guid AttendeeInnovationOrganizationUid { get; set; }
        public int InnovationOrganizationId { get; set; }
        public Guid InnovationOrganizationUid { get; set; }
        public string InnovationOrganizationName { get; set; }
        public string InnovationOrganizationServiceName { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public decimal? Grade { get; set; }
        public int EvaluationsCount { get; set; }
        public string EvaluationHtmlString { get; set; }
        public string MenuActionsHtmlString { get; set; }

        public IList<string> InnovationOrganizationTracksNames { get; set; }

        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationJsonDto"/> class.</summary>
        public AttendeeInnovationOrganizationJsonDto()
        {
        }
    }
}