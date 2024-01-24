// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-22-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTicketsInformationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorTicketsInformationDto</summary>
    public class AttendeeCollaboratorTicketsInformationDto
    {
        public Edition Edition { get; set; }
        public CollaboratorDto CollaboratorDto { get; set; }
        public int AttendeeCollaboratorTicketsCount { get; set; }
        public IEnumerable<AttendeeMusicBandDto> AttendeeMusicBandDtos { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationDto> AttendeeInnovationOrganizationDtos { get; set; }

        #region Helpers

        #region Music

        #region Pitching

        /// <summary>
        /// Gets the music pitching projects count.
        /// </summary>
        /// <returns></returns>
        private int GetMusicPitchingProjectsCount()
        {
            return this.AttendeeMusicBandDtos.Count(dto => dto.WouldYouLikeParticipatePitching);
        }

        /// <summary>
        /// Gets the music pitching maximum sell projects count.
        /// </summary>
        /// <returns></returns>
        private int GetMusicPitchingMaxSellProjectsCount()
        {
            return this.CollaboratorDto.Document?.IsCnpj() == true ? this.Edition.MusicPitchingEntityMaxSellProjectsCount :
                                                                     this.Edition.MusicPitchingIndividualMaxSellProjectsCount;
        }

        /// <summary>
        /// Determines whether [has music pitching projects subscriptions available].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has music pitching projects subscriptions available]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasMusicPitchingProjectsSubscriptionsAvailable()
        {
            return this.HasTicket() && this.GetMusicPitchingProjectsCount() < this.GetMusicPitchingMaxSellProjectsCount();
        }

        /// <summary>
        /// Gets the music pitching projects subscriptions available.
        /// </summary>
        /// <returns></returns>
        public int GetMusicPitchingProjectsSubscriptionsAvailable()
        {
            if (this.HasTicket())
            {
                return this.GetMusicPitchingMaxSellProjectsCount() - this.GetMusicPitchingProjectsCount();
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region Business Rounds

        /// <summary>
        /// Gets the music business rounds projects count.
        /// </summary>
        /// <returns></returns>
        private int GetMusicBusinessRoundsProjectsCount()
        {
            return this.AttendeeMusicBandDtos.Count(dto => dto.WouldYouLikeParticipateBusinessRound);
        }

        /// <summary>
        /// Determines whether [has music business rounds projects subscriptions available].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has music business rounds projects subscriptions available]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasMusicBusinessRoundsProjectsSubscriptionsAvailable()
        {
            return this.HasTicket() && this.GetMusicBusinessRoundsProjectsCount() < this.Edition.MusicBusinessRoundsMaxSellProjectsCount;
        }

        /// <summary>
        /// Gets the music business rounds projects subscriptions available.
        /// </summary>
        /// <returns></returns>
        public int GetMusicBusinessRoundsProjectsSubscriptionsAvailable()
        {
            if (this.HasTicket())
            {
                return (this.AttendeeCollaboratorTicketsCount * this.Edition.MusicBusinessRoundsMaxSellProjectsCount) - this.GetMusicBusinessRoundsProjectsCount();
            }
            else
            {
                return 0;
            }
                
        }

        #endregion

        /// <summary>
        /// Gets the music messages.
        /// </summary>
        /// <returns></returns>
        public string[] GetMusicMessages()
        {
            if (!this.HasTicket())
            {
                return null;
            }

            List<string> messages = new List<string>();

            if (this.HasMusicPitchingProjectsSubscriptionsAvailable())
            {
                messages.Add(string.Format(Messages.ThereAreStillXSlotsLeftToRegisterProjects, this.GetMusicPitchingProjectsSubscriptionsAvailable(), Labels.MusicProjects, Labels.Pitching));
            }
            else
            {
                messages.Add(string.Format(Messages.ProjectRegistrationLimitReachedFor, Labels.MusicProjects, Labels.Pitching));
            }

            if (this.HasMusicBusinessRoundsProjectsSubscriptionsAvailable())
            {
                messages.Add(string.Format(Messages.ThereAreStillXSlotsLeftToRegisterProjects, this.GetMusicBusinessRoundsProjectsSubscriptionsAvailable(), Labels.MusicProjects, Labels.BusinessRound));
            }
            else
            {
                messages.Add(string.Format(Messages.ProjectRegistrationLimitReachedFor, Labels.MusicProjects, Labels.BusinessRound));
            }

            return messages.ToArray();
        }

        #endregion

        #region Innovation

        #region Pitching

        /// <summary>
        /// Gets the innovation pitching projects count.
        /// </summary>
        /// <returns></returns>
        private int GetInnovationPitchingProjectsCount()
        {
            return this.AttendeeInnovationOrganizationDtos.Count(dto => dto.WouldYouLikeParticipatePitching);
        }

        /// <summary>
        /// Determines whether [has innovation pitching projects subscriptions available].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has innovation pitching projects subscriptions available]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasInnovationPitchingProjectsSubscriptionsAvailable()
        {
            return this.HasTicket() && this.GetInnovationPitchingProjectsCount() < this.Edition.InnovationPitchingMaxSellProjectsCount;
        }

        /// <summary>
        /// Gets the innovation pitching projects subscriptions available.
        /// </summary>
        /// <returns></returns>
        public int GetInnovationPitchingProjectsSubscriptionsAvailable()
        {
            if (this.HasTicket())
            {
                return this.Edition.InnovationPitchingMaxSellProjectsCount - this.GetInnovationPitchingProjectsCount();
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region Business Rounds

        /// <summary>
        /// Gets the innovation business rounds projects count.
        /// </summary>
        /// <returns></returns>
        private int GetInnovationBusinessRoundsProjectsCount()
        {
            return this.AttendeeInnovationOrganizationDtos.Count(dto => dto.WouldYouLikeParticipateBusinessRound);
        }

        /// <summary>
        /// Determines whether [has innovation business rounds projects subscriptions available].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has innovation business rounds projects subscriptions available]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasInnovationBusinessRoundsProjectsSubscriptionsAvailable()
        {
            return this.HasTicket() && this.GetInnovationBusinessRoundsProjectsCount() < this.Edition.InnovationBusinessRoundsMaxSellProjectsCount;
        }

        /// <summary>
        /// Gets the innovation business rounds projects subscriptions available.
        /// </summary>
        /// <returns></returns>
        public int GetInnovationBusinessRoundsProjectsSubscriptionsAvailable()
        {
            if (this.HasTicket())
            {
                return (this.AttendeeCollaboratorTicketsCount * this.Edition.InnovationBusinessRoundsMaxSellProjectsCount) - this.GetInnovationBusinessRoundsProjectsCount();
            }
            else
            {
                return 0;
            }
        }

        #endregion

        /// <summary>
        /// Gets the innovation messages.
        /// </summary>
        /// <returns></returns>
        public string[] GetInnovationMessages()
        {
            if (!this.HasTicket())
            {
                return null;
            }

            List<string> messages = new List<string>();

            if (this.HasInnovationPitchingProjectsSubscriptionsAvailable())
            {
                messages.Add(string.Format(Messages.ThereAreStillXSlotsLeftToRegisterProjects, this.GetInnovationPitchingProjectsSubscriptionsAvailable(), Labels.Startups, Labels.Pitching));
            }
            else
            {
                messages.Add(string.Format(Messages.ProjectRegistrationLimitReachedFor, Labels.Startups, Labels.Pitching));
            }

            if (this.HasInnovationBusinessRoundsProjectsSubscriptionsAvailable())
            {
                messages.Add(string.Format(Messages.ThereAreStillXSlotsLeftToRegisterProjects, this.GetInnovationBusinessRoundsProjectsSubscriptionsAvailable(), Labels.Startups, Labels.BusinessRound));
            }
            else
            {
                messages.Add(string.Format(Messages.ProjectRegistrationLimitReachedFor, Labels.Startups, Labels.BusinessRound));
            }

            return messages.ToArray();
        }

        #endregion

        /// <summary>
        /// Determines whether [has attendee collaborator ticket].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has attendee collaborator ticket]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasTicket()
        {
            return this.AttendeeCollaboratorTicketsCount > 0;
        }

        /// <summary>
        /// Gets all messages.
        /// </summary>
        /// <returns></returns>
        public string[] GetAllMessages()
        {
            return this.GetMusicMessages()
                        .Concat(this.GetInnovationMessages())
                        .Distinct()
                        .ToArray();
        }

        #endregion

        /// <summary>Initializes a new instance of the <see cref="UserEmailSettingsDto"/> class.</summary>
        public AttendeeCollaboratorTicketsInformationDto()
        {
        }
    }
}