BEGIN TRY
    BEGIN TRANSACTION

        -- Add new columns to Editions table
        ALTER TABLE "Editions" 
        ADD "AudiovisualPitchingSubmitStartDate" datetimeoffset NULL,
            "AudiovisualPitchingSubmitEndDate" datetimeoffset NULL;

    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    -- Re-raise the error
    DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
    SELECT 
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH;
