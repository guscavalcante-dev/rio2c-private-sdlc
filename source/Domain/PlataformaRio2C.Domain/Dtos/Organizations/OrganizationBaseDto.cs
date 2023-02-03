// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-25-2021
// ***********************************************************************
// <copyright file="OrganizationBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>OrganizationBaseDto</summary>
    public class OrganizationBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public string TradeName { get; set; }
        public HoldingBaseDto HoldingBaseDto { get; set; }
        public bool IsCompanyNumberRequired { get; set; }
        public bool? IsVirtualMeeting { get; set; }
        public string Document { get; set; }
        public string Website { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public bool IsInCurrentEdition { get; set; }
        public bool IsInOtherEdition { get; set; }

        public bool IsApiDisplayEnabled { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationBaseDto"/> class.</summary>
        public OrganizationBaseDto()
        {
        }

        /// <summary>
        /// Determines whether this instance has image.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has image; otherwise, <c>false</c>.
        /// </returns>
        public bool HasImage()
        {
            return this.ImageUploadDate.HasValue;
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <returns></returns>
        public string DisplayName => this.TradeName ?? this.Name;

        /// <summary>
        /// Gets the trade name abbreviation.
        /// </summary>
        /// <returns></returns>
        public string GetTradeNameAbbreviation()
        {
            return this.DisplayName.GetTwoLetterCode();
        }
    }
}