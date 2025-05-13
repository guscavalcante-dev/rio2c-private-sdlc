// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-28-2019
// ***********************************************************************
// <copyright file="FindCollaboratorDtoByUidAndByEditionIdAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindCollaboratorDtoByUidAndByEditionIdAsync</summary>
    public class FindAllCollaboratorGenderAsync : BaseQuery<List<CollaboratorGender>>
    {
        /// <summary>Initializes a new instance of the <see cref="FindAllLanguagesDtosAsync"/> class.</summary>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindAllCollaboratorGenderAsync(string userInterfaceLanguage)
        {
            this.UpdatePreSendProperties(userInterfaceLanguage);
        }
    }
}