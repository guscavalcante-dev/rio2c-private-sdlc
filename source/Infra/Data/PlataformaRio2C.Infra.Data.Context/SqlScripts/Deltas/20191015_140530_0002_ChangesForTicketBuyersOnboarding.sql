--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."AttendeeCollaborators"
ADD OnboardingOrganizationDataSkippedDate  datetime  NULL
go