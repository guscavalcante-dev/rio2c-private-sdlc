// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-20-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-24-2021
// ***********************************************************************
// <copyright file="EditionMainInformationBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

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
        public DateTime? SellEndDate { get; set; }

        [Display(Name = nameof(OneToOneMeetingsScheduleDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? OneToOneMeetingsScheduleDate { get; set; }

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

            this.Name = editionDto.Edition.Name;
            this.UrlCode = editionDto.Edition.UrlCode;
            this.IsCurrent = editionDto.Edition.IsCurrent;
            this.IsActive = editionDto.Edition.IsActive;
            this.StartDate = editionDto.Edition.StartDate.ToBrazilTimeZone();
            this.EndDate = editionDto.Edition.EndDate.ToBrazilTimeZone();
            this.SellStartDate = editionDto.Edition.SellStartDate.ToBrazilTimeZone();
            this.SellEndDate = editionDto.Edition.SellEndDate.ToBrazilTimeZone();
            this.OneToOneMeetingsScheduleDate = editionDto.Edition.OneToOneMeetingsScheduleDate.ToBrazilTimeZone();
        }
    }
}