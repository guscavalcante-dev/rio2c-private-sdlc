// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-16-2019
// ***********************************************************************
// <copyright file="FindHoldingByUidAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindHoldingByUidAsync</summary>
    public class FindHoldingByUidAsync : BaseQuery<HoldingDto>
    {
        public Guid HoldingUid { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindHoldingByUidAsync"/> class.</summary>
        /// <param name="holdingUid">The holding uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public FindHoldingByUidAsync(
            Guid? holdingUid,
            string userInterfaceLanguage)
        {
            this.HoldingUid = holdingUid ?? Guid.Empty;
            this.UpdateBaseProperties(userInterfaceLanguage);
        }
    }
}