--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "HorizontalTracks"
DROP CONSTRAINT "FK_Users_HorizontalTracks_CreateUserId"
go

ALTER TABLE "HorizontalTracks"
DROP CONSTRAINT "FK_Users_HorizontalTracks_UpdateUserId"
go

ALTER TABLE "VerticalTracks"
DROP CONSTRAINT "FK_Users_VerticalTracks_CreateUserId"
go

ALTER TABLE "VerticalTracks"
DROP CONSTRAINT "FK_Users_VerticalTracks_UpdateUserId"
go

ALTER TABLE "ConferenceVerticalTracks"
DROP CONSTRAINT "FK_Conferences_ConferenceVerticalTracks_ConferenceId"
go

ALTER TABLE "ConferenceVerticalTracks"
DROP CONSTRAINT "FK_VerticalTracks_ConferenceVerticalTracks_VerticalTrackId"
go

ALTER TABLE "ConferenceVerticalTracks"
DROP CONSTRAINT "FK_Users_ConferenceVerticalTracks_CreateUserId"
go

ALTER TABLE "ConferenceVerticalTracks"
DROP CONSTRAINT "FK_Users_ConferenceVerticalTracks_UpdateUserId"
go

ALTER TABLE "ConferenceHorizontalTracks"
DROP CONSTRAINT "FK_Conferences_ConferenceHorizontalTracks_ConferenceId"
go

ALTER TABLE "ConferenceHorizontalTracks"
DROP CONSTRAINT "FK_HorizontalTracks_ConferenceHorizontalTracks_HorizontalTrackId"
go

ALTER TABLE "ConferenceHorizontalTracks"
DROP CONSTRAINT "FK_Users_ConferenceHorizontalTracks_CreateUserId"
go

ALTER TABLE "ConferenceHorizontalTracks"
DROP CONSTRAINT "FK_Users_ConferenceHorizontalTracks_UpdateUserId"
go

ALTER TABLE HorizontalTracks
DROP CONSTRAINT "PK_HorizontalTracks"
go

ALTER TABLE "HorizontalTracks"
DROP CONSTRAINT "IDX_UQ_HorizontalTracks_Uid"
go

ALTER TABLE VerticalTracks
DROP CONSTRAINT "PK_VerticalTracks"
go

ALTER TABLE "VerticalTracks"
DROP CONSTRAINT "IDX_UQ_VerticalTracks_Uid"
go

ALTER TABLE ConferenceVerticalTracks
DROP CONSTRAINT "PK_ConferenceVerticalTracks"
go

ALTER TABLE "ConferenceVerticalTracks"
DROP CONSTRAINT "IDX_UQ_ConferenceVerticalTracks_Uid"
go

ALTER TABLE ConferenceHorizontalTracks
DROP CONSTRAINT "PK_ConferenceHorizontalTracks"
go

ALTER TABLE "ConferenceHorizontalTracks"
DROP CONSTRAINT "IDX_UQ_ConferenceHorizontalTracks_Uid"
go

DROP TABLE "ConferenceVerticalTracks"
go

DROP TABLE "ConferenceHorizontalTracks"
go

DROP TABLE "HorizontalTracks"
go

DROP TABLE "VerticalTracks"
go

CREATE TABLE "Tracks"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"EditionId"          int  NOT NULL ,
	"Name"               varchar(600)  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_Tracks_EditionId" ON "Tracks"
( 
	"EditionId"           ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_Tracks_Name" ON "Tracks"
( 
	"Name"                ASC
)
go

CREATE TABLE "ConferenceTracks"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"ConferenceId"       int  NOT NULL ,
	"TrackId"            int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_ConferenceTracks_TrackId" ON "ConferenceTracks"
( 
	"TrackId"             ASC
)
go

CREATE TABLE "PresentationFormats"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"EditionId"          int  NOT NULL ,
	"Name"               varchar(600)  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_PresentationFormats_EditionId" ON "PresentationFormats"
( 
	"EditionId"           ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_PresentationFormats_Name" ON "PresentationFormats"
( 
	"Name"                ASC
)
go

CREATE TABLE "ConferencePresentationFormats"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"ConferenceId"       int  NOT NULL ,
	"PresentationFormatId" int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_ConferencePresentationFormats_PresentationFormatId" ON "ConferencePresentationFormats"
( 
	"PresentationFormatId"  ASC
)
go

ALTER TABLE "Tracks"
ADD CONSTRAINT "PK_Tracks" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "Tracks"
ADD CONSTRAINT "IDX_UQ_Tracks_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "ConferenceTracks"
ADD CONSTRAINT "PK_ConferenceTracks" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "ConferenceTracks"
ADD CONSTRAINT "IDX_UQ_ConferenceTracks_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "ConferenceTracks"
ADD CONSTRAINT "IDX_UQ_ConferenceTracks_ConferenceId_TrackId" UNIQUE ("ConferenceId"  ASC,"TrackId"  ASC)
go

ALTER TABLE "PresentationFormats"
ADD CONSTRAINT "PK_PresentationFormats" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "PresentationFormats"
ADD CONSTRAINT "IDX_UQ_PresentationFormats_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "ConferencePresentationFormats"
ADD CONSTRAINT "PK_ConferencePresentationFormats" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "ConferencePresentationFormats"
ADD CONSTRAINT "IDX_UQ_ConferencePresentationFormats_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "ConferencePresentationFormats"
ADD CONSTRAINT "IDX_UQ_ConferencePresentationFormats_ConferenceId_PresentationFormatId" UNIQUE ("ConferenceId"  ASC,"PresentationFormatId"  ASC)
go

ALTER TABLE "Tracks"
	ADD CONSTRAINT "FK_Editions_Tracks_EditionId" FOREIGN KEY ("EditionId") REFERENCES "dbo"."Editions"("Id")
go

ALTER TABLE "Tracks"
	ADD CONSTRAINT "FK_Users_Tracks_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "Tracks"
	ADD CONSTRAINT "FK_Users_Tracks_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ConferenceTracks"
	ADD CONSTRAINT "FK_Conferences_ConferenceTracks_ConferenceId" FOREIGN KEY ("ConferenceId") REFERENCES "dbo"."Conferences"("Id")
go

ALTER TABLE "ConferenceTracks"
	ADD CONSTRAINT "FK_Tracks_ConferenceTracks_TrackId" FOREIGN KEY ("TrackId") REFERENCES "Tracks"("Id")
go

ALTER TABLE "ConferenceTracks"
	ADD CONSTRAINT "FK_Users_ConferenceTracks_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ConferenceTracks"
	ADD CONSTRAINT "FK_Users_ConferenceTracks_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "PresentationFormats"
	ADD CONSTRAINT "FK_Editions_PresentationFormats_EditionId" FOREIGN KEY ("EditionId") REFERENCES "dbo"."Editions"("Id")
go

ALTER TABLE "PresentationFormats"
	ADD CONSTRAINT "FK_Users_PresentationFormats_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "PresentationFormats"
	ADD CONSTRAINT "FK_Users_PresentationFormats_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ConferencePresentationFormats"
	ADD CONSTRAINT "FK_Conferences_ConferencePresentationFormats_ConferenceId" FOREIGN KEY ("ConferenceId") REFERENCES "dbo"."Conferences"("Id")
go

ALTER TABLE "ConferencePresentationFormats"
	ADD CONSTRAINT "FK_PresentationFormats_ConferencePresentationFormats_PresentationFormatId" FOREIGN KEY ("PresentationFormatId") REFERENCES "PresentationFormats"("Id")
go

ALTER TABLE "ConferencePresentationFormats"
	ADD CONSTRAINT "FK_Users_ConferencePresentationFormats_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ConferencePresentationFormats"
	ADD CONSTRAINT "FK_Users_ConferencePresentationFormats_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go


SET IDENTITY_INSERT [dbo].[Tracks] ON 

GO
INSERT [dbo].[Tracks] ([Id], [Uid], [EditionId], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'761529f9-f3f7-476e-bd2c-a1ee908a59e5', 2, N'Audiovisual | Audiovisual', 0, CAST(N'2020-01-04 08:39:41.960' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.960' AS DateTime), 1)
GO
INSERT [dbo].[Tracks] ([Id], [Uid], [EditionId], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'24417536-5c46-4d8c-90df-71a96d1797bc', 2, N'Música | Music', 0, CAST(N'2020-01-04 08:39:41.963' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.963' AS DateTime), 1)
GO
INSERT [dbo].[Tracks] ([Id], [Uid], [EditionId], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'21b2cc48-60b9-415b-8ae6-2c9b9d81fd35', 2, N'Inovação | Innovation', 0, CAST(N'2020-01-04 08:39:41.967' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.967' AS DateTime), 1)
GO
INSERT [dbo].[Tracks] ([Id], [Uid], [EditionId], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'41f51907-0a02-448b-98aa-42065a5b475a', 2, N'Marcas | Brands', 0, CAST(N'2020-01-04 08:39:41.967' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.967' AS DateTime), 1)
GO
INSERT [dbo].[Tracks] ([Id], [Uid], [EditionId], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'c63da8e5-3a1c-460e-abb4-fd5649fad4ba', 2, N'Cérebro | Brain', 0, CAST(N'2020-01-04 08:39:41.970' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.970' AS DateTime), 1)
GO
INSERT [dbo].[Tracks] ([Id], [Uid], [EditionId], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'd09aaaaf-d3e8-4170-86a9-103e139ca4c3', 2, N'Educação | Education', 0, CAST(N'2020-01-04 08:39:41.970' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.970' AS DateTime), 1)
GO
INSERT [dbo].[Tracks] ([Id], [Uid], [EditionId], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (7, N'b6322dd2-997c-437c-8485-a9600d0a5979', 2, N'Arte, Mídia e Entretenimento | Art, Media & Entertainment', 0, CAST(N'2020-01-04 08:39:41.977' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.977' AS DateTime), 1)
GO
INSERT [dbo].[Tracks] ([Id], [Uid], [EditionId], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (8, N'b6e2ed47-553a-4f92-84cf-b505148ea084', 2, N'Comunidades colaborativas | Smart Communities', 0, CAST(N'2020-01-04 08:39:41.977' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.977' AS DateTime), 1)
GO
INSERT [dbo].[Tracks] ([Id], [Uid], [EditionId], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (9, N'fe555e57-0396-4e64-bccd-e85b6ee28a05', 2, N'Novo consumo e Tendências | New Consumers & Trends', 0, CAST(N'2020-01-04 08:39:41.980' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.980' AS DateTime), 1)
GO
INSERT [dbo].[Tracks] ([Id], [Uid], [EditionId], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (10, N'b098b966-2f41-499b-b867-f0783a4bb901', 2, N'O futuro da sociedade e impacto socioambiental | Cities of the future and the future of society', 0, CAST(N'2020-01-04 08:39:41.983' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.983' AS DateTime), 1)
GO
INSERT [dbo].[Tracks] ([Id], [Uid], [EditionId], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (11, N'244b01ec-bb26-4b42-a3d9-7faf54965c92', 2, N'Educação e Qualificação profissional | Education & Professional Capacity Building', 0, CAST(N'2020-01-04 08:39:41.983' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.983' AS DateTime), 1)
GO
INSERT [dbo].[Tracks] ([Id], [Uid], [EditionId], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (12, N'1517e35c-d6f1-41e7-89c8-cb8bcf151007', 2, N'Saúde, Alimentação e Bem-estar | Health, Food & Well-being', 0, CAST(N'2020-01-04 08:39:41.987' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.987' AS DateTime), 1)
GO
INSERT [dbo].[Tracks] ([Id], [Uid], [EditionId], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (13, N'aeea9445-c123-425f-b16a-0d6f7bcfcd91', 2, N'Transformação das empresas e empregos | Transformations for Enterprises & Employment', 0, CAST(N'2020-01-04 08:39:41.987' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.987' AS DateTime), 1)
GO
INSERT [dbo].[Tracks] ([Id], [Uid], [EditionId], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (14, N'32a90fdb-8362-41ba-a24f-a797e4c1e6ad', 2, N'Energia & Impacto socio-ambiental | Energy & Socio-environmental impact', 0, CAST(N'2020-01-04 08:39:41.987' AS DateTime), 1, CAST(N'2020-01-04 08:39:41.987' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Tracks] OFF
GO