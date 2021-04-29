// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="CollaboratorBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CollaboratorBaseDto</summary>
    public class CollaboratorBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }

        public bool? Active { get; set; }
        public Guid? UserUid { get; set; }

        //[ScriptIgnore]
        public UserDto User { get; set; }

        public string FullName => this.FirstName + (!string.IsNullOrEmpty(this.LastNames) ? " " + this.LastNames : String.Empty);
        public string FirstName { get; set; }
        public string LastNames { get; set; }
        public string Badge { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CellPhone { get; set; }
        public string PublicEmail { get; set; }
        public string JobTitle { get; set; }

        public HoldingBaseDto HoldingBaseDto { get; set; }
        public OrganizationBaseDto OrganizatioBaseDto { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public DateTimeOffset? CurrentEditionOnboardingFinishDate => EditionAttendeeCollaborator?.OnboardingFinishDate;
        public DateTimeOffset? SpeakerCurrentEditionOnboardingFinishDate => EditionAttendeeCollaborator?.SpeakerTermsAcceptanceDate;
        public bool IsInCurrentEdition => EditionAttendeeCollaborator != null;
        public bool IsInOtherEdition { get; set; }

        public IEnumerable<AttendeeOrganizationBaseDto> AttendeeOrganizationBasesDtos { get; set; }
        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitlesDtos { get; set; }


        public Role Role { get; set; }
        public List<AttendeeCollaboratorTypeDto> AttendeeCollaboratorTypeDtos { get; set; }
        public string CollaboratorTypeNames => GetAttendeeCollaboratorTypesNames();
        public string RoleWithCollaboratorTypeNameHtmlString
        {
            get
            {
                var name = "";
                var roleName = this.Role?.Name;

                if (!string.IsNullOrEmpty(roleName) && !string.IsNullOrEmpty(this.CollaboratorTypeNames))
                {
                    name = $"{roleName}<br/>{this.CollaboratorTypeNames}";
                }
                else if (!string.IsNullOrEmpty(this.CollaboratorTypeNames))
                {
                    name = this.CollaboratorTypeNames;
                }
                else if (!string.IsNullOrEmpty(roleName))
                {
                    name = roleName;
                }

                return name;
            }
        }
        public bool? IsAdminFull => this.Role?.Name == Constants.Role.Admin;

        #region Private Methods

        /// <summary>
        /// Gets the attendee collaborator types names.
        /// </summary>
        /// <returns></returns>
        private string GetAttendeeCollaboratorTypesNames()
        {
            if (AttendeeCollaboratorTypeDtos != null && AttendeeCollaboratorTypeDtos.Count > 0)
            {
                return this.AttendeeCollaboratorTypeDtos.Select(act => act.CollaboratorType.Name).ToList().ToString("<br/>");
            }
            else
            {
                return "";
            }
        }

        #endregion


        #region Json ignored properties 

        [ScriptIgnore]
        public AttendeeCollaborator EditionAttendeeCollaborator { get; set; }

        #endregion

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

        /// <summary>Initializes a new instance of the <see cref="CollaboratorBaseDto"/> class.</summary>
        public CollaboratorBaseDto()
        {
        }
    }
}