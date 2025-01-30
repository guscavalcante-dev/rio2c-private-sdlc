BEGIN TRY
    BEGIN TRANSACTION

    -- Remove o registro inserido pelo script .up utilizando apenas o Uid
    DELETE FROM [dbo].[Interests]
    WHERE [Uid] = '7EF2AF9A-BBD1-48FC-A2AB-7109B17C0FD4';

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