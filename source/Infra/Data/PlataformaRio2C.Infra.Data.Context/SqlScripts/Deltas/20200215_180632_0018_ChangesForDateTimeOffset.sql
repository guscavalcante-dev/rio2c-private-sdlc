--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Users" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."Users" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."Languages" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."Languages" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."Holdings" 
   ALTER COLUMN "ImageUploadDate" datetimeoffset
go

ALTER TABLE "dbo"."Holdings" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."Holdings" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."HoldingDescriptions" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."HoldingDescriptions" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."Activities" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."Activities" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."TargetAudiences" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."TargetAudiences" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."InterestGroups" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."InterestGroups" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."Interests" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."Interests" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."Organizations" 
   ALTER COLUMN "ImageUploadDate" datetimeoffset
go

ALTER TABLE "dbo"."Organizations" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."Organizations" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."OrganizationInterests" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."OrganizationInterests" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."OrganizationTargetAudiences" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."OrganizationTargetAudiences" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."OrganizationActivities" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."OrganizationActivities" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."OrganizationDescriptions" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."OrganizationDescriptions" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."OrganizationRestrictionSpecifics" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."OrganizationRestrictionSpecifics" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."Collaborators" 
   ALTER COLUMN "ImageUploadDate" datetimeoffset
go

ALTER TABLE "dbo"."Collaborators" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."Collaborators" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."Cities" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."Cities" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."States" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."States" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."Countries" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."Countries" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."Addresses" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."Addresses" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."CollaboratorMiniBios" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."CollaboratorMiniBios" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."CollaboratorJobTitles" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."CollaboratorJobTitles" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."SystemParameters" 
   ALTER COLUMN "DateChanges" datetimeoffset
go

ALTER TABLE "dbo"."Logistics" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."Logistics" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."SalesPlatforms" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."SalesPlatforms" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."Projects" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."Projects" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."Projects" 
   ALTER COLUMN "FinishDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectImageLinks" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectImageLinks" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectTeaserLinks" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectTeaserLinks" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."OrganizationTypes" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."OrganizationTypes" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectSummaries" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectSummaries" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectLogLines" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectLogLines" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectAdditionalInformations" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectAdditionalInformations" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectTitles" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectTitles" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectBuyerEvaluations" 
   ALTER COLUMN "EvaluationDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectBuyerEvaluations" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectBuyerEvaluations" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectBuyerEvaluations" 
   ALTER COLUMN "BuyerEmailSendDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectEvaluationStatuses" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectEvaluationStatuses" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."Negotiations" 
   ALTER COLUMN "StartDate" datetimeoffset
go

ALTER TABLE "dbo"."Negotiations" 
   ALTER COLUMN "EndDate" datetimeoffset
go

ALTER TABLE "dbo"."Negotiations" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."Negotiations" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."Rooms" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."Rooms" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."NegotiationRoomConfigs" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."NegotiationRoomConfigs" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."NegotiationConfigs" 
   ALTER COLUMN "StartDate" datetimeoffset
go

ALTER TABLE "dbo"."NegotiationConfigs" 
   ALTER COLUMN "EndDate" datetimeoffset
go

ALTER TABLE "dbo"."NegotiationConfigs" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."NegotiationConfigs" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."RoomNames" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."RoomNames" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."Conferences" 
   ALTER COLUMN "StartDate" datetimeoffset
go

ALTER TABLE "dbo"."Conferences" 
   ALTER COLUMN "EndDate" datetimeoffset
go

ALTER TABLE "dbo"."Conferences" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."Conferences" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."ConferenceSynopsis" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."ConferenceSynopsis" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."ConferenceTitles" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."ConferenceTitles" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."ConferenceParticipants" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."ConferenceParticipants" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."ConferenceParticipantRoles" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."ConferenceParticipantRoles" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."ConferenceParticipantRoleTitles" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."ConferenceParticipantRoleTitles" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectInterests" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectInterests" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."Messages" 
   ALTER COLUMN "SendDate" datetimeoffset
go

DROP INDEX "IDX_Messages_ReadDate" ON "dbo"."Messages" 
go

ALTER TABLE "dbo"."Messages" 
   ALTER COLUMN "ReadDate" datetimeoffset
go

CREATE NONCLUSTERED INDEX "IDX_Messages_ReadDate" ON "dbo"."Messages"
( 
	"ReadDate"            ASC
)
go

DROP INDEX "IDX_Messages_NotificationEmailSendDate" ON "dbo"."Messages"
go

ALTER TABLE "dbo"."Messages" 
   ALTER COLUMN "NotificationEmailSendDate" datetimeoffset
go

CREATE NONCLUSTERED INDEX "IDX_Messages_NotificationEmailSendDate" ON "dbo"."Messages"
( 
	"NotificationEmailSendDate"  ASC
)
go

ALTER TABLE "dbo"."Editions" 
   ALTER COLUMN "StartDate" datetimeoffset
go

ALTER TABLE "dbo"."Editions" 
   ALTER COLUMN "EndDate" datetimeoffset
go

ALTER TABLE "dbo"."Editions" 
   ALTER COLUMN "SellStartDate" datetimeoffset
go

ALTER TABLE "dbo"."Editions" 
   ALTER COLUMN "SellEndDate" datetimeoffset
go

ALTER TABLE "dbo"."Editions" 
   ALTER COLUMN "ProjectSubmitStartDate" datetimeoffset
go

ALTER TABLE "dbo"."Editions" 
   ALTER COLUMN "ProjectSubmitEndDate" datetimeoffset
go

ALTER TABLE "dbo"."Editions" 
   ALTER COLUMN "ProjectEvaluationStartDate" datetimeoffset
go

ALTER TABLE "dbo"."Editions" 
   ALTER COLUMN "ProjectEvaluationEndDate" datetimeoffset
go

ALTER TABLE "dbo"."Editions" 
   ALTER COLUMN "NegotiationStartDate" datetimeoffset
go

ALTER TABLE "dbo"."Editions" 
   ALTER COLUMN "NegotiationEndDate" datetimeoffset
go

ALTER TABLE "dbo"."Editions" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."Editions" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."Editions" 
   ALTER COLUMN "OneToOneMeetingsScheduleDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeOrganizations" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeOrganizations" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeOrganizations" 
   ALTER COLUMN "OnboardingFinishDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeOrganizations" 
   ALTER COLUMN "OnboardingInterestsDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeOrganizations" 
   ALTER COLUMN "OnboardingStartDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeOrganizations" 
   ALTER COLUMN "OnboardingOrganizationDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeOrganizations" 
   ALTER COLUMN "ProjectSubmissionOrganizationDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeOrganizationTypes" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeOrganizationTypes" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeCollaborators" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeCollaborators" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeCollaborators" 
   ALTER COLUMN "WelcomeEmailSendDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeCollaborators" 
   ALTER COLUMN "OnboardingFinishDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeCollaborators" 
   ALTER COLUMN "OnboardingStartDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeCollaborators" 
   ALTER COLUMN "OnboardingUserDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeCollaborators" 
   ALTER COLUMN "OnboardingCollaboratorDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeCollaborators" 
   ALTER COLUMN "PlayerTermsAcceptanceDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeCollaborators" 
   ALTER COLUMN "ProducerTermsAcceptanceDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeCollaborators" 
   ALTER COLUMN "OnboardingOrganizationDataSkippedDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeCollaborators" 
   ALTER COLUMN "SpeakerTermsAcceptanceDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeOrganizationCollaborators" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeOrganizationCollaborators" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeSalesPlatforms" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeSalesPlatforms" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeSalesPlatformTicketTypes" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeSalesPlatformTicketTypes" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeCollaboratorTickets" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeCollaboratorTickets" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeCollaboratorTickets" 
   ALTER COLUMN "BarcodeUpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."AttendeeCollaboratorTickets" 
   ALTER COLUMN "SalesPlatformUpdateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectTypes" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "dbo"."ProjectTypes" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "Quizzes" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "Quizzes" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "QuizQuestions" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "QuizQuestions" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "QuizOptions" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "QuizOptions" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "QuizAnswers" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "QuizAnswers" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

DROP INDEX "IDX_SalesPlatformWebhookRequests_IsProcessed_IsProcessing_CreateDate" ON "SalesPlatformWebhookRequests"
go

ALTER TABLE "SalesPlatformWebhookRequests" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

CREATE NONCLUSTERED INDEX "IDX_SalesPlatformWebhookRequests_IsProcessed_IsProcessing_CreateDate" ON "SalesPlatformWebhookRequests"
( 
	"IsProcessed"         ASC,
	"IsProcessing"        ASC,
	"CreateDate"          ASC
)
go

ALTER TABLE "SalesPlatformWebhookRequests" 
   ALTER COLUMN "LastProcessingDate" datetimeoffset
go

ALTER TABLE "SalesPlatformWebhookRequests" 
   ALTER COLUMN "NextProcessingDate" datetimeoffset
go

ALTER TABLE "SentEmails" 
   ALTER COLUMN "EmailSendDate" datetimeoffset
go

ALTER TABLE "SentEmails" 
   ALTER COLUMN "EmailReadDate" datetimeoffset
go

ALTER TABLE "CollaboratorTypes" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "CollaboratorTypes" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "AttendeeCollaboratorTypes" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "AttendeeCollaboratorTypes" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "ProjectProductionPlans" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "ProjectProductionPlans" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "ProjectTargetAudiences" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "ProjectTargetAudiences" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "SubscribeLists" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "SubscribeLists" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "UserUnsubscribedLists" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "UserUnsubscribedLists" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "ProjectEvaluationRefuseReasons" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "ProjectEvaluationRefuseReasons" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "EditionEvents" 
   ALTER COLUMN "StartDate" datetimeoffset
go

ALTER TABLE "EditionEvents" 
   ALTER COLUMN "EndDate" datetimeoffset
go

ALTER TABLE "EditionEvents" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "EditionEvents" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "Tracks" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "Tracks" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "ConferenceTracks" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "ConferenceTracks" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "PresentationFormats" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "PresentationFormats" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "ConferencePresentationFormats" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "ConferencePresentationFormats" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "CollaboratorRoles" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "CollaboratorRoles" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "CollaboratorIndustries" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "CollaboratorIndustries" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "CollaboratorGenders" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "CollaboratorGenders" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "CollaboratorEditionParticipations" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "CollaboratorEditionParticipations" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "Pillars" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "Pillars" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

ALTER TABLE "ConferencePillars" 
   ALTER COLUMN "CreateDate" datetimeoffset
go

ALTER TABLE "ConferencePillars" 
   ALTER COLUMN "UpdateDate" datetimeoffset
go

update dbo.Editions
set
	StartDate = DATEADD(HOUR, 3, StartDate),
	EndDate = DATEADD(HOUR, 3, EndDate),
	SellStartDate = DATEADD(HOUR, 3, SellStartDate),
	SellEndDate = DATEADD(HOUR, 3, SellEndDate),
	ProjectSubmitStartDate = DATEADD(HOUR, 3, ProjectSubmitStartDate),
	ProjectSubmitEndDate = DATEADD(HOUR, 3, ProjectSubmitEndDate),
	ProjectEvaluationStartDate = DATEADD(HOUR, 3, ProjectEvaluationStartDate),
	ProjectEvaluationEndDate = DATEADD(HOUR, 3, ProjectEvaluationEndDate),
	NegotiationStartDate = DATEADD(HOUR, 3, NegotiationStartDate),
	NegotiationEndDate = DATEADD(HOUR, 3, NegotiationEndDate),
	OneToOneMeetingsScheduleDate = DATEADD(HOUR, 3, OneToOneMeetingsScheduleDate)
go

update dbo.EditionEvents
set
	StartDate = DATEADD(HOUR, 3, StartDate),
	EndDate = DATEADD(HOUR, 3, EndDate)
go