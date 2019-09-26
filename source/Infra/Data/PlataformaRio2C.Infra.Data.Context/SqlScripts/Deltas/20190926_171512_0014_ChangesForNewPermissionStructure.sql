--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."TicketTypes"
DROP CONSTRAINT "FK_Roles_TicketTypes_RoleId"
go

ALTER TABLE "dbo"."TicketTypes"
DROP CONSTRAINT "FK_Users_TicketTypes_CreateUserId"
go

ALTER TABLE "dbo"."TicketTypes"
DROP CONSTRAINT "FK_Users_TicketTypes_UpdateUserId"
go

ALTER TABLE "dbo"."AttendeeSalesPlatformTicketTypes"
DROP CONSTRAINT "FK_TicketTypes_AttendeeSalesPlatformTicketTypes_TicketTypeId"
go

ALTER TABLE "dbo".TicketTypes
DROP CONSTRAINT "PK_TicketTypes"
go

ALTER TABLE "dbo"."TicketTypes"
DROP CONSTRAINT "IDX_UQ_TicketTypes_Uid"
go

ALTER TABLE "dbo"."TicketTypes"
DROP CONSTRAINT "IDX_UQ_TicketTypes_Code"
go

ALTER TABLE "dbo"."AttendeeSalesPlatformTicketTypes"
  DROP COLUMN "TicketTypeId"
go

ALTER TABLE "dbo"."AttendeeSalesPlatformTicketTypes"
ADD CollaboratorTypeId  int  NULL
go

CREATE TABLE "CollaboratorTypes"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(256)  NOT NULL ,
	"RoleId"             int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE TABLE "AttendeeCollaboratorTypes"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"AttendeeCollaboratorId" int  NOT NULL ,
	"CollaboratorTypeId" int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

DROP table "dbo"."TicketTypes"
go

ALTER TABLE "CollaboratorTypes"
ADD CONSTRAINT "PK_CollaboratorTypes" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "CollaboratorTypes"
ADD CONSTRAINT "IDX_UQ_CollaboratorTypes_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "AttendeeCollaboratorTypes"
ADD CONSTRAINT "PK_AttendeeCollaboratorTypes" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "AttendeeCollaboratorTypes"
ADD CONSTRAINT "IDX_UQ_AttendeeCollaboratorTypes_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "AttendeeCollaboratorTypes"
ADD CONSTRAINT "IDX_UQ_AttendeeCollaboratorTypes_AttendeeCollaboratorId_CollaboratorTypeId" UNIQUE ("AttendeeCollaboratorId"  ASC,"CollaboratorTypeId"  ASC)
go

ALTER TABLE "CollaboratorTypes"
	ADD CONSTRAINT "FK_Users_CollaboratorTypes_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "CollaboratorTypes"
	ADD CONSTRAINT "FK_Users_CollaboratorTypes_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "CollaboratorTypes"
	ADD CONSTRAINT "FK_Roles_CollaboratorTypes_RoleId" FOREIGN KEY ("RoleId") REFERENCES "dbo"."Roles"("Id")
go

ALTER TABLE "AttendeeCollaboratorTypes"
	ADD CONSTRAINT "FK_AttendeeCollaborators_AttendeeCollaboratorTypes_AttendeeCollaboratorId" FOREIGN KEY ("AttendeeCollaboratorId") REFERENCES "dbo"."AttendeeCollaborators"("Id")
go

ALTER TABLE "AttendeeCollaboratorTypes"
	ADD CONSTRAINT "FK_CollaboratorTypes_AttendeeCollaboratorTypes_CollaboratorTypeId" FOREIGN KEY ("CollaboratorTypeId") REFERENCES "CollaboratorTypes"("Id")
go

ALTER TABLE "AttendeeCollaboratorTypes"
	ADD CONSTRAINT "FK_Users_AttendeeCollaboratorTypes_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeCollaboratorTypes"
	ADD CONSTRAINT "FK_Users_AttendeeCollaboratorTypes_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "dbo"."AttendeeSalesPlatformTicketTypes"
	ADD CONSTRAINT "FK_CollaboratorTypes_AttendeeSalesPlatformTicketTypes_CollaboratorTypeId" FOREIGN KEY ("CollaboratorTypeId") REFERENCES "CollaboratorTypes"("Id")
go

SET IDENTITY_INSERT [dbo].[CollaboratorTypes] ON 
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (100, N'f886728d-6937-4ac9-8ebd-c06e2c524cb7', N'Admin', 1, 0, CAST(N'2019-09-26 17:48:45.390' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.390' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (101, N'3871f510-c081-4b69-9ecc-8889e791b0cc', N'Admin | Audiovisual', 1, 0, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (200, N'2d6f0e07-7990-458a-8207-1471dc3d1833', N'Executive | Audiovisual', 2, 0, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (201, N'f05c6213-5cde-46b8-a617-df339d9903a9', N'Executive | Music', 2, 0, CAST(N'2019-09-26 17:48:45.407' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.407' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (202, N'7e4909e0-3de9-4b55-a678-3c4c277a89da', N'Executive | Innovation', 2, 0, CAST(N'2019-09-26 17:48:45.407' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.407' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (300, N'60aafb26-b483-425f-bfa6-ed0d45f3cbcb', N'Commission | Audiovisual', 2, 0, CAST(N'2019-09-26 17:48:45.410' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.410' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (301, N'3633cf67-840f-4061-b480-c075a5e9f5ee', N'Commission | Music', 2, 0, CAST(N'2019-09-26 17:48:45.413' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.413' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (302, N'758a53bb-7c3c-4b6f-967b-c6e613568586', N'Commission | Innovation', 2, 0, CAST(N'2019-09-26 17:48:45.413' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.413' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (400, N'5da172d8-8d4a-493b-9eee-f544805a511f', N'Speaker', 2, 0, CAST(N'2019-09-26 17:48:45.417' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.417' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (500, N'4b0dd2ca-12ae-4357-bec4-ba4d3820351d', N'Industry', 2, 0, CAST(N'2019-09-26 17:48:45.417' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.417' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (501, N'1a3bb310-44d0-4677-9938-394c138fd77c', N'Creator', 2, 0, CAST(N'2019-09-26 17:48:45.420' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.420' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (502, N'536824fb-e98d-4949-b6be-e6e94d8329e4', N'Summit', 2, 0, CAST(N'2019-09-26 17:48:45.420' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.420' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (503, N'c23c069d-0e3f-4e52-a96d-1f0abd79e82d', N'Festvalia', 2, 0, CAST(N'2019-09-26 17:48:45.423' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.423' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[CollaboratorTypes] OFF
GO

UPDATE [dbo].[AttendeeSalesPlatformTicketTypes] SET [CollaboratorTypeId] = 500 WHERE [Uid] = '6d0a919e-484e-4b42-85c2-264e6759757c'
GO
UPDATE [dbo].[AttendeeSalesPlatformTicketTypes] SET [CollaboratorTypeId] = 500 WHERE [Uid] = '0dd84df9-4a9c-4749-b37f-db4046283b6d'
GO
UPDATE [dbo].[AttendeeSalesPlatformTicketTypes] SET [CollaboratorTypeId] = 501 WHERE [Uid] = '0a435465-da95-417a-af63-67816d4524d4'
GO
UPDATE [dbo].[AttendeeSalesPlatformTicketTypes] SET [CollaboratorTypeId] = 501 WHERE [Uid] = '68d892b7-2516-4623-96c4-a85947dcbde1'
GO

ALTER TABLE "dbo"."AttendeeSalesPlatformTicketTypes"
ALTER COLUMN CollaboratorTypeId  int  NOT NULL
go
