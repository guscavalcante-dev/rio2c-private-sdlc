// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-03-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-03-2024
// ***********************************************************************
// <copyright file="PlayerCollaboratorBaseApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>PlayerCollaboratorBaseApiDto</summary>
    public class PlayerCollaboratorBaseApiDto
    {
        public Guid Uid { get; set; }
        public string FirstName { get; set; }
        public string LastNames { get; set; }
        public string Badge { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public int? ApiHighlightPosition { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public bool IsDeleted { get; set; }

        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitleBaseDtos { get; set; }
        public IEnumerable<CollaboratorMiniBioBaseDto> MiniBioBaseDtos { get; set; }

        public string FullName => this.FirstName + (!string.IsNullOrEmpty(this.LastNames) ? " " + this.LastNames : String.Empty);

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerCollaboratorBaseApiDto" /> class.
        /// </summary>
        public PlayerCollaboratorBaseApiDto()
        {
        }

        /// <summary>Gets the collaborator job title base dto by language code.</summary>
        /// <param name="culture">The language code.</param>
        /// <returns></returns>
        public CollaboratorJobTitleBaseDto GetCollaboratorJobTitleBaseDtoByLanguageCode(string culture)
        {
            if (string.IsNullOrEmpty(culture))
            {
                culture = "pt-br";
            }

            return this.JobTitleBaseDtos?.FirstOrDefault(jtd => jtd.LanguageDto.Code == culture) ??
                   this.JobTitleBaseDtos?.FirstOrDefault(jtd => jtd.LanguageDto.Code == "pt-br");
        }

        /// <summary>
        /// Gets the mini bio base dto by language code.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public CollaboratorMiniBioBaseDto GetMiniBioBaseDtoByLanguageCode(string culture)
        {
            return this.MiniBioBaseDtos?.FirstOrDefault(mbd => mbd.LanguageDto.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        }

    }
}