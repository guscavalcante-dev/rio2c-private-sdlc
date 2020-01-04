--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

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
INSERT [dbo].[HorizontalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'b6322dd2-997c-437c-8485-a9600d0a5979', N'Arte, Mídia e Entretenimento', 1, 0, CAST(N'2020-01-04 08:39:41.977' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.977' AS DateTime), 1)
GO
INSERT [dbo].[HorizontalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'b6e2ed47-553a-4f92-84cf-b505148ea084', N'Smart Communities e Comunidades colaborativas', 2, 0, CAST(N'2020-01-04 08:39:41.977' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.977' AS DateTime), 1)
GO
INSERT [dbo].[HorizontalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'fe555e57-0396-4e64-bccd-e85b6ee28a05', N'Novo consumo e Tendências', 3, 0, CAST(N'2020-01-04 08:39:41.980' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.980' AS DateTime), 1)
GO
INSERT [dbo].[HorizontalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'b098b966-2f41-499b-b867-f0783a4bb901', N'O futuro da sociedade e impacto socioambiental', 4, 0, CAST(N'2020-01-04 08:39:41.983' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.983' AS DateTime), 1)
GO
INSERT [dbo].[HorizontalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'244b01ec-bb26-4b42-a3d9-7faf54965c92', N'Educação e Qualificação profissional', 5, 0, CAST(N'2020-01-04 08:39:41.983' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.983' AS DateTime), 1)
GO
INSERT [dbo].[HorizontalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'1517e35c-d6f1-41e7-89c8-cb8bcf151007', N'Saúde, Alimentação e Bem-estar', 6, 0, CAST(N'2020-01-04 08:39:41.987' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.987' AS DateTime), 1)
GO
INSERT [dbo].[HorizontalTracks] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (7, N'aeea9445-c123-425f-b16a-0d6f7bcfcd91', N'Transformação das empresas e empregos', 7, 0, CAST(N'2020-01-04 08:39:41.987' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.987' AS DateTime), 1)
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
INSERT [dbo].[EditionEvents] ([Id], [Uid], [EditionId], [Name], [StartDate], [EndDate], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'd1828ff4-86cb-43de-82af-1fa38501d214', 2, N'Festvália', CAST(N'2020-05-09 00:00:00.000' AS DateTime), CAST(N'2020-05-10 23:59:59.000' AS DateTime), 0, CAST(N'2020-01-04 12:39:30.000' AS DateTime), 1, CAST(N'2020-01-04 12:39:30.000' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[EditionEvents] OFF
GO