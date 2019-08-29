// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-28-2019
// ***********************************************************************
// <copyright file="FindAllAttendeeOrganizationsBaseDtosByEditionUidAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAllAttendeeOrganizationsBaseDtosByEditionUidAsync</summary>
    public class FindAllAttendeeOrganizationsBaseDtosByEditionUidAsync : BaseQuery<List<AttendeeOrganizationBaseDto>>
    {
        public int EditionId { get; private set; }
        public bool ShowAllEditions { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindAllAttendeeOrganizationsBaseDtosByEditionUidAsync"/> class.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindAllAttendeeOrganizationsBaseDtosByEditionUidAsync(int? editionId,  bool showAllEditions, string userInterfaceLanguage)
            : base(userInterfaceLanguage)
        {
            this.EditionId = editionId ?? 0;
            this.ShowAllEditions = showAllEditions;
        }
    }
}