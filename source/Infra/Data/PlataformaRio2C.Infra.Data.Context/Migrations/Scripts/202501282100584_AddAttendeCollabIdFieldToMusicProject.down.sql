BEGIN TRY
    BEGIN TRANSACTION

        -- Remove the column AttendeeCollaboratorId from MusicBusinessRoundProjectBuyerEvaluations table
        ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations" 
        DROP COLUMN "AttendeeCollaboratorId";

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
