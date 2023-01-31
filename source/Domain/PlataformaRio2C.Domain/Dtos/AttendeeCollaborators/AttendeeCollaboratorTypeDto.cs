// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-31-2023
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTypeDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorTypeDto</summary>
    public class AttendeeCollaboratorTypeDto
    {
        public int AttendeeCollaboratorId { get; set; }
        public int CollaboratorTypeId { get; set; }
        public bool IsApiDisplayEnabled { get; set; }
        public int? ApiHighlightPosition { get; set; }
        public DateTimeOffset? TermsAcceptanceDate { get; set; }

        public AttendeeCollaboratorType AttendeeCollaboratorType { get; set; }
        public CollaboratorType CollaboratorType { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorTypeDto"/> class.</summary>
        public AttendeeCollaboratorTypeDto()
        {
        }
    }
}