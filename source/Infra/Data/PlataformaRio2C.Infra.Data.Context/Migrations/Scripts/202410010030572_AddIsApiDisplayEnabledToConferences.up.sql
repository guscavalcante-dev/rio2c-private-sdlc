BEGIN TRY
	BEGIN TRANSACTION
		---------------------------
		-- Changes for Conferences
		---------------------------
		IF COL_LENGTH('dbo.Conferences', 'IsApiDisplayEnabled') IS NULL
		BEGIN
			ALTER TABLE "dbo"."Conferences"
			ADD IsApiDisplayEnabled BIT NULL;

			EXEC('UPDATE "dbo"."Conferences" SET IsApiDisplayEnabled = 1');

			ALTER TABLE "dbo"."Conferences"
			ALTER COLUMN IsApiDisplayEnabled BIT NOT NULL;
		END

		IF COL_LENGTH('dbo.Conferences', 'ApiHighlightPosition') IS NULL
		BEGIN
			ALTER TABLE "dbo"."Conferences"
			ADD ApiHighlightPosition CHAR(18) NULL;
		END

		---------------------------
		-- Changes for Editions
		---------------------------
		IF COL_LENGTH('dbo.Editions', 'ConferenceApiHighlightPositionsCount') IS NULL
		BEGIN
			ALTER TABLE "dbo"."Editions"
			ADD ConferenceApiHighlightPositionsCount INT NULL;

			EXEC('UPDATE "dbo"."Editions" SET ConferenceApiHighlightPositionsCount = 10');

			ALTER TABLE "dbo"."Editions"
			ALTER COLUMN ConferenceApiHighlightPositionsCount INT NOT NULL;
		END

	COMMIT TRAN -- Transaction Success!
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN -- RollBack in case of Error

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
		 
	RAISERROR ('Error found in line %i: %s', @ErrorSeverity, @ErrorState, @ErrorLine, @ErrorMessage) WITH SETERROR;
END CATCH
