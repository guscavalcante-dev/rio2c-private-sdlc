// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-10-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-10-2023
// ***********************************************************************
// <copyright file="PlayerExecutiveReportDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>
    /// PlayerExecutiveReportDto
    /// </summary>
    public class PlayerExecutiveReportDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string FirstName { get; set; }
        public string LastNames { get; set; }
        public string Badge { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CellPhone { get; set; }
        public string PublicEmail { get; set; }
        public string Website { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public bool? HasAnySpecialNeeds { get; set; }
        public string SpecialNeedsDescription { get; set; }

        public AttendeeCollaborator EditionAttendeeCollaborator { get; set; }
        public CollaboratorIndustry Industry { get; set; }
        public CollaboratorRole Role { get; set; }
        public CollaboratorGender Gender { get; set; }

        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitleBaseDtos { get; set; }
        public IEnumerable<CollaboratorMiniBioBaseDto> MiniBioBaseDtos { get; set; }
        public IEnumerable<CollaboratorEditionParticipationBaseDto> EditionParticipationBaseDtos { get; set; }
        public IEnumerable<AttendeeOrganizationBaseDto> AttendeeOrganizationBasesDtos { get; set; }

        public DateTimeOffset? OnboardingStartDate => this.EditionAttendeeCollaborator?.OnboardingStartDate;
        public DateTimeOffset? OnboardingFinishDate => this.EditionAttendeeCollaborator?.OnboardingFinishDate;

        /// <summary>Gets the collaborator job title base dto by language code.</summary>
        /// <param name="culture">The language code.</param>
        /// <returns></returns>
        public CollaboratorJobTitleBaseDto GetJobTitleBaseDtoByLanguageCode(string culture)
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

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerExecutiveReportDto"/> class.
        /// </summary>
        public PlayerExecutiveReportDto()
        {
        }
    }
}