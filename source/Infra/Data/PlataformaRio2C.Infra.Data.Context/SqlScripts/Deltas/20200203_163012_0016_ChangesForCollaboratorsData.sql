--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Collaborators"
ADD BirthDate  datetime  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD CollaboratorGenderId  int  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD CollaboratorGenderAdditionalInfo  varchar(300)  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD CollaboratorRoleId  int  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD CollaboratorRoleAdditionalInfo  varchar(300)  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD CollaboratorIndustryId  int  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD CollaboratorIndustryAdditionalInfo  varchar(300)  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD HasAnySpecialNeeds  bit  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD SpecialNeedsDescription  varchar(300)  NULL
go

CREATE TABLE "CollaboratorRoles"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(300)  NOT NULL ,
	"HasAdditionalInfo"  bit  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_CollaboratorRoles_Name" ON "CollaboratorRoles"
( 
	"Name"                ASC
)
go

CREATE TABLE "CollaboratorIndustries"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(300)  NOT NULL ,
	"HasAdditionalInfo"  bit  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_CollaboratorIndustries_Name" ON "CollaboratorIndustries"
( 
	"Name"                ASC
)
go

CREATE TABLE "CollaboratorGenders"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(300)  NOT NULL ,
	"HasAdditionalInfo"  bit  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_CollaboratorGenders_Name" ON "CollaboratorGenders"
( 
	"Name"                ASC
)
go

CREATE TABLE "CollaboratorEditionParticipations"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"CollaboratorId"     int  NOT NULL ,
	"EditionId"          int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

ALTER TABLE "CollaboratorRoles"
ADD CONSTRAINT "PK_CollaboratorRoles" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "CollaboratorRoles"
ADD CONSTRAINT "IDX_UQ_CollaboratorRoles_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "CollaboratorIndustries"
ADD CONSTRAINT "PK_CollaboratorIndustries" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "CollaboratorIndustries"
ADD CONSTRAINT "IDX_UQ_CollaboratorIndustries_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "CollaboratorGenders"
ADD CONSTRAINT "PK_CollaboratorGenders" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "CollaboratorGenders"
ADD CONSTRAINT "IDX_UQ_CollaboratorGenders_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "CollaboratorEditionParticipations"
ADD CONSTRAINT "PK_CollaboratorEditionParticipations" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "CollaboratorEditionParticipations"
ADD CONSTRAINT "IDX_UQ_CollaboratorEditionParticipations_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "CollaboratorEditionParticipations"
ADD CONSTRAINT "IDX_UQ_CollaboratorEditionParticipations_CollaboratorId_EditionId" UNIQUE ("CollaboratorId"  ASC,"EditionId"  ASC)
go

ALTER TABLE "CollaboratorRoles"
	ADD CONSTRAINT "FK_Users_CollaboratorRoles_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "CollaboratorRoles"
	ADD CONSTRAINT "FK_Users_CollaboratorRoles_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "CollaboratorIndustries"
	ADD CONSTRAINT "FK_Users_CollaboratorIndustries_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "CollaboratorIndustries"
	ADD CONSTRAINT "FK_Users_CollaboratorIndustries_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "CollaboratorGenders"
	ADD CONSTRAINT "FK_Users_CollaboratorGenders_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "CollaboratorGenders"
	ADD CONSTRAINT "FK_Users_CollaboratorGenders_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "dbo"."Collaborators"
	ADD CONSTRAINT "FK_CollaboratorGenders_Collaborators_CollaboratorGenderId" FOREIGN KEY ("CollaboratorGenderId") REFERENCES "CollaboratorGenders"("Id")
go

ALTER TABLE "dbo"."Collaborators"
	ADD CONSTRAINT "FK_CollaboratorRoles_Collaborators_CollaboratorRoleId" FOREIGN KEY ("CollaboratorRoleId") REFERENCES "CollaboratorRoles"("Id")
go

ALTER TABLE "dbo"."Collaborators"
	ADD CONSTRAINT "FK_CollaboratorIndustries_Collaborators_CollaboratorIndustryId" FOREIGN KEY ("CollaboratorIndustryId") REFERENCES "CollaboratorIndustries"("Id")
go

ALTER TABLE "CollaboratorEditionParticipations"
	ADD CONSTRAINT "FK_Collaborators_CollaboratorEditionParticipations_CollaboratorId" FOREIGN KEY ("CollaboratorId") REFERENCES "dbo"."Collaborators"("Id")
go

ALTER TABLE "CollaboratorEditionParticipations"
	ADD CONSTRAINT "FK_Editions_CollaboratorEditionParticipations_EditionId" FOREIGN KEY ("EditionId") REFERENCES "dbo"."Editions"("Id")
go

ALTER TABLE "CollaboratorEditionParticipations"
	ADD CONSTRAINT "FK_Users_CollaboratorEditionParticipations_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "CollaboratorEditionParticipations"
	ADD CONSTRAINT "FK_Users_CollaboratorEditionParticipations_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go


SET IDENTITY_INSERT [dbo].[CollaboratorGenders] ON 
GO
INSERT [dbo].[CollaboratorGenders] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'85016891-2d7b-420e-863d-380af4242b0c', N'Masculino | Male', 0, 0, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorGenders] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'79da8e08-1a70-4c8d-88eb-4da7df5eb8f3', N'Feminino | Female', 0, 0, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorGenders] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'5ba45352-91f6-41c1-a04c-dd9e7c17e7f1', N'Outros | Others', 0, 0, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[CollaboratorGenders] OFF
GO

SET IDENTITY_INSERT [dbo].[CollaboratorIndustries] ON 
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'a86cc435-3ace-46d4-bf44-23071187f40e', N'Aeroespacial/Aviação | Aerospace/Aviation', 0, 0, CAST(N'2020-02-03 18:29:32.100' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.100' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'89ed6b62-b77f-47cc-9ef5-d1788d352f55', N'Aceleradora/Incubadora | Accelerator/Incubator', 0, 0, CAST(N'2020-02-03 18:29:32.123' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.123' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'574a8727-8eeb-441b-9992-2aa6ceeb515e', N'Agricultura | Agriculture', 0, 0, CAST(N'2020-02-03 18:29:32.123' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.123' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'9ba19f1d-c81c-489c-85e4-adc83c3bdf92', N'Vestuário e Moda | Apparel & Fashion', 0, 0, CAST(N'2020-02-03 18:29:32.123' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.123' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'bc77aaa4-1686-4789-8393-fd3f84c1dcbf', N'Arte | Art', 0, 0, CAST(N'2020-02-03 18:29:32.167' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.167' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'acde3515-0511-4e74-a3bf-ec0439145093', N'Automotivo | Automotive', 0, 0, CAST(N'2020-02-03 18:29:32.167' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.167' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (7, N'a156986b-b7cc-4e2a-930d-f1d3b923cffe', N'Biotech | Biotech', 0, 0, CAST(N'2020-02-03 18:29:32.190' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.190' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (8, N'fa3eb1c1-2e52-427b-825c-2dd1afd7f046', N'Construção/Arquitetura | Building/Architecture', 0, 0, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (9, N'772f5720-4172-4dc2-a5b9-c9c8861f06db', N'Serviço de Informática | Computer Service ', 0, 0, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (10, N'9e266951-0d92-4fbc-838f-1ecee572b80e', N'Tecnologia de Computação | Computer Technology', 0, 0, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (11, N'1394b731-2683-43b0-a49b-9873d04b6be1', N'Bens de consumo | Consumer Goods', 0, 0, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (12, N'64318740-7135-4349-b404-6bfecdb3ee77', N'Logística | Logistic', 0, 0, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (13, N'5c57771a-c7d7-4a30-ab25-7b7c636abe01', N'Design | Design', 0, 0, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (14, N'b4235680-ffb5-40ec-a2a4-1cb444419b03', N'Educação | Education', 0, 0, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (15, N'10e7b8b6-ef58-43fb-97b7-ec2981c5472e', N'Energia | Energy', 0, 0, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (16, N'49f45421-df41-4579-a925-35a0632883a2', N'Serviços ambientais | Environmetal Services', 0, 0, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.193' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (17, N'14e52d6f-ebc0-450c-af23-1c4993b2ac91', N'Serviços para Eventos | Event Services', 0, 0, CAST(N'2020-02-03 18:29:32.197' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.197' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (18, N'0e6bc4d8-aa65-40d3-bb6c-75c8f214496f', N'Cinema/Televisão | Film/Television', 0, 0, CAST(N'2020-02-03 18:29:32.197' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.197' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (19, N'8875b117-2f80-4f70-baeb-e057a9f2bff0', N'Cinema/Televisão - Ator/Atriz | Film/Television - Actor/Actress', 0, 0, CAST(N'2020-02-03 18:29:32.197' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.197' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (20, N'da5788d1-9c11-4c2c-bf36-146a17c44a5f', N'Cinema/Televisão - Animação | Film/Television - Animation', 0, 0, CAST(N'2020-02-03 18:29:32.197' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.197' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (21, N'8718d65a-ff3d-44b3-8645-38eeca5e8e1a', N'Cinema/Televisão - Transmissão | Film/Television - Broadcast', 0, 0, CAST(N'2020-02-03 18:29:32.197' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.197' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (22, N'7816ff2d-2ef2-477f-b2db-eea475ddc4de', N'Cinema/Televisão - Distribuição | Film/Television - Distribution', 0, 0, CAST(N'2020-02-03 18:29:32.197' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.197' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (23, N'99e8994f-3ce3-4f4c-9410-07bd9e08ecce', N'Cinema/Televisão - Cineasta/Diretor | Film/Television - Filmmaker/Director', 0, 0, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (24, N'134eb2e7-c252-4520-a27a-acca237690c0', N'Cinema/Televisão - Estúdio independente | Film/Television - Indie Studio', 0, 0, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (25, N'dd063173-e5e8-4e2a-aec3-e38f107db991', N'Cinema/Televisão - Rede | Film/Television - Network', 0, 0, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (26, N'd9248cbc-fe63-4b01-9a8c-8189533a4f3a', N'Cinema/Televisão - Produção | Film/Television - Production', 0, 0, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (27, N'3c0c346c-9e8d-4017-b768-3a4273768b94', N'Serviços financeiros | Financial Services', 0, 0, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (28, N'b3f2e555-76f9-449b-93b7-e852f86ca67d', N'Alimentos e bebidas | Food & Beverages', 0, 0, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (29, N'762e5b47-1445-4e63-99b6-d5f78dbb4b77', N'Games/Games | Gaming/Games', 0, 0, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (30, N'9770332c-c824-46b1-a737-20d91b6b55d5', N'Games/Games - Acessórios | Gaming/Games - Accessories', 0, 0, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (31, N'6ec55826-9777-4589-8128-1699fe2b2de5', N'Games/Games - Desenvolvimento | Gaming/Games - Development', 0, 0, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (32, N'8939ee82-69aa-413a-a025-bc5e60d4efab', N'Games/Games - Esports | Gaming/Games - Esports', 0, 0, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (33, N'd3f5295e-e1f7-429f-82fb-394bb9387c66', N'Governo | Government', 0, 0, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (34, N'170ebc8c-1d71-43ff-9f56-416a3b949972', N'Administração governamental | Government - Administration', 0, 0, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (35, N'52960bc4-93cc-4adc-af7c-ca7b82fa6971', N'Agencia do governo | Government - Agency', 0, 0, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.200' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (36, N'78e6e0e2-f3bf-4670-b1a2-8d4c5fd022e8', N'Saúde | Health', 0, 0, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (37, N'69bcdda2-9c41-4489-a282-41bd17e37032', N'Industria pesada | Heavy Industry', 0, 0, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (38, N'85401a80-ee84-41f3-9de4-5cfef8aa8fbe', N'Hospitalidade | Hospitality', 0, 0, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (39, N'774dc5e6-60cd-4b98-9ddb-1b09c96c4591', N'Relações Internacionais e Negócios | International Affairs & Trades', 0, 0, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (40, N'38af766d-2e29-4eca-a58e-8a4b818f457e', N'Jurídico | Legal Services', 0, 0, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (41, N'ed9e882f-0b92-4a34-8f0a-009eb85b8365', N'Marketing/Publicidade | Marketing/Advertising', 0, 0, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (42, N'6b4d9bd5-da14-4866-adb0-cec91aa0c9f7', N'Marketing/Propaganda - Mídia Social | Marketing/Advertising - Social Media', 0, 0, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (43, N'9cf1dc2e-47a2-4a40-8af0-bf8adb092d83', N'Engenharia Mecânica | Mechanical Engineering', 0, 0, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (44, N'd26bf565-ad70-4a0b-91c2-af66b549c9da', N'Museus e Instituições | Museums & Institutions', 0, 0, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (45, N'651f3a48-8f95-4759-a2f5-7c20dba1650b', N'Música | Music', 0, 0, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (46, N'c28f4962-551e-4ad8-abcf-08c752ae1de6', N'Música - Artista/Perfomer | Music - Artist/Perfomer', 0, 0, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (47, N'18d9991b-c702-4f7c-84a4-49aeb64da62d', N'Compositor de musica | Music - Composer', 0, 0, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.203' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (48, N'8c41f9e5-9006-462e-b2b9-4dda9aab89e0', N'Música - Distribuição | Music - Distribution', 0, 0, CAST(N'2020-02-03 18:29:32.207' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.207' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (49, N'74dbf587-3cd7-4265-840e-1627ee2aa9bb', N'Música - Equipamentos/Instrumentos | Music - Equipment/Instruments', 0, 0, CAST(N'2020-02-03 18:29:32.210' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.210' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (50, N'7f1e81ad-c63b-4d92-839b-286845676801', N'Música - Gravadora independente | Music - Indie Label', 0, 0, CAST(N'2020-02-03 18:29:32.210' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.210' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (51, N'63b51bc8-66c1-4400-a28e-efd4ff498802', N'Música - Licenciamento | Music - Licensing', 0, 0, CAST(N'2020-02-03 18:29:32.210' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.210' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (52, N'49606735-7edc-4f0e-95ee-d7e07b6a89ea', N'Sem fins lucrativos | Non-Profit', 0, 0, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (53, N'c37de855-54b5-4b7c-a68b-113c5fa168fc', N'Artes performáticas | Performing Arts', 0, 0, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (54, N'ee10d369-b936-4ecb-a2dd-af2e32a42989', N'Fotografia | Photography', 0, 0, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (55, N'c6b02edb-879e-4559-878f-faea41ac2f04', N'Imprensa | Press/Media', 0, 0, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (56, N'890902e7-76f3-4dfe-9a4b-a309c8b519d4', N'Imobiliária | Real Estate', 0, 0, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (57, N'0dd0fe12-0255-403b-8eb1-c93dc71f0acd', N'Esportes | Sports', 0, 0, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (58, N'3e195107-cc8e-463c-a891-3145c703f2a9', N'Recrutamento e seleção | Staffing & Recruiting', 0, 0, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (59, N'188ba315-ff0a-4f42-842d-a7181904b410', N'Telecomunicação | Telecommunication', 0, 0, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (60, N'33b214f1-289f-46a3-97be-09b78bad55ae', N'Serviços de utilidade pública | Utilities', 0, 0, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (61, N'4b0ab5cc-1c66-465e-a053-4492eea4cd46', N'Capital de Risco/Private Equity | Venture Capital/Private Equity', 0, 0, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.213' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (62, N'05ea65bf-62ca-47c0-8820-316b7d1efaa9', N'Serviços de Internet/Internet | Web/Internet Services', 0, 0, CAST(N'2020-02-03 18:29:32.217' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.217' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (63, N'3f2820b6-edb8-4a02-aafd-eb46916ca47e', N'Redação e Edição | Writing & Editing', 0, 0, CAST(N'2020-02-03 18:29:32.217' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.217' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorIndustries] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (64, N'754bf4a0-3d58-4583-a20b-edd7dec04cbb', N'Outras | Others', 1, 0, CAST(N'2020-02-03 18:29:32.217' AS DateTime), 1, CAST(N'2020-02-03 18:29:32.217' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[CollaboratorIndustries] OFF
GO

SET IDENTITY_INSERT [dbo].[CollaboratorRoles] ON 
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'0326d455-d1eb-4d35-a782-3166edf042cf', N'C- Level | C-Level', 0, 0, CAST(N'2020-02-03 17:33:05.077' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.077' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'8eee11a4-51e0-4305-9fd6-af98de8583b5', N'Chair | Chair', 0, 0, CAST(N'2020-02-03 17:33:05.133' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.133' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'7b7d379a-b9d7-42e2-8e92-a587d0946e96', N'Diretor | Director', 0, 0, CAST(N'2020-02-03 17:33:05.133' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.133' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'68bd17b9-b52f-4bd6-aa9c-9fc635fbe0b5', N'Presidente | President', 0, 0, CAST(N'2020-02-03 17:33:05.133' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.133' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'4b333205-990f-417d-a253-681127e5733a', N'Fundador / Co-Fundador | Founder/Co-Founder', 0, 0, CAST(N'2020-02-03 17:33:05.133' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.133' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'263a049e-6ca3-442d-947a-30944d907b65', N'Gerente Geral / Head | General Manager/Head', 0, 0, CAST(N'2020-02-03 17:33:05.133' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.133' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (7, N'8abd35ea-fb00-4d4e-a91d-a5fcf40c2afe', N'Estagiário | Intern', 0, 0, CAST(N'2020-02-03 17:33:05.133' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.133' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (8, N'6cb233ac-cde2-453d-8514-3bb716dd0948', N'Gerente | Manager', 0, 0, CAST(N'2020-02-03 17:33:05.133' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.133' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (9, N'1ef5ea84-623a-41b1-b519-8d59d7a5e1e5', N'Proprietário / Co-proprietário | Owner/ Co-Owner', 0, 0, CAST(N'2020-02-03 17:33:05.133' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.133' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (10, N'dd3e5619-9b85-48b8-b5f6-a0f42e361c9d', N'Parceiro | Partner', 0, 0, CAST(N'2020-02-03 17:33:05.137' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.137' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (11, N'a5232e47-c5b6-4313-ad74-636019005a5f', N'Diretor / Reitor | Principal/Dean', 0, 0, CAST(N'2020-02-03 17:33:05.137' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.137' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (12, N'bcd59827-5262-4a3c-9aef-6d3e7b68e949', N'Produtor | Producer', 0, 0, CAST(N'2020-02-03 17:33:05.137' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.137' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (13, N'4ccad6d3-5387-4df6-abd6-34bca1561360', N'Administrador da escola | School Administrator', 0, 0, CAST(N'2020-02-03 17:33:05.137' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.137' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (14, N'a187f33a-b8e4-4ca9-8894-f3bcbcf3fe6a', N'Analista | Analyst', 0, 0, CAST(N'2020-02-03 17:33:05.137' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.137' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (15, N'96937d75-4acf-402a-b222-d285e06096b3', N'Aluno (a) | Student', 0, 0, CAST(N'2020-02-03 17:33:05.137' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.137' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (16, N'f28c9857-0242-4049-944c-65c5c1aa0290', N'Superintendente | Superintendet', 0, 0, CAST(N'2020-02-03 17:33:05.137' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.137' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (17, N'1f65b996-ab3f-4245-9269-ec0f4c02d645', N'Supervisor | Supervisor', 0, 0, CAST(N'2020-02-03 17:33:05.137' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.137' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (18, N'0dbe08dc-881f-44a0-a0e0-abe8fa53e4c2', N'Professor / Professor | Teacher', 0, 0, CAST(N'2020-02-03 17:33:05.140' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.140' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (19, N'b7ebb7f9-040f-4342-9d15-fe078c09c24a', N'Voluntário | Volunteer', 0, 0, CAST(N'2020-02-03 17:33:05.140' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.140' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (20, N'e18f2fc3-31e2-46e7-8a4d-2e653732c8e4', N'Freelancer | Freelancer', 0, 0, CAST(N'2020-02-03 17:33:05.140' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.140' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (21, N'b986018f-8446-46c3-917a-8e20b836626e', N'Fora do mercado | Not Employed', 0, 0, CAST(N'2020-02-03 17:33:05.140' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.140' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorRoles] ([Id], [Uid], [Name], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (22, N'53239053-83ee-44a1-9bf4-4bcb9c78339e', N'Outras | Others', 1, 0, CAST(N'2020-02-03 17:33:05.140' AS DateTime), 1, CAST(N'2020-02-03 17:33:05.140' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[CollaboratorRoles] OFF
GO
