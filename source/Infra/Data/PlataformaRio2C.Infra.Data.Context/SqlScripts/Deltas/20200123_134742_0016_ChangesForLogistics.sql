--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

CREATE TABLE "LogisticSponsors"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(100)  NOT NULL ,
	"IsAirfareTicketRequired" bit  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE TABLE "AttendeeLogisticSponsors"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"EditionId"          int  NOT NULL ,
	"LogisticSponsorId"  int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeeLogisticSponsors_EditionId_IsDeleted" ON "AttendeeLogisticSponsors"
( 
	"EditionId"           ASC,
	"IsDeleted"           ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeeLogisticSponsors_LogisticSponsorId_IsDeleted" ON "AttendeeLogisticSponsors"
( 
	"LogisticSponsorId"   ASC,
	"IsDeleted"           ASC
)
go

CREATE TABLE "Places"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(100)  NOT NULL ,
	"IsHotel"            bit  NOT NULL ,
	"AddressId"          int  NULL ,
	"Website"            varchar(300)  NULL ,
	"AdditionalInfo"     varchar(1000)  NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE TABLE "AttendeePlaces"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"EditionId"          int  NOT NULL ,
	"PlaceId"            int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeePlaces_EditionId_IsDeleted" ON "AttendeePlaces"
( 
	"EditionId"           ASC,
	"IsDeleted"           ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeePlaces_PlaceId_IsDeleted" ON "AttendeePlaces"
( 
	"PlaceId"             ASC,
	"IsDeleted"           ASC
)
go

ALTER TABLE "LogisticSponsors"
ADD CONSTRAINT "PK_LogisticSponsors" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "LogisticSponsors"
ADD CONSTRAINT "IDX_UQ_LogisticSponsors_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "AttendeeLogisticSponsors"
ADD CONSTRAINT "PK_AttendeeLogisticSponsors_Uid" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "Places"
ADD CONSTRAINT "PK_Places" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "Places"
ADD CONSTRAINT "IDX_UQ_Places_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "AttendeePlaces"
ADD CONSTRAINT "PK_AttendeePlaces" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "AttendeePlaces"
ADD CONSTRAINT "IDX_UQ_AttendeePlaces" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "LogisticSponsors"
	ADD CONSTRAINT "FK_Users_LogisticSponsors_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "LogisticSponsors"
	ADD CONSTRAINT "FK_Users_LogisticSponsors_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeLogisticSponsors"
	ADD CONSTRAINT "FK_Editions_AttendeeLogisticSponsors_EditionId" FOREIGN KEY ("EditionId") REFERENCES "dbo"."Editions"("Id")
go

ALTER TABLE "AttendeeLogisticSponsors"
	ADD CONSTRAINT "FK_LogisticSponsors_AttendeeLogisticSponsors_LogisticSponsorId" FOREIGN KEY ("LogisticSponsorId") REFERENCES "LogisticSponsors"("Id")
go

ALTER TABLE "AttendeeLogisticSponsors"
	ADD CONSTRAINT "FK_Users_AttendeeLogisticSponsors_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeLogisticSponsors"
	ADD CONSTRAINT "FK_Users_AttendeeLogisticSponsors_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "Places"
	ADD CONSTRAINT "FK_Addresses_Places_AddressId" FOREIGN KEY ("AddressId") REFERENCES "dbo"."Addresses"("Id")
go

ALTER TABLE "Places"
	ADD CONSTRAINT "FK_Users_Places_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "Places"
	ADD CONSTRAINT "FK_Users_Places_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeePlaces"
	ADD CONSTRAINT "FK_Editions_AttendeePlaces_EditionId" FOREIGN KEY ("EditionId") REFERENCES "dbo"."Editions"("Id")
go

ALTER TABLE "AttendeePlaces"
	ADD CONSTRAINT "FK_Places_AttendeePlaces_PlaceId" FOREIGN KEY ("PlaceId") REFERENCES "Places"("Id")
go

ALTER TABLE "AttendeePlaces"
	ADD CONSTRAINT "FK_Users_AttendeePlaces_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeePlaces"
	ADD CONSTRAINT "FK_Users_AttendeePlaces_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go


-- Places Addresses
INSERT INTO [dbo].[Addresses] ([Uid], [CountryId], [StateId], [CityId], [Address1], [ZipCode], [IsManual], [Latitude], [Longitude], [IsGeoLocationUpdated], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (NEWID(), (SELECT Id FROM dbo.Countries where Code = 'BR'), (SELECT Id FROM dbo.States where Code = 'RJ'), (SELECT Id FROM dbo.Cities where Name = 'Rio de Janeiro'), 'Av. das Américas, 8585 - Barra da Tijuca', '22793-081', 0, null, null, 0, 0, GETDATE(), 1, GETDATE(), 1)
GO
INSERT INTO [dbo].[Addresses] ([Uid], [CountryId], [StateId], [CityId], [Address1], [ZipCode], [IsManual], [Latitude], [Longitude], [IsGeoLocationUpdated], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (NEWID(), (SELECT Id FROM dbo.Countries where Code = 'BR'), (SELECT Id FROM dbo.States where Code = 'RJ'), (SELECT Id FROM dbo.Cities where Name = 'Rio de Janeiro'), 'Av. Lúcio Costa, 5400 - Barra da Tijuca', '22630-012', 0, null, null, 0, 0, GETDATE(), 1, GETDATE(), 1)
GO
INSERT INTO [dbo].[Addresses] ([Uid], [CountryId], [StateId], [CityId], [Address1], [ZipCode], [IsManual], [Latitude], [Longitude], [IsGeoLocationUpdated], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (NEWID(), (SELECT Id FROM dbo.Countries where Code = 'BR'), (SELECT Id FROM dbo.States where Code = 'RJ'), (SELECT Id FROM dbo.Cities where Name = 'Rio de Janeiro'), 'Av. das Américas, 5300 - Barra da Tijuca', '22793-080', 0, null, null, 0, 0, GETDATE(), 1, GETDATE(), 1)
GO

-- Places
SET IDENTITY_INSERT [dbo].[Places] ON 
GO
INSERT [dbo].[Places] ([Id], [Uid], [Name], [IsHotel], [AddressId], [Website], [AdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'daa2cf74-bf8a-49df-aaa1-af0ad3640dff', N'Vogue Square Fashion Hotel', 1,  (SELECT Id FROM dbo.Addresses WHERE Address1 = 'Av. das Américas, 8585 - Barra da Tijuca' AND ZipCode = '22793-081'), N'http://www.voguefashionhotel.com.br', N'Café da Manhã Incluído | Breakfast included', 0, CAST(N'2020-01-23 14:50:28.463' AS DateTime), 1, CAST(N'2020-01-23 14:50:28.463' AS DateTime), 1)
GO
INSERT [dbo].[Places] ([Id], [Uid], [Name], [IsHotel], [AddressId], [Website], [AdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'f334193e-0b47-4cbc-81b3-657c88a91981', N'Windsor Marapendi', 1, (SELECT Id FROM dbo.Addresses WHERE Address1 = 'Av. Lúcio Costa, 5400 - Barra da Tijuca' AND ZipCode = '22630-012'), N'https://windsorhoteis.com/hotel/windsor-marapendi', N'Café da Manhã Incluído | Breakfast included', 0, CAST(N'2020-01-23 14:50:28.473' AS DateTime), 1, CAST(N'2020-01-23 14:50:28.473' AS DateTime), 1)
GO
INSERT [dbo].[Places] ([Id], [Uid], [Name], [IsHotel], [AddressId], [Website], [AdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'39b526d0-e560-4eed-8ef4-27251e8a8cde', N'Cidade das Artes', 0, (SELECT Id FROM dbo.Addresses WHERE Address1 = 'Av. das Américas, 5300 - Barra da Tijuca' AND ZipCode = '22793-080'), NULL, NULL, 0, CAST(N'2020-01-23 14:50:28.487' AS DateTime), 1, CAST(N'2020-01-23 14:50:28.487' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Places] OFF
GO

SET IDENTITY_INSERT [dbo].[AttendeePlaces] ON 
GO
INSERT [dbo].[AttendeePlaces] ([Id], [Uid], [EditionId], [PlaceId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'a9a88577-df52-4777-a816-1eca2d82269f', 2, 1, 0, CAST(N'2020-01-23 14:54:28.037' AS DateTime), 1, CAST(N'2020-01-23 14:54:28.037' AS DateTime), 1)
GO
INSERT [dbo].[AttendeePlaces] ([Id], [Uid], [EditionId], [PlaceId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'1dc3637c-48fd-4b59-b103-119dae5f8849', 2, 2, 0, CAST(N'2020-01-23 14:54:28.040' AS DateTime), 1, CAST(N'2020-01-23 14:54:28.040' AS DateTime), 1)
GO
INSERT [dbo].[AttendeePlaces] ([Id], [Uid], [EditionId], [PlaceId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'b1302f94-586a-47d6-ba7f-a101321f1664', 2, 3, 0, CAST(N'2020-01-23 14:54:28.050' AS DateTime), 1, CAST(N'2020-01-23 14:54:28.050' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[AttendeePlaces] OFF
GO

-- Logistic Sponsors
SET IDENTITY_INSERT [dbo].[LogisticSponsors] ON 
GO
INSERT [dbo].[LogisticSponsors] ([Id], [Uid], [Name], [IsAirfareTicketRequired], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'56d74d6b-31d5-4652-8685-213216fb22a8', N'Rio2C', 1, 0, CAST(N'2020-01-23 15:02:16.733' AS DateTime), 1, CAST(N'2020-01-23 15:02:16.733' AS DateTime), 1)
GO
INSERT [dbo].[LogisticSponsors] ([Id], [Uid], [Name], [IsAirfareTicketRequired], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'b75920d0-05e0-4b72-bed1-1ab3cfe8ed2b', N'APEX', 1, 0, CAST(N'2020-01-23 15:02:16.740' AS DateTime), 1, CAST(N'2020-01-23 15:02:16.740' AS DateTime), 1)
GO
INSERT [dbo].[LogisticSponsors] ([Id], [Uid], [Name], [IsAirfareTicketRequired], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'421c6c70-ef6e-4071-b00f-7c4c222aa410', N'Próprio | Own', 1, 0, CAST(N'2020-01-23 15:02:16.750' AS DateTime), 1, CAST(N'2020-01-23 15:02:16.750' AS DateTime), 1)
GO
INSERT [dbo].[LogisticSponsors] ([Id], [Uid], [Name], [IsAirfareTicketRequired], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'37030831-5d54-4227-a050-820a32a0ad95', N'Patrocinado por outras instituições | Sponsored by other institutions', 1, 0, CAST(N'2020-01-23 15:02:16.753' AS DateTime), 1, CAST(N'2020-01-23 15:02:16.753' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[LogisticSponsors] OFF
GO

SET IDENTITY_INSERT [dbo].[AttendeeLogisticSponsors] ON 
GO
INSERT [dbo].[AttendeeLogisticSponsors] ([Id], [Uid], [EditionId], [LogisticSponsorId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'616a9d09-9aa7-47a8-959c-7d14ac897a3f', 2, 1, 0, CAST(N'2020-01-23 15:02:16.757' AS DateTime), 1, CAST(N'2020-01-23 15:02:16.757' AS DateTime), 1)
GO
INSERT [dbo].[AttendeeLogisticSponsors] ([Id], [Uid], [EditionId], [LogisticSponsorId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'4d1583a0-9fb0-47ac-b159-b77530930f0b', 2, 2, 0, CAST(N'2020-01-23 15:02:16.760' AS DateTime), 1, CAST(N'2020-01-23 15:02:16.760' AS DateTime), 1)
GO
INSERT [dbo].[AttendeeLogisticSponsors] ([Id], [Uid], [EditionId], [LogisticSponsorId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'dfe407a2-8e7a-45d5-9bcd-1a6be011b0ae', 2, 3, 0, CAST(N'2020-01-23 15:02:16.763' AS DateTime), 1, CAST(N'2020-01-23 15:02:16.763' AS DateTime), 1)
GO
INSERT [dbo].[AttendeeLogisticSponsors] ([Id], [Uid], [EditionId], [LogisticSponsorId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'ab120dd7-2868-4ca5-a0eb-432810d3ddee', 2, 4, 0, CAST(N'2020-01-23 15:02:16.763' AS DateTime), 1, CAST(N'2020-01-23 15:02:16.763' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[AttendeeLogisticSponsors] OFF
GO