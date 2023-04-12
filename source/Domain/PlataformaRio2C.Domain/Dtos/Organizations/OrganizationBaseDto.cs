// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-12-2023
// ***********************************************************************
// <copyright file="OrganizationBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
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
        public string CompanyName { get; set; }
        public string Document { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public bool IsCompanyNumberRequired { get; set; }
        public bool? IsVirtualMeeting { get; set; }
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public bool IsInCurrentEdition { get; set; }
        public bool IsInOtherEdition { get; set; }
		public bool IsApiDisplayEnabled { get; set; }
        public int ReceivedProjectsCount { get; set; }
		
        public HoldingBaseDto HoldingBaseDto { get; set; }
        public UserBaseDto CreatorBaseDto { get; set; }
        public UserBaseDto UpdaterBaseDto { get; set; }
        public AddressBaseDto AddressBaseDto { get; set; }

        public IEnumerable<OrganizationDescriptionBaseDto> OrganizationDescriptionBaseDtos { get; set; }
        public IEnumerable<OrganizationRestrictionSpecificBaseDto> OrganizationRestrictionSpecificBaseDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationBaseDto"/> class.</summary>
        public OrganizationBaseDto()
        {
        }

        #region Extension Methods and Extension Properties

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <returns></returns>
        public string DisplayName => this.TradeName ?? this.Name;

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
        /// Gets the trade name abbreviation.
        /// </summary>
        /// <returns></returns>
        public string GetTradeNameAbbreviation()
        {
            return this.DisplayName.GetTwoLetterCode();
        }

        /// <summary>
        /// Gets the organization description base dto by language code.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public OrganizationDescriptionBaseDto GetOrganizationDescriptionBaseDtoByLanguageCode(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                languageCode = "pt-br";
            }

            return this.OrganizationDescriptionBaseDtos?.FirstOrDefault(odbDto => odbDto.LanguageDto?.Code == languageCode) ??
                   this.OrganizationDescriptionBaseDtos?.FirstOrDefault(odbDto => odbDto.LanguageDto?.Code == "pt-br");
        }

        #endregion
    }
}