--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."AttendeeCollaborators"
	ADD SpeakerTermsAcceptanceDate  datetime  NULL
go

ALTER TABLE "dbo"."AttendeeOrganizations"
	ADD ApiHighlightPosition  int  NULL
go

ALTER TABLE "dbo"."AttendeeCollaborators"
	ADD ApiHighlightPosition  int  NULL
go
