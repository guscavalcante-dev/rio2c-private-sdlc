BEGIN TRY
	BEGIN TRANSACTION
		ALTER TABLE "dbo"."EditionEvents"
		DROP CONSTRAINT "FK_Languages_EditionEvents_EditionId"

		ALTER TABLE "EditionEvents"
		ADD CONSTRAINT "FK_Editions_EditionEvents_EditionId" FOREIGN KEY ("EditionId") REFERENCES "dbo"."Editions"("Id")
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
