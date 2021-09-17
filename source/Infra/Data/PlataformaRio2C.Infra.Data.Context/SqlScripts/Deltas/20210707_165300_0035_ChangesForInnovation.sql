--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION
		-- Renames for "dbo"."AttendeeInnovationOrganization"
		execute sp_rename '"dbo"."AttendeeInnovationOrganization"', 'AttendeeInnovationOrganizations', 'OBJECT'
		execute sp_rename '"IDX_UQ_AttendeeInnovationOrganization_Uid"', 'IDX_UQ_AttendeeInnovationOrganizations_Uid', 'OBJECT'
		execute sp_rename '"IDX_UQ_AttendeeInnovationOrganization_EditionId_InnovationOrganizationId"', 'IDX_UQ_AttendeeInnovationOrganizations_EditionId_InnovationOrganizationId', 'OBJECT'
		execute sp_rename '"dbo"."AttendeeInnovationOrganizations"."IDX_AttendeeInnovationOrganization_EvaluationuserId"', 'IDX_AttendeeInnovationOrganizations_EvaluationuserId', 'INDEX'
		execute sp_rename '"dbo"."AttendeeInnovationOrganizations"."IDX_AttendeeInnovationOrganization_InnovationOrganizationId"', 'IDX_AttendeeInnovationOrganizations_InnovationOrganizationId', 'INDEX'
		execute sp_rename '"dbo"."AttendeeInnovationOrganizations"."IDX_AttendeeInnovationOrganization_ProjectEvaluationStatusId"', 'IDX_AttendeeInnovationOrganizations_ProjectEvaluationStatusId', 'INDEX'


		-- Drops for "dbo"."AttendeeInnovationOrganizationCollaborators"
		ALTER TABLE "dbo"."AttendeeInnovationOrganizationCollaborators"
		DROP CONSTRAINT "FK_AttendeeInnovationOrganization_AttendeeInnovationOrganizationCollaborators_AttendeeInnovationOrganizationId"


		-- Drops for "dbo".""AttendeeInnovationOrganizations""
		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		DROP CONSTRAINT "FK_ProjectEvaluationStatuses_AttendeeInnovationOrganization_ProjectEvaluationStatusId"

		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		DROP CONSTRAINT "FK_ProjectEvaluationRefuseReasons_AttendeeInnovationOrganization_ProjectEvaluationRefuseReasonId"

		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		DROP CONSTRAINT "FK_Users_AttendeeInnovationOrganization_EvaluationUserId"

		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		DROP CONSTRAINT "PK_AttendeeInnovationOrganization"

		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		DROP CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizations_Uid"

		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		DROP CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizations_EditionId_InnovationOrganizationId"

		DROP INDEX [IDX_AttendeeInnovationOrganizations_ProjectEvaluationStatusId] ON [dbo].[AttendeeInnovationOrganizations]
		DROP INDEX [IDX_AttendeeInnovationOrganizations_EvaluationuserId] ON [dbo].[AttendeeInnovationOrganizations]


		-- Drops for "dbo"."AttendeeInnovationOrganizations"
		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		  DROP COLUMN "ProjectEvaluationStatusId"

		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		  DROP COLUMN "Reason"

		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		  DROP COLUMN "ProjectEvaluationRefuseReasonId"

		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		  DROP COLUMN "EvaluationUserId"


		-- Changes for "dbo"."Editions"
		ALTER TABLE "dbo"."Editions"
		ADD InnovationProjectMinimumEvaluationsCount  int  NULL

		EXEC('UPDATE dbo.Editions set InnovationProjectMinimumEvaluationsCount = 3')

		ALTER TABLE "dbo"."Editions"
		ALTER COLUMN InnovationProjectMinimumEvaluationsCount  int  NOT NULL

		ALTER TABLE "dbo"."Editions"
		ADD InnovationProjectMaximumApprovedCompaniesCount  int  NULL

		EXEC('UPDATE dbo.Editions set InnovationProjectMaximumApprovedCompaniesCount = 3')

		ALTER TABLE "dbo"."Editions"
		ALTER COLUMN InnovationProjectMaximumApprovedCompaniesCount  int  NOT NULL


		-- Changes for "dbo"."AttendeeInnovationOrganizations"
		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		ADD Grade  decimal(4,2)  NULL

		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		ADD EvaluationsCount  int  NOT NULL CONSTRAINT  DF_AttendeeInnovationOrganizations_EvaluationsCount
					DEFAULT  0

		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
		ADD LastEvaluationDate  datetimeoffset  NULL


		-- Changes for "dbo"."AttendeeInnovationOrganizations"
		CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganizations_EditionId_Grade" ON "dbo"."AttendeeInnovationOrganizations"
		( 
			"EditionId"           ASC,
			"Grade"               ASC
		)

		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
			ADD CONSTRAINT "PK_AttendeeInnovationOrganizations" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
			ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizations_Uid" UNIQUE ("Uid"  ASC)

		ALTER TABLE "dbo"."AttendeeInnovationOrganizations"
			ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizations_EditionId_InnovationOrganizationId" UNIQUE ("EditionId"  ASC,"InnovationOrganizationId"  ASC)


		-- Changes for "dbo"."AttendeeInnovationOrganizationCollaborators"
		ALTER TABLE "dbo"."AttendeeInnovationOrganizationCollaborators"
			ADD CONSTRAINT "FK_AttendeeInnovationOrganizations_AttendeeInnovationOrganizationCollaborators_AttendeeInnovationOrganizationId" FOREIGN KEY ("AttendeeInnovationOrganizationId") REFERENCES "dbo"."AttendeeInnovationOrganizations"("Id")


		-- Create for "dbo"."AttendeeInnovationOrganizationEvaluations"
		CREATE TABLE "AttendeeInnovationOrganizationEvaluations"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"AttendeeInnovationOrganizationId" int  NOT NULL ,
			"EvaluatorUserId"    int  NOT NULL ,
			"Grade"              decimal(4,2)  NOT NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)

		CREATE NONCLUSTERED INDEX "IDX_AttendeeInnovationOrganizationEvaluations_EvaluatorUserId" ON "AttendeeInnovationOrganizationEvaluations"
		( 
			"EvaluatorUserId"     ASC
		)

		ALTER TABLE "AttendeeInnovationOrganizationEvaluations"
			ADD CONSTRAINT "PK_AttendeeInnovationOrganizationEvaluations" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "AttendeeInnovationOrganizationEvaluations"
			ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizationEvaluations" UNIQUE ("AttendeeInnovationOrganizationId"  ASC,"EvaluatorUserId"  ASC)

		ALTER TABLE "AttendeeInnovationOrganizationEvaluations"
			ADD CONSTRAINT "IDX_UQ_AttendeeInnovationOrganizationEvaluations_Uid" UNIQUE ("Uid"  ASC)

		ALTER TABLE "AttendeeInnovationOrganizationEvaluations"
			ADD CONSTRAINT "FK_AttendeeInnovationOrganizations_AttendeeInnovationOrganizationEvaluations_AttendeeInnovationOrganizationId" FOREIGN KEY ("AttendeeInnovationOrganizationId") REFERENCES "dbo"."AttendeeInnovationOrganizations"("Id")

		ALTER TABLE "AttendeeInnovationOrganizationEvaluations"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationEvaluations_EvaluatorUserId" FOREIGN KEY ("EvaluatorUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "AttendeeInnovationOrganizationEvaluations"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationEvaluations_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "AttendeeInnovationOrganizationEvaluations"
			ADD CONSTRAINT "FK_Users_AttendeeInnovationOrganizationEvaluations_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

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
