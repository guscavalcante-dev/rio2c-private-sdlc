BEGIN TRY
    BEGIN TRANSACTION
		EXEC sys.sp_rename N'dbo.Editions.MusicPitchingSubmitStartDate' , N'MusicProjectSubmitStartDate', 'COLUMN';
		EXEC sys.sp_rename N'dbo.Editions.MusicPitchingSubmitEndDate' , N'MusicProjectSubmitEndDate', 'COLUMN';
		ALTER TABLE dbo.Editions DROP COLUMN MusicBusinessRoundSubmitStartDate;
		ALTER TABLE dbo.Editions DROP COLUMN MusicBusinessRoundSubmitEndDate;
        ALTER TABLE dbo.Editions DROP COLUMN MusicBusinessRoundEvaluationStartDate;
		ALTER TABLE dbo.Editions DROP COLUMN MusicBusinessRoundEvaluationEndDate;
		ALTER TABLE dbo.Editions DROP COLUMN MusicBusinessRoundNegotiationStartDate;
		ALTER TABLE dbo.Editions DROP COLUMN MusicBusinessRoundNegotiationEndDate;
		ALTER TABLE dbo.Editions DROP COLUMN MusicBusinessRoundsMaximumProjectSubmissionsByCompany;
		ALTER TABLE dbo.Editions DROP COLUMN MusicBusinessRoundMaximumEvaluatorsByProject;
    COMMIT TRAN -- Transaction Success!
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN -- Rollback in case of error

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
END CATCH;
