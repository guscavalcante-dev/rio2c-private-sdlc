// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-04-2019
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-08-2025
// ***********************************************************************
// <copyright file="UserAccessControlDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>UserAccessControlDto</summary>
    public class UserAccessControlDto
    {
        public User User { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public Language Language { get; set; }
        public bool HasUnreadMessages { get; set; }
        public int ProjectEvaluationsPendingCount { get; set; }

        public Collaborator Collaborator { get; set; }
        public AttendeeCollaborator EditionAttendeeCollaborator { get; set; }
        public IEnumerable<AttendeeCollaborator> EditionAttendeeCollaborators { get; set; }
        public IEnumerable<CollaboratorType> EditionCollaboratorTypes { get; set; }
        public IEnumerable<AttendeeOrganization> EditionAttendeeOrganizations { get; set; } //TODO: Remove EditionAttendeeOrganizations from UserAccessControlDto
        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitlesDtos { get; set; }  //TODO: Remove EditionAttendeeOrganizations from UserAccessControlDto
        public IEnumerable<AttendeeCollaboratorTicketDto> EditionAttendeeCollaboratorTickets { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UserAccessControlDto"/> class.</summary>
        public UserAccessControlDto()
        {
        }

        #region Properties manipulation 

        /// <summary>Gets the display name.</summary>
        /// <returns></returns>
        public string GetDisplayName()
        {
            return this.Collaborator?.GetDisplayName() ?? this.User.Name;
        }

        /// <summary>Gets the display name abbreviation.</summary>
        /// <returns></returns>
        public string GetDisplayNameAbbreviation()
        {
            return this.GetDisplayName().GetTwoLetterCode();
        }

        /// <summary>Gets the full name.</summary>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <returns></returns>
        public string GetFullName(string userInterfaceLanguage)
        {
            return this.GetDisplayName()?.UppercaseFirstOfEachWord(userInterfaceLanguage);
        }

        /// <summary>Gets the first name.</summary>
        /// <returns></returns>
        public string GetFirstName()
        {
            return this.GetDisplayName()?.GetFirstWord();
        }

        /// <summary>Gets the name abbreviation code.</summary>
        /// <returns></returns>
        public string GetNameAbbreviationCode()
        {
            return this.GetDisplayName()?.GetTwoLetterCode();
        }

        /// <summary>Determines whether this instance has image.</summary>
        /// <returns>
        ///   <c>true</c> if this instance has image; otherwise, <c>false</c>.</returns>
        public bool HasImage()
        {
            return this.Collaborator?.ImageUploadDate != null;
        }

        /// <summary>Gets the job title.</summary>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <returns></returns>
        public string GetJobTitle(string userInterfaceLanguage)
        {
            var cultureJobTitle = this.JobTitlesDtos?.FirstOrDefault(jb => jb.LanguageDto.Code == userInterfaceLanguage);
            if (cultureJobTitle != null)
            {
                return cultureJobTitle.Value;
            }

            return this.JobTitlesDtos?.FirstOrDefault()?.Value;
        }

        /// <summary>Gets the organizations.</summary>
        /// <returns></returns>
        public List<Organization> GetOrganizations()
        {
            return this.EditionAttendeeOrganizations?.Where(eao => !eao.IsDeleted && !eao.Organization.IsDeleted)?.Select(eao => eao.Organization)?.ToList();
        }

        /// <summary>Gets the first attendee organization created.</summary>
        /// <returns></returns>
        public AttendeeOrganization GetFirstAttendeeOrganizationCreated()
        {
            return this.EditionAttendeeOrganizations?.Where(eao => !eao.IsDeleted)?.OrderBy(eao => eao.Id).FirstOrDefault();
        }

        /// <summary>Gets the attendee organization by edition id.</summary>
        /// <param name="editionId">The edition id.</param>
        /// <returns></returns>
        public AttendeeOrganization GetAttendeeOrganizationByEditionId(int editionId)
        {
            var attendeeOrganizations = this.EditionAttendeeOrganizations
                ?.Where(eao => !eao.IsDeleted && eao.EditionId == editionId)
                ?.ToList();
            return Enumerable.FirstOrDefault(attendeeOrganizations);
        }

        #endregion

        #region Permissions

        /// <summary>Determines whether this instance is admin.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is admin; otherwise, <c>false</c>.</returns>
        public bool IsAdmin()
        {
            return this.Roles?.Any(r => r.Name == Constants.Role.Admin) == true;
        }

        /// <summary>
        /// Determines whether this instance is player.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is player; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPlayerExecutive()
        {
            return this.HasAnyCollaboratorType(Constants.CollaboratorType.PlayerExecutives);
        }

        /// <summary>
        /// Determines whether [is audiovisual player executive].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual player executive]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAudiovisualPlayerExecutive()
        {
            return this.HasCollaboratorType(Constants.CollaboratorType.PlayerExecutiveAudiovisual);
        }

        /// <summary>
        /// Determines whether [is audiovisual or music player executive].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual or music player executive]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAudiovisualOrMusicPlayerExecutive()
        {
            var collaboratorTypes = new string[] { Constants.CollaboratorType.PlayerExecutiveAudiovisual, Constants.CollaboratorType.PlayerExecutiveMusic };
            return this.HasAnyCollaboratorType(collaboratorTypes);
        }

        /// <summary>
        /// Determines whether [is innovation player executive].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is innovation player executive]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInnovationPlayerExecutive()
        {
            return this.HasCollaboratorType(Constants.CollaboratorType.PlayerExecutiveInnovation);
        }

        /// <summary>
        /// Determines whether [is music player executive].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is music player executive]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMusicPlayerExecutive()
        {
            return this.HasCollaboratorType(Constants.CollaboratorType.PlayerExecutiveMusic);
        }

        /// <summary>
        /// Determines whether this instance is speaker.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is speaker; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSpeaker()
        {
            return this.HasCollaboratorType(Constants.CollaboratorType.Speaker);
        }

        /// <summary>
        /// Determines whether this instance is industry.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is industry; otherwise, <c>false</c>.
        /// </returns>
        public bool IsIndustry()
        {
            return this.HasCollaboratorType(Constants.CollaboratorType.Industry);
        }

        /// <summary>
        /// Determines whether this instance is commission music curator.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is commission curator; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCommissionMusicCurator()
        {
            return this.HasCollaboratorType(Constants.CollaboratorType.CommissionMusicCurator);
        }

        /// <summary>
        /// Determines whether [is ticket buyer].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is ticket buyer]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsTicketBuyer()
        {
            return this.HasAnyCollaboratorType(Constants.CollaboratorType.TicketBuyers);
        }

        /// <summary>Determines whether [has collaborator type] [the specified collaborator type].</summary>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <returns>
        ///   <c>true</c> if [has collaborator type] [the specified collaborator type]; otherwise, <c>false</c>.</returns>
        public bool HasCollaboratorType(string collaboratorType)
        {
            if (this.IsAdmin())
            {
                return true;
            }

            if (string.IsNullOrEmpty(collaboratorType))
            {
                return false;
            }

            return this.EditionCollaboratorTypes?.Any(ect => !ect.IsDeleted && ect.Name == collaboratorType) == true;
        }

        /// <summary>Determines whether [has any collaborator type] [the specified collaborator types].</summary>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <returns>
        ///   <c>true</c> if [has any collaborator type] [the specified collaborator types]; otherwise, <c>false</c>.</returns>
        public bool HasAnyCollaboratorType(string[] collaboratorTypes)
        {
            if (this.IsAdmin())
            {
                return true;
            }

            if (collaboratorTypes?.Any() != true)
            {
                return false;
            }

            return this.EditionCollaboratorTypes?.Any(ect => collaboratorTypes.Contains(ect.Name)) == true;
        }

        /// <summary>
        /// Counts the collaborator types.
        /// </summary>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <returns></returns>
        public int CountCollaboratorTypes(string[] collaboratorTypes)
        {
            if (this.IsAdmin())
            {
                //Return 2 only for...
                return 2;
            }

            if (collaboratorTypes?.Any() != true)
            {
                return 0;
            }

            return this.EditionCollaboratorTypes?.Count(ect => collaboratorTypes.Contains(ect.Name)) ?? 0;
        }

        /// <summary>Determines whether [has edition attendee organization] [the specified attendee organization uid].</summary>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <returns>
        ///   <c>true</c> if [has edition attendee organization] [the specified attendee organization uid]; otherwise, <c>false</c>.</returns>
        public bool HasEditionAttendeeOrganization(Guid attendeeOrganizationUid)
        {
            return this.IsAdmin() || this.EditionAttendeeOrganizations?.Any(eao => eao.Uid == attendeeOrganizationUid) == true;
        }

        /// <summary>Determines whether [has any edition attendee organization] [the specified attendee organization uids].</summary>
        /// <param name="attendeeOrganizationUids">The attendee organization uids.</param>
        /// <returns>
        ///   <c>true</c> if [has any edition attendee organization] [the specified attendee organization uids]; otherwise, <c>false</c>.</returns>
        public bool HasAnyEditionAttendeeOrganization(List<Guid> attendeeOrganizationUids)
        {
            return this.IsAdmin() 
                   || (attendeeOrganizationUids?.Any() == true &&  this.EditionAttendeeOrganizations?.Any(eao => attendeeOrganizationUids.Contains(eao.Uid)) == true);
        }

        /// <summary>
        /// Determines whether [has cards to show].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has cards to show]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasCardsToShow()
        {
            return this.HasAnyCollaboratorType(Constants.CollaboratorType.CardsWidget);
        }

        #endregion

        #region Onboarding

        /// <summary>Determines whether [is onboarding pending].</summary>
        /// <returns>
        ///   <c>true</c> if [is onboarding pending]; otherwise, <c>false</c>.</returns>
        public bool IsOnboardingPending()
        {
            return !this.IsAdmin() 
                   && (!this.IsAttendeeCollaboratorOnboardingFinished() 
                       || !this.IsPlayerExecutiveAttendeeOrganizationsOnboardingFinished() 
                       || this.IsTicketBuyerOrganizationOnboardingPending());
        }

        #region Collaborator

        /// <summary>Determines whether [is attendee collaborator onboarding finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is attendee collaborator onboarding finished]; otherwise, <c>false</c>.</returns>
        public bool IsAttendeeCollaboratorOnboardingFinished()
        {
            return this.IsAudiovisualPlayerTermsAcceptanceFinished() && 
                   this.IsInnovationPlayerTermsAcceptanceFinished() &&
                   this.IsMusicPlayerTermsAcceptanceFinished() &&
                   this.IsUserOnboardingFinished() && 
                   this.IsCollaboratorOnboardingFinished() && 
                   this.IsSpeakerTermsAcceptanceFinished();
        }

        /// <summary>
        /// Determines whether [is audiovisual player terms acceptance finished].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual player terms acceptance finished]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAudiovisualPlayerTermsAcceptanceFinished()
        {
            return !this.IsAudiovisualPlayerExecutive()                                                                 // Is not Audiovisual Player
                        || this.EditionAttendeeCollaborator?.AudiovisualPlayerTermsAcceptanceDate != null;              // Or has accepted the player terms
        }

        /// <summary>
        /// Determines whether [is audiovisual or music player terms acceptance finished].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual or music player terms acceptance finished]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPlayerExecutiveTermsAcceptanceFinished()
        {
            var collaboratorTypes = new string[] { Constants.CollaboratorType.PlayerExecutiveAudiovisual, Constants.CollaboratorType.PlayerExecutiveMusic };
            return !this.HasAnyCollaboratorType(collaboratorTypes)
                || (
                    this.EditionAttendeeCollaborator?.AudiovisualPlayerTermsAcceptanceDate != null
                        || this.EditionAttendeeCollaborator?.MusicPlayerTermsAcceptanceDate != null
                    );
        }

        /// <summary>
        /// Determines whether [is innovation player terms acceptance finished].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is innovation player terms acceptance finished]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInnovationPlayerTermsAcceptanceFinished()
        {
            return !this.IsInnovationPlayerExecutive()                                                                  // Is not Innovation Player
                        || this.EditionAttendeeCollaborator?.InnovationPlayerTermsAcceptanceDate != null;               // Or has accepted the player terms
        }

        /// <summary>
        /// Determines whether [is music player terms acceptance finished].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is music player terms acceptance finished]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMusicPlayerTermsAcceptanceFinished()
        {
            return !this.IsMusicPlayerExecutive()                                                                  // Is not Music Player
                        || this.EditionAttendeeCollaborator?.MusicPlayerTermsAcceptanceDate != null;               // Or has accepted the player terms
        }

        /// <summary>Determines whether [is speaker terms acceptance finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is speaker terms acceptance finished]; otherwise, <c>false</c>.</returns>
        public bool IsSpeakerTermsAcceptanceFinished()
        {
            return !this.HasCollaboratorType(Constants.CollaboratorType.Speaker)                                                            // Is not Speaker
                   || this.EditionAttendeeCollaborator?.SpeakerTermsAcceptanceDate != null;                                                 // Or has accepted the speaker terms
        }

        /// <summary>Determines whether [is user onboarding finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is user onboarding finished]; otherwise, <c>false</c>.</returns>
        public bool IsUserOnboardingFinished()
        {
            return this.EditionAttendeeCollaborator?.OnboardingUserDate != null;
        }

        /// <summary>Determines whether [is collaborator onboarding finished].</summary>
        /// <returns>
        ///   <c>true</c> if [is collaborator onboarding finished]; otherwise, <c>false</c>.</returns>
        public bool IsCollaboratorOnboardingFinished()
        {
            return this.EditionAttendeeCollaborator?.OnboardingCollaboratorDate != null;
        }

        #endregion

        #region Organizations

        /// <summary>
        /// Determines whether [is audiovisual player attendee organizations onboarding finished].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual player attendee organizations onboarding finished]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAudiovisualPlayerAttendeeOrganizationsOnboardingFinished()
        {
            return this.IsAudiovisualPlayerOrganizationsOnboardingFinished() 
                   && this.IsAudiovisualPlayerOrganizationsInterestsOnboardingFinished();
        }

        /// <summary>
        /// Determines whether [is audiovisual or music player attendee organizations onboarding finished].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual or music player attendee organizations onboarding finished]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPlayerExecutiveAttendeeOrganizationsOnboardingFinished()
        {
            return this.IsPlayerExecutiveOrganizationsOnboardingFinished()
                   && this.IsPlayerExecutiveOrganizationsInterestsOnboardingFinished();
        }

        /// <summary>
        /// Determines whether [is audiovisual or music player organizations onboarding finished].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual or music player organizations onboarding finished]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPlayerExecutiveOrganizationsOnboardingFinished()
        {
            var collaboratorTypes = new string[] { Constants.CollaboratorType.PlayerExecutiveAudiovisual, Constants.CollaboratorType.PlayerExecutiveMusic };
            IEnumerable<string> playerOrganizationTypes = new string[] { OrganizationType.AudiovisualPlayer.Name, OrganizationType.MusicPlayer.Name };
            return !this.HasAnyCollaboratorType(collaboratorTypes)
                    || (
                        this.EditionAttendeeOrganizations?.Any() == false
                        || (
                            this.EditionAttendeeOrganizations?.Any() == true
                            && this.EditionAttendeeOrganizations?.Where(ao => {
                                return ao.AttendeeOrganizationTypes.Any(aot => {
                                    return !aot.IsDeleted && playerOrganizationTypes.Any(p => p == aot.OrganizationType.Name);
                                });
                            }).All(ao => {
                                return ao.OnboardingOrganizationDate.HasValue;
                            }) == true
                        )
                    );
        }

        /// <summary>
        /// Determines whether [is audiovisual player organizations onboarding finished].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual player organizations onboarding finished]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAudiovisualPlayerOrganizationsOnboardingFinished()
        {
            return !this.IsAudiovisualPlayerExecutive()                                                                                    // Is not Audiovisual Player 
                    || (this.EditionAttendeeOrganizations?.Any() == false                                                                  // No organization related
                        || (this.EditionAttendeeOrganizations?.Any() == true                                                               // or has at least one organization linked
                            && this.EditionAttendeeOrganizations?.Where(ao => ao.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted && aot.OrganizationType.Name == OrganizationType.AudiovisualPlayer.Name))
                                                                    .All(ao => ao.OnboardingOrganizationDate.HasValue) == true));          // and all organizations interests onboarding are finished
        }

        /// <summary>
        /// Determines whether [is audiovisual player organization interests onboarding pending].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual player organization interests onboarding pending]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAudiovisualPlayerOrganizationInterestsOnboardingPending()
        {
            return this.IsAudiovisualPlayerExecutive()                                                                                      // Is Audiovisual Player
                   && this.EditionAttendeeOrganizations?.Any() == true                                                                      // Has at least one organization linked
                   && this.EditionAttendeeOrganizations?.Where(ao => ao.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted && aot.OrganizationType.Name == OrganizationType.AudiovisualPlayer.Name))
                                                        .Any(ao => ao.OnboardingOrganizationDate.HasValue                                   // and at least one organization onboarded
                                                                   && !ao.OnboardingInterestsDate.HasValue) == true;                        // and this organization interests area not onboarded
        }

        /// <summary>
        /// Determines whether [is audiovisual or music player organization interests onboarding pending].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual or music player organization interests onboarding pending]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPlayerExecutiveOrganizationInterestsOnboardingPending()
        {
            var collaboratorTypes = new string[] { Constants.CollaboratorType.PlayerExecutiveAudiovisual, Constants.CollaboratorType.PlayerExecutiveMusic };
            IEnumerable<string> playerOrganizationTypes = new string[] { OrganizationType.AudiovisualPlayer.Name, OrganizationType.MusicPlayer.Name };
            return this.HasAnyCollaboratorType(collaboratorTypes)
                && this.EditionAttendeeOrganizations?.Any() == true
                && this.EditionAttendeeOrganizations?.Where(ao => {
                    return ao.AttendeeOrganizationTypes.Any(aot => {
                        return !aot.IsDeleted && playerOrganizationTypes.Any(p => p == aot.OrganizationType.Name);
                    });
                }).Any(ao => {
                    return ao.OnboardingOrganizationDate.HasValue && !ao.OnboardingInterestsDate.HasValue;
                }) == true;
        }

        /// <summary>
        /// Determines whether [is audiovisual player organizations interests onboarding finished].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual player organizations interests onboarding finished]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAudiovisualPlayerOrganizationsInterestsOnboardingFinished()
        {
            return !this.IsAudiovisualPlayerExecutive()                                                                                    // Is not Audiovisual Player 
                   || (this.EditionAttendeeOrganizations?.Any() == false                                                                   // No organization related
                       || (this.EditionAttendeeOrganizations?.Any() == true                                                                // or has at least one organization linked
                           && this.EditionAttendeeOrganizations?.Where(ao => ao.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted && aot.OrganizationType.Name == OrganizationType.AudiovisualPlayer.Name))
                                                                    .All(ao => ao.OnboardingInterestsDate.HasValue) == true));             // and all organizations interests onboarding are finished
        }

        /// <summary>
        /// Determines whether [is audiovisual or music player organizations interests onboarding finished].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is audiovisual or music player organizations interests onboarding finished]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPlayerExecutiveOrganizationsInterestsOnboardingFinished()
        {            
            var collaboratorTypes = new string[] { Constants.CollaboratorType.PlayerExecutiveAudiovisual, Constants.CollaboratorType.PlayerExecutiveMusic };
            IEnumerable<string> playerOrganizationTypes = new string[] { OrganizationType.AudiovisualPlayer.Name, OrganizationType.MusicPlayer.Name };
            return !this.HasAnyCollaboratorType(collaboratorTypes)
                   || (
                        this.EditionAttendeeOrganizations?.Any() == false
                        || (
                            this.EditionAttendeeOrganizations?.Any() == true
                            && this.EditionAttendeeOrganizations?.Where(ao => {
                                return ao.AttendeeOrganizationTypes.Any(aot => {
                                    return !aot.IsDeleted && playerOrganizationTypes.Any(p => p == aot.OrganizationType.Name);
                                });
                            }).All(ao => {
                               return ao.OnboardingInterestsDate.HasValue;
                            }) == true
                        )
                    );
        }

        /// <summary>Determines whether [is ticket buyer organization onboarding pending].</summary>
        /// <returns>
        ///   <c>true</c> if [is ticket buyer organization onboarding pending]; otherwise, <c>false</c>.</returns>
        public bool IsTicketBuyerOrganizationOnboardingPending()
        {
            return !this.IsAudiovisualPlayerExecutive()                                                                                    // Is not Player 
                   && this.IsTicketBuyer()                                                                                                 // Is ticket buyer
                   && (!this.EditionAttendeeCollaborator.OnboardingOrganizationDataSkippedDate.HasValue                                    // Not skipped the onboarding of company data
                       && (this.EditionAttendeeOrganizations?.Any() == false                                                               // No organization related
                           || this.EditionAttendeeOrganizations?.Where(ao => ao.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted && aot.OrganizationType.Name != OrganizationType.AudiovisualPlayer.Name))
                                                                    .All(ao => ao.OnboardingFinishDate.HasValue) == false));               // or has organizations without onboarding     
        }

        #endregion

        #endregion

        #region Projects required information

        /// <summary>Determines whether [is project submission organization information pending].</summary>
        /// <returns>
        ///   <c>true</c> if [is project submission organization information pending]; otherwise, <c>false</c>.</returns>
        public bool IsProjectSubmissionOrganizationInformationPending()
        {
            return this.HasCollaboratorType(Constants.CollaboratorType.Industry)
                   && (this.EditionAttendeeOrganizations?.Any() == false
                       || this.EditionAttendeeOrganizations?.Any(ao => ao.ProjectSubmissionOrganizationDate.HasValue) == false);
        }

        /// <summary>Determines whether [is project submission terms acceptance pending].</summary>
        /// <returns>
        ///   <c>true</c> if [is project submission terms acceptance pending]; otherwise, <c>false</c>.</returns>
        public bool IsAudiovisualProducerBusinessRoundTermsAcceptanceDatePending()
        {
            return this.HasCollaboratorType(Constants.CollaboratorType.Industry)
                   && this.EditionAttendeeCollaborator?.AudiovisualProducerBusinessRoundTermsAcceptanceDate.HasValue == false;
        }

        /// <summary>Determines whether [is project submission terms acceptance pending].</summary>
        /// <returns>
        ///   <c>true</c> if [is project submission terms acceptance pending]; otherwise, <c>false</c>.</returns>
        public bool IsAudiovisualProducerPitchingTermsAcceptanceDatePending()
        {
            return this.HasCollaboratorType(Constants.CollaboratorType.Industry)
                   && this.EditionAttendeeCollaborator?.AudiovisualProducerPitchingTermsAcceptanceDate.HasValue == false;
        }

        #endregion 

        #region Music - BusinessRound

        /// <summary>Determines whether [is project submission terms acceptance pending].</summary>
        /// <returns>
        ///   <c>true</c> if [is project submission terms acceptance pending]; otherwise, <c>false</c>.</returns>
        public bool IsMusicProducerBusinessRoundTermsAcceptanceDatePending()
        {
            var collaboratorTypes = new string[] { Constants.CollaboratorType.Creator, Constants.CollaboratorType.Industry };
            return this.HasAnyCollaboratorType(collaboratorTypes)
                   && this.EditionAttendeeCollaborator?.MusicProducerTermsAcceptanceDate.HasValue == false;
        }

        /// <summary>Determines whether [is project submission organization information pending].</summary>
        /// <returns>
        ///   <c>true</c> if [is project submission organization information pending]; otherwise, <c>false</c>.</returns>
        public bool IsMusicProjectSubmissionOrganizationInformationPending()
        {
            return false;
            //TODO:Daniel > Checar com Renan se será necessário novo campo de controle no AttendeeOrganizations tbm.
            var collaboratorTypes = new string[] { Constants.CollaboratorType.Creator, Constants.CollaboratorType.Industry };
            return this.HasAnyCollaboratorType(collaboratorTypes)
                   && (this.EditionAttendeeOrganizations?.Any() == false
                       || this.EditionAttendeeOrganizations?.Any(ao => ao.ProjectSubmissionOrganizationDate.HasValue) == false);
        }

        #endregion

    }
}