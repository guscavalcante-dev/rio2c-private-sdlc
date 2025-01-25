// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-18-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-20-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundProjects.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    public class MusicBusinessRoundProject : Entity
    {
        public static readonly int PlayerCategoriesThatHaveOrHadContractMaxLength = 300;
        public static readonly int AttachmentUrlMaxLength = 300;

        public int SellerAttendeeCollaboratorId { get; private set; }
        public string PlayerCategoriesThatHaveOrHadContract { get; private set; }
        public string AttachmentUrl { get; private set; }
        public DateTimeOffset? FinishDate { get; private set; }
        public int ProjectBuyerEvaluationsCount { get; private set; }
        public virtual AttendeeCollaborator SellerAttendeeCollaborator { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectTargetAudience> MusicBusinessRoundProjectTargetAudience { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectInterest> MusicBusinessRoundProjectInterests { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectPlayerCategory> PlayerCategories { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectActivity> MusicBusinessRoundProjectActivities { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectExpectationsForMeeting> MusicBusinessRoundProjectExpectationsForMeetings { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectBuyerEvaluation> MusicBusinessRoundProjectBuyerEvaluations { get; private set; }

        public MusicBusinessRoundProject()
        {
            
        }
        public MusicBusinessRoundProject(
                int sellerAttendeeCollaboratorId,
                string playerCategoriesThatHaveOrHadContract,
                string attachmentUrl,
                DateTimeOffset? finishDate,
                int projectBuyerEvaluationsCount,
                AttendeeCollaborator sellerAttendeeCollaborator,
                ICollection<MusicBusinessRoundProjectTargetAudience> musicBusinessRoundProjectTargetAudience,
                ICollection<MusicBusinessRoundProjectInterest> musicBusinessRoundProjectInterests,
                ICollection<MusicBusinessRoundProjectPlayerCategory> playerCategories,
                ICollection<MusicBusinessRoundProjectActivity> musicBusinessRoundProjectActivities,
                ICollection<MusicBusinessRoundProjectExpectationsForMeeting> musicBusinessRoundProjectExpectationsForMeetings,
                ICollection<MusicBusinessRoundProjectBuyerEvaluation> musicBusinessRoundProjectBuyerEvaluations
)
        {
            SellerAttendeeCollaboratorId = sellerAttendeeCollaboratorId;
            PlayerCategoriesThatHaveOrHadContract = playerCategoriesThatHaveOrHadContract;
            AttachmentUrl = attachmentUrl;
            FinishDate = finishDate;
            ProjectBuyerEvaluationsCount = projectBuyerEvaluationsCount;
            SellerAttendeeCollaborator = sellerAttendeeCollaborator;
            MusicBusinessRoundProjectTargetAudience = musicBusinessRoundProjectTargetAudience ?? new List<MusicBusinessRoundProjectTargetAudience>();
            MusicBusinessRoundProjectInterests = musicBusinessRoundProjectInterests ?? new List<MusicBusinessRoundProjectInterest>();
            PlayerCategories = playerCategories ?? new List<MusicBusinessRoundProjectPlayerCategory>();
            MusicBusinessRoundProjectActivities = musicBusinessRoundProjectActivities ?? new List<MusicBusinessRoundProjectActivity>();
            MusicBusinessRoundProjectExpectationsForMeetings = musicBusinessRoundProjectExpectationsForMeetings ?? new List<MusicBusinessRoundProjectExpectationsForMeeting>();
            MusicBusinessRoundProjectBuyerEvaluations = musicBusinessRoundProjectBuyerEvaluations ?? new List<MusicBusinessRoundProjectBuyerEvaluation>();
        }

        public MusicBusinessRoundProject(
               int sellerAttendeeCollaboratorId,
               string playerCategoriesThatHaveOrHadContract,
               string attachmentUrl,
               DateTimeOffset? finishDate,
               ICollection<MusicBusinessRoundProjectTargetAudience> musicBusinessRoundProjectTargetAudience,
               ICollection<MusicBusinessRoundProjectInterest> musicBusinessRoundProjectInterests,
               ICollection<MusicBusinessRoundProjectPlayerCategory> playerCategories,
               ICollection<MusicBusinessRoundProjectActivity> musicBusinessRoundProjectActivities,
               ICollection<MusicBusinessRoundProjectExpectationsForMeeting> musicBusinessRoundProjectExpectationsForMeetings
)
        {
            SellerAttendeeCollaboratorId = sellerAttendeeCollaboratorId;
            PlayerCategoriesThatHaveOrHadContract = playerCategoriesThatHaveOrHadContract;
            AttachmentUrl = attachmentUrl;
            FinishDate = finishDate;
            MusicBusinessRoundProjectTargetAudience = musicBusinessRoundProjectTargetAudience ?? new List<MusicBusinessRoundProjectTargetAudience>();
            MusicBusinessRoundProjectInterests = musicBusinessRoundProjectInterests ?? new List<MusicBusinessRoundProjectInterest>();
            PlayerCategories = playerCategories ?? new List<MusicBusinessRoundProjectPlayerCategory>();
            MusicBusinessRoundProjectActivities = musicBusinessRoundProjectActivities ?? new List<MusicBusinessRoundProjectActivity>();
            MusicBusinessRoundProjectExpectationsForMeetings = musicBusinessRoundProjectExpectationsForMeetings ?? new List<MusicBusinessRoundProjectExpectationsForMeeting>();
        }


        public bool IsFinished()
        {
            return this.FinishDate.HasValue;
        }

        #region Validations

      
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidatePlayerCategoriesThatHaveOrHadContract();
            this.ValidateAttachmentUrl();

            return this.ValidationResult.IsValid;
        }

        private void ValidatePlayerCategoriesThatHaveOrHadContract()
        {
            if (this.AttachmentUrl?.Trim().Length > AttachmentUrlMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Attachments, AttachmentUrlMaxLength, 1), new string[] { "AttachmentUrl" }));
            }
        }
        private void ValidateAttachmentUrl()
        {
            if (this.PlayerCategoriesThatHaveOrHadContract?.Trim().Length > PlayerCategoriesThatHaveOrHadContractMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.PlayerCategoriesThatHaveOrHadContract, PlayerCategoriesThatHaveOrHadContractMaxLength, 1), new string[] { "PlayerCategoriesThatHaveOrHadContract" }));
            }
        }

        #endregion
    }
}