BEGIN TRY
	BEGIN TRANSACTION
		---------------------------
		-- Changes for Conferences
		---------------------------
		ALTER TABLE "dbo"."Conferences"
		ADD IsApiDisplayEnabled  bit  NULL

		EXEC('UPDATE "dbo"."Conferences" SET IsApiDisplayEnabled = 1')

		ALTER TABLE "dbo"."Conferences"
		ALTER COLUMN IsApiDisplayEnabled  bit  NOT NULL

		ALTER TABLE "dbo"."Conferences"
		ADD ApiHighlightPosition  char(18)  NULL
	
		---------------------------
		-- Changes for Editions
		---------------------------
		ALTER TABLE "dbo"."Editions"
		ADD ConferenceApiHighlightPositionsCount  int NULL

		EXEC('UPDATE "dbo"."Editions" SET ConferenceApiHighlightPositionsCount = 10')

		ALTER TABLE "dbo"."Editions"
		ALTER COLUMN  ConferenceApiHighlightPositionsCount  int NOT NULL

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