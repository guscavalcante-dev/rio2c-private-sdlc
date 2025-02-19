BEGIN TRY
	BEGIN TRANSACTION

		ALTER TABLE CreatorProjects DROP COLUMN Synopsis
		ALTER TABLE CreatorProjects DROP COLUMN Clipping
		ALTER TABLE CreatorProjects ALTER COLUMN ProjectAwards varchar(300)
		ALTER TABLE CreatorProjects ALTER COLUMN ProjectPublicNotice varchar(300)
		ALTER TABLE CreatorProjects ALTER COLUMN PreviouslyDevelopedProjects varchar(300)
		ALTER TABLE CreatorProjects ALTER COLUMN Synopsis varchar(300)

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