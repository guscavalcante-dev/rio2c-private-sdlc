BEGIN TRY
	BEGIN TRANSACTION
		EXEC sys.sp_rename N'dbo.Editions.MusicProjectSubmitStartDate' , N'MusicPitchingSubmitStartDate', 'COLUMN';
		EXEC sys.sp_rename N'dbo.Editions.MusicProjectSubmitEndDate' , N'MusicPitchingSubmitEndDate', 'COLUMN';
        IF COL_LENGTH('Editions', 'MusicBusinessRoundSubmitStartDate') IS NULL
            ALTER TABLE dbo.Editions ADD MusicBusinessRoundSubmitStartDate datetimeoffset NULL;
        IF COL_LENGTH('Editions', 'MusicBusinessRoundSubmitEndDate') IS NULL
            ALTER TABLE dbo.Editions ADD MusicBusinessRoundSubmitEndDate datetimeoffset NULL;
        IF COL_LENGTH('Editions', 'MusicBusinessRoundEvaluationStartDate') IS NULL
            ALTER TABLE dbo.Editions ADD MusicBusinessRoundEvaluationStartDate datetimeoffset NULL;
        IF COL_LENGTH('Editions', 'MusicBusinessRoundEvaluationEndDate') IS NULL
            ALTER TABLE dbo.Editions ADD MusicBusinessRoundEvaluationEndDate datetimeoffset NULL;
        IF COL_LENGTH('Editions', 'MusicBusinessRoundNegotiationStartDate') IS NULL
            ALTER TABLE dbo.Editions ADD MusicBusinessRoundNegotiationStartDate datetimeoffset NULL;
        IF COL_LENGTH('Editions', 'MusicBusinessRoundNegotiationEndDate') IS NULL
            ALTER TABLE dbo.Editions ADD MusicBusinessRoundNegotiationEndDate datetimeoffset NULL;
		IF COL_LENGTH('Editions', 'MusicBusinessRoundsMaximumProjectSubmissionsByCompany') IS NULL
            ALTER TABLE dbo.Editions ADD MusicBusinessRoundsMaximumProjectSubmissionsByCompany int NULL;
		IF COL_LENGTH('Editions', 'MusicBusinessRoundMaximumEvaluatorsByProject') IS NULL
            ALTER TABLE dbo.Editions ADD MusicBusinessRoundMaximumEvaluatorsByProject int NULL;
		EXEC('UPDATE dbo.Editions SET MusicBusinessRoundsMaximumProjectSubmissionsByCompany=1, MusicBusinessRoundMaximumEvaluatorsByProject=3');
		EXEC('ALTER TABLE dbo.Editions ALTER COLUMN MusicBusinessRoundsMaximumProjectSubmissionsByCompany int NOT NULL');
		EXEC('ALTER TABLE dbo.Editions ALTER COLUMN MusicBusinessRoundMaximumEvaluatorsByProject int NOT NULL');
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