﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-20-2021
//
// Last Modified By : Elton Assunção
// Last Modified On : 02-01-2023
// ***********************************************************************
// <copyright file="EditionMainInformationBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>EditionMainInformationBaseCommand</summary>
    public class EditionMainInformationBaseCommand : BaseCommand
    {
        public new Guid EditionUid { get; set; }

        [Display(Name = nameof(Name), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string Name { get; set; }

        [Display(Name = nameof(UrlCode), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? UrlCode { get; set; }

        [Display(Name = nameof(IsCurrent), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool IsCurrent { get; set; }

        [Display(Name = nameof(IsActive), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool IsActive { get; set; }

        [Display(Name = nameof(StartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? StartDate { get; set; }

        [Display(Name = nameof(EndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [GreaterThanOrEqualTo(nameof(StartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? EndDate { get; set; }

        [Display(Name = nameof(SellStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? SellStartDate { get; set; }

        [Display(Name = nameof(SellEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [GreaterThanOrEqualTo(nameof(SellStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? SellEndDate { get; set; }

        [Display(Name = nameof(OneToOneMeetingsScheduleDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? OneToOneMeetingsScheduleDate { get; set; }

        [Display(Name = nameof(SpeakersApiHighlightPositionsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int SpeakersApiHighlightPositionsCount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditionMainInformationBaseCommand"/> class.
        /// </summary>
        /// <param name="editionDto">The edition dto.</param>
        public EditionMainInformationBaseCommand(EditionDto editionDto)
        {
            this.UpdateMainInformation(editionDto);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditionMainInformationBaseCommand"/> class.
        /// </summary>
        public EditionMainInformationBaseCommand()
        {
        }

        /// <summary>
        /// Updates the main information.
        /// </summary>
        /// <param name="editionDto">The edition dto.</param>
        private void UpdateMainInformation(EditionDto editionDto)
        {
            this.EditionUid = editionDto?.Edition?.Uid ?? Guid.Empty;

            if (editionDto?.Edition == null)
                return;

            this.Name = editionDto.Name;
            this.UrlCode = editionDto.UrlCode;
            this.IsCurrent = editionDto.IsCurrent;
            this.IsActive = editionDto.IsActive;
            this.StartDate = editionDto.StartDate.ToBrazilTimeZone();
            this.EndDate = editionDto.EndDate.ToBrazilTimeZone();
            this.SellStartDate = editionDto.SellStartDate.ToBrazilTimeZone();
            this.SellEndDate = editionDto.SellEndDate.ToBrazilTimeZone();
            this.OneToOneMeetingsScheduleDate = editionDto.OneToOneMeetingsScheduleDate.ToBrazilTimeZone();
            this.SpeakersApiHighlightPositionsCount = editionDto.SpeakersApiHighlightPositionsCount;
        }
    }
}