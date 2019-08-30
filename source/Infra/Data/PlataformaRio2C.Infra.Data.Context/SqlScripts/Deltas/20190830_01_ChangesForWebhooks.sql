--must run on deploy: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."SalesPlatformWebhooRequests"
DROP CONSTRAINT "FK_SalesPlatforms_SalesPlatformWebhooRequests_SalesPlatformId"
go



ALTER TABLE "dbo"."SalesPlatformWebhooRequests"
DROP CONSTRAINT "FK_Users_SalesPlatformWebhooRequests_ManualProcessingUserId"
go



ALTER TABLE "dbo".SalesPlatformWebhooRequests
DROP CONSTRAINT "PK_SalesPlatformWebhooRequests"
go



CREATE TABLE "SalesPlatformWebhookRequests"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"SalesPlatformId"    int  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"Endpoint"           varchar(250)  NOT NULL ,
	"Header"             varchar(1000)  NULL ,
	"Payload"            varchar(max)  NULL ,
	"IpAddress"          varchar(38)  NOT NULL ,
	"IsProcessed"        bit  NOT NULL ,
	"IsProcessing"       bit  NOT NULL ,
	"ProcessingCount"    int  NOT NULL ,
	"LastProcessingDate" datetime  NULL ,
	"NextProcessingDate" datetime  NOT NULL ,
	"ProcessingErrorCode" varchar(10)  NULL ,
	"ProcessingErrorMessage" varchar(250)  NULL ,
	"ManualProcessingUserId" int  NULL ,
	"SecurityStamp"      varchar(36)  NOT NULL 
)
go



CREATE NONCLUSTERED INDEX "IDX_SalesPlatformWebhookRequests_IsProcessed_IsProcessing_CreateDate" ON "SalesPlatformWebhookRequests"
( 
	"IsProcessed"         ASC,
	"IsProcessing"        ASC,
	"CreateDate"          ASC
)
go



ALTER TABLE "SalesPlatformWebhookRequests"
ADD CONSTRAINT "PK_SalesPlatformWebhookRequests" PRIMARY KEY  CLUSTERED ("Id" ASC)
go



ALTER TABLE "SalesPlatformWebhookRequests"
ADD CONSTRAINT "IDX_UQ_SalesPlatformWebhookRequests_Uid" UNIQUE ("Uid"  ASC)
go



ALTER TABLE "SalesPlatformWebhookRequests"
	ADD CONSTRAINT "FK_SalesPlatforms_SalesPlatformWebhookRequests_SalesPlatformId" FOREIGN KEY ("SalesPlatformId") REFERENCES "dbo"."SalesPlatforms"("Id")
go



ALTER TABLE "SalesPlatformWebhookRequests"
	ADD CONSTRAINT "FK_Users_SalesPlatformWebhookRequests_ManualProcessingUserId" FOREIGN KEY ("ManualProcessingUserId") REFERENCES "dbo"."Users"("Id")
go