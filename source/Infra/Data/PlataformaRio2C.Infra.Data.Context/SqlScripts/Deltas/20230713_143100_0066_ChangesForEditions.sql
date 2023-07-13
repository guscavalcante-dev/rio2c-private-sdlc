--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION

		ALTER TABLE "dbo"."Editions" 
		ALTER COLUMN "CartoonProjectSubmitStartDate" datetimeoffset NULL

		ALTER TABLE "dbo"."Editions" 
		ALTER COLUMN "CartoonProjectSubmitEndDate" datetimeoffset NULL

		ALTER TABLE "dbo"."Editions" 
		ALTER COLUMN "CartoonCommissionEvaluationStartDate" datetimeoffset NULL

		ALTER TABLE "dbo"."Editions" 
		ALTER COLUMN "CartoonCommissionEvaluationEndDate" datetimeoffset NULL

		ALTER TABLE "dbo"."Editions" 
		ALTER COLUMN "CartoonCommissionMinimumEvaluationsCount" int NULL

		ALTER TABLE "dbo"."Editions" 
		ALTER COLUMN "CartoonCommissionMaximumApprovedProjectsCount" int NULL
		
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