BEGIN TRY
	BEGIN TRANSACTION

		IF COL_LENGTH('ProjectBuyerEvaluations', 'IsVirtualMeeting') IS NULL
		BEGIN
			ALTER TABLE ProjectBuyerEvaluations 
			ADD IsVirtualMeeting BIT NULL;

			EXEC('UPDATE ProjectBuyerEvaluations SET IsVirtualMeeting = 0');

			ALTER TABLE ProjectBuyerEvaluations 
			ALTER COLUMN IsVirtualMeeting BIT NOT NULL;
		END

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