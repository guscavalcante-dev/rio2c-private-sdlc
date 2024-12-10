BEGIN TRY
	BEGIN TRANSACTION
		IF COL_LENGTH('AttendeeCollaborators', 'AudiovisualProducerBusinessRoundTermsAcceptanceDate') IS NOT NULL
		BEGIN
			EXEC sys.sp_rename N'dbo.AttendeeCollaborators.AudiovisualProducerBusinessRoundTermsAcceptanceDate' , N'ProducerTermsAcceptanceDate', 'COLUMN';
		END
		IF COL_LENGTH('AttendeeCollaborators', 'AudiovisualProducerPitchingTermsAcceptanceDate') IS NOT NULL
		BEGIN
			ALTER TABLE dbo.AttendeeCollaborators DROP COLUMN AudiovisualProducerPitchingTermsAcceptanceDate;
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
