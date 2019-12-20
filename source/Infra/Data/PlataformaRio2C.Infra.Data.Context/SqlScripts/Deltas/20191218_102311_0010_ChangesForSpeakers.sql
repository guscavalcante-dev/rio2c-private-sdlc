--must run on deploy | test: yes, done
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

CREATE NONCLUSTERED INDEX [IDX_AttendeeCollaboratorTypes_IsApiDisplayEnabled] ON [dbo].[AttendeeCollaboratorTypes]
(
	[IsApiDisplayEnabled] ASC
)
go

CREATE NONCLUSTERED INDEX [IDX_AttendeeOrganizationTypes_IsApiDisplayEnabled] ON [dbo].[AttendeeOrganizationTypes]
(
	[IsApiDisplayEnabled] ASC
)
go

SET IDENTITY_INSERT [dbo].[CollaboratorTypes] ON 
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (101, N'4ac5a971-ba73-493b-9749-0f51bb6925b5', N'Curatorship | Audiovisual', 2, 0, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[CollaboratorTypes] OFF
GO
