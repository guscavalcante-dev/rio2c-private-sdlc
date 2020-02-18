// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="CollaboratorApiListDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CollaboratorApiListDto</summary>
    public class CollaboratorApiListDto
    {
        public Guid Uid { get; set; }
        public string BadgeName { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public int? ApiHighlightPosition { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }

        public IEnumerable<CollaboratorMiniBioBaseDto> MiniBiosDtos { get; set; }
        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitlesDtos { get; set; }
        public IEnumerable<OrganizationApiListDto> OrganizationsDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorApiListDto"/> class.</summary>
        public CollaboratorApiListDto()
        {
        }

        /// <summary>Gets the collaborator mini bio base dto by language code.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public CollaboratorMiniBioBaseDto GetCollaboratorMiniBioBaseDtoByLanguageCode(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                languageCode = "pt-br";
            }

            return this.MiniBiosDtos?.FirstOrDefault(jtd => jtd.LanguageDto.Code == languageCode) ??
                   this.MiniBiosDtos?.FirstOrDefault(jtd => jtd.LanguageDto.Code == "pt-br");
        }

        /// <summary>Gets the collaborator job title base dto by language code.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public CollaboratorJobTitleBaseDto GetCollaboratorJobTitleBaseDtoByLanguageCode(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                languageCode = "pt-br";
            }

            return this.JobTitlesDtos?.FirstOrDefault(jtd => jtd.LanguageDto.Code == languageCode) ??
                   this.JobTitlesDtos?.FirstOrDefault(jtd => jtd.LanguageDto.Code == "pt-br");
        }
    }
}