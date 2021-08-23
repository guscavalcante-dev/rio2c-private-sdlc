// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-20-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-20-2021
// ***********************************************************************
// <copyright file="EditionBaseCommand.cs" company="Softo">
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
    /// <summary>EditionBaseCommand</summary>
    public class EditionBaseCommand : BaseCommand
    {
        public new Guid EditionUid { get; set; }

        #region Main Information

        public bool IsMainInformationRequired { get; set; }

        [Display(Name = nameof(Name), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsMainInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string Name { get; set; }

        [Display(Name = nameof(UrlCode), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsMainInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? UrlCode { get; set; }

        [Display(Name = nameof(IsCurrent), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsMainInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool IsCurrent { get; set; }

        [Display(Name = nameof(IsActive), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsMainInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool IsActive { get; set; }

        [Display(Name = nameof(StartDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsMainInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? StartDate { get; set; }

        [Display(Name = nameof(EndDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsMainInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [GreaterThanOrEqualTo(nameof(StartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? EndDate { get; set; }

        [Display(Name = nameof(SellStartDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsMainInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? SellStartDate { get; set; }

        [Display(Name = nameof(SellEndDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsMainInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? SellEndDate { get; set; }

        [Display(Name = nameof(OneToOneMeetingsScheduleDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsMainInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? OneToOneMeetingsScheduleDate { get; set; }

        #endregion

        #region Dates Information

        public bool IsDatesInformationRequired { get; set; }

        #region Music - Commissions

        [Display(Name = nameof(MusicProjectSubmitStartDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? MusicProjectSubmitStartDate { get; set; }

        [Display(Name = nameof(MusicProjectSubmitEndDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? MusicProjectSubmitEndDate { get; set; }

        [Display(Name = nameof(MusicCommissionEvaluationStartDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? MusicCommissionEvaluationStartDate { get; set; }

        [Display(Name = nameof(MusicCommissionEvaluationEndDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? MusicCommissionEvaluationEndDate { get; set; }

        [Display(Name = nameof(MusicCommissionMaximumApprovedBandsCount), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? MusicCommissionMaximumApprovedBandsCount { get; set; }

        [Display(Name = nameof(MusicCommissionMinimumEvaluationsCount), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? MusicCommissionMinimumEvaluationsCount { get; set; }

        #endregion

        #region Innovation - Commissions

        [Display(Name = nameof(InnovationProjectSubmitStartDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? InnovationProjectSubmitStartDate { get; set; }

        [Display(Name = nameof(InnovationProjectSubmitEndDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? InnovationProjectSubmitEndDate { get; set; }

        [Display(Name = nameof(InnovationCommissionEvaluationStartDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? InnovationCommissionEvaluationStartDate { get; set; }

        [Display(Name = nameof(InnovationCommissionEvaluationEndDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? InnovationCommissionEvaluationEndDate { get; set; }

        [Display(Name = nameof(InnovationCommissionMaximumApprovedCompaniesCount), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? InnovationCommissionMaximumApprovedCompaniesCount { get; set; }

        [Display(Name = nameof(InnovationCommissionMinimumEvaluationsCount), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? InnovationCommissionMinimumEvaluationsCount { get; set; }

        #endregion

        #region Audiovisual - Commissions

        [Display(Name = nameof(AudiovisualCommissionEvaluationStartDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? AudiovisualCommissionEvaluationStartDate { get; set; }

        [Display(Name = nameof(AudiovisualCommissionEvaluationEndDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? AudiovisualCommissionEvaluationEndDate { get; set; }

        [Display(Name = nameof(AudiovisualCommissionMaximumApprovedProjectsCount), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? AudiovisualCommissionMaximumApprovedProjectsCount { get; set; }

        [Display(Name = nameof(AudiovisualCommissionMinimumEvaluationsCount), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? AudiovisualCommissionMinimumEvaluationsCount { get; set; }

        #endregion

        #region Audiovisual - Negotiations

        [Display(Name = nameof(ProjectSubmitStartDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? ProjectSubmitStartDate { get; set; }

        [Display(Name = nameof(ProjectSubmitEndDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? ProjectSubmitEndDate { get; set; }

        [Display(Name = nameof(ProjectEvaluationStartDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? ProjectEvaluationStartDate { get; set; }

        [Display(Name = nameof(ProjectEvaluationEndDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? ProjectEvaluationEndDate { get; set; }

        [Display(Name = nameof(NegotiationStartDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? NegotiationStartDate { get; set; }

        [Display(Name = nameof(NegotiationEndDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? NegotiationEndDate { get; set; }

        [Display(Name = nameof(AudiovisualNegotiationsCreateStartDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? AudiovisualNegotiationsCreateStartDate { get; set; }

        [Display(Name = nameof(AudiovisualNegotiationsCreateEndDate), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? AudiovisualNegotiationsCreateEndDate { get; set; }

        [Display(Name = nameof(AttendeeOrganizationMaxSellProjectsCount), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? AttendeeOrganizationMaxSellProjectsCount { get; set; }

        [Display(Name = nameof(ProjectMaxBuyerEvaluationsCount), ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsDatesInformationRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? ProjectMaxBuyerEvaluationsCount { get; set; }

        #endregion

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="EditionBaseCommand"/> class.
        /// </summary>
        public EditionBaseCommand(EditionDto editionDto, bool isMainInformationRequired, bool isDatesInformationRequired)
        {
            this.UpdateMainInformation(editionDto, isMainInformationRequired);
            this.UpdateDatesInformation(editionDto, isDatesInformationRequired);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditionBaseCommand"/> class.
        /// </summary>
        public EditionBaseCommand()
        {
        }

        /// <summary>
        /// Updates the main information.
        /// </summary>
        /// <param name="editionDto">The edition dto.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        private void UpdateMainInformation(EditionDto editionDto, bool isRequired)
        {
            this.IsMainInformationRequired = isRequired;
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

        /// <summary>
        /// Updates the dates information.
        /// </summary>
        /// <param name="editionDto">The edition dto.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        private void UpdateDatesInformation(EditionDto editionDto, bool isRequired)
        {
            this.IsDatesInformationRequired = isRequired;
            this.EditionUid = editionDto?.Edition?.Uid ?? Guid.Empty;

            if (editionDto?.Edition == null)
                return;

            // Audiovisual - Negotiations
            this.ProjectSubmitStartDate = editionDto.Edition.ProjectSubmitStartDate.ToBrazilTimeZone();
            this.ProjectSubmitEndDate = editionDto.Edition.ProjectSubmitEndDate.ToBrazilTimeZone();
            this.ProjectEvaluationStartDate = editionDto.Edition.ProjectEvaluationStartDate.ToBrazilTimeZone();
            this.ProjectEvaluationEndDate = editionDto.Edition.ProjectEvaluationEndDate.ToBrazilTimeZone();
            this.NegotiationStartDate = editionDto.Edition.NegotiationStartDate.ToBrazilTimeZone();
            this.NegotiationEndDate = editionDto.Edition.NegotiationEndDate.ToBrazilTimeZone();
            this.AttendeeOrganizationMaxSellProjectsCount = editionDto.Edition.AttendeeOrganizationMaxSellProjectsCount;
            this.ProjectMaxBuyerEvaluationsCount = editionDto.Edition.ProjectMaxBuyerEvaluationsCount;

            // Music - Commissions
            this.MusicProjectSubmitStartDate = editionDto.Edition.MusicProjectSubmitStartDate.ToBrazilTimeZone();
            this.MusicProjectSubmitEndDate = editionDto.Edition.MusicProjectSubmitEndDate.ToBrazilTimeZone();
            this.MusicCommissionEvaluationStartDate = editionDto.Edition.MusicCommissionEvaluationStartDate.ToBrazilTimeZone();
            this.MusicCommissionEvaluationEndDate = editionDto.Edition.MusicCommissionEvaluationEndDate.ToBrazilTimeZone();
            this.MusicCommissionMaximumApprovedBandsCount = editionDto.Edition.MusicCommissionMaximumApprovedBandsCount;
            this.MusicCommissionMinimumEvaluationsCount = editionDto.Edition.MusicCommissionMinimumEvaluationsCount;

            // Innovation - Commissions
            this.InnovationProjectSubmitStartDate = editionDto.Edition.InnovationProjectSubmitStartDate.ToBrazilTimeZone();
            this.InnovationProjectSubmitEndDate = editionDto.Edition.InnovationProjectSubmitEndDate.ToBrazilTimeZone();
            this.InnovationCommissionEvaluationStartDate = editionDto.Edition.InnovationCommissionEvaluationStartDate.ToBrazilTimeZone();
            this.InnovationCommissionEvaluationEndDate = editionDto.Edition.InnovationCommissionEvaluationEndDate.ToBrazilTimeZone();
            this.InnovationCommissionMaximumApprovedCompaniesCount = editionDto.Edition.InnovationCommissionMaximumApprovedCompaniesCount;
            this.InnovationCommissionMinimumEvaluationsCount = editionDto.Edition.InnovationCommissionMinimumEvaluationsCount;

            // Audiovisual - Commissions
            this.AudiovisualNegotiationsCreateStartDate = editionDto.Edition.AudiovisualNegotiationsCreateStartDate.ToBrazilTimeZone();
            this.AudiovisualNegotiationsCreateEndDate = editionDto.Edition.AudiovisualNegotiationsCreateEndDate.ToBrazilTimeZone();
            this.AudiovisualCommissionEvaluationStartDate = editionDto.Edition.AudiovisualCommissionEvaluationStartDate.ToBrazilTimeZone();
            this.AudiovisualCommissionEvaluationEndDate = editionDto.Edition.AudiovisualCommissionEvaluationEndDate.ToBrazilTimeZone();
            this.AudiovisualCommissionMaximumApprovedProjectsCount = editionDto.Edition.AudiovisualCommissionMaximumApprovedProjectsCount;
            this.AudiovisualCommissionMinimumEvaluationsCount = editionDto.Edition.AudiovisualCommissionMinimumEvaluationsCount;
        }
    }
}