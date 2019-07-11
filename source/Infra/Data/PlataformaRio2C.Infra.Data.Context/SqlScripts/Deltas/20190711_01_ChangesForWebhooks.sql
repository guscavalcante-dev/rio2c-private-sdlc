--must run on deploy: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

CREATE TABLE "WebhookRequest"
( 
	"Id"                 uniqueidentifier  NOT NULL ,
	"AlternativeId"      bigint IDENTITY ( 1,1 ) ,
	"SalesPlatformId"    uniqueidentifier  NOT NULL ,
	"CreationDate"       datetime  NOT NULL 
	CONSTRAINT "DF_WebhookRequest_CreationDate"
		 DEFAULT  GETDATE(),
	"Endpoint"           nvarchar(250)  NOT NULL ,
	"Header"             nvarchar(1000)  NULL ,
	"Payload"            nvarchar(max)  NULL ,
	"IpAddress"          varchar(38)  NULL ,
	"IsProcessed"        bit  NOT NULL 
	CONSTRAINT "DF_WebhookRequest_IsProcessed"
		 DEFAULT  0,
	"IsProcessing"       bit  NOT NULL 
	CONSTRAINT "DF_WebhookRequest_IsProcessing"
		 DEFAULT  0,
	"ProcessingCount"    int  NOT NULL 
	CONSTRAINT "DF_WebhookRequest_ProcessingCount"
		 DEFAULT  0,
	"LastProcessingDate" datetime  NULL ,
	"ProcessingErrorCode" nvarchar(10)  NULL ,
	"ProcessingErrorMessage" nvarchar(250)  NULL ,
	"ManualProcessingUserId" int  NULL ,
	"SecurityStamp"      uniqueidentifier  NOT NULL 
)
go



CREATE NONCLUSTERED INDEX "IDX_WebhookRequest_IsProcessed_IsProcessing" ON "WebhookRequest"
( 
	"IsProcessed"         ASC,
	"IsProcessing"        ASC
)
go



CREATE TABLE "SalesPlatform"
( 
	"Id"                 uniqueidentifier  NOT NULL ,
	"AlternativeId"      bigint IDENTITY ( 1,1 ) ,
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
	"SecurityStamp"      uniqueidentifier  NOT NULL 
)
go



ALTER TABLE "WebhookRequest"
ADD CONSTRAINT "PK_WebhookRequest" PRIMARY KEY  CLUSTERED ("Id" ASC)
go



ALTER TABLE "SalesPlatform"
ADD CONSTRAINT "PK_SalesPlatform" PRIMARY KEY  CLUSTERED ("Id" ASC)
go



ALTER TABLE "WebhookRequest"
	ADD CONSTRAINT "FK_SalesPlatform_WebhookRequest_SalesPlatformId" FOREIGN KEY ("SalesPlatformId") REFERENCES "SalesPlatform"("Id")
go



ALTER TABLE "WebhookRequest"
	ADD CONSTRAINT "FK_AspNetUsers_WebhookRequest_ManualProcessingUserId" FOREIGN KEY ("ManualProcessingUserId") REFERENCES "dbo"."AspNetUsers"("Id")
go



ALTER TABLE "SalesPlatform"
	ADD CONSTRAINT "FK_AspNetUsers_SalesPlatform_CreationUserId" FOREIGN KEY ("CreationUserId") REFERENCES "dbo"."AspNetUsers"("Id")
go



ALTER TABLE "SalesPlatform"
	ADD CONSTRAINT "FK_AspNetUsers_SalesPlatform_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."AspNetUsers"("Id")
go


