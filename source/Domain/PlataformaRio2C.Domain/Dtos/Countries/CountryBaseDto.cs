// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-21-2019
// ***********************************************************************
// <copyright file="CountryBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Globalization;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CountryBaseDto</summary>
    public class CountryBaseDto : BaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CompanyNumberMask { get; set; }
        public string ZipCodeMask { get; set; }
        public string PhoneNumberMask { get; set; }
        public string MobileMask { get; set; }
        public bool IsCompanyNumberRequired { get; set; }
        public int Ordering { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CountryBaseDto"/> class.</summary>
        public CountryBaseDto()
        {
        }

        /// <summary>Gets the display name.</summary>
        /// <value>The display name.</value>
        public string DisplayName => this.Name.GetSeparatorTranslation(CultureInfo.CurrentCulture.ToString(), '|');
    }
}