// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Franco
// Created          : 02-08-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 02-08-2022
// ***********************************************************************
// <copyright file="AttendeeCartoonOrganizationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ************************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCartoonProjectDto</summary>
    public class AttendeeCartoonProjectDto
    {
        public CartoonProject CartoonProject { get; set; }
        public AttendeeCartoonProject AttendeeCartoonProject { get; set; }
        public IEnumerable<AttendeeCartoonProjectEvaluationDto> AttendeeCartoonProjectEvaluationDtos { get; set; }
        //public IEnumerable<AttendeeCollaboratorCartoonEvaluationsWidgetDto> AttendeeCollaboratorCartoonEvaluationsWidgetDtos { get; set; }
        public IEnumerable<AttendeeCartoonProjectCollaboratorDto> AttendeeCartoonProjectCollaboratorDtos { get; set; }

        /// <summary>
        /// Gets the attendee cartoon organization evaluation by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public AttendeeCartoonProjectEvaluationDto GetAttendeeCartoonProjectEvaluationByUserId(int? userId)
        {
            if (!userId.HasValue)
                return null;

            if (this.AttendeeCartoonProjectEvaluationDtos == null)
            {
                this.AttendeeCartoonProjectEvaluationDtos = new List<AttendeeCartoonProjectEvaluationDto>();
            }

            return this.AttendeeCartoonProjectEvaluationDtos.FirstOrDefault(w => w.EvaluatorUser?.Id == userId);
        }

       
        /// <summary>
        /// Gets the attendee cartoon organization evaluation by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public AttendeeCartoonProjectCollaboratorDto GetAttendeeCartoonProjectEvaluationByEditionId(int? editionId)
        {
            if (!editionId.HasValue)
                return null;

            if (this.AttendeeCartoonProjectCollaboratorDtos == null)
            {
                this.AttendeeCartoonProjectCollaboratorDtos = new List<AttendeeCartoonProjectCollaboratorDto>();
            }

            return this.AttendeeCartoonProjectCollaboratorDtos.FirstOrDefault(w => w.AttendeeCollaborator.EditionId == editionId);
        }

        public AttendeeCartoonProjectCollaboratorDto GetAttendeeCartoonProjectCollaboratorDtoCollaboratorByEditionId(int? editionId)
        {
            if (!editionId.HasValue)
                return null;

            if (this.AttendeeCartoonProjectCollaboratorDtos == null)
            {
                this.AttendeeCartoonProjectCollaboratorDtos = new List<AttendeeCartoonProjectCollaboratorDto>();
            }

            return this.AttendeeCartoonProjectCollaboratorDtos.FirstOrDefault(w => w.AttendeeCollaborator.EditionId == editionId);
        }


        /// <summary>Initializes a new instance of the <see cref="AttendeeCartoonProjectDto"/> class.</summary>
        public AttendeeCartoonProjectDto()
        {
        }
    }
}