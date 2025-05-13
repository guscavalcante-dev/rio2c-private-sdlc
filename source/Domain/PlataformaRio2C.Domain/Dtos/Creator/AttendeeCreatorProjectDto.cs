// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-27-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-27-2024
// ***********************************************************************
// <copyright file="AttendeeCreatorProjectDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCreatorProjectDto</summary>
    public class AttendeeCreatorProjectDto : BaseDto
    {
        public int CreatorProjectId { get; set; }
        public int EditionId { get; set; }
        public decimal? Grade { get; set; }
        public int EvaluationsCount { get; set; }
        public DateTimeOffset? LastEvaluationDate { get; set; }
        public DateTimeOffset? EvaluationEmailSendDate { get; set; }

        public CreatorProjectDto CreatorProjectDto { get; set; }
        public IEnumerable<AttendeeCreatorProjectEvaluationDto> AttendeeCreatorProjectEvaluationDtos { get; set; }

        /// <summary>
        /// Gets the attendee creator project evaluation by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public AttendeeCreatorProjectEvaluationDto GetAttendeeCreatorProjectEvaluationByUserId(int? userId)
        {
            if (!userId.HasValue)
                return null;

            if (this.AttendeeCreatorProjectEvaluationDtos == null)
            {
                this.AttendeeCreatorProjectEvaluationDtos = new List<AttendeeCreatorProjectEvaluationDto>();
            }

            return this.AttendeeCreatorProjectEvaluationDtos.FirstOrDefault(w => w.EvaluatorUserId == userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCreatorProjectDto"/> class.
        /// </summary>
        public AttendeeCreatorProjectDto()
        {
        }
    }
}