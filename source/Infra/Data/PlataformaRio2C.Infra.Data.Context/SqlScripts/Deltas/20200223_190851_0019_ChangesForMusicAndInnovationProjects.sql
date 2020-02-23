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
	"ImageUploadDate"    datetimeoffset  NULL ,
	"FormationDate"      datetime  NOT NULL ,
	"MainMusicInfluences" varchar(150)  NULL ,
	"Facebook"           varchar(100)  NULL ,
	"Instagram"          varchar(100)  NULL ,
	"Twitter"            varchar(100)  NULL ,
	"Youtube"            varchar(300)  NULL ,
	"Release"            varchar(600)  NULL ,
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
	"ClippingUploadDate" datetimeoffset  NULL ,
	"ProjectEvaluationStatusId" int  NOT NULL ,
	"ProjectEvaluationRefuseId" int  NULL ,
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

CREATE TABLE "AttendeeMusicBandsCollaborators"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"AttendeeMusicBandId" int  NOT NULL ,
	"CollaboratorId"     int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeeMusicBandsCollaborators_CollaboratorId" ON "AttendeeMusicBandsCollaborators"
( 
	"CollaboratorId"      ASC
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
	"Year"               datetime  NULL ,
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

CREATE TABLE "MusicBandTeam"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"MusicBandId"        int  NOT NULL ,
	"Name"               varchar(300)  NOT NULL ,
	"Role"               varchar(100)  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_MusicBandTeam_MusicBandId" ON "MusicBandTeam"
( 
	"MusicBandId"         ASC
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
	"CollaboratorId"     int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganizationCollaborators_CollaboratorId" ON "AttendeeInnovationOrganizationCollaborators"
( 
	"CollaboratorId"      ASC
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

ALTER TABLE "AttendeeMusicBandsCollaborators"
ADD CONSTRAINT "PK_AttendeeMusicBandsCollaborators" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "AttendeeMusicBandsCollaborators"
ADD CONSTRAINT "IDX_UQ_AttendeeMusicBandsCollaborators_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "AttendeeMusicBandsCollaborators"
ADD CONSTRAINT "IDX_UQ_AttendeeMusicBandsCollaborators_AttendeeMusicBandId_CollaboratorId" UNIQUE ("AttendeeMusicBandId"  ASC,"CollaboratorId"  ASC)
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

ALTER TABLE "MusicBandTeam"
ADD CONSTRAINT "PK_MusicBandTeam" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "MusicBandTeam"
ADD CONSTRAINT "IDX_UQ_MusicBandTeam_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "InnovationOrganizations"
ADD CONSTRAINT "PK_InnovationOrganizations" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "InnovationOrganizations"
ADD CONSTRAINT "IDX_UQ_InnovationOrganizations_Uid" UNIQUE ("Uid"  ASC)
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
ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizationCollaborators_AttendeeInnovationOrganizationId_CollaboratorId" UNIQUE ("AttendeeInnovationOrganizationId"  ASC,"CollaboratorId"  ASC)
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
	ADD CONSTRAINT "FK_ProjectEvaluationRefuseReasons_MusicProjects_ProjectEvaluationRefuseId" FOREIGN KEY ("ProjectEvaluationRefuseId") REFERENCES "ProjectEvaluationRefuseReasons"("Id")
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

ALTER TABLE "MusicBandTypes"
	ADD CONSTRAINT "FK_Users_MusicBandTypes_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicBandTypes"
	ADD CONSTRAINT "FK_Users_MusicBandTypes_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeMusicBandsCollaborators"
	ADD CONSTRAINT "FK_AttendeeMusicBands_AttendeeMusicBandsCollaborators_AttendeeMusicBandId" FOREIGN KEY ("AttendeeMusicBandId") REFERENCES "AttendeeMusicBands"("Id")
go

ALTER TABLE "AttendeeMusicBandsCollaborators"
	ADD CONSTRAINT "FK_Collaborators_AttendeeMusicBandsCollaborators_CollaboratorId" FOREIGN KEY ("CollaboratorId") REFERENCES "dbo"."Collaborators"("Id")
go

ALTER TABLE "AttendeeMusicBandsCollaborators"
	ADD CONSTRAINT "FK_Users_AttendeeMusicBandsCollaborators_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeMusicBandsCollaborators"
	ADD CONSTRAINT "FK_Users_AttendeeMusicBandsCollaborators_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
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

ALTER TABLE "MusicBandTeam"
	ADD CONSTRAINT "FK_MusicBands_MusicBandTeam_MusicBandId" FOREIGN KEY ("MusicBandId") REFERENCES "MusicBands"("Id")
go

ALTER TABLE "MusicBandTeam"
	ADD CONSTRAINT "FK_Users_MusicBandTeam_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "MusicBandTeam"
	ADD CONSTRAINT "FK_Users_MusicBandTeam_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
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
	ADD CONSTRAINT "FK_Collaborators_AttendeeInnovationOrganizationCollaborators_CollaboratorId" FOREIGN KEY ("CollaboratorId") REFERENCES "dbo"."Collaborators"("Id")
go

ALTER TABLE "AttendeeInnovationOrganizationCollaborators"
	ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationCollaborators_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeInnovationOrganizationCollaborators"
	ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationCollaborators_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go
