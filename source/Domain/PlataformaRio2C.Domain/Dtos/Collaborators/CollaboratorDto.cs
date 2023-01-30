// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-23-2021
// ***********************************************************************
// <copyright file="CollaboratorDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>
    /// CollaboratorDto
    /// </summary>
    public class CollaboratorDto : CollaboratorBaseDto
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CollaboratorGenderId { get; set; }
        public CollaboratorGender Gender { get; set; }
        public string CollaboratorGenderAdditionalInfo { get; set; }
        public int? CollaboratorRoleId { get; set; }
        public CollaboratorRole CollaboratorRole { get; set; }
        public string CollaboratorRoleAdditionalInfo { get; set; }
        public int? CollaboratorIndustryId { get; set; }
        public CollaboratorIndustry Industry { get; set; }
        public string CollaboratorIndustryAdditionalInfo { get; set; }
        public bool? HasAnySpecialNeeds { get; set; }
        public string SpecialNeedsDescription { get; set; }
        public List<Guid> EditionsUids { get; set; }
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }

        public bool IsInCurrentEdition => this.EditionAttendeeCollaboratorBaseDto != null;

        public AttendeeCollaboratorBaseDto EditionAttendeeCollaboratorBaseDto { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorDto"/> class.
        /// </summary>
        public CollaboratorDto()
        {
        }
    }
}