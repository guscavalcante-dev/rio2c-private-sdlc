BEGIN TRY
	BEGIN TRANSACTION

		UPDATE Interests set Name = 'Sinopse em até 3 páginas de podcast, ficção, documentário/factual e animação | Synopsis of up to 3 pages for podcast, fiction, documentary/factual and animation' 
		WHERE Uid = '70F460AB-2ED7-4254-82E7-561982E4DA73'

		UPDATE Interests set Name = 'Artigos publicados online | Articles published online' 
		WHERE Uid = '64B4834F-AC2E-498F-8B07-5343BD634F61'

		UPDATE Interests set Name = 'Contos publicados online | Short stories published online' 
		WHERE Uid = 'E174ECE0-8A89-43AD-9912-590C0F0A7C0B'

		UPDATE Interests set Name = 'Animação | Animation' 
		WHERE Uid = 'FD691FB9-06A0-4983-9FDC-167FB7935B87'

		-- Add new fields
		ALTER TABLE CreatorProjects ADD Synopsis varchar(600)
		ALTER TABLE CreatorProjects ADD Clipping varchar(600)
		ALTER TABLE CreatorProjects ALTER COLUMN PreviouslyDevelopedProjects varchar(1024)

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