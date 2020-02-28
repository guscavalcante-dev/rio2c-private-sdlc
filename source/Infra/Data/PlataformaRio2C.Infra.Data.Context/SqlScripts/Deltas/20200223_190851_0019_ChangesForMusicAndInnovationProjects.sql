--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."AttendeeCollaboratorTypes"
ADD TermsAcceptanceDate  datetimeoffset  NULL
go

ALTER TABLE "ProjectEvaluationRefuseReasons"
ADD ProjectTypeId  int  NULL
go

UPDATE "ProjectEvaluationRefuseReasons" SET ProjectTypeId = 1
go

ALTER TABLE "ProjectEvaluationRefuseReasons"
ALTER COLUMN ProjectTypeId  int  NOT NULL
go

CREATE TABLE "MusicBands"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"MusicBandTypeId"    int  NOT NULL ,
	"Name"               varchar(300)  NOT NULL ,
	"ImageUrl"           varchar(300)  NULL ,
	"FormationDate"      varchar(300)  NULL ,
	"MainMusicInfluences" varchar(600)  NULL ,
	"Facebook"           varchar(300)  NULL ,
	"Instagram"          varchar(300)  NULL ,
	"Twitter"            varchar(300)  NULL ,
	"Youtube"            varchar(300)  NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_MusicBands_Name" ON "MusicBands"
( 
	"Name"                ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_MusicBands_MusicBandTypeId" ON "MusicBands"
( 
	"MusicBandTypeId"     ASC
)
go

CREATE TABLE "AttendeeMusicBands"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"EditionId"          int  NOT NULL ,
	"MusicBandId"        int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeeMusicBands_MusicBandId" ON "AttendeeMusicBands"
( 
	"MusicBandId"         ASC
)
go

CREATE TABLE "MusicProjects"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"AttendeeMusicBandId" int  NOT NULL ,
	"VideoUrl"           varchar(300)  NULL ,
	"Music1Url"          varchar(300)  NULL ,
	"Music2Url"          varchar(300)  NULL ,
	"Release"            varchar(max)  NULL ,
	"Clipping1"          varchar(300)  NULL ,
	"Clipping2"          varchar(300)  NULL ,
	"Clipping3"          varchar(300)  NULL ,
	"ProjectEvaluationStatusId" int  NOT NULL ,
	"ProjectEvaluationRefuseReasonId" int  NULL ,
	"Reason"             varchar(1500)  NULL ,
	"EvaluationUserId"   int  NULL ,
	"EvaluationEmailSendDate" datetimeoffset  NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_MusicProjects_AttendeeMusicBandId" ON "MusicProjects"
( 
	"AttendeeMusicBandId"  ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_MusicProjects_ProjectEvaluationStatusId" ON "MusicProjects"
( 
	"ProjectEvaluationStatusId"  ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_MusicProjects_EvaluationUserId" ON "MusicProjects"
( 
	"EvaluationUserId"    ASC
)
go

CREATE TABLE "MusicBandTypes"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(100)  NOT NULL ,
	"DisplayOrder"       int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_MusicBandTypes_Name" ON "MusicBandTypes"
( 
	"Name"                ASC
)
go

CREATE TABLE "MusicGenres"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(100)  NOT NULL ,
	"DisplayOrder"       int  NOT NULL ,
	"HasAdditionalInfo"  bit  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_MusicGenres_Name" ON "MusicGenres"
( 
	"Name"                ASC
)
go

CREATE TABLE "MusicBandGenres"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"MusicBandId"        int  NOT NULL ,
	"MusicGenreId"       int  NOT NULL ,
	"AdditionalInfo"     char(18)  NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_MusicBandGenres_MusicGenreId" ON "MusicBandGenres"
( 
	"MusicGenreId"        ASC
)
go

CREATE TABLE "MusicBandTargetAudiences"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"MusicBandId"        int  NOT NULL ,
	"TargetAudienceId"   int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_MusicBandTargetAudiences_TargetAudienceId" ON "MusicBandTargetAudiences"
( 
	"TargetAudienceId"    ASC
)
go

CREATE TABLE "MusicBandMembers"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"MusicBandId"        int  NOT NULL ,
	"Name"               varchar(300)  NOT NULL ,
	"MusicInstrumentName" varchar(100)  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_MusicBandMembers_MusicBandId" ON "MusicBandMembers"
( 
	"MusicBandId"         ASC
)
go

CREATE TABLE "ReleasedMusicProjects"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"MusicBandId"        int  NOT NULL ,
	"Name"               varchar(200)  NOT NULL ,
	"Year"               varchar(300)  NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_ReleasedMusicProjects_MusicBandId" ON "ReleasedMusicProjects"
( 
	"MusicBandId"         ASC
)
go

CREATE TABLE "AttendeeInnovationOrganization"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"EditionId"          int  NOT NULL ,
	"InnovationOrganizationId" int  NOT NULL ,
	"ProjectEvaluationStatusId" int  NOT NULL ,
	"ProjectEvaluationRefuseReasonId" int  NULL ,
	"Reason"             varchar(1500)  NULL ,
	"EvaluationUserId"   int  NULL ,
	"EvaluationEmailSendDate" datetimeoffset  NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganization_InnovationOrganizationId" ON "AttendeeInnovationOrganization"
( 
	"InnovationOrganizationId"  ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganization_ProjectEvaluationStatusId" ON "AttendeeInnovationOrganization"
( 
	"ProjectEvaluationRefuseReasonId"  ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganization_EvaluationuserId" ON "AttendeeInnovationOrganization"
( 
	"EvaluationUserId"    ASC
)
go

CREATE TABLE "InnovationOrganizations"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(100)  NOT NULL ,
	"Document"           varchar(50)  NULL ,
	"ServiceName"        varchar(150)  NOT NULL ,
	"FoundersNames"      varchar(1000)  NOT NULL ,
	"FoundationDate"     datetime  NOT NULL ,
	"AccumulatedRevenue" decimal(12,2)  NOT NULL ,
	"Description"        varchar(600)  NOT NULL ,
	"Curriculum"         varchar(600)  NOT NULL ,
	"WorkDedicationId"   int  NOT NULL ,
	"BusinessDefinition" varchar(300)  NULL ,
	"Website"            varchar(300)  NULL ,
	"BusinessFocus"      varchar(300)  NULL ,
	"MarketSize"         varchar(300)  NULL ,
	"BusinessEconomicModel" varchar(300)  NULL ,
	"BusinessOperationalModel" varchar(300)  NULL ,
	"BusinessDifferentials" varchar(300)  NULL ,
	"CompetingCompanies" varchar(300)  NULL ,
	"BusinessStage"      varchar(300)  NULL ,
	"PresentationUploadDate" datetimeoffset  NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_InnovationOrganizations_Name" ON "InnovationOrganizations"
( 
	"Name"                ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_InnovationOrganizations_Document" ON "InnovationOrganizations"
( 
	"Document"            ASC
)
go

CREATE TABLE "InnovationOptionGroups"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(150)  NOT NULL ,
	"DisplayOrder"       int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_InnovationOptionGroups_Name" ON "InnovationOptionGroups"
( 
	"Name"                ASC
)
go

CREATE TABLE "InnovationOptions"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"InnovationOptionGroupId" int  NOT NULL ,
	"Name"               varchar(150)  NOT NULL ,
	"Description"        varchar(500)  NULL ,
	"DisplayOrder"       int  NOT NULL ,
	"HasAdditionalInfo"  bit  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_InnovationOptions_InnovationOptionGroupId" ON "InnovationOptions"
( 
	"InnovationOptionGroupId"  ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_InnovationOptions_Name" ON "InnovationOptions"
( 
	"Name"                ASC
)
go

CREATE TABLE "InnovationOrganizationOptions"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"InnovationOrganizationId" int  NOT NULL ,
	"InnovationOptionId" int  NOT NULL ,
	"AdditionalInfo"     varchar(200)  NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_InnovationOrganizationOptions_InnovationOptionId" ON "InnovationOrganizationOptions"
( 
	"InnovationOptionId"  ASC
)
go

CREATE TABLE "WorkDedications"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(100)  NOT NULL ,
	"DisplayOrder"       int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_WorkDedications_Name" ON "WorkDedications"
( 
	"Name"                ASC
)
go

CREATE TABLE "AttendeeInnovationOrganizationCollaborators"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"AttendeeInnovationOrganizationId" int  NOT NULL ,
	"AttendeeCollaboratorId" int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganizationCollaborators_AttendeeCollaboratorId" ON "AttendeeInnovationOrganizationCollaborators"
( 
	"AttendeeCollaboratorId"  ASC
)
go

CREATE TABLE "MusicBandTeamMembers"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"MusicBandId"        int  NOT NULL ,
	"Name"               varchar(300)  NOT NULL ,
	"Role"               varchar(300)  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_MusicBandTeamMembers_MusicBandId" ON "MusicBandTeamMembers"
( 
	"MusicBandId"         ASC
)
go

CREATE TABLE "AttendeeMusicBandCollaborators"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"AttendeeMusicBandId" int  NOT NULL ,
	"AttendeeCollaboratorId" int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeeMusicBandCollaborators_AttendeeCollaboratorId" ON "AttendeeMusicBandCollaborators"
( 
	"AttendeeCollaboratorId"  ASC
)
go

ALTER TABLE "MusicBands"
ADD CONSTRAINT "PK_MusicBands" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "MusicBands"
ADD CONSTRAINT "IDX_UQ_MusicBands_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "AttendeeMusicBands"
ADD CONSTRAINT "PK_AttendeeMusicBands" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "AttendeeMusicBands"
ADD CONSTRAINT "IDX_UQ_AttendeeMusicBands_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "AttendeeMusicBands"
ADD CONSTRAINT "IDX_UQ_AttendeeMusicBands_EditionId_MusicBandId" UNIQUE ("EditionId"  ASC,"MusicBandId"  ASC)
go

ALTER TABLE "MusicProjects"
ADD CONSTRAINT "PK_MusicProjects" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "MusicProjects"
ADD CONSTRAINT "IDX_UQ_MusicProjects_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "MusicBandTypes"
ADD CONSTRAINT "PK_MusicBandTypes" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "MusicBandTypes"
ADD CONSTRAINT "IDX_UQ_MusicBandTypes_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "MusicGenres"
ADD CONSTRAINT "PK_MusicGenres" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "MusicGenres"
ADD CONSTRAINT "IDX_UQ_MusicGenres_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "MusicBandGenres"
ADD CONSTRAINT "PK_MusicBandGenres" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "MusicBandGenres"
ADD CONSTRAINT "IDX_UQ_MusicBandGenres_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "MusicBandGenres"
ADD CONSTRAINT "IDX_UQ_MusicBandGenres_MusicBandId_MusicGenreId" UNIQUE ("MusicBandId"  ASC,"MusicGenreId"  ASC)
go

ALTER TABLE "MusicBandTargetAudiences"
ADD CONSTRAINT "PK_MusicBandTargetAudiences" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "MusicBandTargetAudiences"
ADD CONSTRAINT "IDX_UQ_MusicBandTargetAudiences_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "MusicBandTargetAudiences"
ADD CONSTRAINT "IDX_UQ_MusicBandTargetAudiences_MusicBandId_TargetAudienceId" UNIQUE ("MusicBandId"  ASC,"TargetAudienceId"  ASC)
go

ALTER TABLE "MusicBandMembers"
ADD CONSTRAINT "PK_MusicBandMembers" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "MusicBandMembers"
ADD CONSTRAINT "IDX_UQ_MusicBandMembers_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "ReleasedMusicProjects"
ADD CONSTRAINT "PK_ReleasedMusicProjects" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "ReleasedMusicProjects"
ADD CONSTRAINT "IDX_UQ_ReleasedMusicProjects_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "AttendeeInnovationOrganization"
ADD CONSTRAINT "PK_AttendeeInnovationOrganization" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "AttendeeInnovationOrganization"
ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganization_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "AttendeeInnovationOrganization"
ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganization_EditionId_InnovationOrganizationId" UNIQUE ("EditionId"  ASC,"InnovationOrganizationId"  ASC)
go

ALTER TABLE "InnovationOrganizations"
ADD CONSTRAINT "PK_InnovationOrganizations" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "InnovationOrganizations"
ADD CONSTRAINT "IDX_UQ_InnovationOrganizations_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "InnovationOptionGroups"
ADD CONSTRAINT "PK_InnovationOptionGroups" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "InnovationOptionGroups"
ADD CONSTRAINT "IDX_UQ_InnovationOptionGroups_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "InnovationOptions"
ADD CONSTRAINT "PK_InnovationOptions" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "InnovationOptions"
ADD CONSTRAINT "IDX_UQ_InnovationOptions_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "InnovationOrganizationOptions"
ADD CONSTRAINT "PK_InnovationOrganizationOptions" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "InnovationOrganizationOptions"
ADD CONSTRAINT "IDX_UQ_InnovationOrganizationOptions_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "InnovationOrganizationOptions"
ADD CONSTRAINT "IDX_UQ_InnovationOrganizationOptions_InnovationOrganizationId_InnovationOptionId" UNIQUE ("InnovationOrganizationId"  ASC,"InnovationOptionId"  ASC)
go

ALTER TABLE "WorkDedications"
ADD CONSTRAINT "PK_WorkDedications" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "WorkDedications"
ADD CONSTRAINT "IDX_UQ_WorkDedications_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "AttendeeInnovationOrganizationCollaborators"
ADD CONSTRAINT "PK_AttendeeInnovationOrganizationCollaborators" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "AttendeeInnovationOrganizationCollaborators"
ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizationCollaborators_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "AttendeeInnovationOrganizationCollaborators"
ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizationCollaborators_AttendeeInnovationOrganizationId_AttendeeCollaboratorId" UNIQUE ("AttendeeInnovationOrganizationId"  ASC,"AttendeeCollaboratorId"  ASC)
go

ALTER TABLE "MusicBandTeamMembers"
ADD CONSTRAINT "PK_MusicBandTeamMembers" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "MusicBandTeamMembers"
ADD CONSTRAINT "IDX_UQ_MusicBandTeamMembers_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "AttendeeMusicBandCollaborators"
ADD CONSTRAINT "PK_AttendeeMusicBandCollaborators" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "AttendeeMusicBandCollaborators"
ADD CONSTRAINT "IDX_UQ_AttendeeMusicBandCollaborators_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "AttendeeMusicBandCollaborators"
ADD CONSTRAINT "IDX_UQ_AttendeeMusicBandCollaborators_AttendeeMusicBandId_AttendeeCollaboratorId" UNIQUE ("AttendeeMusicBandId"  ASC,"AttendeeCollaboratorId"  ASC)
go

ALTER TABLE "MusicBands"
	ADD CONSTRAINT "FK_MusicBandTypes_MusicBands_MusicBandTypeId" FOREIGN KEY ("MusicBandTypeId") REFERENCES "MusicBandTypes"("Id")
go

ALTER TABLE "MusicBands"
	ADD CONSTRAINT "FK_Users_MusicBands_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicBands"
	ADD CONSTRAINT "FK_Users_MusicBands_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeMusicBands"
	ADD CONSTRAINT "FK_Editions_AttendeeMusicBands_EditionId" FOREIGN KEY ("EditionId") REFERENCES "dbo"."Editions"("Id")
go

ALTER TABLE "AttendeeMusicBands"
	ADD CONSTRAINT "FK_MusicBands_AttendeeMusicBands_MusicBandId" FOREIGN KEY ("MusicBandId") REFERENCES "MusicBands"("Id")
go

ALTER TABLE "AttendeeMusicBands"
	ADD CONSTRAINT "FK_Users_AttendeeMusicBands_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeMusicBands"
	ADD CONSTRAINT "FK_Users_AttendeeMusicBands_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicProjects"
	ADD CONSTRAINT "FK_AttendeeMusicBands_MusicProjects_AttendeeMusicBandId" FOREIGN KEY ("AttendeeMusicBandId") REFERENCES "AttendeeMusicBands"("Id")
go

ALTER TABLE "MusicProjects"
	ADD CONSTRAINT "FK_ProjectEvaluationStatuses_MusicProjects_ProjectEvaluationStatusId" FOREIGN KEY ("ProjectEvaluationStatusId") REFERENCES "dbo"."ProjectEvaluationStatuses"("Id")
go

ALTER TABLE "MusicProjects"
	ADD CONSTRAINT "FK_Users_MusicProjects_EvaluationUserId" FOREIGN KEY ("EvaluationUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicProjects"
	ADD CONSTRAINT "FK_Users_MusicProjects_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicProjects"
	ADD CONSTRAINT "FK_Users_MusicProjects_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicProjects"
	ADD CONSTRAINT "FK_ProjectEvaluationRefuseReasons_MusicProjects_ProjectEvaluationRefuseReasonId" FOREIGN KEY ("ProjectEvaluationRefuseReasonId") REFERENCES "ProjectEvaluationRefuseReasons"("Id")
go

ALTER TABLE "MusicBandTypes"
	ADD CONSTRAINT "FK_Users_MusicBandTypes_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicBandTypes"
	ADD CONSTRAINT "FK_Users_MusicBandTypes_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicGenres"
	ADD CONSTRAINT "FK_Users_MusicGenres_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicGenres"
	ADD CONSTRAINT "FK_Users_MusicGenres_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicBandGenres"
	ADD CONSTRAINT "FK_MusicBands_MusicBandGenres_MusicBandId" FOREIGN KEY ("MusicBandId") REFERENCES "MusicBands"("Id")
go

ALTER TABLE "MusicBandGenres"
	ADD CONSTRAINT "FK_MusicGenres_MusicBandGenres_MusicGenreId" FOREIGN KEY ("MusicGenreId") REFERENCES "MusicGenres"("Id")
go

ALTER TABLE "MusicBandGenres"
	ADD CONSTRAINT "FK_Users_MusicBandGenres_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicBandGenres"
	ADD CONSTRAINT "FK_Users_MusicBandGenres_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicBandTargetAudiences"
	ADD CONSTRAINT "FK_MusicBands_MusicBandTargetAudiences_MusicBandId" FOREIGN KEY ("MusicBandId") REFERENCES "MusicBands"("Id")
go

ALTER TABLE "MusicBandTargetAudiences"
	ADD CONSTRAINT "FK_TargetAudiences_MusicBandTargetAudiences_TargetAudienceId" FOREIGN KEY ("TargetAudienceId") REFERENCES "dbo"."TargetAudiences"("Id")
go

ALTER TABLE "MusicBandTargetAudiences"
	ADD CONSTRAINT "FK_Users_MusicBandTargetAudiences_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicBandTargetAudiences"
	ADD CONSTRAINT "FK_Users_MusicBandTargetAudiences_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicBandMembers"
	ADD CONSTRAINT "FK_MusicBands_MusicBandMembers_MusicBandId" FOREIGN KEY ("MusicBandId") REFERENCES "MusicBands"("Id")
go

ALTER TABLE "MusicBandMembers"
	ADD CONSTRAINT "FK_Users_MusicBandMembers_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicBandMembers"
	ADD CONSTRAINT "FK_Users_MusicBandMembers_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ReleasedMusicProjects"
	ADD CONSTRAINT "FK_MusicBands_ReleasedMusicProjects_MusicBandId" FOREIGN KEY ("MusicBandId") REFERENCES "MusicBands"("Id")
go

ALTER TABLE "ReleasedMusicProjects"
	ADD CONSTRAINT "FK_Users_ReleasedMusicProjects_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ReleasedMusicProjects"
	ADD CONSTRAINT "FK_Users_ReleasedMusicProjects_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeInnovationOrganization"
	ADD CONSTRAINT "FK_Editions_AttendeeInnovationOrganization_EditionId" FOREIGN KEY ("EditionId") REFERENCES "dbo"."Editions"("Id")
go

ALTER TABLE "AttendeeInnovationOrganization"
	ADD CONSTRAINT "FK_InnovationOrganizations_AttendeeInnovationOrganization_InnovationOrganizationId" FOREIGN KEY ("InnovationOrganizationId") REFERENCES "InnovationOrganizations"("Id")
go

ALTER TABLE "AttendeeInnovationOrganization"
	ADD CONSTRAINT "FK_ProjectEvaluationStatuses_AttendeeInnovationOrganization_ProjectEvaluationStatusId" FOREIGN KEY ("ProjectEvaluationStatusId") REFERENCES "dbo"."ProjectEvaluationStatuses"("Id")
go

ALTER TABLE "AttendeeInnovationOrganization"
	ADD CONSTRAINT "FK_ProjectEvaluationRefuseReasons_AttendeeInnovationOrganization_ProjectEvaluationRefuseReasonId" FOREIGN KEY ("ProjectEvaluationRefuseReasonId") REFERENCES "ProjectEvaluationRefuseReasons"("Id")
go

ALTER TABLE "AttendeeInnovationOrganization"
	ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganization_EvaluationUserId" FOREIGN KEY ("EvaluationUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeInnovationOrganization"
	ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganization_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeInnovationOrganization"
	ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganization_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "InnovationOrganizations"
	ADD CONSTRAINT "FK_WorkDedications_InnovationOrganizations_WorkDedicationId" FOREIGN KEY ("WorkDedicationId") REFERENCES "WorkDedications"("Id")
go

ALTER TABLE "InnovationOrganizations"
	ADD CONSTRAINT "FK_Users_InnovationOrganizations_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "InnovationOrganizations"
	ADD CONSTRAINT "FK_Users_InnovationOrganizations_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ProjectEvaluationRefuseReasons"
	ADD CONSTRAINT "FK_ProjectTypes_ProjectEvaluationRefuseReasons_ProjectTypeId" FOREIGN KEY ("ProjectTypeId") REFERENCES "dbo"."ProjectTypes"("Id")
go

ALTER TABLE "InnovationOptionGroups"
	ADD CONSTRAINT "FK_Users_InnovationOptionGroups_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "InnovationOptionGroups"
	ADD CONSTRAINT "FK_Users_InnovationOptionGroups_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "InnovationOptions"
	ADD CONSTRAINT "FK_InnovationOptionGroups_InnovationOptions_InnovationOptionGroupId" FOREIGN KEY ("InnovationOptionGroupId") REFERENCES "InnovationOptionGroups"("Id")
go

ALTER TABLE "InnovationOptions"
	ADD CONSTRAINT "FK_Users_InnovationOptions_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "InnovationOptions"
	ADD CONSTRAINT "FK_Users_InnovationOptions_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "InnovationOrganizationOptions"
	ADD CONSTRAINT "FK_InnovationOrganizations_InnovationOrganizationOptions_InnovationOrganizationId" FOREIGN KEY ("InnovationOrganizationId") REFERENCES "InnovationOrganizations"("Id")
go

ALTER TABLE "InnovationOrganizationOptions"
	ADD CONSTRAINT "FK_InnovationOptions_InnovationOrganizationOptions_InnovationOptionId" FOREIGN KEY ("InnovationOptionId") REFERENCES "InnovationOptions"("Id")
go

ALTER TABLE "InnovationOrganizationOptions"
	ADD CONSTRAINT "FK_Users_InnovationOrganizationOptions_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "InnovationOrganizationOptions"
	ADD CONSTRAINT "FK_Users_InnovationOrganizationOptions_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "WorkDedications"
	ADD CONSTRAINT "FK_Users_WorkDedications_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "WorkDedications"
	ADD CONSTRAINT "FK_Users_WorkDedications_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeInnovationOrganizationCollaborators"
	ADD CONSTRAINT "FK_AttendeeInnovationOrganization_AttendeeInnovationOrganizationCollaborators_AttendeeInnovationOrganizationId" FOREIGN KEY ("AttendeeInnovationOrganizationId") REFERENCES "AttendeeInnovationOrganization"("Id")
go

ALTER TABLE "AttendeeInnovationOrganizationCollaborators"
	ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationCollaborators_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeInnovationOrganizationCollaborators"
	ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationCollaborators_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeInnovationOrganizationCollaborators"
	ADD CONSTRAINT "FK_AttendeeCollaborators_AttendeeInnovationOrganizationCollaborators_AttendeeCollaboratorId" FOREIGN KEY ("AttendeeCollaboratorId") REFERENCES "dbo"."AttendeeCollaborators"("Id")
go

ALTER TABLE "MusicBandTeamMembers"
	ADD CONSTRAINT "FK_MusicBands_MusicBandTeamMembers_MusicBandId" FOREIGN KEY ("MusicBandId") REFERENCES "MusicBands"("Id")
go

ALTER TABLE "MusicBandTeamMembers"
	ADD CONSTRAINT "FK_Users_MusicBandTeamMembers_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicBandTeamMembers"
	ADD CONSTRAINT "FK_Users_MusicBandTeamMembers_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeMusicBandCollaborators"
	ADD CONSTRAINT "FK_AttendeeMusicBands_AttendeeMusicBandCollaborators_AttendeeMusicBandId" FOREIGN KEY ("AttendeeMusicBandId") REFERENCES "AttendeeMusicBands"("Id")
go

ALTER TABLE "AttendeeMusicBandCollaborators"
	ADD CONSTRAINT "FK_AttendeeCollaborators_AttendeeMusicBandCollaborators_AttendeeCollaboratorId" FOREIGN KEY ("AttendeeCollaboratorId") REFERENCES "dbo"."AttendeeCollaborators"("Id")
go

ALTER TABLE "AttendeeMusicBandCollaborators"
	ADD CONSTRAINT "FK_Users_AttendeeMusicBandCollaborators_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeMusicBandCollaborators"
	ADD CONSTRAINT "FK_Users_AttendeeMusicBandCollaborators_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go


SET IDENTITY_INSERT [dbo].[MusicGenres] ON 
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'9a04295a-2e61-41d9-940f-686e9e87b4b4', N'Soul', 1, 0, 0, CAST(N'2020-02-23T20:00:38.0630000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0630000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'd4e39aed-cce7-498d-a234-e1d1ef542036', N'Blues', 2, 0, 0, CAST(N'2020-02-23T20:00:38.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0770000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'd90fc220-9e9e-4e58-891c-61849d260300', N'Clássica', 3, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'3c7f85a0-0d63-4d6b-a6f4-7c70e32195dc', N'Country', 4, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'4044d656-e6ec-418c-9174-97eefc91977b', N'Forró', 5, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'59d477e1-20c0-4b32-af58-003e0c723a14', N'Funk', 6, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (7, N'99c7fd85-d42e-4dcd-b974-29e275d15e32', N'Gospel', 7, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (8, N'b3dda8ce-1b7f-401c-8c73-e94d8fa22aa0', N'Indie', 8, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (9, N'21fdb552-e67e-4e6d-be48-7e81a8a8d5b0', N'Instrumental', 9, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (10, N'e2407520-62dd-419b-a918-3f01f8f881b9', N'Jazz', 10, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (11, N'fce5cdca-a74e-4c9c-90d5-0da4c351f579', N'Kids', 11, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (12, N'd8070e89-48a0-47a6-83f0-0ab79ea153c1', N'MPB', 12, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (13, N'1e8f1594-95e5-42d0-85e6-dbf6eaeeab55', N'Eletrônica', 13, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (14, N'9e092c55-d4fd-47e6-9ed7-b2df8eac3370', N'Pop Rock', 14, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (15, N'58569cef-2fb6-49f7-8a74-fc5beab28527', N'Punk Rock', 15, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (16, N'1c34bc4c-f51c-4dc1-ba3a-de3be2db1f88', N'Rap', 16, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (17, N'5474000e-2a7d-44c9-8dd1-bd38e4649f73', N'Reggae', 17, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (18, N'f3e1fba6-c370-4ae0-9b2d-b08a8a34bf3b', N'Reggaeton', 18, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (19, N'cafc1d28-7477-4ab3-99f8-fdbd4281cb41', N'Heavy Metal', 19, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (20, N'8c7e52d5-1e42-46c2-94bc-5ae7830d95c5', N'Rock Progressivo', 20, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (21, N'bf3dddd0-230a-4b9c-98e5-9bd31f5e0e79', N'Samba', 21, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (22, N'1b3723df-beaa-4729-8d84-46919463ade8', N'Axé', 22, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (23, N'e76d7959-2757-4e30-a3ef-16af2b4f9820', N'Pagode', 23, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (24, N'b3291c19-212a-46f5-814c-a7983927d4ba', N'Sertanejo', 24, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (25, N'ddd19940-7cc6-41ff-a492-e0bbfa48a5fd', N'Surf Music', 25, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (26, N'141d6a5b-5610-424d-9779-710266fa38b0', N'Techno Brega', 26, 0, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicGenres] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (27, N'668bc592-16ff-4ced-a2aa-829947e0cf68', N'Outros', 27, 1, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
SET IDENTITY_INSERT [dbo].[MusicGenres] OFF
GO

SET IDENTITY_INSERT [dbo].[TargetAudiences] ON 
GO
INSERT [dbo].[TargetAudiences] ([Id], [Uid], [ProjectTypeId], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'f69df4be-3994-451b-bdac-e019e17f1668', 2, N'Adulto | Adult', 1, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[TargetAudiences] ([Id], [Uid], [ProjectTypeId], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (7, N'd7dd48cd-4781-44af-bef8-e0c67deb11cf', 2, N'Jovem | Young Adults', 2, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[TargetAudiences] ([Id], [Uid], [ProjectTypeId], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (8, N'7cadfd2f-380d-44ee-a91a-6b4e2f768b18', 2, N'Infantil | Children', 3, 0, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T20:00:38.0800000+00:00' AS DateTimeOffset), 1)
GO
SET IDENTITY_INSERT [dbo].[TargetAudiences] OFF
GO


SET IDENTITY_INSERT [dbo].[InnovationOptionGroups] ON 
GO
INSERT [dbo].[InnovationOptionGroups] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'752598d8-d445-48f7-be41-6a1b110fa749', N'Quais dessas experiências a empresa já participou?', 1, 0, CAST(N'2020-02-23T23:10:26.0870000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:10:26.0870000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptionGroups] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'50a702a9-d189-4f65-bef9-88365d73ba66', N'Enquadre seu produto ou serviço num track abaixo:', 2, 0, CAST(N'2020-02-23T23:10:26.0900000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:10:26.0900000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptionGroups] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'ab308844-5b2f-41e1-9a11-c00088ad2ddf', N'Tecnologia usadas:', 3, 0, CAST(N'2020-02-23T23:10:26.0900000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:10:26.0900000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptionGroups] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'2acbcb51-8520-4767-a5c8-927b8cc007e5', N'Qual o seu principal objetivo em participar das Pitching de Startups?', 4, 0, CAST(N'2020-02-23T23:10:26.0900000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:10:26.0900000+00:00' AS DateTimeOffset), 1)
GO
SET IDENTITY_INSERT [dbo].[InnovationOptionGroups] OFF
GO

SET IDENTITY_INSERT [dbo].[InnovationOptions] ON 
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'82167c1d-7ca6-447f-80c7-ae9188add436', 1, N'Recebeu apoio de incubadora/aceleradora', NULL, 1, 0, 0, CAST(N'2020-02-23T23:13:38.2500000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:13:38.2500000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'29b2cc2f-374d-4f2f-ac00-3513d02ec9c3', 1, N'Captou recursos para pesquisa e desenvolvimento de produtos/serviços tecnológicos', NULL, 2, 0, 0, CAST(N'2020-02-23T23:13:38.2530000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:13:38.2530000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'2fd9f6ba-8852-4dd5-a402-dcd2c14923cb', 1, N'Se relacionou formalmente com grandes ou médias empresas', NULL, 3, 0, 0, CAST(N'2020-02-23T23:13:38.2530000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:13:38.2530000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'60079b3b-a5d9-4e59-a964-725339afbe7f', 1, N'Recebeu investimento de terceiros que envolveram parte do capital da empresa', NULL, 4, 0, 0, CAST(N'2020-02-23T23:13:38.2530000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:13:38.2530000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'4f440536-bab7-4e43-a3a4-f977abafbda8', 1, N'Nenhuma das opções acima', NULL, 5, 0, 0, CAST(N'2020-02-23T23:13:38.2530000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:13:38.2530000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'702a9b2e-dbcb-4bb0-8405-c5bd72eaf627', 2, N'Educação e qualificação profissional', N'Learning experience; desenvolvimento de talentos para indústrias criativas e visionary educators; robótica na educação; gamificação; jogos educativos; educação à distância; edutainment; pensamento computacional; aprendizagem maker e salas de aula do futuro.', 1, 0, 0, CAST(N'2020-02-23T23:15:59.3970000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:15:59.3970000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (7, N'0b1bd552-ad36-4a4d-a55d-d7b07eb6e4e0', 2, N'Arte, mídia e entretenimento', N'Conteúdos e narrativas audiovisuais, musicais e artísticas; linguagens das artes visuais;arquitetura; cultura maker; plataformas digitais; publicidade; branded content; branded experience; branded entertainment; jornalismo; fake news; mídias sociais; game; VR/AR; instalações interativas; wearable tech.', 2, 0, 0, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (8, N'fb997a1a-cbf0-446b-82ff-ff7b5eaa21bf', 2, N'Novo consumo e tendências', N'Economia do entretenimento no mundo digital; modelos de negócios; economia colaborativa; Internet das coisas (IoT); SaaS/PaaS; Inteligência Artificial (AI); blockchain/crypto; e-commerce; customer services; cybersegurança; informação; privacidade; segurança; business intelligence – BigData; insurance tech; investimentos; pagamentos; banking e robôs de investimento.', 3, 0, 0, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (9, N'1646a4d0-f43a-4633-9730-3f8893a627ce', 2, N'Saúde, Alimentação e Bem-Estar', N'Comportamento, wellness; mindfullness; biotech, foodtech; farmtech; agritech; diagnósticos; devices de healthtech; inteligência artificial para saúde; inovação em restaurantes e supermercados, acessibilidade e computer visioning para diagnósticos e narrativas criativas.', 4, 0, 0, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (10, N'a7eedef9-88b9-4ef4-a6ea-32ea5db28598', 2, N'Cidades e Sociedades do Futuro', N'Construtech; smartcities, mercado imobiliário; tecnologia limpa; geolocalização; segurança e mobilidade urbana.', 5, 0, 0, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (11, N'b05fdb02-f880-49d0-818d-65ba716ec0b8', 2, N'A transformação das empresas e empregos', N'Integração das áreas, novos padrões de desempenho; novos negócios; novos critérios de seleção; novas habilidades; novos serviços; flexibilização; autogestão; knowledge management; conhecimento; estabilidade; empreendedorismo e indústrias criativas e design thinking.', 6, 0, 0, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (12, N'de7f9c0a-1aa4-4f7d-985d-2d56d229b3b5', 2, N'Energia para transformar', N'Tecnologia limpa, energias renováveis; Humanidades; sustentabilidade social e ambiental; amplo acesso à tecnologia; políticas públicas; questões sociais e sistema de inovação e economia circular.', 7, 0, 0, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (13, N'0ee805cd-7e63-47de-8034-c405dc5e1da3', 3, N'SAAS', NULL, 1, 0, 0, CAST(N'2020-02-23T23:16:55.9570000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:16:55.9570000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (14, N'6932ac40-e16d-4858-b550-1b4cd2f9461d', 3, N'AI', NULL, 2, 0, 0, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (15, N'516e3187-ce60-4541-9f46-ac41f29ea0eb', 3, N'IOT', NULL, 3, 0, 0, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (16, N'9b3efffc-b4f9-4e65-b679-69eee581dcc2', 3, N'Blockchain', NULL, 4, 0, 0, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (17, N'3663d8a3-df1d-4a41-a3ef-13cf2602fa9d', 3, N'Robótica', NULL, 5, 0, 0, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (18, N'f5b4623d-f4f9-4440-b575-140c273c41d2', 3, N'Outros', NULL, 6, 1, 0, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (19, N'9ecefe6d-ea9a-4dce-88a0-5b720e02eae0', 4, N'Desenvolvimento tecnológico de uma nova solução', NULL, 1, 0, 0, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (20, N'0a0ea283-d9cb-4bce-87b9-5582c57b6e42', 4, N'Aprimoramento tecnológico de uma solução existente', NULL, 2, 0, 0, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (21, N'0d2684e1-4a8a-4088-8547-6ac274eb1ee4', 4, N'Venda de produto/serviço/solução', NULL, 3, 0, 0, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (22, N'064371d0-864f-4b79-b709-221f73b0d35d', 4, N'Oportunidade de conexão com médias/grandes empresas', NULL, 4, 0, 0, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (23, N'38a07682-8bd4-4ede-8be2-1593bad8e0b7', 4, N'Validação de ideias/protótipos', NULL, 5, 0, 0, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (24, N'5f872b5b-da41-43e6-abe7-cef7e6bc0ce6', 4, N'Oportunidade de investimento', NULL, 6, 0, 0, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (25, N'1a144db0-dbf2-4ecc-a4c0-267cea374faa', 4, N'Acesso a serviços de incubação/aceleração', NULL, 7, 0, 0, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptions] ([Id], [Uid], [InnovationOptionGroupId], [Name], [Description], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (26, N'5f62c762-01c0-4f55-a89d-d1e5690817f6', 4, N'Outros', NULL, 8, 1, 0, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1)
GO
SET IDENTITY_INSERT [dbo].[InnovationOptions] OFF
GO

SET IDENTITY_INSERT [dbo].[WorkDedications] ON 
GO
INSERT [dbo].[WorkDedications] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'dcc9878d-ebc7-438c-8a0a-5952b75a8b54', N'Parcial', 1, 0, CAST(N'2020-02-23T23:21:38.8500000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:21:38.8500000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[WorkDedications] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'ada0c122-45ef-41e4-9002-edb9e9fbdb51', N'Integral', 2, 0, CAST(N'2020-02-23T23:21:38.8530000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:21:38.8530000+00:00' AS DateTimeOffset), 1)
GO
SET IDENTITY_INSERT [dbo].[WorkDedications] OFF
GO

SET IDENTITY_INSERT [dbo].[MusicBandTypes] ON 
GO
INSERT [dbo].[MusicBandTypes] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'dd8d2040-52d2-427b-962b-026b7b1c4604', N'Banda / Grupo Musical', 1, 0, CAST(N'2020-02-27T22:28:42.2670000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-27T22:28:42.2670000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[MusicBandTypes] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'8b86b02c-179c-4c5b-b2de-58066bea209e', N'Artista Solo', 2, 0, CAST(N'2020-02-27T22:28:42.2700000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-27T22:28:42.2700000+00:00' AS DateTimeOffset), 1)
GO
SET IDENTITY_INSERT [dbo].[MusicBandTypes] OFF
GO

SET IDENTITY_INSERT [dbo].[CollaboratorTypes] ON 
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (601, N'1610eb14-d2e0-4b09-81f9-f904c1ff37b5', N'Music', 3, 0, CAST(N'2020-02-27 18:27:15.121' AS DateTime), 1, CAST(N'2020-02-27 18:27:15.121' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (602, N'e1a6aeee-15fd-4bdb-b899-acc462f30258', N'Innovation', 3, 0, CAST(N'2020-02-27 18:27:15.121' AS DateTime), 1, CAST(N'2020-02-27 18:27:15.121' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[CollaboratorTypes] OFF
GO