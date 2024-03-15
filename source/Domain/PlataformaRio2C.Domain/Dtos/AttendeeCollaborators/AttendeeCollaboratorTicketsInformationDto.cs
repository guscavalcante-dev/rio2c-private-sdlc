// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-22-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-15-2024
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTicketsInformationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
        public IEnumerable<AttendeeCollaboratorTicketDto> AttendeeCollaboratorTicketDtos { get; set; }
        public int AttendeeCollaboratorTicketsCount => this.AttendeeCollaboratorTicketDtos?.Count() ?? 0;
        public IEnumerable<AttendeeMusicBandDto> AttendeeMusicBandDtos { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationDto> AttendeeInnovationOrganizationDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UserEmailSettingsDto"/> class.</summary>
        public AttendeeCollaboratorTicketsInformationDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaboratorTicketsInformationDto"/> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        public AttendeeCollaboratorTicketsInformationDto(Edition edition)
        {
            this.Edition = edition;
            this.CollaboratorDto = new CollaboratorDto();
            this.AttendeeCollaboratorTicketDtos = new List<AttendeeCollaboratorTicketDto>();
            this.AttendeeMusicBandDtos = new List<AttendeeMusicBandDto>();
            this.AttendeeInnovationOrganizationDtos = new List<AttendeeInnovationOrganizationDto>();
        }

        //TODO: Move all this validations inside AttendeeCollaboratorTicketsInformationDto to a Service and refactor this.
        #region Helpers

        #region Music

        #region Pitching

        /// <summary>
        /// Gets the music pitching projects count.
        /// </summary>
        /// <param name="addingMusicProjectsCount">The adding music projects count.</param>
        /// <returns></returns>
        private int GetMusicPitchingProjectsCount(int addingMusicProjectsCount)
        {
            // When adding new music projects, must consider them in the projects count.
            return this.AttendeeMusicBandDtos.Count(dto => dto.WouldYouLikeParticipatePitching) + addingMusicProjectsCount;
        }

        /// <summary>
        /// Gets the music pitching maximum sell projects count.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        public int GetMusicPitchingMaxSellProjectsCount(string document)
        {
            // TODO: Always returning MusicPitchingEntityMaxSellProjectsCount(3) because we have a problem to identify if a foreigner is an Individual or Entity by the document.
            // For now, validating the number of projects a participant can register is only on the front-end.
            // The customer opened https://softohq.atlassian.net/browse/RIO2CMY-1032 to review this rule later.
            return this.Edition.MusicPitchingEntityMaxSellProjectsCount;

            //if (!string.IsNullOrEmpty(document))
            //{
            //    return document.IsCnpj() ? this.Edition.MusicPitchingEntityMaxSellProjectsCount :
            //                               this.Edition.MusicPitchingIndividualMaxSellProjectsCount;
            //}
            //else
            //{
            //    return this.CollaboratorDto?.Document?.IsCnpj() == true ? this.Edition.MusicPitchingEntityMaxSellProjectsCount :
            //                                                              this.Edition.MusicPitchingIndividualMaxSellProjectsCount;
            //}
        }

        /// <summary>
        /// Determines whether [has music pitching projects subscriptions available].
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="addingMusicProjectsCount">The adding music pitching projects count.</param>
        /// <returns>
        ///   <c>true</c> if [has music pitching projects subscriptions available]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasMusicPitchingProjectsSubscriptionsAvailable(string document, int addingMusicProjectsCount)
        {
            if (addingMusicProjectsCount > 0)
            {
                // When adding new music pitching project, we need to use <= operator
                return this.GetMusicPitchingProjectsCount(addingMusicProjectsCount) <= this.GetMusicPitchingMaxSellProjectsCount(document);
            }
            else
            {
                return this.GetMusicPitchingProjectsCount(addingMusicProjectsCount) < this.GetMusicPitchingMaxSellProjectsCount(document);
            }
        }

        /// <summary>
        /// Gets the music pitching projects subscriptions available.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="addingMusicProjectsCount">The adding music pitching projects count.</param>
        /// <returns></returns>
        public int GetMusicPitchingProjectsSubscriptionsAvailable(string document, int addingMusicProjectsCount)
        {
            if (this.HasTicket())
            {
                return this.GetMusicPitchingMaxSellProjectsCount(document) - this.GetMusicPitchingProjectsCount(addingMusicProjectsCount);
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
        /// <param name="addingMusicProjectsCount">The adding music projects count.</param>
        /// <returns></returns>
        private int GetMusicBusinessRoundsProjectsCount(int addingMusicProjectsCount)
        {
            // When adding new music projects, must consider them in the projects count.
            return this.AttendeeMusicBandDtos.Count(dto => dto.WouldYouLikeParticipateBusinessRound) + addingMusicProjectsCount;
        }

        /// <summary>
        /// Determines whether [has music business rounds projects subscriptions available].
        /// </summary>
        /// <param name="addingMusicProjectsCount">The adding music projects count.</param>
        /// <returns>
        ///   <c>true</c> if [has music business rounds projects subscriptions available]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasMusicBusinessRoundsProjectsSubscriptionsAvailable(int addingMusicProjectsCount)
        {
            if (addingMusicProjectsCount > 0)
            {
                // When adding new music pitching project, we need to use <= operator
                return this.HasTicket() && this.GetMusicBusinessRoundsProjectsCount(addingMusicProjectsCount) <= this.Edition.MusicBusinessRoundsMaxSellProjectsCount;
            }
            else
            {
                return this.HasTicket() && this.GetMusicBusinessRoundsProjectsCount(addingMusicProjectsCount) < this.Edition.MusicBusinessRoundsMaxSellProjectsCount;
            }
        }

        /// <summary>
        /// Gets the music business rounds projects subscriptions available.
        /// </summary>
        /// <param name="addingMusicProjectsCount">The adding music projects count.</param>
        /// <returns></returns>
        public int GetMusicBusinessRoundsProjectsSubscriptionsAvailable(int addingMusicProjectsCount)
        {
            if (this.HasTicket())
            {
                return (this.AttendeeCollaboratorTicketsCount * this.Edition.MusicBusinessRoundsMaxSellProjectsCount) - this.GetMusicBusinessRoundsProjectsCount(addingMusicProjectsCount);
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
        /// <param name="document">The document.</param>
        /// <param name="addingMusicPitchingProjectsCount">The adding music pitching projects count.</param>
        /// <param name="addingMusicBusinessRoundsProjectsCount">The adding music business rounds projects count.</param>
        /// <returns></returns>
        public string[] GetMusicMessages(string document, int addingMusicPitchingProjectsCount, int addingMusicBusinessRoundsProjectsCount)
        {
            if (!this.HasTicket())
            {
                return null;
            }

            List<string> messages = new List<string>();

            if (this.HasMusicPitchingProjectsSubscriptionsAvailable(document, addingMusicPitchingProjectsCount))
            {
                messages.Add(string.Format(Messages.ThereAreStillXSlotsLeftToRegisterProjects, this.GetMusicPitchingProjectsSubscriptionsAvailable(document, addingMusicPitchingProjectsCount), Labels.MusicProjects, Labels.Pitching));
            }
            else
            {
                messages.Add(string.Format(Messages.ProjectRegistrationLimitReachedFor, Labels.MusicProjects, Labels.Pitching));
            }

            if (this.HasMusicBusinessRoundsProjectsSubscriptionsAvailable(addingMusicBusinessRoundsProjectsCount))
            {
                messages.Add(string.Format(Messages.ThereAreStillXSlotsLeftToRegisterProjects, this.GetMusicBusinessRoundsProjectsSubscriptionsAvailable(addingMusicBusinessRoundsProjectsCount), Labels.MusicProjects, Labels.BusinessRound));
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
            return this.GetInnovationPitchingProjectsCount() < this.Edition.InnovationPitchingMaxSellProjectsCount;
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
        /// <param name="document">The document.</param>
        /// <param name="addingMusicPitchingProjectsCount">The adding music pitching projects count.</param>
        /// <param name="addingMusicBusinessRoundsProjectsCount">The adding music business rounds projects count.</param>
        /// <returns></returns>
        public string[] GetAllMessages(string document, int addingMusicPitchingProjectsCount, int addingMusicBusinessRoundsProjectsCount)
        {
            return this.GetMusicMessages(document, addingMusicPitchingProjectsCount, addingMusicBusinessRoundsProjectsCount)
                        .Concat(this.GetInnovationMessages())
                        .Distinct()
                        .ToArray();
        }

        #endregion
    }
}