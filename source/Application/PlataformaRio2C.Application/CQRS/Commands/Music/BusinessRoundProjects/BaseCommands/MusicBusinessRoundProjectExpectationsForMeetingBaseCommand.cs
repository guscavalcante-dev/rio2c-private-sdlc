
// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Daniel Giese Rodrigues
// Created          : 23-01-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 23-01-2025
// ***********************************************************************
// <copyright file="ProjectLogLineBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>ProjectLogLineBaseCommand</summary>
    public class MusicBusinessRoundProjectExpectationsForMeetingBaseCommand : BaseCommand
    {
        public static readonly int ValueMaxLength = 256;
        public static readonly int ValueMinLength = 1;
        public int MusicBusinessRoundProjectId { get; set; }
        public int LanguageId { get; set; }
        
        [Display(Name = "ExpectationsForMeeting", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(256, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public bool IsRequired { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectExpectationsForMeetingBaseCommand"/> class.
        /// </summary>
        public MusicBusinessRoundProjectExpectationsForMeetingBaseCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectExpectationsForMeetingBaseCommand"/> class 
        /// based on an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public MusicBusinessRoundProjectExpectationsForMeetingBaseCommand(MusicBusinessRoundProjectExpectationsForMeeting entity)
        {
            this.MusicBusinessRoundProjectId = entity.MusicBusinessRoundProjectId;
            this.LanguageId = entity.LanguageId;
            this.Value = entity.Value?.Trim();
            this.LanguageCode = entity.Language?.Code;
            this.LanguageName = entity.Language?.Name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectExpectationsForMeetingBaseCommand"/> class 
        /// using language details and value.
        /// </summary>
        /// <param name="value">The expectation value.</param>
        /// <param name="languageDto">The language details.</param>
        /// <param name="musicBusinessRoundProjectId">The ID of the music business round project.</param>
        public MusicBusinessRoundProjectExpectationsForMeetingBaseCommand(string value, LanguageDto languageDto, int musicBusinessRoundProjectId)
        {
            this.Value = value?.Trim();
            this.LanguageId = languageDto.Id;
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
            this.MusicBusinessRoundProjectId = musicBusinessRoundProjectId;
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectSummaryBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        public MusicBusinessRoundProjectExpectationsForMeetingBaseCommand(MusicBusinessRoundProjectExpectationsForMeetingDto entity, bool isRequired)
        {
            this.Value = entity.Value;
            this.LanguageCode = entity.Language.Code;
            this.LanguageName = entity.Language.Name;
            this.IsRequired = isRequired;
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectSummaryBaseCommand"/> class.</summary>
        /// <param name="languageDto">The language dto.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        public MusicBusinessRoundProjectExpectationsForMeetingBaseCommand(LanguageDto languageDto, bool isRequired)
        {
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
            this.IsRequired = isRequired;
        }
    }
}