--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION

		ALTER TABLE "dbo"."InnovationOrganizations" 
			DROP COLUMN "FoundationDate";
			
		ALTER TABLE "dbo"."InnovationOrganizations" 
			ADD "FoundationYear" int NULL

		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		ADD WouldYouLikeParticipateBusinessRound  bit  NOT NULL CONSTRAINT  DF_AttendeeInnovationOrganizations_WouldYouLikeParticipateBusinessRound
					DEFAULT  0

		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		ADD AccumulatedRevenueForLastTwelveMonths  decimal(12,2)  NULL

		CREATE TABLE "InnovationOrganizationSustainableDevelopmentObjectivesOptions"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"Name"               varchar(500)  NOT NULL ,
			"Description"        varchar(1000)  NOT NULL ,
			"DisplayOrder"       int  NOT NULL ,
			"HasAdditionalInfo"  bit  NOT NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)

		CREATE TABLE "AttendeeInnovationOrganizationSustainableDevelopmentObjectives"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NULL ,
			"AttendeeInnovationOrganizationId" int  NOT NULL ,
			"InnovationOrganizationSustainableDevelopmentObjectiveOptionId" int  NOT NULL ,
			"AdditionalInfo"     varchar(200)  NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)

		CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganizationSustainableDevelopmentObjectives_AttendeeInnovationOrganizationId" ON "AttendeeInnovationOrganizationSustainableDevelopmentObjectives"
		( 
			"AttendeeInnovationOrganizationId"  ASC
		)

		CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganizationSustainableDevelopmentObjectives_InnovationOrganizationSustainableDevelopmentObjectiveOptionId" ON "AttendeeInnovationOrganizationSustainableDevelopmentObjectives"
		( 
			"InnovationOrganizationSustainableDevelopmentObjectiveOptionId"  ASC
		)

		ALTER TABLE "InnovationOrganizationSustainableDevelopmentObjectivesOptions"
		ADD CONSTRAINT "PK_InnovationOrganizationSustainableDevelopmentObjectivesOptions" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "InnovationOrganizationSustainableDevelopmentObjectivesOptions"
		ADD CONSTRAINT "IDX_UQ_InnovationOrganizationSustainableDevelopmentObjectivesOptions_Uid" UNIQUE ("Uid"  ASC)

		ALTER TABLE "AttendeeInnovationOrganizationSustainableDevelopmentObjectives"
		ADD CONSTRAINT "PK_AttendeeInnovationOrganizationSustainableDevelopmentObjectives" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "AttendeeInnovationOrganizationSustainableDevelopmentObjectives"
		ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizationSustainableDevelopmentObjectives_Uid" UNIQUE ("Uid"  ASC)

		ALTER TABLE "InnovationOrganizationSustainableDevelopmentObjectivesOptions"
			ADD CONSTRAINT "FK_Users_InnovationOrganizationSustainableDevelopmentObjectivesOptions_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "InnovationOrganizationSustainableDevelopmentObjectivesOptions"
			ADD CONSTRAINT "FK_Users_InnovationOrganizationSustainableDevelopmentObjectivesOptions_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "AttendeeInnovationOrganizationSustainableDevelopmentObjectives"
			ADD CONSTRAINT "FK_AttendeeInnovationOrganizations_AttendeeInnovationOrganizationSustainableDevelopmentObjectives_AttendeeInnovationOrganization" FOREIGN KEY ("AttendeeInnovationOrganizationId") REFERENCES "dbo"."AttendeeInnovationOrganizations"("Id")

		ALTER TABLE "AttendeeInnovationOrganizationSustainableDevelopmentObjectives"
			ADD CONSTRAINT "FK_InnovationOrganizationSustainableDevelopmentObjectivesOptions_AttendeeInnovationOrganizationSustainableDevelopmentObjectives_" FOREIGN KEY ("InnovationOrganizationSustainableDevelopmentObjectiveOptionId") REFERENCES "InnovationOrganizationSustainableDevelopmentObjectivesOptions"("Id")

		ALTER TABLE "AttendeeInnovationOrganizationSustainableDevelopmentObjectives"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationSustainableDevelopmentObjectives_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "AttendeeInnovationOrganizationSustainableDevelopmentObjectives"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationSustainableDevelopmentObjectives_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

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