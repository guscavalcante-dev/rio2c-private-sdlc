// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Almado
// Created          : 10-15-2019
//
// Last Modified By : William Almado
// Last Modified On : 10-15-2019
// ***********************************************************************
// <copyright file="CollaboratorSiteMainInformationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CollaboratorSiteMainInformationDto</summary>
    public class CollaboratorSiteMainInformationWidgetDto : CollaboratorBaseDto
    {
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }

        public UserBaseDto CreatorDto { get; set; }
        public UserBaseDto UpdaterDto { get; set; }

        public AttendeeCollaboratorBaseDto EditionAttendeeCollaboratorBaseDto { get; set; }

        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitlesDtos { get; set; }
        public IEnumerable<CollaboratorMiniBioBaseDto> MiniBiosDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorDto"/> class.</summary>
        public CollaboratorSiteMainInformationWidgetDto()
        {
        }
    }
}