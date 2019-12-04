--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

CREATE TABLE "SubscribeLists"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(200)  NOT NULL ,
	"Description"        varchar(2000)  NULL ,
	"Code"               varchar(50)  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_SubscribeLists_Code" ON "SubscribeLists"
( 
	"Code"                ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_SubscribeLists_IsDeleted" ON "SubscribeLists"
( 
	"IsDeleted"           ASC
)
go

CREATE TABLE "SubscribeListUsers"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"SubscribeListId"    int  NOT NULL ,
	"UserId"             int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_SubscribeListUsers_IsDeleted" ON "SubscribeListUsers"
( 
	"IsDeleted"           ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_SubscribeListUsers_UserId" ON "SubscribeListUsers"
( 
	"UserId"              ASC
)
go

ALTER TABLE "SubscribeLists"
ADD CONSTRAINT "PK_SubscribeLists" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "SubscribeLists"
ADD CONSTRAINT "IDX_UQ_SubscribeLists_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "SubscribeListUsers"
ADD CONSTRAINT "PK_SubscribeListUsers" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "SubscribeListUsers"
ADD CONSTRAINT "IDX_UQ_SubscribeListUsers_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "SubscribeListUsers"
ADD CONSTRAINT "IDX_UQ_SubscribeListUsers_SubscribeListId_UserId" UNIQUE ("SubscribeListId"  ASC,"UserId"  ASC)
go

ALTER TABLE "SubscribeLists"
	ADD CONSTRAINT "FK_Users_SubscribeLists_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "SubscribeLists"
	ADD CONSTRAINT "FK_Users_SubscribeLists_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "SubscribeListUsers"
	ADD CONSTRAINT "FK_SubscribeLists_SubscribeListUsers_SubscribeListId" FOREIGN KEY ("SubscribeListId") REFERENCES "SubscribeLists"("Id")
go

ALTER TABLE "SubscribeListUsers"
	ADD CONSTRAINT "FK_Users_SubscribeListUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "SubscribeListUsers"
	ADD CONSTRAINT "FK_Users_SubscribeListUsers_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "SubscribeListUsers"
	ADD CONSTRAINT "FK_Users_SubscribeListUsers_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

SET IDENTITY_INSERT [dbo].[SubscribeLists] ON 
GO
INSERT [dbo].[SubscribeLists] ([Id], [Uid], [Name], [Description], [Code], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'736c2355-8b73-42ea-a767-6510376d2a12', N'Emails de conversas não lidas | Unread conversations emails', N'Lista para recebimento de emails de mensagens recebidas e não lidas | List to receive emails of messages received and not read', N'UnreadConversationEmail', 0, CAST(N'2019-12-04 14:38:15.827' AS DateTime), 1, CAST(N'2019-12-04 14:38:15.827' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[SubscribeLists] OFF
GO

INSERT INTO dbo.SubscribeListUsers (Uid, SubscribeListId, UserId, IsDeleted, CreateDate, CreateUserId, UpdateDate, UpdateUserId)
	SELECT NEWID(), sl.Id, u.Id, 0, GETDATE(), 1, GETDATE(), 1 FROM dbo.Users u, dbo.SubscribeLists sl
go