// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-22-2021
// ***********************************************************************
// <copyright file="AttendeeOrganizationMainInformationWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeOrganizationMainInformationWidgetDto</summary>
    public class AttendeeOrganizationMainInformationWidgetDto
    {
        public AttendeeOrganization AttendeeOrganization { get; set; }
        public Organization Organization { get; set; }
        public Country Country { get; set; }
        public State State { get; set; }
        public bool? IsInCurrentEdition { get; set; }

        public IEnumerable<OrganizationDescriptionBaseDto> DescriptionsDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationMainInformationWidgetDto"/> class.</summary>
        public AttendeeOrganizationMainInformationWidgetDto()
        {
        }

        /// <summary>Gets the description dto by language code.</summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public OrganizationDescriptionBaseDto GetDescriptionDtoByLanguageCode(string culture)
        {
            return this.DescriptionsDtos?.FirstOrDefault(dd => dd.LanguageDto.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        }

        /// <summary>Gets the location.</summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public string GetLocation(string culture)
        {
            if (this.Country == null)
            {
                return string.Empty;
            }

            var location = this.Country?.Name?.GetSeparatorTranslation(culture, '|');

            if (this.State != null)
            {
                location += "/" + this.State.Name;
            }

            return location;
        }
    }
}