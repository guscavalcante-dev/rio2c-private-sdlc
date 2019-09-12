--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."AttendeeCollaborators"
ADD PlayerTermsAcceptanceDate  datetime  NULL
go

ALTER TABLE "dbo"."AttendeeCollaborators"
ADD ProducerTermsAcceptanceDate  datetime  NULL
go
