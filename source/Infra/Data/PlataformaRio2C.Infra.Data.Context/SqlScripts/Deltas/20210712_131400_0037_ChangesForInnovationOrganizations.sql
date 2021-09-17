--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION
		ALTER TABLE "dbo"."InnovationOptionGroups"
		DROP CONSTRAINT "FK_Users_InnovationOptionGroups_CreateUserId"
		;



		ALTER TABLE "dbo"."InnovationOptionGroups"
		DROP CONSTRAINT "FK_Users_InnovationOptionGroups_UpdateUserId"
		;



		ALTER TABLE "dbo"."InnovationOptions"
		DROP CONSTRAINT "FK_InnovationOptionGroups_InnovationOptions_InnovationOptionGroupId"
		;



		ALTER TABLE "dbo"."InnovationOptions"
		DROP CONSTRAINT "FK_Users_InnovationOptions_CreateUserId"
		;



		ALTER TABLE "dbo"."InnovationOptions"
		DROP CONSTRAINT "FK_Users_InnovationOptions_UpdateUserId"
		;



		ALTER TABLE "dbo"."InnovationOrganizationOptions"
		DROP CONSTRAINT "FK_InnovationOrganizations_InnovationOrganizationOptions_InnovationOrganizationId"
		;



		ALTER TABLE "dbo"."InnovationOrganizationOptions"
		DROP CONSTRAINT "FK_InnovationOptions_InnovationOrganizationOptions_InnovationOptionId"
		;



		ALTER TABLE "dbo"."InnovationOrganizationOptions"
		DROP CONSTRAINT "FK_Users_InnovationOrganizationOptions_CreateUserId"
		;



		ALTER TABLE "dbo"."InnovationOrganizationOptions"
		DROP CONSTRAINT "FK_Users_InnovationOrganizationOptions_UpdateUserId"
		;



		ALTER TABLE "dbo"."InnovationOrganizations"
		DROP CONSTRAINT "FK_WorkDedications_InnovationOrganizations_WorkDedicationId"
		;



		ALTER TABLE "dbo"."InnovationOptionGroups"
		DROP CONSTRAINT "PK_InnovationOptionGroups"
		;



		ALTER TABLE "dbo"."InnovationOptionGroups"
		DROP CONSTRAINT "IDX_UQ_InnovationOptionGroups_Uid"
		;



		ALTER TABLE "dbo"."InnovationOptions"
		DROP CONSTRAINT "PK_InnovationOptions"
		;



		ALTER TABLE "dbo"."InnovationOptions"
		DROP CONSTRAINT "IDX_UQ_InnovationOptions_Uid"
		;



		ALTER TABLE "dbo"."InnovationOrganizationOptions"
		DROP CONSTRAINT "PK_InnovationOrganizationOptions"
		;



		ALTER TABLE "dbo"."InnovationOrganizationOptions"
		DROP CONSTRAINT "IDX_UQ_InnovationOrganizationOptions_Uid"
		;



		ALTER TABLE "dbo"."InnovationOrganizationOptions"
		DROP CONSTRAINT "IDX_UQ_InnovationOrganizationOptions_InnovationOrganizationId_InnovationOptionId"
		;



		ALTER TABLE "dbo"."InnovationOrganizations"
			DROP COLUMN "FoundersNames"
		;



		ALTER TABLE "dbo"."InnovationOrganizations"
			DROP COLUMN "AccumulatedRevenue"
		;



		ALTER TABLE "dbo"."InnovationOrganizations"
			DROP COLUMN "Curriculum"
		;



		ALTER TABLE "dbo"."InnovationOrganizations"
			DROP COLUMN "BusinessDefinition"
		;



		ALTER TABLE "dbo"."InnovationOrganizations"
			DROP COLUMN "BusinessFocus"
		;



		ALTER TABLE "dbo"."InnovationOrganizations"
			DROP COLUMN "MarketSize"
		;



		ALTER TABLE "dbo"."InnovationOrganizations"
			DROP COLUMN "BusinessEconomicModel"
		;



		ALTER TABLE "dbo"."InnovationOrganizations"
			DROP COLUMN "BusinessOperationalModel"
		;



		ALTER TABLE "dbo"."InnovationOrganizations"
			DROP COLUMN "BusinessDifferentials"
		;



		ALTER TABLE "dbo"."InnovationOrganizations"
			DROP COLUMN "CompetingCompanies"
		;



		ALTER TABLE "dbo"."InnovationOrganizations"
			DROP COLUMN "BusinessStage"
		;



		ALTER TABLE "dbo"."InnovationOrganizations"
			DROP COLUMN "PresentationUploadDate"
		;



		ALTER TABLE "dbo"."InnovationOrganizations"
			DROP COLUMN "WorkDedicationId"
		;



		ALTER TABLE "dbo"."InnovationOrganizations"
		ADD ImageUploadDate  datetimeoffset  NULL
		;



		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		ADD AccumulatedRevenue  decimal(12,2)  NULL;

		EXEC('UPDATE dbo.AttendeeInnovationOrganizations set AccumulatedRevenue = 0');

		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		ALTER COLUMN AccumulatedRevenue decimal(12,2)  NOT NULL;



		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		ADD MarketSize  varchar(300)  NULL
		;



		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		ADD BusinessDefinition  varchar(300)  NULL
		;



		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		ADD BusinessFocus  varchar(300)  NULL
		;



		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		ADD BusinessEconomicModel  varchar(300)  NULL
		;



		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		ADD BusinessDifferentials  varchar(300)  NULL
		;



		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		ADD BusinessStage  varchar(300)  NULL
		;



		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		ADD PresentationUploadDate  datetimeoffset  NULL
		;



		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		ADD BusinessOperationalModel  varchar(300)  NULL
		;

		DROP INDEX [IDX_InnovationOrganizations_Name] ON [dbo].[InnovationOrganizations]
		;

		ALTER TABLE "dbo"."InnovationOrganizations" 
			ALTER COLUMN "Name" varchar(300)
		;

		CREATE NONCLUSTERED INDEX "IDX_InnovationOrganizations_Name" ON "dbo"."InnovationOrganizations"
		( 
			"Name"                ASC
		)
		;



		ALTER TABLE "dbo"."InnovationOrganizations" 
			ALTER COLUMN "ServiceName" varchar(300)
		;



		CREATE TABLE "AttendeeInnovationOrganizationFounders"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"AttendeeInnovationOrganizationId" int  NOT NULL ,
			"Fullname"           varchar(200)  NOT NULL ,
			"Curriculum"         varchar(710)  NOT NULL ,
			"WorkDedicationId"   int  NOT NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)
		;



		CREATE TABLE "AttendeeInnovationOrganizationExperiences"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"AttendeeInnovationOrganizationId" int  NOT NULL ,
			"InnovationOrganizationExperienceOptionId" int  NOT NULL ,
			"AdditionalInfo"     varchar(200)  NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)
		;



		CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganizationExperiences_AttendeeInnovationOrganizationId" ON "AttendeeInnovationOrganizationExperiences"
		( 
			"AttendeeInnovationOrganizationId"  ASC
		)
		;



		CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganizationExperiences_InnovationOrganizationExperienceOptionId" ON "AttendeeInnovationOrganizationExperiences"
		( 
			"InnovationOrganizationExperienceOptionId"  ASC
		)
		;



		CREATE TABLE "InnovationOrganizationExperienceOptions"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"Name"               varchar(500)  NOT NULL ,
			"DisplayOrder"       int  NOT NULL ,
			"HasAdditionalInfo"  bit  NOT NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)
		;



		CREATE TABLE "AttendeeInnovationOrganizationCompetitors"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"AttendeeInnovationOrganizationId" int  NOT NULL ,
			"Name"               varchar(300)  NOT NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)
		;



		CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganizationCompetitors_AttendeeInnovationOrganizationId" ON "AttendeeInnovationOrganizationCompetitors"
		( 
			"AttendeeInnovationOrganizationId"  ASC
		)
		;



		CREATE TABLE "InnovationOrganizationTrackOptions"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"Name"               varchar(500)  NOT NULL ,
			"DisplayOrder"       int  NOT NULL ,
			"HasAdditionalInfo"  bit  NOT NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)
		;



		CREATE TABLE "AttendeeInnovationOrganizationTracks"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"AttendeeInnovationOrganizationId" int  NOT NULL ,
			"InnovationOrganizationTrackOptionId" int  NOT NULL ,
			"AdditionalInfo"     varchar(200)  NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)
		;



		CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganizationTracks_AttendeeInnovationOrganizationId" ON "AttendeeInnovationOrganizationTracks"
		( 
			"AttendeeInnovationOrganizationId"  ASC
		)
		;



		CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganizationTracks_InnovationOrganizationTrackOptionId" ON "AttendeeInnovationOrganizationTracks"
		( 
			"InnovationOrganizationTrackOptionId"  ASC
		)
		;



		CREATE TABLE "InnovationOrganizationTechnologyOptions"
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
		;



		CREATE TABLE "AttendeeInnovationOrganizationTechnologies"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"AttendeeInnovationOrganizationId" int  NOT NULL ,
			"InnovationOrganizationTechnologyOptionId" int  NOT NULL ,
			"AdditionalInfo"     varchar(200)  NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)
		;



		CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganizationTechnologies_AttendeeInnovationOrganizationId" ON "AttendeeInnovationOrganizationTechnologies"
		( 
			"AttendeeInnovationOrganizationId"  ASC
		)
		;



		CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganizationTechnologies_InnovationOrganizationTechnologyOptionId" ON "AttendeeInnovationOrganizationTechnologies"
		( 
			"InnovationOrganizationTechnologyOptionId"  ASC
		)
		;



		CREATE TABLE "InnovationOrganizationObjectivesOptions"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"Name"               varchar(500)  NOT NULL ,
			"DisplayOrder"       int  NOT NULL ,
			"HasAdditionalInfo"  bit  NOT NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)
		;



		CREATE TABLE "AttendeeInnovationOrganizationObjectives"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"AttendeeInnovationOrganiaztionId" int  NOT NULL ,
			"InnovationOrganizationObjectiveOptionId" int  NOT NULL ,
			"AdditionalInfo"     varchar(200)  NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)
		;



		CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganizationObjectives_AttendeeInnovationOrganizationId" ON "AttendeeInnovationOrganizationObjectives"
		( 
			"AttendeeInnovationOrganiaztionId"  ASC
		)
		;



		CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganizationObjectives_InnovationOrganizationObjectiveOptionId" ON "AttendeeInnovationOrganizationObjectives"
		( 
			"InnovationOrganizationObjectiveOptionId"  ASC
		)
		;



		CREATE TABLE "AttendeeCollaboratorInnovationOrganizationTracks"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"AttendeeCollaboratorId" int  NOT NULL ,
			"InnovationOrganizationTrackOptionId" int  NOT NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)
		;



		CREATE NONCLUSTERED INDEX "IDX_AttendeeCollaboratorInnovationOrganizationTracks_AttendeeCollaboratorId" ON "AttendeeCollaboratorInnovationOrganizationTracks"
		( 
			"AttendeeCollaboratorId"  ASC
		)
		;



		CREATE NONCLUSTERED INDEX "IDX_AttendeeCollaboratorInnovationOrganizationTracks_InnovationOrganizationTrackOptionId" ON "AttendeeCollaboratorInnovationOrganizationTracks"
		( 
			"InnovationOrganizationTrackOptionId"  ASC
		)
		;



		ALTER TABLE "AttendeeInnovationOrganizationFounders"
		ADD CONSTRAINT "PK_AttendeeInnovationOrganizationFounders" PRIMARY KEY  CLUSTERED ("Id" ASC)
		;



		ALTER TABLE "AttendeeInnovationOrganizationFounders"
		ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizationFounders_Uid" UNIQUE ("Uid"  ASC)
		;



		ALTER TABLE "AttendeeInnovationOrganizationFounders"
		ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizationFounders_AttendeeInnovationOrganizationId_Fullname" UNIQUE ("AttendeeInnovationOrganizationId"  ASC,"Fullname"  ASC)
		;



		ALTER TABLE "AttendeeInnovationOrganizationExperiences"
		ADD CONSTRAINT "PK_AttendeeInnovationOrganizationExperiences" PRIMARY KEY  CLUSTERED ("Id" ASC)
		;



		ALTER TABLE "AttendeeInnovationOrganizationExperiences"
		ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizationExperiences_Uid" UNIQUE ("Uid"  ASC)
		;



		ALTER TABLE "InnovationOrganizationExperienceOptions"
		ADD CONSTRAINT "PK_InnovationOrganizationExperienceOptions" PRIMARY KEY  CLUSTERED ("Id" ASC)
		;



		ALTER TABLE "InnovationOrganizationExperienceOptions"
		ADD CONSTRAINT "IDX_UQ_InnovationOrganizationExperienceOptions_Uid" UNIQUE ("Uid"  ASC)
		;



		ALTER TABLE "AttendeeInnovationOrganizationCompetitors"
		ADD CONSTRAINT "PK_AttendeeInnovationOrganizationCompetitors" PRIMARY KEY  CLUSTERED ("Id" ASC)
		;



		ALTER TABLE "AttendeeInnovationOrganizationCompetitors"
		ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizationCompetitors_Uid" UNIQUE ("Uid"  ASC)
		;



		ALTER TABLE "InnovationOrganizationTrackOptions"
		ADD CONSTRAINT "PK_InnovationOrganizationTrackOptions" PRIMARY KEY  CLUSTERED ("Id" ASC)
		;



		ALTER TABLE "InnovationOrganizationTrackOptions"
		ADD CONSTRAINT "IDX_UQ_InnovationOrganizationTrackOptions_Uid" UNIQUE ("Uid"  ASC)
		;



		ALTER TABLE "AttendeeInnovationOrganizationTracks"
		ADD CONSTRAINT "PK_AttendeeInnovationOrganizationTracks" PRIMARY KEY  CLUSTERED ("Id" ASC)
		;



		ALTER TABLE "AttendeeInnovationOrganizationTracks"
		ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizationTracks_Uid" UNIQUE ("Uid"  ASC)
		;



		ALTER TABLE "InnovationOrganizationTechnologyOptions"
		ADD CONSTRAINT "PK_InnovationOrganizationTechnologyOptions" PRIMARY KEY  CLUSTERED ("Id" ASC)
		;



		ALTER TABLE "InnovationOrganizationTechnologyOptions"
		ADD CONSTRAINT "IDX_UQ_InnovationOrganizationTechnologyOptions_Uid" UNIQUE ("Uid"  ASC)
		;



		ALTER TABLE "AttendeeInnovationOrganizationTechnologies"
		ADD CONSTRAINT "PK_AttendeeInnovationOrganizationTechnologies" PRIMARY KEY  CLUSTERED ("Id" ASC)
		;



		ALTER TABLE "AttendeeInnovationOrganizationTechnologies"
		ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizationTechnologies_Uid" UNIQUE ("Uid"  ASC)
		;



		ALTER TABLE "InnovationOrganizationObjectivesOptions"
		ADD CONSTRAINT "PK_InnovationOrganizationObjectiveOptions" PRIMARY KEY  CLUSTERED ("Id" ASC)
		;



		ALTER TABLE "InnovationOrganizationObjectivesOptions"
		ADD CONSTRAINT "IDX_UQ_InnovationOrganizationObjectiveOptions_Uid" UNIQUE ("Uid"  ASC)
		;



		ALTER TABLE "AttendeeInnovationOrganizationObjectives"
		ADD CONSTRAINT "PK_AttendeeInnovationOrganizationObjectives" PRIMARY KEY  CLUSTERED ("Id" ASC)
		;



		ALTER TABLE "AttendeeInnovationOrganizationObjectives"
		ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizationObjectives_Uid" UNIQUE ("Uid"  ASC)
		;



		ALTER TABLE "AttendeeCollaboratorInnovationOrganizationTracks"
		ADD CONSTRAINT "PK_AttendeeCollaboratorInnovationOrganizationTracks" PRIMARY KEY  CLUSTERED ("Id" ASC)
		;



		ALTER TABLE "AttendeeCollaboratorInnovationOrganizationTracks"
		ADD CONSTRAINT "IDX_UQ_AttendeeCollaboratorInnovationOrganizationTracks_Uid" UNIQUE ("Uid"  ASC)
		;



		ALTER TABLE "AttendeeInnovationOrganizationFounders"
			ADD CONSTRAINT "FK_AttendeeInnovationOrganizations_AttendeeInnovationOrganizationFounders_AttendeeInnovationOrganizationId" FOREIGN KEY ("AttendeeInnovationOrganizationId") REFERENCES "dbo"."AttendeeInnovationOrganizations"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationFounders"
			ADD CONSTRAINT "FK_WorkDedications_AttendeeInnovationOrganizationFounders_WorkDedicationId" FOREIGN KEY ("WorkDedicationId") REFERENCES "dbo"."WorkDedications"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationFounders"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationFounders_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationFounders"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationFounders_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationExperiences"
			ADD CONSTRAINT "FK_AttendeeInnovationOrganizations_AttendeeInnovationOrganizationExperiences_AttendeeInnovationOrganizationId" FOREIGN KEY ("AttendeeInnovationOrganizationId") REFERENCES "dbo"."AttendeeInnovationOrganizations"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationExperiences"
			ADD CONSTRAINT "FK_InnovationOrganizationExperienceOptions_AttendeeInnovationOrganizationExperiences_InnovationOrganizationExperienceOptionId" FOREIGN KEY ("InnovationOrganizationExperienceOptionId") REFERENCES "dbo"."InnovationOrganizationExperienceOptions"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationExperiences"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationExperiences_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationExperiences"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationExperiences_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "InnovationOrganizationExperienceOptions"
			ADD CONSTRAINT "FK_Users_InnovationOrganizationExperienceOptions_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "InnovationOrganizationExperienceOptions"
			ADD CONSTRAINT "FK_Users_InnovationOrganizationExperienceOptions_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationCompetitors"
			ADD CONSTRAINT "FK_AttendeeInnovationOrganizations_AttendeeInnovationOrganizationCompetitors_AttendeeInnovationOrganizationId" FOREIGN KEY ("AttendeeInnovationOrganizationId") REFERENCES "dbo"."AttendeeInnovationOrganizations"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationCompetitors"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationCompetitors_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationCompetitors"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationCompetitors_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "InnovationOrganizationTrackOptions"
			ADD CONSTRAINT "FK_Users_InnovationOrganizationTrackOptions_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "InnovationOrganizationTrackOptions"
			ADD CONSTRAINT "FK_Users_InnovationOrganizationTrackOptions_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationTracks"
			ADD CONSTRAINT "FK_AttendeeInnovationOrganizations_AttendeeInnovationOrganizationTracks_AttendeeInnovationOrganizationId" FOREIGN KEY ("AttendeeInnovationOrganizationId") REFERENCES "dbo"."AttendeeInnovationOrganizations"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationTracks"
			ADD CONSTRAINT "FK_InnovationOrganizationTrackOptions_AttendeeInnovationOrganizationTracks_InnovationOrganizationTrackOptionId" FOREIGN KEY ("InnovationOrganizationTrackOptionId") REFERENCES "dbo"."InnovationOrganizationTrackOptions"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationTracks"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationTracks_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationTracks"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationTracks_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "InnovationOrganizationTechnologyOptions"
			ADD CONSTRAINT "FK_Users_InnovationOrganizationTechnologyOptions_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "InnovationOrganizationTechnologyOptions"
			ADD CONSTRAINT "FK_Users_InnovationOrganizationTechnologyOptions_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationTechnologies"
			ADD CONSTRAINT "FK_AttendeeInnovationOrganizations_AttendeeInnovationOrganizationTechnologies_AttendeeInnovationOrganizationId" FOREIGN KEY ("AttendeeInnovationOrganizationId") REFERENCES "dbo"."AttendeeInnovationOrganizations"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationTechnologies"
			ADD CONSTRAINT "FK_InnovationOrganizationTechnologyOptions_AttendeeInnovationOrganizationTechnologies_InnovationOrganizationTechnologyOptionId" FOREIGN KEY ("InnovationOrganizationTechnologyOptionId") REFERENCES "InnovationOrganizationTechnologyOptions"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationTechnologies"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationTechnologies_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationTechnologies"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationTechnologies_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "InnovationOrganizationObjectivesOptions"
			ADD CONSTRAINT "FK_Users_InnovationOrganizationObjectivesOptions_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "InnovationOrganizationObjectivesOptions"
			ADD CONSTRAINT "FK_Users_InnovationOrganizationObjectivesOptions_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationObjectives"
			ADD CONSTRAINT "FK_AttendeeInnovationOrganizations_AttendeeInnovationOrganizationObjectives_AttendeeInnovationOrganiaztionId" FOREIGN KEY ("AttendeeInnovationOrganiaztionId") REFERENCES "dbo"."AttendeeInnovationOrganizations"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationObjectives"
			ADD CONSTRAINT "FK_InnovationOrganizationObjectivesOptions_AttendeeInnovationOrganizationObjectives_InnovationOrganizationObjectiveOptionId" FOREIGN KEY ("InnovationOrganizationObjectiveOptionId") REFERENCES "InnovationOrganizationObjectivesOptions"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationObjectives"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationObjectives_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "AttendeeInnovationOrganizationObjectives"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationObjectives_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "AttendeeCollaboratorInnovationOrganizationTracks"
			ADD CONSTRAINT "FK_AttendeeCollaborators_AttendeeCollaboratorInnovationOrganizationTracks_AttendeeCollaboratorId" FOREIGN KEY ("AttendeeCollaboratorId") REFERENCES "dbo"."AttendeeCollaborators"("Id")
		;



		ALTER TABLE "AttendeeCollaboratorInnovationOrganizationTracks"
			ADD CONSTRAINT "FK_InnovationOrganizationTrackOptions_AttendeeCollaboratorInnovationOrganizationTracks_InnovationOrganizationTrackOptionId" FOREIGN KEY ("InnovationOrganizationTrackOptionId") REFERENCES "InnovationOrganizationTrackOptions"("Id")
		;



		ALTER TABLE "AttendeeCollaboratorInnovationOrganizationTracks"
			ADD CONSTRAINT "FK_Users_AttendeeCollaboratorInnovationOrganizationTracks_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
		;



		ALTER TABLE "AttendeeCollaboratorInnovationOrganizationTracks"
			ADD CONSTRAINT "FK_Users_AttendeeCollaboratorInnovationOrganizationTracks_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
		;

	-- Commands End
	COMMIT TRAN -- Transaction Success!
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN --RollBack in case of Error

	-- Raise ERROR with RAISEERROR() Statement including the details of the exception
	DECLARE @ErrorLine INT;
	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;

	SELECT
		@ErrorLine = ERROR_LINE(),
		@ErrorMessage = ERROR_MESSAGE(),
		@ErrorSeverity = ERROR_SEVERITY(),
		@ErrorState = ERROR_STATE();
		 
	RAISERROR ('Error found in line %i: %s', @ErrorSeverity, @ErrorState, @ErrorLine, @ErrorMessage) WITH SETERROR
END CATCH
