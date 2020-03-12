--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Conferences"
DROP CONSTRAINT "FK_Editions_Conferences_EditionId"
go

ALTER TABLE "dbo"."Conferences"
DROP CONSTRAINT "FK_Rooms_Conferences_RoomId"
go

ALTER TABLE "dbo"."Conferences"
DROP CONSTRAINT "FK_Users_Conferences_CreateUserId"
go

ALTER TABLE "dbo"."Conferences"
DROP CONSTRAINT "FK_Users_Conferences_UpdateUserId"
go

ALTER TABLE "dbo"."ConferenceTitles"
DROP CONSTRAINT "FK_Conferences_ConferenceTitles_ConferenceId"
go

ALTER TABLE "dbo"."ConferenceSynopsis"
DROP CONSTRAINT "FK_Conferences_ConferenceSynopsis_ConferenceId"
go

ALTER TABLE "dbo"."ConferenceParticipants"
DROP CONSTRAINT "FK_Conferences_ConferenceParticipants_ConferenceId"
go

ALTER TABLE "dbo".Conferences
DROP CONSTRAINT "PK_Conferences"
go

DROP TABLE "dbo".Conferences
go

CREATE TABLE "HorizontalTracks"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(500)  NOT NULL ,
	"DisplayOrder"       int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_HorizontalTracks_Name" ON "HorizontalTracks"
( 
	"Name"                ASC
)
go

CREATE TABLE "VerticalTracks"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(500)  NOT NULL ,
	"DisplayOrder"       int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_VerticalTracks_Name" ON "VerticalTracks"
( 
	"Name"                ASC
)
go

CREATE TABLE "ConferenceVerticalTracks"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"ConferenceId"       int  NOT NULL ,
	"VerticalTrackId"    int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_ConferenceVerticalTracks_ConferenceId" ON "ConferenceVerticalTracks"
( 
	"ConferenceId"        ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_ConferenceVerticalTracks_VerticalTrackId" ON "ConferenceVerticalTracks"
( 
	"VerticalTrackId"     ASC
)
go

CREATE TABLE "ConferenceHorizontalTracks"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"ConferenceId"       int  NOT NULL ,
	"HorizontalTrackId"  int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_ConferenceHorizontalTracks_ConferenceId" ON "ConferenceHorizontalTracks"
( 
	"ConferenceId"        ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_ConferenceHorizontalTracks_HorizontalTrackId" ON "ConferenceHorizontalTracks"
( 
	"HorizontalTrackId"   ASC
)
go

CREATE TABLE "dbo"."Conferences"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"EditionEventId"     int  NOT NULL ,
	"RoomId"             int  NULL ,
	"StartDate"          datetime  NOT NULL ,
	"EndDate"            datetime  NOT NULL ,
	"Info"               varchar(3000)  NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_Conferences_EditionEventId" ON "dbo"."Conferences"
( 
	"EditionEventId"      ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_Conferences_RoomId" ON "dbo"."Conferences"
( 
	"RoomId"              ASC
)
go

CREATE TABLE "EditionEvents"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"EditionId"          int  NOT NULL ,
	"Name"               varchar(200)  NOT NULL ,
	"StartDate"          datetime  NOT NULL ,
	"EndDate"            datetime  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_EditionEvents_EditionId" ON "EditionEvents"
( 
	"EditionId"           ASC
)
go

ALTER TABLE "HorizontalTracks"
ADD CONSTRAINT "PK_HorizontalTracks" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "HorizontalTracks"
ADD CONSTRAINT "IDX_UQ_HorizontalTracks_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "VerticalTracks"
ADD CONSTRAINT "PK_VerticalTracks" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "VerticalTracks"
ADD CONSTRAINT "IDX_UQ_VerticalTracks_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "ConferenceVerticalTracks"
ADD CONSTRAINT "PK_ConferenceVerticalTracks" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "ConferenceVerticalTracks"
ADD CONSTRAINT "IDX_UQ_ConferenceVerticalTracks_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "ConferenceHorizontalTracks"
ADD CONSTRAINT "PK_ConferenceHorizontalTracks" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "ConferenceHorizontalTracks"
ADD CONSTRAINT "IDX_UQ_ConferenceHorizontalTracks_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "dbo"."Conferences"
ADD CONSTRAINT "PK_Conferences" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "EditionEvents"
ADD CONSTRAINT "PK_EditionEvents" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "EditionEvents"
ADD CONSTRAINT "IDX_UQ_EditionEvents_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "HorizontalTracks"
	ADD CONSTRAINT "FK_Users_HorizontalTracks_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "HorizontalTracks"
	ADD CONSTRAINT "FK_Users_HorizontalTracks_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "VerticalTracks"
	ADD CONSTRAINT "FK_Users_VerticalTracks_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "VerticalTracks"
	ADD CONSTRAINT "FK_Users_VerticalTracks_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ConferenceVerticalTracks"
	ADD CONSTRAINT "FK_Conferences_ConferenceVerticalTracks_ConferenceId" FOREIGN KEY ("ConferenceId") REFERENCES "dbo"."Conferences"("Id")
go

ALTER TABLE "ConferenceVerticalTracks"
	ADD CONSTRAINT "FK_VerticalTracks_ConferenceVerticalTracks_VerticalTrackId" FOREIGN KEY ("VerticalTrackId") REFERENCES "VerticalTracks"("Id")
go

ALTER TABLE "ConferenceVerticalTracks"
	ADD CONSTRAINT "FK_Users_ConferenceVerticalTracks_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ConferenceVerticalTracks"
	ADD CONSTRAINT "FK_Users_ConferenceVerticalTracks_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ConferenceHorizontalTracks"
	ADD CONSTRAINT "FK_Conferences_ConferenceHorizontalTracks_ConferenceId" FOREIGN KEY ("ConferenceId") REFERENCES "dbo"."Conferences"("Id")
go

ALTER TABLE "ConferenceHorizontalTracks"
	ADD CONSTRAINT "FK_HorizontalTracks_ConferenceHorizontalTracks_HorizontalTrackId" FOREIGN KEY ("HorizontalTrackId") REFERENCES "HorizontalTracks"("Id")
go

ALTER TABLE "ConferenceHorizontalTracks"
	ADD CONSTRAINT "FK_Users_ConferenceHorizontalTracks_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ConferenceHorizontalTracks"
	ADD CONSTRAINT "FK_Users_ConferenceHorizontalTracks_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "dbo"."Conferences"
	ADD CONSTRAINT "FK_Rooms_Conferences_RoomId" FOREIGN KEY ("RoomId") REFERENCES "dbo"."Rooms"("Id")
go

ALTER TABLE "dbo"."Conferences"
	ADD CONSTRAINT "FK_Users_Conferences_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "dbo"."Conferences"
	ADD CONSTRAINT "FK_Users_Conferences_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "dbo"."Conferences"
	ADD CONSTRAINT "FK_EditionEvents_Conferences_EditionEventId" FOREIGN KEY ("EditionEventId") REFERENCES "EditionEvents"("Id")
go

ALTER TABLE "dbo"."ConferenceTitles"
	ADD CONSTRAINT "FK_Conferences_ConferenceTitles_ConferenceId" FOREIGN KEY ("ConferenceId") REFERENCES "dbo"."Conferences"("Id")
go

ALTER TABLE "dbo"."ConferenceSynopsis"
	ADD CONSTRAINT "FK_Conferences_ConferenceSynopsis_ConferenceId" FOREIGN KEY ("ConferenceId") REFERENCES "dbo"."Conferences"("Id")
go

ALTER TABLE "dbo"."ConferenceParticipants"
	ADD CONSTRAINT "FK_Conferences_ConferenceParticipants_ConferenceId" FOREIGN KEY ("ConferenceId") REFERENCES "dbo"."Conferences"("Id")
go

ALTER TABLE "EditionEvents"
	ADD CONSTRAINT "FK_Languages_EditionEvents_EditionId" FOREIGN KEY ("EditionId") REFERENCES "dbo"."Languages"("Id")
go

ALTER TABLE "EditionEvents"
	ADD CONSTRAINT "FK_Users_EditionEvents_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "EditionEvents"
	ADD CONSTRAINT "FK_Users_EditionEvents_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "dbo"."ConferenceTitles"
	ADD CONSTRAINT "IDX_UQ_ConferenceTitles_ConferenceId_LanguageId" UNIQUE ("ConferenceId"  ASC,"LanguageId"  ASC)
go

ALTER TABLE "dbo"."ConferenceSynopsis"
	ADD CONSTRAINT "IDX_UQ_ConferenceSynopsis_ConferenceId_LanguageId" UNIQUE ("ConferenceId"  ASC,"LanguageId"  ASC)
go


SET IDENTITY_INSERT [dbo].[VerticalTracks] ON 

GO
INSERT [dbo].[VerticalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'761529f9-f3f7-476e-bd2c-a1ee908a59e5', N'Audiovisual | Audiovisual', 1, 0, CAST(N'2020-01-04 08:39:41.960' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.960' AS DateTime), 1)
GO
INSERT [dbo].[VerticalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'24417536-5c46-4d8c-90df-71a96d1797bc', N'Música | Music', 2, 0, CAST(N'2020-01-04 08:39:41.963' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.963' AS DateTime), 1)
GO
INSERT [dbo].[VerticalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'21b2cc48-60b9-415b-8ae6-2c9b9d81fd35', N'Inovação | Innovation', 3, 0, CAST(N'2020-01-04 08:39:41.967' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.967' AS DateTime), 1)
GO
INSERT [dbo].[VerticalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'41f51907-0a02-448b-98aa-42065a5b475a', N'Marcas | Brands', 4, 0, CAST(N'2020-01-04 08:39:41.967' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.967' AS DateTime), 1)
GO
INSERT [dbo].[VerticalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'c63da8e5-3a1c-460e-abb4-fd5649fad4ba', N'Cérebro | Brain', 5, 0, CAST(N'2020-01-04 08:39:41.970' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.970' AS DateTime), 1)
GO
INSERT [dbo].[VerticalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'd09aaaaf-d3e8-4170-86a9-103e139ca4c3', N'Educação | Education', 6, 0, CAST(N'2020-01-04 08:39:41.970' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.970' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[VerticalTracks] OFF
GO
SET IDENTITY_INSERT [dbo].[HorizontalTracks] ON 

GO
INSERT [dbo].[HorizontalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'b6322dd2-997c-437c-8485-a9600d0a5979', N'Arte, Mídia e Entretenimento | Art, Media & Entertainment', 1, 0, CAST(N'2020-01-04 08:39:41.977' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.977' AS DateTime), 1)
GO
INSERT [dbo].[HorizontalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'b6e2ed47-553a-4f92-84cf-b505148ea084', N'Comunidades colaborativas | Smart Communities', 2, 0, CAST(N'2020-01-04 08:39:41.977' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.977' AS DateTime), 1)
GO
INSERT [dbo].[HorizontalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'fe555e57-0396-4e64-bccd-e85b6ee28a05', N'Novo consumo e Tendências | New Consumers & Trends', 3, 0, CAST(N'2020-01-04 08:39:41.980' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.980' AS DateTime), 1)
GO
INSERT [dbo].[HorizontalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'b098b966-2f41-499b-b867-f0783a4bb901', N'O futuro da sociedade e impacto socioambiental | Cities of the future and the future of society', 4, 0, CAST(N'2020-01-04 08:39:41.983' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.983' AS DateTime), 1)
GO
INSERT [dbo].[HorizontalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'244b01ec-bb26-4b42-a3d9-7faf54965c92', N'Educação e Qualificação profissional | Education & Professional Capacity Building', 5, 0, CAST(N'2020-01-04 08:39:41.983' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.983' AS DateTime), 1)
GO
INSERT [dbo].[HorizontalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'1517e35c-d6f1-41e7-89c8-cb8bcf151007', N'Saúde, Alimentação e Bem-estar | Health, Food & Well-being', 6, 0, CAST(N'2020-01-04 08:39:41.987' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.987' AS DateTime), 1)
GO
INSERT [dbo].[HorizontalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (7, N'aeea9445-c123-425f-b16a-0d6f7bcfcd91', N'Transformação das empresas e empregos | Transformations for Enterprises & Employment', 7, 0, CAST(N'2020-01-04 08:39:41.987' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.987' AS DateTime), 1)
GO
INSERT [dbo].[HorizontalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (8, N'32a90fdb-8362-41ba-a24f-a797e4c1e6ad', N'Energia & Impacto socio-ambiental | Energy & Socio-environmental impact', 8, 0, CAST(N'2020-01-04 08:39:41.987' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.987' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[HorizontalTracks] OFF
GO
SET IDENTITY_INSERT [dbo].[EditionEvents] ON 

GO
INSERT [dbo].[EditionEvents] ([Id], [Uid], [EditionId], [Name], [StartDate], [EndDate], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'e7a60b6a-5131-4337-9190-fbc2acb0b032', 2, N'Summit', CAST(N'2020-05-05 00:00:00.000' AS DateTime), CAST(N'2020-05-05 23:59:59.000' AS DateTime), 0, CAST(N'2020-01-04 12:39:30.000' AS DateTime), 1, CAST(N'2020-01-04 12:39:30.000' AS DateTime), 1)
GO
INSERT [dbo].[EditionEvents] ([Id], [Uid], [EditionId], [Name], [StartDate], [EndDate], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'dd0e6004-dc73-4f1c-a886-07e58b806dd3', 2, N'Conferência', CAST(N'2020-05-06 00:00:00.000' AS DateTime), CAST(N'2020-05-08 23:59:59.000' AS DateTime), 0, CAST(N'2020-01-04 12:39:30.000' AS DateTime), 1, CAST(N'2020-01-04 12:39:30.000' AS DateTime), 1)
GO
INSERT [dbo].[EditionEvents] ([Id], [Uid], [EditionId], [Name], [StartDate], [EndDate], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'9c3bbba8-5128-4b01-b2c9-e9f14273a059', 2, N'Mercado', CAST(N'2020-05-05 00:00:00.000' AS DateTime), CAST(N'2020-05-08 23:59:59.000' AS DateTime), 0, CAST(N'2020-01-04 12:39:30.000' AS DateTime), 1, CAST(N'2020-01-04 12:39:30.000' AS DateTime), 1)
GO
INSERT [dbo].[EditionEvents] ([Id], [Uid], [EditionId], [Name], [StartDate], [EndDate], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'd1828ff4-86cb-43de-82af-1fa38501d214', 2, N'Festvalia®', CAST(N'2020-05-09 00:00:00.000' AS DateTime), CAST(N'2020-05-10 23:59:59.000' AS DateTime), 0, CAST(N'2020-01-04 12:39:30.000' AS DateTime), 1, CAST(N'2020-01-04 12:39:30.000' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[EditionEvents] OFF
GO

SET IDENTITY_INSERT [dbo].[Rooms] ON 
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (23, N'05966836-05d8-43bb-b6d7-07ff821a79d0', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (24, N'9028ae4f-cf02-4d26-b5df-1ad5f7435815', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (25, N'51d82768-676d-4359-82da-e2976cfc5b14', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (26, N'5ec2ed97-d371-4293-925e-95666d8322d5', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (27, N'f78e3a5e-8e69-4d5d-b7e7-f67c2efa0f1b', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (28, N'53f0283d-aca7-4a27-9956-1de7442cb247', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (29, N'e78bb9e4-a79e-422b-b4a9-a76f44e41a93', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (30, N'cba1ae4d-40b5-4499-bd13-d2dd2e318931', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (31, N'06a27b8e-cf49-43e5-a616-58fea1ba9ff2', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (32, N'9ac5bbb4-122a-4750-8523-af4f8c754484', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (33, N'7cbf77ba-44fd-4374-8e01-3bbf79e0e2d3', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (34, N'027152d9-426d-46a0-8412-daf0481c85f9', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (35, N'b45143ca-28a0-45db-b8c2-7125bf08608b', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (36, N'51dea3fc-b68d-40ba-aa6b-ac022763819a', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (37, N'4c78b744-cd51-4bfa-a333-fd1fbe60944e', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (38, N'6cb72337-5537-4d89-967d-fce04459d571', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (39, N'3c5fb334-f551-4bea-91b9-80fa0930e62d', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (40, N'c311f2eb-78f5-4fd3-99d2-9d268a03f579', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (41, N'cdc9ebae-0ccc-4bb3-9b0f-b404185c287c', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (42, N'53b56368-9bef-4719-b89e-1fabadbefb8f', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (43, N'6a02d5dd-32b3-42a1-8d56-fd36f1de93ed', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[Rooms] ([Id], [Uid], [EditionId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (44, N'c30c63ea-05b5-4b61-a08f-35e43963176e', 2, 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Rooms] OFF
GO

SET IDENTITY_INSERT [dbo].[RoomNames] ON 
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (45, N'cc520447-e1f0-4aec-bbab-f87d137a9997', 23, 1, N'BrainSpace®', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (46, N'bedf26d0-84d9-4923-82b9-bea4f230c2b4', 23, 2, N'BrainSpace®', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (47, N'f8edc6fd-0828-4fb9-97e7-60ccd6ee3f08', 24, 1, N'Brazilian Content', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (48, N'ff50245d-38d3-453f-9aeb-f3270123254e', 24, 2, N'Brazilian Content', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (49, N'74cdfc74-6468-4e91-a238-633f44fac449', 25, 1, N'House of Brands', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (50, N'44b0d5ff-3f44-45f4-bfc9-06df121753a6', 25, 2, N'Casa das Marcas', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (51, N'5ff60156-b5af-4665-8ec9-358944916f8f', 26, 1, N'Experience Room', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (52, N'75a3a4ad-7df5-44fc-b55c-798753a1c932', 26, 2, N'Experience Room', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (53, N'49a673d3-54c8-42ce-987e-51a3844a869f', 27, 1, N'Grand Salon RIO2C', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (54, N'c7152234-717c-4961-8d4c-518161a58ee5', 27, 2, N'Grande Sala RIO2C', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (55, N'1e2dfa78-77ed-431c-b02a-d6847ca561db', 28, 1, N'Audiovisual Lounge', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (56, N'bc65e402-e107-4e9d-acc0-38e6d31fd353', 28, 2, N'Lounge Audiovisual', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (57, N'07f33d03-2193-4f38-bd9b-6dc0b5c3c725', 29, 1, N'Music Lounge', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (58, N'7af37d13-8ec1-4993-a3d8-bf895506e464', 29, 2, N'Lounge da Música', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (59, N'c058b88e-6a28-4a5a-b407-882a8edc8211', 30, 1, N'Eletroacustic Stage', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (60, N'f32e9490-2f09-4aaa-98a0-5a9568e35440', 30, 2, N'Palco Eletroacústico', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (61, N'78dd9393-ae61-4785-b12d-ef3d96835684', 31, 1, N'Music Stage', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (62, N'f1ac0616-1424-4fdf-82ef-7454f2a36b7c', 31, 2, N'Palco Música', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (63, N'be424950-7495-43d6-94f6-7fa3fdb20a51', 32, 1, N'PitchingShow®', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (64, N'4cf556f2-9e0d-4d17-a129-b9fd08ec24c6', 32, 2, N'PitchingShow®', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (65, N'626581b6-1c20-4fc3-ae99-7960b3a9f870', 33, 1, N'Audiovisual Room', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (66, N'28bf7577-868d-479d-8a51-1922b7191dd4', 33, 2, N'Sala de Audiovisual', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (67, N'dffd0ee1-eb24-4247-b45f-348c866f31a6', 34, 1, N'Innovation Room', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (68, N'1a341126-e785-4795-8115-fd5329073bb4', 34, 2, N'Sala de Inovação', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (69, N'2f4b0989-5edd-4156-9c57-a209606a2f41', 35, 1, N'Innovation Room 2', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (70, N'031c5572-8593-4ea8-a125-7d32d8db74b9', 35, 2, N'Sala de Inovação 2', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (71, N'f57a6731-08c8-4ff6-a5b8-128d38956e9e', 36, 1, N'Innovation Room 3', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (72, N'5e9a9fb5-f5a2-4924-a7ef-cc53c3c7b6f4', 36, 2, N'Sala de Inovação 3', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (73, N'79242f44-1ac5-4d8b-8e7d-dde88cc659f7', 37, 1, N'Innovation Room 4', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (74, N'a647ffa2-dc20-451a-8a6d-c944d7c61d0e', 37, 2, N'Sala de Inovação 4', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (75, N'5b542a52-9d0f-45ca-a5e5-13a2b8ef6da9', 38, 1, N'Music Room', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (76, N'7c307460-e909-4d0d-9722-9fd67f8cc6db', 38, 2, N'Sala de Música', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (77, N'7023b572-2569-42df-8f2d-b8ae2387f961', 39, 1, N'Audiovisual Pitching Room', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (78, N'2eaa1b54-1815-40bd-8061-c6ac7651c1bd', 39, 2, N'Sala Pitching de Audiovisual', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (79, N'd76c581e-dc76-4e16-8e35-9a1edaf5ef6e', 40, 1, N'StoryVillage', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (80, N'a28c861d-06f9-4262-8694-991843406a99', 40, 2, N'StoryVillage', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (81, N'51d42efd-7df4-4421-b01f-e97d379914d8', 41, 1, N'Summit Educa', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (82, N'25fb45c8-c53f-4f4f-b2a0-f1196b1f2cfc', 41, 2, N'Summit Educa', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (83, N'2c712b95-6b5a-4f64-b09d-78adade14dd0', 42, 1, N'Branded Content Summit by Meio & Mensagem', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (84, N'7fa99bb2-b577-4909-89a3-be1fb010eba2', 42, 2, N'Summit Marcas e Conteúdo by Meio & Mensagem', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (85, N'0967180d-036d-4748-8182-421bb65bfbbb', 43, 1, N'Chamber Theater PETROBRAS', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (86, N'4745235f-3502-4ca7-ab1f-51c11d9a84ad', 43, 2, N'Teatro de Câmara', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (87, N'146078af-b1ea-447e-ac84-05b1a35cb5e1', 44, 1, N'XR Game Stage', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
INSERT [dbo].[RoomNames] ([Id], [Uid], [RoomId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (88, N'eb184fa7-ca71-4ba3-94de-67fa41706f36', 44, 2, N'XR Game Stage', 0, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1, CAST(N'2020-01-05 08:30:41.320' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[RoomNames] OFF
GO

DELETE FROM [dbo].[ConferenceParticipantRoleTitles]
go
DELETE FROM [dbo].[ConferenceParticipantRoles]
go

SET IDENTITY_INSERT [dbo].[ConferenceParticipantRoles] ON 

GO
INSERT [dbo].[ConferenceParticipantRoles] ([Id], [Uid], [Name], [IsLecturer], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'36457f36-14d4-47f3-b058-1b3d11ed2ee7', N'Interviewer', 0, 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
INSERT [dbo].[ConferenceParticipantRoles] ([Id], [Uid], [Name], [IsLecturer], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'ccfcc04a-70de-4d37-b7d2-fe82d4838874', N'Interference', 0, 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
INSERT [dbo].[ConferenceParticipantRoles] ([Id], [Uid], [Name], [IsLecturer], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'c7f9e23a-3132-4b7f-9648-7a19879e3490', N'Keynote', 0, 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
INSERT [dbo].[ConferenceParticipantRoles] ([Id], [Uid], [Name], [IsLecturer], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'83eeb82d-fd18-4f47-b3da-204f35722a5d', N'Modarator (M)', 0, 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
INSERT [dbo].[ConferenceParticipantRoles] ([Id], [Uid], [Name], [IsLecturer], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'0511a35e-f18c-40a7-b7c5-11147405bcb7', N'Moderator (F)', 0, 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
INSERT [dbo].[ConferenceParticipantRoles] ([Id], [Uid], [Name], [IsLecturer], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'fac6b500-39c0-44fe-a214-4135d10bcd99', N'Speaker', 1, 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[ConferenceParticipantRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[ConferenceParticipantRoleTitles] ON 

GO
INSERT [dbo].[ConferenceParticipantRoleTitles] ([Id], [Uid], [ConferenceParticipantRoleId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'8a8ab20b-5030-4a49-90f4-d12d483f3773', 1, 1, N'Interviewer', 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
INSERT [dbo].[ConferenceParticipantRoleTitles] ([Id], [Uid], [ConferenceParticipantRoleId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'f293dd10-7501-4abd-87a2-8333e3caefb1', 1, 2, N'Entrevistador', 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
INSERT [dbo].[ConferenceParticipantRoleTitles] ([Id], [Uid], [ConferenceParticipantRoleId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'd7496a45-f5f4-468f-a057-a8e8a34807a0', 2, 1, N'Interference', 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
INSERT [dbo].[ConferenceParticipantRoleTitles] ([Id], [Uid], [ConferenceParticipantRoleId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'37181f22-7d03-417f-bc0f-0a319264f41c', 2, 2, N'Interferência', 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
INSERT [dbo].[ConferenceParticipantRoleTitles] ([Id], [Uid], [ConferenceParticipantRoleId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'1bc1cb1d-4633-4b71-b9f1-a5816c7b5616', 3, 1, N'Keynote', 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
INSERT [dbo].[ConferenceParticipantRoleTitles] ([Id], [Uid], [ConferenceParticipantRoleId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'52b2c030-3630-45f8-b1d9-2e7158717667', 3, 2, N'Keynote', 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
INSERT [dbo].[ConferenceParticipantRoleTitles] ([Id], [Uid], [ConferenceParticipantRoleId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (7, N'17ff3431-eabc-4cc7-ac08-ca22e5fc3ff0', 4, 1, N'Moderator', 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
INSERT [dbo].[ConferenceParticipantRoleTitles] ([Id], [Uid], [ConferenceParticipantRoleId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (8, N'75f51a7e-b9d5-40c7-a484-d5563a21f0e1', 4, 2, N'Moderador', 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
INSERT [dbo].[ConferenceParticipantRoleTitles] ([Id], [Uid], [ConferenceParticipantRoleId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (9, N'0248fdd5-3d52-400d-9920-a906a68c1c1e', 5, 1, N'Moderator', 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
INSERT [dbo].[ConferenceParticipantRoleTitles] ([Id], [Uid], [ConferenceParticipantRoleId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (10, N'97675274-e706-43fc-9af9-afcd17391129', 5, 2, N'Moderadora', 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
INSERT [dbo].[ConferenceParticipantRoleTitles] ([Id], [Uid], [ConferenceParticipantRoleId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (11, N'67cbd280-5d4d-4914-ba16-2441cde4b4c9', 6, 1, N'Speaker', 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
INSERT [dbo].[ConferenceParticipantRoleTitles] ([Id], [Uid], [ConferenceParticipantRoleId], [LanguageId], [Value], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (12, N'd71e70a1-94fc-4544-9579-5fd98162e092', 6, 2, N'Palestrante', 0, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1, CAST(N'2020-01-05 11:25:31.000' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[ConferenceParticipantRoleTitles] OFF
GO
