BEGIN TRY
	BEGIN TRANSACTION
		-- add colums Deezer e Spotify in dbo.MusicBands table
		IF COL_LENGTH('MusicBands', 'Deezer') IS NULL
		BEGIN
			ALTER TABLE dbo.MusicBands ADD Deezer varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AI NULL;
		END

		IF COL_LENGTH('MusicBands', 'Spotify') IS NULL
		BEGIN
			ALTER TABLE dbo.MusicBands ADD Spotify varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AI NULL;
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
