BEGIN TRY
    BEGIN TRANSACTION

    -- Check if column exists before altering the table
    IF NOT EXISTS (
        SELECT 1 
        FROM INFORMATION_SCHEMA.COLUMNS 
        WHERE TABLE_NAME = 'MusicBusinessRoundProjectBuyerEvaluations' 
        AND COLUMN_NAME = 'AttendeeCollaboratorId'
    )
    BEGIN
        ALTER TABLE MusicBusinessRoundProjectBuyerEvaluations 
        ADD AttendeeCollaboratorId INT NULL;
    END

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
