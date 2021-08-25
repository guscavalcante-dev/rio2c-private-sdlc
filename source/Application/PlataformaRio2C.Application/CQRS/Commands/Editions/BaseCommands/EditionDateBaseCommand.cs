// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-20-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-24-2021
// ***********************************************************************
// <copyright file="EditionDateBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>EditionDateBaseCommand</summary>
    public class EditionDateBaseCommand : BaseCommand
    {
        public new Guid EditionUid { get; set; }

        #region Music - Commissions

        [Display(Name = nameof(MusicProjectSubmitStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? MusicProjectSubmitStartDate { get; set; }

        [Display(Name = nameof(MusicProjectSubmitEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? MusicProjectSubmitEndDate { get; set; }

        [Display(Name = nameof(MusicCommissionEvaluationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? MusicCommissionEvaluationStartDate { get; set; }

        [Display(Name = nameof(MusicCommissionEvaluationEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? MusicCommissionEvaluationEndDate { get; set; }

        [Display(Name = nameof(MusicCommissionMaximumApprovedBandsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? MusicCommissionMaximumApprovedBandsCount { get; set; }

        [Display(Name = nameof(MusicCommissionMinimumEvaluationsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? MusicCommissionMinimumEvaluationsCount { get; set; }

        #endregion

        #region Innovation - Commissions

        [Display(Name = nameof(InnovationProjectSubmitStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? InnovationProjectSubmitStartDate { get; set; }

        [Display(Name = nameof(InnovationProjectSubmitEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? InnovationProjectSubmitEndDate { get; set; }

        [Display(Name = nameof(InnovationCommissionEvaluationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? InnovationCommissionEvaluationStartDate { get; set; }

        [Display(Name = nameof(InnovationCommissionEvaluationEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? InnovationCommissionEvaluationEndDate { get; set; }

        [Display(Name = nameof(InnovationCommissionMaximumApprovedCompaniesCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? InnovationCommissionMaximumApprovedCompaniesCount { get; set; }

        [Display(Name = nameof(InnovationCommissionMinimumEvaluationsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? InnovationCommissionMinimumEvaluationsCount { get; set; }

        #endregion

        #region Audiovisual - Commissions

        [Display(Name = nameof(AudiovisualCommissionEvaluationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? AudiovisualCommissionEvaluationStartDate { get; set; }

        [Display(Name = nameof(AudiovisualCommissionEvaluationEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? AudiovisualCommissionEvaluationEndDate { get; set; }

        [Display(Name = nameof(AudiovisualCommissionMaximumApprovedProjectsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? AudiovisualCommissionMaximumApprovedProjectsCount { get; set; }

        [Display(Name = nameof(AudiovisualCommissionMinimumEvaluationsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? AudiovisualCommissionMinimumEvaluationsCount { get; set; }

        #endregion

        #region Audiovisual - Negotiations

        [Display(Name = nameof(ProjectSubmitStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? ProjectSubmitStartDate { get; set; }

        [Display(Name = nameof(ProjectSubmitEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? ProjectSubmitEndDate { get; set; }

        [Display(Name = nameof(ProjectEvaluationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? ProjectEvaluationStartDate { get; set; }

        [Display(Name = nameof(ProjectEvaluationEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? ProjectEvaluationEndDate { get; set; }

        [Display(Name = nameof(NegotiationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? NegotiationStartDate { get; set; }

        [Display(Name = nameof(NegotiationEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? NegotiationEndDate { get; set; }

        [Display(Name = nameof(AudiovisualNegotiationsCreateStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? AudiovisualNegotiationsCreateStartDate { get; set; }

        [Display(Name = nameof(AudiovisualNegotiationsCreateEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? AudiovisualNegotiationsCreateEndDate { get; set; }

        [Display(Name = nameof(AttendeeOrganizationMaxSellProjectsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? AttendeeOrganizationMaxSellProjectsCount { get; set; }

        [Display(Name = nameof(ProjectMaxBuyerEvaluationsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? ProjectMaxBuyerEvaluationsCount { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="EditionDateBaseCommand"/> class.
        /// </summary>
        /// <param name="editionDto">The edition dto.</param>
        public EditionDateBaseCommand(EditionDto editionDto)
        {
            this.UpdateDatesInformation(editionDto);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditionDateBaseCommand"/> class.
        /// </summary>
        public EditionDateBaseCommand()
        {
        }

        /// <summary>
        /// Updates the dates information.
        /// </summary>
        /// <param name="editionDto">The edition dto.</param>
        private void UpdateDatesInformation(EditionDto editionDto)
        {
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