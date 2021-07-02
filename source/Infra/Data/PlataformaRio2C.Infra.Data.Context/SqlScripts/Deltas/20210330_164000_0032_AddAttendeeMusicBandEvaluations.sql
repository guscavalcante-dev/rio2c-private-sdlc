--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION
		DROP INDEX [IDX_MusicProjects_ProjectEvaluationStatusId] ON [dbo].[MusicProjects]
		;
		
		DROP INDEX [IDX_MusicProjects_EvaluationUserId] ON [dbo].[MusicProjects]
		;

		ALTER TABLE "dbo"."MusicProjects"
		DROP CONSTRAINT "FK_ProjectEvaluationStatuses_MusicProjects_ProjectEvaluationStatusId"
		;

		ALTER TABLE "dbo"."MusicProjects"
		DROP CONSTRAINT "FK_Users_MusicProjects_EvaluationUserId"
		;

		ALTER TABLE "dbo"."MusicProjects"
		DROP CONSTRAINT "FK_ProjectEvaluationRefuseReasons_MusicProjects_ProjectEvaluationRefuseReasonId"
		;

		ALTER TABLE "dbo"."MusicProjects"
		  DROP COLUMN "ProjectEvaluationStatusId"
		;

		ALTER TABLE "dbo"."MusicProjects"
		  DROP COLUMN "Reason"
		;

		ALTER TABLE "dbo"."MusicProjects"
		  DROP COLUMN "EvaluationUserId"
		;

		ALTER TABLE "dbo"."MusicProjects"
		  DROP COLUMN "EvaluationEmailSendDate"
		;

		ALTER TABLE "dbo"."MusicProjects"
		  DROP COLUMN "ProjectEvaluationRefuseReasonId"
		;

		ALTER TABLE "dbo"."MusicProjects"
		  DROP COLUMN "EvaluationDate"
		;

		ALTER TABLE "dbo"."Editions"
		ADD MusicProjectMinimumEvaluationsCount  int  NULL
		;

		EXEC('UPDATE dbo.Editions set MusicProjectMinimumEvaluationsCount = 3')
		;

		ALTER TABLE "dbo"."Editions"
		ALTER COLUMN MusicProjectMinimumEvaluationsCount  int  NOT NULL
		;

		ALTER TABLE "dbo"."Editions"
		ADD MusicProjectMaximumApprovedBandsCount  int  NULL
		;

		EXEC('UPDATE dbo.Editions set MusicProjectMaximumApprovedBandsCount = 10')
		;

		ALTER TABLE "dbo"."Editions"
		ALTER COLUMN MusicProjectMaximumApprovedBandsCount  int  NOT NULL
		;


		ALTER TABLE "dbo"."AttendeeMusicBands"
		ADD Grade  decimal(4,2)  NULL
		;

		ALTER TABLE "dbo"."AttendeeMusicBands"
		ADD EvaluationsCount  int  NOT NULL CONSTRAINT  DF_AttendeeMusicBands_EvaluationsCount
				 DEFAULT  0
		;

		ALTER TABLE "dbo"."AttendeeMusicBands"
		ADD LastEvaluationDate  datetimeoffset  NULL
		;

		ALTER TABLE "dbo"."AttendeeMusicBands"
		ADD EvaluationEmailSendDate  datetimeoffset  NULL
		;

		CREATE TABLE "AttendeeMusicBandEvaluations"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"AttendeeMusicBandId" int  NOT NULL ,
			"EvaluatorUserId"    int  NOT NULL ,
			"Grade"              decimal(4,2)  NOT NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)
		;

		CREATE NONCLUSTERED INDEX "IDX_AttendeeMusicBandEvaluations_AttendeeMusicBandId" ON "AttendeeMusicBandEvaluations"
		( 
			"AttendeeMusicBandId"  ASC
		)
		;

		CREATE NONCLUSTERED INDEX "IDX_AttendeeMusicBands_EditionId_Grade_LastEvaluationDate" ON "dbo"."AttendeeMusicBands"
		( 
			"EditionId"           ASC,
			"Grade"               DESC,
			"LastEvaluationDate"  ASC
		)
		;

		ALTER TABLE "AttendeeMusicBandEvaluations"
		ADD CONSTRAINT "PK_AttendeeMusicBandEvaluations" PRIMARY KEY  CLUSTERED ("Id" ASC)
		;

		ALTER TABLE "AttendeeMusicBandEvaluations"
			ADD CONSTRAINT "FK_AttendeeMusicBands_AttendeeMusicBandEvaluations_AttendeeMusicBandId" FOREIGN KEY ("AttendeeMusicBandId") REFERENCES "dbo"."AttendeeMusicBands"("Id")
		;

		ALTER TABLE "AttendeeMusicBandEvaluations"
			ADD CONSTRAINT "FK_Users_AttendeeMusicBandEvaluations_EvaluatorUserId" FOREIGN KEY ("EvaluatorUserId") REFERENCES "dbo"."Users"("Id")
		;

		ALTER TABLE "AttendeeMusicBandEvaluations"
			ADD CONSTRAINT "FK_Users_AttendeeMusicBandEvaluations_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
		;

		ALTER TABLE "AttendeeMusicBandEvaluations"
			ADD CONSTRAINT "FK_Users_AttendeeMusicBandEvaluations_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id");
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
