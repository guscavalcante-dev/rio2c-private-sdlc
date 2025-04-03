// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Daniel Giese Rodrigues
// Created          : 03-14-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 03-14-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundNegotiationAttendeeOrganizationBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicBusinessRoundNegotiationAttendeeOrganizationBaseDto</summary>

    public class MusicBusinessRoundNegotiationAttendeeCollaboratorBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public CollaboratorDto CollaboratorDto { get; set; }
        public EditionBaseDto EditionBaseDto { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public IEnumerable<AttendeeCollaboratorBaseDto> AttendeeCollaboratorBaseDtos { get; set; }
        public IEnumerable<MusicBusinessRoundNegotiationBaseDto> MusicBusinessRoundNegotiationBaseDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationAttendeeCollaboratorBaseDto"/> class.</summary>
        public MusicBusinessRoundNegotiationAttendeeCollaboratorBaseDto()
        {
        }
    }
}
