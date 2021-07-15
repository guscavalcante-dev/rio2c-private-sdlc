--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION

	ALTER TABLE "dbo"."AttendeeSalesPlatforms"
	DROP CONSTRAINT "IDX_UQ_AttendeeSalesPlatforms_EditionId_SalesPlatformId"
	;

	ALTER TABLE "dbo"."AttendeeSalesPlatforms"
	ADD CONSTRAINT "IDX_UQ_AttendeeSalesPlatforms_EditionId_SalesPlatformId_SalesPlatformEventId" UNIQUE ("EditionId"  ASC,"SalesPlatformId"  ASC,"SalesPlatformEventId"  ASC)
	;

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
