--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

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
