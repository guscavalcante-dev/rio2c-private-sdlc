--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."AttendeeOrganizationTypes"
ADD IsApiDisplayEnabled  bit  NULL
go

UPDATE "dbo"."AttendeeOrganizationTypes"
SET IsApiDisplayEnabled = 0
go

UPDATE dbo.AttendeeOrganizationTypes
SET IsApiDisplayEnabled = 1
FROM
	dbo.Organizations o
	INNER JOIN dbo.AttendeeOrganizations ao ON o.Id = ao.OrganizationId AND ao.IsApiDisplayEnabled = 1 AND ao.EditionId = 2 AND ao.IsDeleted = 0
	INNER JOIN dbo.AttendeeOrganizationTypes aot ON ao.Id = aot.AttendeeOrganizationId AND OrganizationTypeId = 1 AND aot.IsDeleted = 0
WHERE
	o.IsDeleted = 0
go

ALTER TABLE "dbo"."AttendeeOrganizationTypes"
ALTER COLUMN IsApiDisplayEnabled  bit  NOT NULL
go

ALTER TABLE "dbo"."AttendeeOrganizationTypes"
ADD ApiHighlightPosition  int  NULL
go


ALTER TABLE "AttendeeCollaboratorTypes"
ADD IsApiDisplayEnabled  bit  NULL
go

UPDATE "dbo"."AttendeeCollaboratorTypes"
SET IsApiDisplayEnabled = 0
go

ALTER TABLE "AttendeeCollaboratorTypes"
ALTER COLUMN IsApiDisplayEnabled  bit  NOT NULL
go

ALTER TABLE "AttendeeCollaboratorTypes"
ADD ApiHighlightPosition  int  NULL
go

ALTER TABLE "dbo"."AttendeeOrganizations"
  DROP COLUMN "IsApiDisplayEnabled"
go

ALTER TABLE "dbo"."AttendeeOrganizations"
  DROP COLUMN "ApiHighlightPosition"
go

ALTER TABLE "dbo"."AttendeeCollaborators"
  DROP COLUMN "IsApiDisplayEnabled"
go

ALTER TABLE "dbo"."AttendeeCollaborators"
  DROP COLUMN "ApiHighlightPosition"
go
