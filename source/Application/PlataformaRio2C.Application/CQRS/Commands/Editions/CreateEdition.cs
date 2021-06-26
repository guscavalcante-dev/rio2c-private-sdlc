// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-03-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-26-2021
// ***********************************************************************
// <copyright file="CreateEdition.cs" company="Softo">
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
    /// <summary>CreateEdition</summary>
    public class CreateEdition : BaseCommand
    {
        [Display(Name = nameof(Name), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string Name { get; set; }

        [Display(Name = nameof(UrlCode), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int UrlCode { get; set; }

        [Display(Name = nameof(IsCurrent), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool IsCurrent { get; set; }

        [Display(Name = nameof(IsActive), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool IsActive { get; set; }

        [Display(Name = nameof(AttendeeOrganizationMaxSellProjectsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int AttendeeOrganizationMaxSellProjectsCount { get; set; }

        [Display(Name = nameof(ProjectMaxBuyerEvaluationsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int ProjectMaxBuyerEvaluationsCount { get; set; }
                        
        [Display(Name = nameof(MusicProjectMaximumApprovedBandsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int MusicProjectMaximumApprovedBandsCount { get; set; }

        [Display(Name = nameof(MusicProjectMinimumEvaluationsCount), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int MusicProjectMinimumEvaluationsCount { get; set; }

        [Display(Name = nameof(StartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? StartDate { get; set; }

        [Display(Name = nameof(EndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [GreaterThanOrEqualTo(nameof(StartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? EndDate { get; set; }

        [Display(Name = nameof(OneToOneMeetingsScheduleDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? OneToOneMeetingsScheduleDate { get; set; }

        #region Dates Properties

        [Display(Name = nameof(SellStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? SellStartDate { get; set; }

        [Display(Name = nameof(SellEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        ////[GreaterThanOrEqualTo(nameof(SellStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? SellEndDate { get;  set; }

        [Display(Name = nameof(ProjectSubmitStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? ProjectSubmitStartDate { get; set; }

        [Display(Name = nameof(ProjectSubmitEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        //[GreaterThanOrEqualTo(nameof(ProjectSubmitStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? ProjectSubmitEndDate { get; set; }

        [Display(Name = nameof(ProjectEvaluationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? ProjectEvaluationStartDate { get; set; }

        [Display(Name = nameof(ProjectEvaluationEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        //[GreaterThanOrEqualTo(nameof(ProjectEvaluationStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? ProjectEvaluationEndDate { get; set; }

        [Display(Name = nameof(NegotiationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? NegotiationStartDate { get; set; }

        [Display(Name = nameof(NegotiationEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        //[GreaterThanOrEqualTo(nameof(NegotiationStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? NegotiationEndDate { get;  set; }

        [Display(Name = nameof(MusicProjectSubmitStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? MusicProjectSubmitStartDate { get;  set; }

        [Display(Name = nameof(MusicProjectSubmitEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        //[GreaterThanOrEqualTo(nameof(MusicProjectSubmitStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? MusicProjectSubmitEndDate { get;  set; }

        [Display(Name = nameof(MusicProjectEvaluationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? MusicProjectEvaluationStartDate { get;  set; }

        [Display(Name = nameof(MusicProjectEvaluationEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        //[GreaterThanOrEqualTo(nameof(MusicProjectEvaluationStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? MusicProjectEvaluationEndDate { get;  set; }

        [Display(Name = nameof(InnovationProjectSubmitStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? InnovationProjectSubmitStartDate { get;  set; }

        [Display(Name = nameof(InnovationProjectSubmitEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        //[GreaterThanOrEqualTo(nameof(InnovationProjectSubmitStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? InnovationProjectSubmitEndDate { get;  set; }

        [Display(Name = nameof(InnovationProjectEvaluationStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? InnovationProjectEvaluationStartDate { get;  set; }

        [Display(Name = nameof(InnovationProjectEvaluationEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        //[GreaterThanOrEqualTo(nameof(InnovationProjectEvaluationStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? InnovationProjectEvaluationEndDate { get;  set; }

        [Display(Name = nameof(AudiovisualNegotiationsCreateStartDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? AudiovisualNegotiationsCreateStartDate { get;  set; }

        [Display(Name = nameof(AudiovisualNegotiationsCreateEndDate), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        //[GreaterThanOrEqualTo(nameof(AudiovisualNegotiationsCreateStartDate), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyGreaterThanProperty")]
        public DateTime? AudiovisualNegotiationsCreateEndDate { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEdition"/> class.
        /// </summary>
        /// <param name="editionDto">The edition dto.</param>
        public CreateEdition(EditionDto editionDto)
        {
            if (editionDto?.Edition == null)
                return;

            this.Name = editionDto.Edition.Name;
            this.UrlCode = editionDto.Edition.UrlCode;
            this.IsCurrent = editionDto.Edition.IsCurrent;
            this.IsActive = editionDto.Edition.IsActive;
            this.AttendeeOrganizationMaxSellProjectsCount = editionDto.Edition.AttendeeOrganizationMaxSellProjectsCount;
            this.ProjectMaxBuyerEvaluationsCount = editionDto.Edition.ProjectMaxBuyerEvaluationsCount;
            this.StartDate = editionDto.Edition.StartDate.ToBrazilTimeZone();
            this.EndDate = editionDto.Edition.EndDate.ToBrazilTimeZone();
            this.SellStartDate = editionDto.Edition.SellStartDate.ToBrazilTimeZone();
            this.SellEndDate = editionDto.Edition.SellEndDate.ToBrazilTimeZone();
            this.ProjectSubmitStartDate = editionDto.Edition.ProjectSubmitStartDate.ToBrazilTimeZone();
            this.ProjectSubmitEndDate = editionDto.Edition.ProjectSubmitEndDate.ToBrazilTimeZone();
            this.ProjectEvaluationStartDate = editionDto.Edition.ProjectEvaluationStartDate.ToBrazilTimeZone();
            this.ProjectEvaluationEndDate = editionDto.Edition.ProjectEvaluationEndDate.ToBrazilTimeZone();
            this.OneToOneMeetingsScheduleDate = editionDto.Edition.OneToOneMeetingsScheduleDate.ToBrazilTimeZone();
            this.NegotiationStartDate = editionDto.Edition.NegotiationStartDate.ToBrazilTimeZone();
            this.NegotiationEndDate = editionDto.Edition.NegotiationEndDate.ToBrazilTimeZone();
            this.MusicProjectSubmitStartDate = editionDto.Edition.MusicProjectSubmitStartDate.ToBrazilTimeZone();
            this.MusicProjectSubmitEndDate = editionDto.Edition.MusicProjectSubmitEndDate.ToBrazilTimeZone();
            this.MusicProjectEvaluationStartDate = editionDto.Edition.MusicProjectEvaluationStartDate.ToBrazilTimeZone();
            this.MusicProjectEvaluationEndDate = editionDto.Edition.MusicProjectEvaluationEndDate.ToBrazilTimeZone();
            this.InnovationProjectSubmitStartDate = editionDto.Edition.InnovationProjectSubmitStartDate.ToBrazilTimeZone();
            this.InnovationProjectSubmitEndDate = editionDto.Edition.InnovationProjectSubmitEndDate.ToBrazilTimeZone();
            this.InnovationProjectEvaluationStartDate = editionDto.Edition.InnovationProjectEvaluationStartDate.ToBrazilTimeZone();
            this.InnovationProjectEvaluationEndDate = editionDto.Edition.InnovationProjectEvaluationEndDate.ToBrazilTimeZone();
            this.AudiovisualNegotiationsCreateStartDate = editionDto.Edition.AudiovisualNegotiationsCreateStartDate.ToBrazilTimeZone();
            this.AudiovisualNegotiationsCreateEndDate = editionDto.Edition.AudiovisualNegotiationsCreateEndDate.ToBrazilTimeZone();
        }

        /// <summary>Initializes a new instance of the <see cref="CreateEdition"/> class.</summary>
        public CreateEdition()
        {
        }
    }
}