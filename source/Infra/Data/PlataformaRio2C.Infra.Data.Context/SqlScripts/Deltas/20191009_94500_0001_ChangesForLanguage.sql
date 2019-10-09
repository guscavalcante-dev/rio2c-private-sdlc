--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."AttendeeOrganizations"
ADD IsApiDisplayEnabled  bit  NULL
go

ALTER TABLE "dbo"."AttendeeCollaborators"
ADD IsApiDisplayEnabled  bit  NULL
go

UPDATE "dbo"."AttendeeOrganizations"
SET IsApiDisplayEnabled = 0
go

UPDATE "dbo"."AttendeeCollaborators"
SET IsApiDisplayEnabled = 0
go

ALTER TABLE "dbo"."AttendeeOrganizations"
ALTER COLUMN IsApiDisplayEnabled  bit  NOT NULL
go

ALTER TABLE "dbo"."AttendeeCollaborators"
ALTER COLUMN IsApiDisplayEnabled  bit  NOT NULL
GO

UPDATE "dbo"."Languages" 
SET Code = 'en-us' 
WHERE Code = 'en'
GO