// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-10-2019
// ***********************************************************************
// <copyright file="AttendeeOrganizationSiteAddressWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeOrganizationSiteAddressWidgetDto</summary>
    public class AttendeeOrganizationSiteAddressWidgetDto
    {
        public AttendeeOrganization AttendeeOrganization { get; set; }
        public Organization Organization { get; set; }
        public Address Address { get; set; }
        public Country Country { get; set; }
        public State State { get; set; }
        public City City { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationSiteAddressWidgetDto"/> class.</summary>
        public AttendeeOrganizationSiteAddressWidgetDto()
        {
        }

        /// <summary>Gets the name of the country.</summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public string GetCountryName(string culture)
        {
            return this.Country?.Name?.GetSeparatorTranslation(culture, '|');
        }

        /// <summary>Gets the address base dto.</summary>
        /// <returns></returns>
        public AddressBaseDto GetAddressBaseDto()
        {
            return new AddressBaseDto
            {
                Id = this.Address?.Id,
                Uid = this.Address?.Uid,
                CountryUid = this.Country?.Uid,
                StateUid = this.State?.Uid,
                CityUid = this.City?.Uid,
                Address1 = this.Address?.Address1,
                AddressZipCode = this.Address?.ZipCode
            };
        }
    }
}