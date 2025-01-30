BEGIN TRY
    BEGIN TRANSACTION

        -- Alter column Value in MusicBusinessRoundProjectExpectationsForMeetings to have a max length of 3000
        ALTER TABLE "MusicBusinessRoundProjectExpectationsForMeetings"
        ALTER COLUMN "Value" nvarchar(3000) NOT NULL;

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
