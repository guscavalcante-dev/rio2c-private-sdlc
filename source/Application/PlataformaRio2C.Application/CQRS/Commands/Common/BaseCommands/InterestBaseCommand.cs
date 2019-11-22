// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-21-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="InterestBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>InterestBaseCommand</summary>
    public class InterestBaseCommand : BaseCommand
    {
        public Guid InterestGroupUid { get; set; }
        public string InterestGroupName { get; set; }
        public int InterestGroupDisplayOrder { get; set; }

        public Guid InterestUid { get; set; }
        public string InterestName { get; set; }
        public int InterestDisplayOrder { get; set; }
        public bool InterestHasAdditionalInfo { get; set; }

        public bool IsChecked { get; set; }

        [Display(Name = "AdditionalInformation", ResourceType = typeof(Labels))]
        [RequiredIf("IsChecked", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string AdditionalInfo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="InterestBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public InterestBaseCommand(OrganizationInterestDto entity)
        {
            this.InterestGroupUid = entity.InterestGroup.Uid;
            this.InterestGroupName = entity.InterestGroup.Name;
            this.InterestGroupDisplayOrder = entity.InterestGroup.DisplayOrder;

            this.InterestUid = entity.Interest.Uid;
            this.InterestName = entity.Interest.Name;
            this.InterestDisplayOrder = entity.Interest.DisplayOrder;
            this.InterestHasAdditionalInfo = entity.Interest.HasAdditionalInfo;

            this.AdditionalInfo = entity.ProjectInterest.AdditionalInfo;
            this.IsChecked = true;
        }

        /// <summary>Initializes a new instance of the <see cref="InterestBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public InterestBaseCommand(ProjectInterestDto entity)
        {
            this.InterestGroupUid = entity.InterestGroup.Uid;
            this.InterestGroupName = entity.InterestGroup.Name;
            this.InterestGroupDisplayOrder = entity.InterestGroup.DisplayOrder;

            this.InterestUid = entity.Interest.Uid;
            this.InterestName = entity.Interest.Name;
            this.InterestDisplayOrder = entity.Interest.DisplayOrder;
            this.InterestHasAdditionalInfo = entity.Interest.HasAdditionalInfo;

            this.AdditionalInfo = entity.ProjectInterest.AdditionalInfo;
            this.IsChecked = true;
        }

        /// <summary>Initializes a new instance of the <see cref="InterestBaseCommand"/> class.</summary>
        /// <param name="interestDto">The interest dto.</param>
        public InterestBaseCommand(InterestDto interestDto)
        {
            this.InterestGroupUid = interestDto.InterestGroup.Uid;
            this.InterestGroupName = interestDto.InterestGroup.Name;
            this.InterestGroupDisplayOrder = interestDto.InterestGroup.DisplayOrder;

            this.InterestUid = interestDto.Interest.Uid;
            this.InterestName = interestDto.Interest.Name;
            this.InterestDisplayOrder = interestDto.Interest.DisplayOrder;
            this.InterestHasAdditionalInfo = interestDto.Interest.HasAdditionalInfo;

            this.IsChecked = false;
        }

        /// <summary>Initializes a new instance of the <see cref="InterestBaseCommand"/> class.</summary>
        public InterestBaseCommand()
        {
        }
    }
}