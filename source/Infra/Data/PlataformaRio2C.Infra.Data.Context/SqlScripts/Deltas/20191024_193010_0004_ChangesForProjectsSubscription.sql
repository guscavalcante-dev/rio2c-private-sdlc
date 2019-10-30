--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Editions"
ADD OneToOneMeetingsScheduleDate  datetime  NULL
go

UPDATE dbo.Editions SET OneToOneMeetingsScheduleDate = '2019-03-23' WHERE Name = 'Rio2C 2019'
go

UPDATE dbo.Editions SET OneToOneMeetingsScheduleDate = '2020-03-23' WHERE Name = 'Rio2C 2020'
go

ALTER TABLE "dbo"."Editions"
ALTER COLUMN OneToOneMeetingsScheduleDate  datetime  NOT NULL
go

ALTER TABLE "dbo"."AttendeeOrganizations"
ADD ProjectSubmissionOrganizationDate  datetime  NULL
go