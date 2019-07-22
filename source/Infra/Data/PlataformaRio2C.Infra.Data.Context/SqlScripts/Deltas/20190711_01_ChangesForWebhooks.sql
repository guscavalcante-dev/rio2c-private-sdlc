--must run on deploy: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

CREATE TABLE "SalesPlatformWebhookRequest"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"SalesPlatformId"    int  NOT NULL ,
	"CreationDate"       datetime  NOT NULL 
	CONSTRAINT "DF_SalesPlatformWebhookRequest_CreationDate"
		 DEFAULT  GETDATE(),
	"Endpoint"           nvarchar(250)  NOT NULL ,
	"Header"             nvarchar(1000)  NULL ,
	"Payload"            nvarchar(max)  NULL ,
	"IpAddress"          varchar(38)  NULL ,
	"IsProcessed"        bit  NOT NULL 
	CONSTRAINT "DF_SalesPlatformWebhookRequest_IsProcessed"
		 DEFAULT  0,
	"IsProcessing"       bit  NOT NULL 
	CONSTRAINT "DF_SalesPlatformWebhookRequest_IsProcessing"
		 DEFAULT  0,
	"ProcessingCount"    int  NOT NULL 
	CONSTRAINT "DF_SalesPlatformWebhookRequest_ProcessingCount"
		 DEFAULT  0,
	"LastProcessingDate" datetime  NULL ,
	"NextProcessingDate" datetime  NOT NULL ,
	"ProcessingErrorCode" nvarchar(10)  NULL ,
	"ProcessingErrorMessage" nvarchar(250)  NULL ,
	"ManualProcessingUserId" int  NULL ,
	"SecurityStamp"      varchar(36)  NOT NULL 
)
go



CREATE NONCLUSTERED INDEX "IDX_SalesPlatformWebhookRequest_IsProcessed_IsProcessing" ON "SalesPlatformWebhookRequest"
( 
	"IsProcessed"         ASC,
	"IsProcessing"        ASC
)
go



CREATE TABLE "SalesPlatform"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               nvarchar(100)  NOT NULL ,
	"IsActive"           bit  NOT NULL 
	CONSTRAINT "DF_SalesPlatform_IsActive"
		 DEFAULT  1,
	"WebhookSecurityKey" uniqueidentifier  NOT NULL ,
	"ApiKey"             nvarchar(200)  NULL ,
	"ApiSecret"          nvarchar(200)  NULL ,
	"MaxProcessingCount" int  NOT NULL 
	CONSTRAINT "DF_SalesPlatform_MaxProcessingCount"
		 DEFAULT  1,
	"CreationUserId"     int  NOT NULL ,
	"CreationDate"       datetime  NOT NULL 
	CONSTRAINT "DF_SalesPlatform_CreationDate"
		 DEFAULT  GETDATE(),
	"UpdateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL 
	CONSTRAINT "DF_SalesPlatform_UpdateDate"
		 DEFAULT  GETDATE(),
	"SecurityStamp"      varchar(36)  NOT NULL 
)
go



ALTER TABLE "SalesPlatformWebhookRequest"
ADD CONSTRAINT "PK_SalesPlatformWebhookRequest" PRIMARY KEY  CLUSTERED ("Id" ASC)
go



ALTER TABLE "SalesPlatformWebhookRequest"
ADD CONSTRAINT "UQ_IDX_SalesPlatformWebhookRequest_Uid" UNIQUE ("Uid"  ASC)
go



ALTER TABLE "SalesPlatform"
ADD CONSTRAINT "PK_SalesPlatform" PRIMARY KEY  CLUSTERED ("Id" ASC)
go



ALTER TABLE "SalesPlatform"
ADD CONSTRAINT "UQ_IDX_SalesPlatform_Uid" UNIQUE ("Uid"  ASC)
go



ALTER TABLE "SalesPlatformWebhookRequest"
	ADD CONSTRAINT "FK_SalesPlatform_SalesPlatformWebhookRequest_SalesPlatformId" FOREIGN KEY ("SalesPlatformId") REFERENCES "SalesPlatform"("Id")
go



ALTER TABLE "SalesPlatformWebhookRequest"
	ADD CONSTRAINT "FK_AspNetUsers_SalesPlatformWebhookRequest_ManualProcessingUserId" FOREIGN KEY ("ManualProcessingUserId") REFERENCES "dbo"."AspNetUsers"("Id")
go



ALTER TABLE "SalesPlatform"
	ADD CONSTRAINT "FK_AspNetUsers_SalesPlatform_CreationUserId" FOREIGN KEY ("CreationUserId") REFERENCES "dbo"."AspNetUsers"("Id")
go



ALTER TABLE "SalesPlatform"
	ADD CONSTRAINT "FK_AspNetUsers_SalesPlatform_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."AspNetUsers"("Id")
go



UPDATE dbo.AspNetUsers SET Email = 'admin@rio2c.com' WHERE Email = 'projeto.rio2c@marlin.com.br';
go


SET IDENTITY_INSERT [dbo].[SalesPlatform] ON 
INSERT [dbo].[SalesPlatform] ([Id], [Uid], [Name], [IsActive], [WebhookSecurityKey], [ApiKey], [ApiSecret], [MaxProcessingCount], [CreationUserId], [CreationDate], [UpdateUserId], [UpdateDate], [SecurityStamp]) VALUES (1, N'717287c1-bb97-43ca-99c0-4d25576eb3b0', N'Eventbrite', 1, N'3718d5bd-d3d3-4e8f-a3e0-5270c5830af2', N'AR735YTCZCCPZYKPFN', NULL, 15, 1, GETDATE(), 1, GETDATE(), N'5852137c-86a3-4798-9ece-509818ae0955')
SET IDENTITY_INSERT [dbo].[SalesPlatform] OFF
go