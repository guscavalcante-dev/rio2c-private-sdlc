BEGIN TRY
	BEGIN TRANSACTION
		-- deleting colums Deezer and Spotify from dbo.MusicBands
		IF COL_LENGTH('MusicBands', 'Deezer') IS NOT NULL
		BEGIN
			ALTER TABLE dbo.MusicBands DROP COLUMN Deezer;
		END

		IF COL_LENGTH('MusicBands', 'Spotify') IS NOT NULL
		BEGIN
			ALTER TABLE dbo.MusicBands DROP COLUMN Spotify;
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
		 
	RAISERROR ('Error found in line %i: %s', @ErrorSeverity, @ErrorState, @ErrorLine, @ErrorMessage) WITH SETERROR
END CATCH
