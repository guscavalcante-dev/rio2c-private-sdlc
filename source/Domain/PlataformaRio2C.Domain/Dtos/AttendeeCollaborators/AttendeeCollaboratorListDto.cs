// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-11-2020
// ***********************************************************************
// <copyright file="AttendeeCollaboratorBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorBaseDto</summary>
    public class AttendeeCollaboratorListDto
    {
        public Guid Uid { get; set; }
        public Guid CollaboratorUid { get; set; }
        public string Name { get; set; }
        public string BadgeName { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public IEnumerable<OrganizationApiListDto> OrganizationsDtos { get; set; }
        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitlesDtos { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }

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