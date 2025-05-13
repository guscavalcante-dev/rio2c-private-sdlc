BEGIN TRY
	BEGIN TRANSACTION
		-- Commands Begin
		-- This is a sample when needed to execute script into a specific environment [dev (MyRio2C_Dev) | test (MyRio2C_Test) | prod (MyRio2C_Prod)]
		IF DB_NAME() = 'MyRio2C_Prod'
		BEGIN
			PRINT 'Script runs only into Production database';
		END
		ELSE
		BEGIN
			PRINT 'Unrecognized environment. Nothing will be executed.';
		END
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