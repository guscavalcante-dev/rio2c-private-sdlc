// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-20-2021
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 11-18-2024
// ***********************************************************************
// <copyright file="EditionDateBaseCommand.cs" company="Softo">
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
        [GreaterThanOrEqualTo(nameof(MusicProjectSubmitStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? MusicProjectSubmitEndDate { get; set; }

        [Display(Name = nameof(MusicCommissionEvaluationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? MusicCommissionEvaluationStartDate { get; set; }

        [Display(Name = nameof(MusicCommissionEvaluationEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [GreaterThanOrEqualTo(nameof(MusicCommissionEvaluationStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? MusicCommissionEvaluationEndDate { get; set; }

        [Display(Name = nameof(MusicCommissionMaximumApprovedBandsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? MusicCommissionMaximumApprovedBandsCount { get; set; }

        [Display(Name = nameof(MusicCommissionMinimumEvaluationsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? MusicCommissionMinimumEvaluationsCount { get; set; }

        [Display(Name = nameof(MusicPitchingMaximumProjectSubmissionsByEdition), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int MusicPitchingMaximumProjectSubmissionsByEdition { get; set; }

        [Display(Name = nameof(MusicPitchingMaximumProjectSubmissionsByParticipant), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int MusicPitchingMaximumProjectSubmissionsByParticipant { get; set; }

        [Display(Name = nameof(MusicPitchingMaximumApprovedProjectsByCommissionMember), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int MusicPitchingMaximumApprovedProjectsByCommissionMember { get; set; }

        [Display(Name = nameof(MusicPitchingCuratorEvaluationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? MusicPitchingCuratorEvaluationStartDate { get; set; }

        [Display(Name = nameof(MusicPitchingMaximumApprovedProjectsByCurator), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int MusicPitchingMaximumApprovedProjectsByCurator { get; set; }

        [Display(Name = nameof(MusicPitchingPopularEvaluationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? MusicPitchingPopularEvaluationStartDate { get; set; }

        [Display(Name = nameof(MusicPitchingMaximumApprovedProjectsByPopularVote), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int MusicPitchingMaximumApprovedProjectsByPopularVote { get; set; }

        [Display(Name = nameof(MusicPitchingRepechageEvaluationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? MusicPitchingRepechageEvaluationStartDate { get; set; }

        [Display(Name = nameof(MusicPitchingMaximumApprovedProjectsByRepechage), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int MusicPitchingMaximumApprovedProjectsByRepechage { get; set; }

        #endregion

        #region Innovation - Commissions

        [Display(Name = nameof(InnovationProjectSubmitStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? InnovationProjectSubmitStartDate { get; set; }

        [Display(Name = nameof(InnovationProjectSubmitEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [GreaterThanOrEqualTo(nameof(InnovationProjectSubmitStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? InnovationProjectSubmitEndDate { get; set; }

        [Display(Name = nameof(InnovationCommissionEvaluationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? InnovationCommissionEvaluationStartDate { get; set; }

        [Display(Name = nameof(InnovationCommissionEvaluationEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [GreaterThanOrEqualTo(nameof(InnovationCommissionEvaluationStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
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
        [GreaterThanOrEqualTo(nameof(AudiovisualCommissionEvaluationStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
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
        [GreaterThanOrEqualTo(nameof(ProjectSubmitStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? ProjectSubmitEndDate { get; set; }

        [Display(Name = nameof(ProjectEvaluationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? ProjectEvaluationStartDate { get; set; }

        [Display(Name = nameof(ProjectEvaluationEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [GreaterThanOrEqualTo(nameof(ProjectEvaluationStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? ProjectEvaluationEndDate { get; set; }

        [Display(Name = nameof(NegotiationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? NegotiationStartDate { get; set; }

        [Display(Name = nameof(NegotiationEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [GreaterThanOrEqualTo(nameof(NegotiationStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? NegotiationEndDate { get; set; }
                
        [Display(Name = nameof(AttendeeOrganizationMaxSellProjectsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? AttendeeOrganizationMaxSellProjectsCount { get; set; }

        [Display(Name = nameof(ProjectMaxBuyerEvaluationsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? ProjectMaxBuyerEvaluationsCount { get; set; }

        [Display(Name = nameof(AudiovisualNegotiationsVirtualMeetingsJoinMinutes), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public short? AudiovisualNegotiationsVirtualMeetingsJoinMinutes { get; set; }

        #endregion

        #region Cartoon - Commissions

        [Display(Name = nameof(CartoonProjectSubmitStartDate), ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? CartoonProjectSubmitStartDate { get; set; }

        [Display(Name = nameof(CartoonProjectSubmitEndDate), ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        //[GreaterThanOrEqualTo(nameof(CartoonProjectSubmitStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? CartoonProjectSubmitEndDate { get; set; }

        [Display(Name = nameof(CartoonCommissionEvaluationStartDate), ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? CartoonCommissionEvaluationStartDate { get; set; }

        [Display(Name = nameof(CartoonCommissionEvaluationEndDate), ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        //[GreaterThanOrEqualTo(nameof(CartoonCommissionEvaluationStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? CartoonCommissionEvaluationEndDate { get; set; }

        [Display(Name = nameof(CartoonCommissionMaximumApprovedProjectsCount), ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? CartoonCommissionMaximumApprovedProjectsCount { get; set; }

        [Display(Name = nameof(CartoonCommissionMinimumEvaluationsCount), ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? CartoonCommissionMinimumEvaluationsCount { get; set; }

        #endregion

        #region Creator - Commissions

        [Display(Name = nameof(Labels.ProjectSubmitStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime CreatorProjectSubmitStartDate { get; set; }

        [Display(Name = nameof(Labels.ProjectSubmitEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [GreaterThanOrEqualTo(nameof(CreatorProjectSubmitStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime CreatorProjectSubmitEndDate { get; set; }

        [Display(Name = nameof(Labels.ProjectEvaluationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime CreatorCommissionEvaluationStartDate { get; set; }

        [Display(Name = nameof(Labels.ProjectEvaluationEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [GreaterThanOrEqualTo(nameof(CreatorCommissionEvaluationStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime CreatorCommissionEvaluationEndDate { get; set; }

        [Display(Name = nameof(Labels.CommissionMaximumApprovedProjectsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int CreatorCommissionMaximumApprovedProjectsCount { get; set; }

        [Display(Name = nameof(Labels.CommissionMinimumEvaluationsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int CreatorCommissionMinimumEvaluationsCount { get; set; }

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
            this.AudiovisualNegotiationsVirtualMeetingsJoinMinutes = editionDto.Edition.AudiovisualNegotiationsVirtualMeetingsJoinMinutes;

            // Music - Commissions
            this.MusicProjectSubmitStartDate = editionDto.Edition.MusicProjectSubmitStartDate.ToBrazilTimeZone();
            this.MusicProjectSubmitEndDate = editionDto.Edition.MusicProjectSubmitEndDate.ToBrazilTimeZone();
            this.MusicCommissionEvaluationStartDate = editionDto.Edition.MusicCommissionEvaluationStartDate.ToBrazilTimeZone();
            this.MusicCommissionEvaluationEndDate = editionDto.Edition.MusicCommissionEvaluationEndDate.ToBrazilTimeZone();
            this.MusicCommissionMaximumApprovedBandsCount = editionDto.Edition.MusicCommissionMaximumApprovedBandsCount;
            this.MusicCommissionMinimumEvaluationsCount = editionDto.Edition.MusicCommissionMinimumEvaluationsCount;
            this.MusicPitchingMaximumProjectSubmissionsByEdition = editionDto.Edition.MusicPitchingMaximumProjectSubmissionsByEdition;
            this.MusicPitchingMaximumProjectSubmissionsByParticipant = editionDto.Edition.MusicPitchingMaximumProjectSubmissionsByParticipant;
            this.MusicPitchingMaximumApprovedProjectsByCommissionMember = editionDto.Edition.MusicPitchingMaximumApprovedProjectsByCommissionMember;
            this.MusicPitchingCuratorEvaluationStartDate = editionDto.Edition.MusicPitchingCuratorEvaluationStartDate.ToBrazilTimeZone();
            this.MusicPitchingMaximumApprovedProjectsByCurator = editionDto.Edition.MusicPitchingMaximumApprovedProjectsByCurator;
            this.MusicPitchingPopularEvaluationStartDate = editionDto.Edition.MusicPitchingPopularEvaluationStartDate.ToBrazilTimeZone();
            this.MusicPitchingMaximumApprovedProjectsByPopularVote = editionDto.Edition.MusicPitchingMaximumApprovedProjectsByPopularVote;
            this.MusicPitchingRepechageEvaluationStartDate = editionDto.Edition.MusicPitchingRepechageEvaluationStartDate.ToBrazilTimeZone();
            this.MusicPitchingMaximumApprovedProjectsByRepechage = editionDto.Edition.MusicPitchingMaximumApprovedProjectsByRepechage;

            // Innovation - Commissions
            this.InnovationProjectSubmitStartDate = editionDto.Edition.InnovationProjectSubmitStartDate.ToBrazilTimeZone();
            this.InnovationProjectSubmitEndDate = editionDto.Edition.InnovationProjectSubmitEndDate.ToBrazilTimeZone();
            this.InnovationCommissionEvaluationStartDate = editionDto.Edition.InnovationCommissionEvaluationStartDate.ToBrazilTimeZone();
            this.InnovationCommissionEvaluationEndDate = editionDto.Edition.InnovationCommissionEvaluationEndDate.ToBrazilTimeZone();
            this.InnovationCommissionMaximumApprovedCompaniesCount = editionDto.Edition.InnovationCommissionMaximumApprovedCompaniesCount;
            this.InnovationCommissionMinimumEvaluationsCount = editionDto.Edition.InnovationCommissionMinimumEvaluationsCount;

            // Audiovisual - Commissions
            this.AudiovisualCommissionEvaluationStartDate = editionDto.Edition.AudiovisualCommissionEvaluationStartDate.ToBrazilTimeZone();
            this.AudiovisualCommissionEvaluationEndDate = editionDto.Edition.AudiovisualCommissionEvaluationEndDate.ToBrazilTimeZone();
            this.AudiovisualCommissionMaximumApprovedProjectsCount = editionDto.Edition.AudiovisualCommissionMaximumApprovedProjectsCount;
            this.AudiovisualCommissionMinimumEvaluationsCount = editionDto.Edition.AudiovisualCommissionMinimumEvaluationsCount;

            // Cartoon - Commissions
            this.CartoonProjectSubmitStartDate = editionDto.Edition.CartoonProjectSubmitStartDate.ToBrazilTimeZone();
            this.CartoonProjectSubmitEndDate = editionDto.Edition.CartoonProjectSubmitEndDate.ToBrazilTimeZone();
            this.CartoonCommissionEvaluationStartDate = editionDto.Edition.CartoonCommissionEvaluationStartDate.ToBrazilTimeZone();
            this.CartoonCommissionEvaluationEndDate = editionDto.Edition.CartoonCommissionEvaluationEndDate.ToBrazilTimeZone();
            this.CartoonCommissionMaximumApprovedProjectsCount = editionDto.Edition.CartoonCommissionMaximumApprovedProjectsCount;
            this.CartoonCommissionMinimumEvaluationsCount = editionDto.Edition.CartoonCommissionMinimumEvaluationsCount;

            // Creator - Commissions
            this.CreatorProjectSubmitStartDate = editionDto.Edition.CreatorProjectSubmitStartDate.ToBrazilTimeZone();
            this.CreatorProjectSubmitEndDate = editionDto.Edition.CreatorProjectSubmitEndDate.ToBrazilTimeZone();
            this.CreatorCommissionEvaluationStartDate = editionDto.Edition.CreatorCommissionEvaluationStartDate.ToBrazilTimeZone();
            this.CreatorCommissionEvaluationEndDate = editionDto.Edition.CreatorCommissionEvaluationEndDate.ToBrazilTimeZone();
            this.CreatorCommissionMaximumApprovedProjectsCount = editionDto.Edition.CreatorCommissionMaximumApprovedProjectsCount;
            this.CreatorCommissionMinimumEvaluationsCount = editionDto.Edition.CreatorCommissionMinimumEvaluationsCount;
        }
    }
}