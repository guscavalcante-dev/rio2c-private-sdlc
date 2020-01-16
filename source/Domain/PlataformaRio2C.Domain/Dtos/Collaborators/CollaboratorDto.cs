// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-16-2020
// ***********************************************************************
// <copyright file="OrganizationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CollaboratorDto</summary>
    public class CollaboratorDto : CollaboratorBaseDto
    {
        public string Website { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }

        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }

        public UserBaseDto CreatorDto { get; set; }
        public UserBaseDto UpdaterDto { get; set; }

        public AttendeeCollaboratorBaseDto EditionAttendeeCollaboratorBaseDto { get; set; }

        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitlesDtos { get; set; }
        public IEnumerable<CollaboratorMiniBioBaseDto> MiniBiosDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorDto"/> class.</summary>
        public CollaboratorDto()
        {
        }
    }
}