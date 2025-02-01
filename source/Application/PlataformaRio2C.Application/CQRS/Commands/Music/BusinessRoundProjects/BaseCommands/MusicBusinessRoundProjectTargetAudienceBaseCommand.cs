// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Gilson Oliveira
// Created          : 01-31-2025
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 01-31-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundProjectTargetAudienceBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class MusicBusinessRoundProjectTargetAudienceBaseCommand : BaseCommand
    {
        public static readonly int AdditionalInfoMaxLength = 200;
        public int MusicBusinessRoundProjectId { get; set; }
        public int TargetAudienceId { get; set; }
        public Guid TargetAudienceUid { get; set; }
        public string TargetAudienceName { get; set; }
        public int TargetAudienceDisplayOrder { get; set; }
        public bool TargetAudienceHasAdditionalInfo { get; set; }
        public bool IsChecked { get; set; }

        [Display(Name = "AdditionalInformation", ResourceType = typeof(Labels))]
        [StringLength(200, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectTargetAudienceBaseCommand"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public MusicBusinessRoundProjectTargetAudienceBaseCommand(MusicBusinessRoundProjectTargetAudience entity)
        {
            this.MusicBusinessRoundProjectId = entity.MusicBusinessRoundProjectId;
            this.TargetAudienceId = entity.TargetAudienceId;
            this.TargetAudienceUid = entity.TargetAudience.Uid;
            this.TargetAudienceName = entity.TargetAudience.Name;
            this.TargetAudienceDisplayOrder = entity.TargetAudience.DisplayOrder;
            this.TargetAudienceHasAdditionalInfo = entity.TargetAudience.HasAdditionalInfo;

            //TODO: Create AttendeeCollaboratorInterest.AdditionalInfo field at database and put here.
            this.IsChecked = true;
            this.AdditionalInfo = entity.AdditionalInfo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectTargetAudienceBaseCommand"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public MusicBusinessRoundProjectTargetAudienceBaseCommand(TargetAudience entity)
        {            
            this.TargetAudienceId = entity.Id;
            this.TargetAudienceUid = entity.Uid;
            this.TargetAudienceName = entity.Name;
            this.TargetAudienceDisplayOrder = entity.DisplayOrder;
            this.TargetAudienceHasAdditionalInfo = entity.HasAdditionalInfo;
            this.IsChecked = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectTargetAudienceBaseCommand"/> class.
        /// </summary>
        public MusicBusinessRoundProjectTargetAudienceBaseCommand()
        {
        }
    }

}
