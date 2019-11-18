--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."ProjectBuyerEvaluations"
DROP CONSTRAINT "FK_ProjectEvaluationStatuses_ProjectBuyerEvaluations_ProjectEvaluationStatusId"
go

ALTER TABLE "dbo"."Projects"
DROP CONSTRAINT "FK_AttendeeOrganizations_Projects_SellerAttendeeOrganizationId"
go

ALTER TABLE "dbo"."ProjectProductPlans"
DROP CONSTRAINT "FK_Projects_ProjectProductPlans_ProjectId"
go

ALTER TABLE "dbo"."ProjectProductPlans"
DROP CONSTRAINT "FK_Languages_ProjectProductPlans_LanguageId"
go

ALTER TABLE "dbo"."ProjectProductPlans"
DROP CONSTRAINT "FK_Users_ProjectProductPlans_CreateUserId"
go

ALTER TABLE "dbo"."ProjectProductPlans"
DROP CONSTRAINT "FK_Users_ProjectProductPlans_UpdateUserId"
go

ALTER TABLE "dbo".ProjectProductPlans
DROP CONSTRAINT "PK_ProjectProductPlans"
go

ALTER TABLE "dbo"."ProjectProductPlans"
DROP CONSTRAINT "IDX_UQ_ProjectProductPlans_Uid"
go

ALTER TABLE "dbo"."ProjectProductPlans"
DROP CONSTRAINT "IDX_UQ_ProjectProductPlans_ProjectId_LanguageId"
go

ALTER TABLE "dbo"."Projects"
  DROP COLUMN "EasyEpisodePlayingTime"
go

ALTER TABLE "dbo"."Projects"
  DROP COLUMN "Pitching"
go

ALTER TABLE "dbo"."Projects"
ADD EachEpisodePlayingTime  varchar(10)  NULL
go

ALTER TABLE "dbo"."Projects"
ADD TotalPlayingTime  varchar(10)  NOT NULL
go

ALTER TABLE "dbo"."Projects"
ADD IsPitching  bit  NOT NULL
go

ALTER TABLE "dbo"."Projects"
ADD FinishDate  datetime  NULL
go

ALTER TABLE "dbo"."Projects"
ADD ProjectBuyerEvaluationsCount  int  NOT NULL
go

ALTER TABLE "dbo"."Projects"
ALTER COLUMN NumberOfEpisodes int NULL
go

ALTER TABLE "dbo"."OrganizationTypes"
ADD IsSeller  bit  NULL
go
UPDATE "OrganizationTypes" SET IsSeller = 1 WHERE Uid IN ('7ce5a34f-e31f-4c26-bed9-cdd6a0206185', 'f2efdbaa-27bd-42bd-bf29-a8daed6093ff', 'd077ba5c-2982-4b69-95d4-d9aa1bf8e7f4')
go
UPDATE "OrganizationTypes" SET IsSeller = 0 WHERE Uid IN ('936b3262-8b8f-472c-94ad-3a2b925dd0ae', '7eb327a9-95e8-4514-8e66-39510fc9ed03', '243aafb2-b610-49b4-b9bc-33cdf631c367')
go
ALTER TABLE "dbo"."OrganizationTypes"
ALTER COLUMN IsSeller  bit  NOT NULL
go

ALTER TABLE "dbo"."ProjectBuyerEvaluations"
ADD BuyerEmailSendDate  datetime  NULL
go

ALTER TABLE "dbo"."Editions"
ADD AttendeeOrganizationMaxSellProjectsCount  int  NULL
go
ALTER TABLE "dbo"."Editions"
ADD ProjectMaxBuyerEvaluationsCount  int  NULL
go
UPDATE "dbo"."Editions" SET AttendeeOrganizationMaxSellProjectsCount = 3, ProjectMaxBuyerEvaluationsCount = 5
go
ALTER TABLE "dbo"."Editions"
ALTER COLUMN AttendeeOrganizationMaxSellProjectsCount  int  NOT NULL
go
ALTER TABLE "dbo"."Editions"
ALTER COLUMN ProjectMaxBuyerEvaluationsCount  int  NOT NULL
go

ALTER TABLE "dbo"."AttendeeOrganizations"
ADD SellProjectsCount  int  NULL
go
UPDATE "dbo"."AttendeeOrganizations" SET SellProjectsCount = 0
go
ALTER TABLE "dbo"."AttendeeOrganizations"
ALTER COLUMN SellProjectsCount  int  NOT NULL
go

ALTER TABLE "dbo"."Projects" 
   ALTER COLUMN "ValuePerEpisode" int
go

ALTER TABLE "dbo"."Projects" 
   ALTER COLUMN "TotalValueOfProject" int
go

ALTER TABLE "dbo"."Projects" 
   ALTER COLUMN "ValueAlreadyRaised" int
go

ALTER TABLE "dbo"."Projects" 
   ALTER COLUMN "ValueStillNeeded" int
go

CREATE TABLE "ProjectProductionPlans"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"ProjectId"          int  NOT NULL ,
	"LanguageId"         int  NOT NULL ,
	"Value"              varchar(3000)  NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE TABLE "ProjectTargetAudiences"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"ProjectId"          int  NOT NULL ,
	"TargetAudienceId"   int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go



ALTER TABLE "ProjectProductionPlans"
ADD CONSTRAINT "PK_ProjectProductionPlans" PRIMARY KEY  CLUSTERED ("Id" ASC)
go



ALTER TABLE "ProjectProductionPlans"
ADD CONSTRAINT "IDX_UQ_ProjectProductionPlans_Uid" UNIQUE ("Uid"  ASC)
go



ALTER TABLE "ProjectProductionPlans"
ADD CONSTRAINT "IDX_UQ_ProjectProductionPlans_ProjectId_LanguageId" UNIQUE ("ProjectId"  ASC,"LanguageId"  ASC)
go



ALTER TABLE "ProjectTargetAudiences"
ADD CONSTRAINT "PK_ProjectTargetAudiences" PRIMARY KEY  CLUSTERED ("Id" ASC)
go



ALTER TABLE "ProjectTargetAudiences"
ADD CONSTRAINT "IDX_UQ_ProjectTargetAudiences_Uid" UNIQUE ("Uid"  ASC)
go



ALTER TABLE "ProjectTargetAudiences"
ADD CONSTRAINT "IDX_UQ_ProjectTargetAudiences_ProjectId_TargetAudienceId" UNIQUE ("ProjectId"  ASC,"TargetAudienceId"  ASC)
go



ALTER TABLE "dbo"."ProjectBuyerEvaluations"
	ADD CONSTRAINT "FK_ProjectEvaluationStatuses_ProjectBuyerEvaluations_ProjectEvaluationStatusId" FOREIGN KEY ("ProjectEvaluationStatusId") REFERENCES "dbo"."ProjectEvaluationStatuses"("Id")
go



ALTER TABLE "dbo"."Projects"
	ADD CONSTRAINT "FK_AttendeeOrganizations_Projects_SellerAttendeeOrganizationId" FOREIGN KEY ("SellerAttendeeOrganizationId") REFERENCES "dbo"."AttendeeOrganizations"("Id")
go



ALTER TABLE "ProjectProductionPlans"
	ADD CONSTRAINT "FK_Projects_ProjectProductionPlans_ProjectId" FOREIGN KEY ("ProjectId") REFERENCES "dbo"."Projects"("Id")
go



ALTER TABLE "ProjectProductionPlans"
	ADD CONSTRAINT "FK_Languages_ProjectProductionPlans_LanguageId" FOREIGN KEY ("LanguageId") REFERENCES "dbo"."Languages"("Id")
go



ALTER TABLE "ProjectProductionPlans"
	ADD CONSTRAINT "FK_Users_ProjectProductionPlans_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go



ALTER TABLE "ProjectProductionPlans"
	ADD CONSTRAINT "FK_Users_ProjectProductionPlans_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go



ALTER TABLE "ProjectTargetAudiences"
	ADD CONSTRAINT "FK_Projects_ProjectTargetAudiences_ProjectId" FOREIGN KEY ("ProjectId") REFERENCES "dbo"."Projects"("Id")
go



ALTER TABLE "ProjectTargetAudiences"
	ADD CONSTRAINT "FK_TargetAudiences_ProjectTargetAudiences_TargetAudienceId" FOREIGN KEY ("TargetAudienceId") REFERENCES "dbo"."TargetAudiences"("Id")
go



ALTER TABLE "ProjectTargetAudiences"
	ADD CONSTRAINT "FK_Users_ProjectTargetAudiences_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go



ALTER TABLE "ProjectTargetAudiences"
	ADD CONSTRAINT "FK_Users_ProjectTargetAudiences_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go
