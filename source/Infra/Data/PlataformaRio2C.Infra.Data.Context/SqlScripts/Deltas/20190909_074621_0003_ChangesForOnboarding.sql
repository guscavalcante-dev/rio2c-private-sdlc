--must run on deploy: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."AttendeeOrganizations"
DROP COLUMN OnboardingFinishDate
go

ALTER TABLE "dbo"."AttendeeOrganizations"
ADD OnboardingStartDate  datetime  NULL
go

ALTER TABLE "dbo"."AttendeeOrganizations"
ADD OnboardingFinishDate  datetime  NULL
go

ALTER TABLE "dbo"."AttendeeOrganizations"
ADD OnboardingOrganizationDate  datetime  NULL
go

ALTER TABLE "dbo"."AttendeeOrganizations"
ADD OnboardingInterestsDate  date  NULL
go


ALTER TABLE "dbo"."AttendeeCollaborators"
DROP COLUMN OnboardingFinishDate
go

ALTER TABLE "dbo"."AttendeeCollaborators"
ADD OnboardingStartDate  datetime  NULL
go

ALTER TABLE "dbo"."AttendeeCollaborators"
ADD OnboardingFinishDate  datetime  NULL
go

ALTER TABLE "dbo"."AttendeeCollaborators"
ADD OnboardingUserDate  datetime  NULL
go

ALTER TABLE "dbo"."AttendeeCollaborators"
ADD OnboardingCollaboratorDate  datetime  NULL
go
