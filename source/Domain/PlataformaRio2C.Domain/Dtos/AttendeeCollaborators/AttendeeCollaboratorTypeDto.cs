// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-18-2019
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTypeDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorTypeDto</summary>
    public class AttendeeCollaboratorTypeDto
    {
        public AttendeeCollaboratorType AttendeeCollaboratorType { get; set; }
        public CollaboratorType CollaboratorType { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorTypeDto"/> class.</summary>
        public AttendeeCollaboratorTypeDto()
        {
        }
    }
}