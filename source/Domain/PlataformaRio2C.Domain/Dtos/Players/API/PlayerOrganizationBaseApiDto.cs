// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 12-15-2023
//
// Last Modified By : Elton Assunção
// Last Modified On : 01-05-2024
// ***********************************************************************
// <copyright file="PlayerOrganizationApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>PlayerOrganizationApiDto</summary>
    public class PlayerOrganizationBaseApiDto
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string TradeName { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public int? ApiHighlightPosition { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<OrganizationDescriptionBaseDto> OrganizationDescriptionBaseDtos { get; set; }
        public IEnumerable<OrganizationInterestDto> OrganizationInterestDtos { get; set; }
        public IEnumerable<CollaboratorDto> CollaboratorsDtos { get; set; }

        /// <summary>
        /// Gets the interest group API responses.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<InterestGroupApiResponse> GetInterestGroupApiResponses()
        {
            var groupedinterestsGroups = this.OrganizationInterestDtos?.GroupBy(oid => new
            {
                InterestGroupId = oid.InterestGroup.Id,
                InterestGroupUid = oid.InterestGroup.Uid,
                InterestGroupName = oid.InterestGroup.Name
            });

            var interestGroupApiResponses = groupedinterestsGroups?.Select(ig => new InterestGroupApiResponse
            {
                Uid = ig.Key.InterestGroupUid,
                Name = ig.Key.InterestGroupName,
                InterestsApiResponses = ig.Select(i => new InterestApiResponse
                {
                    Uid = i.Interest.Uid,
                    Name = i.Interest.Name
                })?.ToList()
            });

            return interestGroupApiResponses;
        }

        /// <summary>
        /// Gets the descriptions API responses.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LanguageValueApiResponse> GetDescriptionsApiResponses()
        {
            return this.OrganizationDescriptionBaseDtos?.Select(dto => new LanguageValueApiResponse
            {
                Culture = dto.LanguageDto.Code,
                Value = HttpUtility.HtmlDecode(dto.Value)
            });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerOrganizationBaseApiDto" /> class.
        /// </summary>
        public PlayerOrganizationBaseApiDto()
        {
        }
    }
}